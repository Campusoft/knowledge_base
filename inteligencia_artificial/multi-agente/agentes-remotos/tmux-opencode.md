# tmux + OpenCode en un VPS

> Documento revisado: 2026-10-22
> Producto documentado: [OpenCode V2](https://opencode.ai/v2/docs/)
> Requisito previo: [Agentes IA remotos en VPS](README.md)

## Objetivo

Ejecutar OpenCode en un VPS dentro de una sesión tmux, conectarte desde tu máquina por SSH y darle órdenes sin perder la sesión cuando se corta la conexión.

```
Tu máquina ──SSH──> VPS ──> tmux ──> opencode ──> API del modelo
```

Para la referencia completa de tmux, consulta [tmux](../../../sistema_operativo/linux/tmux.md). Aquí solo se usa lo necesario.

## Requisitos

- VPS con Linux (Ubuntu/Debian, RHEL/Fedora o similar).
- Usuario **no root** con acceso SSH por clave.
- 2 vCPU, 4 GB de RAM y 20 GB de disco como punto de partida.
- Git y el repositorio que va a modificar el agente.
- Una cuenta o clave API de un proveedor de modelos.

El agente en sí consume pocos recursos; la mayor parte del trabajo la hace el modelo remoto. Si compilas proyectos grandes o ejecutas contenedores, dimensiona en consecuencia.

## Instalación

```bash
# Utilidades base
sudo apt update
sudo apt install -y tmux git ripgrep curl

# OpenCode V2 (instalación oficial)
curl -fsSL https://opencode.ai/v2/install | bash

# Verificar
opencode --version
```

Alternativas de instalación: `npm install -g @opencode/cli`, Homebrew o binario standalone. Consulta la [documentación oficial](https://opencode.ai/v2/docs/) para tu plataforma.

Si usas Pi en lugar de OpenCode, instálalo según su documentación; el patrón con tmux es el mismo.

## Configuración mínima de tmux

```bash
cat >> ~/.tmux.conf <<'EOF'
set -g mouse on
set -g history-limit 50000
set -g base-index 1
setw -g pane-base-index 1
set -g mode-keys vi
set -g escape-time 10
EOF

tmux source-file ~/.tmux.conf
```

Esto habilita ratón, historial amplio y numeración desde 1. No necesitas plugins para este flujo.

## Preparar el repositorio y los planes

```bash
git clone git@github.com:tu-usuario/tu-repo.git
cd tu-repo
mkdir -p plans
```

Crea un plan por tarea:

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
```

## Primera sesión

```bash
tmux new -s agente
cd ~/tu-repo
opencode
```

Dentro de la TUI:

1. Ejecuta `/connect` para conectar el proveedor del modelo.
2. Elige el modelo.
3. Comprueba que el agente ve el repositorio y puede ejecutar los tests.

Si la TUI no se dibuja correctamente por SSH, asegúrate de usar una terminal con TTY (`ssh -t`) y un `TERM` compatible, por ejemplo `xterm-256color`.

## Dar órdenes

En modo interactivo, escribe la orden directamente:

```
Implementa plans/001-login.md.
Respeta el alcance del plan.
Ejecuta los tests y no hagas commit hasta que pasen.
```

Buenas prácticas al pedir:

- Referencia el archivo del plan por ruta.
- Delimita los archivos o carpetas permitidos.
- Exige ejecutar tests y mostrar el resultado.
- Pide un resumen de cambios y riesgos.
- Revisa el diff antes de aceptar.

Si el agente se queda esperando una aprobación, revisa la configuración de permisos de OpenCode. En las primeras sesiones conviene mantener la aprobación interactiva.

## Desconectarse y volver

```bash
# Dentro de tmux: Ctrl+B d
```

Desde tu máquina:

```bash
# Reconectar a la sesión existente o crearla
ssh -t usuario@vps 'tmux attach -t agente || tmux new -s agente'
```

Otras operaciones útiles:

```bash
# Listar sesiones
ssh usuario@vps 'tmux ls'

# Renombrar
ssh usuario@vps 'tmux rename-session -t agente repo-x'

# Cerrar la sesión
ssh usuario@vps 'tmux kill-session -t agente'
```

La sesión sobrevive a la desconexión SSH, pero **no a un reinicio del VPS**.

## Órdenes puntuales sin abrir la TUI

OpenCode tiene un modo no interactivo:

```bash
ssh usuario@vps 'cd ~/tu-repo && opencode run "implementa plans/002-api.md y ejecuta los tests"'
```

Para tareas largas que deben seguir si se corta la conexión:

```bash
ssh usuario@vps 'tmux new-session -d -s plan002 "cd ~/tu-repo && opencode run \"implementa plans/002-api.md\" > /tmp/plan002.log 2>&1"'
```

Seguir el progreso:

```bash
ssh usuario@vps 'tail -f /tmp/plan002.log'
```

Ventaja: no dependes de mantener la terminal abierta. Desventaja: el modo no interactivo necesita que los permisos estén configurados para no bloquearse esperando confirmación.

## Varios agentes o varios planes

Usa una sesión o ventana por contexto:

```bash
# Una sesión por repositorio
tmux new -s repo-a
tmux new -s repo-b

# O ventanas dentro de una sesión
# Ctrl+B c crea una ventana
# Ctrl+B , la renombra
```

Reglas:

- No ejecutes dos agentes sobre el **mismo árbol de trabajo**: se pisarán los cambios.
- Para paralelizar sobre el mismo repo, usa **git worktrees** o clones separados.
- Nombra sesiones y ventanas con el plan o la rama.

## Interfaz web opcional

OpenCode V2 puede abrir una interfaz web temporal:

```bash
# En el VPS
opencode pair
```

Muestra una URL local similar a `http://127.0.0.1:<puerto>/auth/connect/...`. Para usarla desde tu máquina sin exponerla a Internet:

```bash
# Túnel SSH usando el puerto que imprimió el comando
ssh -L <puerto>:127.0.0.1:<puerto> usuario@vps
```

Abre la URL en tu navegador local antes de que caduque (el enlace es de un solo uso y expira en unos minutos). No publiques ese puerto en Internet; usa túnel o VPN.

## Flujo con Git por plan

```bash
# Antes de empezar
git switch -c plan/001-login

# Pedir al agente el trabajo
# ...

# Revisar
git status
git diff

# Verificar
npm test

# Integrar
git add -A
git commit -m "feat(auth): implementar login"
git push -u origin plan/001-login
```

Abre un PR y revisa antes de mergear. El agente no debería integrar directamente en la rama principal.

## Seguridad operativa

- Ejecuta OpenCode con un **usuario dedicado**, nunca como root.
- SSH solo por clave; desactiva el login de root y usa firewall.
- Si el puerto SSH está expuesto, valora **Tailscale** o **WireGuard**.
- No expongas la API ni `opencode pair` fuera de un túnel o VPN.
- No pongas claves API en el repositorio ni en los prompts.
- Revisa los diffs antes de mergear.
- Limita tiempo y recursos; vigila `htop` y el espacio en disco.
- Guarda logs de los trabajos largos.

## Solución de problemas

| Síntoma | Causa probable | Solución |
|---------|----------------|----------|
| La TUI se ve mal o no acepta teclas | Falta TTY o `TERM` incorrecto | Usa `ssh -t` y `export TERM=xterm-256color` |
| La sesión desapareció | El VPS se reinició | tmux no persiste reinicios; relanza la sesión |
| `opencode: command not found` | PATH no actualizado | Abre una shell nueva o revisa la instalación |
| El agente no responde | Falta conectar proveedor o modelo | `/connect` dentro de la TUI |
| Se queda esperando aprobación | Permisos interactivos | Revisa la configuración de permisos |
| Dos agentes se pisan | Mismo árbol de trabajo | Usa worktrees o clones separados |
| Consumo alto de CPU/RAM | Build, tests o bucle del agente | Revisa `htop`, limita recursos y cancela |
| `sessions should be nested with care` | tmux dentro de tmux | `unset TMUX` o usa otro socket |

Para diagnosticar el servicio de OpenCode:

```bash
opencode service status
opencode api get /api/info
```

## Checklist

- [ ] Usuario no root con SSH por clave.
- [ ] tmux instalado y configuración mínima aplicada.
- [ ] OpenCode instalado y proveedor conectado.
- [ ] Repositorio clonado y planes en `plans/`.
- [ ] Una rama por plan.
- [ ] Tests definidos en cada plan.
- [ ] Sesión tmux nombrada y verificada tras desconectar.
- [ ] Logs para trabajos largos.
- [ ] Revisión de diffs antes de integrar.
- [ ] Backup y remoto de Git configurados.

## Ver también

- [Agentes IA remotos en VPS](README.md): modelo conceptual y decisiones de arquitectura.
- [tmux](../../../sistema_operativo/linux/tmux.md): atajos, configuración y limitaciones.
- [Herdr](../herdr.md): alternativa para gestionar varios agentes CLI.
- [Orca](../orca.md): worktrees y ejecución remota por SSH.

## Referencias

- OpenCode V2: <https://opencode.ai/v2/docs/>
- CLI de OpenCode: <https://opencode.ai/v2/docs/cli>
- tmux: <https://github.com/tmux/tmux>
- Tailscale: <https://tailscale.com/>
