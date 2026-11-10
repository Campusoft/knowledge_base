# Herdr en una VPS Ubuntu y monitoreo desde tu equipo

> Documento revisado: 2026-11-09  
> Producto: [Herdr](https://herdr.dev/docs/)

## Objetivo

Instalar o preparar Herdr en una VPS Ubuntu, ejecutar allí agentes y procesos de desarrollo, y conectarse desde una máquina personal para revisar su avance. La VPS mantiene los panes y procesos; la interfaz cliente de tu máquina muestra y controla esos terminales.

```text
Máquina personal (cliente Herdr) ── SSH ──> VPS Ubuntu (servidor Herdr)
                                                 ├── workspace/repositorio
                                                 ├── pane: OpenCode
                                                 └── pane: pruebas y logs
```

Para usar OpenCode con la integración de Herdr, consulta [Herdr + OpenCode](agentes-remotos/herdr-opencode.md).

## Requisitos

- VPS Ubuntu con arquitectura compatible y un usuario sin privilegios de administrador.
- Acceso SSH por clave desde tu máquina personal.
- Git y las herramientas necesarias para compilar y probar el repositorio.
- Cliente Herdr instalado en tu máquina personal.

La interfaz local se conecta al host remoto mediante SSH. No hace falta exponer un puerto de Herdr a Internet.

## 1. Configurar y probar SSH

Desde tu máquina personal, primero confirma que puedes entrar por SSH. En Windows, ejecuta los comandos desde PowerShell o Windows Terminal. Si todavía no tienes un alias SSH, puedes conectarte usando directamente el usuario y el host:

```powershell
ssh desarrollador@vps.ejemplo.com
```

Si SSH usa un puerto distinto al predeterminado:

```powershell
ssh -p 2222 desarrollador@vps.ejemplo.com
```

Para no repetir esos datos, crea o edita `~/.ssh/config`; en Windows esa ruta corresponde a `%USERPROFILE%\.ssh\config`. Define un alias:

```sshconfig
Host mi-vps
  HostName vps.ejemplo.com
  User desarrollador
  IdentityFile ~/.ssh/id_ed25519
  IdentitiesOnly yes
```

Usa el DNS o IP de la VPS, el usuario real y la ruta de tu clave. Si SSH escucha en otro puerto, añade `Port 2222` con el puerto correspondiente.

Prueba primero la conexión normal usando el alias:

```bash
ssh mi-vps
```

Si el acceso no funciona, resuelve primero la autenticación, el firewall o la configuración SSH.

## 2. Instalar Herdr en Ubuntu

En la VPS, instala Herdr como el usuario que ejecutará los agentes, sin `sudo`:

```bash
curl -fsSL https://herdr.dev/install.sh | sh
```

Abre una shell nueva si hace falta actualizar el `PATH` y comprueba la instalación:

```bash
herdr --version
```

El instalador coloca el ejecutable en `~/.local/bin/herdr`. Si aparece el aviso de que esa carpeta no está en el `PATH`, la instalación terminó, pero Bash todavía no puede resolver el comando `herdr`.

Para usarlo inmediatamente en la sesión actual:

```bash
export PATH="$HOME/.local/bin:$PATH"
herdr --version
```

Para conservar el cambio en futuras sesiones de Bash:

```bash
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc
herdr --version
```

Como alternativa, puedes comprobar o ejecutar el binario con su ruta completa:

```bash
~/.local/bin/herdr --version
```

Si Herdr funciona en una sesión interactiva pero no cuando te conectas desde el cliente remoto, verifica que `~/.local/bin` esté disponible en el `PATH` de la shell no interactiva que usa SSH.

La conexión remota interactiva de Herdr también puede instalar o actualizar el paquete remoto si aún falta. La instalación manual deja el host preparado de antemano.

Para actualizar una instalación directa:

```bash
herdr update
```

## 3. Conectar el cliente personal

Instala Herdr en tu máquina personal desde la [guía oficial](https://herdr.dev/docs/install/). Desde PowerShell o Windows Terminal, puedes conectarte de cualquiera de estas formas.

Sin alias SSH, indica directamente el usuario y el host:

```powershell
herdr --remote ssh://desarrollador@vps.ejemplo.com
```

Si SSH usa un puerto distinto al predeterminado, inclúyelo en la dirección:

```powershell
herdr --remote ssh://desarrollador@vps.ejemplo.com:2222
```

Con el alias `mi-vps` configurado en el archivo SSH, usa:

```bash
herdr --remote mi-vps
```

La interfaz se ejecuta en tu máquina personal y presenta los terminales que corren en la VPS. Si cierras o separas el cliente, los procesos siguen en el servidor Herdr; vuelve a conectar con el mismo comando para ver el workspace.

## 4. Ejecutar y supervisar agentes

En Herdr, crea o selecciona un workspace y abre panes en el repositorio remoto. Por ejemplo:

```bash
cd ~/tu-repo
git status --short
opencode
```

Puedes organizar otros panes para ejecutar pruebas, observar logs o iniciar servicios auxiliares. Herdr muestra la salida de terminal y el estado detectado del agente. Revisa también la conversación de OpenCode, el diff de Git y el resultado real de las pruebas: el estado del pane es una señal de actividad, no una validación del código.

Para retomar el trabajo desde otra máquina, instala el cliente Herdr allí y usa el mismo alias SSH. No ejecutes una segunda copia del agente sobre el mismo árbol de trabajo sin aislar sus cambios.

## 5. Persistencia y reinicios

- **Cerrar el cliente local:** no detiene los panes ni los procesos del servidor. Reconecta con `herdr --remote mi-vps`.
- **Cerrar un pane o detener Herdr en la VPS:** puede terminar los procesos que corrían en ese terminal.
- **Reiniciar Ubuntu:** no conserva los procesos originales por defecto. Herdr puede restaurar la estructura del workspace, pero la reanudación del agente depende de su integración y de que exista una referencia de sesión válida.
- **Actualizar o reiniciar Herdr:** consulta la documentación de persistencia antes de hacerlo durante una tarea importante; la transferencia en vivo entre servidores depende de la versión y puede ser experimental.

Mantén commits o checkpoints de Git en tareas largas y registra cómo reanudar el agente antes de reiniciar la VPS.

## Seguridad

- Usa un usuario de VPS dedicado y sin privilegios de administrador para los agentes.
- Protege la clave SSH y limita el acceso de red al puerto SSH.
- No publiques APIs ni interfaces de agentes en Internet; entra mediante SSH o VPN.
- No coloques claves de proveedores en el repositorio, prompts o logs.
- Ten presente que el historial del terminal puede mostrar información sensible.
- Revisa los diffs y ejecuta las verificaciones del proyecto antes de integrar cambios.

## Solución de problemas

| Síntoma | Comprobación |
|---|---|
| `ssh mi-vps` falla | Verifica DNS/IP, puerto, usuario, clave, permisos y firewall. |
| Herdr no puede adjuntarse | Confirma que SSH funciona y que el usuario remoto puede ejecutar Herdr o acepta la instalación interactiva. |
| `herdr: command not found` después de instalar | Añade `~/.local/bin` al `PATH` con `export PATH="$HOME/.local/bin:$PATH"`; para Bash, guarda esa línea en `~/.bashrc` y ejecuta `source ~/.bashrc`. |
| `herdr` funciona en la terminal, pero no por SSH remoto | Verifica que `~/.local/bin` también esté en el `PATH` de la shell no interactiva; prueba `~/.local/bin/herdr --version`. |
| El pane reaparece como shell tras reiniciar | Se restauró el workspace, pero no el proceso original; inicia el agente o reanuda su sesión si está disponible. |
| No se ve el progreso | Comprueba que te conectaste al mismo host y workspace; revisa el proceso, la salida del pane y el estado de OpenCode. |
| No se reanuda OpenCode | Revisa la instalación de su integración y que la sesión conserve su referencia. |

## Referencias

- [Instalación de Herdr](https://herdr.dev/docs/install/)
- [Conexión entre máquinas](https://herdr.dev/docs/connecting-machines/)
- [Trabajo remoto con Herdr](https://herdr.dev/docs/how-to-work/)
- [Persistencia y conexión remota por SSH](https://herdr.dev/docs/persistence-remote/)
- [Herdr + OpenCode](agentes-remotos/herdr-opencode.md)
- [Agentes IA remotos en VPS](agentes-remotos/README.md)
