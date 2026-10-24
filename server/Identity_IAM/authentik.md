# authentik

## Descripción

authentik es una plataforma de identidad autohospedada para integrar inicio de sesión y políticas de acceso en aplicaciones. Su arquitectura utiliza proveedores para definir el método de autenticación o integración de una aplicación, y puede ejecutar componentes auxiliares llamados *outposts* para algunas integraciones.

## Capacidades

- SSO para aplicaciones web y servicios.
- Proveedores para OAuth 2.0/OpenID Connect, SAML, LDAP y proxy, entre otros.
- Flujos configurables para autenticación, recuperación y aprobación.
- Integración con directorios y fuentes externas según los conectores disponibles.
- MFA y políticas de autenticación.
- Gestión centralizada de usuarios, grupos, aplicaciones y sesiones.
- Despliegue autogestionado y automatización por API.

## Casos de uso

- Centralizar el acceso a aplicaciones web internas y servicios autohospedados.
- Proteger aplicaciones que soportan OIDC u OAuth 2.0 o SAML.
- Integrar aplicaciones sin soporte moderno de SSO mediante proxy o LDAP, cuando el patrón de aplicación lo permita.
- Crear flujos comunes de MFA, acceso y recuperación de cuentas.
- Gestionar una capa de SSO para laboratorios, intranets y organizaciones que operan sus propios servicios.

## Consideraciones de operación

- Mantener actualizados servidor, base de datos, workers y outposts asociados.
- Usar TLS, copias de seguridad consistentes y almacenamiento persistente para los componentes que lo requieren.
- Revisar los límites de cada proveedor e integración; no todas las aplicaciones aceptan el mismo flujo de autenticación.
- Proteger claves de firma, secretos de proveedores y endpoints administrativos.
- Probar la disponibilidad del servidor de identidad y el comportamiento de aplicaciones dependientes durante una interrupción.
- Verificar en la documentación vigente los requisitos de despliegue y las funciones disponibles por versión o edición.

## Documentación oficial

- [Documentación de authentik](https://docs.goauthentik.io/)
- [Proveedores](https://docs.goauthentik.io/add-secure-apps/providers/)
- [Arquitectura](https://docs.goauthentik.io/core/architecture)
- [Instalación](https://docs.goauthentik.io/install-config/)

## Relación con IAM

- [Identity and Access Management](Identity_Access_Management.md)
