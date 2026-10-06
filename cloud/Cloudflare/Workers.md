# Cloudflare Workers

**Cloudflare Workers** es una plataforma de computación serverless distribuida que permite ejecutar código en la red global de Cloudflare, cerca de quien realiza la solicitud. Se utiliza para crear APIs, procesar tráfico, personalizar respuestas, ejecutar tareas programadas e integrar servicios de Cloudflare sin administrar servidores virtuales ni contenedores.

> **Vigencia:** lenguajes, runtimes, límites y precios pueden cambiar. Esta guía refleja la documentación oficial consultada el **2 de octubre de 2026**. Verificar los enlaces oficiales antes de estimar costos o diseñar con límites específicos.

## Descripción y clasificación

Workers pertenece a la categoría de **computación serverless en el edge** (borde de la red). El desarrollador publica una unidad de código llamada Worker; Cloudflare la ejecuta cuando llega una solicitud HTTP, un evento programado, un mensaje de una cola u otro evento compatible.

“Serverless” significa que el equipo no administra servidores ni capacidad de cómputo subyacente. No significa que la ejecución sea ilimitada: hay límites de CPU, memoria, tamaño, solicitudes y otros recursos que dependen del plan y del producto.

Workers puede funcionar como:

- **Capa de entrada** delante de un sitio, API o servicio existente.
- **Backend serverless** para APIs y aplicaciones web.
- **Componente de una arquitectura distribuida**, conectado con bases de datos, almacenamiento, colas y otros Workers mediante bindings.
- **Ejecución orientada a eventos**, por ejemplo con Cron Triggers, Queues o Durable Workflows.

## Arquitectura

### Ejecución en la red global

Una solicitud que coincide con un dominio, ruta o dirección configurada llega a la red de Cloudflare. El runtime invoca el punto de entrada del Worker, que puede aplicar lógica y devolver una respuesta, consultar servicios vinculados o reenviar la solicitud a un origen.

Cloudflare ejecuta Workers sobre **workerd**, un runtime de código abierto basado en **V8**, el motor de JavaScript y WebAssembly. El código se ejecuta en **aislados (isolates)**: contextos ligeros y separados que permiten alojar muchos Workers en una misma instancia de runtime. Un aislado no equivale a una máquina virtual ni a un contenedor dedicado.

La plataforma distribuye la ejecución por sus ubicaciones globales. Las solicitudes sucesivas no tienen garantizado llegar a la misma instancia; un aislado puede desaparecer y volver a crearse. Por eso el estado mutable global solo debe usarse como caché local efímera, nunca como almacenamiento persistente ni como mecanismo de coordinación.

### Flujo de una solicitud HTTP

```text
Cliente
  │ HTTPS
  ▼
Red Cloudflare ──► ruta/dominio del Worker ──► runtime e isolate
                                                │
                          ┌─────────────────────┼──────────────────┐
                          ▼                     ▼                  ▼
                    lógica Worker         bindings/servicios   fetch a origen
                          │
                          ▼
                    Response al cliente
```

El manejador habitual con módulos ES recibe `request`, `env` y `ctx`:

- **`request`** contiene la solicitud web entrante, basada en las APIs estándar `Request` y `Response`.
- **`env`** proporciona las variables de entorno, secretos y **bindings** configurados para el Worker.
- **`ctx`** ofrece contexto de ejecución, incluida la posibilidad de registrar trabajo posterior a la respuesta mediante `ctx.waitUntil()`.

Ejemplo mínimo en TypeScript:

```ts
export default {
  async fetch(request, env, ctx): Promise<Response> {
    const url = new URL(request.url);

    if (url.pathname === "/saludo") {
      return Response.json({ mensaje: "Hola desde Workers" });
    }

    return new Response("No encontrado", { status: 404 });
  },
} satisfies ExportedHandler;
```

### Workers y estado

Conviene distinguir entre el código de ejecución y los servicios que almacenan o coordinan datos:

