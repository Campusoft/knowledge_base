# OpenCode V2

> Documento revisado: 2026-10-22
> Versión documentada: OpenCode V2
> Licencia: MIT
> Documentación oficial: <https://opencode.ai/v2/docs/>

## Descripción

OpenCode es un agente de programación de código abierto con interfaz de terminal (TUI), aplicación de escritorio y aplicación web. Interpreta instrucciones en lenguaje natural, explora el repositorio, edita archivos, ejecuta comandos y tests, y mantiene sesiones de trabajo con distintos modelos y proveedores.

Su rasgo diferencial es que es **agnóstico al proveedor**: no está atado a un modelo concreto. Puede usar modelos de Anthropic, OpenAI, Google, OpenRouter, Amazon Bedrock, Azure, GitHub Copilot, modelos locales con Ollama, LM Studio o vLLM, y APIs compatibles con OpenAI.

La versión V2 introduce una arquitectura cliente-servidor con un servidor en segundo plano compartido, una API HTTP, SDK embebido y un sistema de configuración más estructurado.

## Arquitectura

### Cliente-servidor

OpenCode separa la interfaz del motor que ejecuta el trabajo:

```
┌──────────────────────┐
│ Cliente              │
│ TUI / desktop / web  │
│ CLI / SDK / API      │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────────────────┐
│ Servidor OpenCode en segundo plano   │
│                                      │
│ - Sesiones                           │
│ - Configuración                      │
│ - Proveedores y modelos              │
│ - Plugins, skills y comandos         │
│ - Permisos y políticas               │
│ - Ejecución de herramientas          │
└──────────┬───────────────────────────┘
           │
           ▼
┌──────────────────────┐
│ Proveedor del modelo │
│ API o modelo local   │
└──────────────────────┘
```

Por defecto, OpenCode descubre o inicia **un servidor compartido por usuario**. Todos los clientes locales se conectan a él. El servidor es el dueño del estado: sesiones, configuración, plugins, permisos y ejecución de herramientas.

Opciones útiles:

```bash
# Servidor privado para una sola ejecución
opencode --standalone

# Conectarse a un servidor concreto
opencode --server http://localhost:4096

# Gestionar el servicio compartido
opencode service status
opencode service restart
```

### Componentes

| Componente | Función |
|------------|---------|
| **Cliente TUI** | Interfaz principal en la terminal |
| **Desktop** | Aplicación nativa con interfaz gráfica |
| **Web** | Interfaz accesible desde navegador |
| **Servidor** | Sesiones, estado, permisos y ejecución |
| **API HTTP** | Contrato para clientes e integraciones |
| **SDK** | Incrustar OpenCode en una aplicación |
| **CLI** | `run`, `mini`, `pair`, `api`, `service`, etc. |
| **Proveedores** | Conexión con APIs y modelos locales |

### Flujo de una orden

1. El cliente envía un prompt a una sesión.
2. El servidor construye el contexto: instrucciones, archivos, historial, skills y referencias.
3. El modelo elige herramientas: leer, buscar, editar, ejecutar shell, consultar MCP, etc.
4. El motor de permisos decide si cada acción se permite, se pregunta o se deniega.
5. Las herramientas se ejecutan y devuelven resultados al modelo.
6. El ciclo continúa hasta terminar; la sesión conserva el historial.
7. Los cambios pueden revisarse, revertirse o compararse mediante snapshots y Git.

### Almacenamiento y configuración

| Ruta | Contenido |
|------|-----------|
| `~/.config/opencode/opencode.json(c)` | Configuración global del usuario |
| `~/.config/opencode/` | Agentes, comandos, skills y plugins globales |
| `opencode.json(c)` o `.opencode/opencode.json(c)` | Configuración del proyecto |
| `/etc/opencode/` | Configuración administrada del sistema |
| `~/.local/share/opencode/` | Datos, base de datos y sesiones |
| `~/.local/share/opencode/log/opencode.log` | Logs |
| `~/.local/state/opencode/service.json` | Registro del servicio |
| `~/.config/opencode/service.json` | Configuración del servicio |

