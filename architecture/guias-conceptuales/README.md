# Guías conceptuales

Esta carpeta reúne guías que explican cómo implementar diseños, patrones, mecanismos y otras soluciones de arquitectura sin depender de un lenguaje de programación, framework, plataforma o producto específico.

## Alcance

Una guía conceptual debe ser agnóstica a la tecnología, pero concreta en su implementación. Debe permitir que una persona entienda qué componentes necesita, qué responsabilidad tiene cada uno, cómo colaboran y en qué orden realizar el flujo. No basta con definir el concepto o enumerar sus ventajas.

Por ejemplo, una guía sobre procesamiento asíncrono de eventos puede describir publicadores, canales, suscripciones, despachadores y manejadores; cómo se registra una suscripción, cómo se enruta y entrega un evento, y cómo se gestiona el resultado del procesamiento. No debe asumir que esos roles corresponden a clases, servicios o productos con nombres específicos.

## Contenido esperado

Según el tema, cada guía debería cubrir:

- **Propósito:** qué problema resuelve y cuándo considerar la solución.
- **Conceptos y participantes:** componentes, responsabilidades y relaciones.
- **Diseño:** estructura, contratos, datos y límites relevantes.
- **Implementación conceptual:** pasos y flujo concretos para construir la solución, expresados sin código ni APIs de una tecnología particular.
- **Fallos y compensaciones:** escenarios de error, límites, costos y decisiones que deben tomarse.
- **Verificación:** cómo comprobar que la solución cumple su propósito.
- **Implementaciones tecnológicas:** enlaces a guías específicas por lenguaje, framework o producto, si existen.
- **Referencias:** fuentes utilizadas para desarrollar la guía.

No es necesario usar todos los apartados en todos los documentos; ajustar la estructura al tema sin omitir los detalles necesarios para implementarlo.

## Qué corresponde a otras áreas

- Las instrucciones con código, comandos, APIs o configuración de un lenguaje, framework, plataforma o producto pertenecen a su área tecnológica. La guía conceptual puede enlazarlas.
- La descripción general de un estilo arquitectónico pertenece a [Estilos y patrones](../estilos-y-patrones/README.md); esta carpeta puede enlazarla si además hace falta una guía agnóstica que detalle cómo llevar un diseño a la práctica.
- Evitar duplicar explicaciones existentes. Cuando un documento de otra categoría ya cubra el tema, enlazarlo o proponer su traslado y actualizar las referencias relacionadas.

## Organización

Organizar el contenido por tema y agregar nuevas categorías cuando la colección lo requiera. No limitar la estructura a una lista fija de tipos: pueden incorporarse guías de patrones, mecanismos, diseños, estrategias, procesos u otros conceptos.

Cada guía nueva debe añadirse al índice de documentos de este README con una descripción breve de su alcance.

## Documentos

- [Implementar un bus de eventos asíncrono](bus-de-eventos-asincrono.md): componentes, contratos, publicación, transporte, despacho, serialización, errores e implementación conceptual de un `IEventBus`.
- [Implementar una Saga para coordinar transacciones distribuidas](saga.md): coreografía y orquestación, contratos, estados, compensaciones, confiabilidad y recuperación de procesos entre servicios.
- [Implementar Transactional Outbox en una solución con varios módulos](outbox.md): un Outbox por módulo, Inbox, publicador, módulos que publican y consumen, ciclos, orden, fallos y verificación.
