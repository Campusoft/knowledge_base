# DeepSeek Harness: arquitectura y sistema de plugins

> Documento elaborado: 2026-11-13  
> Alcance: arquitectura Cordis, presets, composición y extensiones.  
> Lectura previa: [DeepSeek Harness](deepseek-harness.md), con descripción general, instalación, casos de uso y riesgos.

Este documento desarrolla las notas del resumen de video aportado sobre DeepSeek Harness (DSH), contrastando sus conceptos técnicos con la documentación oficial. El proyecto está en evolución: las APIs y composiciones deben comprobarse contra la versión utilizada.

## 1. Principio de diseño: todo es un plugin

DSH conecta un modelo con las capacidades necesarias para actuar como agente. Su principio **“everything is a plugin”** también comprende las piezas que otros sistemas incorporan en un núcleo fijo: el adaptador del modelo, el registro de herramientas, las sesiones y el propio ciclo del agente.

**Cordis** proporciona el marco para montar esas piezas, resolver sus dependencias y gestionar sus registros. Extender DSH consiste en incorporar plugins a una composición; reemplazar una capacidad puede hacerse mediante configuración, siempre que la nueva implementación respete el contrato que consumen los demás plugins.

La [arquitectura oficial](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/architecture.md) explica esta separación entre capacidades y composición.

## 2. Contexto compartido y servicios

Los plugins acceden a servicios mediante un contexto `ctx`. En lugar de importar directamente una implementación concreta, consumen la capacidad publicada con una clave conocida.

| Acceso | Responsabilidad |
|---|---|
| `ctx.sessions` | Historial de eventos y almacenamiento de sesiones en memoria. |
| `ctx.systemPrompt` | Composición de secciones del prompt y esquemas de herramientas. |
| `ctx.tools` | Registro de herramientas y flujo controlado de ejecución. |
| `ctx.agents` | Registro de agentes activos y contratos de coordinación. |
| `ctx.agentLoop` | Implementación del ciclo del agente. |
| `ctx.llm` | Contrato de interacción con adaptadores de modelos. |

Estas claves corresponden a la arquitectura consultada. Los nombres `ctx.session` y `ctx.model` del resumen inicial no se utilizan aquí como sustitutos de `ctx.sessions` y `ctx.llm`.

El contexto no garantiza que todos los servicios estén presentes en cualquier composición. Cordis también permite contextos con ámbitos y aislamiento; la imagen de un contexto compartido sirve para entender la comunicación, pero no implica un único objeto global sin límites.

## 3. Cuatro operaciones para entender un plugin

Las operaciones **provide, inject, register y listen** son una clasificación didáctica de lo que puede hacer un plugin, no cuatro métodos universales con esos nombres.

| Operación | Qué significa | Mecanismo habitual |
|---|---|---|
| **Provide** | Publicar una capacidad para otros plugins. | Un servicio reclama una clave de contexto; una subclase de `Service` puede usar `super(ctx, 'nombre')`. |
| **Inject** | Declarar qué servicios necesita para activarse. | `export const inject = ['tools']`. |
| **Register** | Incorporar recursos a un servicio. | Por ejemplo, `ctx.tools.register(...)`. |
| **Listen** | Observar o intervenir en eventos. | `ctx.on(...)`, respetando el contrato de cada evento. |

La inyección expresa dependencias: un plugin que requiere `tools` espera a que ese servicio esté disponible. Los servicios reducen el acoplamiento a implementaciones concretas, aunque los consumidores siguen dependiendo de sus contratos.

Consultar el [Cordis primer](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/cordis-primer.md) y el [tutorial de servicios](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/cordis-tutorial/03-services.md).

## 4. Presets del agente y perfiles de ejecución

Son dos niveles diferentes de configuración.

Un **perfil de ejecución** define la aplicación que se inicia y sus bundles. La arquitectura oficial recoge `web`, `headless`, `sdk`, `sdk-minimal` y `acp`.

Un **preset del agente** define la composición de plugins de ese agente. En la interfaz web aparecen los modos descritos en el resumen:

| Modo | Identificador | Propósito |
|---|---|---|
| **Standard** | `standard` | Composición general para tareas de programación, con herramientas y capacidades de apoyo. |
| **PTC** | `ptc` | Programmatic Tool Calling: organización de llamadas a herramientas mediante código. |
| **Minimal** | `minimal` | Composición reducida y explícita para experimentación y escenarios de evaluación. No significa ausencia de herramientas. |
| **Creator** | `cordis` | Personalizar DSH mediante conversación, creando plugins o combinando herramientas y prompts en modos propios. |

