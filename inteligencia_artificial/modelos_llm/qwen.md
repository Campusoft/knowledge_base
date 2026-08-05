# Qwen

Qwen es una familia de modelos de inteligencia artificial desarrollada por el equipo Qwen de Alibaba Cloud. Incluye modelos de lenguaje generales, modelos multimodales y variantes especializadas para programación, agentes, visión, audio, embeddings y generación de imágenes.

La familia ofrece modelos con pesos abiertos que pueden ejecutarse localmente, ajustarse para tareas específicas o desplegarse mediante servicios administrados.

## Características principales

- Modelos densos y modelos de mezcla de expertos o MoE.
- Variantes ajustadas para instrucciones y uso conversacional.
- Modos de razonamiento y respuesta directa en generaciones recientes.
- Soporte multilingüe; Qwen3 admite 119 idiomas y dialectos.
- Llamadas a funciones, uso de herramientas y compatibilidad con agentes.
- Capacidades de programación, matemáticas y razonamiento lógico.
- Procesamiento de texto, imágenes, audio y video según la variante.
- Ventanas de contexto extensas para documentos, repositorios y conversaciones largas.
- Cuantizaciones para reducir memoria y almacenamiento.
- Compatibilidad con Hugging Face Transformers, vLLM, SGLang, KTransformers, llama.cpp, Ollama y LM Studio, según el formato del modelo.

## Versiones principales

| Generación | Tamaños o arquitecturas destacadas | Características principales |
| --- | --- | --- |
| Qwen 1 y 1.5 | Desde 0,5B hasta 110B | Primeras generaciones de modelos base y de chat, con capacidades multilingües y contexto ampliado. |
| Qwen 2 | 0,5B, 1,5B, 7B, 57B-A14B y 72B | Modelos densos y MoE, soporte de herramientas y hasta 128K tokens en determinadas variantes. |
| Qwen 2.5 | Desde 0,5B hasta 72B; variantes de 1M tokens | Mejoras en instrucciones, conocimiento, programación, matemáticas y manejo de contexto largo. |
| Qwen 3 | Densos de 0,6B a 32B; MoE 30B-A3B y 235B-A22B | Razonamiento híbrido, 119 idiomas y dialectos, programación, agentes, herramientas y MCP. |
| Qwen 3.5 | Densos y MoE, incluidos 2B, 9B, 27B, 35B-A3B, 122B-A10B y 397B-A17B | Modelos multimodales con mejoras en razonamiento, agentes y comprensión visual. |
| Qwen 3.6 | 27B denso y 35B-A3B MoE | Generación vigente de pesos abiertos, orientada a programación con agentes, razonamiento estable, visión y contexto muy largo. |

`B` significa miles de millones de parámetros. En un nombre como `35B-A3B`, el modelo contiene aproximadamente 35 mil millones de parámetros, pero activa cerca de 3 mil millones en cada paso de inferencia. Esto reduce el cálculo, aunque normalmente todos los pesos deben almacenarse o cargarse entre memoria y dispositivos.

## Qwen 2.5

Qwen 2.5 es una generación de modelos densos de tipo Transformer, publicada en versiones base y ajustadas para instrucciones. Aunque existen generaciones posteriores, continúa siendo útil por su amplia variedad de tamaños, disponibilidad de cuantizaciones y compatibilidad con herramientas de ejecución local.

### Características

- Mayor capacidad de conocimiento, programación y matemáticas que Qwen 2.
- Mejor seguimiento de instrucciones y mayor resistencia a diferentes mensajes de sistema.
- Generación de textos de más de 8.000 tokens.
- Comprensión de datos estructurados como tablas.
- Generación de salidas estructuradas, especialmente JSON.
- Soporte para más de 29 idiomas, incluido español.
- Modelos base para ajuste fino y modelos `Instruct` para conversación y tareas directas.
- Arquitectura con RoPE, SwiGLU, RMSNorm y Grouped Query Attention.

### Tamaños y contexto

