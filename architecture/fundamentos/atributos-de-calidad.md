# Atributos de calidad

Los **atributos de calidad** describen propiedades del sistema que condicionan cómo se comporta y qué tan bien satisface necesidades de usuarios, negocio y operación. Complementan los requisitos funcionales: estos indican qué hace el sistema; los atributos de calidad describen con qué nivel de servicio, bajo qué condiciones y con qué restricciones lo hace.

No existe una lista universal que todos los sistemas deban cumplir por igual. La arquitectura debe priorizar los atributos relevantes para el contexto, resolver sus tensiones y convertirlos en escenarios verificables. Por ejemplo, «el sistema debe ser rápido» no se puede evaluar con precisión hasta definir operación, carga, umbral y condiciones.

## Atributos frecuentes

| Atributo | Concepto y uso | Ejemplo de requisito verificable |
|---|---|---|
| **Rendimiento** | Tiempo y recursos que requiere el sistema para procesar una carga. Se considera en operaciones sensibles a la latencia, procesamiento por lotes y uso eficiente de recursos. | El 95 % de las consultas de búsqueda responde en menos de 300 ms con 500 solicitudes concurrentes. |
| **Disponibilidad** | Proporción de tiempo en que una capacidad está operativa y accesible. Es crítica cuando una interrupción afecta ventas, operaciones o compromisos de servicio. | La API de pagos alcanza 99,9 % de disponibilidad mensual, excluyendo mantenimientos acordados. |
| **Confiabilidad** | Capacidad de prestar el servicio esperado de manera correcta y consistente durante un período. Considera frecuencia de fallos y resultados incorrectos. | Menos de 1 de cada 10 000 operaciones confirmadas produce un resultado inconsistente. |
| **Resiliencia y recuperabilidad** | Capacidad para mantener o recuperar funciones ante fallos, degradación o interrupciones. Se aplica a dependencias, infraestructura y datos. | Ante la caída del proveedor de correo, las compras siguen completándose y las notificaciones pendientes se reintentan al recuperarse. |
| **Escalabilidad** | Capacidad de sostener el crecimiento de usuarios, datos o solicitudes mediante más recursos o instancias. | Al duplicar instancias de lectura, el sistema sostiene el doble de consultas con latencia dentro del umbral acordado. |
| **Seguridad** | Protección de datos y operaciones frente a acceso, uso, modificación o divulgación no autorizados. Incluye autenticación, autorización, auditoría y gestión de riesgos. | Solo usuarios con el rol autorizado pueden consultar expedientes; los accesos se registran para auditoría. |
| **Privacidad** | Tratamiento apropiado de datos personales durante su recolección, uso, almacenamiento y eliminación. | Los datos personales se conservan solo durante el período definido y se excluyen de los registros operativos. |
| **Mantenibilidad** | Facilidad para comprender, corregir, probar y operar el sistema. Reduce el costo y el riesgo de cambios frecuentes. | Un cambio en reglas de cálculo puede desplegarse sin modificar módulos de autenticación y cuenta con pruebas automatizadas del dominio. |
| **Modificabilidad y extensibilidad** | Facilidad para incorporar cambios o capacidades nuevas con impacto acotado en componentes existentes. | Agregar un nuevo proveedor de pago no requiere cambiar el flujo del dominio ni los adaptadores de proveedores existentes. |
| **Observabilidad** | Capacidad de comprender el estado y comportamiento interno a partir de telemetría, para detectar y diagnosticar problemas. | Una solicitud fallida puede seguirse entre servicios mediante una traza y registros correlacionados. Ver [Observabilidad](observabilidad.md). |
| **Operabilidad** | Facilidad con la que los equipos pueden desplegar, configurar, supervisar, diagnosticar y recuperar el sistema. | Una versión puede desplegarse y revertirse con un procedimiento automatizado y observable. |
| **Interoperabilidad** | Capacidad de intercambiar datos y trabajar con otros sistemas mediante contratos y protocolos compatibles. | El sistema expone una API documentada y versionada que permite integrar el LMS con el sistema académico. |
| **Compatibilidad** | Capacidad de coexistir o funcionar correctamente con versiones, plataformas y componentes definidos. | La nueva versión del servicio acepta los mensajes emitidos por la versión anterior durante la ventana de migración. |
| **Portabilidad** | Facilidad para ejecutar o migrar el sistema entre entornos o plataformas con cambios acotados. | El servicio puede ejecutarse en los entornos de desarrollo y producción mediante configuración externa, sin cambios de código. |
| **Usabilidad y accesibilidad** | Facilidad y efectividad con que las personas completan tareas, incluyendo personas con distintas capacidades y tecnologías de asistencia. | Una persona puede completar el registro con teclado y lector de pantalla, y la tarea se realiza en menos de tres minutos en una prueba definida. |
| **Consistencia e integridad de datos** | Corrección, validez y coherencia de los datos durante operaciones, concurrencia y fallos. | No se confirma una reserva si la cantidad disponible resulta negativa, incluso con solicitudes concurrentes. |
| **Desplegabilidad** | Facilidad y seguridad para construir, probar, liberar y revertir cambios. | Una versión se despliega gradualmente y puede revertirse sin pérdida de transacciones confirmadas. |
| **Costo y eficiencia** | Recursos financieros y técnicos necesarios para construir y operar la solución. Permite evaluar opciones dentro de límites presupuestarios. | El costo mensual por 1 000 operaciones se mantiene bajo el presupuesto a la carga prevista. |

Los nombres se solapan según el contexto y los marcos de arquitectura utilizados. Por ejemplo, la recuperabilidad puede considerarse parte de resiliencia u operabilidad; la modificabilidad puede tratarse dentro de mantenibilidad. Lo importante es definir el comportamiento esperado y cómo se comprobará.