| Necesidad | Servicio habitual | Propiedad a considerar |
| --- | --- | --- |
| Configuración no sensible | Variables de entorno | Se declara por entorno de despliegue. |
| Credenciales | Secrets | Se administran como secretos; no se guardan en el repositorio. |
| Datos clave-valor con acceso global | Workers KV | Adecuado para lecturas distribuidas; considerar su modelo de consistencia y latencia de propagación. |
| Datos relacionales SQL | D1 | Base de datos SQLite distribuida, accedida mediante binding. |
| Archivos y objetos | R2 | Almacenamiento de objetos para archivos y datos binarios. |
| Estado coordinado por objeto | Durable Objects | Útil para estado con coordinación, sesiones, salas o presencia. |
| Acceso a bases de datos SQL existentes | Hyperdrive | Conexión optimizada desde Workers a bases de datos compatibles. |
| Procesamiento en segundo plano | Queues o Workflows | Desacopla el trabajo del tiempo de respuesta HTTP. |

La elección depende del patrón de acceso y de las garantías requeridas. No usar variables globales, el filesystem temporal ni KV como sustituto automático de una base de datos transaccional o de un coordinador de estado.

## Lenguajes soportados en 2026

La documentación de Cloudflare clasifica como lenguajes con **soporte de primera clase**:

| Lenguaje | Forma general de ejecución | Consideraciones |
| --- | --- | --- |
| JavaScript | Runtime V8 y APIs de Workers | Usa APIs web disponibles en Workers; no debe asumirse compatibilidad completa con Node.js. |
| TypeScript | Se compila/agrupa para ejecutarse en el runtime JavaScript | Wrangler y el flujo de desarrollo de Workers gestionan el bundling. |
| Python | Python Workers, ejecutado mediante Pyodide/WebAssembly e integrado con APIs y bindings de Workers | Revisar compatibilidad de paquetes; bibliotecas con extensiones nativas o dependencias del sistema pueden requerir wheels Pyodide/PyEmscripten y no estar disponibles. Algunas funciones de la biblioteca estándar, como `threading` y `multiprocessing`, no funcionan como en un servidor convencional. |
| Rust | Compilado a WebAssembly | Adecuado para lógica de alto rendimiento y componentes Wasm; seguir la plantilla y toolchain recomendadas por Cloudflare. |

También se pueden ejecutar módulos **WebAssembly (Wasm)** compilados desde lenguajes como C, C++, Kotlin o Go, entre otros. Esto no significa soporte de primera clase equivalente: hay que compilar para el entorno Wasm compatible y respetar las APIs y restricciones del runtime.

La lista de paquetes Python soportados depende de la disponibilidad de paquetes puros de Python o wheels compatibles con Pyodide/PyEmscripten. El filesystem de Python Workers es efímero y en memoria; los archivos desaparecen cuando se destruye el isolate. Para persistencia se debe usar un servicio de almacenamiento.

## Características principales

- Ejecución en la red distribuida de Cloudflare, próxima a usuarios y orígenes.
- Escalado gestionado sin aprovisionamiento manual de servidores.
- Runtime basado en V8 y APIs web como Fetch, Request, Response, Streams, Web Crypto y WebSockets, sujetas a las capacidades documentadas.
- **Bindings** tipados hacia servicios de Cloudflare y hacia otros Workers; evitan tener que administrar manualmente algunas credenciales y conexiones.
- Activación por HTTP y otros eventos, como temporizadores programados, colas y alarmas de Durable Objects.
- Desarrollo local con **Wrangler**, pruebas y despliegue desde CLI o integración de CI/CD.
- Variables, secretos, dominios personalizados, rutas y distintos entornos de despliegue.
- Logs, métricas y trazas mediante las funciones de observabilidad de Workers.
- Compatibilidad con frameworks web que puedan construirse para el runtime de Workers.

## Casos de uso

