# AI Gateway

> Documento revisado: 2026-10-22
> Alcance: concepto, arquitectura, capacidades y criterios de selección de un gateway para tráfico de modelos de IA.

## Descripción

Un **AI Gateway** es un proxy especializado que se coloca entre las aplicaciones, agentes o SDKs y los proveedores de modelos de IA. Ofrece un punto único para autenticación, enrutamiento, control de gasto, observabilidad y políticas, sin que cada aplicación tenga que implementar y mantener una integración distinta por proveedor.

En términos prácticos, responde a estas preguntas:

- ¿Cómo uso varios proveedores con una sola integración?
- ¿Dónde guardo y roto las claves de los proveedores?
- ¿Cómo sé cuánto gasta cada equipo, aplicación o usuario?
- ¿Cómo evito que una caída de un proveedor tumbe mi aplicación?
- ¿Cómo aplico límites, caché y filtros de contenido de forma uniforme?
- ¿Cómo registro latencia, tokens y coste de cada solicitud?

No es un modelo de IA, ni un orquestador de agentes, ni un sustituto del diseño de seguridad de la aplicación.

## Qué problema resuelve

Sin gateway, cada aplicación repite el mismo trabajo:

```
App A ──> OpenAI SDK ──> OpenAI
App B ──> Anthropic SDK ──> Anthropic
App C ──> SDK propio ──> Bedrock
```

Con gateway:

```
App A ─┐
App B ─┼──> AI Gateway ──> OpenAI / Anthropic / Google / Bedrock / local
App C ─┘
```

Beneficios principales:

- **Una sola integración** para varios proveedores.
- **Credenciales centralizadas** y rotación de claves.
- **Control de gasto** por equipo, proyecto, usuario o clave.
- **Resiliencia**: reintentos, timeouts, fallback y balanceo.
- **Observabilidad** homogénea: latencia, tokens, coste y errores.
- **Políticas uniformes**: límites, modelos permitidos, guardrails.
- **Migración más sencilla** entre proveedores y modelos.

## Arquitectura

```
┌─────────────────────────────────────────────┐
│ Clientes                                    │
│ apps · agentes · CLIs · SDKs · workflows    │
└──────────────────┬──────────────────────────┘
                   │  HTTP / streaming
                   ▼
┌─────────────────────────────────────────────┐
│ AI Gateway                                  │
│                                             │
│ - Autenticación y claves                    │
│ - Catálogo de modelos y proveedores         │
│ - Enrutamiento, balanceo y fallback         │
│ - Cuotas, presupuestos y rate limits        │
│ - Caché                                     │
│ - Guardrails y filtros                      │
│ - Logs, métricas, trazas y coste            │
└──────────────────┬──────────────────────────┘
                   │
      ┌────────────┼────────────┬─────────────┐
      ▼            ▼            ▼             ▼
   OpenAI      Anthropic      Google       Bedrock
      │            │            │             │
      └────────────┴────────────┴─────────────┘
                   Modelos locales (opcional)
```

El gateway puede ser un servicio gestionado (SaaS), un servidor autoalojado, un SDK embebido o una función nativa de un proveedor cloud.

## Conceptos clave

| Concepto | Significado |
|----------|-------------|
| **Endpoint unificado** | Una URL y un formato de petición para varios proveedores, normalmente compatible con OpenAI |
| **Proveedor** | OpenAI, Anthropic, Google, Amazon Bedrock, Azure, OpenRouter, etc. |
| **Despliegue** | Una instancia concreta de un modelo en un proveedor, con su clave, cuota y región |
| **Grupo de modelos** | Alias lógico que agrupa varios despliegues del mismo modelo |
| **Clave virtual** | Credencial emitida por el gateway para una app, equipo o usuario |
| **Presupuesto** | Límite de gasto por clave, equipo, proyecto o periodo |
| **Rate limit** | Límite de peticiones o tokens por minuto |
| **Enrutamiento** | Regla que decide qué despliegue atiende cada solicitud |
| **Fallback** | Proveedor o modelo alternativo si el principal falla |
| **Caché** | Respuesta reutilizada para reducir coste y latencia |
| **Guardrail** | Filtro de contenido, PII, prompt injection o política de uso |
| **BYOK** | Bring Your Own Key: usar las claves del cliente en el gateway |
| **ZDR** | Zero Data Retention: no retener el contenido de las peticiones |

## Capacidades

### Unificación de proveedores

- API compatible con OpenAI, Anthropic o SDK propios.
- Catálogo de modelos y capacidades.
- Soporte de texto, imágenes, audio, embeddings, reranking y herramientas.
- Streaming y tool calling.
- Traducción entre formatos de petición y respuesta.

### Gestión de credenciales

