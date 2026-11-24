# SSO, OAuth, OIDC y SAML

Resumen de las diferencias entre SSO, OAuth, OIDC y SAML, con una comparación para saber qué resuelve cada uno y cuándo usarlo.

## Video

Animación con las cuatro definiciones:

[Ver video: sso-oauth-oidc-saml.mp4](sso-oauth-oidc-saml/sso-oauth-oidc-saml.mp4)

<video src="sso-oauth-oidc-saml/sso-oauth-oidc-saml.mp4" controls muted loop width="640"></video>

## Resumen

Los cuatro términos se confunden porque aparecen juntos, pero no están en el mismo nivel:

- **SSO** es una experiencia de uso.
- **OAuth** es un protocolo de autorización.
- **OIDC** es una capa de autenticación sobre OAuth 2.0.
- **SAML** es un estándar anterior, basado en XML, usado sobre todo para SSO empresarial.

En una frase cada uno:

- OAuth = acceso.
- OIDC = identidad.
- SAML = SSO empresarial, más antiguo.
- SSO = la experiencia.

## Los cuatro conceptos

### 1. SSO (Single Sign-On)

- Es una experiencia de usuario, no un protocolo.
- Permite iniciar sesión una sola vez y acceder a varias aplicaciones sin autenticarse de nuevo, con acceso continuo entre herramientas.
- Se apoya en protocolos como SAML u OIDC.

### 2. OAuth

- Es un protocolo de autorización.
- Permite que una aplicación acceda a datos o servicios del usuario sin compartir sus credenciales.
- Controla a qué puede acceder una aplicación, no quién es el usuario.

### 3. OIDC (OpenID Connect)

- Es una capa de autenticación construida sobre OAuth 2.0.
- Verifica la identidad del usuario y entrega su información mediante ID tokens, normalmente JWT.
- Es el estándar para inicio de sesión e identidad en aplicaciones modernas.

### 4. SAML

- Es un estándar más antiguo, basado en XML, usado principalmente para SSO empresarial.
- Sigue siendo común en sistemas heredados y empresariales, aunque las aplicaciones nuevas adoptan cada vez más OIDC.
- Es potente, pero más complejo que OIDC.

## Comparación

| | SSO | OAuth 2.0 | OIDC | SAML |
|---|---|---|---|---|
| Qué es | Experiencia de uso | Protocolo de autorización | Capa de autenticación sobre OAuth 2.0 | Estándar de autenticación y SSO |
| Pregunta que responde | ¿Puedo entrar una vez a varias aplicaciones? | ¿Qué puede hacer esta aplicación en nombre del usuario? | ¿Quién es el usuario? | ¿Quién es el usuario y puede entrar a esta aplicación? |
| Resultado | Sesión compartida entre aplicaciones | Access token con permisos (scopes) | ID token (JWT) con datos de identidad, además del access token | Aserción XML firmada |
| Formato | No aplica | Tokens, a menudo JWT u opacos | JSON y JWT | XML |
| Uso típico | Acceso unificado a las herramientas de una organización | Dar a una aplicación acceso limitado a una API o a datos del usuario | Login en aplicaciones web, móviles y SPA | Integración con aplicaciones empresariales y heredadas |
| Complejidad | Depende del protocolo elegido | Media | Media, menor que SAML | Alta |

## Cómo elegir

1. Si necesitas que un usuario **inicie sesión** en una aplicación moderna, usar **OIDC**.
2. Si necesitas que una aplicación acceda a una **API o a datos** en nombre del usuario, usar **OAuth 2.0**. Si además necesitas conocer la identidad, usar OIDC, que ya incluye OAuth.
3. Si debes integrarte con una aplicación empresarial o heredada que solo soporta **SAML**, usar SAML.
4. Si el objetivo es que el usuario entre una vez a varias aplicaciones, implementar **SSO** con un proveedor de identidad que use OIDC, SAML o ambos.

## Consideraciones

- **OAuth no autentica:** un access token indica qué se permite hacer, no quién es el usuario. Para identidad usar OIDC.
- **SSO no es un protocolo:** al elegir una solución de SSO hay que decidir qué protocolo usa cada aplicación.
- **Una organización puede usar ambos:** es habitual que un proveedor de identidad ofrezca SAML para aplicaciones heredadas y OIDC para las nuevas.
- **Identidad no es permisos:** autenticar al usuario no define qué puede hacer dentro de cada aplicación. Esa decisión requiere roles, políticas y control de acceso a los datos.
- **Gestión de permisos dispersa:** cuantas más aplicaciones y agentes de IA acceden a datos de producción, más difícil es mantener los permisos por separado en cada herramienta. Centralizar el acceso y aplicar controles comunes reduce ese riesgo.

## Material de apoyo

- [Video del resumen](sso-oauth-oidc-saml/sso-oauth-oidc-saml.mp4)
- [Autenticación](../../seguridad/seguridad-aplicativa/authentication.md)
- [Implementación de OpenID Connect](../../seguridad/seguridad-aplicativa/OpenID.Connect.implementation.md)
- [Seguridad aplicativa](../../seguridad/seguridad-aplicativa/security.md)
- [Autenticación en .NET](../../../dotnet/net/web/autenticacion/README.md)

## Referencias

- Resumen de los cuatro conceptos y animación proporcionados por el propietario del repositorio. El video proviene de una publicación en X; la URL del archivo original es `https://video.twimg.com/tweet_video/HX5JUY5bsAATuxd.mp4`. No se identificó el autor. La publicación original terminaba con un mensaje promocional de la herramienta Retool y un enlace a un artículo, que no se incluyen aquí.
- Las secciones "Comparación", "Cómo elegir" y "Consideraciones" amplían el resumen original con criterios generales y no provienen de esa publicación. Contrastar las definiciones con las especificaciones oficiales (OAuth 2.0, OpenID Connect, SAML 2.0) antes de usarlas como referencia técnica.
