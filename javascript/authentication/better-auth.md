# Better Auth

Better Auth es una biblioteca de autenticación y autorización para TypeScript. Se integra en el servidor de una aplicación y ofrece flujos comunes, persistencia de sesiones y un sistema de plugins. Puede usarse con distintos frameworks y también desplegarse como servicio de autenticación independiente. Esta guía resume sus capacidades y los aspectos que conviene evaluar antes de adoptarlo.

La documentación oficial es la referencia para la compatibilidad con versiones, bases de datos y entornos concretos: [better-auth.com/docs](https://www.better-auth.com/docs).

## Características

- **Correo y contraseña:** registro, inicio de sesión y opciones de verificación y recuperación de cuenta.
- **Sesiones:** gestión de sesión y cookies seguras; la aplicación debe configurar dominio, HTTPS y políticas de sesión según su arquitectura.
- **OAuth y OIDC:** inicio de sesión con proveedores sociales y configuración de proveedores OAuth/OIDC genéricos mediante plugins.
- **Extensiones:** plugins para capacidades como 2FA, passkeys, organizaciones, múltiples sesiones y SSO, según el paquete y la versión elegidos.
- **Integración TypeScript:** tipos inferidos para la instancia y las sesiones, además de APIs para servidor y cliente.
- **Independencia del framework:** la instancia de Better Auth expone un handler que puede montarse en la capa HTTP del framework.
- **Persistencia configurable:** adaptador integrado y adaptadores para herramientas de acceso a datos compatibles; la elección afecta compatibilidad, migraciones y despliegue.

## Arquitectura

Better Auth se ejecuta junto a la aplicación o como un servicio independiente. La configuración crea una instancia de autenticación, conecta una base de datos y registra proveedores y plugins. La aplicación web entrega las rutas de autenticación al handler; las rutas protegidas consultan la sesión o validan las credenciales y después aplican la autorización del producto.

```text
Cliente
  -> framework HTTP (por ejemplo, Hono)
       -> rutas de autenticación -> handler Better Auth
       -> rutas del producto -> consulta de sesión / autorización
  -> adaptador de base de datos -> usuarios, cuentas, sesiones y verificaciones
  -> proveedor de correo u OAuth/OIDC cuando se configura
```

- **Configuración (`betterAuth`)**: define base URL, secreto, base de datos, autenticación local, proveedores y plugins.
- **Handler HTTP**: procesa las solicitudes de autenticación recibidas por el framework.
- **Adaptador de base de datos**: persiste las entidades que requiere la configuración habilitada.
- **Cliente opcional**: facilita consumir las operaciones de autenticación desde el frontend; no reemplaza la autorización en el servidor.
- **Middleware de la aplicación**: protege las rutas del producto consultando la sesión y aplicando sus propias reglas de acceso.

### Uso con Hono

Hono y Better Auth aceptan objetos `Request` y `Response` de Web Standards, por lo que el handler puede montarse directamente. La ruta debe coincidir con el `basePath` configurado (por defecto, `/api/auth`).

```ts
import { Hono } from 'hono'
import { auth } from './auth'

const app = new Hono()

app.all('/api/auth/*', (c) => auth.handler(c.req.raw))

export default app
```

## Frameworks y entornos

Better Auth es independiente del framework en el servidor; se integra mediante handlers o adaptadores. La documentación oficial mantiene guías para frameworks y entornos, entre ellos Hono y varios frameworks de JavaScript/TypeScript. Verifique la guía específica antes de asumir que todas las funciones son idénticas en cada integración.

Puede desplegarse junto a la aplicación web en un servidor Node.js, o en plataformas serverless y runtimes edge compatibles. La portabilidad depende de que el runtime, el adaptador de base de datos, las bibliotecas auxiliares y el proveedor de correo soporten ese entorno.

## Casos de uso

- SaaS que requiere registro e inicio de sesión con correo y contraseña.
- Aplicaciones que empiezan con cuentas locales y planean añadir acceso mediante Google, Microsoft, otro proveedor OIDC o SSO empresarial.
- Backends TypeScript con Hono u otro framework compatible que desean incorporar autenticación sin desplegar inicialmente un servidor de identidad separado.
- Productos que necesitan ampliar la autenticación mediante plugins, por ejemplo con 2FA, passkeys u organizaciones.
- Aplicaciones serverless o edge cuando la combinación específica de runtime, base de datos y operaciones de autenticación esté soportada.

## Limitaciones y aspectos a evaluar

- **No elimina la responsabilidad de seguridad.** El equipo debe proteger secretos, configurar cookies, limitar intentos, verificar correo y diseñar recuperación de cuenta y respuestas a incidentes.
- **Autenticación y autorización de negocio son distintas.** Better Auth ayuda con identidad y controles de autenticación; la aplicación debe definir qué puede hacer cada usuario en cada recurso y tenant.
- **Dependencia de base de datos.** Deben revisarse el adaptador, el esquema requerido, las migraciones y la conectividad del proveedor de base de datos desde el runtime seleccionado.
- **Restricciones de serverless y edge.** Límites de ejecución, conexiones, APIs disponibles, cold starts y migraciones pueden afectar el diseño. Algunas operaciones de esquema o CLI deben ejecutarse fuera de las funciones de producción.
- **La federación requiere configuración cuidadosa.** Redirecciones, emisores, audiencias, scopes, rotación de secretos y asociación de cuentas deben probarse con cada proveedor OIDC.
- **Plugins y capacidades dependen de versiones.** Confirme el estado, los requisitos y la interoperabilidad de cada plugin antes de basar en él una función crítica.
- **No es un proveedor gestionado por defecto.** Al integrarlo como biblioteca, el equipo mantiene el servicio, la base de datos, las claves, el monitoreo, las copias y la recuperación.
- **Cambios de configuración implican migraciones y pruebas.** Añadir métodos de autenticación o plugins puede modificar el esquema y el comportamiento de sesión.

## Recomendaciones

1. **Encapsular la autenticación.** Centralice la instancia y su configuración en un módulo del servidor; no distribuya secretos ni llamadas internas por el frontend.
2. **Usar almacenamiento persistente compatible.** Seleccione primero la base de datos y el runtime objetivo; revise requisitos y migraciones del adaptador antes de producción.
3. **Proteger secretos y cookies.** Cargue secretos desde el gestor de secretos del entorno, use HTTPS y configure cookies `HttpOnly`, `Secure` y `SameSite` de acuerdo con el flujo y el dominio.
4. **Añadir controles para cuentas locales.** Diseñe verificación de correo, recuperación segura, limitación de intentos, mensajes que no revelen si una cuenta existe y MFA cuando el riesgo lo justifique.
5. **Mantener autorización en el backend.** Después de validar sesión, compruebe permisos, rol, propiedad del recurso y pertenencia al tenant en cada operación protegida.
6. **Modelar identidad externa sin acoplar el dominio.** Vincule las cuentas OIDC a un usuario interno estable; no use el correo como identificador único sin una política explícita de verificación y vinculación.
7. **Probar flujos completos.** Incluya registro, inicio y cierre de sesión, expiración, recuperación, cambios de correo, revocación y callback OIDC, además de los casos de autorización.
8. **Planificar operaciones.** Automatice migraciones en el proceso de despliegue, supervise errores de autenticación y tenga procedimientos para rotar secretos y revocar sesiones.
9. **Verificar edge caso por caso.** Pruebe las rutas reales sobre el runtime final y confirme la compatibilidad de la base de datos, el correo, el adaptador y las migraciones.
10. **Comparar biblioteca propia con identidad gestionada.** Better Auth reduce la necesidad de operar un servidor IAM separado, pero no elimina el costo de mantener autenticación crítica dentro del producto.

## Referencias oficiales

- [Introducción](https://www.better-auth.com/docs/introduction)
- [Instalación y configuración](https://www.better-auth.com/docs/installation)
- [Integración con Hono](https://www.better-auth.com/docs/integrations/hono)
- [Bases de datos](https://www.better-auth.com/docs/concepts/database)
- [Correo y contraseña](https://www.better-auth.com/docs/authentication/email-password)
- [Proveedores sociales y OAuth genérico](https://www.better-auth.com/docs/authentication/other-social-providers)
- [Plugin Generic OAuth](https://www.better-auth.com/docs/plugins/generic-oauth)
- [Plugin SSO](https://www.better-auth.com/docs/plugins/sso)