| Modelo | Parámetros aproximados | Contexto | Salida máxima indicada | Uso orientativo |
| --- | ---: | ---: | ---: | --- |
| Qwen2.5-0.5B | 0,5B | 32.768 tokens | 8.192 tokens | Clasificación, extracción y dispositivos con recursos muy limitados. |
| Qwen2.5-1.5B | 1,5B | 32.768 tokens | 8.192 tokens | Asistentes compactos y automatizaciones sencillas. |
| Qwen2.5-3B | 3B | 32.768 tokens | 8.192 tokens | Chat local, resumen y tareas generales ligeras. |
| Qwen2.5-7B | 7B | Hasta 131.072 tokens | 8.192 tokens | Asistente local general, RAG y programación básica. |
| Qwen2.5-14B | 14B | Hasta 131.072 tokens | 8.192 tokens | RAG, análisis documental y tareas de mayor complejidad. |
| Qwen2.5-32B | 32B | Hasta 131.072 tokens | 8.192 tokens | Programación, razonamiento y servicios internos de mayor calidad. |
| Qwen2.5-72B | 72B | Hasta 131.072 tokens | 8.192 tokens | Servidores, alta calidad y tareas complejas con varias GPU. |

En los modelos compatibles con 128K, la configuración distribuida puede venir preparada para 32.768 tokens. Para superar esa longitud se utiliza escalado YaRN. Activarlo permanentemente puede perjudicar el rendimiento en textos cortos, por lo que se recomienda solamente para cargas que realmente necesiten contexto largo.

También existen `Qwen2.5-7B-Instruct-1M` y `Qwen2.5-14B-Instruct-1M`, diseñados para contextos de hasta un millón de tokens. Procesar esa longitud requiere mucha más memoria para la caché KV y normalmente hardware de servidor.

### Requisitos de memoria para los pesos

La siguiente tabla es una estimación teórica basada en el número de parámetros. No incluye caché KV, contexto, lotes, metadatos ni memoria del motor.

| Modelo | BF16/FP16 | 8 bits | 4 bits |
| --- | ---: | ---: | ---: |
| Qwen2.5-0.5B | 1 GB | 0,5 GB | 0,25 GB |
| Qwen2.5-1.5B | 3 GB | 1,5 GB | 0,75 GB |
| Qwen2.5-3B | 6 GB | 3 GB | 1,5 GB |
| Qwen2.5-7B | 14 GB | 7 GB | 3,5 GB |
| Qwen2.5-14B | 28 GB | 14 GB | 7 GB |
| Qwen2.5-32B | 64 GB | 32 GB | 16 GB |
| Qwen2.5-72B | 144 GB | 72 GB | 36 GB |

Debe reservarse al menos un 20 % a 30 % adicional sobre el peso del modelo. Un contexto grande puede incrementar notablemente el consumo, incluso cuando el modelo está cuantizado.

### Recomendaciones prácticas de hardware

Estas configuraciones son orientativas para inferencia con cuantización de 4 bits y un contexto moderado:

| Tamaño | Memoria práctica recomendada | Equipo sugerido |
| --- | ---: | --- |
| 0,5B y 1,5B | 4 GB de RAM o VRAM | CPU, GPU integrada, móvil o equipo básico. |
| 3B | 6 a 8 GB de RAM o VRAM | Portátil o escritorio básico. |
| 7B | 8 a 12 GB de RAM o VRAM | GPU de 8 GB o CPU con suficiente RAM. |
| 14B | 12 a 16 GB de RAM o VRAM | GPU de 12/16 GB o Apple Silicon con memoria unificada suficiente. |
| 32B | 24 GB o más de RAM/VRAM | GPU de 24 GB, Apple Silicon de alta memoria o CPU con descarga parcial. |
| 72B | 48 GB o más de RAM/VRAM | Varias GPU, estación de trabajo de alta memoria o servidor. |

Para BF16/FP16 se necesita aproximadamente la memoria indicada en la tabla de pesos, más el margen operativo. Para contextos de 128K o 1M tokens se requiere memoria adicional considerable; es preferible comenzar con 8K o 32K y aumentar únicamente después de medir el consumo.

### Selección según el equipo

