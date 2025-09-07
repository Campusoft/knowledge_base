# Inteligencia Artificial Conceptos


# Clustering / Agrupamiento


K-Means es un algoritmo de aprendizaje no supervisado utilizado para resolver problemas de clustering o agrupamiento en ciencia de datos y aprendizaje automático. Su objetivo principal es dividir un conjunto de datos en grupos (clusters) con elementos similares dentro de cada grupo y diferentes entre los grupos.




# Incrustaciones "embeddings"

En términos sencillos, los embeddings son representaciones numéricas de datos como palabras, frases o incluso imágenes, diseñadas para su procesamiento eficiente en modelos de aprendizaje automático.


Los embeddings son representaciones numéricas de datos, generalmente en forma de vectores de alta dimensionalidad, que capturan las características esenciales de los datos en un espacio matemático. En el contexto de IA y aprendizaje automático, se utilizan para representar de manera eficiente datos como texto, imágenes o sonidos, facilitando cálculos y comparaciones.



Un modelo de embeddings es un modelo matemático que convierte datos (como texto, imágenes, o cualquier otro tipo de información) en vectores numéricos en un espacio multidimensional. Estos vectores, conocidos como embeddings, capturan las características y relaciones semánticas de los datos de manera que elementos similares estén más cerca entre sí en este espacio.


Texto: Palabras, frases o documentos se convierten en vectores numéricos que representan su significado.

- Por ejemplo: Las palabras "gato" y "felino" tendrán vectores similares porque tienen significados relacionados.


Imágenes: Una imagen puede convertirse en un vector que representa su contenido visual.

- Por ejemplo: Las imágenes de perros estarán más cerca entre sí que de una imagen de un automóvil.
Audio o datos estructurados: Pueden representarse en un espacio vectorial similar, dependiendo del modelo y el propósito.



**Propósito de los embeddings**

El objetivo principal de los embeddings es hacer que los datos sean comprensibles y manipulables para los algoritmos de machine learning. Son fundamentales en tareas como:

- Búsquedas semánticas:

Dado un texto o consulta, encontrar los elementos más relevantes.
Ejemplo: Buscar preguntas similares en una base de preguntas y respuestas.

- Análisis de similitud:

Comparar la similitud entre dos datos.
Ejemplo: Determinar si dos frases tienen el mismo significado.

- Clustering:

Agrupar datos similares en categorías.
Ejemplo: Agrupar documentos sobre temas similares.

- Clasificación:

Usar embeddings como características para entrenar modelos de predicción.
Ejemplo: Detectar spam en correos electrónicos.

- Recomendaciones:

Sugerir elementos similares basándose en sus embeddings.
Ejemplo: Recomendaciones de productos en tiendas en línea.


**Modelos de embeddings comunes**


Para texto:

- Word2Vec (Google): Aprende relaciones basadas en contexto.
- GloVe (Stanford): Modela co-ocurrencias de palabras.
- BERT (Google): Entiende el contexto completo de una palabra en una frase.
- Sentence-Transformers: Diseñado para crear embeddings de oraciones o textos largos.

Para imágenes:

- ResNet, EfficientNet: Extraen características visuales.

Para datos generales:

- OpenAI Embeddings: Usados para múltiples dominios.


# Soporte Multimodal

En IA, cuando se habla de soporte multimodal significa que el sistema no se limita a un único tipo de entrada o salida (por ejemplo, solo texto), sino que puede procesar y generar información en múltiples modalidades de datos, como:

Texto: conversación escrita tradicional.

Voz/audio: reconocimiento de voz (STT – speech-to-text), síntesis de voz (TTS – text-to-speech), interacción hablada en tiempo real.

Imágenes: interpretar, describir o generar imágenes a partir de texto (ejemplo: "dibuja un diagrama" o "analiza esta foto").

Video: análisis de escenas, generación de subtítulos o incluso generación de video.

Datos estructurados: tablas, gráficos, documentos.


# RAG  (Retrieval-Augmented Generation)

RAG, que significa Retrieval-Augmented Generation (Generación Aumentada con Recuperación), es una técnica en inteligencia artificial que combina modelos de generación de texto (como los modelos de lenguaje) con sistemas de recuperación de información. Este enfoque permite que un modelo de lenguaje genere respuestas informadas y precisas utilizando fuentes de información externas.


[Detalles](RAG.md)



# Lenguaje natural (NLP)


Procesamiento del Lenguaje Natural (NLP):
- Traducción automática.
- Análisis de sentimientos.
- Generación de texto.
- Resumen de textos.

Es el campo amplio que engloba todos los aspectos del trabajo con lenguaje natural.

# NLU (Natural Language Understanding)

Comprensión de Lenguaje Natural

Es una subárea dentro de NLP, enfocada en que la máquina no solo procese el texto, sino que entienda su significado e intención.

Incluye tareas como:

- Detección de intención (intent recognition).
- Extracción de entidades (NER: named entity recognition).
- Análisis de sentimiento.
- Resolución de ambigüedades (“banco” → ¿institución financiera o asiento?).
- Comprensión semántica para responder preguntas o ejecutar acciones.

Es el nivel semántico del NLP.