Precedencia de configuración, de menor a mayor:

1. `~/.config/opencode/opencode.json(c)`
2. `opencode.json(c)` del proyecto
3. `opencode.json(c)` de paquetes anidados
4. `/etc/opencode/opencode.json(c)`

Los archivos dentro de `.opencode/` tienen prioridad sobre los archivos directos del mismo nivel.

## Características

### Proveedores y modelos

- Catálogo basado en [models.dev](https://models.dev).
- Conexión mediante `/connect` y selección con `/models`.
- Proveedores integrados: Anthropic, OpenAI, Google, Azure, Amazon Bedrock, Google Vertex, OpenRouter, GitHub Copilot, xAI y otros.
- APIs compatibles con OpenAI mediante un proveedor personalizado.
- Modelos locales: Ollama, LM Studio y vLLM con descubrimiento automático.
- Variantes por modelo para ajustar esfuerzo de razonamiento (`#low`, `#high`, etc.).
- Modelo por defecto global, por agente, por comando o por ejecución (`--model`).
- Selección de modelo por sesión sin reescribir la configuración.

### Agentes

OpenCode incluye agentes integrados:

| Agente | Modo | Uso |
|--------|------|-----|
| **Build** | Primario | Implementación y cambios de código |
| **Plan** | Primario | Explorar y planificar sin editar archivos normales |
| **General** | Subagente | Investigación y tareas multi-paso |
| **Explore** | Subagente | Buscar y leer código sin modificar |

Se pueden definir agentes propios en Markdown (`.opencode/agents/*.md`) o en JSONC. Cada agente puede tener:

- modelo y variante,
- prompt de sistema,
- permisos propios,
- límite de pasos,
- modo `primary`, `subagent` o `all`,
- color y visibilidad.

Los subagentes permiten dividir el trabajo: un agente principal delega exploración o revisión en agentes especializados.

### Permisos y políticas

Las reglas son ordenadas y la **última coincidencia gana**:

```jsonc
{
  "permissions": [
    { "action": "shell", "resource": "*", "effect": "ask" },
    { "action": "shell", "resource": "git status *", "effect": "allow" },
    { "action": "shell", "resource": "git diff *", "effect": "allow" },
    { "action": "shell", "resource": "git push *", "effect": "deny" }
  ]
}
```

- `allow`: se ejecuta sin preguntar.
- `ask`: espera aprobación del cliente.
- `deny`: se bloquea.

Acciones habituales: `read`, `edit`, `glob`, `grep`, `shell`, `subagent`, `skill`, `webfetch`, `websearch`, `external_directory` y herramientas MCP.

Las **políticas** (`policies`) pueden imponer un `deny` definitivo que ni el proyecto ni las aprobaciones guardadas pueden saltarse. Son útiles para entornos administrados.

### Herramientas

| Herramienta | Función |
|-------------|---------|
| `read` | Leer archivos o listar directorios |
| `glob` | Buscar rutas por patrón |
| `grep` | Buscar contenido por expresión regular |
| `edit` | Reemplazar texto exacto en un archivo |
| `write` | Crear o reemplazar un archivo |
| `patch` | Aplicar cambios en varios archivos |
| `shell` | Ejecutar comandos en la shell del usuario |
| `webfetch` | Descargar una URL |
| `websearch` | Buscar en la web |
| `question` | Preguntar al usuario |
| `skill` | Cargar una skill |
| `subagent` | Lanzar un agente hijo |
| `execute` | Ejecutar JavaScript en Code Mode y combinar herramientas |
| `browser` | Controlar el navegador del desktop (opcional) |

La shell se ejecuta con los permisos del usuario del sistema. No es un sandbox por sí misma.

### Extensiones

- **MCP:** servidores locales o remotos, con OAuth y timeouts.
- **Skills:** instrucciones y recursos reutilizables para tareas concretas.
- **Comandos:** plantillas de prompt reutilizables (`/comando`).
- **Plugins:** hooks, herramientas y transformaciones; la API de V2 es distinta de V1.
- **Instrucciones:** `AGENTS.md`, archivos `CONTRIBUTING.md` y rutas adicionales.
- **Referencias:** directorios o repositorios externos disponibles por nombre.
- **Formatters:** formateo automático tras editar.

### Sesiones y estado

- Sesiones persistentes en el servidor compartido.
- Renombrar y mover sesiones entre directorios.
- **Snapshots** para deshacer y revertir cambios.
- **Compactación** automática del contexto en sesiones largas.
- **Warming** opcional para mantener sesiones activas.
- **Worktrees** de Git para trabajar en paralelo sin pisar el árbol principal.

### Interfaces y automatización

| Interfaz | Uso |
|----------|-----|
| `opencode` | TUI completa |
| `opencode mini` | Interfaz interactiva mínima |
| `opencode run "..."` | Ejecución no interactiva |
| `opencode pair` | Interfaz web temporal con enlace de un solo uso |
| `opencode api` | Llamadas a la API HTTP local |
| `opencode service` | Gestión del servidor en segundo plano |
| SDK `@opencode/sdk` | OpenCode embebido en una aplicación |
| ACP | Integración con clientes y editores compatibles |

## Instalación

OpenCode V2 se puede instalar con el script oficial, npm, Bun/pnpm o un binario standalone. En Linux Ubuntu, el instalador deja el binario en `~/.opencode/bin/opencode` y añade esa ruta al `PATH` del shell.

```bash
curl -fsSL https://opencode.ai/v2/install | bash
opencode --version
```

Guía completa, requisitos, solución de problemas de `PATH` y configuración inicial:

- [Instalación de OpenCode V2](opencode-v2-instalacion.md)

## Recomendaciones

1. **Empieza con permisos interactivos.** Deja `shell` en `ask` y permite solo comandos de lectura habituales. Añade `allow` a medida que confíes en el flujo.
2. **Nunca ejecutes OpenCode como root.** La shell hereda los permisos del usuario; un error puede afectar a todo el sistema.
3. **Deniega operaciones peligrosas.** `git push`, `sudo`, `rm -rf`, despliegues y cambios en producción deben requerir aprobación o estar bloqueados.
4. **Usa una rama o worktree por tarea.** Facilita la revisión y evita que dos agentes se pisen.
5. **Define el alcance en el prompt.** Indica archivos, criterios de aceptación y tests que deben pasar.
6. **Revisa siempre el diff.** Los snapshots y Git ayudan, pero la revisión humana es la última barrera.
7. **Mantén el contexto pequeño.** Usa `plan` para explorar, `build` para implementar y subagentes para investigación.
8. **Configura el modelo por tarea.** Un modelo potente para diseño y uno más económico para tareas mecánicas.
9. **Gestiona secretos con variables o archivos.** Nunca en `opencode.json`, prompts o historial.
10. **Para uso remoto, ejecuta dentro de tmux.** Así la sesión sobrevive a cortes de SSH.
11. **Para automatización, usa `opencode run`.** Revisa antes cómo se resuelven los permisos en modo no interactivo.
12. **Vigila coste y contexto.** La compactación automática ayuda, pero no sustituye una buena división de tareas.
13. **Fija versiones en entornos críticos.** V2 evoluciona rápido; prueba actualizaciones antes de aplicarlas.
14. **No expongas el servidor ni la interfaz web a Internet.** Usa SSH, túnel o VPN.

## Casos de uso

### Implementar una funcionalidad desde un plan

```bash
opencode run "Implementa plans/001-login.md. Ejecuta los tests y no hagas commit hasta que pasen."
```

### Corrección de bugs con verificación

```text
Reproduce el fallo del test tests/parser.test.ts, encuentra la causa,
aplica el cambio mínimo y ejecuta la suite completa.
```

### Revisión de código

Un subagente `reviewer` con `edit` y `shell` denegados puede revisar el diff y reportar hallazgos sin modificar nada.

### Exploración y onboarding

El agente `explore` mapea la arquitectura, encuentra los puntos de entrada y explica el flujo sin editar archivos.

### Refactorizaciones y migraciones

Tareas acotadas con tests que validan cada paso. Conviene dividir en varios planes y ramas.

### Generación de tests

Pedir tests para un módulo concreto, ejecutarlos y revisar que fallan antes del cambio y pasan después.

### Documentación

Generar o actualizar documentación a partir del código, con revisión humana del contenido.

### Automatización y CI

`opencode run` permite integrar el agente en scripts, pipelines o tareas programadas. Para trabajos largos, combínalo con tmux, logs y un runner con estado.

### Trabajo remoto en un VPS

Ejecutar OpenCode dentro de tmux y conectarse por SSH desde tu máquina. Ver [tmux + OpenCode](../multi-agente/agentes-remotos/tmux-opencode.md).

### Modelos locales y privacidad

Con Ollama, LM Studio o vLLM, el código no sale de tu infraestructura. Requiere hardware suficiente y un modelo con soporte de herramientas.

### Integración con editores

Mediante ACP o la API/SDK, OpenCode puede usarse desde clientes y herramientas propias.

## Limitaciones

- **No es un sandbox.** La shell y las herramientas usan los permisos del usuario; para aislamiento real usa contenedor, VM o un usuario dedicado.
- **Depende del proveedor del modelo.** La calidad, el coste y los límites de contexto varían.
- **El modo no interactivo necesita permisos configurados.** Si una acción resuelve a `ask`, el proceso puede bloquearse.
- **Los modelos gratuitos tienen condiciones y límites.** No asumas disponibilidad o privacidad ilimitadas.
- **La API de plugins cambió en V2.** Los plugins de V1 pueden requerir migración.
- **El proyecto evoluciona rápido.** Comprueba la documentación y fija versiones en entornos estables.
- **El servidor en segundo plano es compartido por usuario.** Un estado corrupto puede afectar a todas las sesiones; usa `opencode service status` y los logs para diagnosticar.
- **No sustituye la revisión humana.** La verificación automática reduce riesgos, no los elimina.

## Ver también

- [Instalación de OpenCode V2](opencode-v2-instalacion.md): requisitos, Linux Ubuntu, solución de `PATH` y configuración inicial.
- [Agentes IA remotos en VPS](../multi-agente/agentes-remotos/README.md): modelo conceptual.
- [tmux + OpenCode en un VPS](../multi-agente/agentes-remotos/tmux-opencode.md): guía práctica.
- [tmux](../../sistema_operativo/linux/tmux.md): sesiones persistentes.
- [Herdr](../multi-agente/herdr.md) y [Orca](../multi-agente/orca.md): orquestación de agentes.
- [Agent Client Protocol](../protocolos/agent_client_protocol.md): integración con editores y clientes.

## Referencias

- Introducción: <https://opencode.ai/v2/docs/>
- Configuración: <https://opencode.ai/v2/docs/config/>
- Agentes: <https://opencode.ai/v2/docs/agents/>
- Modelos: <https://opencode.ai/v2/docs/models/>
- Proveedores: <https://opencode.ai/v2/docs/providers/>
- Permisos: <https://opencode.ai/v2/docs/permissions/>
- Herramientas: <https://opencode.ai/v2/docs/tools/>
- CLI: <https://opencode.ai/v2/docs/cli/>
- API: <https://opencode.ai/v2/docs/api/>
- SDK: <https://opencode.ai/v2/docs/build/sdk/>
- Solución de problemas: <https://opencode.ai/v2/docs/troubleshooting/>
- Repositorio: <https://github.com/anomalyco/opencode>
