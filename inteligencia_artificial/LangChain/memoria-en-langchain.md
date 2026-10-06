# Memoria en LangChain: tipos, implementaciones y buenas prácticas

**Versión:** 1.0  
**Fecha:** 2026-09-30  
**Alcance:** memoria de agentes LangChain actuales (LangChain v1 y runtime LangGraph), opciones de persistencia y referencias a implementaciones complementarias en Python.  
**Estado:** guía técnica; verificar versiones y compatibilidad al implementar.

## 1. Resumen ejecutivo

Un modelo de lenguaje no conserva por sí mismo el historial entre llamadas. La aplicación debe recuperar y enviar el contexto que corresponda en cada interacción. En las versiones actuales de LangChain, la memoria de un agente se organiza principalmente con dos mecanismos de LangGraph:

- **Checkpointer:** guarda el estado de una ejecución/hilo, normalmente incluidos los mensajes de una conversación. Es memoria de corto plazo y se identifica con `thread_id`.
- **Store:** guarda datos de aplicación que deben estar disponibles en distintos hilos, como preferencias estables de un usuario. Es la base de la memoria de largo plazo y se organiza con namespaces y keys.

El checkpointer conserva el estado, pero no resume automáticamente toda conversación ni impone qué hechos recordar. El Store tampoco determina por sí solo qué dato es cierto o relevante. La aplicación decide qué se guarda, quién puede leerlo, cuándo se recupera y cómo se elimina.

En nuevos agentes, conviene usar checkpointer y Store actuales de LangGraph. Las clases antiguas de memoria de LangChain siguen siendo relevantes al mantener aplicaciones heredadas, pero no son la opción recomendada para diseñar un agente nuevo en LangChain v1.

## 2. Qué significa “memoria” en un agente

La memoria es el contexto que la aplicación conserva y vuelve a proporcionar para que un agente pueda continuar una tarea, relacionar turnos o personalizar una interacción. Puede residir en el estado del grafo, una base de datos, un Store, un índice vectorial, el perfil del cliente o un sistema de negocio.

Conviene distinguir memoria de estas capacidades relacionadas:

| Concepto | Para qué sirve | Ejemplo |
|---|---|---|
| Historial conversacional | Continuar el hilo actual. | El usuario ya indicó en esta conversación qué variante quiere. |
| Memoria de largo plazo del usuario | Personalizar conversaciones futuras. | El usuario prefiere retiro en tienda. |
| Base de conocimiento/RAG | Responder con información de una fuente documental. | Políticas vigentes, manuales o fichas aprobadas. |
| Estado transaccional | Representar la situación oficial de una operación. | Pedido cotizado, pago pendiente o pedido confirmado. |
| Contexto de ejecución | Datos temporales para completar un paso del grafo. | Candidatos de búsqueda mientras se desambigua un producto. |

No se debe usar la memoria conversacional como fuente de verdad para pedidos, pagos, precios, permisos, stock o políticas oficiales. Esos datos deben consultarse en su sistema de dominio.

## 3. Clasificación por duración y alcance

### 3.1 Memoria de corto plazo

La memoria de corto plazo dura lo que dure una conversación o tarea. Incluye el historial de mensajes, los resultados recientes de herramientas, el estado de una ruta, las selecciones todavía pendientes y las respuestas necesarias para completar la interacción.

En LangChain actual se conserva en el estado del agente y se hace durable entre invocaciones mediante un **checkpointer** asociado a un `thread_id`.

Características:

- Se limita a un hilo de conversación/tarea.
- Puede incluir mensajes y cualquier otro campo definido en el estado del agente.
- Puede permitir pausar y reanudar una ejecución.
- Puede crecer demasiado para el límite de contexto del modelo; requiere recorte, resumen o política de retención.
- Debe poder expirar o eliminarse según la política del producto.

### 3.2 Memoria de largo plazo

La memoria de largo plazo sobrevive al hilo actual y puede recuperarse en conversaciones posteriores. Suele representar datos específicos de un usuario, hechos o episodios útiles, preferencias confirmadas o instrucciones estables.

En el ecosistema LangChain se suele guardar en un **Store**. El Store ofrece acceso por namespace/key; algunos Stores permiten además indexar campos con embeddings para búsqueda semántica.

