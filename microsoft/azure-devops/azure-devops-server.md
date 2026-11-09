# Azure DevOps Server

> **Revisado:** 2026-11-08  
> **Alcance:** arquitectura y operación del producto autohospedado. Confirmar siempre los requisitos y notas de versión de Microsoft antes de instalar, migrar o actualizar.

## Qué es

**Azure DevOps Server** es la versión instalable y autohospedada de Azure DevOps. Proporciona servicios como Boards, Repos, Pipelines, Test Plans, Artifacts y Wiki en infraestructura administrada por la organización.

Se diferencia de **Azure DevOps Services**, que Microsoft opera como servicio hospedado. Server ofrece control sobre infraestructura y ubicación de datos, a cambio de que la organización asuma instalación, configuración, mantenimiento, respaldo, seguridad y continuidad.

## Cuándo evaluarlo

Puede evaluarse cuando la organización requiere mantener el servicio y sus datos en infraestructura propia, operar en redes controladas o atender necesidades de integración y personalización que no encajan en el servicio hospedado. Antes de elegirlo, confirmar qué requisitos son realmente obligatorios y si se pueden cubrir con Azure DevOps Services, controles de identidad, agentes privados u opciones de residencia disponibles.

La autogestión no elimina el riesgo: traslada a la organización la responsabilidad de administrar la plataforma y mantenerla dentro de soporte.

## Componentes de arquitectura

Una instalación de Azure DevOps Server normalmente incluye:

| Componente | Responsabilidad |
|---|---|
| **Capa de aplicación** | Hospeda los servicios web y APIs de Azure DevOps Server y atiende las conexiones de usuarios, clientes y agentes. |
| **SQL Server** | Almacena las bases de datos de configuración, colecciones de proyectos y otros datos de la aplicación. Es un componente crítico para disponibilidad, rendimiento y recuperación. |
| **Agentes de Pipelines** | Ejecutan compilación, pruebas y despliegues. Son nodos separados que la organización instala y administra; no son equivalentes al servidor de aplicación. |
| **Identidad y certificados** | Integran autenticación y autorización con el directorio configurado y protegen las conexiones, normalmente mediante TLS. |
| **Almacenamiento y respaldos** | Conservan artefactos y respaldos conforme a la configuración del producto y a las necesidades de recuperación. Revisar la estrategia completa según la topología y las extensiones instaladas. |
| **Búsqueda e informes** | Capacidades opcionales o dependientes de versión y configuración. Evaluar sus prerrequisitos y efecto en infraestructura. |

## Topologías

La topología debe seleccionarse según usuarios, colecciones, carga, disponibilidad requerida y capacidad operativa.

- **Servidor único:** capa de aplicación y datos en un servidor; adecuada para evaluación o equipos pequeños cuando los requisitos de disponibilidad son modestos.
- **Dos servidores:** separa la capa de aplicación de SQL Server y permite dimensionar o administrar cada capa por separado.
- **Varios servidores o alta disponibilidad:** distribuye componentes para cargas mayores y requisitos más exigentes de disponibilidad. Requiere validar dependencias, balanceo, SQL, almacenamiento, certificados, backups y procedimientos de failover con la documentación vigente.

Las topologías no hacen que el servicio sea automáticamente tolerante a fallos. Diseñar y probar recuperación de datos y aplicación, y documentar puntos únicos de fallo.

## Requisitos y ciclo de vida