- **Sin GPU dedicada:** 0,5B, 1,5B o 3B en GGUF de 4 bits. Un 7B también puede funcionar si existe suficiente RAM, pero será más lento.
- **GPU de 8 GB:** 3B o 7B cuantizado, utilizando un contexto moderado.
- **GPU de 12 a 16 GB:** 7B con mayor precisión o 14B cuantizado.
- **GPU de 24 GB:** 14B con comodidad o 32B cuantizado con límites de contexto.
- **Varias GPU o servidor:** 32B con mayor precisión, 72B y contextos extensos.
- **Apple Silicon:** modelos GGUF o MLX cuantizados; la memoria unificada debe cubrir el modelo, el sistema y la caché de contexto.

### Casos de uso

#### Modelos generales Qwen2.5

- Chatbots y asistentes internos.
- Resumen, clasificación, extracción y traducción.
- Generación de JSON y procesamiento de tablas.
- RAG sobre documentos privados.
- Creación de datos sintéticos y apoyo para ajuste fino.
- Automatización mediante llamadas a herramientas.

#### Qwen2.5-Coder

Disponible en 0,5B, 1,5B, 3B, 7B, 14B y 32B. Está orientado a:

- Generación y finalización de código.
- Explicación, transformación y reparación de programas.
- Asistentes de programación locales.
- Generación de interfaces y artefactos de software.

#### Qwen2.5-Math

Disponible principalmente en 1,5B, 7B y 72B. Está especializado en problemas matemáticos en inglés y chino mediante razonamiento paso a paso y razonamiento integrado con herramientas. No se recomienda como modelo general de conversación.

#### Qwen2.5-VL

Disponible en 3B, 7B y 72B. Sus casos de uso incluyen:

- OCR, formularios, facturas, tablas y documentos escaneados.
- Comprensión de imágenes, gráficos y diseños.
- Localización de objetos mediante puntos o cajas delimitadoras.
- Análisis de videos largos y localización temporal de eventos.
- Agentes visuales para utilizar computadoras o teléfonos con controles de seguridad.

#### Qwen2.5-Omni

Procesa texto, imágenes, audio y video, y puede producir texto y voz. Es apropiado para asistentes de voz, transcripción, traducción, análisis audiovisual e interacción multimodal en tiempo real.

### Ejecución local con Ollama

```bash
ollama run qwen2.5:0.5b
ollama run qwen2.5:3b
ollama run qwen2.5:7b
ollama run qwen2.5:14b
ollama run qwen2.5:32b
ollama run qwen2.5:72b
```

El tamaño elegido debe ajustarse a la memoria disponible. La descarga publicada por Ollama puede usar una cuantización específica, por lo que el tamaño real debe comprobarse antes de desplegarla.

### Licencias de Qwen 2.5

- La mayoría de los modelos generales Qwen 2.5 utiliza Apache License 2.0.
- Qwen2.5-3B utiliza Qwen Research License.
- Qwen2.5-72B utiliza la licencia Qwen.
- Las variantes Coder, Math, VL y Omni pueden tener condiciones diferentes. Se debe revisar siempre la licencia de la tarjeta concreta del modelo antes de usarlo comercialmente o redistribuirlo.

## Qwen3.6

Qwen3.6 es la generación recomendada para nuevos proyectos que necesiten alta capacidad de razonamiento, programación o agentes. Sus dos modelos abiertos principales son:

| Modelo | Arquitectura | Parámetros | Contexto nativo | Uso sugerido |
| --- | --- | ---: | ---: | --- |
| Qwen3.6-27B | Densa | 27B activos | 262.144 tokens | Razonamiento, programación, visión y ejecución en un servidor o equipo potente. |
| Qwen3.6-35B-A3B | MoE | 35B totales, 3B activos | 262.144 tokens | Agentes, programación y alto rendimiento con menor cálculo activo por token. |

Ambos modelos pueden extenderse hasta aproximadamente 1.010.000 tokens mediante configuración de escalado de contexto. Esta extensión aumenta considerablemente el consumo de memoria y puede afectar la calidad en textos cortos; debe habilitarse solamente cuando sea necesaria.

### Capacidades destacadas

- Modelo causal con codificador visual.
- Entrada de texto e imágenes y salida de texto.
- Razonamiento antes de generar la respuesta final.
- Conservación opcional del contexto de razonamiento entre mensajes.
- Generación de código y razonamiento sobre repositorios completos.
- Uso de herramientas y llamadas a funciones.
- Flujos de agentes con Qwen-Agent, Qwen Code o servidores compatibles con la API de OpenAI.
- Predicción de múltiples tokens para acelerar la inferencia en motores compatibles.
- Comprensión de documentos, gráficos, interfaces y contenido visual.

