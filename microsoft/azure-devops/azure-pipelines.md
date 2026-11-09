# Azure Pipelines

> **Revisado:** 2026-11-08  
> **Alcance:** conceptos y prácticas de integración y entrega continua con Azure Pipelines, independientes del lenguaje y del destino del despliegue.

## Qué es

**Azure Pipelines** es el servicio de CI/CD de Azure DevOps. Ejecuta flujos automatizados para validar cambios, crear versiones y desplegar aplicaciones. Puede trabajar con Azure Repos, GitHub y otros repositorios compatibles, y con diversos lenguajes, sistemas operativos y destinos.

Está disponible en **Azure DevOps Services** y **Azure DevOps Server**, aunque algunos tipos de agentes y capacidades dependen del modelo y la versión. Compruebe la documentación de cada función antes de diseñar una arquitectura.

## Conceptos principales

| Concepto | Función |
|---|---|
| **Pipeline** | Flujo versionable que define cuándo y cómo validar, compilar, probar, publicar o desplegar. |
| **Trigger** | Evento manual, programado o asociado a cambios que inicia una ejecución. |
| **Stage (etapa)** | Agrupa una fase lógica, como build, test, staging o producción; puede tener dependencias y controles. |
| **Job (trabajo)** | Unidad de ejecución dentro de una etapa. Normalmente se ejecuta en un agente; algunos jobs de validación pueden ser agentless. |
| **Step (paso)** | Acción concreta dentro del job: script o tarea. |
| **Task (tarea)** | Acción empaquetada y configurable para una operación frecuente. Revisar origen, permisos y mantenimiento de tareas externas. |
| **Agent (agente)** | Proceso que recibe y ejecuta los jobs en una máquina hospedada o administrada por la organización. |
| **Agent pool (grupo de agentes)** | Conjunto de agentes disponibles para ciertos proyectos o pipelines, con controles de acceso y capacidades. |
| **Artifact (artefacto)** | Archivo o paquete producido por una ejecución y disponible para etapas posteriores o distribución. |
| **Environment (entorno)** | Recurso que representa un destino de despliegue y puede aportar historial, permisos, aprobaciones y controles. |
| **Service connection (conexión de servicio)** | Conexión autenticada a un servicio o destino externo, con permisos y alcance que deben limitarse. |

## YAML y tipos de pipeline

Los **pipelines YAML** describen etapas, trabajos, pasos, variables y condiciones en un archivo versionado con el código. Facilitan revisión por pull request, reutilización mediante plantillas y cambios trazables.

Azure DevOps también conserva **Classic pipelines**, configurados desde la interfaz. Pueden ser relevantes para definiciones existentes o migraciones graduales, pero se debe decidir cómo revisar y auditar cambios que no están expresados junto al código. No mezclar ambos modelos sin una convención clara de propiedad y mantenimiento.

Un diseño YAML frecuente separa compilación y pruebas de los despliegues. La primera parte produce un artefacto versionado; etapas posteriores descargan y promueven ese mismo artefacto a entornos. Así se evita reconstruir código diferente para cada ambiente.

## Flujo de referencia

```text
Cambio en repositorio
        ↓
Validación de rama o pull request
        ↓
Compilación → pruebas → análisis y controles
        ↓
Publicar artefacto versionado y evidencia
        ↓
Desplegar a entorno de prueba
        ↓
Aprobaciones y controles requeridos para producción
        ↓
Desplegar el mismo artefacto y observar el resultado
```

1. Definir disparadores para ramas, pull requests, tags o programaciones según el flujo del equipo.
2. Validar formato, dependencias, compilación, pruebas y controles de seguridad apropiados.
3. Publicar artefactos identificables; evitar incluir secretos en ellos.
4. Promover el artefacto validado a entornos siguientes con configuración externa por entorno.
5. Proteger recursos de producción con permisos, aprobaciones y comprobaciones aplicables.
6. Registrar el resultado y vincularlo al cambio, revisión y versión desplegada.

## Agentes y ejecución

Los agentes hospedados por Microsoft están disponibles para Azure DevOps Services. Los agentes autohospedados se ejecutan en infraestructura de la organización y pueden usarse con Services o Server. En Azure DevOps Server se requieren agentes propios. También existen opciones de agentes administrados y escalables, cuya disponibilidad depende del modelo de servicio.

Un agente ejecuta código de pipeline, por lo que debe tratarse como un límite de seguridad:

- Preferir máquinas limpias o agentes efímeros para reducir residuos entre trabajos.
- Separar pools por entorno, proyecto y nivel de sensibilidad; evitar que trabajos no confiables compartan recursos con despliegues productivos.
- Ejecutar agentes con identidad de bajo privilegio, acceso de red mínimo y versiones actualizadas.
- Proteger los directorios del agente, cachés, logs y artefactos; destruir o limpiar el entorno cuando termine el trabajo si corresponde.
- Revisar cuidadosamente tareas y scripts de terceros, ya que un pipeline puede ejecutar comandos arbitrarios.

