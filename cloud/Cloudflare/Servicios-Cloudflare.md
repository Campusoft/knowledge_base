# Servicios de Cloudflare: catálogo, características y equivalencias

> **Fecha de revisión:** 14 de agosto de 2026  
> **Alcance:** catálogo práctico de los principales servicios de Cloudflare. Cloudflare mantiene productos muy especializados y lanza servicios con frecuencia; por tanto, este documento no sustituye la [documentación de productos](https://developers.cloudflare.com/products/) ni la [página oficial de planes](https://www.cloudflare.com/es-la/plans/).

## 1. Cómo leer este documento

Cloudflare combina CDN, seguridad, conectividad, plataforma de desarrollo y servicios Zero Trust sobre una red global. Sus productos siguen varios modelos comerciales:

- **Incluido desde Free:** existe una versión funcional en el plan gratuito de una zona/dominio, normalmente con límites menores que Pro, Business o Enterprise.
- **Cuota gratuita + consumo:** incluye un nivel gratuito; al superar la cuota se requiere un plan de pago o se factura por uso.
- **Complemento de pago:** se contrata aparte del plan base, a veces con un precio inicial y consumo adicional.
- **Zero Trust Free/PAYG/Contract:** oferta gratuita para hasta 50 usuarios, modalidad por usuario y contrato empresarial.
- **Enterprise/contrato:** precio personalizado y, en algunos casos, disponibilidad condicionada a una contratación empresarial.

La columna AWS/Azure muestra el servicio **conceptualmente más cercano**. Una equivalencia no implica igualdad de arquitectura, cobertura, límites, SLA o precio. En varios casos hay que combinar dos o más servicios del hyperscaler para aproximar una sola capacidad de Cloudflare.

### Planes base de servicios de aplicación

| Plan | Orientación | Precio público de referencia* |
|---|---|---:|
| Free | Proyectos personales, pruebas y sitios no críticos | USD 0/mes |
| Pro | Sitios profesionales no críticos | USD 20/mes anual o USD 25 mensual |
| Business | Pequeñas empresas y aplicaciones con mayores requisitos | USD 200/mes anual o USD 250 mensual |
| Enterprise/Contract | Aplicaciones críticas, controles avanzados y soporte contractual | Cotización |

\* Los planes de zona se facturan por dominio. Los complementos y productos por consumo se cobran aparte. Ver [planes](https://www.cloudflare.com/plans/) y [funcionamiento de la facturación](https://developers.cloudflare.com/billing/understand/how-billing-works/).

## 2. Dominios, entrega y rendimiento de aplicaciones

| Servicio | Características principales | Casos de uso | Disponibilidad | Equivalente aproximado en AWS | Equivalente aproximado en Azure |
|---|---|---|---|---|---|
| **Authoritative DNS** | DNS autoritativo Anycast, DNSSEC, CNAME flattening, analítica y API | Alojar zonas DNS con alta disponibilidad y baja latencia | Incluido desde Free; más funciones en planes superiores | Amazon Route 53 | Azure DNS |
| **Cloudflare Registrar** | Registro y renovación de dominios a precio de costo, DNSSEC integrado y protección de cuenta | Centralizar dominio, DNS y seguridad | De pago por dominio; no es una suscripción Free | Route 53 Domains | App Service Domains; no hay equivalente general exacto |
| **CDN y Cache** | Caché global, reglas de caché, compresión, HTTP/3 y protección del origen | Acelerar sitios, APIs y contenido estático | CDN básico incluido desde Free; controles avanzados según plan | Amazon CloudFront | Azure Front Door / Azure CDN |
| **SSL/TLS** | Certificados Universal SSL, TLS en el borde, modos de cifrado y certificados administrados | Habilitar HTTPS y cifrar tráfico entre cliente, borde y origen | Universal SSL desde Free; Advanced Certificate Manager es complemento | AWS Certificate Manager + CloudFront | Certificados administrados de Front Door / App Service Certificates |
| **Pages** | Despliegue desde Git, previews, dominios personalizados, assets estáticos y Pages Functions | Sitios estáticos, JAMstack y aplicaciones full-stack ligeras | Free con límites; planes superiores amplían compilaciones y archivos | AWS Amplify Hosting | Azure Static Web Apps |
| **Load Balancing** | Balanceo global/local, health checks, failover, steering geográfico y por latencia | Alta disponibilidad multi-origen o multicloud | Complemento de pago desde aproximadamente USD 5/mes más uso | Elastic Load Balancing + Route 53 / Global Accelerator | Azure Front Door + Traffic Manager / Load Balancer |
| **Smart Shield + Argo Smart Routing** | Rutas optimizadas por la red de Cloudflare, Tiered Cache y reducción de carga al origen | Mejorar latencia y confiabilidad entre el borde y el origen | Complemento de pago desde aproximadamente USD 5/mes | Global Accelerator + CloudFront Origin Shield | Azure Front Door Premium |
| **Cache Reserve** | Almacenamiento persistente de objetos cacheables para elevar el hit ratio y reducir solicitudes al origen | Proteger orígenes costosos o con alto volumen de contenido de larga duración | Complemento facturado por uso | CloudFront Origin Shield + Amazon S3 | Front Door + Blob Storage |
| **Automatic Platform Optimization (APO)** | Optimización de WordPress, caché de HTML y entrega desde el borde | Acelerar WordPress sin rediseñar la aplicación | De pago en Free; incluido en Pro, Business y Enterprise | CloudFront + solución/plug-in para WordPress | Front Door + optimización de WordPress en App Service |
| **Waiting Room** | Cola virtual, control de concurrencia, sesiones y reglas para picos de demanda | Venta de entradas, lanzamientos, matrículas y comercio electrónico | Principalmente Business/Enterprise o complemento según oferta | AWS Virtual Waiting Room Solution | Arquitectura personalizada con Front Door, Functions y almacenamiento |

## 3. Seguridad de aplicaciones y APIs

| Servicio | Características principales | Casos de uso | Disponibilidad | Equivalente aproximado en AWS | Equivalente aproximado en Azure |
|---|---|---|---|---|---|
| **DDoS Protection** | Mitigación automática de ataques L3/L4/L7 sobre la red Anycast, sin medición del volumen mitigado para sitios | Mantener sitios y APIs disponibles durante ataques | Protección básica incluida desde Free; controles/SLA avanzados en Enterprise | AWS Shield Standard / Shield Advanced | Azure DDoS IP Protection / Network Protection |
| **Web Application Firewall (WAF)** | Reglas administradas y personalizadas, puntuación de ataque, listas y Ruleset Engine | Bloquear OWASP Top 10, exploits y tráfico malicioso | Free incluye ruleset administrado básico y reglas limitadas; más capacidad en Pro+ | AWS WAF | Azure Web Application Firewall |
| **Rate Limiting** | Límites por ruta, IP y otros atributos; acciones de bloqueo o challenge | Evitar abuso de APIs, scraping agresivo y ataques de fuerza bruta | Una regla básica en Free; mayores límites y funciones en planes superiores/add-on | AWS WAF rate-based rules | Azure WAF rate-limit rules / API Management policies |
| **Bot Management** | Clasificación de bots, señales de comportamiento, machine learning y políticas | Frenar scraping, fraude, credential stuffing e inventario falso | Super Bot Fight Mode según plan; Bot Management completo como oferta Enterprise | AWS WAF Bot Control | Azure WAF Bot Manager ruleset |
| **API Shield** | Descubrimiento de endpoints, mTLS, validación de esquema, JWT y detección de abuso | Proteger APIs públicas y de socios | Algunas capacidades por plan; paquete completo principalmente Enterprise | Amazon API Gateway + AWS WAF + mTLS | Azure API Management + WAF + mTLS |
| **Turnstile** | Alternativa a CAPTCHA con desafíos no interactivos, widgets y Siteverify | Proteger formularios, login, registro y checkout con menos fricción | Plan Free disponible; Enterprise ofrece controles y soporte adicionales | AWS WAF CAPTCHA / Challenge | Azure WAF CAPTCHA challenge |
| **Page Shield / Client-Side Security** | Inventario y monitorización de JavaScript, CSP y detección de cambios o exfiltración en el navegador | Mitigar ataques Magecart y riesgo de scripts de terceros | Visibilidad limitada según plan; protección avanzada/complemento de pago | Sin equivalente 1:1; CloudFront + WAF y monitorización de terceros | Sin equivalente 1:1; Front Door + WAF y Defender/monitorización personalizada |
| **Leaked Credentials Detection** | Identifica credenciales conocidas como filtradas durante autenticaciones | Reducir account takeover y credential stuffing | Campo básico disponible desde Free; más integración según plan | AWS WAF Fraud Control Account Takeover Prevention | Entra ID Protection + WAF, sin equivalencia 1:1 |
| **Security Center** | Gestión de postura, recomendaciones, insights y búsqueda de riesgos en dominios | Priorizar configuraciones inseguras y exposición | Capacidades variables por plan; funciones avanzadas en Enterprise | AWS Security Hub | Microsoft Defender for Cloud |
| **Cloudflare for SaaS** | Hostnames personalizados, certificados, orígenes por cliente y analítica para plataformas | Dar CDN y seguridad a dominios de clientes de un SaaS | 100 hostnames incluidos en Free/Pro/Business; excedentes y Enterprise de pago | CloudFront SaaS Manager + ACM | Front Door + administración de dominios personalizados |

## 4. Cómputo y alojamiento para desarrolladores

| Servicio | Características principales | Casos de uso | Disponibilidad | Equivalente aproximado en AWS | Equivalente aproximado en Azure |
|---|---|---|---|---|---|
| **Workers** | Runtime serverless distribuido, JavaScript/TypeScript, WebAssembly, bindings y despliegue global | APIs, middleware, SSR, personalización en el borde y microservicios | Free: 100 000 solicitudes/día; Paid desde USD 5/mes más consumo | Lambda@Edge, CloudFront Functions y AWS Lambda | Azure Functions + Azure Front Door |
| **Workers Static Assets** | Sirve assets y código dinámico dentro del mismo proyecto Worker | SPA, sitios estáticos y aplicaciones full-stack | Incluido en Workers Free/Paid con sus límites | CloudFront + S3 / Amplify Hosting | Static Web Apps / Front Door + Blob Storage |
| **Pages Functions** | Funciones serverless integradas a proyectos Pages | Formularios, autenticación, SSR y APIs junto a un sitio | Se contabilizan como Workers Free/Paid | Amplify Functions / Lambda | Static Web Apps managed functions / Azure Functions |
| **Containers** | Contenedores serverless asociados a Workers, con CPU, memoria, disco y escalado administrado | Ejecutar lenguajes, binarios o cargas no compatibles con el runtime Workers | Cuota incluida y pago por consumo; revisar disponibilidad regional/estado del producto | AWS Fargate / App Runner | Azure Container Apps |
| **Durable Objects** | Cómputo con identidad global única, coordinación fuerte, almacenamiento transaccional SQLite y WebSockets | Salas de chat, sesiones colaborativas, líderes, contadores y estado consistente | Disponible en Free y Paid; backend y cuotas dependen del plan | Lambda + DynamoDB, sin equivalente 1:1 | Durable Functions + Cosmos DB, sin equivalente 1:1 |
| **Workflows** | Ejecuciones durables, pasos, reintentos, pausas y estado para procesos largos | Orquestar pagos, aprovisionamiento, pipelines y tareas de IA | Cuota gratuita/uso incluido y facturación en Workers Paid | AWS Step Functions | Durable Functions / Logic Apps |
| **Browser Rendering / Browser Run** | Navegadores Chromium controlables mediante Puppeteer y automatización de navegación | Capturas, PDF, scraping autorizado, pruebas y agentes que usan la web | Cuota gratuita limitada y consumo en plan Workers Paid | Fargate/Lambda con navegador headless; sin servicio 1:1 | Container Apps/Functions con navegador headless; sin servicio 1:1 |
| **Workers for Platforms** | Despliegue aislado de código de clientes, dispatch, límites y multitenancy | Plataformas low-code, extensiones y hosting programable para terceros | De pago; no disponible en Workers Free | Lambda multi-tenant + API Gateway, con diseño propio | Functions/Container Apps multi-tenant, con diseño propio |
| **Cloudflare for Platforms** | Conjunto de Workers for Platforms, Cloudflare for SaaS y recursos aislados por cliente | Crear plataformas de desarrollo, comercio o “vibe coding” | Mixto: componentes self-service y capacidades de pago/Enterprise | Amplify + Lambda + CloudFront + Route 53 | Static Web Apps + Functions + Front Door + Azure DNS |

## 5. Almacenamiento, bases de datos, mensajería y datos

| Servicio | Características principales | Casos de uso | Disponibilidad | Equivalente aproximado en AWS | Equivalente aproximado en Azure |
|---|---|---|---|---|---|
| **R2 Object Storage** | API compatible con S3, clases Standard/Infrequent Access y egreso directo sin cargo | Assets, backups, data lakes y contenido servido por CDN | Free: 10 GB-mes, 1 M operaciones A y 10 M B/mes; excedente por uso | Amazon S3 | Azure Blob Storage |
| **D1** | SQL serverless con semántica SQLite, recuperación integrada y acceso desde Workers/API | Bases por tenant, catálogos, configuración y aplicaciones web | Free: cuotas diarias y 5 GB; Paid incluye uso y cobra excedentes | Aurora Serverless / RDS, sin compatibilidad SQLite administrada equivalente | Azure SQL Database serverless / Cosmos DB for relational alternatives |
| **Workers KV** | Almacén clave-valor global, lecturas de baja latencia y consistencia eventual | Configuración, sesiones tolerantes a eventualidad, flags y caché de datos | Cuota limitada en Free; incluido y por consumo en Workers Paid | DynamoDB Global Tables / CloudFront KeyValueStore según caso | Azure Cosmos DB / App Configuration según caso |
| **Hyperdrive** | Pooling, caché y aceleración global de conexiones a PostgreSQL/MySQL existentes | Conectar Workers a bases regionales reduciendo latencia y conexiones | Uso limitado en Free; incluido y por consumo en Workers Paid | Amazon RDS Proxy + Global Accelerator | PgBouncer/connection pooling de Azure Database + Front Door, sin equivalente 1:1 |
| **Queues** | Colas administradas, productores/consumidores Workers, lotes, reintentos y dead-letter queues | Procesamiento asíncrono, desacoplar servicios y absorber picos | Cuota gratuita y cobro por operaciones en Paid | Amazon SQS | Azure Service Bus Queues / Queue Storage |
| **Pipelines** | Ingesta de streams y carga administrada de eventos hacia R2 | Logs, telemetría y data lake en tiempo casi real | Servicio por consumo; verificar cuota gratuita vigente | Amazon Data Firehose | Azure Event Hubs + Stream Analytics/Data Factory |
| **Vectorize** | Base vectorial distribuida, índices y consultas por similitud integrada con Workers AI | RAG, búsqueda semántica y recomendaciones | Cuota de prueba; la disponibilidad de escritura/producción y cobro se vincula a Workers Paid | OpenSearch Serverless vector engine / Aurora pgvector | Azure AI Search vector search / Cosmos DB vector search |
| **Workers Analytics Engine** | Escritura de eventos de alta cardinalidad y consultas SQL agregadas | Métricas por cliente, uso de APIs y analítica operacional | Cuota en Free y facturación por eventos/consultas en Paid | Timestream / Athena | Azure Data Explorer |
| **R2 Data Catalog y R2 SQL** | Catálogo Apache Iceberg y motor SQL distribuido sobre datos en R2 | Lakehouse, tablas abiertas y consultas analíticas sin mover datos | Cuotas incluidas y pago por almacenamiento/consulta, adicional a R2 | AWS Glue Data Catalog + Athena + S3 | Microsoft Purview/OneLake catalog + Synapse serverless sobre Data Lake |

## 6. Inteligencia artificial, multimedia y tiempo real

| Servicio | Características principales | Casos de uso | Disponibilidad | Equivalente aproximado en AWS | Equivalente aproximado en Azure |
|---|---|---|---|---|---|
| **Workers AI** | Inferencia de modelos generativos y ML sobre GPUs serverless mediante API o binding | Chat, embeddings, visión, clasificación y generación | Disponible en todos los planes con cuota gratuita y cobro por “neurons”/uso | Amazon Bedrock / SageMaker Serverless Inference | Azure AI Foundry Models / Azure Machine Learning |
| **AI Gateway** | Observabilidad, caché, rate limiting, retries, fallback y control de llamadas a modelos | Gobernar y optimizar aplicaciones que usan uno o varios proveedores de IA | Nivel gratuito y funciones/volumen de pago | Amazon Bedrock model invocation logging + API Gateway, sin equivalente 1:1 | Azure API Management AI gateway + Azure Monitor |
| **Agents y Agents SDK** | Agentes con estado, scheduling, WebSockets, Durable Objects y acceso a modelos/herramientas | Asistentes persistentes, automatización y agentes en tiempo real | Framework abierto; el consumo de Workers, Durable Objects e IA puede ser facturable | Agents for Amazon Bedrock | Azure AI Agent Service |
| **AI Search** | Pipeline RAG administrado para indexar contenido, recuperar contexto y responder con modelos | Buscadores internos, soporte y chat sobre documentación | Servicio administrado por consumo; confirmar disponibilidad y precios antes de producción | Amazon Bedrock Knowledge Bases | Azure AI Search + Azure AI Foundry |
| **Cloudflare Images** | Almacenamiento, transformación, optimización y entrega global de imágenes | Miniaturas, formatos modernos, resizing y pipelines de medios | Complemento/consumo; ciertas transformaciones dependen del plan | S3 + CloudFront + solución de Dynamic Image Transformation | Blob Storage + Front Door + Functions para transformación |
| **Stream y Realtime** | Carga, codificación y entrega de video; SFU/TURN y APIs para audio/video/datos en tiempo real | Video bajo demanda, streaming y comunicaciones WebRTC | Stream desde pago por uso (algunos planes incluyen minutos); Realtime por consumo | AWS Elemental MediaConvert/MediaPackage + CloudFront; Kinesis Video Streams WebRTC | Azure Communication Services + Blob/Front Door; no hay reemplazo 1:1 de Azure Media Services retirado |

## 7. Zero Trust, SASE y seguridad del puesto de trabajo

| Servicio | Características principales | Casos de uso | Disponibilidad | Equivalente aproximado en AWS | Equivalente aproximado en Azure |
|---|---|---|---|---|---|
| **Cloudflare Access** | ZTNA por identidad y dispositivo, integración con IdP, service tokens y políticas por aplicación | Sustituir VPN para aplicaciones privadas y proteger herramientas internas | Zero Trust Free hasta 50 usuarios; PAYG desde USD 7/usuario/mes; Contract | AWS Verified Access | Microsoft Entra Private Access / Application Proxy |
| **Cloudflare Tunnel** | Conexión saliente cifrada desde `cloudflared`, sin publicar IP de origen | Publicar apps privadas, conectar redes y proteger orígenes | Crear/publicar un túnel no exige Access de pago; políticas por identidad consumen seats | Systems Manager Session Manager/Verified Access connectors, sin equivalente 1:1 | Entra Application Proxy / Azure Arc tunnels según caso |
| **Gateway (Secure Web Gateway)** | Filtrado DNS, HTTP y red; inspección TLS, aislamiento y políticas de egreso | Proteger navegación y controlar acceso a Internet/SaaS | Zero Trust Free/PAYG/Contract con diferencias de retención y funciones | Route 53 Resolver DNS Firewall + AWS Network Firewall | Azure Firewall + DNS Security Policy / Entra Internet Access |
| **WARP Client** | Agente de dispositivo que envía tráfico a Cloudflare One y aplica postura/políticas | Acceso remoto, DNS seguro y conectividad Zero Trust | Cliente personal gratuito; gestión organizacional bajo Zero Trust | AWS Client VPN / Verified Access | Azure VPN Client / Global Secure Access client |
| **Browser Isolation** | Ejecuta contenido web activo en un navegador remoto y transmite una representación segura | Aislar sitios riesgosos, contratistas y navegación privilegiada | Capacidad limitada/oferta según plan; controles completos en Contract | Sin equivalente nativo 1:1; escritorios aislados con WorkSpaces/AppStream | Defender for Cloud Apps session controls / aislamiento administrado, sin 1:1 |
| **CASB y DLP** | Descubrimiento de Shadow IT, escaneo SaaS y detección/control de datos sensibles | Evitar fuga de datos y evaluar configuraciones de aplicaciones SaaS | Funciones inline limitadas en planes self-service; capacidades completas en Contract | Amazon Macie + controles SaaS de terceros; no hay CASB general 1:1 | Microsoft Defender for Cloud Apps + Microsoft Purview DLP |
| **Email Security / DMARC Management** | Detección de phishing, malware, suplantación y gestión de autenticación de dominio | Proteger correo empresarial y reducir spoofing | Principalmente Contract/complemento empresarial | Amazon SES Mail Manager + GuardDuty/Macie, sin equivalencia completa | Microsoft Defender for Office 365 + DMARC reporting |
| **Digital Experience Monitoring** | Pruebas sintéticas y visibilidad de dispositivo, red y aplicaciones | Diagnosticar problemas de usuarios remotos y SaaS | Parte de Cloudflare One; funciones y retención según plan/contrato | CloudWatch Internet Monitor + CloudWatch Synthetics | Azure Monitor + Application Insights + Network Watcher |

## 8. Redes y conectividad empresarial

| Servicio | Características principales | Casos de uso | Disponibilidad | Equivalente aproximado en AWS | Equivalente aproximado en Azure |
|---|---|---|---|---|---|
| **Magic Transit** | Mitigación DDoS y servicios de red para prefijos IP propios mediante BGP/GRE/CNI | Proteger centros de datos, redes híbridas y rangos IP completos | Enterprise/contrato | AWS Shield Advanced + Global Accelerator + Network Firewall | Azure DDoS Protection + Azure Firewall/Front Door |
| **Magic WAN** | WAN global, conectividad de sedes/cloud, routing y políticas integradas con SASE | Sustituir MPLS/SD-WAN y conectar redes híbridas | Enterprise/contrato | AWS Cloud WAN + Transit Gateway | Azure Virtual WAN |
| **Magic Firewall** | Firewall de red como servicio sobre tráfico protegido por Cloudflare | Aplicar políticas L3/L4 a redes y sitios distribuidos | Enterprise/contrato | AWS Network Firewall | Azure Firewall |
| **Spectrum** | Proxy y protección DDoS para aplicaciones TCP/UDP no HTTP | SSH, juegos, VoIP, correo y protocolos personalizados | Pro/Business para casos limitados; Enterprise para cobertura avanzada | AWS Global Accelerator + Network Load Balancer + Shield | Azure Load Balancer + DDoS Protection; Front Door solo para HTTP/S |
| **Cloudflare Network Interconnect (CNI)** | Interconexión física o virtual privada con la red de Cloudflare | Alto volumen, latencia predecible y evitar Internet público | Enterprise/contrato y costos del proveedor de interconexión | AWS Direct Connect | Azure ExpressRoute |
| **Magic Network Monitoring** | Ingesta y análisis de NetFlow/sFlow para visibilidad y alertas DDoS | Observar tráfico de red antes de migrar protección o investigar anomalías | Oferta empresarial; modalidad gratuita limitada puede estar disponible | VPC Flow Logs + Network Flow Monitor | Network Watcher + NSG Flow Logs/Traffic Analytics |
| **DNS Firewall e Internal DNS** | Proxy DNS para servidores autoritativos y resolución/controles DNS privados | Proteger infraestructura DNS y resolver nombres internos en Zero Trust | Principalmente Enterprise/Cloudflare One según producto | Route 53 Resolver DNS Firewall + Private Hosted Zones | Azure DNS Private Resolver + DNS Security Policy |

## 9. Servicios complementarios destacados

| Servicio | Categoría | Uso principal | Disponibilidad | AWS aproximado | Azure aproximado |
|---|---|---|---|---|---|
| **Email Routing** | Correo | Crear alias y reenviar correo entrante sin operar un servidor | Gratuito; no proporciona buzón ni envío transaccional completo | SES inbound receipt rules | Sin equivalente simple de reenvío; Logic Apps/Functions + servicio de correo |
| **Cloudflare Web Analytics** | Observabilidad | Analítica web centrada en privacidad, sin instalar infraestructura | Gratuito | CloudWatch RUM | Application Insights |
| **Log Explorer / Logpush** | Logs | Explorar o exportar logs de seguridad, red y rendimiento | Log Explorer es complemento de pago; Logpush depende del producto/plan | CloudWatch Logs / Firehose / S3 | Log Analytics / Event Hubs / Storage |
| **Zaraz** | Etiquetas de terceros | Ejecutar y controlar herramientas de analítica/marketing desde el borde | Versión incluida según plan y límites; capacidad adicional de pago | Sin equivalente directo; tag manager + Lambda@Edge | Sin equivalente directo; tag manager + Functions/Front Door |
| **1.1.1.1 Resolver** | DNS público | Resolución DNS recursiva pública con DoH/DoT y opciones familiares | Gratuito para usuarios finales | Route 53 Resolver público no equivalente; Amazon DNS es para workloads | Azure DNS recursivo público no equivalente; servicio orientado a workloads |

## 10. Guía rápida de selección

| Necesidad | Servicios Cloudflare recomendados |
|---|---|
| Sitio estático o JAMstack | Pages o Workers Static Assets + DNS + CDN + Turnstile |
| Aplicación web global | Workers + D1/KV/Durable Objects + R2 + WAF |
| API pública segura | Workers o infraestructura existente + API Shield + WAF + Rate Limiting + Bot Management |
| E-commerce | CDN + WAF + Bot Management + Waiting Room + Images + Load Balancing |
| Plataforma SaaS multi-tenant | Cloudflare for Platforms + Workers for Platforms + Cloudflare for SaaS + recursos aislados |
| Aplicación RAG/IA | Workers AI + AI Gateway + Vectorize/AI Search + R2 + Agents |
| Acceso privado sin VPN tradicional | Access + Tunnel + WARP + Gateway |
| Seguridad de navegación y datos | Gateway + Browser Isolation + CASB + DLP + Email Security |
| Proteger una red o centro de datos completo | Magic Transit + Magic Firewall + CNI + Magic Network Monitoring |
| Conectar sedes y múltiples nubes | Magic WAN + CNI + Cloudflare One |

## 11. Observaciones para comparar costos

1. **No comparar solo el precio nominal.** Cloudflare suele integrar red global, TLS, mitigación DDoS y ausencia de cargos de egreso en determinados productos; AWS y Azure pueden separar esas partidas.
2. **Los planes de aplicación son por dominio.** Pro o Business aplicado a varios dominios multiplica la cuota por el número de dominios facturables.
3. **Workers Paid no equivale a Pro/Business.** Es una suscripción de plataforma para desarrolladores y puede coexistir con el plan de cada dominio.
4. **“Free” implica límites.** Al alcanzarlos, algunos productos bloquean operaciones hasta el siguiente periodo; otros permiten excedentes facturados solo después de activar un plan de pago.
5. **Enterprise requiere una comparación contractual.** Deben evaluarse SLA, soporte, retención de logs, localización de datos, volumen de tráfico y compromisos mínimos.
6. **Revise la arquitectura de salida de datos.** R2 no cobra egreso directo, mientras que otros servicios conectados a R2 sí pueden generar cargos propios.

## 12. Referencias oficiales

### Cloudflare

- [Directorio completo de productos](https://developers.cloudflare.com/products/)
- [Casos de uso](https://developers.cloudflare.com/use-cases/)
- [Planes y precios](https://www.cloudflare.com/plans/)
- [Facturación basada en uso](https://developers.cloudflare.com/billing/understand/usage-based-billing/)
- [Precios de Workers](https://developers.cloudflare.com/workers/platform/pricing/)
- [Límites de Pages](https://developers.cloudflare.com/pages/platform/limits/)
- [Precios de R2](https://developers.cloudflare.com/r2/pricing/)
- [Precios de D1](https://developers.cloudflare.com/d1/platform/pricing/)
- [Opciones de almacenamiento de Workers](https://developers.cloudflare.com/workers/platform/storage-options/)
- [Planes de Turnstile](https://developers.cloudflare.com/turnstile/plans/)
- [Cloudflare One / Zero Trust](https://developers.cloudflare.com/cloudflare-one/)
- [Funciones y planes de DNS](https://developers.cloudflare.com/dns/reference/all-features/)
- [Funciones y planes del WAF](https://developers.cloudflare.com/waf/)

### AWS y Microsoft Azure

- [Productos de AWS](https://aws.amazon.com/products/)
- [AWS Networking and Content Delivery](https://aws.amazon.com/products/networking/)
- [AWS Security, Identity and Compliance](https://aws.amazon.com/products/security/)
- [Productos de Microsoft Azure](https://azure.microsoft.com/products/)
- [Azure Networking](https://azure.microsoft.com/products/category/networking/)
- [Azure Security](https://azure.microsoft.com/products/category/security/)

---

> **Nota de mantenimiento:** validar planes, cuotas y precios en los enlaces oficiales antes de una compra o diseño definitivo. Los nombres y condiciones comerciales pueden cambiar sin que cambie la función general descrita en este documento.