## Selección del modelo

| Necesidad | Modelo o familia sugerida |
| --- | --- |
| Equipo con recursos limitados | Qwen3 de 0,6B, 1,7B o 4B cuantizado. |
| Asistente local general | Qwen3 de 4B u 8B, o una variante Qwen3.5 pequeña. |
| Programación local | Qwen3-Coder en un tamaño compatible con la memoria disponible. |
| Razonamiento y agentes avanzados | Qwen3.6-27B o Qwen3.6-35B-A3B. |
| Análisis de imágenes y documentos | Qwen3-VL o Qwen3.6. |
| Texto, audio, imágenes y video | Qwen3-Omni. |
| Búsqueda semántica y RAG | Qwen3-Embedding y Qwen3-Reranker. |
| Reconocimiento de voz | Qwen3-ASR. |

## Requisitos para inferencia

Los requisitos reales dependen del formato, cuantización, tamaño de contexto, lote, caché KV, motor de inferencia y distribución entre CPU y GPU. No existe un único requisito mínimo para cada modelo.

Como estimación inicial, el almacenamiento de los pesos requiere aproximadamente:

- BF16 o FP16: 2 bytes por parámetro.
- 8 bits: 1 byte por parámetro.
- 4 bits: 0,5 bytes por parámetro.

### Estimación para Qwen3.6

| Modelo | BF16/FP16 | 8 bits | 4 bits |
| --- | ---: | ---: | ---: |
| Qwen3.6-27B | 54 GB | 27 GB | 13,5 GB |
| Qwen3.6-35B-A3B | 70 GB | 35 GB | 17,5 GB |

Estas cifras representan solamente una aproximación de los pesos. Conviene disponer de al menos un 20 % a 30 % adicional para metadatos y funcionamiento básico, además de la memoria necesaria para el contexto, imágenes, caché KV y solicitudes simultáneas.

### Orientación de hardware

- **CPU:** adecuada para modelos pequeños o cuantizados, con menor velocidad que una GPU. Puede combinarse con una GPU mediante descarga parcial de capas.
- **GPU:** recomendada para baja latencia, contexto largo, visión, agentes o varias solicitudes simultáneas.
- **Varias GPU:** normalmente necesarias para modelos grandes en BF16/FP16 y despliegues con el contexto completo.
- **Apple Silicon:** puede ejecutar modelos compatibles y cuantizados mediante MLX-LM o llama.cpp, usando memoria unificada.
- **Almacenamiento:** debe superar el tamaño del modelo y dejar espacio para varias cuantizaciones, cachés y adaptadores.

Usar el contexto máximo de Qwen3.6 puede requerir mucha más memoria que cargar únicamente sus pesos. Si aparece un error de memoria, se recomienda reducir primero la longitud de contexto.

## Software y despliegue

Las opciones más comunes son:

- **Hugging Face Transformers:** pruebas, desarrollo y ejecución directa con PyTorch.
- **vLLM:** servidor de inferencia de alto rendimiento y API compatible con OpenAI.
- **SGLang:** inferencia y agentes con optimizaciones para modelos grandes y MoE.
- **KTransformers:** ejecución heterogénea combinando CPU y GPU.
- **llama.cpp:** ejecución local de modelos convertidos o publicados en formato GGUF.
- **Ollama y LM Studio:** administración y uso local de cuantizaciones compatibles.
- **MLX-LM:** ejecución optimizada en equipos Apple Silicon.
- **DashScope:** acceso administrado a modelos Qwen mediante la nube de Alibaba.

Para producción, conviene usar versiones recientes de los motores de inferencia porque la compatibilidad con arquitecturas nuevas puede cambiar rápidamente.

## Casos de uso

### Asistentes y generación de texto

- Chatbots y asistentes internos.
- Respuestas a preguntas.
- Redacción, resumen, traducción y clasificación.
- Procesamiento multilingüe.

### RAG y documentos

