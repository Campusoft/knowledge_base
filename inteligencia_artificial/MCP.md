# Model Context Protocol (MCP)

MCP is an open protocol that standardizes how applications provide context to LLMs. Think of MCP like a USB-C port for AI applications. Just as USB-C provides a standardized way to connect your devices to various peripherals and accessories, MCP provides a standardized way to connect AI models to different data sources and tools.


MCP helps you build agents and complex workflows on top of LLMs. LLMs frequently need to integrate with data and tools, and MCP provides:

- A growing list of pre-built integrations that your LLM can directly plug into
- The flexibility to switch between LLM providers and vendors
- Best practices for securing your data within your infrastructure


# MCP Server


## Lista de servidores MCP

GitMCP es una herramienta gratuita y de código abierto que transforma cualquier repositorio público de GitHub o sitio de GitHub Pages en un servidor MCP (Model Context Protocol). Esto permite que asistentes de inteligencia artificial comprendan y trabajen con el código y la documentación de un proyecto en su contexto real, mejorando la precisión y reduciendo errores comunes causados por la falta de información actualizada.

https://gitmcp.io/

### Context7

Context7 es una plataforma que proporciona documentación actualizada de librerías y frameworks directamente a los asistentes de programación con inteligencia artificial. Mediante su servidor MCP, permite consultar información relevante y ejemplos de código de acuerdo con la tecnología y la versión utilizadas en un proyecto.

https://context7.com/

#### Casos de uso

- Consultar la documentación actualizada de una librería sin abandonar el entorno de desarrollo.
- Generar ejemplos de código basados en las API disponibles en la versión utilizada por el proyecto.
- Verificar la sintaxis, los parámetros y las opciones de configuración antes de implementar una funcionalidad.
- Identificar cambios de API y evitar ejemplos obsoletos al actualizar una dependencia.
- Ayudar a un agente de programación a instalar, configurar e integrar una librería o framework.
- Comparar enfoques de implementación utilizando la documentación oficial recuperada como contexto.
- Reducir respuestas incorrectas o inventadas al proporcionar al modelo información específica de la tecnología consultada.
