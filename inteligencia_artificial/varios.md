# Varios



# Rows


- Import data from social media, data warehouses, advertising platforms, your back-office platform, and other tools you use every day, all without writing code.

- Your AI co-pilot. Access the power of GPT-3 inside a spreadsheet. Use AI to create lists of data, answer questions, classify customer feedback, translate text and pretty much anything else you can imagine.

- Don’t take another bad screenshot of a chart, or cram a picture of a table in a slide. Add beautiful embeds anywhere on the web, and keep your data in sync and shared across the org.
	
https://rows.com/product




# Prompts

Un prompt es la instrucción en formato texto que le damos a un LLM para comenzar a generar (predecir) un resultado. Si el output es el resultado de la probabilidad a partir del input, podemos ver la importancia de aportar un input lo más adecuado posible.


# Datos vectorial


Las bases de datos vectoriales son sistemas diseñados para almacenar y gestionar vectores de alta dimensión, los cuales son representaciones numéricas de objetos en un espacio multidimensional. Estos vectores suelen ser el resultado de transformar datos no estructurados, como texto, imágenes o sonidos, en una forma que una máquina pueda entender y procesar. Las operaciones más comunes que se realizan sobre estos vectores son las búsquedas de los vecinos más cercanos (Nearest Neighbor Search, NNS), fundamentales en aplicaciones de IA como la búsqueda semántica, el reconocimiento de imágenes y los sistemas de recomendación.



Las 5 mejores bases de datos vectoriales
- Chroma
- Pinecone
- Weaviate
- Faiss
- Qdrant
https://www.datacamp.com/es/blog/the-top-5-vector-databases

# Qdrant


Qdrant es una base de datos vectorial de alto rendimiento diseñada para el almacenamiento, búsqueda y gestión de datos representados como vectores. Estos vectores suelen ser generados por modelos de aprendizaje automático (como embeddings) y se utilizan para resolver problemas como búsqueda semántica, recomendaciones personalizadas, y recuperación de información basada en similitud.



Utiliza una modificación personalizada del algoritmo HNSW (Hierarchical Navigable Small World) para realizar búsquedas aproximadas del vecino más cercano (Approximate Nearest Neighbor Search, ANNS), ofreciendo velocidad y precisión en la recuperación de los vectores más relevantes.



** Características principales de Qdrant**

- Búsqueda de similitud basada en vectores:

Qdrant utiliza métricas como distancia euclidiana, Manhattan, o coseno para encontrar vectores similares en grandes conjuntos de datos.

- Optimización para datos no estructurados:

Ideal para trabajar con datos como texto, imágenes y audios procesados mediante modelos de machine learning, convirtiéndolos en embeddings (representaciones vectoriales).


-  Indexación eficiente:

Implementa estructuras de datos avanzadas, como HNSW (Hierarchical Navigable Small World), para realizar búsquedas rápidas y escalables en espacios vectoriales de alta dimensionalidad.

-  Actualizaciones dinámicas:

Permite agregar, eliminar o actualizar vectores en tiempo real, lo que la hace ideal para aplicaciones en evolución.

-  Compatibilidad con aplicaciones modernas:

Ofrece una API RESTful, lo que facilita la integración con servicios y lenguajes como Python, JavaScript, y más.
Tiene conectores para bibliotecas como LangChain, lo que permite integrarse fácilmente con flujos de trabajo de IA.

- Soporte para filtros estructurados:

Combina búsquedas vectoriales con filtros estructurados para hacer consultas más específicas.

**docker**

```
docker run -p 6333:6333 qdrant/qdrant
```

Qdrant is now accessible:

- REST API: localhost:6333
- Web UI: localhost:6333/dashboard
- GRPC API: localhost:6334






## Hugging Face

Hugging Face is a community or company that develops and builds open-source tools to facilitate the use of NLP(Natural Language Processing) tasks. And with this it also enables users to build, train, and deploy machine learning models based on open-source code and technologies.





# nvidia

The NVIDIA® CUDA® Toolkit provides a development environment for creating high-performance, GPU-accelerated applications. With it, you can develop, optimize, and deploy your applications on GPU-accelerated embedded systems, desktop workstations, enterprise data centers, cloud-based platforms, and supercomputers. The toolkit includes GPU-accelerated libraries, debugging and optimization tools, a C/C++ compiler, and a runtime library.

https://developer.nvidia.com/cuda-toolkit


# Gemini Cli

Gemini CLI es una herramienta de línea de comandos (Command Line Interface) desarrollada por Google que te permite interactuar directamente desde tu terminal con los modelos de inteligencia artificial Gemini, como Gemini Pro y Gemini Pro Vision.

En lugar de usar la interfaz web con chatbots (como Bard, ahora Gemini), la CLI te da acceso a los modelos a través de comandos de texto, lo que la hace ideal para automatizar tareas, integrarla en scripts y flujos de trabajo de desarrollo.