Por ejemplo, **`web` es un perfil; `standard` es un preset de agente dentro de la aplicación web**. El preset `minimal` tampoco equivale al perfil `sdk-minimal`, que posee una composición independiente.

La [documentación del selector de presets](https://github.com/deepseek-ai/deepseek-harness/blob/master/packages/client/ui-agent-preset/README.md) indica que PTC y Minimal se ofrecen al activar *Coding Tools*. La disponibilidad depende de la versión y de la configuración. Creator permite extender la aplicación mediante plugins; no debe interpretarse como una modificación autónoma e ilimitada del sistema.

Las declaraciones se pueden consultar en el [directorio oficial de presets](https://github.com/deepseek-ai/deepseek-harness/tree/master/packages/bundle/web-app/presets).

## 5. Anatomía de un plugin de herramienta

Una forma habitual de plugin es un módulo que exporta `apply(ctx)`, junto con su nombre y sus dependencias. Cordis también admite otras formas, como subclases de `Service`.

El siguiente ejemplo adapta el [tutorial oficial de una herramienta básica](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/user/develop/basic/tool.md):

```ts
import type { Context } from '@deepseek-ai/cordis'
import { defineTool } from '@deepseek-ai/dsh-tools'

export const name = 'greet-tool'
export const inject = ['tools']

export function apply(ctx: Context) {
  ctx.tools.register(defineTool({
    name: 'greet',
    description: 'Greet someone by name.',
    parameters: {
      name: {
        type: 'string',
        required: true,
        description: 'The name to greet',
      },
    },
    output: {
      schema: { type: 'string' },
      render: (_args, value) => [{ type: 'text', text: value }],
    },
    async execute(args) {
      return `Hello, ${args.name}!`
    },
  }))
}
```

`inject` declara la dependencia del registro de herramientas. Al activarse el plugin, `apply` registra `greet`. `parameters` describe la entrada y `output` declara el valor de salida y cómo convertirlo en contenido de texto. `execute` devuelve el valor declarado.

El ejemplo es documental, contrastado con la guía; no se ha ejecutado en esta base de conocimiento. Requiere integrarse en un entorno DSH con dependencias compatibles y una configuración que lo cargue.

## 6. Eventos y puntos de extensión

Los eventos permiten observar o intervenir sin importar directamente el ciclo del agente. La arquitectura distingue:

- **Eventos de sesión:** hechos persistentes del historial, difundidos mediante `session/event`.
- **Eventos del agente:** puntos de coordinación del trabajo en curso, como `agent/pre-step` y `agent/request`.
- **Eventos de capacidades:** extensiones de servicios como herramientas, archivos o telemetría.

No todos los listeners se comportan igual. En un evento de tipo *waterfall*, el listener recibe `next()` para delegar; omitir esa llamada puede interrumpir la cadena. Por eso, un listener que solo observa debe respetar el modo de despacho definido por el evento.

El flujo de herramientas utiliza puntos como `tools/pre-execute`, `tools/execute` y `tools/post-execute`. No se reproduce `tools/result` del resumen como nombre de evento verificado; los resultados persistentes aparecen como `tool/result` en el flujo documentado.


### Dispatch Modes (Modos de Despacho de Eventos)

En Cordis, cada evento tiene un **Dispatch Mode** que define cómo se ejecutan los listeners. Existen 5 modos:

| Mode       | ¿Se espera (await)? | Orden de ejecución                  | ¿Tiene valor de retorno? | Descripción |
|------------|---------------------|-------------------------------------|---------------------------|-------------|
| **emit**       | No                  | Orden de registro                   | No                        | Difusión síncrona (fire-and-forget). Solo notifica a los listeners. |
| **waterfall**  | No                  | Orden de registro                   | Sí                        | Estilo middleware. Cada listener recibe un `next()` y puede transformar, continuar o cortar la cadena. |
| **parallel**   | Sí                  | En paralelo                         | No                        | Todos los listeners se ejecutan concurrentemente y se esperan juntos. |
| **serial**     | Sí                  | Orden de registro                   | Sí                        | Se ejecutan en orden. El primer valor válido (no nulo/falso/undefined) gana y detiene el resto. |
| **bail**       | No                  | Orden de registro                   | Sí                        | Versión síncrona de `serial`. Se detiene en el primer valor que “baila”. |

**Resumen de uso:**
- `emit` → Solo notificar
- `parallel` → Ejecutar varios listeners al mismo tiempo
- `serial` / `bail` → Obtener el primer resultado válido
- `waterfall` → Interceptar, transformar o vetar el flujo (el más potente para extensiones)


## 7. Ciclo de vida y limpieza de recursos

Cordis asocia al ciclo de vida del plugin los efectos gestionados mediante sus mecanismos de registro. Al descargarlo, puede retirar listeners, herramientas y otros recursos registrados de esa forma.

La condición es que **cada recurso tenga una función de limpieza** (*disposer*), ya sea aportada por el helper utilizado o registrada mediante `ctx.effect()`. Un recurso creado fuera de esos mecanismos necesita limpieza explícita.

Esto facilita la descarga y la recarga, pero no significa que cualquier acción sea reversible. Retirar una herramienta del registro no deshace archivos que haya escrito, peticiones externas ni cambios persistentes realizados durante su ejecución. Tampoco debe suponerse un orden global de limpieza que resuelva cualquier dependencia: la guía recomienda agrupar el trabajo relacionado en un efecto cuando el orden sea relevante.

Consultar el [tutorial de ciclo de vida y efectos](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/cordis-tutorial/02-lifecycle-and-effects.md).

## 8. Composición mediante configuración

En el arranque normal de DSH, los perfiles componen un árbol de plugins mediante capas ordenadas:

```text
Bundles del perfil, en orden
        ↓
cordis.patch.yml del perfil
        ↓
Parche del directorio home de DSH
        ↓
Parches adicionales de la invocación (--patch)
        ↓
Composición efectiva de plugins
```

Las capas posteriores pueden reemplazar la configuración de una entrada por su identificador o insertar entradas nuevas. Los parches de preset actúan sobre los plugins de la declaración correspondiente.

`cordis.yml` también aparece en los tutoriales de Cordis para describir árboles de plugins. Conviene distinguir ese entorno didáctico del arranque de la aplicación DSH mediante perfiles y bundles.

La recarga en caliente depende de la composición y de su configuración: la arquitectura documenta valores predeterminados distintos entre perfiles. El sistema de efectos hace posible gestionar la limpieza; no garantiza que la recarga esté habilitada en cualquier ejecución.

## 9. Procedencia y fuentes

### Referencia audiovisual aportada

- **Autor indicado en las notas:** Alejandro AO, [@_alejandroao](https://x.com/_alejandroao).
- **Video enlazado en el material original:** [ver video en YouTube](https://www.youtube.com/watch?v=9F9x3MufdMQ).
- **Origen de estas notas:** resumen aportado por el usuario, descrito como un video de cuatro minutos. El material también menciona un tutorial de aproximadamente 35 minutos.
- **Pendiente de verificación audiovisual:** no se pudo comprobar el contenido del enlace ni establecer si corresponde al resumen o al tutorial completo; la duración, autoría y fecha de publicación no se presentan como verificadas.

La referencia se conserva para mantener la trazabilidad. Las precisiones sobre APIs, nombres y comportamiento de esta guía se apoyan en las fuentes oficiales siguientes.

### Documentación oficial consultada

- [Repositorio de DeepSeek Harness](https://github.com/deepseek-ai/deepseek-harness).
- [Arquitectura: servicios, eventos, perfiles y capas](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/architecture.md).
- [Cordis primer: contexto, dependencias y efectos](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/cordis-primer.md).
- [Tutorial de Cordis](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/cordis-tutorial/index.md).
- [Creación de una herramienta](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/user/develop/basic/tool.md).
- [Selector de presets del agente](https://github.com/deepseek-ai/deepseek-harness/blob/master/packages/client/ui-agent-preset/README.md).
- [Composiciones de los presets web](https://github.com/deepseek-ai/deepseek-harness/tree/master/packages/bundle/web-app/presets).

Consulta técnica realizada el **2026-11-13** mediante documentación oficial y Context7. Los enlaces apuntan a `master`, una referencia móvil; revisar la versión instalada antes de reutilizar ejemplos o configuraciones.
