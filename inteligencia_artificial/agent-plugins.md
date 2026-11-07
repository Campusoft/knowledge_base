# Agent Plugins

[Agent Plugins](https://agent-plugins.org/) es un estándar abierto y neutral respecto al proveedor para empaquetar componentes reutilizables que amplían asistentes de IA. Su objetivo es que un mismo paquete pueda ser descubierto y cargado de forma coherente por clientes compatibles, reduciendo la necesidad de mantener formatos distintos para cada herramienta.

La especificación publicada consultada es la versión **1.0.0**. Agent Plugins define el formato portátil del paquete y una base de interoperabilidad; cada cliente conserva el control de la instalación, distribución, permisos, experiencia de usuario y capacidades específicas. Por ello, que un plugin siga el estándar no garantiza que todos los clientes soporten todos sus componentes.

## Características

- **Formato de paquete portable:** un directorio autocontenido con un manifiesto obligatorio y componentes opcionales en ubicaciones convencionales.
- **Neutralidad de proveedor:** el formato está diseñado para que múltiples clientes puedan compartir paquetes.
- **Skills reutilizables:** puede incluir skills compatibles con Agent Skills bajo `skills/`.
- **Integración con herramientas:** puede declarar servidores MCP locales o remotos en `mcp.json`.
- **Extensiones por cliente:** permite agregar datos y archivos propios del cliente en espacios de nombres de dominio inverso, por ejemplo `com.example.client/`.
- **Validación declarativa:** los manifiestos y la configuración MCP declaran la versión del esquema mediante `$schema` y cuentan con esquemas JSON publicados.
- **Límites de paquete:** las rutas que el estándar define como relativas deben permanecer dentro de la carpeta del plugin.

## Especificación y estructura

Un paquete mínimo incluye `plugin.json` en la raíz. Skills, configuración MCP y extensiones son opcionales.

```text
mi-plugin/
├── plugin.json
├── skills/
│   └── revisar-pr/
│       ├── SKILL.md
│       ├── references/
│       └── scripts/
├── mcp.json
└── com.example.client/
    └── hooks/
```

### Manifiesto `plugin.json`

El manifiesto identifica el paquete y la versión del estándar objetivo. En la versión 1.0.0, los campos portátiles permitidos son `$schema` y `name` (obligatorios), además de `version`, `description`, `author`, `homepage`, `repository`, `license`, `keywords` y `extensions` (opcionales).

El nombre debe tener entre 1 y 64 caracteres, usar minúsculas ASCII, números, guiones o puntos; comenzar y terminar con carácter alfanumérico y no contener `--` ni `..`.

### Componentes estándar en v1

| Componente | Ubicación | Propósito |
|---|---|---|
| Skills | `skills/<nombre>/SKILL.md` | Instrucciones reutilizables para tareas del agente; siguen la especificación Agent Skills. |
| Servidores MCP | `mcp.json` | Configuración para conectarse a servidores MCP mediante `stdio`, `streamable-http` o el transporte heredado `sse`. |
| Extensiones | `com.<organizacion>.<cliente>/` y `extensions` | Comportamiento o metadatos específicos de un cliente, fuera del núcleo portable. |

Agent Plugins v1 estandariza skills y servidores MCP como tipos de componente. No establece otros componentes estándar. El cliente descubre skills como directorios inmediatos de `skills/` que contengan un `SKILL.md`; no debe buscar recursivamente en niveles más profundos.

`mcp.json` usa su propio `$schema` y un objeto `mcpServers`. Para `stdio`, se configura un ejecutable y argumentos separados; para conexiones remotas, se define un endpoint HTTP(S) y el tipo de transporte. Las credenciales y la autorización no cuentan con un mecanismo portable definido por v1, por lo que son responsabilidad del cliente.

### Qué no define el estándar

El formato no obliga a los clientes a usar un método común para instalar, publicar, actualizar o habilitar plugins. Tampoco define un sistema de permisos o una sandbox portable para los procesos ejecutados. Cada cliente puede soportar solo parte de la especificación o añadir capacidades propias mediante extensiones.

## Casos de uso

- **Compartir flujos de trabajo:** distribuir una skill de revisión de código, preparación de reportes o atención de incidencias entre varios agentes compatibles.
- **Conectar servicios internos:** incluir configuración para un servidor MCP que exponga información o acciones de una API corporativa.
- **Publicar herramientas reutilizables:** mantener manifiesto, instrucciones y configuración de integración en un paquete versionado.
- **Mantener compatibilidad parcial:** ofrecer una skill portable y una extensión adicional para un cliente que necesite una integración propia.
- **Construir clientes compatibles:** implementar descubrimiento, validación y carga de los componentes definidos por el estándar.

## Recomendaciones

- Consultar la lista de [clientes compatibles](https://agent-plugins.org/compatible-clients) y confirmar qué versión y componentes soporta cada cliente antes de distribuir el paquete.
- Usar los esquemas oficiales de la versión declarada y validar `plugin.json` y `mcp.json` en integración continua.
- Mantener el paquete autocontenido y comprobar que las rutas no salgan de su raíz; no usar enlaces simbólicos o puntos de reanálisis para escapar de ella.
- Tratar cada skill como instrucciones que pueden influir en el comportamiento del agente: revisarlas, mantenerlas acotadas y documentar scripts y referencias incluidos.
- Revisar los servidores MCP por separado: comandos ejecutados, argumentos, variables de entorno, endpoints, datos accesibles y acciones que pueden realizar.
- No incluir tokens, contraseñas ni secretos en `plugin.json`, `mcp.json` o cabeceras HTTP. La configuración de autorización depende del cliente y de su mecanismo seguro de credenciales.
- Usar HTTPS para servicios remotos fuera de loopback y limitar permisos y datos al mínimo necesario.
- Separar la parte portable de las extensiones específicas del cliente, usando espacios de nombres de dominio inverso para evitar colisiones.
- Versionar y documentar cambios incompatibles; SemVer y los identificadores de licencia SPDX son recomendaciones del estándar para sus metadatos.
- No asumir que el estándar proporciona aislamiento de seguridad: evaluar y controlar la ejecución según el cliente y el entorno donde se instala el plugin.

## Ejemplo: plugin para preparar una revisión de pull request

Este ejemplo crea una skill que guía al agente para resumir una pull request y revisar riesgos antes de solicitar revisión humana. No incluye servidor MCP; puede usarse como paquete mínimo en un cliente que admita Agent Plugins y Agent Skills.

```text
pr-review-helper/
├── plugin.json
└── skills/
    └── preparar-revision/
        └── SKILL.md
```

`plugin.json`:

```json
{
  "$schema": "https://agent-plugins.org/schemas/1.0.0/plugin.schema.json",
  "name": "pr-review-helper",
  "version": "1.0.0",
  "description": "Ayuda a preparar un resumen y una lista de comprobación para una pull request.",
  "keywords": ["pull-request", "code-review"]
}
```

`skills/preparar-revision/SKILL.md`:

```markdown
---
name: preparar-revision
description: Resume una pull request y prepara una lista de comprobación para revisión humana.
---

Cuando te pidan preparar una revisión de pull request:

1. Lee el contexto y los cambios que el usuario haya proporcionado.
2. Resume el propósito y los archivos o áreas afectados.
3. Señala riesgos, casos límite y comprobaciones que falten, sin afirmar que ejecutaste pruebas que no ejecutaste.
4. Organiza los hallazgos por prioridad y cita archivos o fragmentos disponibles.
5. Termina con preguntas concretas para la persona revisora.

No apruebes, combines ni publiques cambios. Pide confirmación antes de ejecutar acciones externas.
```

El manifiesto identifica el plugin; la carpeta `skills/` contiene una skill cuyo nombre coincide con el directorio y con el campo `name` del encabezado. Un cliente compatible descubre y valida el manifiesto y la skill. La disponibilidad de la skill y la forma en que se activa dependen del cliente.

## Referencias

- [Sitio oficial de Agent Plugins](https://agent-plugins.org/)
- [Especificación Agent Plugins v1.0.0](https://agent-plugins.org/specification)
- [Guía para crear un plugin](https://agent-plugins.org/plugin-authors/build-an-agent-plugin)
- [Formato del manifiesto](https://agent-plugins.org/plugin-authors/manifest)
- [Clientes compatibles](https://agent-plugins.org/compatible-clients)
- [Especificaciones de Agent Skills](https://agentskills.io/)
