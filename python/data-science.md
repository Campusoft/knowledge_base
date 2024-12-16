# Data Science


# Libraries for Data Science

- TensorFlow
- ScyPy
- NumbPy
- Pandas
- Matplotlib
- Keras
- SciKit-Learn
- PyTorch
- Scrapy
- BeautifulSoup


# Libraries for read/write Excel data
- OpenPyXL [https://openpyxl.readthedocs.io/](https://openpyxl.readthedocs.io/)
Library to read/write Excel 2010 xlsx/xlsm/xltx/xltm files.

- xlrd [https://xlrd.readthedocs.io/](https://xlrd.readthedocs.io/)
Library for reading data and formatting information from Excel files in the historical .xls format.

# Excel validator
https://github.com/mkesicki/excel_validator/tree/master/validator

Allow to configure data validation using YAML files



# Pandas

In pandas, a data table is called a DataFrame.


A DataFrame is a 2-dimensional data structure that can store data of different types (including characters, integers, floating point values, categorical data and more) in columns. It is similar to a spreadsheet, a SQL table or the data.frame in R.

Each column in a DataFrame is a Series


# PyTorch

PyTorch es un marco de trabajo (framework) de machine learning de código abierto desarrollado por Facebook AI Research. Está diseñado para la creación y el entrenamiento de modelos de aprendizaje profundo (deep learning) y aprendizaje automático (machine learning) de forma flexible y eficiente.


** Características principales de PyTorch ** 

- Grafos computacionales dinámicos:

A diferencia de otros frameworks como TensorFlow (en sus versiones anteriores), PyTorch utiliza un enfoque dinámico para crear grafos computacionales. Esto significa que el grafo se genera "en el momento" mientras se ejecuta el código, lo que facilita la depuración y la experimentación.

-  Soporte de GPU:

PyTorch tiene integración nativa con CUDA, lo que permite aprovechar la potencia de las GPU para acelerar el entrenamiento y la inferencia de modelos.

-  API sencilla y estilo Python:

Su diseño es intuitivo para quienes ya tienen experiencia en Python, utilizando conceptos claros y simples.

- Biblioteca de funciones optimizadas:

Incluye herramientas avanzadas para cálculos matemáticos, procesamiento de tensores y operaciones de redes neuronales.

- Ampliable y modular:

Permite a los desarrolladores crear modelos personalizados y reutilizar módulos existentes para acelerar el desarrollo.

-  Herramientas de desarrollo:

Bibliotecas complementarias como torchvision (para procesamiento de imágenes), torchaudio (para datos de audio) y torchtext (para datos de texto).

-  Comunidad activa:

PyTorch tiene una comunidad vibrante y en constante crecimiento, con contribuciones frecuentes que mejoran la funcionalidad y el soporte.


** Install **

Currently, PyTorch on Windows only supports Python 3.9-3.12; Python 2.x is not supported.


# Generar embeddings con python

Algunas populares son:

- Hugging Face Transformers:

Ofrece modelos como Sentence-BERT.
Ideal para tareas de texto, como emparejar preguntas y respuestas.

-  Sentence-Transformers:

Especialmente diseñada para generar embeddings semánticos.
Modelos recomendados:

all-MiniLM-L6-v2 (rápido y preciso).
multi-qa-MiniLM-L6-v2 (optimizado para búsqueda).

- OpenAI API (si permites servicios no locales):

Utiliza text-embedding-ada-002 para generar embeddings.


**openai Vector embeddings**

https://platform.openai.com/docs/guides/embeddings/embedding-models



# LangChain


**Install**


It is best practice to create a virtual environment to manage dependencies effectively. You may use venv or conda for creating isolated environments.


Create a virtual environment. Replace env_name with your desired environment name:

```
python -m venv env_name
```

Activate the virtual environment:

On Windows:

```
.\env_name\Scripts\activate

```

On macOS or Linux:

```
source env_name/bin/activate
```


## Referencias


Unlocking the Power of Sentence Embeddings with all-MiniLM-L6-v2
There are two main ways to use this model:

- With Sentence-Transformers (an easy-to-use Python library).
- With Hugging Face Transformers (if you prefer using Hugging Face’s library).

https://medium.com/@rahultiwari065/unlocking-the-power-of-sentence-embeddings-with-all-minilm-l6-v2-7d6589a5f0aa