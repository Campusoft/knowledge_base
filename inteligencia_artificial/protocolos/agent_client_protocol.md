# Agent Client Protocol (ACP)

> Documento revisado: 2026-09-26

## Descripción

El **Agent Client Protocol (ACP)** es un protocolo abierto para comunicar clientes de desarrollo —como IDEs, editores, terminales o plataformas de agentes— con agentes de programación basados en modelos de IA.

ACP estandariza la forma en que un cliente inicia un agente, crea sesiones, envía instrucciones, recibe respuestas, muestra eventos de herramientas y gestiona capacidades como archivos, terminales, aprobaciones y cambios de código. Su objetivo es que un cliente pueda trabajar con distintos agentes sin implementar una integración completamente diferente para cada uno.

ACP no es un modelo de lenguaje, un proveedor de modelos ni una API de inferencia. GPT, Claude, Gemini y otros modelos se utilizan a través del agente CLI que los integra. ACP define la comunicación entre el cliente y ese agente.

## Arquitectura conceptual

```text
Cliente ACP
  ├── IDE, editor, terminal o plataforma multiagente
  └── inicia y controla
        ↓ JSON-RPC sobre stdio
Agente ACP
  ├── Codex CLI
  ├── Claude Code mediante adaptador
  ├── Gemini CLI
  ├── OpenCode
  └── otros agentes compatibles
        ↓
Proveedor y modelo
  ├── OpenAI / GPT
  ├── Anthropic / Claude
  ├── Google / Gemini
  └── otros modelos o servicios
```

En el flujo habitual, el cliente inicia el ejecutable del agente como proceso hijo y ambos intercambian mensajes JSON-RPC delimitados por líneas mediante `stdin` y `stdout`. Los logs deben enviarse por `stderr` para no interferir con el canal ACP.

## Características principales

- Protocolo común entre clientes de desarrollo y agentes de código.
- Comunicación basada en JSON-RPC.
- Transporte habitual mediante `stdin`/`stdout`.
- Inicio y administración de sesiones de agente.
- Envío de prompts, texto, imágenes, recursos embebidos y referencias de archivos, cuando el agente lo admite.
- Notificación de respuestas, razonamiento, planes, cambios de archivos, salida de terminal y llamadas a herramientas.
- Solicitudes de aprobación para acciones que requieren autorización.
- Negociación de capacidades entre cliente y agente.
- Selección de modelo, modo de operación o nivel de razonamiento cuando el agente lo expone.
- Soporte para agentes locales y, dependiendo del cliente, escenarios remotos.
- Posibilidad de utilizar suscripciones, API keys o mecanismos de autenticación propios de cada agente.
- Registro público de agentes que implementan ACP.

## Agentes y CLI compatibles

La compatibilidad puede ser nativa o proporcionarse mediante un adaptador. La siguiente lista debe mantenerse diferenciando ambas situaciones:

| Agente o CLI | Organización / modelos habituales | Forma de integración ACP | Comando o paquete habitual | Observaciones |
|---|---|---|---|---|
| **Codex CLI** | OpenAI / GPT y modelos Codex | Adaptador ACP | `codex-acp` o `@agentclientprotocol/codex-acp` | `codex-acp` traduce ACP a operaciones del Codex App Server. La autenticación puede usar ChatGPT, API key u otros métodos soportados por Codex. |
| **Claude Code** | Anthropic / Claude | Adaptador ACP | `claude-agent-acp` o `@agentclientprotocol/claude-agent-acp` | El adaptador permite exponer Claude Code a clientes ACP; la autenticación pertenece a Claude Code/Anthropic. |
| **Gemini CLI** | Google / Gemini | Soporte ACP del propio CLI | `gemini --acp` | Gemini CLI puede ejecutarse en modo ACP; el acceso depende de la autenticación y modelos disponibles en Google. |
| **OpenCode** | OpenCode / múltiples proveedores y modelos | Soporte ACP nativo | `opencode acp` | OpenCode inicia un servidor ACP privado para ese proceso y permite seleccionar modelos con formato `provider/model`. |
| **Mistral Vibe** | Mistral / modelos Mistral | Implementación registrada en ACP | CLI de Mistral Vibe | Debe verificarse el comando de instalación y el método de autenticación de la versión utilizada. |
| **Qwen Code** | Alibaba / Qwen | Implementación registrada en ACP | CLI de Qwen Code | La disponibilidad de funciones puede variar según la versión del CLI y del cliente ACP. |
| **Auggie CLI** | Augment Code | Implementación registrada en ACP | CLI de Augment | Requiere validar capacidades y autenticación en la versión instalada. |
| **Pi** | Pi Coding Agent | Adaptador ACP | `pi-acp` | Se utiliza mediante un adaptador específico; sus capacidades no necesariamente coinciden con las de otros agentes. |
| **Otros agentes** | Diversos proveedores | Registro ACP o adaptador comunitario | Según el agente | El registro oficial cambia con frecuencia; conviene revisar el registro y la documentación del agente antes de automatizarlo. |

### Aclaración sobre GPT, Claude y Gemini

ACP no “permite utilizar GPT” o “permite utilizar Claude” directamente como si fueran modelos intercambiables. La relación correcta es:

- **GPT** se utiliza a través de un agente como Codex CLI, que puede exponerse mediante `codex-acp`.
- **Claude** se utiliza a través de Claude Code, que puede exponerse mediante `claude-agent-acp`.
- **Gemini** se utiliza a través de Gemini CLI, que puede ejecutarse con `--acp`.
- **Otros modelos** pueden estar disponibles a través de OpenCode u otro agente compatible, según sus proveedores y configuración.

