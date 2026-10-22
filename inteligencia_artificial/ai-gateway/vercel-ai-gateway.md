# Vercel AI Gateway

> Documento revisado: 2026-10-22
> Producto documentado: [Vercel AI Gateway](https://vercel.com/docs/ai-gateway)
> Última actualización oficial consultada: 2026-09-14

## Descripción

Vercel AI Gateway es un gateway gestionado para llamar a modelos de IA de distintos proveedores desde cualquier infraestructura. Centraliza credenciales, enrutamiento, fallback, presupuestos, observabilidad y políticas, sin que la aplicación tenga que integrar cada proveedor por separado.

No exige que la aplicación esté desplegada en Vercel: se puede llamar desde un servidor, otra nube o un entorno local con una API key. La integración más directa es con el AI SDK, pero también acepta formatos compatibles con OpenAI, Anthropic y Responses.

Su propuesta principal:

- Una sola API key para cientos de modelos.
- Sin markup sobre el precio del proveedor.
- Fallback automático entre proveedores.
- Logs, métricas y control de gasto centralizados.
- BYOK para conservar contratos y compromisos existentes.

## Arquitectura

```
┌──────────────────────────────┐
│ App / agente / SDK           │
│ - AI SDK                     │
│ - OpenAI Chat Completions    │
│ - OpenAI Responses           │
│ - Anthropic Messages         │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│ Vercel AI Gateway            │
│ - API key / OIDC             │
│ - routing y fallback         │
│ - budgets y observabilidad   │
│ - BYOK                       │
└──────────────┬───────────────┘
               │
     ┌─────────┼─────────┬──────────┐
     ▼         ▼         ▼          ▼
  OpenAI   Anthropic   Google    Otros proveedores
```

El gateway es un servicio gestionado: no hay servidor que operar. La aplicación solo cambia la URL base y la credencial.

## Características

### Un endpoint, muchos modelos

- Catálogo de **400+ modelos** de los principales laboratorios y proveedores.
- Un solo formato de API y una sola factura.
- Modelos identificados como `proveedor/modelo`, por ejemplo `openai/gpt-5.5`.
- Disponible en todos los planes de Vercel.
- Sin markup: pagas el precio de lista del proveedor.

### Protocolos y SDKs

| Interfaz | Uso |
|----------|-----|
| **AI SDK** | Integración nativa en TypeScript/JavaScript |
| **OpenAI Chat Completions** | Compatible con clientes y SDKs de OpenAI |
| **OpenAI Responses** | API de respuestas de OpenAI |
| **Anthropic Messages** | Compatible con el formato de Anthropic |
| **HTTP / cURL** | Cualquier lenguaje o herramienta |

Endpoint base:

```text
https://ai-gateway.vercel.sh/v1
```

### Modalidades

- Texto.
- Imágenes.
- Vídeo.
- Audio: voz y transcripción.
- Realtime.
- Embeddings.
- Reranking.
- Herramientas y búsqueda web, según el modelo.

### Enrutamiento y resiliencia

- Enrutamiento entre proveedores del mismo modelo.
- Orden de proveedores preferido.
- Fallback a modelos alternativos.
- Failover automático si un proveedor se degrada.
- Filtrado de proveedores.
- Caché y service tiers según el modelo.
- Registro de cada intento de enrutamiento.

### Observabilidad y gasto

- Logs por solicitud: modelo, proveedor, intentos, latencia, tokens, estado y coste.
- Dashboard de uso, gasto, peticiones, TTFT y tokens por equipo, proyecto o clave.
- Presupuestos por equipo, proyecto, API key o miembro.
- API de reporting personalizado para etiquetar solicitudes por usuario, cliente, feature o entorno.
- Rechazo de nuevas solicitudes cuando se supera el presupuesto aplicable (con credenciales del sistema).

### Autenticación y BYOK

- **API keys** con alcance de equipo.
- **OIDC** para despliegues en Vercel, con credenciales de corta duración.
- **BYOK**: claves propias de los proveedores, sin fee de plataforma.
- Si una solicitud BYOK falla, el gateway puede usar credenciales del sistema como fallback.
- El gasto BYOK se mide aparte y no consume los presupuestos de credenciales del sistema.

### Seguridad y cumplimiento

- **Zero Data Retention (ZDR)** configurable y aplicable a todo el equipo.
- Enrutamiento solo a proveedores que no entrenan con los datos, configurable.
- Lista de proveedores permitidos por equipo.
- Políticas de acceso y manejo de datos.
- Inferencia regional, según la documentación del producto.

### Agentes de código

Vercel documenta la integración de más de 11 agentes de código, entre ellos Claude Code, OpenAI Codex, OpenCode, Cline y Command Code.

Configuración automática:

```bash
npx vercel@latest ai-gateway setup
```

El comando detecta agentes compatibles, crea una API key y escribe su configuración. También se puede configurar manualmente la URL base y la clave.

## Primeros pasos

### 1. Crear una API key

En el dashboard de Vercel, dentro de AI Gateway, crear una clave y guardarla en una variable de entorno:

```bash
export AI_GATEWAY_API_KEY="tu-clave"
```

### 2. Llamar con cURL

```bash
curl https://ai-gateway.vercel.sh/v1/chat/completions \
  -H "Authorization: Bearer $AI_GATEWAY_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "model": "openai/gpt-5.5",
    "messages": [
      { "role": "user", "content": "Explica qué es un AI Gateway en una frase." }
    ]
  }'
```

### 3. Llamar con el AI SDK

```ts
import { generateText } from 'ai';

const { text } = await generateText({
  model: 'openai/gpt-5.5',
  prompt: 'Explica qué es un AI Gateway en una frase.',
});

console.log(text);
```

### 4. Migrar una integración existente

Si ya usas el SDK de OpenAI o Anthropic, normalmente basta con cambiar la URL base y la credencial:

```text
Base URL: https://ai-gateway.vercel.sh/v1
API key:  AI_GATEWAY_API_KEY
Modelo:   proveedor/modelo
```

Consulta la documentación de cada protocolo para los detalles de compatibilidad.

## Precios

- **Sin markup** sobre el precio de lista de los proveedores.
- Pago por tokens consumidos.
- Créditos prepagados por defecto; los clientes Enterprise pueden solicitar factura consolidada.
- BYOK no tiene fee de plataforma y se factura directamente con el proveedor.
- Algunas capacidades pueden meterse por separado o requerir un plan superior.
- Disponible en AWS Marketplace para aplicar compromisos de AWS.

Verifica siempre la página de precios antes de dimensionar un proyecto: los modelos, promociones y condiciones cambian.

## Casos de uso

- Aplicaciones Next.js o AI SDK que quieren cero operación.
- Equipos que ya usan Vercel y quieren una sola factura.
- Fallback entre OpenAI, Anthropic, Google y otros sin código extra.
- Control de gasto por proyecto o cliente.
- Agentes de código que necesitan una clave central y presupuesto.
- Aplicaciones multimodales: texto, imagen, audio y embeddings.
- Empresas que exigen ZDR y proveedores sin entrenamiento.
- Migraciones rápidas desde el SDK de OpenAI o Anthropic.

## Limitaciones

- **Solo gestionado**: no se puede autoalojar.
- **Dependencia de Vercel**: el tráfico y la configuración pasan por su plataforma.
- **Los presupuestos cubren credenciales del sistema**, no el gasto BYOK.
- **Funciones y modelos varían por plan y por fecha**; el catálogo cambia con frecuencia.
- **No se documenta como gateway MCP/A2A**: su foco es el tráfico de inferencia y las herramientas del modelo.
- **Latencia añadida** por el salto al gateway, aunque el producto está optimizado para minimizarla.
- **Compatibilidad parcial**: cada proveedor puede ignorar o rechazar parámetros específicos.
- **No sustituye la autenticación ni la autorización de tu aplicación.**

## Recomendaciones

1. Crea claves por proyecto o entorno, no una sola global.
2. Define presupuestos y alertas desde el primer día.
3. Configura fallback entre proveedores para los modelos críticos.
4. Usa BYOK si tienes contratos o compromisos de gasto con un proveedor.
5. Activa ZDR y proveedores sin entrenamiento si el dato es sensible.
6. Revisa los logs para detectar modelos caros o consultas anómalas.
7. Evalúa la latencia real desde tu región antes de producción.
8. Mantén un plan de salida: la aplicación debería poder apuntar a otro endpoint compatible.
9. No envíes secretos ni datos personales innecesarios en los prompts.
10. Prueba la conmutación y el comportamiento ante errores 429 o caídas.

## Comparación con LiteLLM

| Criterio | Vercel AI Gateway | [LiteLLM](litellm.md) |
|----------|-------------------|-----------------------|
| Operación | Cero | Autoalojado |
| Datos | Gestionados por Vercel | En tu infraestructura |
| Catálogo | 400+ modelos | 100+ proveedores |
| Protocolos | AI SDK, OpenAI, Anthropic | OpenAI-compatible, MCP, A2A |
| Claves y presupuestos | Sí | Sí |
| Fallback | Sí | Sí |
| MCP/A2A | No documentado | Sí |
| Coste | Sin markup + planes | MIT + infraestructura + Enterprise |

Elige Vercel si priorizas cero operación y el ecosistema Vercel. Elige LiteLLM si necesitas control total, soberanía del dato o gobierno de MCP/A2A.

## Ver también

- [AI Gateway: concepto y comparativa](README.md)
- [LiteLLM](litellm.md)
- [Cloudflare AI Gateway](../../cloud/Cloudflare/AI-Gateway.md)
- [OpenCode V2](../agentes-codigo/opencode-v2.md)

## Referencias

- Documentación: <https://vercel.com/docs/ai-gateway>
- Producto: <https://vercel.com/ai-gateway>
- Precios: <https://vercel.com/docs/ai-gateway/pricing>
- AI SDK: <https://ai-sdk.dev/>
- Seguridad: <https://vercel.com/docs/ai-gateway/security-and-compliance>
- Agentes de código: <https://vercel.com/docs/ai-gateway/coding-agents>
