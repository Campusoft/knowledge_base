# Cloudflare AI Gateway

**Cloudflare AI Gateway** es una capa de proxy y administración para el tráfico entre una aplicación y proveedores de modelos de inteligencia artificial. La aplicación envía sus solicitudes a través de un gateway para centralizar integraciones, observabilidad, controles de rendimiento, costos y seguridad sin tener que implementar cada control por separado en todos los servicios consumidores.

> **Vigencia:** esta guía refleja la documentación oficial consultada el **2 de octubre de 2026**. Algunas capacidades aparecen como beta y las integraciones, limitaciones y precios pueden cambiar; revisar la documentación enlazada antes de diseñar o desplegar.

## Objetivo y clasificación

AI Gateway se clasifica como **gateway de API/proxy de inferencia para servicios de IA**. Se ubica en la ruta de las llamadas de inferencia y ofrece un punto de administración para solicitudes dirigidas a Workers AI y a proveedores externos compatibles.

AI Gateway no es, por sí solo, un modelo fundacional ni una plataforma para entrenar modelos. Tampoco sustituye el diseño de seguridad de la aplicación: autenticación del usuario, autorización, validación del negocio, protección de datos y tratamiento de errores siguen siendo responsabilidad de la solución completa.

## Arquitectura

```text
Usuario o sistema cliente
          │
          ▼
Aplicación / backend / Worker
          │  solicitud de inferencia
          ▼
Cloudflare AI Gateway
  ├─ autenticación y políticas del gateway
  ├─ Guardrails y controles de datos (si están habilitados)
  ├─ caché, rate limiting y reintentos (según configuración)
  ├─ registro, métricas, costos y trazas
  └─ selección/enrutamiento del proveedor
          │
          ▼
Workers AI o proveedor de modelos
          │
          └──────── respuesta de modelo ────────► Gateway ─► Aplicación
```

El cliente puede llamar a los endpoints de AI Gateway o la aplicación puede usar un binding desde un Cloudflare Worker. El gateway procesa la solicitud de acuerdo con la configuración asociada al identificador del gateway y la reenvía al proveedor elegido. La respuesta vuelve a través del gateway, donde pueden aplicarse los controles habilitados y registrarse datos de observabilidad.

La integración no elimina las diferencias entre proveedores: los esquemas de solicitud, modelos, parámetros y funcionalidades disponibles pueden variar. AI Gateway ofrece interfaces de acceso comunes para ciertos flujos, pero hay que verificar qué capacidades soporta cada ruta y proveedor.

## Características

### Acceso a proveedores y modelos

- Enruta solicitudes a Workers AI y a proveedores de modelos de terceros admitidos.
- Ofrece APIs REST, acceso desde Workers Bindings y rutas compatibles con APIs de proveedor para los flujos documentados.
- Permite usar un gateway para administrar políticas y observar llamadas de distintos modelos o proveedores.
- La lista de proveedores y modelos cambia con el tiempo: consultar el catálogo oficial antes de elegir una integración.

### Observabilidad

- Registra solicitudes y respuestas según la configuración de logging.
- Proporciona analítica para solicitudes, latencia, errores, uso de tokens y costos cuando esos datos están disponibles.
- Permite inspeccionar eventos del gateway desde el panel y conectar flujos de logs con herramientas externas mediante integraciones disponibles, como Logpush u OpenTelemetry.
- El logging puede incluir información sensible enviada en prompts o respuestas; definir explícitamente qué se conserva, quién puede consultarlo y durante cuánto tiempo.

### Zero Data Retention (ZDR)

**Zero Data Retention (ZDR)** es una opción de privacidad de **Unified Billing**: el tráfico que usa credenciales administradas por Cloudflare se enruta a endpoints de proveedores configurados para no retener los prompts ni las respuestas.

Su alcance tiene límites importantes:

- ZDR aplica únicamente a solicitudes de Unified Billing que usan credenciales administradas por Cloudflare.
- No aplica a solicitudes que usan **BYOK** (Bring Your Own Key) ni a otros tipos de solicitudes de AI Gateway.
- ZDR no desactiva el logging de solicitudes y respuestas que pueda realizar AI Gateway. Los ajustes de logging deben configurarse por separado.
- La disponibilidad de ZDR depende del modelo; comprobar el catálogo de modelos para confirmar su compatibilidad.

