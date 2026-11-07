# NestJS

NestJS es un framework progresivo para crear aplicaciones de servidor con Node.js. Su estructura modular, la inyección de dependencias y el uso de TypeScript ayudan a mantener sistemas de backend organizados a medida que crecen. Puede servir APIs HTTP y también trabajar con GraphQL, WebSockets y microservicios. Nest usa Express por defecto y permite elegir Fastify como adaptador HTTP.

Esta guía resume sus conceptos, arquitectura, escenarios adecuados y prácticas recomendadas. La referencia principal es la [documentación oficial de NestJS](https://docs.nestjs.com/).

## Características

- **Módulos por funcionalidad:** agrupan controladores y proveedores relacionados, y definen explícitamente qué servicios se exportan o importan.
- **Inyección de dependencias:** el contenedor crea y conecta servicios; facilita sustituir dependencias en pruebas y mantener límites claros.
- **Controladores y decoradores:** describen rutas, parámetros y metadatos con una sintaxis declarativa como `@Controller()` y `@Get()`.
- **Herramientas para el ciclo HTTP:** middleware, guards, interceptors, pipes y exception filters resuelven responsabilidades transversales en etapas definidas.
- **TypeScript y JavaScript:** TypeScript es la opción habitual, aunque también es posible desarrollar en JavaScript.
- **Adaptadores y transportes:** Express o Fastify para HTTP; paquetes oficiales para GraphQL, WebSockets y distintos transportes de microservicios.
- **Pruebas y CLI:** el CLI genera módulos, controladores y servicios; el paquete de testing ofrece utilidades para pruebas unitarias y de integración.

## Arquitectura y ciclo de una petición

Una aplicación Nest empieza en un módulo raíz, que importa los módulos de funcionalidad. Cada módulo delimita una parte del dominio, por ejemplo clientes, facturación o inventario. Un módulo registra sus controladores y proveedores y puede exportar servicios para que otros módulos los consuman.

- **Módulo (`@Module`)**: declara `imports`, `controllers`, `providers` y `exports`.
- **Controlador (`@Controller`)**: recibe solicitudes y define los endpoints. Debe coordinar la entrada y salida HTTP, delegando las reglas de negocio.
- **Proveedor (`@Injectable`)**: servicio, repositorio u otra clase que Nest puede crear e inyectar. Conviene mantener aquí la lógica de aplicación y dominio según el diseño del proyecto.
- **Contenedor de dependencias**: resuelve las dependencias registradas en los módulos, normalmente mediante inyección por constructor.

En una ruta HTTP, la solicitud pasa por middleware; luego pueden ejecutarse guards para autorización, interceptors antes del controlador y pipes para validar o transformar argumentos. El controlador invoca proveedores. La respuesta puede atravesar la fase posterior de interceptors; las excepciones se procesan mediante exception filters. El orden y los componentes aplicables dependen del alcance global, de controlador o de método.

```text
Solicitud
  -> middleware
  -> guards
  -> interceptors (antes)
  -> pipes
  -> controlador -> proveedor(es)
  -> interceptors (después)
  -> respuesta

Excepción -> exception filters
```

Este modelo permite ubicar cada preocupación: autorización en guards, validación de entrada en pipes, métricas o transformación común en interceptors y formato de errores en filters. No es necesario usar todos los componentes en cada endpoint.

## Casos de uso

- APIs REST para aplicaciones web y móviles, especialmente cuando hay varios dominios o equipos trabajando sobre el mismo backend.
- Sistemas empresariales que se benefician de módulos explícitos, convenciones compartidas, TypeScript y pruebas estructuradas.
- APIs GraphQL cuando se necesita un esquema tipado y una capa de resolución organizada por dominio.
- Aplicaciones en tiempo real con WebSockets, como notificaciones, colaboración o seguimiento de estado.
- Procesamiento distribuido con microservicios y mensajería cuando existe una necesidad concreta de comunicación asíncrona o despliegue independiente.
- Servicios HTTP que requieren el ecosistema de Express o Fastify y una estructura de aplicación más prescriptiva.

## Cuándo evaluar otra opción

Nest añade estructura y conceptos. Para un servicio pequeño con pocas rutas y requisitos sencillos, un framework minimalista puede requerir menos configuración. Si el equipo no necesita módulos, DI o la integración de varios transportes, compare el costo de las convenciones de Nest con las necesidades reales. Elegir Fastify como adaptador no convierte automáticamente a toda la aplicación Nest en más rápida: mida el sistema bajo cargas y rutas representativas.

## Recomendaciones

1. **Organizar por dominio o funcionalidad.** Cree módulos que reflejen capacidades del producto; evite módulos enormes y dependencias circulares. Mantenga las dependencias entre dominios explícitas.
2. **Mantener controladores delgados.** Valide la forma de la solicitud y delegue casos de uso a proveedores. No convierta controladores en repositorios ni concentre allí reglas de negocio.
3. **Validar los datos externos.** Configure un `ValidationPipe` global con transformación y rechazo de propiedades no permitidas cuando corresponda; defina DTOs de entrada y no confíe en los tipos de TypeScript como validación en tiempo de ejecución.
4. **Centralizar la configuración.** Use `@nestjs/config`, valide variables requeridas al inicio y evite leer `process.env` dispersamente por la aplicación. No guarde secretos en el repositorio.
5. **Aplicar seguridad por capas.** Use guards para autorización, autenticación con estrategias probadas, límites de tasa cuando el riesgo lo requiera y controles apropiados para CORS, cabeceras, sesiones y secretos. Nunca confíe en un identificador recibido del cliente como prueba de autorización.
6. **Elegir el alcance de los proveedores con intención.** El alcance singleton es adecuado en la mayoría de servicios. Use scopes de solicitud o transitorios solo si el caso lo requiere, pues cambian el ciclo de vida y pueden aumentar el costo de creación.
7. **Probar límites y comportamiento.** Pruebe servicios de forma unitaria con dependencias sustituidas, y pruebe endpoints e integración de módulos con la aplicación de testing de Nest. Incluya casos de autorización y validación.
8. **Instrumentar y observar.** Registre eventos útiles sin incluir contraseñas, tokens ni datos sensibles. Añada métricas, trazas y comprobaciones de salud acordes con el entorno de operación.
9. **Medir antes de optimizar.** Revise latencia, uso de memoria y consultas a dependencias. Elija Express o Fastify con mediciones y compruebe que los middlewares/plugins necesarios sean compatibles.
10. **Separar servicios cuando haya una razón operativa.** Microservicios añaden despliegue, observabilidad, consistencia y comunicación distribuida. Empiece con módulos bien delimitados dentro de una aplicación cuando no haya una necesidad clara de independencia operacional.
11. **Fijar versiones y seguir guías de actualización.** Mantenga alineados los paquetes `@nestjs/*`, revise los cambios incompatibles de la versión objetivo y despliegue con una estrategia que permita detectar regresiones.

## Referencias oficiales

- [Fundamentos y arquitectura](https://docs.nestjs.com/fundamentals)
- [Módulos](https://docs.nestjs.com/modules)
- [Controladores](https://docs.nestjs.com/controllers)
- [Proveedores e inyección de dependencias](https://docs.nestjs.com/providers)
- [Pipes y validación](https://docs.nestjs.com/pipes)
- [Guards](https://docs.nestjs.com/guards)
- [Interceptores](https://docs.nestjs.com/interceptors)
- [Exception filters](https://docs.nestjs.com/exception-filters)
- [Configuración](https://docs.nestjs.com/techniques/configuration)
- [Seguridad](https://docs.nestjs.com/security/helmet)
- [Pruebas](https://docs.nestjs.com/fundamentals/testing)
- [Adaptador Fastify](https://docs.nestjs.com/techniques/performance)
- [Microservicios](https://docs.nestjs.com/microservices/basics)
- [GraphQL](https://docs.nestjs.com/graphql/quick-start)
- [WebSockets](https://docs.nestjs.com/websockets/gateways)
