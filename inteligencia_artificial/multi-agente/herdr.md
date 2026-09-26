# Herdr

> Documento revisado: 2026-09-26  
> Producto documentado: [herdr.dev](https://herdr.dev/)

## Descripción

Herdr es un gestor de espacios de trabajo para terminales que mantiene procesos reales ejecutándose y añade una interfaz para organizar agentes, pestañas, paneles y sesiones. Es adecuado para ejecutar varios agentes CLI en paralelo, observar su estado y volver a conectarse sin perder el proceso cuando se separa el cliente.

Herdr no es un modelo de IA ni un planificador que diseñe automáticamente una solución entre agentes. Su función principal es proporcionar una capa de ejecución, persistencia, supervisión e integración alrededor de los agentes que el usuario instala.

## Modelo de ejecución

Herdr utiliza una arquitectura cliente-servidor:

- El servidor mantiene los paneles, pseudo-terminales y procesos.
- El cliente proporciona la interfaz que se conecta al servidor.
- Un workspace agrupa el trabajo de un repositorio, tarea o investigación.
- Cada workspace contiene tabs.
- Cada tab contiene panes, y cada pane es un terminal real.
- Un agente es un proceso que Herdr detecta dentro de un pane.

Los agentes pueden identificarse como `working`, `blocked`, `done`, `idle` o `unknown`. Las integraciones oficiales aportan información adicional sobre estado, ciclo de vida y reanudación de sesiones.

## Características

- Ejecución de múltiples agentes en panes separados.
- Interfaz mouse-first para crear, redimensionar y cambiar entre paneles.
- Atajos inspirados en tmux, con prefijo predeterminado `Ctrl+B`.
- Workspaces, tabs y panes direccionables desde CLI y API de socket local.
- Separación entre cliente y servidor para desconectar la interfaz sin detener los procesos.
- Sesiones nombradas para separar grupos de panes y estado persistente.
- Detección de agentes a partir de procesos, manifiestos de pantalla e integraciones.
- Integraciones con agentes como Claude Code, Codex, GitHub Copilot CLI, Cursor Agent CLI, OpenCode, Qwen Code, Hermes y otros.
- Reanudación nativa de sesiones de agentes compatibles mediante referencias oficiales.
- Historial reciente de pantalla de los panes, opcional y desactivado inicialmente.
- Conexión a máquinas locales y remotas por SSH desde una misma interfaz.
- CLI para automatización y API de socket local para scripts, herramientas y agentes.
- Plugins ejecutables con acciones declaradas y eventos.
- Binarios para Linux, macOS y Windows.

## Persistencia y recuperación

La persistencia depende del tipo de interrupción:

- Al separar y volver a conectar el cliente, los procesos siguen ejecutándose y se conserva el estado vivo del terminal.
- Después de reiniciar el servidor, Herdr puede restaurar la estructura de workspaces, tabs, panes, directorios y layout, pero los procesos originales no sobreviven por defecto.
- El historial de pantalla puede reproducir contenido reciente, pero no recupera el proceso que lo generó.
- Los agentes con integración oficial pueden reanudar su propia conversación si se conservan sus referencias de sesión.
- El modo de *live handoff* intenta transferir procesos entre servidores durante ciertas actualizaciones o conexiones remotas, pero es experimental y depende de compatibilidad.

## Casos de uso

### Ejecutar una flota de agentes en paralelo

Crear un workspace con panes para distintos agentes: uno para implementar, otro para revisar, otro para ejecutar pruebas y otro para observar logs o servicios auxiliares.

### Mantener agentes trabajando mientras se desconecta la interfaz

Separar el cliente, cerrar la terminal local o cambiar de máquina mientras el servidor y los procesos continúan ejecutándose.

### Operar agentes en una máquina remota

Conectar por SSH a un servidor de desarrollo y administrar agentes, servidores, pruebas y logs desde el mismo entorno.

### Supervisar estados y bloqueos

Usar los estados `working`, `blocked`, `done` e `idle` para saber qué agente requiere atención y cuál ya terminó.

### Construir automatizaciones alrededor de agentes

Utilizar la CLI, la API de socket o plugins para consultar panes, enviar entradas, reaccionar a eventos e integrar Herdr con scripts o herramientas internas.

### Organizar diferentes contextos de trabajo

Usar workspaces para repositorios o iniciativas y sesiones nombradas cuando se necesita aislar completamente panes, sockets y estado persistente.

## Limitaciones y riesgos

- Herdr organiza y mantiene procesos, pero no garantiza que los agentes colaboren semánticamente ni que sus cambios sean compatibles.
- Separar el cliente mantiene vivos los procesos; reiniciar o detener el servidor puede terminar shells, servidores, pruebas y agentes.
- La restauración de snapshot recupera principalmente la forma del espacio de trabajo, no los procesos en ejecución.
- La reanudación de una conversación depende del agente, de su integración oficial y de versiones mínimas compatibles; si falta la referencia, el pane puede volver como un shell normal.
- El historial de panes puede contener tokens, secretos, prompts y salida de comandos. Está desactivado por defecto, y debe tratarse como historial de terminal si se habilita.
- La detección automática puede producir estado `unknown` o no reconocer correctamente un agente personalizado.
- El acceso remoto por SSH introduce dependencia de red, autenticación, permisos, latencia y disponibilidad del servidor.
- Windows está soportado, pero la documentación registra limitaciones específicas y correcciones en curso.
- Los plugins y la API de socket pueden ejecutar comandos o enviar entradas a procesos; deben considerarse una superficie de automatización privilegiada.
- Live handoff es experimental y no garantiza continuidad de solicitudes, streams, sockets ni mensajes durante el reemplazo del servidor.
- Herdr no reemplaza Git, un sistema de CI/CD, una política de revisión ni un mecanismo de gestión de secretos.

## Recomendaciones

1. Crear un workspace por repositorio o iniciativa y usar nombres explícitos para tabs y panes (`implementacion`, `revision`, `tests`, `logs`).
2. Instalar las integraciones oficiales de los agentes que se necesite reanudar y verificar su estado con la CLI.
3. Tratar la separación del cliente y el reinicio del servidor como eventos distintos; no asumir que un snapshot recuperará procesos vivos.
4. Mantener desactivado el historial de pantalla si los panes pueden mostrar secretos. Si se habilita, proteger el directorio de sesión como se protege el historial de shell.
5. Usar credenciales de mínimo privilegio y evitar dejar tokens en prompts, variables visibles o salida persistida.
6. Probar primero la ejecución local y después SSH, reconexión y actualización en una máquina de prueba.
7. Usar estados del agente como señal operativa, no como prueba de que el resultado es correcto; revisar diffs, tests y logs.
8. Automatizar mediante CLI, socket o plugins solo después de definir qué comandos pueden ejecutarse y qué acciones requieren aprobación.
9. Mantener sincronizados Herdr y las integraciones de los agentes para mejorar la restauración nativa.
10. Para operaciones críticas, usar Herdr como capa de supervisión y ejecución, pero conservar controles independientes de Git, CI/CD, secretos y auditoría.

## Cuándo elegir Herdr

Herdr es una buena opción cuando el problema principal es mantener muchos agentes CLI y procesos técnicos activos, organizarlos en terminales, reconectarse, trabajar por SSH o automatizar su supervisión.

Es más apropiado que un IDE multiagente cuando se necesita un multiplexor persistente y flexible. Si el objetivo principal es crear worktrees aislados, comparar diffs y seleccionar entre varias implementaciones de código, una herramienta como Orca ofrece una capa más orientada a ese flujo.

## Fuentes

- [Documentación oficial de Herdr](https://herdr.dev/docs/)
- [Conceptos: workspaces, tabs, panes, agentes y sesiones](https://herdr.dev/docs/concepts/)
- [Instalación y plataformas](https://herdr.dev/docs/install/)
- [Integraciones con agentes](https://herdr.dev/docs/integrations/)
- [Estado y restauración de sesiones](https://herdr.dev/docs/session-state/)
- [Referencia CLI](https://herdr.dev/docs/cli-reference/)