Características:

- Persiste entre conversaciones e invocaciones.
- Debe estar particionada por tenant/usuario o por el ámbito correcto.
- Debe tener reglas explícitas para creación, actualización, conflicto, expiración y borrado.
- Solo se recupera el subconjunto útil para la petición actual; no se inserta toda la memoria en cada prompt.

## 4. Clasificación por contenido: ortogonal a la duración

Los términos semántica, episódica y procedimental describen **qué** representa un recuerdo; corto/largo plazo describen **cuánto dura y dónde se aplica**. Por ejemplo, un episodio reciente puede estar en un checkpoint corto y luego resumirse como un dato duradero del Store.

| Tipo de contenido | Qué representa | Ejemplo | Cuidado principal |
|---|---|---|---|
| Memoria de trabajo | Contexto necesario para el paso en curso. | Productos candidatos y variante pendiente de confirmar. | No convertirla automáticamente en preferencia permanente. |
| Conversacional | Turnos recientes del hilo. | Pregunta, respuesta y aclaración actual. | Tamaño, retención y privacidad del historial. |
| Semántica de usuario | Hechos o preferencias relativamente estables. | “Prefiere respuestas breves”; “es vegetariano”. | Confirmar que sea útil, correcta y permitida. |
| Episódica | Sucesos o interacciones concretas. | “En la conversación anterior pidió cambiar la dirección”. | Fecha, contexto, sensibilidad y vencimiento. |
| Procedimental | Reglas sobre cómo atender una tarea. | Instrucciones aprobadas para tramitar una devolución. | Debe provenir de una fuente administrada, no inferirse de una charla. |
| Conocimiento externo | Información general o documental recuperada. | Manual, catálogo o política vigente. | Control de fuente, versión y acceso; no mezclar con memoria personal. |

Las políticas, procedimientos oficiales y transacciones deben ser datos administrados en sus sistemas fuente. La memoria del agente puede guardar referencias o resúmenes útiles, pero no debe reemplazarlos.

## 5. Mecanismos modernos de memoria en LangChain

### 5.1 Estado de agente y `messages`

Los agentes de LangChain mantienen estado durante la ejecución. Por defecto, el estado suele incluir una secuencia de mensajes; se puede ampliar para representar información adicional necesaria al workflow.

El estado existe durante una ejecución incluso sin persistencia durable. Para continuar entre llamadas separadas, se configura un checkpointer. En cada turno se envía el nuevo mensaje y se reutiliza el mismo `thread_id`.

```python
from langchain.agents import create_agent
from langgraph.checkpoint.memory import InMemorySaver

agent = create_agent(
    model="openai:gpt-5.5",
    tools=[],
    checkpointer=InMemorySaver(),
)

config = {"configurable": {"thread_id": "conversation-123"}}

agent.invoke(
    {"messages": [{"role": "user", "content": "Me llamo Ana."}]},
    config=config,
)

# Mismo thread_id: el agente puede utilizar el historial previo.
result = agent.invoke(
    {"messages": [{"role": "user", "content": "¿Cómo me llamo?"}]},
    config=config,
)
print(result["messages"][-1].content)
```

`InMemorySaver` facilita aprendizaje y desarrollo: el estado desaparece al detener el proceso y no debe usarse como persistencia de producción. La identidad del hilo debe ser estable para esa conversación, pero distinta para conversaciones independientes.

### 5.2 Checkpointers de corto plazo

Los checkpointers implementan la persistencia del estado del grafo por hilo. LangGraph ofrece una interfaz base y adaptadores instalables:

