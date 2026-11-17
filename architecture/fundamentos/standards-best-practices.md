# Estándares y buenas prácticas

Este documento funciona como índice de guías temáticas. Las recomendaciones deben elegirse según el dominio, los atributos de calidad prioritarios, las restricciones y el costo de operación; no todas aplican a todos los sistemas.

## Diseño de software

- [Principios y patrones de diseño](../estilos-y-patrones/principios-y-patrones.md): SOLID, Mediator y Unit of Work.
- [CQRS](../estilos-y-patrones/cqrs.md): separación de operaciones de lectura y escritura, variantes, criterios de uso y límites.
- [Resiliencia](resiliencia.md): reintentos y circuit breaker frente a fallas transitorias.
- [Atributos de calidad](atributos-de-calidad.md): escenarios para guiar decisiones de diseño y operación.

## Estándares técnicos

- [Estándares de bases de datos](../datos/db/estandares.md): modelado relacional y convenciones.
- [Estándares de API](../comunicacion-e-integracion/estandares-api.md): guías REST, manejo de errores y formatos de especificación.
- [Estándares de información y decisiones](estandares-informacion.md): códigos de país y DMN.
- [Arquitectura de interfaz de usuario](ui/architecture_ui.md): diseño de interfaz, accesibilidad y sistemas visuales.

## Prácticas de ingeniería

- [Calidad de código](code-quality.md): herramientas, análisis y cobertura.
- [Prácticas de desarrollo web](practicas-desarrollo-web.md): listas de revisión y código limpio en TypeScript.
- [Documentación y decisiones de arquitectura](../decisiones-y-documentacion/README.md).
- [Operación](../operacion/README.md): despliegue y metodología Twelve-Factor.

## Criterios de mantenimiento

- Mantener una guía por tema y usar este archivo como punto de entrada.
- Distinguir una norma obligatoria de una recomendación o un recurso de referencia.
- Indicar alcance, contexto, fecha o versión cuando una recomendación dependa de una tecnología.
- Revisar los enlaces externos periódicamente; registrar los que hayan cambiado o desaparecido.
