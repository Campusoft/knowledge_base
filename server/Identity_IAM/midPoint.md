# midPoint

## Descripción

midPoint, desarrollado por Evolveum, es una plataforma de **Identity Governance and Administration (IGA)**. Su función principal es gobernar el ciclo de vida de identidades y cuentas en múltiples sistemas mediante políticas, roles, conectores, sincronización y auditoría. Puede desplegarse en infraestructura administrada por la organización.

## Capacidades

- Gestión del ciclo de vida de identidades y cuentas.
- Aprovisionamiento y desaprovisionamiento en sistemas conectados.
- Sincronización y conciliación de atributos, cuentas y derechos.
- Conectores para directorios, aplicaciones, bases de datos y fuentes de datos.
- Modelado de roles, políticas, reglas y metaroles.
- Flujos de aprobación, certificación y revisión de accesos.
- Auditoría, reportes y análisis de identidades y permisos.
- Extensión y personalización de modelos, mappings y workflows.

## Casos de uso

- Automatizar altas, cambios y bajas desde un sistema autoritativo.
- Detectar cuentas huérfanas o diferencias entre la identidad esperada y las cuentas reales.
- Aprovisionar permisos por cargo, unidad organizativa o política.
- Realizar recertificaciones y apoyar controles de cumplimiento.
- Orquestar cambios de identidad entre sistemas heterogéneos con interfaces diferentes.

## Consideraciones de operación

- midPoint es principalmente una plataforma IGA y de aprovisionamiento; no debe asumirse que reemplaza por sí sola a un IdP de autenticación para aplicaciones.
- Confirmar conectores disponibles, estado de soporte y compatibilidad para cada sistema destino.
- Diseñar mappings, roles y workflows con gobierno y propietarios claros; la personalización compleja requiere mantenimiento especializado.
- Proteger credenciales de conectores, limitar los privilegios de las cuentas de integración y probar cambios en sistemas no productivos.
- Planificar base de datos, respaldos, actualización, alta disponibilidad y monitoreo según la versión y el tamaño de despliegue.
- Validar la compatibilidad de conectores y extensiones antes de actualizar midPoint.

## Documentación oficial

- [Documentación de Evolveum](https://docs.evolveum.com/)
- [Visión general de midPoint](https://docs.evolveum.com/book/02-midpoint-overview/)
- [Guía práctica de gestión de identidades](https://docs.evolveum.com/book/practical-identity-management-with-midpoint.html)
- [Catálogo de conectores](https://docs.evolveum.com/connectors/connectors/)

## Relación con IAM

- [Identity and Access Management](Identity_Access_Management.md)
