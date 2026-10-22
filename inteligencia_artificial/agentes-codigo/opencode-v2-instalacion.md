# Instalación de OpenCode V2

> Documento revisado: 2026-10-22
> Producto documentado: [OpenCode V2](https://opencode.ai/v2/docs/)
> Documento principal: [OpenCode V2](opencode-v2.md)

## Alcance

Esta guía cubre la instalación de OpenCode V2, con detalle para **Linux Ubuntu**, y una referencia rápida para macOS y Windows. También incluye la verificación posterior, la solución del error `opencode: command not found` y la configuración inicial.

Para la arquitectura, características y casos de uso, consulta [opencode-v2.md](opencode-v2.md).

## Requisitos

- Linux, macOS o Windows (en Windows se recomienda WSL2).
- Arquitectura x86_64/AMD64 o ARM64.
- Usuario sin privilegios de root para la instalación.
- Conexión a Internet para descargar el binario y para el proveedor del modelo.
- `curl` y `tar` para el instalador oficial.
- Git y un repositorio de trabajo.
- Opcional: `tmux` y `ripgrep`.

OpenCode no requiere GPU: el modelo se ejecuta en el proveedor remoto. Si usas modelos locales con Ollama, LM Studio o vLLM, necesitarás hardware suficiente para el modelo.

## Linux Ubuntu

### 1. Paquetes base

```bash
sudo apt update
sudo apt install -y curl git tar ripgrep tmux
```

`tar` es necesario para que el instalador descomprima el binario. `tmux` y `ripgrep` son opcionales, pero recomendables para trabajar en un VPS y para búsquedas.

### 2. Instalación con el script oficial

```bash
curl -fsSL https://opencode.ai/v2/install | bash
```

El instalador:

- Detecta sistema operativo y arquitectura.
- Elige la variante `baseline` si el CPU no soporta AVX2.
- Elige la variante `musl` en sistemas Alpine.
- Instala el binario en `~/.opencode/bin/opencode`.
- Añade esa ruta al archivo de configuración de tu shell.

> Si prefieres revisar el script antes de ejecutarlo:
>
> ```bash
> curl -fsSL https://opencode.ai/v2/install -o /tmp/opencode-install.sh
> less /tmp/opencode-install.sh
> bash /tmp/opencode-install.sh
> ```

### 3. Verificar

```bash
opencode --version
command -v opencode
```

Salida esperada (la versión puede variar):

```text
opencode v2.1.3
/home/<usuario>/.opencode/bin/opencode
```

### 4. Solución de problemas: `opencode: command not found`

El instalador **no copia el binario a `/usr/local/bin`**. Lo deja en:

```text
~/.opencode/bin/opencode
```

Además, como `curl ... | bash` se ejecuta en un subproceso, el cambio de `PATH` que hace el instalador en `~/.bashrc` **no se aplica a la terminal que ya estaba abierta**.

#### 4.1. Comprobar que el binario existe y funciona

```bash
ls -l "$HOME/.opencode/bin/opencode"
"$HOME/.opencode/bin/opencode" --version
```

Si la segunda orden muestra la versión, el binario está bien y el problema es solo el `PATH`.

#### 4.2. Corregir el `PATH` en la sesión actual

```bash
export PATH="$HOME/.opencode/bin:$PATH"
hash -r
opencode --version
```

Salida esperada:

```text
opencode v2.1.3
```

#### 4.3. Dejarlo permanente en bash

```bash
grep -qF 'opencode/bin' "$HOME/.bashrc" || \
  echo 'export PATH="$HOME/.opencode/bin:$PATH"' >> "$HOME/.bashrc"

source "$HOME/.bashrc"
hash -r
opencode --version
```

Verificar la ruta efectiva:

```bash
command -v opencode
```

Salida esperada:

```text
/home/<usuario>/.opencode/bin/opencode
```

También puedes abrir una terminal nueva o ejecutar:

```bash
exec "$SHELL"
```

#### 4.4. Si usas otro shell

```bash
echo "$SHELL"
```

- **zsh:** añadir la línea a `~/.zshrc` y ejecutar `source ~/.zshrc`.
- **fish:** ejecutar `fish_add_path "$HOME/.opencode/bin"`.

#### 4.5. Si el binario no existe

La instalación no se completó. Reinstalar mostrando toda la salida:

```bash
curl -fsSL https://opencode.ai/v2/install -o /tmp/opencode-install.sh
bash /tmp/opencode-install.sh
```

Comprobar arquitectura y sistema:

```bash
uname -m
uname -s
. /etc/os-release && echo "$PRETTY_NAME"
```

Si se instaló con `sudo`, el binario puede haber quedado en `/root/.opencode/bin/opencode`. En ese caso, reinstalar **sin sudo** como el usuario que va a usar OpenCode.

#### 4.6. Resumen de problemas

| Síntoma | Causa probable | Solución |
|---------|----------------|----------|
| `opencode: command not found` | `~/.opencode/bin` no está en `PATH` | `export PATH="$HOME/.opencode/bin:$PATH"` |
| El comando directo `~/.opencode/bin/opencode --version` funciona | El binario está bien; falta el `PATH` | Añadir la ruta a `~/.bashrc` |
| El binario no existe | Instalación incompleta o hecha con `sudo` | Reinstalar sin `sudo` y revisar la salida |
| `Permission denied` | Falta permiso de ejecución | `chmod +x ~/.opencode/bin/opencode` |
| Error de librerías | glibc/musl o arquitectura incompatible | Usar el binario glibc o musl adecuado |

### 5. Instalación con npm

Requiere una versión LTS reciente de Node.js y npm:

```bash
npm install -g @opencode/cli
opencode --version
```

El paquete usa un `postinstall` para seleccionar el binario nativo de tu plataforma.

Si `opencode` no aparece después de instalar con npm, comprueba el prefijo global:

```bash
npm config get prefix
```

Añade su directorio `bin` al `PATH`. Por ejemplo:

```bash
export PATH="$(npm config get prefix)/bin:$PATH"
hash -r
opencode --version
```

### 6. Instalación con Bun o pnpm

```bash
# Bun
bun install -g --trust @opencode/cli

# pnpm
pnpm add -g --allow-build=@opencode/cli @opencode/cli
```

### 7. Binario standalone

Descarga el archivo correspondiente a tu arquitectura desde la [página de instalación](https://opencode.ai/v2/docs/):

- Linux glibc x64 / ARM64.
- Linux musl x64 / ARM64 (Alpine y similares).
- macOS y Windows también tienen binarios standalone.

Descomprime, mueve el binario a un directorio del `PATH` y dale permisos de ejecución:

```bash
chmod +x opencode
sudo mv opencode /usr/local/bin/
opencode --version
```

### 8. Primer arranque

```bash
cd ~/repos/mi-proyecto
opencode
```

Dentro de la TUI:

```text
/connect     # conectar un proveedor
/models      # elegir modelo
```

También puedes conectar por variables de entorno o configuración.

### 9. Configuración inicial

Crea `opencode.jsonc` en la raíz del proyecto:

```jsonc title="opencode.jsonc"
{
  "$schema": "https://opencode.ai/config.json",
  "model": "anthropic/claude-sonnet-4-5",
  "permissions": [
    { "action": "shell", "resource": "*", "effect": "ask" },
    { "action": "shell", "resource": "git status *", "effect": "allow" },
    { "action": "shell", "resource": "git diff *", "effect": "allow" },
    { "action": "shell", "resource": "git log *", "effect": "allow" },
    { "action": "shell", "resource": "git push *", "effect": "deny" }
  ]
}
```

Ajusta el modelo a uno disponible en tu proveedor. No escribas claves directamente en el archivo; usa variables de entorno:

```jsonc
{
  "providers": {
    "anthropic": {
      "settings": {
        "apiKey": "{env:ANTHROPIC_API_KEY}"
      }
    }
  }
}
```

Añade un `AGENTS.md` en el repositorio con las reglas del proyecto: estilo, comandos de test, estructura y cosas que no se deben tocar.

## macOS

Con Homebrew:

```bash
brew install anomalyco/tap/opencode-v2
opencode --version
```

También puedes usar el script oficial o un binario standalone:

```bash
curl -fsSL https://opencode.ai/v2/install | bash
```

En macOS, el instalador añade la ruta a `~/.zshrc` y puede mostrar el aviso de recargar el shell. Usa `source ~/.zshrc` o abre una terminal nueva.

## Windows

La opción recomendada es **WSL2** con Ubuntu:

```powershell
wsl --install
```

Dentro de WSL2, sigue la sección de Linux Ubuntu. El instalador oficial también ofrece un binario nativo para Windows, pero los gestores de paquetes de Windows no están soportados.

## Desinstalación

```bash
opencode uninstall
```

El comando detiene los servicios registrados y elimina el binario y la caché global. No elimina automáticamente tus sesiones, configuración ni estado; revísalos antes de borrarlos manualmente.

Para previsualizar lo que se eliminaría:

```bash
opencode uninstall --dry-run
```

## Verificación final

```bash
command -v opencode
opencode --version
opencode service status
```

Checklist:

- [ ] `opencode` está en el `PATH`.
- [ ] `opencode --version` responde.
- [ ] La TUI abre correctamente.
- [ ] `/connect` permite conectar un proveedor.
- [ ] `/models` muestra modelos disponibles.
- [ ] `opencode run "responde OK"` funciona en modo no interactivo.
- [ ] `AGENTS.md` y la configuración del proyecto están versionados.

## Referencias

- Introducción e instalación: <https://opencode.ai/v2/docs/>
- CLI: <https://opencode.ai/v2/docs/cli/>
- Configuración: <https://opencode.ai/v2/docs/config/>
- Proveedores: <https://opencode.ai/v2/docs/providers/>
- Solución de problemas: <https://opencode.ai/v2/docs/troubleshooting/>
- Repositorio: <https://github.com/anomalyco/opencode>