| Implementación | Paquete Python | Uso típico | Persistencia y observaciones |
|---|---|---|---|
| `InMemorySaver` | Incluido con la dependencia base de checkpoint de LangGraph. | Ejemplos, pruebas manuales, desarrollo local. | Solo memoria del proceso; se pierde al reiniciar y no se comparte entre réplicas. |
| `SqliteSaver` / `AsyncSqliteSaver` | `langgraph-checkpoint-sqlite` | Laboratorio local o aplicación pequeña de una sola instancia. | Archivo SQLite durable, pero no es automáticamente una solución de alta disponibilidad o concurrencia distribuida. |
| `PostgresSaver` / `AsyncPostgresSaver` | `langgraph-checkpoint-postgres` | Servicios con base PostgreSQL y requisitos de producción. | Estado durable y compartible por instancias según el diseño y configuración de PostgreSQL. Requiere preparar el esquema y administrar conexiones/backups. |
| `MongoDBSaver` / `AsyncMongoDBSaver` | `langgraph-checkpoint-mongodb` | Organizaciones que ya operan MongoDB y prefieren ese almacenamiento. | Adaptador separado; revisar índices, compatibilidad y operación de la versión elegida. |
| Redis saver | `langgraph-checkpoint-redis` (integración Redis) | Entornos que ya operan Redis y desean alojar checkpoints allí. | Verificar semántica de durabilidad, persistencia de Redis y matriz de compatibilidad del paquete. |
| Azure Cosmos DB saver | `langchain-azure-cosmosdb` | Arquitecturas que utilizan Azure Cosmos DB. | Adaptador específico del proveedor; revisar región, autenticación y compatibilidad. |

Los paquetes y sus clases cambian con el tiempo. Confirmar nombre, versión, import path y soporte async en la documentación del adaptador antes de fijar dependencias. Todos los checkpointers deben tratarse como datos sensibles si almacenan mensajes, identificadores o respuestas de herramientas.

### 5.3 Store de largo plazo

Un Store guarda documentos JSON organizados por **namespace** y **key**. La aplicación elige el namespace para aislar ámbitos y la key para recuperar o actualizar un registro concreto.

Ejemplo conceptual de partición:

```text
namespace = (tenant_id, user_id, memory_category)
key       = stable_memory_id
value     = { fact, source, confidence, created_at, expires_at }
```

Los Stores posibles incluyen:

| Implementación | Ejemplos | Uso y límites |
|---|---|---|
| En memoria | `InMemoryStore` (`langgraph.store.memory`) | Aprendizaje y pruebas; desaparece al finalizar el proceso. Puede habilitar búsqueda semántica de desarrollo con embeddings configurados. |
| PostgreSQL | `PostgresStore` / `AsyncPostgresStore`, integración de Store de LangGraph para Postgres | Persistencia de largo plazo y búsqueda por namespace/key; puede combinarse con indexación semántica según el adaptador. Revisar el paquete que contiene el Store en la versión elegida. |
| MongoDB | `MongoDBStore` (integración correspondiente) | Store document-oriented para instalaciones que ya operan MongoDB. Confirmar APIs y paquete vigentes. |
| Redis | `RedisStore` de la integración Redis | Store key-value con opciones de búsqueda vectorial proporcionadas por la integración/configuración. |
| Store propio | Implementación de `BaseStore` | Útil cuando hay una base corporativa o política de persistencia propia; aumenta el trabajo de consistencia, concurrencia y operación. |

El Store puede hacer búsqueda exacta o, si se configura un índice con embeddings, búsqueda semántica. La indexación no hace que los recuerdos sean automáticamente verdaderos o adecuados: la aplicación sigue definiendo aislamiento, filtros, relevancia, ranking, vencimiento y texto que llega al prompt.

### 5.4 Resumen, recorte y administración del historial

El checkpointer puede conservar un historial grande, pero el modelo solo puede procesar una ventana limitada. Son estrategias distintas y complementarias:

- **Recortar:** enviar solo los últimos N mensajes o los que caben en un presupuesto de tokens.
- **Resumir:** condensar turnos antiguos en un resumen y mantener los mensajes recientes completos.
- **Eliminar/redactar:** retirar mensajes que no se necesitan o que contienen datos que no deben persistir.
- **Mantener campos separados:** conservar resumen, mensajes recientes y datos de flujo de forma explícita en el estado.

LangChain proporciona middleware de resumen para agentes; la activación y umbrales dependen de la versión y configuración. Un resumen es una compresión potencialmente imperfecta, no un registro transaccional. Para pedidos, pagos, consentimiento o direcciones, consultar el registro del sistema correspondiente.

## 6. Opciones complementarias y paquetes externos

Una “memoria” puede ser un paquete Python local, un adaptador de base de datos o un servicio separado. Instalar una biblioteca no significa que ya exista persistencia: algunos paquetes requieren una base de datos, un vector store, una clave de API o un servicio administrado.