- Claves de proveedor guardadas en el gateway, nunca en cada aplicación.
- Claves virtuales por equipo, proyecto o usuario.
- Rotación y revocación.
- Integración con gestores de secretos.
- BYOK para conservar contratos y compromisos de gasto.

### Control de acceso y gasto

- Presupuestos por clave, usuario, equipo o proyecto.
- Límites de peticiones y tokens por minuto.
- Modelos permitidos por clave o equipo.
- Alertas y bloqueo al superar umbrales.
- Atribución de coste por etiqueta, aplicación o cliente.

### Enrutamiento y resiliencia

- Balanceo entre despliegues del mismo modelo.
- Estrategias: aleatoria, least-busy, latencia, coste, uso.
- Prioridad de despliegues.
- Reintentos, timeouts y backoff.
- Fallback entre proveedores o modelos.
- Enfriamiento de despliegues con errores.
- Afinidad de sesión para caché de prompt.

### Observabilidad

- Logs de cada solicitud.
- Tokens de entrada y salida.
- Latencia total y time to first token (TTFT).
- Coste estimado y real.
- Proveedor y despliegue que atendió.
- Intentos de enrutamiento y errores.
- Exportación a OpenTelemetry, Prometheus, Datadog, Langfuse y otros.

### Optimización y seguridad

- Caché exacta o semántica.
- Compresión o compactación de contexto.
- Guardrails de entrada y salida.
- Enmascaramiento de PII.
- Listas de proveedores permitidos.
- Zero Data Retention y acuerdos de no entrenamiento.
- Aislamiento por equipo o tenant.

### Protocolos emergentes

- **MCP Gateway:** centraliza el acceso de agentes a herramientas MCP.
- **A2A Gateway:** registra y gobierna agentes que se llaman entre sí.
- **Auto Router:** elige el modelo más barato capaz de resolver la tarea.

## Modelos de despliegue

| Modelo | Ventajas | Inconvenientes | Ejemplos |
|--------|----------|----------------|----------|
| **SaaS gestionado** | Cero operación, alta disponibilidad, soporte | Dependencia del proveedor, datos fuera de tu infraestructura | Vercel AI Gateway, Cloudflare AI Gateway |
| **Autoalojado** | Control total, soberanía del dato, personalización | Operas HA, base de datos, Redis, upgrades y seguridad | LiteLLM, Kong AI Gateway, Portkey Gateway |
| **SDK embebido** | Sin salto de red, integración en el código | No centraliza varias apps; menos gobierno | LiteLLM Python SDK |
| **Nativo del cloud** | Integración con IAM, red y facturación del cloud | Acoplamiento al proveedor | Azure API Management, AWS Bedrock |

## Comparativa: Vercel AI Gateway y LiteLLM

| Criterio | Vercel AI Gateway | LiteLLM |
|----------|-------------------|---------|
| **Modelo** | SaaS gestionado | Servidor autoalojado |
| **Licencia** | Servicio propietario | Núcleo MIT; funciones Enterprise con licencia |
| **Catálogo** | 400+ modelos | 100+ proveedores |
| **Protocolos** | AI SDK, OpenAI Chat Completions, OpenAI Responses, Anthropic Messages | API compatible con OpenAI, MCP y A2A |
| **Claves y presupuestos** | Por equipo, proyecto y clave | Claves virtuales, usuarios, equipos y etiquetas |
| **Enrutamiento** | Orden de proveedores y fallback de modelos | Varias estrategias, orden, reintentos y fallback |
| **Observabilidad** | Dashboard, logs y API de reporting | Admin UI, logs, OpenTelemetry, Prometheus, Datadog, Langfuse |
| **Self-hosted** | No | Sí |
| **MCP / A2A** | No documentado como gateway MCP | Sí, MCP Gateway y Agent Gateway |
| **Operación** | Cero | Postgres, Redis, HA, upgrades |
| **Ideal para** | Equipos sin operación y ecosistema Vercel | Control del dato, multi-tenant y personalización |

Cloudflare AI Gateway es otra alternativa gestionada, documentada en [Cloudflare AI Gateway](../../cloud/Cloudflare/AI-Gateway.md).

## Cuándo usar un AI Gateway

### Tiene sentido

- Varias aplicaciones o equipos consumen modelos.
- Se usan dos o más proveedores.
- Se necesita fallback ante caídas o límites.
- Se quiere controlar gasto por equipo o cliente.
- Se requiere observabilidad uniforme y auditoría.
- Se deben aplicar guardrails y políticas comunes.
- Se quiere migrar de proveedor sin tocar cada aplicación.
- Se necesita gobernar el acceso de agentes a MCP o A2A.

### No hace falta

- Una sola aplicación, un solo proveedor y un solo equipo.
- No hay requisitos de cuotas, auditoría ni multi-tenant.
- El volumen es bajo y el coste de operar el gateway supera el beneficio.
- El SDK del proveedor ya cubre todas las necesidades y no se prevé cambiar.

