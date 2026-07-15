# AGENTS.md


# Qwen Code

Qwen Code is a powerful command-line AI workflow tool adapted from Gemini CLI (details), specifically optimized for Qwen3-Coder models. It enhances your development workflow with advanced code understanding, automated tasks, and intelligent assistance.


# Codex CLI

## Plugins de Codex

Los **plugins de Codex** son paquetes instalables que agrupan capacidades relacionadas para reutilizarlas y distribuirlas como una sola unidad. Un plugin puede incluir:

- **Skills**: instrucciones, referencias y scripts para ejecutar correctamente un flujo de trabajo específico.
- **Apps o conectores**: integración con servicios como GitHub, Slack o Google Drive para consultar información o realizar acciones.
- **Servidores MCP**: herramientas y fuentes de datos adicionales expuestas mediante el Model Context Protocol.
- **Hooks**: automatizaciones que se ejecutan en determinados momentos del ciclo de vida de Codex.
- **Recursos**: iconos, plantillas u otros archivos necesarios para presentar o ejecutar el plugin.

En otras palabras, una *skill* describe principalmente **cómo realizar una tarea**, mientras que un plugin permite empaquetar esa skill junto con las herramientas, integraciones y automatizaciones que necesita. Todo plugin contiene un manifiesto obligatorio en `.codex-plugin/plugin.json`.

### Casos de uso

Los plugins son útiles cuando se necesita:

- Compartir un flujo de trabajo especializado con un equipo, por ejemplo, revisión de *pull requests*, análisis de seguridad o actualización de documentación.
- Conectar Codex con sistemas externos y combinar el acceso a sus datos con instrucciones de trabajo específicas.
- Distribuir varias skills relacionadas dentro de un único paquete instalable.
- Estandarizar procesos internos, como validaciones, generación de reportes, despliegues o atención de incidencias.
- Incorporar herramientas MCP y hooks sin pedir a cada usuario que configure manualmente todos sus componentes.

Antes de instalar un plugin, conviene revisar su autor, las capacidades que incorpora y los permisos solicitados. Instalar o habilitar un plugin no implica confiar automáticamente en sus hooks; Codex solicita que el usuario revise y autorice esas automatizaciones.

### Cómo instalar un plugin

#### Desde Codex CLI

1. Iniciar Codex en la terminal:

   ```bash
   codex
   ```

2. Abrir el navegador de plugins dentro de la sesión:

   ```text
   /plugins
   ```

3. Seleccionar un marketplace, buscar el plugin, revisar sus detalles y elegir la opción de instalación.
4. Si el plugin requiere acceso a un servicio externo, completar la autorización solicitada.
5. Iniciar una **sesión nueva de Codex** para que estén disponibles las skills y herramientas instaladas.

Desde el mismo navegador se puede desinstalar un plugin o, sobre uno ya instalado, presionar `Espacio` para habilitarlo o deshabilitarlo.

#### Desde la extensión de IDE

Abrir **Settings > Plugins**, buscar el plugin, revisar sus detalles e instalarlo para el host de Codex seleccionado. Después se debe iniciar un chat nuevo.

#### Agregar un marketplace propio

Cuando el plugin pertenece a un repositorio o catálogo privado, primero se registra su marketplace desde la terminal:

```bash
codex plugin marketplace add owner/repo
```

También se admite una URL Git o un directorio local:

```bash
codex plugin marketplace add https://github.com/organizacion/plugins.git
codex plugin marketplace add ./ruta-al-marketplace
```

Para consultar o actualizar los marketplaces configurados:

```bash
codex plugin marketplace list
codex plugin marketplace upgrade
```

Una vez agregado el marketplace, se abre `/plugins`, se selecciona ese origen y se instala el plugin desde el navegador.

Documentación oficial:

- [Plugins de Codex](https://developers.openai.com/codex/plugins)
- [Crear y distribuir plugins](https://developers.openai.com/codex/plugins/build)


# Gemini Code Assist

GEMINI.md

En términos sencillos, es el manual de instrucciones personalizado que le das a la IA para que entienda cómo debe comportarse específicamente en tu proyecto.

# Claude Code

Work with Claude directly in your codebase. Build, debug, and ship from your terminal, IDE, Slack, or the web. Describe what you need, and Claude handles the rest.




SKILL.md


# OpenCode

OpenCode is an open source AI coding agent. It’s available as a terminal-based interface, desktop app, or IDE extension.

https://opencode.ai/



# TODO - Revisiones

