# Observabilidad

La **observabilidad** es un **atributo de calidad** que describe qué tan bien puede entenderse el estado interno y el comportamiento de un sistema a partir de la información que produce mientras se ejecuta. Un sistema es observable cuando sus señales permiten investigar qué está fallando, a quién afecta, desde cuándo y qué componentes participan, incluso ante situaciones que no se anticiparon al diseñar alertas.

Como atributo de calidad, influye en la operación y el mantenimiento: facilita detectar, diagnosticar y resolver incidentes, y reduce el tiempo necesario para comprender el impacto de un cambio o una falla. Debe considerarse desde el diseño y expresarse en requisitos verificables, por ejemplo: poder seguir una solicitud entre servicios, medir la latencia de operaciones críticas y acceder a registros correlacionados para investigar errores.

La observabilidad no es solo instalar una herramienta ni recopilar grandes cantidades de datos. Requiere instrumentar el sistema, recopilar señales útiles, relacionarlas entre sí y dar a los equipos contexto para diagnosticar y actuar. Las herramientas y prácticas de observabilidad ayudan a alcanzar este atributo; por sí solas no garantizan que el sistema sea observable.

## Observabilidad y monitoreo

El monitoreo comprueba condiciones conocidas —por ejemplo, uso elevado de CPU o una tasa de errores por encima de un umbral— y alerta cuando se cumplen. La observabilidad también ayuda a investigar situaciones desconocidas, explorando los datos de ejecución para encontrar causas y relaciones. En producción se necesitan ambos: el monitoreo detecta síntomas conocidos y la observabilidad facilita el diagnóstico.

## Señales principales

- **Métricas:** mediciones numéricas agregadas a lo largo del tiempo, como latencia, solicitudes por segundo, errores y uso de recursos. Sirven para observar tendencias, definir umbrales y alertar.
- **Registros (logs):** eventos discretos con contexto sobre lo ocurrido. Los registros estructurados, con campos consistentes como nivel, servicio, hora e identificador de correlación, son más fáciles de consultar.
- **Trazas distribuidas:** muestran el recorrido de una solicitud a través de servicios y dependencias. Una traza contiene *spans* con duración, operación y contexto, y ayuda a localizar demoras o fallos en sistemas distribuidos.
- **Perfiles (profiling):** describen el uso de CPU, memoria u otros recursos por partes del programa. Complementan las otras señales al investigar problemas de rendimiento a nivel de código.

Las señales son más útiles cuando se pueden correlacionar. Por ejemplo, una alerta de latencia puede llevar a una traza y, desde ella, a los registros asociados a la misma solicitud.

## Conceptos relacionados

- **Instrumentación:** código o agentes que capturan y propagan la información de telemetría.
- **Contexto de correlación:** identificadores como `trace_id` y `span_id` que conectan eventos y operaciones entre componentes.
- **Telemetría:** datos emitidos por el sistema —métricas, registros, trazas y perfiles— para describir su ejecución.
- **SLO y error budget:** un objetivo de nivel de servicio define la confiabilidad que se busca desde la perspectiva del usuario; el presupuesto de error ayuda a equilibrar cambios y estabilidad.

## Caso de uso: solicitud que atraviesa microservicios

Una compra tarda demasiado y el indicador de latencia activa una alerta. El equipo abre una traza y observa que la petición pasó por API Gateway, Pedidos, Inventario y Pagos. La traza muestra que Inventario demoró la mayor parte del tiempo; los registros con el mismo `trace_id` aportan el detalle de la operación. Las métricas permiten verificar si el problema afecta a todas las solicitudes o solo a una parte.

Con estas señales relacionadas, el equipo puede delimitar el componente afectado, comprobar el impacto y orientar la investigación sin depender de revisar cada servicio por separado.

## Recomendaciones

1. Instrumentar primero los recorridos críticos para el usuario y las dependencias que cruzan límites de servicio.
2. Propagar el contexto de trazas en llamadas síncronas y mensajes asíncronos, y usar identificadores de correlación en los registros.
3. Medir latencia, volumen, errores y saturación; definir alertas que correspondan a impacto observable para el usuario.
4. Usar nombres, atributos y formatos consistentes entre servicios para facilitar consultas y tableros comunes.
5. Limitar atributos de alta cardinalidad —por ejemplo, identificadores únicos como `user_id` en métricas— para controlar costos y evitar series excesivas.
6. Aplicar muestreo de trazas según el volumen y conservar suficiente información de errores y operaciones relevantes.
7. Establecer retención, acceso y controles para datos sensibles. No incluir contraseñas, tokens ni información personal innecesaria en telemetría.
8. Revisar periódicamente que las alertas sean accionables y que los datos retenidos ayuden a resolver incidentes reales.

## Limitaciones y riesgos

- La instrumentación incompleta o inconsistente deja tramos sin contexto y dificulta seguir una solicitud entre servicios.
- El muestreo puede descartar trazas útiles; capturar todo puede ser costoso en sistemas de alto volumen.
- Etiquetas de cardinalidad elevada aumentan el costo y pueden degradar el almacenamiento y las consultas de métricas.
- La telemetría añade consumo de CPU, memoria, red y almacenamiento, además de trabajo operativo.
- Los relojes desincronizados, la pérdida de contexto y las colas de exportación saturadas pueden producir datos confusos o incompletos.
- Los logs y atributos pueden exponer información confidencial si no se diseñan y protegen correctamente.
- Las herramientas muestran indicios, no determinan por sí solas la causa raíz ni sustituyen el conocimiento del sistema.

## OpenTelemetry

[OpenTelemetry](https://opentelemetry.io/) es un proyecto abierto que ofrece APIs, SDKs y convenciones para generar, recopilar y exportar telemetría. Puede ayudar a instrumentar aplicaciones con una base común e integrar distintos backends. No es en sí mismo un backend de observabilidad: normalmente se combina con uno o más sistemas para almacenar, consultar y visualizar los datos.

## Referencias internas

- [Microservicios](../estilos-y-patrones/microservicios/microservice.md)
- [Operación](../operacion/README.md)