Por tanto, ZDR se refiere a la retención de datos por el endpoint del proveedor en este flujo específico; no equivale a que ningún componente del sistema registre o procese los datos. Revisar también la política de logs del gateway y los requisitos de privacidad de la aplicación. [Documentación de Unified Billing y ZDR](https://developers.cloudflare.com/ai-gateway/features/unified-billing/)

### Rendimiento y control de costos

- **Caching:** puede reutilizar resultados para solicitudes coincidentes, reducir llamadas repetidas al proveedor y disminuir latencia y consumo. No habilitarlo para respuestas personalizadas o datos sensibles sin entender cómo se construye la clave y el alcance de la caché.
- **Rate limiting:** limita la frecuencia de solicitudes que pasan por el gateway.
- **Retries:** reintenta solicitudes fallidas según la configuración. Debe considerarse la idempotencia y la posibilidad de cargos repetidos del proveedor.
- **Spend limits (beta, según documentación consultada):** define límites de gasto con reglas basadas en dimensiones como proveedor, modelo o metadatos personalizados.
- **Dynamic routing y Auto Router (beta, según disponibilidad):** opciones para enrutar entre modelos o proveedores con políticas específicas. Evaluar comportamiento, compatibilidad y condiciones de beta antes de depender de ellos.

### Controles de seguridad y datos

- **Guardrails (beta, según documentación consultada):** evalúa prompts y respuestas frente a categorías de seguridad, con acciones de señalización o bloqueo.
- **Data Loss Prevention, DLP (beta, según documentación consultada):** puede identificar y controlar datos sensibles según las capacidades y perfiles disponibles.
- Autenticación de gateway y almacenamiento de claves de proveedor (BYOK) están disponibles en los flujos documentados. Su soporte y estado pueden variar; gestionar tokens con permisos mínimos y evitar incluirlos en clientes públicos.

## Formas de integración

### REST API

El endpoint REST de AI Gateway recibe el identificador de cuenta y el del gateway en la ruta. La solicitud se autentica con los mecanismos requeridos por el gateway y el proveedor configurado. Una forma ilustrativa de llamada es:

```bash
curl "https://gateway.ai.cloudflare.com/v1/$ACCOUNT_ID/$GATEWAY_ID/openai/chat/completions" \
  --header "Authorization: Bearer $OPENAI_API_KEY" \
  --header "Content-Type: application/json" \
  --data '{
    "model": "gpt-4o-mini",
    "messages": [
      {"role": "user", "content": "Resume qué hace AI Gateway."}
    ]
  }'
```

El segmento del proveedor y la forma exacta del endpoint deben corresponder a la guía actual de ese proveedor. No copiar nombres de modelos ni rutas de ejemplo sin verificar su vigencia.

### Cloudflare Workers Binding

Una aplicación que ya corre en Workers puede usar el binding de AI para llamar a Workers AI o a proveedores externos a través de AI Gateway, cuando la configuración de cuenta y gateway lo permita. Este patrón evita que el backend tenga que construir manualmente la URL del gateway y facilita usar bindings y configuración de Workers.

```ts
export default {
  async fetch(_request, env): Promise<Response> {
    const result = await env.AI.run(
      "@cf/meta/llama-3.3-70b-instruct-fp8-fast",
      { messages: [{ role: "user", content: "Hola" }] },
      { gateway: { id: "mi-gateway" } },
    );

    return Response.json(result);
  },
} satisfies ExportedHandler;
```

La sintaxis y los modelos disponibles dependen de la versión y del tipo de binding utilizado; consultar la documentación de Workers Bindings antes de implementar.

## Guardrails

### Qué son y dónde actúan

Guardrails es una función de AI Gateway que inspecciona interacciones en el trayecto entre la aplicación y el modelo. Puede evaluar el **prompt entrante** antes de enviarlo al modelo y la **respuesta del modelo** antes de devolverla a la aplicación. Así se pueden aplicar criterios comunes aunque el tráfico se dirija a distintos proveedores compatibles.

Guardrails no garantiza que un modelo sea seguro ni sustituye autorización, validación semántica, controles de abuso, revisión humana o políticas propias del producto. Es una capa de moderación configurable; su eficacia depende de las categorías seleccionadas, el tipo de modelo, el flujo y las limitaciones documentadas.

### Acciones disponibles

Para cada categoría, se puede configurar una acción distinta para prompts y respuestas:

| Acción | Comportamiento general |
| --- | --- |
| **Flag** | Permite que el flujo continúe y registra el hallazgo para inspección en logs. Útil para observar resultados antes de imponer bloqueos. |
| **Ignore** | No aplica una acción de Guardrails a esa categoría. |
| **Block** | Detiene la solicitud si la evaluación encuentra una infracción, según la parte del flujo (prompt o respuesta). |

La API acepta `FLAG` y `BLOCK`; para dejar una categoría en Ignore se omite esa categoría en la configuración. Las solicitudes bloqueadas generan errores estructurados diferenciados: código `2016` para bloqueo de prompt y `2017` para bloqueo de respuesta. La aplicación debe capturarlos y devolver un resultado controlado al usuario.

### Categorías evaluadas

La documentación consultada describe categorías `S1` a `S13`, más `P1` para inyección de prompt. Los códigos pueden usarse al configurar acciones y aparecen en los resultados de evaluación.

| Código | Categoría |
| --- | --- |
| `P1` | Inyección de prompt |
| `S1` | Delitos violentos |
| `S2` | Delitos no violentos |
| `S3` | Delitos relacionados con sexo |
| `S4` | Explotación sexual infantil |
| `S5` | Difamación |
| `S6` | Asesoría especializada |
| `S7` | Privacidad |
| `S8` | Propiedad intelectual |
| `S9` | Armas indiscriminadas |
| `S10` | Odio |
| `S11` | Suicidio y autolesión |
| `S12` | Contenido sexual |
| `S13` | Elecciones |

La documentación indica que `S14` (abuso del intérprete de código) no se evalúa actualmente mediante Guardrails. La lista y las descripciones pueden cambiar; comprobar la página de categorías vigente.

### Modelo y costo de la evaluación

Según la documentación vigente consultada, Guardrails utiliza modelos de Workers AI para las evaluaciones: Llama Guard para categorías de seguridad y un detector dedicado para prompt injection. Habilitarlo genera uso adicional de Workers AI y añade latencia; Cloudflare indica que una evaluación típica puede agregar alrededor de 500 ms, con variación según tamaño y contenido. Contenido largo puede dividirse en segmentos y aumentar el tiempo de procesamiento.

La documentación indica compatibilidad de clasificación de contenido en inglés, francés, alemán, hindi, italiano, portugués, español y tailandés. Probar con los idiomas, formatos y casos reales de la aplicación; soporte declarado no significa detección perfecta.

### Modelos y tipos de solicitud

- En modelos de generación de texto se evalúan tanto el prompt como la respuesta.
- Para modelos de embeddings se evalúa el prompt; el vector resultante no se modera como texto.
- Cuando AI Gateway no puede determinar el tipo de modelo, se puede evaluar el prompt y omitir la evaluación de respuesta.
- Guardrails no ofrece enforcement completo en solicitudes de streaming. En las rutas de gateway que procesan streaming, la respuesta puede ser almacenada en búfer, evaluada y entregada como un único resultado no transmitido por streaming. En determinadas rutas REST de Cloudflare, la respuesta streaming puede entregarse sin bloquearse aunque se evalúe y registre. Confirmar el comportamiento de la ruta concreta antes de habilitar streaming.

### Configurar desde el panel

1. En el panel de Cloudflare, seleccionar la cuenta y abrir **AI > AI Gateway**.
2. Seleccionar el gateway y entrar en **Guardrails**.
3. Activar la función.
4. Elegir las categorías y definir acciones separadas para prompts y respuestas.
5. Guardar los cambios.
6. Revisar los logs y ajustar políticas antes de ampliar el uso de `Block`.

### Configurar mediante API

La configuración se puede actualizar mediante el endpoint de administración de gateways. Ejemplo de política:

```json
{
  "guardrails": {
    "prompt": {
      "P1": "BLOCK",
      "S9": "FLAG"
    },
    "response": {
      "S9": "BLOCK"
    }
  }
}
```

El endpoint de actualización usa `PUT`. Antes de actualizar, obtener la configuración existente y conservar sus otros campos: omitir `guardrails` en el cuerpo puede desactivar la función y eliminar su configuración. La API requiere un token con permiso **AI Gateway Write**.

### Fallos de evaluación y disponibilidad

La documentación describe un comportamiento distinto según la acción: si una categoría está en `Block` y AI Gateway no puede obtener respuesta de Workers AI, la solicitud se bloquea; si la acción es `Flag` y la evaluación no está disponible, la solicitud continúa sin evaluación. Diseñar los mensajes de error y monitoreo conforme a estas opciones fail-closed/fail-open.

## Casos de uso

- Centralizar llamadas a modelos de distintos proveedores en una sola capa de observabilidad.
- Medir consumo, latencia, errores y costos por aplicación, proveedor o modelo.
- Reducir solicitudes repetidas con caché, cuando los resultados puedan compartirse con seguridad.
- Establecer límites de uso y presupuestos para servicios de IA.
- Aplicar controles de contenido a prompts y respuestas a través de Guardrails.
- Inspeccionar o controlar datos sensibles enviados a servicios de modelos con DLP, cuando la función esté disponible y configurada.
- Facilitar el cambio de proveedor o la evaluación comparativa de modelos, teniendo en cuenta diferencias de APIs y comportamiento.
- Enrutar modelos desde aplicaciones desplegadas en Workers mediante bindings.

## Buenas prácticas

1. **Mantener las llamadas a modelos en el backend.** No exponer claves de proveedores en aplicaciones web o móviles; usar un backend controlado y credenciales con permisos mínimos.
2. **Activar logging de forma deliberada.** Definir retención, acceso y enmascaramiento; prompts y respuestas pueden contener datos personales, secretos o información regulada.
3. **Empezar Guardrails en modo Flag** para observar falsos positivos y negativos con datos de prueba representativos; después activar Block en categorías justificadas.
4. **Probar por separado prompts y respuestas.** Una política apropiada para entrada no siempre es apropiada para salida.
5. **Incluir errores de Guardrails en el diseño de la aplicación.** Manejar al menos bloqueos de prompt y de respuesta, fallos de proveedor y fallos del servicio de evaluación.
6. **Revisar streaming y modalidad del modelo.** Confirmar si se soporta evaluación de respuesta y si el gateway bufferiza, antes de prometer streaming al usuario.
7. **No usar caché indiscriminadamente.** Validar aislamiento entre usuarios, datos personalizados, expiración e invalidación; desactivarla para resultados que no puedan reutilizarse de forma segura.
8. **Configurar reintentos con idempotencia y límites.** Una llamada repetida puede generar más costo o repetir efectos en herramientas externas.
9. **Separar gateways por entorno o propósito** cuando ayude a mantener políticas, credenciales, logs y límites independientes.
10. **Tratar funciones beta como dependencias sujetas a cambio.** Revisar limitaciones, versiones y alternativas antes de adoptarlas para flujos críticos.
11. **Evaluar latencia y costos completos.** Incluir la evaluación de Guardrails, llamadas a Workers AI, posibles reintentos y uso del proveedor externo.
12. **Usar controles por capas.** Complementar Guardrails con validación de entrada y salida, permisos de herramientas, prevención de exfiltración, límites de tasa y revisión humana cuando el riesgo lo requiera.

## Referencias oficiales

- [Documentación general de AI Gateway](https://developers.cloudflare.com/ai-gateway/)
- [REST API de AI Gateway](https://developers.cloudflare.com/ai-gateway/usage/rest-api/)
- [Workers Bindings](https://developers.cloudflare.com/ai-gateway/usage/worker-binding-methods/)
- [Características de AI Gateway](https://developers.cloudflare.com/ai-gateway/features/)
- [Observabilidad y logs](https://developers.cloudflare.com/ai-gateway/observability/)
- [Guardrails: descripción](https://developers.cloudflare.com/ai-gateway/features/guardrails/)
- [Configurar Guardrails](https://developers.cloudflare.com/ai-gateway/features/guardrails/set-up-guardrail/)
- [Tipos de modelos soportados por Guardrails](https://developers.cloudflare.com/ai-gateway/features/guardrails/supported-model-types/)
- [Consideraciones de uso, categorías, streaming y latencia](https://developers.cloudflare.com/ai-gateway/features/guardrails/usage-considerations/)
- [Límites de AI Gateway](https://developers.cloudflare.com/ai-gateway/platform/limits/)
- [Precios de AI Gateway](https://developers.cloudflare.com/ai-gateway/platform/pricing/)
