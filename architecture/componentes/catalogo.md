# Catálogo de componentes de arquitectura

Este catálogo sirve para reconocer componentes, su función y cuándo considerarlos. Resume el concepto y enlaza a documentación especializada cuando existe. No implica que una solución deba incluir todos los componentes; seleccionar según requisitos y evitar duplicar funciones sin una razón explícita.

## 1. Entrada, borde y tráfico

| Componente | Descripción y casos de uso | Recomendación | Detalle relacionado |
|---|---|---|---|
| DNS | Traduce nombres de dominio a destinos de red. Se usa para publicar servicios y dirigir tráfico. | Diseñar nombres, zonas, registros, TTL y estrategia de cambio/failover. | — |
| CDN | Distribuye contenido desde ubicaciones cercanas al usuario; reduce latencia y carga de origen. Útil para contenido estático y ciertos contenidos dinámicos cacheables. | Definir explícitamente qué se cachea, por cuánto tiempo y cómo invalidarlo; proteger el origen. | [Cloudflare: servicios](../../cloud/Cloudflare/Servicios-Cloudflare.md) |
| WAF | Inspecciona tráfico web y bloquea solicitudes que coinciden con reglas de ataque. Se usa para proteger aplicaciones web y APIs expuestas. | Ajustar reglas con monitoreo para evitar falsos positivos; no reemplaza validación ni controles dentro de la aplicación. | [Documento WAF](../seguridad/WAF.md) |
| Balanceador de carga | Distribuye conexiones o solicitudes entre instancias y puede dejar fuera instancias no saludables. Útil para disponibilidad y escalado horizontal. | Elegir capa y algoritmo según protocolo y tráfico; configurar health checks, drenaje y terminación TLS de forma intencional. | [AWS Application Load Balancer](../../cloud/amazon/AWS_ALB.md) |
| Proxy inverso | Recibe solicitudes en nombre de servidores internos y las enruta; puede terminar TLS, comprimir o cachear. | Usarlo para ocultar orígenes y controlar el ingreso; documentar encabezados reenviados y límites de confianza. Puede balancear, pero no es sinónimo de balanceador. | — |
| API Gateway | Punto de entrada para APIs que aplica enrutamiento, autenticación, cuotas y administración de versiones. | Mantenerlo enfocado en políticas transversales; dejar reglas de negocio en los servicios responsables. | [API Gateway](API_Gateway.md) |

## 2. Aplicación y ejecución

| Componente | Descripción y casos de uso | Recomendación | Detalle relacionado |
|---|---|---|---|
| Servidor web | Atiende HTTP/HTTPS y sirve contenido estático o reenvía solicitudes a una aplicación. | Separar responsabilidades de contenido, proxy y lógica de aplicación cuando eso simplifique operación y escalado. | — |
| Servidor de aplicaciones | Ejecuta lógica de aplicación y expone interfaces a clientes u otros servicios. | Definir límites, configuración, escalado, health checks y gestión de sesiones. | [Microservicios](../estilos-y-patrones/microservicios/README.md) |
| Contenedor | Empaqueta aplicación y dependencias para ejecución consistente entre ambientes. | Diseñar imágenes reproducibles, pequeñas y sin secretos; definir límites de recursos y proceso de actualización. | [Contenedores en cloud](../../cloud/container.md) |
| Orquestador de contenedores | Programa contenedores, administra réplicas, red, configuración y actualizaciones. Útil para operar múltiples cargas contenedorizadas. | Adoptarlo cuando las necesidades operativas justifican su complejidad; definir políticas de salud, capacidad, seguridad y recuperación. | [Azure contenedores](../../cloud/azure/azure-contenedores.md), [AWS ECS](../../cloud/amazon/AWS_ECS.md) |
| Función serverless | Ejecuta código bajo demanda sin administrar directamente servidores. Adecuada para tareas event-driven, automatizaciones y cargas variables. | Considerar límites de duración, arranque, concurrencia, observabilidad y dependencia del proveedor. | [AWS Lambda](../../cloud/amazon/AWS-Lambda.md), [Azure Functions](../../cloud/azure/azure-functions.md), [Serverless](../../cloud/Serverless.md) |

## 3. Datos y almacenamiento

| Componente | Descripción y casos de uso | Recomendación | Detalle relacionado |
|---|---|---|---|
| Base de datos relacional | Persiste datos estructurados y relaciones, normalmente con transacciones y consultas SQL. | Elegir motor según consistencia, consultas, escala, operación y capacidades del equipo; definir respaldo, recuperación y migraciones. | [Base de datos](../../data_base/data_base.md), [modelado](../datos/db/design.model.db.md) |
| Base de datos NoSQL | Familia de almacenes documentales, clave-valor, columnares o grafos. Puede servir modelos de acceso y escalado específicos. | Empezar por patrones de acceso y garantías requeridas; no elegir solo por volumen o moda. | [Elasticsearch](../../data_base/elasticsearch/elasticsearch.md) |
| Almacenamiento de objetos | Guarda archivos y objetos no estructurados con metadatos; útil para documentos, imágenes, respaldos y artefactos. | Definir acceso, versionado, cifrado, retención, ciclo de vida y recuperación. | [AWS S3](../../cloud/amazon/AWS_S3.md), [Azure Storage](../../cloud/azure/storage.md) |
| Caché | Mantiene temporalmente datos calculados o consultados con frecuencia para reducir latencia y presión en sistemas de origen. | Definir expiración, invalidación, comportamiento ante caída y si los datos son regenerables; evitar tratar la caché como fuente de verdad sin diseño explícito. | — |
| Almacén analítico | Organiza datos para reportes y análisis, separado a menudo de las cargas transaccionales. | Diseñar cargas, frescura, calidad y gobierno de datos para evitar afectar sistemas operacionales. | [Data warehousing Oracle](../../data_base/oracle/data_warehousing.md) |

