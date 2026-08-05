# Gemma

Gemma es una familia de modelos de inteligencia artificial con pesos abiertos desarrollada por Google DeepMind. Está basada en investigaciones y tecnologías utilizadas en Gemini, pero está diseñada para que los modelos puedan descargarse, ajustarse y ejecutarse en infraestructura propia, dispositivos personales o servicios en la nube.

## Características principales

- Pesos abiertos disponibles para investigación y uso comercial responsable bajo los términos de Gemma.
- Modelos de distintos tamaños para dispositivos móviles, computadoras personales y servidores.
- Variantes preentrenadas y ajustadas para seguir instrucciones.
- Soporte multilingüe en más de 140 idiomas en las generaciones recientes.
- Capacidades multimodales para procesar texto, imágenes y, en determinados modelos, audio.
- Ventanas de contexto extensas para analizar documentos y conversaciones largas.
- Razonamiento configurable, generación y corrección de código, y llamadas a funciones en Gemma 4.
- Posibilidad de ajuste fino para tareas o dominios específicos.
- Compatibilidad con herramientas como Hugging Face Transformers, JAX, Keras, PyTorch, LiteRT y motores de inferencia locales.

## Versiones principales

| Generación | Tamaños principales | Modalidades | Características destacadas |
| --- | --- | --- | --- |
| Gemma 1 | 2B y 7B | Texto → texto | Primera generación, orientada a generación de texto y ejecución local. |
| Gemma 2 | 2B, 9B y 27B | Texto → texto | Mejor calidad y eficiencia; base de variantes como ShieldGemma. |
| Gemma 3 | 270M, 1B, 4B, 12B y 27B | Texto; texto e imágenes desde 4B | Hasta 128K tokens de contexto, más de 140 idiomas y tamaños adecuados para dispositivos con diferentes capacidades. |
| Gemma 3n | E2B y E4B | Texto, imágenes y audio → texto | Optimizada para teléfonos, tabletas y portátiles mediante parámetros efectivos, caché PLE y arquitectura MatFormer. Contexto de 32K tokens. |
| Gemma 4 | E2B, E4B, 12B, 26B A4B y 31B | Texto e imágenes; audio en E2B, E4B y 12B | Generación vigente. Incorpora razonamiento, llamadas a funciones, mejor capacidad de programación, rol de sistema y predicción de múltiples tokens. |

`B` significa miles de millones de parámetros. En `E2B` y `E4B`, la letra `E` indica parámetros efectivos: el modelo utiliza técnicas que reducen los parámetros activos y la memoria necesaria durante la ejecución. `A4B` indica que el modelo de mezcla de expertos activa aproximadamente 4 mil millones de parámetros por inferencia, aunque contiene más parámetros totales.

## Gemma 4

Gemma 4 es la generación recomendada para nuevos proyectos de propósito general. Sus capacidades incluyen:

- Entrada intercalada de texto e imágenes.
- Comprensión de documentos, interfaces, gráficos, escritura manual y OCR multilingüe.
- Procesamiento de video como secuencias de imágenes.
- Reconocimiento y traducción de voz en E2B, E4B y 12B.
- Razonamiento configurable antes de producir la respuesta.
- Generación, finalización y corrección de código.
- Llamadas estructuradas a funciones para agentes y automatizaciones.
- Contexto de 128K tokens en E2B y E4B, y hasta 256K en 12B, 26B A4B y 31B.

### Selección del tamaño

| Modelo | Plataforma sugerida | Uso recomendado |
| --- | --- | --- |
| E2B | Móviles y dispositivos de borde | Clasificación, extracción, asistentes compactos y tareas de baja latencia. |
| E4B | Móviles potentes y portátiles | Asistentes locales y aplicaciones multimodales con recursos limitados. |
| 12B | Portátiles potentes, equipos de escritorio y servidores pequeños | RAG, análisis documental, programación y razonamiento de complejidad media. |
| 26B A4B | Equipos de escritorio potentes y servidores pequeños | Alto rendimiento, razonamiento avanzado y solicitudes concurrentes. |
| 31B | Servidores grandes o clústeres | Máxima calidad de la familia para tareas complejas y despliegues centralizados. |

## Requisitos para inferencia

Los requisitos cambian según el modelo, la cuantización, el tamaño del contexto, el motor de inferencia y el uso de CPU, GPU o TPU. La siguiente tabla muestra la memoria aproximada indicada por Google para cargar Gemma 4, incluyendo un 20 % de sobrecarga.

| Modelo | BF16 | 8 bits | Q4 de 4 bits |
| --- | ---: | ---: | ---: |
| Gemma 4 E2B | 11,4 GB | 5,7 GB | 2,9 GB |
| Gemma 4 E4B | 17,9 GB | 8,9 GB | 4,5 GB |
| Gemma 4 12B | 26,7 GB | 13,4 GB | 6,7 GB |
| Gemma 4 26B A4B | 57,7 GB | 28,8 GB | 14,4 GB |
| Gemma 4 31B | 69,9 GB | 34,9 GB | 17,5 GB |

