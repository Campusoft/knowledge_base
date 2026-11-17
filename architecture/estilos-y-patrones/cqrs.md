# Command Query Responsibility Segregation (CQRS)

CQRS es un patrón arquitectónico que separa las responsabilidades de cambiar el estado del sistema y consultar información. Su nombre significa *Command Query Responsibility Segregation*. Se inspira en **Command Query Separation (CQS)**, que aplica una distinción parecida a las operaciones de un objeto; CQRS lleva esa separación al diseño de los casos de uso y, según la variante, a los modelos de datos.

## Conceptos

- **Comando:** expresa una intención de cambiar el estado, por ejemplo `CrearPedido` o `CancelarPedido`. Puede validarse y rechazarse. El manejador coordina el caso de uso; las reglas e invariantes del negocio pertenecen al modelo de dominio.
- **Consulta:** solicita información y no debe cambiar el estado observado por la aplicación. Puede devolver un DTO o una vista preparada para una pantalla o reporte.
- **Modelo de escritura:** protege las reglas de negocio y aplica cambios.
- **Modelo de lectura:** presenta los datos de una forma adecuada para las consultas. Puede ser una proyección del modelo de escritura.

Separar comandos y consultas en clases o manejadores distintos es una separación lógica útil, pero no obliga por sí sola a separar bases de datos. CQRS tampoco exige un bus de mensajes, comunicación asíncrona ni una biblioteca como MediatR.

## Flujo ilustrativo

En una aplicación de pedidos, `CrearPedido` llega al manejador de comandos. Este coordina la validación y el dominio, persiste el cambio y puede publicar un evento. `ConsultarPedido` ejecuta una consulta que devuelve el DTO requerido por la pantalla. En la forma más sencilla ambas rutas leen y escriben en la misma base de datos; en una forma más avanzada, la escritura actualiza una proyección de lectura independiente.

```text
CrearPedido ──> manejador ──> dominio / modelo de escritura ──> persistencia
ConsultarPedido ──> manejador de consulta ──> consulta / modelo de lectura
```

El diagrama representa responsabilidades; no prescribe componentes desplegados por separado.

## Niveles de aplicación

Una progresión habitual va desde separar el código hasta separar el almacenamiento:

1. **Separación interna:** comandos y consultas tienen rutas, funciones o manejadores distintos, con el mismo modelo y almacenamiento.
2. **Modelos distintos:** la escritura usa el modelo de dominio; las consultas usan DTO, proyecciones o SQL optimizado.
3. **Persistencia separada:** las proyecciones de lectura se almacenan aparte y se actualizan a partir de cambios del lado de escritura.

Los niveles superiores aumentan la libertad de optimización, pero también el costo de sincronización, monitoreo y recuperación. La separación física debe responder a una necesidad medible, no es un requisito para afirmar que se separaron responsabilidades.

## Event Sourcing y eventos

**Event Sourcing es un patrón distinto y opcional.** En Event Sourcing se conservan los eventos de negocio como registro persistente de los cambios, en lugar de guardar únicamente el último estado. CQRS puede usarse sin Event Sourcing, y Event Sourcing suele combinarse con modelos de lectura derivados de eventos.

Un comando no es un evento: el comando expresa una solicitud que puede ser rechazada (`CancelarPedido`); el evento describe un hecho ya ocurrido (`PedidoCancelado`). Los eventos pueden alimentar proyecciones o integraciones, pero CQRS simple no requiere eventos.

## Respuesta de comandos

Una regla estricta derivada de CQS recomienda que los comandos no devuelvan datos de consulta. En aplicaciones reales, la interfaz puede necesitar una confirmación, errores de validación o el identificador generado al crear un recurso. Devolver ese resultado de ejecución no convierte automáticamente al comando en consulta: la distinción principal es si la operación cambia el estado. Otra opción es generar el identificador antes de enviar el comando y luego consultar el recurso.

Conviene evitar devolver una copia amplia de la entidad como si el comando fuera también una consulta. Define explícitamente qué resultado de ejecución necesita el cliente y cómo obtiene la vista actualizada; si la lectura es eventual, contempla que la proyección todavía no refleje el cambio.

## Cuándo considerar CQRS

Puede ser útil cuando:

- las reglas de escritura son complejas y conviene aislar sus invariantes;
- las consultas necesitan formas de datos muy distintas de las usadas para modificar el dominio;
- lectura y escritura tienen cargas, seguridad o necesidades de escalado diferentes;
- se necesita construir varias vistas o proyecciones a partir de cambios del dominio.

