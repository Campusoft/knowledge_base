# Keycloak

## Descripción

Keycloak es una plataforma de gestión de identidad y acceso de código abierto que puede desplegarse en infraestructura propia. Centraliza autenticación y SSO para aplicaciones y servicios; también admite federación de identidades y conexión con directorios existentes.

## Capacidades

- SSO y cierre de sesión centralizados.
- Protocolos OpenID Connect, OAuth 2.0 y SAML 2.0.
- Federación con directorios LDAP y Active Directory.
- Delegación de autenticación a proveedores de identidad externos.
- Gestión de usuarios, grupos, sesiones, credenciales y flujos de autenticación.
- MFA, políticas de contraseñas y flujos personalizables.
- Autorización fina mediante recursos, scopes, permisos y políticas.
- Consolas administrativas y de autoservicio de cuenta.
- Extensión mediante SPI y despliegue en clúster para disponibilidad y escala.

## Casos de uso

- Añadir inicio de sesión único a un conjunto de aplicaciones internas.
- Proteger APIs y aplicaciones web o móviles con protocolos estándar.
- Integrar una aplicación con cuentas que ya viven en LDAP o Active Directory.
- Permitir autenticación federada de socios o de otro proveedor de identidad.
- Aplicar MFA, flujos de autenticación y políticas de autorización centralizadas.

## Consideraciones de operación

- Operar la base de datos, actualizaciones, certificados TLS, copias de seguridad y recuperación.
- Configurar correctamente hostname, proxy inverso, encabezados confiables y TLS en producción.
- Diseñar disponibilidad del servicio de autenticación y proteger la consola administrativa.
- Revisar compatibilidad de adaptadores, extensiones personalizadas y versiones antes de actualizar.
- Definir sincronización y reglas de escritura para usuarios federados; evitar cambios ambiguos entre directorios.
- Usar roles y permisos mínimos para clientes, cuentas de servicio y administradores.

## Documentación oficial

- [Sitio de Keycloak](https://www.keycloak.org/)
- [Guía de administración](https://www.keycloak.org/docs/latest/server_admin/)
- [Guía de instalación](https://www.keycloak.org/server/)
- [Authorization Services](https://www.keycloak.org/docs/latest/authorization_services/)

## Relación con IAM

- [Identity and Access Management](Identity_Access_Management.md)