## Escenarios de calidad

Para que un atributo guíe decisiones de arquitectura, redactarlo como un escenario con:

1. **Estímulo:** evento que pone a prueba el sistema (por ejemplo, un pico de solicitudes o la caída de un proveedor).
2. **Origen y contexto:** quién o qué genera el estímulo y en qué condiciones ocurre.
3. **Respuesta esperada:** comportamiento que debe observarse.
4. **Medida de respuesta:** umbral que permite aceptar o rechazar el resultado.

Ejemplo: «Durante el cierre de matrículas, cuando llegan 800 solicitudes por segundo, el servicio de inscripción procesa el 95 % en menos de 500 ms y mantiene una tasa de error inferior a 0,1 %».

## Técnicas y mecanismos

Las técnicas son decisiones de diseño, implementación y operación que ayudan a satisfacer atributos. Una técnica puede mejorar un atributo y afectar otro: por ejemplo, la replicación puede mejorar disponibilidad, pero añadir complejidad de consistencia y costos. Validar cada mecanismo frente a escenarios y cargas representativas.

| Atributo | Técnicas y mecanismos habituales | Documentación relacionada en este repositorio |
|---|---|---|
| Rendimiento | Caché, índices, consultas eficientes, procesamiento asíncrono, pruebas de carga, perfilado y límites de recursos. | [Observabilidad](observabilidad.md) trata métricas y perfiles; revisar también [arquitectura de datos](../datos/README.md). |
| Disponibilidad | Redundancia, balanceo, réplicas, eliminación de puntos únicos de falla, despliegues graduales y comprobaciones de salud. | [Salud de servicios](../comunicacion-e-integracion/specification/health.md), [Operación](../operacion/README.md). |
| Resiliencia y recuperabilidad | Timeouts, reintentos con espera incremental, circuit breaker, aislamiento, degradación controlada, respaldos y pruebas de recuperación. | [Microservicios: resiliencia](../estilos-y-patrones/microservicios/microservice.md), [Operación](../operacion/README.md). |
| Escalabilidad | Escalado horizontal o vertical, servicios sin estado cuando sea viable, particionamiento, colas y pruebas de carga. | [Microservicios](../estilos-y-patrones/microservicios/README.md), [infraestructura](../infraestructura/README.md). |
| Seguridad y privacidad | Mínimo privilegio, autenticación y autorización, cifrado, gestión de secretos, auditoría, minimización y retención limitada de datos. | [Seguridad](../seguridad/README.md), [Autenticación](../seguridad/seguridad-aplicativa/authentication.md), [Autorización](../seguridad/seguridad-aplicativa/authorization.md), [Gestión de secretos](../seguridad/seguridad-aplicativa/gestor.secretos.md). |
| Mantenibilidad y modificabilidad | Alta cohesión, bajo acoplamiento, contratos explícitos, modularidad, automatización de compilación y pruebas, documentación y ADR. | [Calidad de código](code-quality.md), [documentación de decisiones](../decisiones-y-documentacion/README.md). |
| Observabilidad y operabilidad | Métricas, registros estructurados, trazas correlacionadas, alertas accionables, tableros y procedimientos operativos. | [Observabilidad](observabilidad.md), [Operación](../operacion/README.md). |
| Interoperabilidad y compatibilidad | APIs y eventos con contratos versionados, formatos compartidos, compatibilidad hacia atrás y pruebas de contrato. | [Comunicación e integración](../comunicacion-e-integracion/README.md), [API](../comunicacion-e-integracion/api.md), [formatos](../comunicacion-e-integracion/formatos.md). |
| Consistencia e integridad | Transacciones en el límite de propiedad de los datos, restricciones, idempotencia, control de concurrencia, reconciliación y, en procesos distribuidos, sagas. | [Arquitectura de datos](../datos/README.md), [microservicios](../estilos-y-patrones/microservicios/microservice.md), [flujos de trabajo](../comunicacion-e-integracion/workflows.md). |
| Usabilidad y accesibilidad | Investigación con usuarios, diseño consistente, navegación accesible y pruebas de tareas con tecnologías de asistencia. | [Arquitectura de interfaz de usuario](ui/architecture_ui.md), [Usabilidad](../../usabilidad/README.md). |
| Desplegabilidad | Integración y despliegue continuos, infraestructura como código, migraciones compatibles, canary o blue/green y reversión. | [Migraciones](../migraciones/README.md), [Operación](../operacion/README.md). |
| Costo y eficiencia | Presupuestos, medición por carga o transacción, dimensionamiento, escalado automático y revisión de consumo. | [Infraestructura](../infraestructura/README.md), [Operación](../operacion/README.md). |

Las referencias indican material relacionado, no necesariamente una guía exhaustiva de cada técnica. Si una técnica aún no está documentada, se puede añadir luego en la categoría correspondiente y enlazarla desde esta tabla.

## Priorización

1. Identificar objetivos de negocio, usuarios, riesgos, obligaciones y condiciones operativas.
2. Elegir los atributos que cambian las decisiones de arquitectura; evitar tratar todos como requisitos máximos.
3. Ordenar los atributos por importancia y registrar las tensiones entre ellos.
4. Convertir los atributos prioritarios en escenarios medibles y asignarles una forma de verificación.
5. Seleccionar técnicas que respondan a esos escenarios y validar con prototipos, análisis o pruebas representativas.
6. Revisar los objetivos durante el diseño, los cambios y la operación; la evidencia en producción puede exigir ajustar los escenarios.

## Referencias internas

- [Guía de arquitectura](../architecture.md)
- [Fundamentos](README.md)
- [Observabilidad](observabilidad.md)

