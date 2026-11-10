# Herdr + OpenCode

> Documento revisado: 2026-11-09  
> Herramientas: [Herdr](https://herdr.dev/docs/) y [OpenCode](https://opencode.ai/docs/)

## Objetivo

Usar Herdr para organizar y mantener sesiones de OpenCode en terminales persistentes, tanto en la máquina local como en un servidor remoto por SSH. Herdr aporta workspaces, tabs, panes y supervisión; OpenCode interpreta las instrucciones y trabaja sobre el repositorio.

```text
Cliente Herdr ──> servidor Herdr ──> workspace ──> pane ──> OpenCode ──> API del modelo
                                      └── repositorio
```

Para ejecutar un único agente remoto en una sesión sencilla, consulta [tmux + OpenCode en un VPS](tmux-opencode.md). Para la arquitectura y las opciones de Herdr, consulta [Herdr](../herdr.md).

Para instalar Herdr en Ubuntu y monitorear agentes desde tu equipo personal, consulta [Herdr en una VPS Ubuntu](../herdr-vps-ubuntu.md).

## Qué resuelve cada herramienta

| Herramienta | Función |
|---|---|
| **Herdr** | Mantiene terminales y procesos, organiza varios contextos y permite reconectarse al servidor. |
| **OpenCode** | Proporciona el agente de programación, la TUI, los proveedores de modelos y las herramientas de edición. |
| **Git** | Aísla, registra y permite revisar los cambios del agente. |

Herdr no planifica por sí solo la colaboración ni garantiza que dos agentes sean compatibles. Para usar varios agentes sobre el mismo repositorio, crea worktrees o clones separados.

## Requisitos

- Herdr instalado en el equipo desde el que administrarás los workspaces.
- OpenCode instalado en el host donde se ejecutará el agente.
- Una cuenta o credencial configurada para un proveedor de modelos.
- El repositorio y las herramientas de compilación o pruebas instalados en ese host.
- Para un host remoto, acceso SSH configurado y probado desde Herdr.

Las versiones compatibles de la integración pueden cambiar. La documentación de Herdr indica soporte para OpenCode V1 desde `1.18.29` y OpenCode V2; comprueba la matriz vigente antes de actualizar cualquiera de los dos.

## Preparar OpenCode

Instala OpenCode según la [guía de instalación](https://opencode.ai/docs/) y verifica el comando:

```bash
opencode --version
```

Inicia OpenCode una vez y completa la autenticación/configuración inicial del proveedor. La integración de Herdr requiere que exista el directorio de configuración de OpenCode antes de instalar el plugin.

Desde el directorio del repositorio, puedes comprobar que la TUI abre correctamente:

```bash
cd ~/tu-repo
opencode
```

Sal de la TUI después de verificar la conexión. Para administrar credenciales usa el flujo de autenticación documentado por OpenCode; no guardes claves en el repositorio ni en prompts.

## Instalar la integración de Herdr

En el host que ejecuta OpenCode, instala la integración oficial:

```bash
herdr integration install opencode
```

La integración añade los plugins de estado y sesión al directorio de configuración de OpenCode y registra el plugin TUI para la versión correspondiente. Reinicia la TUI de OpenCode después de instalarla.

Si Herdr no detecta el agente o no muestra su estado, comprueba que la integración aparece en la configuración, que OpenCode se inició después de instalarla y que las versiones son compatibles. Reinstala la integración después de resolver una migración pendiente de la configuración de OpenCode, si la CLI lo solicita.

## Crear el workspace de trabajo

En Herdr, crea o selecciona un workspace para el repositorio y abre un pane en el host donde está clonado. Usa un nombre identificable, por ejemplo `api-auth` o `repo-backend`.

En el pane:

```bash
cd ~/tu-repo
git status --short
opencode
```

Escribe las instrucciones en la TUI de OpenCode. Por ejemplo:

```text
Implementa el plan plans/001-login.md.
Respeta su alcance, ejecuta las pruebas indicadas y resume el diff.
No hagas commit ni integres cambios sin mi revisión.
```

Usa tabs o panes separados para contextos distintos, como implementación, revisión, pruebas y logs. Si varios agentes deben modificar el mismo proyecto, asigna un worktree y una rama a cada uno.

## Uso remoto por SSH

Herdr puede conectarse a máquinas remotas por SSH. Configura y prueba primero el acceso al host; después selecciona ese servidor al crear el workspace o pane. En el host remoto deben estar instalados OpenCode, la integración de Herdr, el repositorio y sus dependencias.

El proceso de OpenCode se ejecuta en el host remoto. Cerrar o separar la interfaz cliente no equivale a cerrar el servidor: al volver a conectarte al mismo servidor Herdr, sus panes y procesos pueden seguir activos.

Mantén SSH limitado a claves y usuarios autorizados. Evita exponer directamente a Internet los servicios locales de Herdr u OpenCode; usa SSH o una VPN para acceder.

## Desconexión, reinicio y reanudación

- **Separar el cliente:** los procesos siguen vivos en el servidor Herdr; vuelve a conectarte al servidor y abre el workspace existente.
- **Cerrar un pane o detener el servidor:** puede terminar el proceso de OpenCode y los comandos que se ejecutan en ese terminal.
- **Reiniciar el host:** Herdr puede restaurar la estructura del workspace, pero esto no significa que todos los procesos originales hayan sobrevivido.
- **Reanudar conversación:** con la integración oficial y un identificador de sesión conservado, Herdr puede volver a abrir OpenCode con `opencode --session <id>`. La disponibilidad depende de que la sesión sea reconocible y la integración funcione.

La restauración de la estructura y la reanudación de una conversación son mecanismos distintos. Verifica el pane y la sesión antes de continuar trabajo importante.

## Flujo recomendado con Git

```bash
git switch -c plan/001-login
git status --short
# Trabajar con OpenCode desde el pane de Herdr
git diff
npm test
git add -A
git commit -m "feat(auth): implementar login"
git push -u origin plan/001-login
```

Adapta la prueba y el mensaje de commit al proyecto. Revisa los cambios antes de integrarlos en la rama principal.

## Seguridad y operación

- Ejecuta OpenCode con un usuario sin privilegios de administrador.
- Protege credenciales y archivos de configuración en cada host.
- Considera el historial de terminal como información sensible: puede contener prompts, tokens o salida privada.
- No uses el estado `done` de Herdr como evidencia de que las pruebas pasan; revisa comandos, logs y diff.
- Limita el número de agentes que trabajan sobre el mismo repositorio y aísla sus cambios.
- Usa ramas y worktrees para que los cambios simultáneos no se pisen.

## Solución de problemas

| Síntoma | Comprobación |
|---|---|
| Herdr no reconoce OpenCode | Verifica `opencode --version`, reinstala la integración y reinicia la TUI. |
| Falla `herdr integration install opencode` | Abre OpenCode una vez para crear su configuración y vuelve a ejecutar la instalación. |
| No se puede reanudar la conversación | Comprueba que la integración oficial está activa y que la sesión conserva su referencia. |
| El pane vuelve como una shell | Puede haberse restaurado el workspace sin el proceso; inicia OpenCode y recupera la sesión si su ID está disponible. |
| OpenCode espera autenticación | Completa el inicio de sesión del proveedor en el host correspondiente. |
| Los cambios de agentes se mezclan | Asigna worktrees o clones y ramas diferentes. |
| El pane remoto no inicia | Prueba SSH y verifica PATH, instalación de OpenCode y permisos en el host remoto. |

## Checklist

- [ ] Herdr instalado y conectado al host correcto.
- [ ] OpenCode instalado y autenticado en el host de ejecución.
- [ ] Directorio de configuración de OpenCode creado.
- [ ] Integración instalada con `herdr integration install opencode`.
- [ ] TUI reiniciada después de instalar la integración.
- [ ] Workspace y pane apuntan al repositorio correcto.
- [ ] Rama y aislamiento definidos antes de ejecutar varios agentes.
- [ ] Diff, pruebas y logs revisados antes de integrar cambios.

## Ver también

- [Herdr](../herdr.md): conceptos, persistencia, SSH y limitaciones.
- [Agentes IA remotos en VPS](README.md): arquitectura y decisiones de ejecución remota.
- [tmux + OpenCode en un VPS](tmux-opencode.md): alternativa con un multiplexor y un único agente.
- [Orca](../orca.md): ejecución de agentes y worktrees.
- [OpenCode](../../agentes-codigo/opencode-v2.md): capacidades y configuración del agente.

## Referencias

- Integración OpenCode de Herdr: <https://herdr.dev/docs/integrations/>
- Instalación de Herdr: <https://herdr.dev/docs/install/>
- Estado y restauración de sesiones: <https://herdr.dev/docs/session-state/>
- CLI de OpenCode: <https://opencode.ai/docs/cli/>
- Autenticación de OpenCode: <https://opencode.ai/docs/cli/#auth>
