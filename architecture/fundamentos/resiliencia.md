# Resiliencia

La resiliencia es la capacidad de un sistema para mantener o recuperar sus funciones ante fallos y degradaciones. Debe diseñarse en la aplicación, sus dependencias, los datos, la infraestructura y la operación. Un mecanismo aislado no vuelve resiliente a un sistema.

## Retry

Los reintentos pueden resolver fallos transitorios al comunicarse con servicios o recursos de red. Deben tener límites, demoras crecientes y, cuando corresponda, aleatorización. Reintentar sin límite puede amplificar una caída; solo deben repetirse operaciones seguras o protegidas contra duplicación, por ejemplo mediante idempotencia.

Antes de implementar lógica propia, considerar bibliotecas adecuadas al ecosistema, como Polly para .NET o Resilience4j para Java. Revisar la documentación vigente de la versión usada antes de configurar una política.

## Circuit breaker

El circuit breaker deja de enviar temporalmente solicitudes a una dependencia que falla de forma sostenida, permite una recuperación controlada y luego prueba si el servicio volvió. Complementa los timeouts y límites de concurrencia; no los reemplaza. Definir qué respuesta degradada ofrecer y cómo observar las transiciones.

## Decisiones y referencias

- Usar timeouts explícitos y presupuestos de tiempo coherentes entre llamadas.
- Limitar reintentos y evitar multiplicarlos en varias capas.
- Registrar métricas y trazas para distinguir fallas propias de fallas de dependencias.
- Probar tanto recuperación como degradación.
- [Atributos de calidad: resiliencia y recuperabilidad](atributos-de-calidad.md)
- [Microservicios: resiliencia](../estilos-y-patrones/microservicios/microservice.md)
