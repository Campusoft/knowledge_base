# Hono

Hono es un framework web ligero para crear APIs y aplicaciones con JavaScript o TypeScript. Está construido sobre estándares web, como `Request` y `Response`, y puede ejecutarse en runtimes como Cloudflare Workers, Deno, Bun y Node.js, además de varias plataformas serverless y de edge. Su diseño permite compartir buena parte del código entre entornos, aunque cada runtime conserva diferencias de despliegue y APIs disponibles.

Esta guía explica sus características, arquitectura, escenarios de uso, limitaciones y prácticas recomendadas. La documentación oficial de Hono es la referencia para los detalles de cada runtime y versión.

## Características

- **Enrutamiento:** rutas declarativas por método HTTP, parámetros, grupos y subaplicaciones.
- **Middleware:** funciones que se pueden aplicar a toda la aplicación, a una ruta o a un método y una ruta; sirven para logging, CORS, autenticación y otras tareas compartidas.
- **Estándares web:** el contexto de Hono trabaja con las APIs Fetch y facilita mover lógica entre runtimes compatibles.
- **TypeScript:** permite tipar el contexto y los valores compartidos por middleware, manteniendo esos tipos disponibles en las rutas posteriores.
- **Extensible:** dispone de middleware, helpers y adaptadores para distintos entornos; también se pueden crear middleware propios.
- **Ligero y componible:** puede funcionar como servicio pequeño o como base para una API mayor organizada en módulos y routers.
- **Funciones HTTP comunes:** respuestas JSON, texto, HTML, archivos y streaming, de acuerdo con las capacidades del runtime.

## Arquitectura

Una aplicación comienza con una instancia de `Hono`. Se registran middleware y rutas, y luego se conecta la aplicación a un runtime mediante el adaptador o la convención de despliegue correspondiente.

```text
Cliente HTTP
  -> runtime o adaptador (Node.js, Worker, Deno, Bun, etc.)
  -> aplicación Hono
       -> middleware aplicable en orden
       -> router selecciona método y ruta
       -> handler recibe Context y produce Response
  -> respuesta HTTP
```

- **Aplicación (`Hono`)**: conserva las rutas, middleware y opciones de manejo de errores.
- **Router**: encuentra el handler que corresponde a la ruta y método solicitados.
- **Middleware**: envuelve el procesamiento y puede preparar el contexto, responder antes de llegar a la ruta o continuar con `next()`.
- **Context (`c`)**: ofrece acceso a la solicitud, parámetros, entorno y métodos para construir respuestas.
- **Handler**: ejecuta la operación de la ruta y retorna una respuesta basada en Web Standards.
- **Adaptador o runtime**: acepta la conexión y entrega la solicitud Fetch a Hono. En Node.js, por ejemplo, se puede usar `@hono/node-server`; en Workers se integra con el entry point requerido por la plataforma.

El orden de registro del middleware importa. Registra primero las tareas que deben aplicarse antes de las rutas y usa patrones de ruta explícitos para limitar su alcance. Para compartir lógica entre áreas, se pueden separar routers y montarlos en una aplicación principal.

## Casos de uso

- APIs HTTP pequeñas y medianas, con despliegue en Node.js, Bun o Deno.
- Endpoints de baja latencia en runtimes edge, cuando la lógica y las dependencias son compatibles con el entorno.
- Funciones serverless que necesitan enrutamiento y middleware sin un framework de aplicación más amplio.
- Proxies y gateways ligeros entre un frontend y servicios existentes.
- Servicios auxiliares, webhooks y backends para aplicaciones web o móviles.
- Código compartido entre runtimes, cuando conviene reutilizar rutas y middleware que dependen de estándares web.

## Limitaciones y aspectos a evaluar

