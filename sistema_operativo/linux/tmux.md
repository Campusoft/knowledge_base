# tmux

> Documento revisado: 2026-10-22
> Producto documentado: [tmux](https://github.com/tmux/tmux)

## Descripción

tmux es un multiplexor de terminal para Linux, macOS y otros sistemas tipo Unix. Permite ejecutar varias terminales dentro de una sola ventana, dividirlas en paneles y, sobre todo, mantener procesos vivos aunque se cierre la conexión SSH o el cliente de terminal.

Usa una arquitectura cliente-servidor: un servidor tmux mantiene las sesiones y los procesos, mientras que uno o varios clientes se conectan a él. Al desconectarse el cliente, el servidor y los procesos continúan ejecutándose.

## Características

- **Sesiones persistentes:** los procesos sobreviven a la desconexión del cliente y a caídas de la red.
- **Ventanas y paneles:** varias terminales dentro de una sesión, con división horizontal y vertical.
- **Detach/attach:** separar el cliente sin detener los procesos y volver a conectarse después.
- **Sesiones agrupadas:** varios clientes pueden compartir la misma sesión (útil para pair programming).
- **Modo copia:** desplazamiento por el historial, búsqueda y copia de texto.
- **Automatización:** se puede controlar desde scripts (`new-session -d`, `send-keys`, `capture-pane`).
- **Configuración centralizada:** `~/.tmux.conf` para atajos, colores, estado y comportamiento.
- **Bajo consumo:** el servidor es ligero y no requiere un entorno gráfico.
- **Multiplataforma:** Linux, macOS, BSD y Windows mediante WSL o MSYS2/Cygwin.

## Modelo de ejecución

```
Cliente (terminal) ──> Servidor tmux ──> Sesiones ──> Ventanas ──> Paneles
```

- **Servidor:** proceso en segundo plano que posee las sesiones y los pseudo-terminales.
- **Sesión:** agrupación de ventanas; normalmente una por proyecto o servidor.
- **Ventana:** ocupa toda la terminal y contiene uno o más paneles.
- **Panel:** terminal real donde se ejecuta un proceso.

Por defecto hay un único servidor por usuario. Las sesiones se pueden listar, nombrar, agrupar y destruir sin afectar a las demás.

## Instalación

```bash
# Debian / Ubuntu
sudo apt install tmux

# RHEL / Fedora
sudo dnf install tmux

# Arch Linux
sudo pacman -S tmux

# macOS
brew install tmux

# Verificar versión
tmux -V
```

En Windows no hay soporte nativo: se usa WSL2, MSYS2 o Cygwin.

## Conceptos y operación básica

El **prefijo** por defecto es `Ctrl+B`. Se pulsa el prefijo y después la tecla de la acción.

### Sesiones

| Acción | Comando | Atajo |
|--------|---------|-------|
| Crear sesión con nombre | `tmux new -s proyecto` | — |
| Listar sesiones | `tmux ls` | `Ctrl+B s` |
| Conectarse | `tmux attach -t proyecto` | — |
| Separarse (detach) | — | `Ctrl+B d` |
| Renombrar sesión | `tmux rename-session -t viejo nuevo` | `Ctrl+B $` |
| Cerrar sesión | `tmux kill-session -t proyecto` | — |
| Cerrar todas las sesiones | `tmux kill-server` | — |

### Ventanas

| Acción | Atajo |
|--------|-------|
| Nueva ventana | `Ctrl+B c` |
| Siguiente / anterior | `Ctrl+B n` / `Ctrl+B p` |
| Ir a ventana por número | `Ctrl+B 0`…`9` |
| Listar ventanas | `Ctrl+B w` |
| Renombrar ventana | `Ctrl+B ,` |
| Cerrar ventana | `Ctrl+B &` |

### Paneles

| Acción | Atajo |
|--------|-------|
| Dividir verticalmente | `Ctrl+B %` |
| Dividir horizontalmente | `Ctrl+B "` |
| Moverse entre paneles | `Ctrl+B` + flechas |
| Alternar pane | `Ctrl+B o` |
| Zoom / restaurar panel | `Ctrl+B z` |
| Cerrar panel | `Ctrl+B x` |
| Rotar paneles | `Ctrl+B Ctrl+O` |
| Cambiar layout | `Ctrl+B Espacio` |

### Otros

| Acción | Atajo |
|--------|-------|
| Modo copia / historial | `Ctrl+B [` |
| Pegar buffer | `Ctrl+B ]` |
| Ayuda de atajos | `Ctrl+B ?` |
| Línea de comandos tmux | `Ctrl+B :` |
| Recargar configuración | `tmux source-file ~/.tmux.conf` |

En modo copia, con `set -g mode-keys vi` se usan las teclas de vi: `Espacio` para empezar la selección y `Enter` para copiar. `q` sale del modo.

## Flujo típico en un servidor remoto

```bash
# 1. Conectarse por SSH
ssh usuario@vps

# 2. Crear (o reutilizar) una sesión de trabajo
tmux new -s proyecto

# 3. Ejecutar el proceso largo: build, tests, agente, etc.
./scripts/test.sh

# 4. Desconectarse sin detenerlo
#    Ctrl+B d

# 5. Cerrar la conexión SSH y volver más tarde
ssh usuario@vps
tmux attach -t proyecto
```

Patrón idempotente para scripts:

```bash
if ! tmux has-session -t proyecto 2>/dev/null; then
  tmux new-session -d -s proyecto
fi
tmux send-keys -t proyecto 'comando-largo' Enter
```

## Configuración recomendada

Crear `~/.tmux.conf` (las versiones recientes también aceptan `~/.config/tmux/tmux.conf`):

```tmux
# Terminal y colores
set -g default-terminal "tmux-256color"
set -ag terminal-overrides ",xterm-256color:RGB"

# Comportamiento
set -g mouse on
set -g history-limit 50000
set -g base-index 1
setw -g pane-base-index 1
set -g renumber-windows on
set -g escape-time 10
set -g focus-events on
set -g mode-keys vi

# Barra de estado
set -g status-interval 5
set -g status-right "#{?client_prefix,#[reverse]PREFIX#[noreverse] ,}%H:%M %d-%b"

# Recargar configuración con Ctrl+B r
bind r source-file ~/.tmux.conf \; display "Config recargada"
```

Notas:

- `tmux-256color` requiere que el término esté disponible; si hay problemas de colores, usar `screen-256color`.
- `history-limit` consume memoria por panel; 50 000 líneas es un equilibrio razonable.
- `escape-time 10` reduce la latencia percibida en editores como Vim.
- Para copiar al portapapeles del sistema hace falta integración adicional (OSC 52 o herramientas como `xclip`/`wl-copy`).

## Automatización y scripting

tmux es útil para lanzar y observar procesos sin intervención:

```bash
# Crear una sesión en segundo plano ejecutando un comando
tmux new-session -d -s worker 'python job.py'

# Enviar comandos a una ventana o panel
tmux send-keys -t worker 'make test' Enter

# Leer el contenido visible de un panel
tmux capture-pane -p -t worker

# Guardar toda la salida en un archivo
tmux pipe-pane -o -t worker 'cat >> /tmp/worker.log'

# Listar paneles de todas las sesiones
tmux list-panes -a

# Conectarse en modo solo lectura
tmux attach -r -t worker
```

También se puede usar desde un `systemd` timer o un script de CI para supervisar tareas, aunque para servicios reales conviene `systemd` o un contenedor.

## Casos de uso

- **Trabajo remoto por SSH:** mantener el entorno de desarrollo aunque se caiga la conexión.
- **Procesos largos:** builds, migraciones, descargas, entrenamientos o tests que tardan horas.
- **Agentes IA de código:** ejecutar `opencode`, `pi` u otros agentes en un VPS y reconectarse a la sesión para dar órdenes o revisar resultados.
- **Monitorización:** un panel con logs, otro con `htop`, otro con la shell.
- **Pair programming:** sesiones agrupadas para que dos personas vean la misma terminal.
- **Automatización:** lanzar trabajos en segundo plano y capturar su salida.
- **Contexto persistente:** conservar el directorio, el historial y los procesos entre sesiones de trabajo.

## Recomendaciones

1. **Una sesión por proyecto o servidor**, con nombres descriptivos (`api-prod`, `cliente-x`).
2. **No usar tmux como gestor de servicios.** Si el proceso debe reiniciarse solo o arrancar con el sistema, usar `systemd`, Docker o un supervisor.
3. **Versionar la configuración** y mantenerla pequeña; los plugins de terceros añaden dependencias y riesgos.
4. **Habilitar `history-limit` y mouse** para una experiencia cómoda, sin abusar de la memoria.
5. **Registrar trabajos largos** con `pipe-pane` si necesitas auditoría o depuración.
6. **Limpiar sesiones huérfanas** periódicamente y valorar `destroy-unattached on`.
7. **Automatizar con `has-session`** para que los scripts sean idempotentes.
8. **No ejecutar tmux como root** salvo necesidad; usar un usuario con los permisos mínimos.
9. **Proteger el acceso** con SSH por clave, VPN o Tailscale, y no exponer el socket ni puertos innecesarios.
10. **Actualizar tmux** y probar la configuración después de cada actualización importante.

## Limitaciones

- **No sobrevive al reinicio del servidor.** Si el VPS se reinicia, las sesiones y los procesos se pierden; para persistencia entre reinicios hacen falta plugins como `tmux-resurrect`/`tmux-continuum` o gestión externa.
- **No es un supervisor de procesos.** No reinicia automáticamente un proceso que falla ni garantiza el estado deseado.
- **No aísla ni limita recursos.** Todos los paneles comparten usuario, sistema de archivos y credenciales.
- **Un servidor por usuario y socket.** Las sesiones anidadas requieren otro socket o cambiar el prefijo.
- **Colaboración limitada.** Las sesiones agrupadas no ofrecen permisos por usuario ni edición concurrente real.
- **Portapapeles no nativo.** Copiar al portapapeles del sistema depende del terminal y de configuración adicional.
- **Historial en memoria.** Un `history-limit` alto en muchos paneles aumenta el consumo de RAM.
- **Compatibilidad de terminal.** Colores, teclas y truecolor pueden requerir ajustes según el emulador.
- **Sin soporte nativo en Windows.** Depende de WSL, MSYS2 o Cygwin.
- **Ecosistema de plugins de terceros.** Su mantenimiento y calidad no están garantizados.

## Seguridad

- El socket de tmux se crea en un directorio privado del usuario; no cambiar sus permisos a algo más permisivo.
- Cualquier persona con acceso al mismo usuario puede conectarse a las sesiones y ver o enviar comandos.
- No dejar credenciales en el historial de la shell ni en la salida de los paneles.
- Usar SSH con clave, `fail2ban`/firewall y, preferiblemente, una VPN o Tailscale.
- En un VPS compartido, ejecutar los agentes o procesos sensibles en un contenedor o usuario dedicado.

## Alternativas

| Herramienta | Cuándo considerarla |
|-------------|---------------------|
| **GNU screen** | Entornos muy antiguos o donde tmux no está disponible. |
| **Zellij** | Se busca una experiencia más moderna y guiada. |
| **abduco / dtach** | Solo se necesita separar y reconectar un proceso, sin ventanas. |
| **byobu** | Se quiere una capa amigable sobre tmux/screen. |
| **systemd** | El proceso debe arrancar con el sistema y reiniciarse solo. |
| **Docker / Podman** | Se necesita aislamiento y límites de recursos. |
| **Herdr / Orca** | Se gestionan varios agentes CLI con workspaces, paneles y sesiones. |

## Solución de problemas

- **`no server running on ...`**: no hay sesiones activas o el servidor se detuvo (por ejemplo, tras reiniciar).
- **`sessions should be nested with care`**: se está ejecutando tmux dentro de tmux; usar otro prefijo, otro socket o `unset TMUX`.
- **Colores incorrectos**: revisar `default-terminal` y el `TERM` del terminal; probar `screen-256color`.
- **El portapapeles no funciona**: configurar OSC 52 o integrar `xclip`/`wl-copy`.
- **Latencia al pulsar Escape**: reducir `escape-time`.
- **No se puede conectar a una sesión**: verificar usuario, permisos del socket y que la sesión exista con `tmux ls`.

## Ver también

- [Agentes IA remotos en VPS](../../inteligencia_artificial/multi-agente/agentes-remotos/README.md): modelo conceptual para ejecutar agentes en un servidor remoto.
- [tmux + OpenCode en un VPS](../../inteligencia_artificial/multi-agente/agentes-remotos/tmux-opencode.md): guía práctica de sesiones persistentes con OpenCode.
- [Herdr](../../inteligencia_artificial/multi-agente/herdr.md): gestor de workspaces para agentes CLI inspirado en tmux.
- [Orca](../../inteligencia_artificial/multi-agente/orca.md): orquestación de agentes y worktrees, con soporte remoto por SSH.

## Referencias

- Repositorio oficial: <https://github.com/tmux/tmux>
- Wiki oficial: <https://github.com/tmux/tmux/wiki>
- Manual: `man tmux`
- Chuleta de atajos: <https://tmuxcheatsheet.com/>