1. **APIs ligeras y backend para aplicaciones web:** validar solicitudes, autenticar usuarios, aplicar reglas de negocio y responder desde el edge.
2. **Middleware para un origen existente:** reescritura de URL, encabezados, control de acceso, redirecciones, geolocalización o personalización de contenido.
3. **Caché y optimización:** decidir qué respuestas se pueden cachear, adaptar la clave de caché y reducir viajes al origen.
4. **BFF (Backend for Frontend):** combinar varias APIs y devolver una respuesta ajustada a una aplicación web o móvil.
5. **Procesamiento asíncrono:** aceptar una operación rápidamente y encolar el trabajo que no necesita terminar dentro de la respuesta HTTP.
6. **Tareas programadas:** sincronizaciones, mantenimiento y llamadas periódicas mediante Cron Triggers.
7. **Aplicaciones con estado coordinado:** sesiones, coordinación de salas, contadores o presencia usando Durable Objects cuando se necesite consistencia y coordinación por entidad.
8. **Aplicaciones basadas en Python o Wasm:** reutilizar ecosistemas y componentes compatibles con el runtime, teniendo en cuenta las restricciones de paquetes.
9. **Protección o adaptación de servicios:** aplicar validaciones, autenticación o límites de uso antes de llegar al origen; complementarlo con controles de seguridad adecuados.

## Desarrollo y despliegue

El flujo típico usa Wrangler. La configuración suele estar en `wrangler.jsonc` o `wrangler.toml`; los proyectos nuevos deben seguir el formato recomendado por la versión actual de Wrangler.

Comandos comunes:

```bash
# Crear un proyecto desde una plantilla
npm create cloudflare@latest mi-worker

# Ejecutar el servidor de desarrollo local
npx wrangler dev

# Desplegar el proyecto configurado
npx wrangler deploy
```

La referencia del Worker en Wrangler identifica el archivo de entrada y puede declarar compatibilidad, bindings, rutas y entornos. Un fragmento ilustrativo:

```jsonc
{
  "name": "mi-worker",
  "main": "src/index.ts",
  "compatibility_date": "2026-10-02",
  "observability": { "enabled": true }
}
```

La fecha de compatibilidad fija el conjunto de comportamientos del runtime disponibles para el Worker. Usar una fecha reciente en un proyecto nuevo y actualizarla deliberadamente en proyectos existentes, revisando los cambios antes de desplegar.

Los secretos deben cargarse con los mecanismos de Wrangler o del entorno de CI, no escribirse como texto en el archivo de configuración. Separar desarrollo, staging y producción mediante entornos y bindings explícitos.

## Buenas prácticas

### Diseño y portabilidad

- Mantener las funciones de negocio pequeñas y con dependencias explícitas mediante `env` y bindings.
- No asumir que el Worker se ejecutará en una instancia fija ni que las variables globales persistirán entre solicitudes.
- Usar las APIs web estándar cuando sea posible. Revisar qué APIs de Node.js y qué módulos nativos admite `nodejs_compat`; no asumir que Workers es un proceso Node.js tradicional.
- Seleccionar el lenguaje por compatibilidad del ecosistema y costo operacional: TypeScript/JavaScript ofrece integración directa; Python tiene soporte de primera clase con salvedades de paquetes; Wasm sirve para módulos que puedan compilarse correctamente.

### Estado y almacenamiento

- Elegir KV, D1, R2, Durable Objects, Queues o Hyperdrive según el tipo de dato y las garantías que requiere la aplicación.
- Guardar estado persistente en un servicio diseñado para ello, no en memoria global o filesystem efímero.
- Usar Durable Objects cuando se necesite coordinar operaciones sobre una entidad con estado; no recrear coordinación con cachés locales.
- Evitar solicitudes seriales innecesarias: paralelizar operaciones independientes y reducir subrequests cuando el diseño lo permita.

### Seguridad

- Mantener claves y credenciales en Secrets y limitar permisos de los tokens de despliegue.
- Validar método, ruta, tamaño y contenido de las solicitudes; aplicar límites de frecuencia y controles de abuso cuando corresponda.
- No confiar automáticamente en encabezados enviados por el cliente. Si se usan encabezados de proxy, definir qué proxies son confiables.
- Usar Web Crypto para operaciones criptográficas y generación de valores aleatorios sensibles; no usar `Math.random()` para tokens o secretos.
- Revisar dependencias, permisos, bindings y datos registrados en logs para evitar filtrar información sensible.

### Rendimiento, confiabilidad y costos