Para un CRUD sencillo, separar manejadores puede aportar claridad, pero introducir proyecciones, mensajería y almacenes separados puede ser complejidad sin beneficio suficiente. Evalúa el costo de consistencia eventual, duplicación de datos, reintentos, idempotencia, reconstrucción de proyecciones, observabilidad y operación.

## Fuentes consultadas

Los resúmenes siguientes recogen las ideas relevantes para esta guía con palabras propias. Se indica el estado de acceso para distinguir el contenido revisado directamente de las referencias que solo pudieron verificarse parcialmente.

### Fundamentos y alcance de CQRS

#### Microsoft Learn — CQRS pattern

**Título:** Command Query Responsibility Segregation (CQRS) pattern

**Contenido esencial:** recomienda separar modelos de lectura y escritura cuando el modelo CRUD único se vuelve difícil de optimizar para necesidades diferentes. Los comandos representan tareas de negocio y pueden rechazarse; las consultas no modifican datos y devuelven DTOs. La separación puede ayudar con claridad, rendimiento, escalabilidad y seguridad, aunque añade lógica propia y otros costos.

**Referencia:** [Microsoft Learn — Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)

**Estado:** Disponible; consultado para este resumen.

#### Ricardo Borillo — CQRS y los beneficios surgidos de la necesidad

**Título:** CQRS y los beneficios surgidos de la necesidad

**Contenido esencial:** la descripción de la charla presenta CQRS como una respuesta a aplicaciones con muchas lecturas: separar modelos y preparar datos de lectura puede mejorar el rendimiento; además, esa separación puede favorecer el diseño y la mantenibilidad. El propio título enfatiza que los beneficios aparecen al responder a necesidades concretas.

