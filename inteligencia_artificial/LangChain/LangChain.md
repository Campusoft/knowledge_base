# LangChain

> Documento revisado: 2026-09-26
> Sitio oficial: [docs.langchain.com](https://docs.langchain.com/)

## Descripción

**LangChain** es un framework open source para construir aplicaciones basadas en modelos de lenguaje y agentes de IA. Proporciona abstracciones e integraciones para conectar modelos, mensajes, herramientas, fuentes de datos, memoria, recuperación de información y flujos de ejecución.

Su propósito no es entrenar modelos, sino facilitar la construcción de aplicaciones que utilizan modelos existentes de distintos proveedores. Puede utilizarse con Python y JavaScript/TypeScript y permite comenzar con componentes sencillos o evolucionar hacia agentes y workflows más controlados.

LangChain forma parte de un ecosistema que incluye:

- **LangChain**: componentes, integraciones y APIs para aplicaciones LLM y agentes.
- **LangGraph**: runtime y framework orientado a workflows/agentes con estado, ciclos, persistencia y control explícito del flujo.
- **LangSmith**: trazabilidad, depuración, evaluación, pruebas y monitoreo de aplicaciones de IA.
- **Deep Agents**: una capa más opinada para agentes con planificación, filesystem, subagentes y gestión de contexto.

## Conceptos fundamentales

### Modelo de chat

Es la interfaz con el proveedor del modelo. Recibe mensajes y devuelve respuestas, tool calls o salidas estructuradas. LangChain ofrece una interfaz común para trabajar con proveedores diferentes, aunque las capacidades concretas dependen de cada modelo.

### Mensajes

Representan la conversación y el contexto enviado al modelo. Los roles habituales son `system`, `user`, `assistant` y `tool`. También pueden incluir contenido multimodal, metadatos y llamadas a herramientas.

### Prompt y plantilla

Un prompt define las instrucciones y el contexto que recibe el modelo. Las plantillas permiten separar la estructura del prompt de los valores variables, como la pregunta del usuario, documentos recuperados o datos de una tarea.

### Herramientas (tools)

Son funciones que el modelo puede solicitar para actuar sobre el mundo externo: consultar una API, buscar información, ejecutar una operación de base de datos, leer un archivo o llamar a un servicio interno.

Una herramienta debe tener:

- nombre claro;
- descripción precisa;
- esquema de entrada validado;
- permisos definidos;
- manejo de errores y límites de tiempo.

### Agente

Un agente combina un modelo con herramientas y un ciclo de decisión. El modelo interpreta el objetivo, decide si necesita usar una herramienta, analiza el resultado y continúa hasta producir una respuesta o llegar a un límite.

En las versiones actuales, `create_agent` proporciona una forma estándar de crear agentes. Para necesidades más específicas, el flujo puede modelarse explícitamente con LangGraph.

### Middleware

El middleware permite intervenir en el ciclo del agente sin modificar el modelo principal. Puede utilizarse para implementar reintentos, resumen de contexto, moderación, políticas de permisos, logging, selección dinámica de modelos o validación de entradas y salidas.

### Salida estructurada

Permite solicitar al modelo una respuesta que cumpla un esquema definido, por ejemplo JSON, un objeto tipado o un esquema Pydantic/Zod. Es preferible a extraer datos mediante parsing frágil de texto libre.

### Documentos, embeddings y retrievers

- **Document**: contenido y metadatos, como una página, registro, archivo o fragmento.
- **Embedding**: representación vectorial de texto u otro contenido.
- **Vector store**: almacenamiento y búsqueda de embeddings.
- **Retriever**: componente que recibe una consulta y devuelve documentos relevantes.

Estos componentes son la base de muchos sistemas RAG.

### RAG

**Retrieval-Augmented Generation** combina recuperación de información con generación de texto. La aplicación busca documentos relevantes, los incorpora al contexto del modelo y genera una respuesta fundamentada en esos documentos.

### SelfQueryRetriever

`SelfQueryRetriever` utiliza un modelo intermedio para convertir una consulta en lenguaje natural en dos partes:

1. una consulta semántica para recuperar contenido;
2. filtros estructurados sobre los metadatos.

Por ejemplo, una consulta como “contratos de 2025 del área financiera” puede transformarse en una búsqueda semántica acompañada de filtros como `año = 2025` y `área = financiera`. La calidad depende de la descripción de los metadatos, del modelo y de las capacidades del vector store.

### Estado y persistencia

Los agentes sencillos pueden ser stateless. Los workflows complejos suelen necesitar estado para conservar mensajes, resultados intermedios, aprobaciones, checkpoints y reanudación. LangGraph proporciona primitivas para modelar este tipo de ejecución.

## Características

- Interfaces comunes para modelos de distintos proveedores.
- Integraciones con modelos de chat, embeddings, vector stores, loaders y APIs.
- Construcción de agentes con herramientas.
- Soporte para salidas estructuradas.
- RAG y recuperación con filtros de metadatos.
- Composición de pasos mediante workflows.
- Middleware para políticas, reintentos, resumen y control del ciclo.
- Streaming de tokens, eventos y actualizaciones de ejecución.
- Soporte para Python y JavaScript/TypeScript.
- Integración con LangGraph para agentes con estado y flujos durables.
- Integración con LangSmith para traces, evaluación y monitoreo.
- Posibilidad de iniciar con un prototipo y reemplazar componentes por implementaciones más especializadas cuando crece el sistema.

## Arquitectura

Una aplicación típica puede entenderse en capas:

```text
Aplicación
  ├── API, interfaz web, CLI, chatbot o proceso programado
  │
  ├── Orquestación
  │     ├── create_agent para agentes estándar
  │     ├── LangGraph para workflows con estado
  │     └── middleware, aprobaciones y límites
  │
  ├── Razonamiento
  │     ├── modelo de chat
  │     ├── prompts y mensajes
  │     └── salida estructurada
  │
  ├── Acción y conocimiento
  │     ├── tools y APIs
  │     ├── loaders y splitters
  │     ├── embeddings y vector stores
  │     └── retrievers / SelfQueryRetriever
  │
  └── Operación
        ├── LangSmith: trazas y evaluación
        ├── persistencia y checkpoints
        ├── logs, métricas y alertas
        └── autenticación, permisos y sandbox
```

### Flujo de un agente

1. La aplicación recibe una solicitud.
2. Se construyen los mensajes y el contexto.
3. El modelo decide responder o solicitar una herramienta.
4. El runtime valida y ejecuta la herramienta si está permitido.
5. El resultado de la herramienta vuelve al contexto.
6. El modelo interpreta el resultado y continúa o finaliza.
7. La aplicación devuelve la respuesta y registra la ejecución.

### LangChain y LangGraph

LangChain es apropiado para crear agentes y componentes rápidamente. LangGraph es útil cuando se necesita controlar explícitamente nodos, transiciones, ciclos, estado, reintentos, interrupciones, aprobación humana o reanudación.

No son alternativas excluyentes: un nodo de LangGraph puede utilizar un agente de LangChain, un retriever, un modelo o una herramienta.

### LangSmith

LangSmith registra trazas de la ejecución, incluyendo entradas, respuestas del modelo, llamadas a herramientas y decisiones del agente. Esto facilita depurar errores, comparar versiones, crear datasets de evaluación y monitorear aplicaciones en producción.

## Casos de uso

### Aplicaciones RAG

Construir asistentes que respondan usando documentación interna, manuales, contratos, tickets, bases de conocimiento o sitios web indexados.

### Agentes con herramientas

Crear asistentes capaces de consultar APIs, buscar información, ejecutar cálculos, interactuar con sistemas internos o preparar acciones para aprobación humana.

### Extracción estructurada

Extraer entidades, fechas, importes, clasificaciones, relaciones o campos de documentos con un esquema validable.

### Chatbots con contexto

Implementar asistentes para soporte, ventas, operaciones o recursos humanos que mantengan historial y consulten fuentes autorizadas.

### Automatización de procesos

Modelar procesos de varios pasos, como analizar una solicitud, validar datos, consultar sistemas, pedir aprobación y generar un resultado.

### Investigación y síntesis

Coordinar búsqueda, recuperación, lectura, comparación y generación de informes con fuentes y trazabilidad.

### Clasificación y enrutamiento

Clasificar solicitudes, detectar intención, elegir un modelo o herramienta y derivar cada caso a un flujo especializado.

### Evaluación de aplicaciones de IA

Usar LangSmith para comparar prompts, modelos, retrievers, herramientas y versiones de una aplicación con datasets y evaluadores.

## Ejemplo conceptual de agente

```python
from langchain.agents import create_agent


def consultar_pedidos(cliente: str) -> str:
    """Consulta el estado de los pedidos de un cliente autorizado."""
    # La implementación real debe validar identidad y permisos.
    return f"Pedidos encontrados para {cliente}"


agent = create_agent(
    model="openai:gpt-5.5",
    tools=[consultar_pedidos],
    system_prompt=(
        "Responde con precisión. Usa consultar_pedidos solo cuando sea necesario "
        "y no inventes estados de pedidos."
    ),
)

result = agent.invoke({
    "messages": [
        {"role": "user", "content": "¿Cuál es el estado de mis pedidos?"}
    ]
})
```

El nombre del modelo, la integración del proveedor y el formato de invocación pueden cambiar según la versión y el paquete utilizado. El esquema de entrada de una herramienta debe ser explícito y la función real debe aplicar autorización antes de consultar o modificar datos.

## Recomendaciones

### Diseño

1. Empezar con el flujo más simple que resuelva el caso; no convertir cada llamada a un modelo en un agente.
2. Separar modelo, prompt, herramientas, recuperación, estado y presentación.
3. Usar `create_agent` para agentes estándar y LangGraph cuando se requiera un flujo explícito o durable.
4. Definir objetivos, criterios de finalización y límites de iteración.
5. Mantener las herramientas pequeñas, deterministas y con esquemas claros.

### RAG

1. Medir la calidad de la recuperación por separado de la calidad de la respuesta.
2. Conservar metadatos útiles y diseñar filtros antes de elegir `SelfQueryRetriever`.
3. Usar un vector store persistente en producción; los stores en memoria sirven para pruebas y ejemplos.
4. Evaluar tamaño de fragmentos, solapamiento, embeddings, cantidad de documentos y estrategia de reranking.
5. Instruir al modelo para diferenciar contexto confiable de instrucciones contenidas dentro de documentos.

### Seguridad

1. Aplicar mínimo privilegio a herramientas, APIs, bases de datos y archivos.
2. Requerir aprobación humana para borrados, publicaciones, compras, cambios en producción y operaciones irreversibles.
3. No insertar secretos en prompts, documentos indexados, logs ni trazas.
4. Tratar el contenido recuperado como datos no confiables para reducir riesgos de prompt injection.
5. Validar entradas y salidas con esquemas y políticas de negocio, no solo con instrucciones al modelo.

### Producción

1. Configurar timeouts, reintentos con backoff, límites de costo y cancelación.
2. Registrar modelo, versión de prompt, herramientas, documentos recuperados y resultado, respetando privacidad.
3. Usar LangSmith u otra solución de observabilidad para analizar trazas y regresiones.
4. Crear evaluaciones automatizadas para exactitud, relevancia, groundedness, seguridad y trayectoria de herramientas.
5. Fijar versiones de paquetes e integraciones y revisar cambios antes de actualizar.
6. Probar fallos de proveedor, respuestas inválidas, herramientas no disponibles, contexto demasiado grande y sesiones interrumpidas.

## Limitaciones

- La abstracción común no elimina las diferencias entre proveedores y modelos.
- Los agentes pueden llamar herramientas de forma incorrecta o detenerse antes de completar el objetivo.
- Las respuestas estructuradas reducen errores de formato, pero no garantizan que los valores sean correctos.
- RAG puede recuperar documentos irrelevantes, incompletos o desactualizados.
- La memoria y el estado agregan complejidad, costo y requisitos de privacidad.
- Un workflow multiagente puede aumentar latencia, consumo de tokens y dificultad de depuración.
- Las integraciones cambian de forma independiente al núcleo de LangChain.
- LangSmith es útil para observabilidad, pero introduce una decisión adicional sobre datos enviados, retención y cumplimiento.
- El código de ejemplo no sustituye autenticación, autorización, validación, sandboxing ni pruebas.

## LangChain, LangGraph y MCP

- **LangChain**: componentes y abstracciones para aplicaciones LLM y agentes.
- **LangGraph**: ejecución y orquestación de workflows con estado.
- **MCP**: protocolo para conectar agentes/modelos con herramientas y fuentes de contexto externas.

Una aplicación puede combinar los tres: LangChain define el agente, LangGraph controla el workflow y MCP expone herramientas externas.

## Resumen

LangChain es un framework para construir aplicaciones LLM y agentes con modelos, herramientas, recuperación, salida estructurada y observabilidad. Su valor está en reducir el código de integración y ofrecer componentes reutilizables sin impedir que el equipo controle la arquitectura.

Para prototipos, `create_agent` y las integraciones preconstruidas suelen ser suficientes. Para sistemas con estado, ciclos, aprobaciones, subagentes y reanudación conviene incorporar LangGraph. En producción, la prioridad debe ser la evaluación, trazabilidad, seguridad, control de costos y validación de las herramientas.

## Fuentes

- [Documentación oficial de LangChain](https://docs.langchain.com/oss/python/langchain/overview)
- [Agentes en LangChain](https://docs.langchain.com/oss/python/langchain/agents)
- [LangGraph](https://docs.langchain.com/oss/python/langgraph/overview)
- [LangSmith: observabilidad](https://docs.langchain.com/langsmith/observability)
- [LangSmith: evaluación de RAG](https://docs.langchain.com/langsmith/evaluate-rag-tutorial)
- [LangChain Reference](https://reference.langchain.com/)