## 4. Comunicación e integración

| Componente | Descripción y casos de uso | Recomendación | Detalle relacionado |
|---|---|---|---|
| Servicio/API | Interfaz programática para consultar o modificar capacidades de una aplicación. | Definir contratos, autenticación, errores, versionado, límites y compatibilidad. | [APIs](../comunicacion-e-integracion/api.md) |
| Broker o cola de mensajes | Intermedia mensajes entre productores y consumidores; desacopla tiempos y permite procesamiento asíncrono. | Documentar entrega, orden, reintentos, duplicados, expiración, DLQ y evolución de esquemas. | [Azure Event Grid](../comunicacion-e-integracion/specification.md), [AWS SQS](../../cloud/amazon/AWS_SQS.md) |
| Plataforma de eventos | Distribuye eventos de negocio a suscriptores o flujos. Útil para reaccionar a cambios y conectar capacidades con menor acoplamiento temporal. | Publicar hechos de negocio estables; definir propiedad, retención, reintentos e idempotencia. | [Arquitectura orientada a eventos](../estilos-y-patrones/Event-Driven.md) |
| Servicio de identidad (IdP) | Autentica usuarios o servicios y emite credenciales o tokens para otros sistemas. | Separar autenticación de autorización; definir federación, MFA, ciclo de vida y recuperación de cuentas. | [Autenticación](../seguridad/seguridad-aplicativa/authentication.md), [Azure AD](../../cloud/azure/Azure_AD.md) |

## 5. Seguridad y configuración

| Componente | Descripción y casos de uso | Recomendación | Detalle relacionado |
|---|---|---|---|
| Gestor de secretos | Almacena y entrega claves, contraseñas, certificados o tokens a aplicaciones autorizadas. | Aplicar privilegio mínimo, rotación, auditoría y acceso de carga de trabajo; evitar secretos en código o repositorios. | [Gestión de secretos](../seguridad/seguridad-aplicativa/gestor.secretos.md), [AWS](../../cloud/amazon/key-vault.md), [Azure](../../cloud/azure/key-vault.md) |
| KMS o servicio criptográfico | Administra claves y operaciones criptográficas para cifrar datos y controlar su uso. | Definir propiedad, ciclo de vida, separación de funciones, rotación y recuperación de claves. | [Gestión de secretos](../seguridad/seguridad-aplicativa/gestor.secretos.md) |
| Firewall de red | Controla conexiones permitidas entre redes, subredes o sistemas según reglas de red. | Aplicar denegación por defecto y reglas mínimas; revisar flujos requeridos y registrar cambios. | [Seguridad de microservicios](../estilos-y-patrones/microservicios/security.md) |
| Certificado y autoridad de certificación | Vincula una identidad a una clave pública y permite establecer canales cifrados o verificar identidad. | Automatizar emisión y renovación cuando sea posible; vigilar expiración y proteger claves privadas. | [Cookies y seguridad web](../seguridad/seguridad-aplicativa/cookies.md) |
| IPS (sistema de prevención de intrusiones) | Analiza actividad de red o de host y puede bloquear actividad identificada como intrusiva. Se usa para proteger segmentos críticos, tráfico de entrada/salida o servidores. | Definir cobertura y modo de respuesta; medir falsos positivos antes de bloquear y diseñar disponibilidad, rendimiento y reversión. | [Documento IPS](../seguridad/IPS.md) |

## 6. Operación y entrega

| Componente | Descripción y casos de uso | Recomendación | Detalle relacionado |
|---|---|---|---|
| Plataforma de observabilidad | Recibe y permite consultar métricas, logs y trazas para entender salud y comportamiento. | Instrumentar señales con contexto correlacionable, controlar datos sensibles y definir alertas accionables. | [Operación y confiabilidad](../operacion/README.md), [Grafana](../../analytics/grafana.md) |
| Health check | Endpoint o mecanismo que informa si una instancia está viva o lista para recibir tráfico. | Diferenciar liveness y readiness; no declarar lista una instancia hasta completar dependencias críticas. | [Health checks](../comunicacion-e-integracion/specification/health.md) |
| Plataforma CI/CD | Automatiza construcción, validaciones y despliegues de software. | Proteger credenciales, hacer artefactos reproducibles, incluir aprobaciones necesarias y facilitar rollback. | [DevSecOps](../../Operation/DevSecOps.md) |
| Balanceador de base de datos o pool de conexiones | Reutiliza y distribuye conexiones de aplicaciones hacia una base de datos. | Dimensionar límites y tiempos de espera en conjunto con la base y la aplicación; vigilar saturación y conexiones largas. | [Pool Oracle](../../data_base/oracle/pool.md) |

## Recorrido típico de una solicitud web

Un flujo posible es **DNS → CDN → WAF → balanceador/proxy inverso → API Gateway o servidor web → aplicación → caché/base de datos**. La secuencia depende de la solución: algunos productos combinan funciones, y componentes como broker, gestor de secretos y observabilidad participan en otros flujos.

## Documentación especializada

La ficha del catálogo explica el papel del componente. Para instalación, configuración, decisiones de proveedor o detalles profundos, seguir el enlace especializado o crear un documento en la categoría temática correspondiente y enlazarlo aquí.