| Opción | Tipo de memoria | Paquete/servicio orientativo | Cuándo evaluarla |
|---|---|---|---|
| LangMem | Extracción, actualización y búsqueda de memoria de largo plazo con LLM; trabaja con Stores. | `langmem` | Cuando se quiere que un proceso de memoria extraiga y consolide recuerdos, en primer plano o en segundo plano, en vez de guardar cada turno sin procesar. Requiere políticas para validar lo que se aprende. |
| Mem0 | Capa de memoria de usuario con operaciones de guardar y buscar; opciones OSS y administradas. | `mem0ai` / `mem0`, según SDK y modalidad | Cuando se busca una capa dedicada que extraiga hechos y permita recuperar recuerdos por usuario. Confirmar si la configuración elegida usa servicios hospedados o infraestructura propia. |
| Zep | Plataforma/servicio de memoria de largo plazo y contexto temporal para agentes. | SDK Python de Zep, por ejemplo `zep-cloud` en la línea actual | Cuando se evalúa un servicio especializado. Revisar versión, modalidad hospedada/autoalojada, residencia de datos y compatibilidad del SDK. Algunas clases históricas de integración LangChain tienen requisitos de `langchain_core` antiguos; evitar asumir compatibilidad con LangChain v1. |
| Bases vectoriales | Recuperación de recuerdos textuales por similitud semántica; son almacenamiento/índice, no política completa de memoria. | Adaptadores LangChain para Chroma, Qdrant, Redis, PostgreSQL/pgvector, Pinecone, Weaviate, Milvus, entre otros | Cuando hay suficientes recuerdos/documentos para recuperar por relevancia semántica. Cada opción tiene paquete de integración, servicio/configuración, filtros y costos propios. |
| Framework de recuperación distinto | Ingesta, índices y recuperación desde documentos; puede complementar la memoria de agente. | Por ejemplo, LlamaIndex y sus integraciones | Cuando la dificultad central es conectarse a muchas fuentes documentales y construir índices. No reemplaza automáticamente la memoria conversacional/checkpoint de LangGraph. |

Las referencias de paquetes son ejemplos de ecosistema, no instrucciones de instalar todos. Seleccionar un solo mecanismo por responsabilidad y validar el adaptador exacto. Evitar duplicar el mismo recuerdo en varios sistemas sin un plan de sincronización, actualización y borrado.

### 6.1 Vector store no equivale a memoria completa

Un vector store conserva registros y permite recuperar elementos parecidos a una consulta. No define por sí solo:

- Cuándo se crea un recuerdo ni quién puede aprobarlo.
- Cómo se separan usuarios/tenants.
- Cómo se resuelven hechos contradictorios o se actualizan preferencias.
- Qué fuente y fecha acompañan el recuerdo.
- Cuándo caduca o cómo se borra de índices, réplicas y backups.
- Qué recuerdos deben incluirse en el contexto actual.

Se necesita una capa de aplicación que defina esas reglas, incluso cuando el almacenamiento use embeddings.

## 7. Memorias heredadas de LangChain

En LangChain 0.x eran comunes las clases de memoria para cadenas, como:

| Clase histórica | Comportamiento esperado |
|---|---|
| `ConversationBufferMemory` | Conserva el historial completo como buffer. |
| `ConversationBufferWindowMemory` | Conserva una ventana de los últimos turnos. |
| `ConversationSummaryMemory` | Genera un resumen de la conversación. |
| `ConversationSummaryBufferMemory` | Combina resumen de turnos antiguos con un buffer reciente. |
| `ConversationTokenBufferMemory` | Limita el historial según un presupuesto de tokens. |
| `ConversationEntityMemory` | Extrae y acumula información asociada a entidades. |
| `VectorStoreRetrieverMemory` | Indexa mensajes y recupera los más relevantes desde un vector store. |

