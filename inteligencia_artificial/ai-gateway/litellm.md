# LiteLLM

> Documento revisado: 2026-10-22
> Producto documentado: [LiteLLM](https://docs.litellm.ai/)
> Licencia: MIT en el núcleo; funciones Enterprise con licencia comercial

## Descripción

LiteLLM es un gateway de IA open source y autoalojado que expone **un endpoint compatible con OpenAI** para más de 100 proveedores de modelos, herramientas MCP y agentes A2A. Las aplicaciones envían las peticiones al gateway; este las enruta al proveedor correspondiente y devuelve la respuesta en formato OpenAI.

Su objetivo es centralizar el acceso a los modelos sin cambiar el código de cada aplicación:

- Claves virtuales por usuario, equipo o proyecto.
- Presupuestos, cuotas y rate limits.
- Seguimiento de gasto por clave, equipo, usuario o etiqueta.
- Balanceo, reintentos y fallback entre despliegues.
- Logs, métricas y trazas.
- Guardrails y políticas.
- Gateway para MCP y agentes A2A.

Se puede usar de dos formas:

| Forma | Uso |
|-------|-----|
| **Proxy / gateway** | Varias aplicaciones o personas consumen modelos |
| **Python SDK** | Una sola aplicación Python quiere una interfaz común |

El resto de este documento se centra en el gateway.

## Arquitectura

```
┌─────────────────────────────────────────────┐
│ Apps · agentes · SDKs · CLIs                │
│ OpenAI SDK · LangChain · Claude Code · etc. │
└──────────────────┬──────────────────────────┘
                   │  API compatible con OpenAI
                   ▼
┌─────────────────────────────────────────────┐
│ LiteLLM Proxy (:4000)                       │
│                                             │
│ - Claves virtuales y equipos                │
│ - Router: balanceo, orden, fallback         │
│ - Presupuestos y rate limits                │
│ - Caché                                     │
│ - Guardrails                                │
│ - Logs, métricas y coste                    │
│ - MCP Gateway y Agent Gateway               │
└───────┬──────────────────┬──────────────────┘
        │                  │
        ▼                  ▼
┌───────────────┐   ┌─────────────────────────┐
│ PostgreSQL    │   │ Redis                   │
│ claves,       │   │ rate limits, estado del │
│ equipos,      │   │ router, caché           │
│ gasto, config │   │                         │
└───────────────┘   └─────────────────────────┘
        │
        ▼
┌─────────────────────────────────────────────┐
│ Proveedores                                 │
│ OpenAI · Anthropic · Google · Bedrock ·     │
│ Azure · OpenRouter · Ollama · vLLM · ...    │
└─────────────────────────────────────────────┘
```

Componentes:

- **Proxy**: servidor que atiende las peticiones y aplica las políticas.
- **Router**: decide el despliegue y gestiona reintentos y fallback.
- **Admin UI**: interfaz web para modelos, claves, equipos, gasto y logs.
- **PostgreSQL**: persistencia de claves, equipos, usuarios, gasto y configuración.
- **Redis**: estado compartido para rate limits, router y caché en varias instancias.
- **MCP Gateway**: acceso gobernado a herramientas MCP.
- **Agent Gateway**: registro e invocación de agentes A2A.
- **SDK**: interfaz Python para usar LiteLLM dentro de una aplicación.

## Características

### Unificación de proveedores

- API compatible con OpenAI para más de 100 proveedores.
- Soporte de chat, completions, embeddings, imágenes, audio y otros endpoints.
- Traducción de formatos y parámetros.
- Streaming y tool calling.
- Modelos locales mediante Ollama, vLLM, LM Studio o endpoints compatibles.
- Alias y grupos de modelos.

### Claves virtuales y acceso

- Claves virtuales por usuario, equipo, proyecto o servicio.
- Modelos permitidos por clave.
- Límites de peticiones (RPM) y tokens (TPM).
- Presupuestos por clave, usuario o equipo.
- Etiquetas para atribuir gasto.
- Bloqueo de claves; rotación con Enterprise.
- RBAC y gestión de equipos.
- SSO/SAML, SCIM y auditoría con Enterprise.

### Gasto y cuotas

- Cálculo de coste por solicitud.
- Gasto por clave, usuario, equipo y etiqueta.
- Presupuestos con ventanas temporales.
- Alertas y bloqueo al superar límites.
- Modelos y precios configurables.
- Informes y exportación de logs.

### Enrutamiento y resiliencia

| Estrategia | Comportamiento |
|------------|----------------|
| `simple-shuffle` | Distribución aleatoria; opción por defecto |
| `least-busy` | Menos solicitudes activas |
| `usage-based-routing` | Menor uso de RPM/TPM |
| `latency-based-routing` | Menor latencia observada |
| `cost-based-routing` | Menor coste |

Además:

- Prioridad de despliegues con `order`.
- Reintentos y timeouts.
- Fallback entre modelos o proveedores.
- Enfriamiento de despliegues con errores.
- Estado compartido en Redis para varias instancias.
- Afinidad de sesión para caché de prompt.
- Auto Router (add-on) para elegir el modelo más barato capaz de resolver la tarea.

### Observabilidad

- Logs de solicitudes y respuestas.
- Métricas de latencia, tokens y coste.
- Integración con Langfuse, Datadog, OpenTelemetry, Prometheus y otros.
- Admin UI con uso, gasto y logs.
- Trazas de agentes con LiteLLM Lens.

### Seguridad

- Claves de proveedor guardadas en el gateway o en un gestor de secretos.
- Claves virtuales para no distribuir credenciales reales.
- Guardrails de contenido, PII y políticas.
- Listas de modelos y proveedores permitidos.
- Aislamiento por equipo.
- Auditoría de cambios administrativos con Enterprise.
- Datos y claves permanecen en tu infraestructura.

### Extensiones

- **MCP Gateway:** un endpoint para herramientas MCP, con permisos por clave y equipo.
- **Agent Gateway:** registro de agentes A2A y llamadas con clave virtual y coste.
- **lite CLI:** ejecuta Claude Code o Codex a través del gateway.
- **LiteAdmin MCP:** administra el gateway desde un agente.
- **Plugins y hooks:** personalización del comportamiento.
- **Traffic mirroring:** duplicar tráfico para pruebas.

## Instalación y puesta en marcha

### Quickstart

```bash
curl -fsSL https://raw.githubusercontent.com/BerriAI/litellm/main/scripts/quickstart.sh | sh
```

Abre la Admin UI en:

```text
http://localhost:4000/ui
```

El quickstart levanta el gateway con Docker y PostgreSQL. Para producción se recomienda Helm, Terraform o Kubernetes.

### Configuración mínima

```yaml title="config.yaml"
model_list:
  - model_name: chat
    litellm_params:
      model: openai/gpt-5.5
      api_key: os.environ/OPENAI_API_KEY

  - model_name: claude
    litellm_params:
      model: anthropic/claude-sonnet-4-5
      api_key: os.environ/ANTHROPIC_API_KEY

general_settings:
  master_key: os.environ/LITELLM_MASTER_KEY
  database_url: os.environ/DATABASE_URL

router_settings:
  routing_strategy: simple-shuffle
  num_retries: 2
  timeout: 30
```

Variables principales:

```bash
export LITELLM_MASTER_KEY="sk-..."
export DATABASE_URL="postgresql://usuario:password@host:5432/litellm"
export LITELLM_SALT_KEY="sk-..."   # cifra credenciales en la base de datos
```

> `LITELLM_SALT_KEY` no se puede rotar después de añadir modelos: cifra las credenciales de proveedor guardadas en la base de datos. Guárdala en un gestor de secretos.

Iniciar el proxy:

```bash
litellm --config config.yaml
```

### Crear una clave virtual

```bash
curl http://localhost:4000/key/generate \
  -H "Authorization: Bearer $LITELLM_MASTER_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "models": ["chat", "claude"],
    "max_budget": 10
  }'
```

La respuesta incluye una clave `sk-...` que se entrega a la aplicación o al usuario.

### Llamar al gateway

```python
from openai import OpenAI

client = OpenAI(
    api_key="sk-tu-clave-virtual",
    base_url="http://localhost:4000",
)

response = client.chat.completions.create(
    model="chat",
    messages=[{"role": "user", "content": "Hola"}],
)

print(response.choices[0].message.content)
```

Con cURL:

```bash
curl http://localhost:4000/v1/chat/completions \
  -H "Authorization: Bearer sk-tu-clave-virtual" \
  -H "Content-Type: application/json" \
  -d '{
    "model": "chat",
    "messages": [{"role": "user", "content": "Hola"}]
  }'
```

Cualquier cliente compatible con OpenAI puede apuntar al gateway cambiando la URL base y la clave.

## Despliegue en producción

### Requisitos

| Componente | Necesidad |
|------------|-----------|
| **PostgreSQL** | Claves, equipos, usuarios, gasto y configuración |
| **Redis** | Rate limits, estado del router y caché en varias instancias |
| **Migraciones** | Job que aplica el esquema antes de arrancar las réplicas |
| **Secrets** | Master key, salt key y claves de proveedores |

Sin PostgreSQL no hay gestión de claves, gasto ni persistencia de la UI. Sin Redis, los límites y el estado del router se cuentan por proceso, no entre réplicas.

### Opciones

- **Monolítico:** una imagen sirve tráfico, APIs de administración y UI. Es la opción más simple.
- **Microservicios:** gateway, backend y UI separados, escalables de forma independiente.
- **Kubernetes:** Helm en EKS, GKE o AKS.
- **Terraform:** módulos oficiales para AWS y GCP; en Azure se usa AKS con Helm.
- **Docker Compose:** útil para laboratorio y pruebas.

Buenas prácticas de producción:

- Fijar la versión de la imagen; no usar `latest`.
- Ejecutar al menos dos réplicas detrás de un balanceador.
- Separar el job de migraciones de las réplicas (`DISABLE_SCHEMA_UPDATE=true`).
- Guardar las claves en un gestor de secretos.
- Configurar health checks en `/health/readiness`.
- Monitorizar con Prometheus y logs centralizados.
- Probar la conmutación de base de datos y Redis.
- Revisar el sizing y las conexiones a Postgres.

## Casos de uso

- **Multi-proveedor** con una sola API compatible con OpenAI.
- **SaaS multi-tenant** con claves, presupuestos y gasto por cliente.
- **Equipos internos** que comparten modelos y necesitan cuotas.
- **Agentes de código** como Claude Code, Codex u OpenCode apuntando al gateway.
- **Gobierno de MCP** para herramientas usadas por agentes.
- **Orquestación A2A** con permisos y coste por agente.
- **Entornos regulados** que exigen self-hosting y control del dato.
- **Migración** entre proveedores sin tocar las aplicaciones.
- **Pruebas A/B** y enrutamiento por coste o latencia.
- **Modelos locales** con Ollama, vLLM o endpoints compatibles.

## Limitaciones

- **Lo operas tú**: alta disponibilidad, Postgres, Redis, backups, upgrades y seguridad.
- **PostgreSQL es necesario** para claves, equipos, gasto y persistencia de la UI.
- **Redis es necesario** para límites y estado compartido entre varias instancias.
- **Funciones Enterprise** como SSO/SAML, SCIM, auditoría avanzada y multi-región requieren licencia.
- **La compatibilidad con proveedores varía**: algunos parámetros o capacidades pueden no estar soportados.
- **Latencia añadida** por el salto al proxy y el procesamiento.
- **Complejidad operativa** mayor que un servicio gestionado.
- **No es un sandbox** ni sustituye la autenticación de la aplicación.
- **El ecosistema se mueve rápido**: fija versiones y prueba actualizaciones.

## Comparación con Vercel AI Gateway

| Criterio | LiteLLM | [Vercel AI Gateway](vercel-ai-gateway.md) |
|----------|---------|-------------------------------------------|
| Despliegue | Autoalojado | Gestionado |
| Datos | En tu infraestructura | En Vercel |
| Licencia | MIT + Enterprise | Servicio propietario |
| Proveedores | 100+ | 400+ modelos |
| Protocolos | OpenAI-compatible, MCP, A2A | AI SDK, OpenAI, Anthropic |
| Claves y presupuestos | Sí | Sí |
| Fallback | Sí | Sí |
| MCP/A2A | Sí | No documentado |
| Operación | Postgres, Redis, HA | Cero |

Elige LiteLLM si necesitas control, soberanía del dato, MCP/A2A o personalización. Elige Vercel si prefieres cero operación y una integración directa con su ecosistema.

## Recomendaciones

1. Empieza con el quickstart y pasa a Helm/Terraform cuando vayas a producción.
2. Usa claves virtuales por aplicación, equipo o entorno.
3. Define presupuestos y rate limits antes de abrir el acceso.
4. Configura fallback y timeouts para los modelos críticos.
5. Guarda `LITELLM_MASTER_KEY`, `LITELLM_SALT_KEY` y las claves de proveedor en un gestor de secretos.
6. No rotes `LITELLM_SALT_KEY` después de añadir modelos.
7. Despliega al menos dos réplicas y Redis para límites compartidos.
8. Redacta prompts y datos sensibles en logs.
9. Revisa el gasto por etiqueta y equipo semanalmente.
10. Prueba la migración a otro gateway para evitar lock-in.
11. Fija versiones y lee las notas de actualización.
12. Documenta el flujo de datos y la retención.

## Ver también

- [AI Gateway: concepto y comparativa](README.md)
- [Vercel AI Gateway](vercel-ai-gateway.md)
- [Cloudflare AI Gateway](../../cloud/Cloudflare/AI-Gateway.md)
- [OpenCode V2](../agentes-codigo/opencode-v2.md)
- [Agentes IA remotos en VPS](../multi-agente/agentes-remotos/README.md)

## Referencias

- Documentación: <https://docs.litellm.ai/>
- Gateway: <https://docs.litellm.ai/docs/simple_proxy>
- Claves virtuales: <https://docs.litellm.ai/docs/proxy/virtual_keys>
- Despliegue: <https://docs.litellm.ai/docs/proxy/deploy>
- Balanceo y routing: <https://docs.litellm.ai/docs/proxy/load_balancing>
- MCP Gateway: <https://docs.litellm.ai/docs/mcp>
- Agent Gateway: <https://docs.litellm.ai/docs/a2a>
- GitHub: <https://github.com/BerriAI/litellm>
- Modelos y precios: <https://models.litellm.ai/>
