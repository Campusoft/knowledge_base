# vLLM y SGLang

 motores de inferencia de LLM enfocados en alto rendimiento y producción. Ambos resuelven el mismo problema central: servir modelos de lenguaje de forma eficiente bajo carga concurrente, gestionando la memoria KV cache y maximizando el uso de la GPU.

- vLLM: https://github.com/vllm-project/vllm
- SGLang: https://github.com/sgl-project/sglang
- Docs vLLM: https://docs.vllm.ai/
- Docs SGLang: https://sgl-project.github.io/

---

# vLLM

## Descripción

vLLM es un motor de inferencia y serving de LLM de código abierto, desarrollado originalmente en UC Berkeley. Su pieza clave es **PagedAttention**, un algoritmo de atención inspirado en la memoria virtual y el paginado de los sistemas operativos.

## Características

- **PagedAttention**: gestiona la KV cache en bloques fijos (páginas), reduciendo la fragmentación de memoria a casi cero (<4% de desperdicio).
- **Continuous batching**: agrupa continuamente peticiones entrantes y salientes sin reconstruir el lote completo, mejorando el throughput 2-4x frente a sistemas anteriores.
- **Prefix caching**: reutiliza la KV cache de prefijos comunes entre peticiones (system prompts, contexts de RAG).
- **Chunked prefill**: divide el prefill en trozos para intercalarlo con el decode y reducir la latencia percibida.
- **Speculative decoding**: decodificación especulativa con n-gram, EAGLE, DFlash, etc.
- **Quantización**: FP8, INT8, INT4, GPTQ, AWQ, GGUF, compressed-tensors y más.
- **Paralelismo**: tensor parallelism, pipeline parallelism, disaggregated prefill/decode.
- **API compatible con OpenAI**: expone endpoints `/v1/chat/completions`, `/v1/completions`, `/v1/models`.
- **Soporte amplio de modelos**: LLaMA, Qwen, Mistral, DeepSeek, Gemma, modelos multimodales, etc.

## Arquitectura

```
Cliente (API OpenAI-compatible)
        │
        ▼
┌─────────────────────┐
│   API Server        │  FastAPI / OpenAI-compatible
└─────────┬───────────┘
          │
          ▼
┌─────────────────────┐
│  Centralized        │  Planifica el scheduling de peticiones
│  Scheduler          │  (continuous batching, preemptive scheduling)
└─────────┬───────────┘
          │
          ▼
┌─────────────────────┐
│  KV Cache Manager   │  Gestiona bloques KV (block tables,
│  (PagedAttention)   │  lógico → físico, copy-on-write)
└─────────┬───────────┘
          │
          ▼
┌─────────────────────┐
│  GPU Workers        │  Ejecutan el modelo con kernels optimizados
│  (N GPUs)           │  FlashAttention, FlashInfer, TRTLLM-GEN...
└─────────────────────┘
```

Puntos clave de la arquitectura:

1. **Scheduler centralizado**: coordina todos los GPU workers y decide qué peticiones entran en el lote actual.
2. **KV Cache Manager**: organiza la cache como bloques de tamaño fijo (como páginas de memoria virtual); los bloques lógicos contiguos se mapean a bloques físicos no contiguos.
3. **Copy-on-write**: permite compartir bloques KV entre secuencias (parallel sampling, beam search) de forma segura.
4. **Workers distribuidos**: soporta tensor parallelism para modelos que no caben en una GPU.

## Casos de uso

- APIs de inferencia en producción con alta concurrencia.
- Servicios multi-usuario con alto throughput (chatbots, copilots).
- Serving de modelos grandes distribuidos en múltiples GPUs.
- Endpoints compatibles con OpenAI para migrar desde APIs comerciales.

## Recomendaciones

- **Elegir vLLM cuando** se necesita máximo throughput, soporte de tensor parallelism maduro, o se proviene de un ecosistema que ya usa su API.
- Es la opción más madura y con mayor comunidad para serving genérico en producción.
- Para empezar: `vllm serve <modelo>` expone un servidor OpenAI-compatible de inmediato.

```bash
# Instalar
pip install vllm

# Servir un modelo con API compatible con OpenAI
vllm serve meta-llama/Llama-3.1-8B-Instruct --port 8000
```

---

# SGLang

## Descripción

SGLang (Structured Generation Language) es un sistema de alto rendimiento para ejecutar programas complejos de LLM. Incluye un **frontend** (lenguaje/DSL) y un **runtime** optimizado. Su pieza clave es **RadixAttention**, para reutilización automática de la KV cache.

## Características

