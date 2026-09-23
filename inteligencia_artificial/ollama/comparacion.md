# Ollama vs vLLM/SGLang: cuándo usar cada uno

Comparación práctica entre Ollama (local, sencillo) y los motores de serving de alto rendimiento vLLM y SGLang (producción, concurrencia).

Documentos relacionados:

- [vLLM y SGLang](../vllm_sglang.md)
- [Instalación de Ollama](instalacion.md)

---

# Resumen ejecutivo

- **Ollama**: mejor para uso local, desarrollo, experimentación y equipos pequeños. Prioriza sencillez sobre rendimiento concurrente.
- **vLLM / SGLang**: mejores para producción multi-usuario, alto throughput y baja latencia bajo carga. Priorizan rendimiento y eficiencia de GPU.

---

# Tabla comparativa

| Criterio | Ollama | vLLM | SGLang |
|---|---|---|---|
| Objetivo principal | Ejecutar LLMs en local con facilidad | Serving de alto throughput | Serving + programas complejos de LLM |
| Curva de aprendizaje | Muy baja | Media | Media-alta |
| Instalación | `ollama run modelo` | `pip install vllm` | `pip install "sglang[all]"` |
| Hardware típico | CPU, 1 GPU consumer | 1+ GPUs (data center o pro) | 1+ GPUs |
| Concurrencia multi-usuario | Limitada | Excelente | Excelente |
| Throughput bajo carga | Bajo-medio | Muy alto | Muy alto |
| Gestión de KV cache | Básica | PagedAttention | RadixAttention |
| Prefix caching | Básico | Sí | Avanzado (radix tree) |
| API OpenAI-compatible | Sí | Sí | Sí |
| Quantización | GGUF (cuantización local) | FP8/INT4/GPTQ/AWQ... | FP8/INT4/GPTQ/AWQ... |
| Multi-GPU / tensor parallel | Muy limitado | Sí | Sí |
| Gestión de modelos | Integrada (registry Ollama) | Hugging Face | Hugging Face |
| Ideal para | Local, dev, prototipos | Producción, APIs | Producción, agentes, structured output |

---

# Ollama: cuándo usarlo

## Mejor opción cuando:

- **Uso local en un equipo de desarrollo**: un `ollama run llama3.2` resuelve sin configurar servidores.
- **Prototipado rápido**: probar modelos, prompts o integraciones sin preocuparse por infraestructura.
- **Privacidad/datos sensibles en el propio equipo**: inferencia 100% local sin salir de la máquina.
- **Equipos pequeños o tráfico bajo**: pocas peticiones concurrentes, no se necesita máximo throughput.
- **Hardware limitado**: CPU o una GPU consumer; Ollama optimiza para ese escenario con GGUF cuantizado.
- **Gestión de modelos simple**: descarga, lista y elimina modelos con un comando.

## Limitaciones a tener en cuenta:

- No está pensado para servir a muchos usuarios simultáneos con baja latencia.
- El scheduling y la gestión de memoria KV no están optimizados para alta concurrencia.
- Multi-GPU y paralelismo de tensores muy limitados frente a vLLM/SGLang.
- Menor control fino sobre batching, kernels y cuantización avanzada.

---

# vLLM: cuándo usarlo

## Mejor opción cuando:

- **Producción con tráfico real**: APIs, chatbots o copilots con múltiples usuarios simultáneos.
- **Máximo throughput por GPU**: continuous batching + PagedAttention aprovechan la memoria al máximo.
- **Modelos grandes en múltiples GPUs**: tensor parallelism maduro.
- **Migración desde OpenAI u otras APIs**: servidor OpenAI-compatible de la caja.
- **Ecosistema estándar de serving**: gran comunidad, amplio soporte de modelos y kernels.

## Evitarlo cuando:

- Solo se necesita un modelo en local para desarrollo → usa Ollama.
- El workload es muy específico de prefijos compartidos/structured output → valora SGLang.

---

# SGLang: cuándo usarlo

## Mejor opción cuando:

- **Prefijos compartidos intensivos**: muchos prompts con el mismo system prompt, contexto RAG o few-shot → RadixAttention reutiliza la KV cache de forma agresiva.
- **Salidas estructuradas**: JSON, extracción, tool calling de forma frecuente (FSM comprimido).
- **Agentes y flujos multi-turno**: el frontend DSL (`gen`, `fork`, `select`) facilita programas de LLM complejos.
- **Alta concurrencia con baja latencia**, igual que vLLM.

## Evitarlo cuando:

- Se busca la opción más simple o con mayor ecosistema consolidado → vLLM o Ollama.
- Solo es uso local de desarrollo → Ollama.

---

# Flujo de decisión

```
¿Se necesita servir a varios usuarios / alta carga en producción?
├── NO → ¿Uso local, desarrollo o prototipado?
│         ├── Sí → Ollama
│         └── No → valorar vLLM incluso para carga baja (preparación)
└── SÍ → ¿El workload tiene muchos prefijos compartidos,
          structured output intensivo o agentes complejos?
          ├── Sí → SGLang
          └── No → vLLM
```

## Reglas prácticas

1. **Desarrollo local / learning** → Ollama.
2. **Producción estándar (API multiusuario)** → vLLM.
3. **Producción con agentes, RAG con prefijos comunes o JSON intensivo** → SGLang.
4. **Del prototipo al prod**: se puede empezar con Ollama en desarrollo y pasar a vLLM/SGLang en producción; ambos exponen API compatible con OpenAI, así que el cliente solo cambia la URL base.

---

# Ejemplo de migración conceptual

**Desarrollo (Ollama):**

```bash
ollama run llama3.1
# http://localhost:11434
```

**Producción (vLLM):**

```bash
vllm serve meta-llama/Llama-3.1-8B-Instruct --port 8000
# http://localhost:8000/v1  (compatible OpenAI)
```

**Producción (SGLang):**

```bash
python -m sglang.launch_server --model meta-llama/Llama-3.1-8B-Instruct --port 8000
# http://localhost:8000/v1  (compatible OpenAI)
```

---

# Referencias

- Ollama: https://github.com/ollama/ollama
- vLLM: https://github.com/vllm-project/vllm
- SGLang: https://github.com/sgl-project/sglang
- vLLM vs otros motores (contexto general): https://docs.vllm.ai/