Con LangChain v1, las cadenas, retrievers y otras APIs heredadas se separaron al paquete **`langchain-classic`** (namespace Python `langchain_classic`). Son útiles para mantener o migrar código antiguo, pero no son la arquitectura primaria de memoria de los agentes actuales. Para nuevos agentes, preferir `AgentState` + checkpointer para corto plazo y Store para largo plazo; usar middleware para administrar contexto. Revisar la [guía oficial de migración a LangChain v1](https://docs.langchain.com/oss/python/migrate/langchain-v1) antes de actualizar aplicaciones heredadas.

No mezclar una memoria de cadena antigua y un checkpointer moderno sin comprender quién agrega el historial. Una doble inyección puede repetir mensajes, aumentar tokens o crear estados inconsistentes.

## 8. Segmentación cuando un agente atiende a varios usuarios

La regla clave es separar **hilo de conversación** de **identidad de usuario**:

| Identificador | Qué identifica | Uso |
|---|---|---|
| `thread_id` | Una conversación o ejecución continuable. | Seleccionar el checkpoint del chat actual. |
| `user_id` | Una persona/cuenta estable. | Recuperar preferencias entre conversaciones. |
| `tenant_id` | Organización o cliente empresarial. | Aislamiento multi-tenant y políticas por organización. |
| `conversation_id` | Conversación del canal externo. | Correlación con Chatwoot/WhatsApp u otro canal. |
| `message_id` | Evento/mensaje individual. | Idempotencia y prevención de procesamiento duplicado. |

### 8.1 Patrón recomendado

1. Resolver `tenant_id` y `user_id` en el backend desde una identidad autenticada o una asociación confiable del canal.
2. Crear un `thread_id` único y estable por conversación, no por usuario. Si un usuario inicia un chat nuevo, crear otro hilo.
3. Guardar la memoria de largo plazo en un namespace que incluya al menos el tenant y el usuario, por ejemplo `(tenant_id, user_id, "preferences")`.
4. Aplicar el mismo namespace/filtro en lectura, búsqueda, actualización y borrado. No hacer búsquedas globales que puedan mezclar clientes.
5. Pasar el contexto validado al agente por el canal de contexto de ejecución, no confiar en un ID escrito por el usuario o generado por el modelo.
6. Mantener estado de pedido y perfil comercial en sistemas de dominio; guardar solo resúmenes o referencias que ayuden a conversar.

Ejemplo de configuración conceptual:

```python
config = {
    "configurable": {
        "thread_id": f"{tenant_id}:{conversation_id}"
    }
}

namespace = (tenant_id, user_id, "preferences")
```

El formato es ilustrativo. En producción se debe normalizar/validar los identificadores, evitar colisiones entre canales y no revelar IDs sensibles al modelo si no son necesarios.

### 8.2 Ejemplo de fuga entre usuarios

Si todos los clientes usan `thread_id="chat"`, compartirán el mismo checkpoint. Si todas sus preferencias se guardan bajo `("users",)` y una key global, las lecturas pueden cruzarse. Un filtro omitido en una consulta semántica puede traer memorias de otra persona aunque los registros estén guardados con IDs separados.

El aislamiento debe verificarse en cada operación, no solo al insertar los datos. Los namespaces ayudan a organizar; no sustituyen autorización, controles de tenant, políticas de base de datos ni pruebas de acceso.

## 9. Buenas prácticas de diseño y operación

### 9.1 Guardar poco, con propósito y procedencia

- Registrar hechos estables que personalizan una atención futura, no cada oración de cada chat como memoria permanente.
- Conservar campos estructurados cuando se conozca su significado: `fact`, `category`, `source`, `created_at`, `updated_at`, `expires_at`, `confidence` y estado de consentimiento.
- Diferenciar dato declarado por el usuario, inferencia del modelo y dato verificado por un sistema.
- No persistir preferencias temporales como permanentes sin confirmación o una regla explícita.
- No guardar secretos, credenciales, datos de pago ni información sensible que no sea necesaria.

### 9.2 Pipeline de escritura

Un patrón seguro de largo plazo es:

```text
Evento/mensaje
  -> detectar candidato a recuerdo
  -> validar relevancia, ámbito, sensibilidad y procedencia
  -> deduplicar y comparar con recuerdos existentes
  -> actualizar con fecha/expiración si corresponde
  -> escribir en namespace del tenant y usuario
  -> registrar auditoría mínima
```

Para información de alto impacto, considerar revisión humana o confirmación del usuario. La extracción automática mediante LangMem o Mem0 puede ser útil, pero no debe autorizar por sí sola acciones ni convertir inferencias en verdades.

### 9.3 Pipeline de lectura

1. Determinar si la solicitud actual necesita memoria histórica.
2. Recuperar solo dentro del ámbito del usuario y tenant autenticados.
3. Aplicar filtros de categoría, vigencia, permisos y sensibilidad.
4. Recuperar un conjunto pequeño y ordenado, con fuente y fecha.
5. Inyectar los datos como contexto no confiable y acotado; instrucciones del recuerdo nunca deben sobreescribir reglas del sistema.
6. Si el recuerdo es contradictorio, antiguo o relevante para una transacción, verificar con el usuario o con el sistema fuente.

### 9.4 Control de contexto y tokenización

- Establecer presupuesto de tokens para instrucciones, historial reciente, resumen, memoria recuperada y respuesta.
- Resumir/recortar historial antes de que supere el contexto útil del modelo.
- No recuperar un top-k grande sin medir precisión y costo.
- Evitar reinyectar en cada turno una memoria que ya está presente en el historial.
- Mantener resúmenes concisos, actualizables y con fecha; regenerarlos cuando la conversación cambie sustancialmente.

### 9.5 Duración, corrección y eliminación

- Definir retención distinta para mensajes, checkpoints, preferencias y episodios.
- Añadir expiración para hechos temporales y revisar su vigencia al recuperar.
- Ofrecer mecanismos para corregir, olvidar o eliminar memorias asociadas a una persona.
- Diseñar borrado coordinado para Store, vector index, cachés, copias y sistemas derivados según la política vigente.
- Evitar depender de que el modelo recuerde ejecutar la operación de borrado: la aplicación debe tener un flujo determinista.

### 9.6 Persistencia y concurrencia

- Usar almacenamiento en memoria solo para pruebas o procesos efímeros.
- En producción con varias réplicas, usar un checkpointer/Store compartido y durable.
- Definir qué ocurre si dos turnos del mismo hilo llegan concurrentemente.
- Diseñar idempotencia para webhooks y escritura de recuerdos.
- Configurar cifrado, control de acceso, backups, límites de conexión y recuperación.
- Registrar métricas de tamaño de estado, latencia de lectura/escritura, fallos y uso de memoria recuperada.

### 9.7 Memoria como dato no confiable

Un recuerdo puede contener contenido malicioso o instrucciones de prompt injection provenientes de mensajes o documentos. No concatenar recuerdos sin delimitarlos. El prompt debe indicar que son datos para contexto y que no cambian las políticas del agente. Mantener autorización y validación fuera del modelo.

## 10. Recomendación de arquitectura para un agente multiusuario

Para un agente que atiende a diferentes usuarios, una arquitectura base sería:

| Necesidad | Componente recomendado |
|---|---|
| Mantener el hilo actual | Estado `messages` + checkpointer configurado por `thread_id` de conversación. |
| Sobrevivir reinicios y múltiples réplicas | Checkpointer respaldado por una base durable, por ejemplo PostgreSQL si ya es operada por la organización. |
| Recordar preferencias entre conversaciones | Store persistente particionado por `(tenant_id, user_id, categoría)`. |
| Buscar recuerdos redactados en lenguaje natural | Indexación semántica opcional sobre campos de memoria, siempre con filtro estricto de tenant/usuario. |
| Reducir el historial enviado al modelo | Recorte o middleware de resumen, preservando turnos recientes relevantes. |
| Conservar datos transaccionales | APIs/bases del dominio; memoria solo como referencia contextual. |
| Extraer/consolidar recuerdos automáticamente | Evaluar LangMem o Mem0 en una fase aparte, con validación, auditoría y evaluación de calidad. |

No es necesario comenzar con un vector database. Para un conjunto pequeño y estructurado de preferencias, un Store key-value con búsqueda exacta puede ser más simple y seguro. Añadir búsqueda semántica cuando haya una necesidad medida de encontrar recuerdos por significado.

## 11. Matriz de decisión rápida

| Escenario | Opción inicial |
|---|---|
| Probar memoria en un ejemplo local | `InMemorySaver` y `InMemoryStore`. |
| Guardar historial durable en una instancia pequeña | SQLite checkpointer, evaluando límites de concurrencia/operación. |
| Servicio de producción con varias instancias | PostgreSQL checkpointer o una integración de base durable ya soportada y operada. |
| Recordar preferencias por usuario en el mismo sistema | Store persistente con namespace tenant/user. |
| Preguntas sobre archivos/documentos de negocio | RAG/vector store con fuentes versionadas; no llamarlo memoria personal. |
| Extraer hechos automáticamente desde conversaciones | Evaluar LangMem o Mem0; medir falsos recuerdos y mecanismos de corrección/borrado. |
| Mantener cadenas antiguas | `langchain-classic` de forma acotada y planificar migración. |

## 12. Riesgos frecuentes

| Riesgo | Causa | Mitigación |
|---|---|---|
| El agente “olvida” entre turnos | No se configuró checkpointer o cambió el `thread_id`. | Persistir un identificador estable por conversación y verificarlo en cada invocación. |
| Se mezclan conversaciones | Se usa un ID común o el ID del usuario como hilo para todos sus chats. | Separar `thread_id` de `user_id`; un hilo por conversación. |
| Fuga entre clientes | Namespace/filtro incorrecto u operación global de búsqueda. | Tenant y usuario en cada acceso; autorización en backend y pruebas de aislamiento. |
| El prompt se hace demasiado grande | Historial acumulado y recuperación indiscriminada. | Presupuesto, recorte, resumen y top-k pequeño. |
| Se usa un recuerdo desactualizado | No hay fecha, TTL ni verificación de vigencia. | Procedencia, expiración y consulta a la fuente autoritativa. |
| Una preferencia inferida se guarda como hecho | Extracción automática sin validación. | Etiquetar procedencia/confianza; confirmar preferencias sensibles o ambiguas. |
| No se elimina completamente un recuerdo | Está duplicado en Store, vectores, cachés o resúmenes. | Catálogo de datos derivados y flujo de borrado centralizado. |
| Se repite un efecto externo al reanudar | Checkpoint y transacción de API no son atómicos. | Idempotencia, registro de operaciones y reconciliación. |
| Clases `Memory` antiguas fallan tras actualizar | APIs legacy movidas o incompatibles con v1. | Usar checkpointer/store en proyectos nuevos; aislar y migrar dependencias legacy. |

## 13. Referencias oficiales y de ecosistema

### LangChain / LangGraph

- [LangChain: short-term memory](https://docs.langchain.com/oss/python/langchain/short-term-memory)
- [LangChain: long-term memory](https://docs.langchain.com/oss/python/langchain/long-term-memory)
- [LangChain: agents y checkpointer](https://docs.langchain.com/oss/python/langchain/agents)
- [LangChain: context engineering y resumen](https://docs.langchain.com/oss/python/langchain/context-engineering)
- [LangGraph: persistence](https://docs.langchain.com/oss/python/langgraph/persistence)
- [LangGraph: stores](https://docs.langchain.com/oss/python/langgraph/stores)
- [LangGraph: checkpointer integrations](https://docs.langchain.com/oss/python/langgraph/checkpointers)
- [Migración a LangChain v1](https://docs.langchain.com/oss/python/migrate/langchain-v1)
- [LangMem: guía conceptual](https://langchain-ai.github.io/langmem/conceptual_guide/)
- [LangMem: documentación](https://github.com/langchain-ai/langmem)

### Alternativas

- [Mem0: repositorio y SDK](https://github.com/mem0ai/mem0)
- [Mem0: conceptos de búsqueda y filtros](https://docs.mem0.ai/core-concepts/memory-operations/search)
- [Zep: SDK Python](https://github.com/getzep/zep-python)
- [LangGraph Redis](https://github.com/redis-developer/langgraph-redis)

## 14. Conclusión

Para LangChain actual, entender memoria como un único buffer oculta necesidades distintas. El patrón base separa el estado del hilo —checkpointer— de los recuerdos que deben sobrevivir entre hilos —Store—. Resumen/recorte administran el tamaño del contexto; RAG recupera conocimiento externo; y los sistemas de dominio conservan la verdad transaccional.

En un agente multiusuario, el aislamiento depende de la identidad y el namespace que construye la aplicación. Usar `thread_id` por conversación y `(tenant_id, user_id)` para memoria persistente es una base segura, siempre acompañado por autorización, retención, procedencia, idempotencia y mecanismos de corrección y borrado.