Azure DevOps Server depende de versiones compatibles de Windows Server, SQL Server, navegadores, agentes y otros componentes. La matriz de compatibilidad cambia por versión. Revisar los [requisitos oficiales de instalación y actualización](https://learn.microsoft.com/azure/devops/server/requirements) y las [notas de versión](https://learn.microsoft.com/azure/devops/server/release-notes/azuredevopsserver) antes de adquirir infraestructura o comenzar una actualización.

La página oficial consultada en noviembre de 2026 presenta Azure DevOps Server 25H2 y sus parches publicados; las versiones cambian, por lo que este documento no sustituye la consulta de la página de descargas y release notes. Mantener una política de actualización con inventario de versión, evaluación de cambios, pruebas previas y ventanas de mantenimiento.

## Instalación y actualización

1. Definir el modelo de identidad, DNS, certificados, TLS, segmentación de red y acceso administrativo.
2. Elegir la topología y dimensionar capa de aplicación, SQL, almacenamiento y agentes con los datos de carga y crecimiento esperados.
3. Seleccionar versiones de Windows y SQL soportadas para la versión de Server elegida.
4. Preparar respaldo verificable y un ambiente de prueba que represente integraciones y extensiones importantes.
5. Instalar o actualizar siguiendo la secuencia oficial de Microsoft, documentando responsables, tiempos y criterios de reversión.
6. Validar autenticación, repositorios, pipelines, agentes, extensiones, correo, búsqueda, integraciones y restauración.
7. Registrar versión, parches, cambios, evidencia y fecha de la siguiente revisión.

Evitar saltos de versión o actualizaciones de SQL basados en suposiciones: comprobar rutas de actualización, dependencias y compatibilidad específica en las páginas oficiales.

## Respaldos y recuperación

Configurar la función de respaldo programado de Azure DevOps Server y definir retención, almacenamiento protegido, acceso separado y copias externas según el plan institucional. Coordinar la protección de SQL Server y de cualquier almacenamiento adicional requerido por la instalación.

- Establecer objetivos de recuperación (RPO y RTO) con responsables del servicio.
- Cifrar y restringir acceso a respaldos; supervisar ejecuciones y fallos.
- Probar periódicamente la restauración completa en una instalación aislada.
- Documentar orden de recuperación, dependencias, DNS, certificados, claves, identidad y validaciones posteriores.
- Incluir agentes, pools, extensiones, service connections e integraciones en el inventario de recuperación, aunque no todos sean datos respaldados por la misma función.

Un respaldo no es evidencia de recuperabilidad hasta que su restauración se ha probado.

## Operación segura

- Mantener el producto y SQL Server en versiones soportadas y aplicar actualizaciones y parches de seguridad en el plazo definido por riesgo.
- Usar cuentas administrativas separadas y limitar administradores de colección, proyecto, pools, grupos y conexiones.
- Proteger la capa de aplicación y SQL con segmentación, firewall, TLS, monitoreo y acceso administrativo controlado.
- Revisar configuración de identidad, sincronización, certificados, cuentas de servicio y auditoría.
- Separar agentes por entorno y sensibilidad; ejecutar los agentes con privilegios mínimos y mantener sus máquinas limpias y actualizadas.
- No entregar secretos productivos a builds de cambios no revisados; limitar permisos de service connections y credenciales.
- Supervisar disponibilidad, espacio, CPU, memoria, SQL, colas, fallos de jobs y errores de aplicación.
- Inventariar extensiones e integraciones; probar compatibilidad y seguridad antes de instalarlas o actualizarlas.
- Mantener procedimientos de incidentes, mantenimiento, upgrade y restauración actualizados y ensayados.

Para un core financiero, considerar dominios de red separados para control plane, agentes de build y despliegue a producción; evidencias inmutables de aprobaciones y releases; segregación de funciones; y revisión independiente del proceso de cambio, de acuerdo con la regulación aplicable. Estas son consideraciones de diseño, no una certificación que el producto proporcione por sí solo.

## Diferencias operativas frente a Services

| Tema | Azure DevOps Services | Azure DevOps Server |
|---|---|---|
| Hospedaje | Microsoft opera la plataforma. | La organización opera servidores y datos. |
| Actualizaciones | El servicio recibe cambios administrados por Microsoft. | La organización planifica e instala versiones y parches. |
| Agentes de Microsoft hospedados | Disponibles según el servicio y sus condiciones. | No disponibles; se usan agentes autohospedados. |
| Infraestructura | Menos administración de la capa de aplicación. | Requiere Windows, SQL Server, almacenamiento, identidad, certificados, respaldo y monitoreo. |
| Personalización y conectividad | Sujeta a límites y funciones del servicio. | Mayor control de la infraestructura; más responsabilidad de soporte y seguridad. |
| Disponibilidad y recuperación | Revisar compromisos y controles publicados del servicio. | Diseñar, pagar, operar y probar la disponibilidad y recuperación propias. |

Las funciones, límites y compatibilidades varían con las versiones. Consulte la [descripción general de Azure DevOps](https://learn.microsoft.com/azure/devops/user-guide/what-is-azure-devops), los requisitos y las notas de versión antes de tomar decisiones.

## Guías relacionadas

- [Azure DevOps](azure-devops.md): servicios y modelos de hospedaje.
- [Azure Pipelines](azure-pipelines.md): definición de pipelines, agentes y controles de despliegue.

## Documentación oficial

- [Instalar y configurar Azure DevOps Server](https://learn.microsoft.com/azure/devops/server/install/get-started)
- [Requisitos de instalación y actualización](https://learn.microsoft.com/azure/devops/server/requirements)
- [Respaldar y restaurar Azure DevOps Server](https://learn.microsoft.com/azure/devops/server/admin/backup/backup-restore)
- [Notas de versión de Azure DevOps Server](https://learn.microsoft.com/azure/devops/server/release-notes/azuredevopsserver)
- [Descargas de Azure DevOps Server](https://learn.microsoft.com/azure/devops/server/download/azuredevopsserver)