- Consulta de bases de conocimiento privadas.
- Búsqueda semántica con Qwen3-Embedding.
- Reordenamiento de resultados con Qwen3-Reranker.
- Análisis de documentos, tablas, capturas de pantalla y formularios.
- Procesamiento de repositorios y documentación extensa.

### Programación

- Generación, explicación y corrección de código.
- Revisión de cambios y navegación de repositorios.
- Automatización desde terminal con Qwen Code.
- Desarrollo asistido mediante Qwen3-Coder.

### Agentes y herramientas

- Llamadas a funciones y APIs.
- Integración con servidores MCP.
- Uso de intérprete de código y herramientas externas.
- Planificación y ejecución de tareas de varios pasos.
- Automatización de navegadores y flujos empresariales con controles adecuados.

### Visión, audio y contenido multimodal

- OCR y análisis de documentos visuales.
- Comprensión de imágenes, gráficos, interfaces y video.
- Reconocimiento y traducción de voz.
- Interacción multimodal en tiempo real con variantes Omni.

### Matemáticas e investigación

- Resolución y explicación de problemas matemáticos.
- Generación de hipótesis y apoyo en análisis técnico.
- Procesamiento de texto científico, siempre con verificación humana.

## Variantes especializadas

| Variante | Propósito |
| --- | --- |
| Qwen3-Coder | Programación, uso de terminal, repositorios y agentes de desarrollo. |
| Qwen3-VL | Comprensión conjunta de texto, imágenes, documentos y video. |
| Qwen3-Omni | Entrada de texto, imágenes, audio y video, con generación de voz en tiempo real. |
| Qwen3-ASR | Reconocimiento de voz, música y canciones, detección de idioma y marcas de tiempo. |
| Qwen3-Embedding | Representaciones vectoriales para búsqueda, clasificación, agrupamiento y RAG. |
| Qwen3-Reranker | Reordenamiento de resultados recuperados según su relevancia. |
| Qwen-Image | Generación y edición de imágenes, incluido texto complejo dentro de imágenes. |
| Qwen2.5-Math | Resolución de problemas matemáticos; pertenece a una generación anterior especializada. |

## Licencia y consideraciones

- Qwen3.6-27B y Qwen3.6-35B-A3B se publican bajo Apache License 2.0.
- La licencia puede cambiar entre generaciones, tamaños o variantes; siempre debe revisarse la tarjeta específica del modelo antes de distribuirlo o usarlo comercialmente.
- Un modelo con pesos abiertos puede generar información incorrecta, sesgada o insegura.
- La cuantización reduce memoria y almacenamiento, pero puede afectar la calidad.
- Las tareas médicas, legales, financieras o de seguridad requieren validación especializada y controles adicionales.
- El modelo más grande no siempre es el más conveniente: deben evaluarse calidad, latencia, memoria, costo y privacidad con datos representativos.

## Fuentes oficiales

- [Organización Qwen en GitHub](https://github.com/QwenLM)
- [Documentación de Qwen](https://qwen.readthedocs.io/en/latest/)
- [Presentación oficial de Qwen 2.5](https://qwenlm.github.io/blog/qwen2.5/)
- [Qwen2.5-7B-Instruct](https://huggingface.co/Qwen/Qwen2.5-7B-Instruct)
- [Qwen2.5-Coder](https://qwenlm.github.io/blog/qwen2.5-coder-family/)
- [Qwen2.5-Math](https://qwenlm.github.io/blog/qwen2.5-math/)
- [Qwen2.5-VL](https://qwenlm.github.io/blog/qwen2.5-vl/)
- [Qwen2.5-Omni](https://qwenlm.github.io/blog/qwen2.5-omni/)
- [Qwen3.6-27B](https://huggingface.co/Qwen/Qwen3.6-27B)
- [Qwen3.6-35B-A3B](https://huggingface.co/Qwen/Qwen3.6-35B-A3B)
- [Modelos oficiales en Hugging Face](https://huggingface.co/Qwen/models)
- [Qwen3-Coder](https://github.com/QwenLM/Qwen3-Coder)
- [Qwen3-VL](https://github.com/QwenLM/Qwen3-VL)
- [Qwen3-Omni](https://github.com/QwenLM/Qwen3-Omni)
- [Qwen-Agent](https://github.com/QwenLM/Qwen-Agent)