Estas cifras son orientativas. La caché de contexto, las imágenes, el audio, las solicitudes simultáneas y el propio motor requieren memoria adicional.

### Hardware

- **CPU:** permite ejecutar modelos cuantizados, aunque generalmente con menor velocidad que una GPU. La memoria RAM disponible debe superar el tamaño cargado del modelo y dejar margen para el sistema.
- **GPU o TPU:** recomendable para baja latencia, contextos grandes, multimodalidad, ajuste fino o varias solicitudes simultáneas.
- **Almacenamiento:** debe ser superior al tamaño del archivo del modelo. Conviene reservar espacio adicional para varias cuantizaciones, cachés y modelos ajustados.
- **Dispositivos móviles:** las variantes E2B y E4B ofrecen versiones optimizadas mediante LiteRT-LM; sus requisitos no equivalen directamente a los archivos BF16 de escritorio.

### Software

Según el entorno elegido, se puede utilizar:

- Hugging Face Transformers con PyTorch.
- La biblioteca oficial de Gemma basada en JAX.
- Keras y KerasNLP.
- LiteRT-LM para ejecución en dispositivos.
- Kaggle Models o Hugging Face para obtener los pesos oficiales.
- Vertex AI o la API de Gemini para ejecución administrada, cuando la variante esté disponible.

El acceso a los pesos puede requerir aceptar los términos de uso de Gemma en la plataforma de descarga.

## Casos de uso

### Asistentes y generación de texto

- Chatbots y asistentes internos.
- Respuestas a preguntas.
- Redacción, reescritura y traducción.
- Resumen de documentos y conversaciones.

### RAG y análisis documental

- Consulta de bases de conocimiento privadas.
- Extracción de información estructurada.
- Clasificación de documentos.
- Análisis de contratos, informes, tablas y formularios con supervisión adecuada.

### Visión y contenido multimodal

- Descripción y clasificación de imágenes.
- OCR y lectura de escritura manual.
- Interpretación de gráficos, capturas de pantalla e interfaces.
- Análisis de documentos PDF convertidos en imágenes.
- Comprensión de videos mediante fotogramas.

### Audio

En Gemma 4 E2B, E4B y 12B, y en Gemma 3n:

- Transcripción de voz.
- Traducción de voz a texto.
- Análisis de grabaciones cortas.

### Desarrollo de software y agentes

- Generación, explicación y corrección de código.
- Asistentes de programación locales.
- Llamadas a herramientas y funciones.
- Automatización de flujos mediante agentes.

### Ejecución local y en dispositivos

- Aplicaciones que requieren privacidad o procesamiento sin conexión.
- Soluciones en teléfonos, tabletas, portátiles y dispositivos de borde.
- Sistemas con restricciones de latencia, conectividad o costo por solicitud.

## Variantes especializadas

| Variante | Propósito |
| --- | --- |
| CodeGemma | Generación y finalización de código. |
| PaliGemma 2 | Tareas de visión y lenguaje. |
| ShieldGemma 2 | Moderación y evaluación de seguridad de contenido. |
| MedGemma | Aplicaciones e investigación en texto e imágenes médicas; no reemplaza la validación clínica. |
| EmbeddingGemma | Embeddings para búsqueda semántica, clasificación, agrupamiento y RAG. |
| FunctionGemma | Llamadas a funciones en dispositivos y flujos basados en herramientas. |
| TranslateGemma | Traducción especializada. |

## Limitaciones y consideraciones

- Los pesos abiertos no eliminan la obligación de cumplir los términos de uso de Gemma.
- Las respuestas pueden contener errores, sesgos o información inventada.
- Los modelos no deben utilizarse sin controles adicionales para decisiones médicas, legales, financieras o de seguridad.
- La cuantización reduce memoria y almacenamiento, pero puede disminuir la calidad.
- La variante más grande no siempre es la mejor: conviene evaluar calidad, latencia, memoria y costo con datos representativos del caso de uso.

## Fuentes oficiales

- [Descripción general de Gemma](https://ai.google.dev/gemma/docs)
- [Primeros pasos y variantes](https://ai.google.dev/gemma/docs/get_started)
- [Descripción de Gemma 4](https://ai.google.dev/gemma/docs/core)
- [Tarjeta del modelo Gemma 4](https://ai.google.dev/gemma/docs/core/model_card_4)
- [Tarjeta del modelo Gemma 3](https://ai.google.dev/gemma/docs/core/model_card_3)
- [Descripción de Gemma 3n](https://ai.google.dev/gemma/docs/gemma-3n)
- [Historial de versiones](https://ai.google.dev/gemma/docs/releases)
