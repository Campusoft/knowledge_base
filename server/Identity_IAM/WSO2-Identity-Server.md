# WSO2 Identity Server

## Descripción

WSO2 Identity Server es una plataforma IAM extensible y orientada a APIs que puede desplegarse on-premise, en nube privada o en nube pública. Se utiliza para centralizar autenticación, federación y control de acceso de usuarios, aplicaciones y servicios, incluidos escenarios de identidad de clientes (CIAM).

## Capacidades

- SSO y autenticación para aplicaciones.
- Federación de identidades e integración con proveedores externos.
- MFA y autenticación adaptativa según configuración y edición.
- Gestión del ciclo de vida de usuarios, grupos y roles.
- Protocolos estándar de identidad y autorización, incluidos OpenID Connect, OAuth 2.0 y SAML.
- Administración mediante consolas, APIs y configuración del servidor.
- Extensibilidad y arquitectura componible para integrar necesidades específicas.
- Despliegues en clúster para disponibilidad y distribución de carga.

## Casos de uso

- Inicio de sesión unificado para aplicaciones corporativas o de clientes.
- Portales de autoservicio y registro de usuarios.
- Federación con directorios empresariales o proveedores de identidad asociados.
- Aplicación de MFA y políticas de autenticación basadas en riesgo o contexto.
- Autorización de acceso a APIs y recursos de usuario.

## Consideraciones de operación

- Dimensionar nodos, base de datos, caché, balanceador y almacenamiento según concurrencia y disponibilidad requerida.
- Diseñar y probar el clúster, balanceo de carga, afinidad de sesión si aplica y recuperación ante fallos.
- Revisar las versiones soportadas de Java, bases de datos, sistemas operativos y directorios.
- Proteger los endpoints administrativos, certificados, secretos y llaves de firma.
- Automatizar despliegues y configuración, y probar migraciones de versión en un entorno representativo.
- Consultar la documentación de la versión desplegada; las guías 5.x, 6.x y 7.x pueden diferir.

## Documentación oficial

- [Sitio de WSO2 Identity Server](https://wso2.com/identity-platform/identity-server/)
- [Documentación de Identity Server](https://is.docs.wso2.com/)
- [Arquitectura](https://is.docs.wso2.com/en/6.0.0/references/architecture/architecture/)
- [Guía de despliegue](https://is.docs.wso2.com/en/5.11.0/setup/deployment-guide/)

## Relación con IAM

- [Identity and Access Management](Identity_Access_Management.md)
