# FreeIPA

## Descripción

FreeIPA es una solución integrada de identidad y autenticación para redes Linux y Unix. Combina servicios de directorio LDAP, Kerberos, DNS, certificados y administración centralizada de políticas. Su foco principal es la identidad de usuarios, grupos, hosts y servicios en dominios Linux/Unix.

## Capacidades

- Directorio LDAPv3 como almacén de identidades y configuración.
- Autenticación Kerberos y SSO para servicios compatibles.
- Administración centralizada de usuarios, grupos, hosts y servicios.
- Políticas de acceso, reglas sudo, HBAC (control de acceso basado en host) y políticas de contraseñas.
- Gestión integrada de DNS y certificados según la configuración del dominio.
- Interfaz web y herramientas de línea de comandos.
- Réplicas multi-master para redundancia y distribución de carga.
- Confianza con dominios Active Directory para escenarios de integración compatibles.

## Casos de uso

- Centralizar usuarios y autenticación para servidores Linux/Unix.
- Administrar políticas de acceso a hosts y permisos sudo.
- Proporcionar Kerberos y resolución de identidad a servicios de red internos.
- Gestionar DNS y certificados dentro del dominio de identidad.
- Integrar identidades Linux con un dominio Active Directory mediante relaciones de confianza compatibles.

## Consideraciones de operación

- FreeIPA está orientado a identidad de sistemas Linux/Unix; no es un sustituto directo de un IdP general para SSO moderno de aplicaciones web.
- Planificar correctamente DNS, nombres de host, sincronización horaria, certificados y puertos de red antes de instalar.
- Desplegar réplicas y probar recuperación; proteger los datos de directorio y las claves del dominio.
- Aplicar actualizaciones coordinadas y revisar compatibilidad entre versiones de servidores y clientes.
- Definir políticas de contraseña, HBAC y sudo con mínimo privilegio; auditar cambios administrativos.
- Confirmar requisitos de distribución y versión de sistema operativo en la documentación del proveedor o del sistema operativo utilizado.

## Documentación oficial

- [Acerca de FreeIPA](https://www.freeipa.org/About.html)
- [Instalación y despliegue](https://www.freeipa.org/page/InstallAndDeploy.html)
- [Servicio de directorio](https://www.freeipa.org/page/Directory_Server)
- [Documentación de FreeIPA](https://www.freeipa.org/page/Documentation)

## Relación con IAM

- [Identity and Access Management](Identity_Access_Management.md)
