# Identity_IAM

## Propósito

Esta carpeta reúne documentación sobre Identity and Access Management (IAM) y sobre los productos de identidad que se pueden desplegar en infraestructura propia (on-premise o autohospedados).

## Qué contenido va aquí

- Conceptos generales de IAM: directorio, autenticación, autorización, federación, gobierno del ciclo de vida (IGA), acceso privilegiado (PAM), identidades no humanas y auditoría.
- Una ficha por producto de identidad autogestionado: directorios, proveedores de identidad (IdP/SSO), plataformas IGA y servicios de identidad para Linux/Unix.
- Guías de arquitectura, operación y buenas prácticas relacionadas con identidad y acceso.

No va aquí: documentación de servicios de identidad exclusivamente cloud ni de temas ajenos a identidad y acceso. AD FS se documenta fuera de esta carpeta, en [`../ADFS.md`](../ADFS.md).

## Archivos existentes

| Archivo | Tipo | Descripción |
|---|---|---|
| [Identity_Access_Management.md](Identity_Access_Management.md) | Concepto general | Definición de IAM, componentes, principios transversales, recomendaciones de implementación e índice de productos. Punto de partida. |
| [Microsoft-Active-Directory.md](Microsoft-Active-Directory.md) | Directorio | AD DS: arquitectura, componentes, ventajas, límites, recomendaciones e integración con AD FS y Microsoft Entra. |
| [Keycloak.md](Keycloak.md) | IdP / SSO | SSO, OIDC, OAuth 2.0, SAML, federación con LDAP/AD y autorización fina. |
| [WSO2-Identity-Server.md](WSO2-Identity-Server.md) | IAM / CIAM | Plataforma IAM extensible, orientada a APIs, con federación, MFA y clúster. |
| [authentik.md](authentik.md) | IdP / SSO | Proveedor de identidad autohospedado con proveedores, flujos y outposts. |
| [midPoint.md](midPoint.md) | IGA | Gobierno y aprovisionamiento de identidades, conectores, roles y recertificación. |
| [FreeIPA.md](FreeIPA.md) | Identidad Linux/Unix | LDAP, Kerberos, DNS, certificados, HBAC y sudo para dominios Linux/Unix. |

## Convención para fichas de producto

Cada ficha usa estas secciones, en este orden:

1. Descripción
2. Capacidades
3. Casos de uso
4. Consideraciones de operación
5. Documentación oficial (enlaces a fuentes del proveedor)
6. Relación con IAM (enlace a [Identity_Access_Management.md](Identity_Access_Management.md))

Al agregar un producto nuevo:

- Crea el archivo con ese formato y nómbralo como el producto, con guiones en lugar de espacios.
- Agrégalo al listado "Productos IAM on-premise" de [Identity_Access_Management.md](Identity_Access_Management.md).
- Agrégalo a la tabla de este README.
