# Azure DevOps

> **Revisado:** 2026-11-08  
> **Alcance:** visión general de Azure DevOps y sus dos modelos de servicio. Para detalles de CI/CD y operación autogestionada, consulte las guías específicas enlazadas abajo.

## Qué es

**Azure DevOps** es una plataforma de Microsoft para planificar, desarrollar, probar, empaquetar y entregar software. Integra servicios que pueden adoptarse juntos o conectarse con herramientas externas.

| Servicio | Propósito |
|---|---|
| **Azure Boards** | Planificar y hacer seguimiento de trabajo con elementos de trabajo, tableros, backlogs y sprints. |
| **Azure Repos** | Hospedar repositorios Git y, según el escenario, TFVC; revisar cambios mediante pull requests y políticas de rama. |
| **Azure Pipelines** | Automatizar compilación, pruebas, publicación de artefactos y despliegues. |
| **Azure Test Plans** | Administrar pruebas manuales, exploratorias y planes de prueba. |
| **Azure Artifacts** | Hospedar, compartir y consumir paquetes como NuGet, npm, Maven, Python y Universal Packages. |

Los equipos pueden trabajar con repositorios de Azure Repos o conectar repositorios externos compatibles. Pipelines puede compilar y desplegar aplicaciones en Azure, otras nubes o infraestructura propia, sujeto a agentes, conexiones y permisos configurados.

## Modelos de servicio

Azure DevOps se ofrece en dos modelos principales:

| Modelo | Administración | Consideraciones |
|---|---|---|
| **Azure DevOps Services** | Servicio hospedado por Microsoft. Microsoft opera la plataforma; la organización configura proyectos, identidades, políticas y agentes según sus necesidades. | Recibe capacidades y mantenimiento del servicio sin que el cliente administre la capa de aplicación. La organización debe revisar requisitos de datos, región, identidad e integración. |
| **Azure DevOps Server** | Producto instalable en infraestructura administrada por la organización, en su centro de datos o entorno propio. | Permite controlar ubicación y topología de los datos, pero la organización mantiene servidores, SQL Server, certificados, respaldos, capacidad, actualizaciones, agentes y recuperación. |

La elección no se reduce a “nube contra seguridad”. Compare residencia de datos, conectividad, funciones disponibles, integración de identidad, continuidad, costo total y capacidad interna para operar la solución. La documentación oficial mantiene las [notas de versión de Azure DevOps Server](https://learn.microsoft.com/azure/devops/server/release-notes/azuredevopsserver) y sus [requisitos de instalación y actualización](https://learn.microsoft.com/azure/devops/server/requirements); consúltelos antes de diseñar o actualizar una instalación.

## Cómo encajan los servicios

Un flujo habitual enlaza el trabajo de Boards con cambios en Repos, validaciones y despliegues en Pipelines, planes y resultados de prueba en Test Plans, y paquetes versionados en Artifacts. Las integraciones y los enlaces entre elementos permiten seguir el cambio desde la necesidad hasta la entrega.

La suite no obliga a que todos los equipos usen cada servicio. Se pueden adoptar capacidades gradualmente y mantener herramientas existentes donde una sustitución no aporte valor.

## Seguridad y gobierno

- Definir grupos, roles, permisos y políticas de rama con privilegio mínimo.
- Revisar quién puede modificar pipelines, conexiones de servicio, grupos de agentes, variables seguras y controles de entornos.
- Separar proyectos o límites de autorización cuando los productos o equipos requieran aislamiento.
- Conservar trazabilidad entre elemento de trabajo, commit, pull request, ejecución, artefacto y despliegue.
- Gestionar secretos con almacenes y conexiones de servicio protegidas; evitar credenciales permanentes y permisos amplios.
- Establecer un proceso para revisar cambios, auditar accesos, aplicar actualizaciones y responder a incidentes.

Para cargas críticas, documentar controles y evidencias conforme a las políticas internas y obligaciones regulatorias aplicables. El producto por sí solo no garantiza cumplimiento.

## Guías de esta carpeta

- [Azure Pipelines](azure-pipelines.md): conceptos, YAML, agentes, despliegues, seguridad y patrones de CI/CD.
- [Azure DevOps Server](azure-devops-server.md): arquitectura autogestionada, planificación, respaldo, actualizaciones y operación.

## Documentación oficial

- [¿Qué es Azure DevOps?](https://learn.microsoft.com/azure/devops/user-guide/what-is-azure-devops)
- [Azure DevOps Services y Server](https://learn.microsoft.com/azure/devops/user-guide/about-devops-services)
- [Notas de versión de Azure DevOps Server](https://learn.microsoft.com/azure/devops/server/release-notes/azuredevopsserver)