Por ello, la compatibilidad debe documentarse en tres niveles: cliente ACP, agente/CLI y proveedor/modelo.

## Clientes que pueden utilizar agentes ACP

Un cliente compatible con ACP puede iniciar agentes y trabajar con ellos desde una interfaz común. Entre los escenarios y clientes documentados o integrables se encuentran:

- IDEs y editores con soporte ACP, como Zed y otros clientes compatibles.
- Plataformas multiagente o de orquestación que lanzan agentes ACP como procesos hijos.
- Herramientas de terminal que proporcionan un adaptador o registro de agentes.
- Integraciones de editores, plugins y aplicaciones que implementen el protocolo.
- Entornos como Kiro Crew cuando la versión instalada registra y habilita el backend ACP correspondiente.

La disponibilidad de un agente dentro de un cliente no implica que todas sus capacidades estén soportadas. Se debe revisar la negociación de capacidades, autenticación, sesiones, subagentes, herramientas y permisos.

## Casos de uso

### Usar varios agentes desde un mismo IDE

Un desarrollador puede cambiar entre Codex, Claude Code, Gemini CLI y OpenCode sin aprender una integración distinta para cada uno.

### Comparar modelos o proveedores

El mismo cliente puede lanzar agentes con diferentes proveedores para comparar soluciones, calidad de código, velocidad, costo y comportamiento.

### Orquestar agentes especializados

Una plataforma puede iniciar un agente para implementar, otro para revisar, otro para ejecutar pruebas y otro para analizar seguridad, siempre que el cliente gestione correctamente sus sesiones y permisos.

### Integrar agentes CLI existentes

Un cliente puede reutilizar una herramienta de terminal ya instalada en lugar de implementar un agente completo desde cero.

### Automatizar tareas de desarrollo

ACP permite integrar agentes en flujos de revisión, generación de pruebas, corrección de errores, documentación y mantenimiento de repositorios.

### Ejecutar agentes con distintas formas de autenticación

Cada agente puede conservar su propio flujo: inicio de sesión, suscripción, API key, variable de entorno o proveedor compatible.

### Construir un cliente propio

Un equipo puede desarrollar un editor, dashboard, terminal o plataforma multiagente que use una interfaz común para agentes ACP.

## Limitaciones y consideraciones

- ACP estandariza la comunicación, no la calidad ni el comportamiento del modelo.
- Dos agentes pueden anunciar capacidades diferentes aunque ambos sean compatibles con ACP.
- Un adaptador puede tener retraso respecto a la versión del CLI que envuelve.
- La autenticación no es necesariamente uniforme: cada agente mantiene sus propios requisitos.
- Las suscripciones no siempre son equivalentes a API keys; el cliente debe respetar las condiciones del proveedor.
- El cliente debe manejar permisos, sandbox, aprobaciones y secretos de cada agente.
- Un cliente ACP no garantiza que los cambios de varios agentes estén aislados o que puedan fusionarse sin conflictos.
- El transporte por `stdin`/`stdout` exige separar estrictamente mensajes del protocolo y logs de diagnóstico.
- Las funciones de imágenes, subagentes, MCP, terminal, reanudación y sesiones pueden ser opcionales.
- La lista de agentes compatibles evoluciona rápidamente y debe verificarse contra el registro oficial.

## Recomendaciones

1. Distinguir siempre entre soporte **nativo**, soporte mediante **adaptador** y soporte **experimental**.
2. Registrar el agente, versión, adaptador, modelo y proveedor usados en cada flujo.
3. Consultar las capacidades negociadas durante el handshake antes de usar terminales, archivos, imágenes o subagentes.
4. Mantener las API keys fuera de archivos versionados, prompts, logs y configuraciones compartidas.
5. Usar permisos mínimos y aprobación humana para acciones destructivas, publicaciones y cambios en producción.
6. Fijar versiones de adaptadores y agentes en automatizaciones o CI.
7. Probar cada agente individualmente antes de incorporarlo a una orquestación multiagente.
8. No asumir que un cliente que funciona con Codex tendrá exactamente el mismo comportamiento con Claude, Gemini u OpenCode.
9. Consultar el [registro oficial de agentes ACP](https://github.com/agentclientprotocol/registry) para verificar disponibilidad y autenticación.
10. Documentar si el proveedor se autentica mediante cuenta, suscripción, API key o gateway compatible.

## Relación con MCP

ACP y MCP resuelven problemas diferentes:

- **ACP** conecta un cliente con un agente de programación y controla sesiones, prompts, eventos, herramientas y cambios.
- **MCP** conecta un modelo o agente con herramientas, recursos y fuentes de contexto externas.

Un agente ACP puede utilizar MCP internamente. Por ejemplo, un cliente puede iniciar OpenCode mediante ACP y OpenCode puede utilizar servidores MCP para acceder a documentación, bases de datos o APIs.

## Resumen

ACP proporciona una interfaz común para ejecutar agentes de programación desde IDEs, editores, terminales y plataformas multiagente. Codex, Claude Code, Gemini CLI y OpenCode pueden integrarse mediante implementaciones nativas o adaptadores, pero cada agente conserva su propio modelo de autenticación, permisos y capacidades. La compatibilidad debe evaluarse siempre considerando el conjunto cliente–agente–proveedor.

## Fuentes

- [Registro oficial de agentes ACP](https://github.com/agentclientprotocol/registry)
- [Codex ACP](https://github.com/zed-industries/codex-acp)
- [Claude Agent ACP](https://github.com/agentclientprotocol/claude-agent-acp)
- [OpenCode: soporte ACP](https://opencode.ai/v2/docs/cli/acp/)
- [Agent Client Protocol](https://agentclientprotocol.com/)