## Entornos, aprobaciones y comprobaciones

Los environments organizan destinos de despliegue y aportan historial y controles. Las aprobaciones y comprobaciones pueden proteger recursos, entornos y conexiones antes de que una etapa los utilice. En Azure Pipelines Services, algunos controles se administran fuera del YAML por propietarios de los recursos; esto evita que un autor de pipeline cambie por sí solo el control que debe superar. Verifique disponibilidad y comportamiento en la versión de Server instalada.

Use controles de acuerdo con el riesgo:

- Para producción, limitar quién puede autorizar y quién puede modificar la definición de pipeline.
- Impedir, cuando sea necesario, que quien propone un cambio se apruebe a sí mismo.
- Usar controles de rama, plantillas obligatorias, políticas y ventanas de cambio cuando correspondan.
- Evitar que una aprobación manual sea el único control de calidad o seguridad.
- Registrar el artefacto, la identidad que aprobó y el resultado del despliegue.

## Seguridad

Azure Pipelines tiene capacidad de ejecutar código y acceder a recursos: la seguridad depende tanto de la configuración como de la plataforma.

- Restringir permisos de repositorios, pipelines, pools, conexiones de servicio, variables y entornos.
- Usar identidades de proyecto con alcance acotado y conexiones de servicio específicas; habilitar el acceso a todos los pipelines solo cuando esté justificado.
- Guardar secretos en variables secretas o gestores de secretos integrados, no en YAML, repositorios, argumentos visibles o logs.
- Preferir autenticación de corta duración y rotar credenciales cuando se usen secretos persistentes.
- No exponer secretos o agentes internos a builds de forks o código no revisado.
- Separar los pools que manejan código no confiable de los que acceden a entornos sensibles.
- Limitar egreso de red, privilegios del agente y acceso a metadatos de nube.
- Auditar modificaciones a pipelines y recursos protegidos; mantener agentes, tareas y dependencias actualizados.

Microsoft recomienda considerar las amenazas de los agentes autohospedados y separar pools para trabajos sensibles. Estas recomendaciones son especialmente relevantes en entidades financieras; no sustituyen la evaluación de riesgo ni los controles regulatorios de la organización.

## Patrones habituales

### CI para pull requests

Ejecutar pruebas rápidas y análisis antes de integrar. No entregar secretos de despliegue a esta validación. Exigir éxito de checks y revisión según política de rama.

### Entrega continua con aprobación

Automatizar build, pruebas y despliegue a entornos no productivos. Mantener la versión lista y exigir autorización o comprobaciones antes de producción si el riesgo o la política lo requieren.

### Despliegue continuo

Desplegar automáticamente a producción solo cuando los controles, pruebas, estrategia de lanzamiento y recuperación permiten hacerlo de forma segura. Puede aplicarse a servicios de bajo riesgo sin implicar que todos los cambios deban seguir el mismo nivel de automatización.

### Pipeline de promoción de artefactos

Construir una sola vez y promover artefacto inmutable entre entornos. Conservar metadatos de versión, commit, dependencias, resultados de controles y despliegues.

## Buenas prácticas de diseño y operación

- Mantener definiciones y plantillas bajo control de versiones; usar plantillas compartidas para estándares, con cambios revisados y versionados.
- Mantener jobs pequeños, repetibles y con timeouts; usar paralelismo y caché sin comprometer reproducibilidad.
- Separar build de deploy y publicar artefactos con identificadores únicos.
- Evitar lógica de negocio compleja dentro del YAML; mover scripts reutilizables al repositorio y revisarlos como código.
- Definir convenciones de nombres, ownership, retención y permisos para pipelines, pools, environments y service connections.
- Probar la recuperación, no solo el camino exitoso; aclarar qué hacer ante despliegue parcial o migraciones de base de datos.
- Monitorear duración, fallos, colas de agentes y tasa de despliegues fallidos; mejorar con datos, sin usar métricas como cuotas individuales.
- Administrar extensiones, tareas y agentes como dependencias de la cadena de suministro de software.

## Documentación oficial

- [Conceptos clave de Azure Pipelines](https://learn.microsoft.com/azure/devops/pipelines/get-started/key-pipelines-concepts)
- [Pipelines YAML](https://learn.microsoft.com/azure/devops/pipelines/yaml-schema)
- [Agentes de Azure Pipelines](https://learn.microsoft.com/azure/devops/pipelines/agents/agents)
- [Aprobaciones y comprobaciones](https://learn.microsoft.com/azure/devops/pipelines/process/approvals)
- [Seguridad de Azure Pipelines](https://learn.microsoft.com/azure/devops/pipelines/security/overview)
- [Seguridad de agentes autohospedados](https://learn.microsoft.com/azure/devops/pipelines/agents/agents#information-security-for-self-hosted-agents)
- [Azure Pipelines en Azure DevOps Server](https://learn.microsoft.com/azure/devops/pipelines/agents/agents)