- **RadixAttention**: gestiona la KV cache en un radix tree (árbol de prefijos) con política LRU, reutilizando prefijos compartidos entre llamadas y peticiones de forma automática.
- **Cache-aware scheduling**: prioriza peticiones con prefijos más largos en común para maximizar el cache hit rate.
- **Structured outputs acelerados**: decodificación de JSON, regex y finite state machines comprimidas (hasta 6.4x más throughput en tareas estructuradas).
- **Frontend DSL**: primitivas como `gen`, `fork`, `select` para programar flujos de generación paralela.
- **Continuous batching y paged attention**: mismas técnicas de base que vLLM.
- **Prefill-decode disaggregation**: separa las fases de prefill y decode en distintos recursos.
- **Speculative decoding, multi-LoRA batching, chunked prefill**.
- **Quantización**: FP4, FP8, INT4, AWQ, GPTQ.
- **Paralelismo**: tensor, pipeline, expert (MoE), data parallelism.
- **Multimodal**: soporte de modelos de lenguaje y multimodales.

## Arquitectura

```
Frontend (SGLang DSL / API)
  gen(), fork(), select(), structured output...
        │
        ▼
┌──────────────────────────┐
│  SGLang Runtime          │
│  ┌────────────────────┐  │
│  │ Cache-aware         │  │  Ordena por prefijo compartido
│  │ Scheduler           │  │
│  └─────────┬──────────┘  │
│            ▼             │
│  ┌────────────────────┐  │
│  │ Radix Tree Cache   │  │  Mapea secuencias de tokens → KV cache
│  │ (LRU + reference    │  │  (páginas no contiguas, unbounded prefix
│  │  counting)          │  │   matching, eviction)
│  └─────────┬──────────┘  │
│            ▼             │
│  ┌────────────────────┐  │
│  │ Attention Backends │  │  FlashInfer, FlashAttention...
│  │ + GPU Workers      │  │
│  └────────────────────┘  │
└──────────────────────────┘
```

Puntos clave:

1. **Radix tree**: a diferencia del prefix caching simple, mantiene un árbol de prefijos completo; cualquier prefijo (no solo el más reciente) puede reutilizarse, con eviction LRU por hojas.
2. **Páginas por token**: cada página equivale a un token, lo que permite matching de prefijos a nivel de token.
3. **Reference counting**: los nodos en uso por peticiones activas no se evictan; se comparte memoria entre caché y peticiones en ejecución.
4. **Unified Radix Cache** (2026): unifica en un solo árbol distintos tipos de caché (full attention, sliding window, Mamba) para modelos híbridos.

## Casos de uso

- Programas complejos de LLM: agentes, multi-turno, few-shot, RAG con muchos prefijos compartidos.
- Salidas estructuradas intensivas (JSON, extracción de datos, tool calling).
- Workloads donde muchas peticiones comparten system prompt o contexto.
- Alta concurrencia con baja latencia en un solo GPU o clústeres distribuidos.

## Recomendaciones

- **Elegir SGLang cuando** el workload tiene mucho aprovechamiento de prefijos compartidos, requiere structured output intensivo, o se trabaja con agentes/multi-turno.
- Rinde especialmente bien en benchmarks de agent control, RAG pipelines y chat multi-turno.
- También expone API compatible con OpenAI.

```bash
# Instalar
pip install "sglang[all]"

# Servir un modelo
python -m sglang.launch_server --model meta-llama/Llama-3.1-8B-Instruct --port 8000
```

---

# Comparación rápida: vLLM vs SGLang

| Aspecto | vLLM | SGLang |
|---|---|---|
| Enfoque | Motor de serving genérico | Programación de LLM + serving |
| Pieza clave | PagedAttention | RadixAttention |
| Reutilización de caché | Prefix caching | Radix tree (cualquier prefijo, LRU) |
| Structured output | Estándar | Optimizado (FSM comprimido) |
| Frontend/DSL | No (solo API) | Sí (gen, fork, select...) |
| Madurez de ecosistema | Muy amplia | En crecimiento rápido |
| Mejor para | Throughput general, multi-GPU | Prefijos compartidos, agentes, JSON |

## ¿Cuándo elegir uno u otro?

- **vLLM**: por defecto para serving estándar en producción, máxima compatibilidad y ecosistema maduro.
- **SGLang**: si el workload aprovecha prefijos compartidos de forma intensiva, se necesita structured output agresivo, o se quiere el frontend DSL para programas de LLM complejos.
- En la práctica ambos están muy cerca en rendimiento; la decisión suele depender del caso de uso concreto y del ecosistema existente.

---

# Referencias

- vLLM paper (SOSP 2023): Efficient Memory Management for Large Language Model Serving with PagedAttention — https://arxiv.org/abs/2309.06180
- SGLang paper (NeurIPS 2024): SGLang: Efficient Execution of Structured Language Model Programs — https://proceedings.neurips.cc/paper_files/paper/2024/file/724be4472168f31ba1c9ac630f15dec8-Paper-Conference.pdf
- vLLM blog: https://vllm.ai/blog/2023-06-20-vllm
- Unified Radix Cache (SGLang, 2026): https://www.lmsys.org/blog/2026-08-11-unified-radix-cache/