- Mantener pequeño el bundle y retirar dependencias no usadas; el tamaño y el tiempo de inicialización importan.
- Definir límites de CPU adecuados para protegerse de ejecuciones inesperadas y revisar los límites del plan antes de procesar cargas grandes.
- Transmitir cuerpos grandes mediante streams cuando corresponda, en vez de almacenarlos enteros en memoria.
- Configurar timeouts, manejo de errores y reintentos con límites; evitar reintentos infinitos o duplicar operaciones no idempotentes.
- Pasar trabajo posterior a la respuesta a `ctx.waitUntil()` solo cuando la operación pueda ejecutarse en ese contexto; para trabajos duraderos o críticos, usar Queues o Workflows.
- Habilitar logs y trazas antes de producción. Usar logs estructurados, no registrar secretos ni cuerpos sensibles, y controlar el muestreo para gestionar volumen y costo.
- Medir subrequests, CPU, errores y latencias en escenarios reales; el tiempo de espera de red y el tiempo de CPU se contabilizan de forma distinta.
- Revisar la página actual de límites y precios. Los valores cambian y varían según el plan y el tipo de invocación.

## Límites y aspectos que revisar

Workers tiene límites de CPU, memoria por isolate, tamaño de bundle, subrequests, conexiones, número de bindings y tráfico según plan. También hay diferencias entre tipos de eventos, planes y productos vinculados. Los límites no deben copiarse de ejemplos antiguos sin verificar la tabla oficial.

Algunos puntos de arquitectura:

- El tiempo de espera por I/O no equivale al tiempo de CPU; aun así, una solicitud lenta puede afectar la experiencia del usuario y retener recursos.
- La memoria es por isolate, no una reserva de memoria exclusiva para cada solicitud.
- Las características del runtime no son iguales a las de Node.js, CPython de servidor o un sistema operativo convencional.
- Las bibliotecas que dependen de procesos, hilos, sockets arbitrarios, extensiones nativas o filesystem persistente pueden requerir cambios o no ser aptas.
- La ubicación del código no sustituye la elección correcta de almacenamiento: la consistencia, residencia de datos y latencia de cada servicio deben formar parte del diseño.
- Los costos dependen de solicitudes, CPU y servicios asociados; crear una estimación requiere consultar precios y uso actuales.

## Recomendaciones de adopción

Workers encaja bien cuando se busca ejecutar lógica HTTP de baja latencia distribuida, aplicar una capa de control cerca de los usuarios o construir un backend que pueda usar bindings y servicios de Cloudflare.

Antes de adoptarlo, conviene:

1. Confirmar que el lenguaje, framework y dependencias funcionan en el runtime, creando una prueba vertical con la funcionalidad más difícil.
2. Identificar el modelo de estado y consistencia requerido, y seleccionar el servicio de almacenamiento correspondiente.
3. Revisar límites, costos, región o ubicación de datos y requisitos de cumplimiento aplicables al caso.
4. Definir estrategia de desarrollo, staging, despliegue gradual y reversión.
5. Habilitar observabilidad y alertas desde el primer entorno desplegado.
6. Verificar qué partes de la solución dependen de servicios específicos de Cloudflare y documentar un plan de recuperación o portabilidad si es un requisito.

## Referencias oficiales

- [Cloudflare Workers: documentación general](https://developers.cloudflare.com/workers/)
- [Cómo funcionan Workers: runtime, isolates y ejecución distribuida](https://developers.cloudflare.com/workers/reference/how-workers-work/)
- [Lenguajes admitidos](https://developers.cloudflare.com/workers/languages/)
- [Python Workers](https://developers.cloudflare.com/workers/languages/python/)
- [Paquetes compatibles con Python Workers](https://developers.cloudflare.com/workers/languages/python/packages/)
- [Bindings](https://developers.cloudflare.com/workers/runtime-apis/bindings/)
- [Buenas prácticas de Workers](https://developers.cloudflare.com/workers/best-practices/workers-best-practices/)
- [Límites de Workers](https://developers.cloudflare.com/workers/platform/limits/)
- [Precios de Workers](https://developers.cloudflare.com/workers/platform/pricing/)
- [Configuración de Wrangler](https://developers.cloudflare.com/workers/wrangler/configuration/)
