# OpenAI Codex

> Documento revisado: 2026-11-12  
> Producto: [OpenAI Codex CLI](https://github.com/openai/codex)  
> Licencia del repositorio: Apache-2.0

## Descripción

**Codex CLI** es un agente de programación de OpenAI que se ejecuta localmente desde la terminal. Puede analizar un repositorio, responder preguntas sobre el código, proponer o realizar cambios en archivos y ejecutar comandos dentro de los límites configurados. El modelo aporta razonamiento; Codex aporta el harness que conecta el modelo con el repositorio, las herramientas, las instrucciones y los controles de ejecución.

Esta ficha se centra en Codex CLI. Codex también está disponible en otras experiencias, como extensiones de editor, la aplicación de escritorio y Codex en la nube; sus entornos y controles no son idénticos.

## Características

- **Trabajo sobre el repositorio local:** explora archivos, explica código, implementa cambios y ayuda a investigar fallos.
- **Ejecución de comandos:** puede usar herramientas de terminal para inspeccionar el proyecto, construirlo o ejecutar tareas autorizadas.
- **Controles de ejecución:** combina sandbox y políticas de aprobación para limitar acciones y pedir intervención cuando corresponda.
- **Instrucciones por proyecto:** lee archivos `AGENTS.md` para incorporar convenciones y orientación del repositorio. Pueden existir instrucciones globales y otras más específicas en subdirectorios.
- **Modos interactivo y automatizado:** permite trabajar conversacionalmente en la terminal y ejecutar tareas no interactivas con `codex exec`.
- **Integraciones:** admite MCP para conectar servidores de herramientas y datos; el conjunto de capacidades depende de la configuración.
- **Autenticación:** se puede iniciar sesión con una cuenta de ChatGPT compatible o configurar el uso mediante API key, según la modalidad y el acceso disponible.
- **Código abierto:** el repositorio público permite consultar la implementación, reportar problemas y contribuir.

## Arquitectura

```text
Usuario
  │ tarea e instrucciones
  ▼
Codex CLI (interfaz de terminal)
  │
  ├── Contexto del proyecto: archivos, historial e instrucciones AGENTS.md
  ├── Orquestación: prepara el contexto, solicita el siguiente paso al modelo
  │   y mantiene el ciclo de llamadas a herramientas
  ├── Controles: sandbox, permisos y solicitudes de aprobación
  └── Herramientas: lectura y edición de archivos, terminal y servidores MCP
         │                         │
         ▼                         ▼
     Modelo de OpenAI       Repositorio y entorno local
         │                         │
         └──── resultados de herramientas ────┘
```

En cada turno, Codex reúne la tarea, las instrucciones aplicables y el contexto relevante; pide al modelo una respuesta o una acción; valida y ejecuta las llamadas a herramientas según los controles activos; y devuelve los resultados al contexto. El ciclo puede repetirse hasta completar la tarea o solicitar orientación. El modelo no ejecuta por sí solo comandos en la máquina: el harness y el entorno local gestionan esa ejecución.

## Casos de uso

- **Orientarse en un código base:** pedir un resumen de la arquitectura, localizar flujos o explicar componentes.
- **Implementar cambios acotados:** añadir una funcionalidad, actualizar una interfaz o modificar una configuración.
- **Investigar errores:** reproducir un problema, seguir el flujo afectado y proponer una corrección.
- **Mantenimiento:** refactorizar, actualizar documentación, resolver deuda técnica o preparar migraciones.
- **Revisión de cambios:** examinar un diff y señalar problemas potenciales o áreas que necesitan pruebas.
- **Automatización local:** realizar tareas repetibles sobre un repositorio mediante `codex exec` y scripts revisables.
- **Integración con herramientas:** acceder a sistemas o datos adicionales mediante servidores MCP configurados.

## Instalación en Ubuntu Linux

La documentación del proyecto indica soporte para Linux. El instalador independiente oficial es la opción directa; instala el ejecutable en el entorno del usuario y puede ajustar el `PATH` si hace falta.

### Opción recomendada: instalador oficial

En una terminal Bash:

```bash
curl -fsSL https://chatgpt.com/codex/install.sh | sh
```

Abre una terminal nueva si el instalador informa que actualizó el `PATH`. Comprueba que el comando esté disponible:

```bash
codex --version
```

Inicia Codex desde la carpeta del proyecto:

```bash
cd /ruta/al/proyecto
codex
```

En el primer inicio, sigue el flujo de autenticación que ofrece la CLI. El instalador oficial también puede invocarse desde una copia descargada del script; en ese caso, revisa el contenido y las instrucciones vigentes antes de ejecutarlo.

### Alternativa: npm

Si ya tienes Node.js y npm instalados:

```bash
npm install -g @openai/codex
codex --version
```

La documentación del repositorio mantiene ambos métodos. Para funciones que requieren la instalación independiente administrada, como ciertas capacidades de servicio en segundo plano, usa el instalador oficial correspondiente.

### Instrucciones del proyecto

Coloca un archivo `AGENTS.md` en la raíz del repositorio para registrar cómo debe trabajar Codex en ese proyecto: comandos de construcción, convenciones, estructura y criterios de verificación. También puede haber archivos `AGENTS.md` en subdirectorios para instrucciones más específicas. El archivo es texto Markdown ordinario; no es una configuración obligatoria para que Codex se ejecute.

## Recomendaciones

1. **Empieza con una tarea concreta y revisable:** delimita el área de trabajo y los criterios de aceptación.
2. **Añade instrucciones útiles en `AGENTS.md`:** incluye comandos reales, convenciones del proyecto y restricciones relevantes; mantenlas actualizadas.
3. **Revisa el estado de Git y el diff:** confirma qué archivos cambiarán antes de aceptar o compartir el resultado.
4. **Mantén activos los controles de sandbox y aprobación adecuados:** concede acceso adicional solo cuando la tarea lo necesite.
5. **No expongas secretos en el prompt ni en archivos de instrucciones:** usa mecanismos de gestión de secretos y limita los permisos del proceso.
6. **Verifica los cambios con los comandos del proyecto:** construcción, pruebas y análisis estático según corresponda.
7. **Trata MCP y otras extensiones como código de terceros:** configura solo servidores confiables y revisa sus permisos.
8. **Para automatización, limita el alcance:** fija versiones y directorios de trabajo, conserva registros y requiere revisión humana para operaciones delicadas.

## Limitaciones

- **Puede equivocarse:** el análisis, el código generado y las conclusiones requieren revisión, especialmente en cambios complejos o de alto impacto.
- **Acceso local conlleva riesgo:** una herramienta autorizada puede cambiar archivos o ejecutar comandos en el entorno accesible al proceso.
- **El sandbox no es una garantía absoluta:** su cobertura depende del sistema operativo, la configuración y las capacidades habilitadas; no sustituye el aislamiento de secretos o recursos sensibles.
- **Los permisos y funciones varían:** el comportamiento depende de la versión de Codex, el sistema, el perfil de ejecución, la autenticación y la configuración.
- **La calidad depende del contexto:** instrucciones incompletas, repositorios grandes, dependencias no disponibles o documentación desactualizada pueden degradar los resultados.
- **Las integraciones amplían la superficie de acceso:** MCP y herramientas externas pueden introducir riesgos de seguridad, privacidad y disponibilidad.
- **El uso puede tener costos o límites:** dependen del tipo de inicio de sesión, el plan o el consumo de API aplicable.
- **No reemplaza revisión ni controles de entrega:** los cambios de producción, despliegues, publicaciones y operaciones irreversibles deben seguir el proceso del equipo.

## Fuentes

- [Repositorio y guía de inicio de Codex CLI](https://github.com/openai/codex)
- [Instalación y requisitos del proyecto](https://github.com/openai/codex/blob/main/docs/install.md)
- [Instrucciones `AGENTS.md` y configuración del agente](https://github.com/openai/codex/tree/main/docs)
- [Documentación oficial de Codex](https://developers.openai.com/codex/)
- [Documentación oficial de Codex CLI](https://developers.openai.com/codex/cli/)