**Referencia:** [Charla de Ricardo Borillo en Agile Spain](https://www.youtube.com/watch?v=8ZfqPqyyf7M&t=793s)

**Estado:** Disponible; resumen basado en la descripción pública del video, no en una transcripción completa.

#### IBM — Event-driven CQRS pattern

**Título:** Event-driven CQRS pattern

**Contenido esencial:** las notas históricas del repositorio atribuían a este recurso una progresión desde acceso convencional a datos hasta APIs, modelos y almacenes separados. Esa progresión sirve como esquema didáctico, pero no se puede validar contra la página original.

**Referencia:** [URL original de IBM](https://www.ibm.com/cloud/architecture/architectures/event-driven-cqrs-pattern/)

**Estado:** El contenido original ya no está disponible en esa URL; actualmente redirige a una página general de arquitectura de datos. No usarla como fuente del esquema de niveles.

### Implementación en la capa de aplicación

#### Microsoft Learn — Microservice application layer

**Título:** Implement the microservice application layer using the Web API

**Contenido esencial:** muestra comandos como entrada a transacciones y describe manejadores que coordinan repositorios, dominio y persistencia. El manejador puede vivir en la capa de aplicación; las reglas del negocio deben permanecer en el modelo de dominio. El ejemplo usa Mediator, pero la guía señala que es una opción de implementación, no un requisito de CQRS.

**Referencia:** [Microsoft Learn — .NET Microservices Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)

**Estado:** Disponible. La antigua dirección `docs.microsoft.com` redirige a `learn.microsoft.com`.

#### Mehmet Ozkaya — Ordering Microservices con CQRS y Event Sourcing

**Título:** CQRS and Event Sourcing in Event Driven Architecture of Ordering Microservices

**Contenido esencial:** tutorial paso a paso de una API de pedidos con ASP.NET Core, Clean Architecture, Entity Framework Core, SQL Server, MediatR, FluentValidation y AutoMapper. Separa comandos y consultas dentro de las capas de una aplicación; también incluye mensajería RabbitMQ para integrar servicios. Es un ejemplo de implementación concreta, no una definición normativa de CQRS.

**Referencia:** [Artículo de aspnetrun en Medium](https://medium.com/aspnetrun/cqrs-and-event-sourcing-in-event-driven-architecture-of-ordering-microservices-fb67dc44da7a)

**Estado:** Disponible. Publicado en 2020; las versiones y herramientas del tutorial pueden estar desactualizadas.

#### Matthew Jones — Real-World CQRS/ES with ASP.NET and Redis

**Título:** Part 2 — The Write Model

**Contenido esencial:** la serie describe un modelo de escritura con comandos, manejadores, agregados y eventos, y un modelo de lectura separado que se alimenta de eventos. Las notas originales del repositorio también resumen una convención útil: comandos expresan solicitudes en imperativo; eventos describen hechos ocurridos en pasado. Esta parte combina CQRS con Event Sourcing, por lo que esos elementos no son requisitos de CQRS por sí solo.

**Referencia:** [Artículo — Part 2: The Write Model](https://www.exceptionnotfound.net/real-world-cqrs-es-with-asp-net-and-redis-part-2-the-write-model/) · [Introducción de la serie](https://www.exceptionnotfound.net/real-world-cqrs-es-with-asp-net-and-redis-part-1-overview/)

**Estado:** La URL directa ya no muestra el cuerpo del artículo durante esta revisión. La página de la serie y su resultado de búsqueda permiten confirmar el tema y el esquema general, pero no verificar los detalles completos.

### CQS, respuestas y límites de segregación

#### Kevin Vogel — Deep Dive into CQRS

**Título:** Deep Dive into CQRS — A Great Microservices Pattern

**Contenido esencial:** relaciona CQRS con Command Query Separation (CQS). CQS separa métodos que cambian el estado de aquellos que consultan sin mutarlo. El ejemplo de una pila distingue `push` como comando, `top` como consulta y `pop` como operación que combina cambio y resultado. Esta explicación ayuda a entender el origen conceptual, aunque CQS a nivel de método y CQRS a nivel de arquitectura no son lo mismo.

**Referencia:** [Artículo en Level Up Coding](https://levelup.gitconnected.com/what-is-cqrs-8ddd74ca05bb)

**Estado:** Página localizable; contenido marcado como artículo para miembros. El resumen se limita a la parte accesible.

#### Oskar Dudycz — Can command return a value?

**Título:** Can command return a value?

**Contenido esencial:** sostiene que la separación se basa en el comportamiento (consultar sin mutar o cambiar el estado), no en una prohibición absoluta de devolver cualquier valor. Puede ser razonable devolver metadatos generados por el servidor, como el identificador o la versión resultante, y mapear el resultado a una respuesta HTTP. Conviene no devolver sin necesidad toda la representación de lectura, porque eso vuelve a acoplar la escritura con las consultas.

**Referencia:** [Event-Driven.io — CQRS guide](https://event-driven.io/en/can_command_return_a_value/)

**Estado:** Disponible; consultado para este resumen.

#### Kamil Grzybek — Modular Monolith with DDD

**Título:** Modular Monolith with DDD — procesamiento de solicitudes mediante CQRS

**Contenido esencial:** el repositorio muestra una interfaz de módulo con tres operaciones: comando con resultado, comando sin resultado y consulta. En su ejemplo, las consultas usan un modelo de lectura con SQL y vistas; los comandos aplican cambios mediante el modelo de dominio. El autor señala que devolver un identificador u otro resultado de ejecución puede ser práctico y no convierte por sí mismo al comando en consulta.

**Referencia:** [Repositorio Modular Monolith with DDD](https://github.com/kgrzybek/modular-monolith-with-ddd), secciones “Request processing” y “Module Requests Processing via CQRS”

**Estado:** Disponible; consultado para este resumen.

#### Vladik Khononov — Tackling Complexity in CQRS

**Título:** Tackling Complexity in CQRS

**Contenido esencial:** advierte que separar en exceso puede añadir complejidad accidental, por ejemplo comandos de solo confirmación que obligan a consultar repetidamente otro modelo para conocer el resultado. Recomienda considerar el objetivo de representar datos con modelos distintos y conservar una fuente de verdad para las escrituras. Es una perspectiva argumentada por el autor, no una regla universal del patrón.

**Referencia:** [Artículo de Vladik Khononov](https://vladikk.com/2017/03/20/tackling-complexity-in-cqrs/)

**Estado:** Disponible; consultado para este resumen.

### Lectura complementaria

#### Microsoft Archive — CQRS Journey

**Título:** Reference 02 — CQRS Introduction

**Contenido esencial:** explica que CQRS no exige separar físicamente los almacenes ni usar una tecnología de persistencia concreta. También describe cómo un modelo de lectura puede reaccionar a eventos para actualizar las vistas que atienden consultas.

**Referencia:** [Microsoft Archive — CQRS Journey](https://github.com/MicrosoftArchive/cqrs-journey/blob/master/docs/Reference_02_CQRSIntroduction.markdown)

**Estado:** Disponible en un repositorio archivado; usar como material conceptual e histórico.

## Nota sobre Change Data Capture

Change Data Capture (CDC) puede servir como mecanismo para propagar cambios y alimentar modelos de lectura, pero no define CQRS ni es un requisito del patrón. La referencia IBM que aparecía en las notas antiguas ya no permite verificar el tratamiento que daba a CDC.
