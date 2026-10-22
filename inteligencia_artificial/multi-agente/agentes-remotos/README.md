# Agentes IA remotos en VPS

> Documento revisado: 2026-10-22
> Alcance: ejecución de agentes de código (OpenCode, Pi y similares) en un VPS, controlados desde una máquina local.

## Propósito

Este documento describe el modelo conceptual para ejecutar un agente de IA que programa en un servidor remoto y recibir órdenes desde tu equipo. No entra en comandos concretos: para eso está la guía [tmux + OpenCode](tmux-opencode.md).

La idea central es separar cuatro cosas que suelen confundirse:

1. **Dónde se ejecuta el agente** (VPS).
2. **Cómo sobrevive a la desconexión** (tmux, systemd, contenedor).
3. **Cómo lo controlas** (SSH, VPN, API, interfaz web).
4. **Cómo organizas el trabajo** (planes, ramas, tests, revisión).

## Escenario

```
Tu máquina ──SSH/VPN──> VPS ──> agente ──> repositorio
                          │
                          └──HTTPS──> API del modelo
```

- El agente corre en el VPS, no en tu laptop.
- Tu máquina solo abre una terminal remota o envía una orden.
- El repositorio y las herramientas viven en el VPS.
- El modelo puede ser una API externa o un modelo local en el propio servidor.

## Modelo por capas

| Capa | Función | Opciones habituales |
|------|---------|---------------------|
| **Ejecución** | Ejecutar el agente y las herramientas del proyecto | VPS, contenedor, máquina dedicada |
| **Persistencia** | Mantener viva la sesión y los procesos | tmux, systemd, Docker |
| **Acceso** | Conectar tu equipo con el VPS | SSH, Tailscale/WireGuard, túnel, API |
| **Orquestación** | Decidir qué trabajo se hace y en qué orden | Planes Markdown, ramas, runner, cola |
| **Modelo** | Proporcionar capacidad de razonamiento | API del proveedor, modelo local |

Cada capa se puede cambiar sin rehacer las demás. Esto permite empezar simple y mejorar por partes.

## Arquitectura de referencia

```
┌──────────────┐        SSH / VPN        ┌─────────────────────────────┐
│ Tu máquina   │ ──────────────────────> │ VPS                         │
│              │                         │                             │
│  terminal    │ <────────────────────── │  tmux                       │
│  git         │      TUI / salida       │   └─ opencode / pi          │
└──────────────┘                         │       ├─ repositorio        │
                                         │       ├─ planes             │
                                         │       └─ tests              │
                                         └──────────────┬──────────────┘
                                                        │ HTTPS
                                                        ▼
                                                 API del modelo
```

Componentes:

- **VPS:** ejecuta el agente, el repositorio y los tests.
- **tmux:** mantiene la sesión viva si se corta la conexión.
- **Agente:** OpenCode, Pi u otro; interpreta las órdenes y modifica el código.
- **Repositorio:** fuente de verdad del código y de los planes.
- **Transporte:** SSH para terminal; VPN si no quieres exponer el puerto.
- **Proveedor del modelo:** recibe las peticiones del agente.

## Modos de operación

| Modo | Cómo funciona | Cuándo usarlo |
|------|---------------|---------------|
| **Interactivo remoto** | Te conectas por SSH a una sesión tmux y usas la TUI | Trabajo normal, supervisión y decisiones |
| **One-shot** | Envías un prompt y el agente termina | Tareas acotadas y scripts |
| **Lote o cola** | Un runner procesa varios planes | Automatización con revisión posterior |
| **Interfaz web** | El agente expone una UI temporal por un túnel | Comodidad desde el navegador |
| **Orquestador** | Herramientas como Herdr u Orca gestionan varios agentes | Varios repos o agentes en paralelo |

El modo interactivo es el punto de partida recomendado: es el más fácil de entender, depurar y controlar.

## Flujo basado en planes

El agente rinde mejor cuando el trabajo está dividido y verificado. Un plan debe ser pequeño y comprobable.

```markdown
# Plan 001: Autenticación de usuarios

## Objetivo
Implementar login con usuario y contraseña.

## Alcance
- `src/auth/`
- `tests/auth/`

## Criterios de aceptación
- Login correcto devuelve un token.
- Credenciales inválidas devuelven 401.
- Los tests existentes siguen pasando.

## Tests
- `npm test -- auth`

## Fuera de alcance
- Recuperación de contraseña.
- Integración con proveedores externos.
```

Reglas prácticas:

1. **Un plan, una rama.** Evita mezclar cambios y facilita la revisión.
2. **Un plan, una sesión o ventana.** Puedes ver qué agente hizo qué.
3. **Verificación automática.** El plan no termina hasta que pasan los tests.
4. **Revisión humana antes de integrar.** El agente propone; tú apruebas.
5. **Estado explícito.** Lleva un registro de planes pendientes, en curso, hechos y fallidos.

Un plan demasiado grande produce cambios difíciles de revisar y errores en cadena.

## Persistencia y ciclo de vida

Hay que distinguir tres eventos:

| Evento | ¿Sobrevive la sesión? | Solución |
|--------|----------------------|----------|
| Cierras la terminal o se cae SSH | Sí, con tmux | `tmux attach` |
| El agente termina o falla | Depende del proceso | Revisar logs y relanzar |
| El VPS se reinicia | No, por defecto | systemd, contenedor o relanzar |

- **tmux** resuelve la desconexión del cliente, no el reinicio del servidor.
- **systemd** es adecuado para procesos que deben arrancar con el sistema.
- **Docker** añade aislamiento y límites de recursos, a cambio de complejidad.
- Los plugins de persistencia de tmux ayudan, pero no sustituyen a un supervisor.

## Seguridad

El agente ejecuta comandos y modifica archivos. Hay que tratarlo como un usuario con privilegios, no como un simple chat.

- **Usuario dedicado, nunca root.** Con permisos mínimos sobre el repositorio.
- **SSH con clave y sin contraseña.** Desactivar login de root.
- **No exponer la API del agente a Internet.** Usar SSH, túnel o VPN.
- **Secretos fuera del repositorio y de los prompts.** Variables de entorno o gestor de secretos.
- **Permisos del agente configurados.** Revisar qué comandos puede ejecutar sin aprobación.
- **Aislamiento cuando haga falta.** Contenedor, usuario separado o máquina dedicada.
- **Revisión de diffs y tests antes de mergear.**
- **Backups del repositorio y logs de las sesiones.**
- **Límites de tiempo y recursos.** Un agente puede entrar en bucles o consumir mucha CPU.

## Costes y licencias

No hay coste de licencia por las piezas básicas:

- OpenSSH, tmux y los clientes de Tailscale: software libre.
- OpenCode y Pi: software open source.
- Tailscale Personal: gratuito para uso individual.

Los costes reales son:

- **VPS:** alquiler mensual.
- **API del modelo:** consumo por tokens, normalmente la partida más variable.
- **Opcionales:** backups, dominio, más CPU/RAM o GPU para modelos locales.

## Niveles de madurez

| Nivel | Configuración | Cuándo |
|-------|---------------|--------|
| 1 | SSH + tmux + TUI | Empezar y aprender el flujo |
| 2 | + Tailscale/WireGuard | No exponer SSH a Internet |
| 3 | + ramas y planes verificables | Trabajo real con revisión |
| 4 | + `opencode run` y logs | Tareas repetitivas o desatendidas |
| 5 | + Docker o usuario aislado | Mayor seguridad y límites |
| 6 | + runner/cola/orquestador | Varios planes o agentes en paralelo |

No saltes al nivel 6 sin haber validado los anteriores. La complejidad debe resolver un problema real, no anticiparse a él.

## Anti-patrones

- Ejecutar el agente como **root** o con credenciales de producción.
- Exponer la API o la interfaz web del agente directamente a Internet.
- Darle **todos los planes a la vez** en un único prompt.
- Fusionar cambios sin tests ni revisión.
- Ejecutar varios agentes sobre el **mismo árbol de trabajo**.
- Guardar claves API en el repositorio o en el historial de la shell.
- Asumir que tmux sobrevive a un reinicio del VPS.
- No registrar la salida de trabajos largos.

## Ver también

- [tmux + OpenCode en un VPS](tmux-opencode.md): guía práctica paso a paso.
- [tmux](../../../sistema_operativo/linux/tmux.md): referencia general del multiplexor.
- [Herdr](../herdr.md): gestor de workspaces para agentes CLI.
- [Orca](../orca.md): orquestación de agentes, worktrees y ejecución remota por SSH.

## Referencias

- OpenCode V2: <https://opencode.ai/v2/docs/>
- CLI de OpenCode: <https://opencode.ai/v2/docs/cli>
- tmux: <https://github.com/tmux/tmux>
- Tailscale: <https://tailscale.com/>
- Headscale: <https://github.com/juanfont/headscale>
- WireGuard: <https://www.wireguard.com/>
