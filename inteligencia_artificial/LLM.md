# LLM

Un LLM es esencialmente un sistema que tomando unas palabras como punto de partida (input) predice qué palabras son las más probables que ocurran a continuación (output). El sistema toma el input y calcula varias posibilidades asignando una probabilidad a cada una, y selecciona una en base a distintos criterios (su probabilidad, su conexión con otras palabras seleccionadas en el mismo texto...)


LangChain is a framework for developing applications powered by language models.
- AI orchestration layer to build intelligent apps
https://github.com/langchain-ai/langchain



LLMs offer a natural language interface between humans and data. LLMs come pre-trained on huge amounts of publicly available data, but they are not trained on your data. Your data may be private or specific to the problem you're trying to solve. It's behind APIs, in SQL databases, or trapped in PDFs and slide decks.


The most popular example of context-augmentation is Retrieval-Augmented Generation or RAG, which combines context with LLMs at inference time.


# Proveedores

## OpenRouter

OpenRouter.ai es una plataforma que funciona como un API unificado para acceder a múltiples modelos de lenguaje (LLMs) provenientes de distintos proveedores. En lugar de manejar varias APIs, cuentas y facturaciones, OpenRouter permite usar un solo endpoint para interactuar con cientos de modelos, optimizando interoperabilidad, costos y rendimiento.


Funcionalidades clave:

- Interfaz única y estándar: Compatible con SDK como el de OpenAI, facilitando su adopción
- Optimización de enrutamiento: Elige automáticamente el modelo más adecuado según criterios como costo, latencia y rendimiento
- Alta disponibilidad: Cuenta con infraestructura distribuida que permite fallback ante fallos de proveedores
- Facturación simplificada: Acumulas créditos y pagas por uso (inferencia), evitando manejar múltiples cuentas y suscripciones
- Escaneo de modelos recientes: Acceso a modelos emergentes de OpenAI, Anthropic, Meta, Google, DeepSeek y más


Cobran una comisión del 5% sobre el costo de inferencia, ya que ellos gestionan el enrutamiento entre múltiples proveedores