## Criterios de selección

1. **Modelos y proveedores** que necesitas hoy y a medio plazo.
2. **Protocolos**: OpenAI, Anthropic, Responses, AI SDK, MCP, A2A.
3. **Modo de despliegue**: SaaS, self-hosted, cloud nativo.
4. **Datos**: residencia, retención, entrenamiento, cifrado y auditoría.
5. **Gobierno**: claves virtuales, presupuestos, rate limits, RBAC, SSO.
6. **Resiliencia**: fallback, balanceo, timeouts, SLA.
7. **Observabilidad**: logs, métricas, trazas y exportación.
8. **Coste total**: licencia, infraestructura, egress y operación.
9. **Latencia añadida** y capacidad de streaming.
10. **Lock-in** y facilidad de migración.
11. **Comunidad y actividad** del proyecto, si es open source.
12. **Seguridad**: rotación de claves, secretos, aislamiento, WAF.

## Costes

Un gateway no elimina el coste de los modelos; lo hace visible y controlable.

- **Software**: puede ser gratuito (LiteLLM MIT) o un servicio de pago.
- **Infraestructura**: servidores, Postgres, Redis, réplicas y backups si es autoalojado.
- **Tráfico**: egresos y posible coste de observabilidad.
- **Tokens**: el gasto principal, facturado por el proveedor.
- **Operación**: tiempo de ingeniería, guardias y upgrades.

Los gateways gestionados suelen cobrar sin markup sobre el token, pero conviene verificar cargos por funcionalidades, créditos, facturación o soporte.

## Riesgos y limitaciones

- **Punto único de fallo** si no se despliega con alta disponibilidad.
- **Latencia adicional** por el salto de red y el procesamiento.
- **Dependencia del gateway** si se convierte en el único camino a los modelos.
- **Retención de datos** en logs y trazas; hay que definir qué se guarda.
- **Falsa sensación de seguridad**: el gateway no sustituye la autenticación de la aplicación.
- **Funciones desiguales** entre proveedores y versiones.
- **Coste oculto** de operar Postgres, Redis y observabilidad.
- **Lock-in** por configuración, claves virtuales o formatos propietarios.
- **Desfase de funciones** si el proveedor lanza capacidades nuevas.

## Buenas prácticas

1. Usa un endpoint compatible con OpenAI cuando sea posible.
2. Guarda las claves en un gestor de secretos, no en el repositorio.
3. Emite claves virtuales por aplicación, equipo o entorno.
4. Define presupuestos y alertas antes de abrir el grifo.
5. Configura timeouts, reintentos y fallback desde el primer día.
6. Redacta prompts y datos sensibles en los logs.
7. Despliega al menos dos réplicas y prueba la conmutación.
8. Versiona la configuración del gateway.
9. Mide la latencia añadida y el impacto en streaming.
10. Documenta el flujo de datos y la retención.
11. Revisa periódicamente claves, permisos y gasto.
12. Prueba la migración a otro proveedor para evitar lock-in.

## Casos de uso

- **Aplicación multi-proveedor** con fallback automático.
- **SaaS multi-tenant** con cuotas y facturación por cliente.
- **Agentes de código** que comparten una clave y un presupuesto, como OpenCode, Codex o Claude Code.
- **Equipos de datos** que centralizan embeddings y modelos de chat.
- **Entornos regulados** que exigen self-hosting y retención cero.
- **Migraciones** de un proveedor a otro sin cambiar el código de cada app.
- **Experimentación A/B** entre modelos y variantes.
- **Gobierno de MCP/A2A** para agentes que usan herramientas externas.

## Ver también

- [Vercel AI Gateway](vercel-ai-gateway.md): guía del servicio gestionado.
- [LiteLLM](litellm.md): gateway open source autoalojado.
- [Cloudflare AI Gateway](../../cloud/Cloudflare/AI-Gateway.md): alternativa gestionada.
- [OpenCode V2](../agentes-codigo/opencode-v2.md): agente de código con soporte de proveedores.
- [Agentes IA remotos en VPS](../multi-agente/agentes-remotos/README.md): ejecución de agentes en servidores.
- [vLLM y SGLang](../vllm_sglang.md): servidores de modelos locales.
- [Comparación de Ollama](../ollama/comparacion.md): alternativas locales.

## Referencias

- Vercel AI Gateway: <https://vercel.com/docs/ai-gateway>
- LiteLLM: <https://docs.litellm.ai/>
- LiteLLM Gateway: <https://docs.litellm.ai/docs/simple_proxy>
- Cloudflare AI Gateway: <https://developers.cloudflare.com/ai-gateway/>
- OpenRouter: <https://openrouter.ai/>
- OpenTelemetry: <https://opentelemetry.io/>