- **No es un marco de aplicación completo:** ofrece menos convenciones integradas para módulos, inyección de dependencias y organización empresarial que frameworks como NestJS. El equipo debe definir cómo estructurar dominios y dependencias.
- **Portabilidad condicionada:** las rutas basadas en Fetch son portables, pero acceso a filesystem, sockets, base de datos, variables de entorno, tareas en segundo plano y límites de ejecución varían entre runtimes.
- **Ecosistema por entorno:** un paquete o middleware puede funcionar en Node.js y no estar disponible, o comportarse distinto, en un Worker u otro runtime restringido.
- **Validación de datos:** TypeScript no valida solicitudes en tiempo de ejecución. Se necesita una biblioteca de esquemas o validadores y una política explícita para datos de entrada y salida.
- **Rendimiento dependiente de la carga:** el framework ligero no elimina costos de base de datos, serialización, red o cold starts. Hay que medir en el entorno donde se desplegará.
- **Complejidad del sistema:** en APIs grandes, la flexibilidad puede llevar a convenciones inconsistentes si no se acuerdan límites, estructura y manejo de errores.
- **Compatibilidad de APIs y versiones:** confirme los requisitos de versión del runtime, del adaptador y de los middleware elegidos antes de actualizar o desplegar.

## Recomendaciones

1. **Elegir el runtime junto con el diseño.** Aclare si el servicio se ejecutará en Node.js, Workers, Deno, Bun o serverless; determine límites de CPU, memoria, duración y acceso a red y archivos.
2. **Separar rutas por dominio.** Mantenga cada grupo de endpoints y sus middleware cerca de su funcionalidad. Monte routers en una aplicación principal y evite acumular toda la lógica en un único archivo.
3. **Mantener handlers pequeños.** Use handlers para coordinar entrada, caso de uso y respuesta HTTP; coloque lógica de negocio en funciones o servicios con dependencias claras.
4. **Controlar el orden y alcance del middleware.** Aplique autenticación, CORS, logging y otros componentes solo donde corresponda. Evite middleware global que añada trabajo innecesario a endpoints públicos o internos.
5. **Validar en tiempo de ejecución.** Valide parámetros, query strings, cabeceras y cuerpos con esquemas; responda errores consistentes y no asuma que los tipos estáticos protegen el servicio.
6. **Definir manejo de errores.** Centralice el formato de errores esperados y registre fallas imprevistas sin revelar detalles internos, credenciales ni datos sensibles.
7. **Usar interfaces portables con criterio.** Mantenga el núcleo de rutas en Web Standards cuando la portabilidad importe y encapsule las APIs específicas del runtime en adaptadores propios.
8. **Probar rutas y middleware.** Pruebe solicitudes y respuestas con las utilidades de testing de Hono, y añada pruebas de integración o despliegue para validar el runtime real.
9. **Medir el despliegue objetivo.** Revise latencia, memoria, cold starts, conexiones y límites del proveedor con tráfico representativo; no elija un runtime por benchmarks genéricos.
10. **Escoger otra opción si se necesita más estructura.** Si el equipo requiere módulos, DI, patrones uniformes, integraciones empresariales y una estructura prescrita, compare Hono con NestJS u otro framework más completo.

## Ejemplo mínimo

```ts
import { Hono } from 'hono'

const app = new Hono()

app.get('/health', (c) => c.json({ status: 'ok' }))

export default app
```

El formato de exportación y el comando de arranque dependen del runtime. Consulte la guía oficial del entorno elegido antes de desplegar este ejemplo.

## Referencias oficiales

- [Hono](https://hono.dev/docs)
- [Web Standards y runtimes](https://hono.dev/docs/concepts/web-standard)
- [Middleware](https://hono.dev/docs/guides/middleware)
- [Mejores prácticas](https://hono.dev/docs/guides/best-practices)
- [Node.js](https://hono.dev/docs/getting-started/nodejs)
- [Testing](https://hono.dev/docs/guides/testing)
- [Cloudflare Workers](https://hono.dev/docs/getting-started/cloudflare-workers)
