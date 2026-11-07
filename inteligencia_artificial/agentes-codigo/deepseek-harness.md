# DeepSeek Harness

> Documento revisado: 2026-11-03  
> Proyecto: [deepseek-ai/deepseek-harness](https://github.com/deepseek-ai/deepseek-harness)  
> Licencia del repositorio: MIT  
> Estado indicado por el proyecto: *developer preview*

## Descripción

**DeepSeek Harness (`dsh`)** es un entorno de ejecución de agentes de IA de código abierto, desarrollado por DeepSeek. Permite ejecutar agentes de programación y automatización y extender sus capacidades mediante plugins. No es un modelo de lenguaje: conecta un modelo con herramientas, sesiones, políticas y una interfaz de ejecución.

Su principio de diseño es **“everything is a plugin”**. Incluso el adaptador del modelo, el registro de herramientas, el historial de sesión y el ciclo del agente se integran como plugins reemplazables. El proyecto se basa en **Cordis**, que coordina plugins, servicios, eventos y efectos en un contexto compartido.

## Arquitectura

```text
Usuario / aplicación
        │
        ├── Web UI
        ├── CLI headless
        ├── SDK / JSON-RPC
        └── ACP
             │
        Perfil + bundles + parches de configuración
             │
        Contexto Cordis y plugins
        ├── Adaptador LLM y prompt del sistema
        ├── Ciclo del agente y registro de agentes
        ├── Herramientas y políticas de ejecución
        ├── Sesiones e historial persistente
        ├── Telemetría y otros servicios
             │
        Modelo, herramientas, archivos y entorno local
```

### Componentes principales

| Componente | Responsabilidad |
|---|---|
| **Cordis** | Compone plugins en un contexto común; ofrece servicios, eventos tipados y descarga reversible de efectos. |
| **Plugins** | Añaden o reemplazan capacidades: modelos, herramientas, sesiones, interfaz y ciclo del agente. |
| **Perfiles** | Definen una composición ejecutable de bundles y configuración. El proyecto incluye `web`, `headless`, `sdk`, `sdk-minimal` y `acp`. |
| **Bundles y parches** | Paquetes distribuibles que aportan plugins y configuración; las capas posteriores pueden reemplazar entradas mediante parches. |
| **Sesiones** | Mantienen el historial de eventos y permiten continuar el trabajo según el perfil y la configuración usados. |
| **Ciclo del agente** | Prepara el contexto, solicita una respuesta al modelo, gestiona llamadas a herramientas y continúa hasta cerrar el turno. |
| **Herramientas y políticas** | Registran capacidades disponibles y aplican controles antes de su ejecución. |
| **Interfaces** | Web para interacción, headless para una ejecución por tarea y perfiles de integración para SDK o ACP. |

### Ciclo de una tarea

1. El usuario o una aplicación envía una tarea al perfil activo.
2. El perfil reúne sus bundles y parches, y Cordis monta los plugins.
3. El agente prepara el prompt, el historial y las herramientas habilitadas para el paso.
4. El adaptador envía la solicitud al modelo configurado.
5. Si el modelo solicita herramientas, el harness aplica la política configurada y ejecuta las permitidas.
6. Los resultados se incorporan a la sesión y alimentan el siguiente paso.
7. El agente finaliza el turno y presenta la respuesta o emite eventos para la aplicación cliente.

## Características

- **Composición modular:** reemplazar capacidades o incorporar plugins sin modificar un núcleo monolítico.
- **Perfiles de ejecución:** separar la interfaz web, la ejecución sin interfaz y las integraciones de SDK/ACP.
- **Personalización por capas:** combinar bundles ordenados con configuración del perfil, parches del usuario y parches adicionales.
- **Herramientas para agentes:** exponer capacidades al modelo con un registro y un flujo controlado de ejecución.
- **Sesiones e historial:** conservar eventos de sesión y, según la configuración, reanudar trabajo previo.
- **Extensión por eventos:** observar o interceptar partes del ciclo mediante puntos de extensión de sesión, agente y capacidades.
- **Inspección de configuración:** la CLI puede mostrar la composición efectiva del perfil antes de ejecutarlo.
- **Licencia MIT:** el repositorio principal declara esta licencia; las dependencias de terceros tienen avisos propios.

## Formas de uso

El repositorio documenta el arranque de la interfaz web mediante npm:

```bash
npx @deepseek-ai/dsh web
```

Por defecto, la interfaz se sirve localmente en `http://127.0.0.1:3080`. Para iniciar sin abrir el navegador:

```bash
npx @deepseek-ai/dsh web --no-open
```

La ejecución desde el código fuente está documentada con Node.js y pnpm:

```bash
git clone https://github.com/deepseek-ai/deepseek-harness.git
cd deepseek-harness
pnpm install
pnpm run build
pnpm dsh web
```

La sintaxis y las opciones pueden cambiar durante el *developer preview*. Consultar siempre la guía actual del perfil deseado antes de automatizar comandos.

## Casos de uso

Los siguientes son usos posibles del producto; la adecuación debe comprobarse con el perfil, los plugins y los modelos disponibles en la versión instalada.

- **Asistencia de programación:** explorar un repositorio, proponer y editar código, y apoyar tareas de mantenimiento mediante herramientas autorizadas.
- **Automatización local:** ejecutar tareas repetibles sobre archivos o comandos en un entorno acotado.
- **Prototipos de agentes:** probar diferentes combinaciones de modelos, herramientas y políticas mediante plugins y perfiles.
- **Integración en aplicaciones:** invocar un agente desde un programa mediante el SDK u otro perfil de integración disponible.
- **Agentes sin interfaz gráfica:** ejecutar una tarea desde CLI en scripts o flujos internos, revisando cuidadosamente permisos y resultados.
- **Desarrollo de extensiones:** crear plugins o bundles para añadir herramientas, adaptadores y otras capacidades reutilizables.

## Limitaciones y riesgos

- **Versión preliminar:** el proyecto advierte que está en *developer preview* y que habrá cambios incompatibles.
- **Sin auditoría de seguridad declarada:** el aviso oficial no lo considera seguro ni listo para producción.
- **Acceso potencialmente amplio:** puede ejecutar código y comandos generados por el modelo, cargar plugins de terceros y acceder a los archivos, procesos, credenciales y red disponibles en el entorno.
- **Aislamiento imperfecto:** sandbox, aprobación y permisos reducen riesgos, pero no garantizan aislamiento ni protegen recursos a los que la aplicación ya tiene acceso.
- **Riesgo de instrucciones maliciosas o errores:** respuestas incorrectas, configuraciones débiles o plugins no confiables pueden borrar o alterar archivos y exponer datos.
- **Dependencia de la composición instalada:** las capacidades reales dependen del perfil, los bundles, los plugins y la configuración montados; no todos los perfiles ofrecen la misma superficie.
- **Compatibilidad en evolución:** los cambios de versión pueden afectar perfiles, parches, plugins e integraciones propias.
- **Costo y calidad variables:** dependen del modelo, del proveedor y de las llamadas a herramientas configuradas.

## Recomendaciones

1. **Probar primero en un entorno aislado:** usar una máquina virtual o contenedor desechable, un repositorio de prueba y datos no sensibles.
2. **Empezar con permisos mínimos:** limitar directorios, credenciales, comandos y acceso a red; no habilitar capacidades que la tarea no necesita.
3. **Revisar el aviso de seguridad oficial** antes de instalar o ejecutar el proyecto.
4. **Inspeccionar los plugins y sus dependencias:** tratar cualquier plugin externo como código con los permisos del proceso.
5. **Revisar la configuración efectiva:** utilizar las opciones de inspección del perfil y confirmar qué plugins y herramientas se cargarán.
6. **Fijar versiones en automatizaciones:** evitar depender de una versión flotante durante el preview y actualizar de forma deliberada.
7. **Guardar cambios en Git y respaldar datos:** revisar diffs y resultados antes de aceptar modificaciones del agente.
8. **Mantener aprobación humana** para cambios destructivos, publicación, despliegue, acceso a secretos u operaciones irreversibles.
9. **No usarlo como único control de seguridad** ni con datos de producción hasta que el nivel de seguridad y madurez requerido esté demostrado.
10. **Evaluar tareas representativas:** medir calidad, tasa de error, latencia, costo y comportamiento ante instrucciones no confiables antes de ampliar su autonomía.

## ¿Cuándo conviene evaluarlo?

Puede ser una opción para equipos que quieren estudiar una arquitectura de agente extensible por plugins, experimentar con perfiles de ejecución o construir integraciones propias. Para un flujo de producción con datos sensibles o acciones de alto impacto, la condición de preview y el aviso de seguridad exigen una evaluación y controles adicionales antes de adoptarlo.

## Ubicación recomendada en esta base

Este documento está en `inteligencia_artificial/agentes-codigo/`, junto a la documentación de OpenCode, porque DeepSeek Harness es una herramienta concreta para agentes de programación. El concepto general de *agent harness* se mantiene en [inteligencia_artificial_agent_harness.md](../inteligencia_artificial_agent_harness.md); allí se puede enlazar esta ficha como ejemplo de implementación.

## Fuentes

- [Repositorio oficial y README](https://github.com/deepseek-ai/deepseek-harness)
- [Arquitectura oficial](https://github.com/deepseek-ai/deepseek-harness/blob/master/docs/architecture.md)
- [Aviso oficial de seguridad](https://github.com/deepseek-ai/deepseek-harness/blob/master/SAFETY.md)
- [Documentación oficial](https://deepseek-harness.github.io/deepseek-harness/)
- [Sitio de DeepSeek Harness](https://www.deepseek.com/harness/en/)
