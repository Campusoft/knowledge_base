# GitOps

> Documento revisado: 2026-11-03  
> Alcance: principios, flujo de trabajo, herramientas y recomendaciones para operar sistemas mediante estado declarado en Git.

## Descripción

**GitOps** es una forma de gestionar la configuración y los despliegues donde Git contiene el estado deseado del sistema y agentes automatizados lo aplican y comparan continuamente con el estado real.

Git funciona como fuente versionada de verdad: los cambios se revisan mediante commits y pull requests; un controlador observa el repositorio y reconcilia el entorno con la configuración aprobada. El término se asocia especialmente con Kubernetes, aunque sus principios también se aplican a configuración de plataformas e infraestructura.

GitOps no es una herramienta única. Requiere prácticas de control de cambios, un formato declarativo y un mecanismo que lea la configuración y la aplique.

## Principios

OpenGitOps define cuatro principios:

1. **Declarativo:** el sistema deseado se describe como configuración declarativa.
2. **Versionado e inmutable:** la configuración se conserva con historial y cambios identificables.
3. **Extraído automáticamente:** agentes de software obtienen el estado declarado desde su fuente.
4. **Reconciliado continuamente:** agentes observan el estado real y trabajan para alinearlo con el estado deseado.

Estos principios facilitan revisar quién cambió qué, reproducir una configuración y detectar diferencias entre lo declarado y lo que está desplegado.

## Cómo trabaja

```text
Desarrollador
     │ cambio propuesto
     ▼
Pull request → revisión, pruebas y controles
     │ merge
     ▼
Repositorio Git de configuración (estado deseado)
     │ lectura periódica o evento
     ▼
Controlador GitOps en el entorno
     │ obtiene y renderiza manifiestos
     ▼
Compara estado deseado con estado real
     │ aplica cambios permitidos y reconcilia
     ▼
Clúster o entorno objetivo
     │ estado y eventos
     └──────────────→ métricas, alertas y auditoría
```

Flujo típico:

1. El equipo define recursos, parámetros y políticas en archivos declarativos almacenados en Git.
2. Un cambio se propone mediante pull request. CI ejecuta validaciones como formato, pruebas, escaneo de seguridad y validación de manifiestos.
3. Tras la aprobación, el cambio se integra en la rama o ruta configurada como fuente del entorno.
4. Un controlador obtiene la revisión aprobada, renderiza la configuración —por ejemplo, con Kustomize o Helm— y compara el resultado con el entorno.
5. El controlador informa diferencias y, si la política lo permite, aplica los cambios para acercar el sistema al estado declarado.
6. El equipo revisa el estado de sincronización, salud, errores y eventos. Para revertir una configuración, normalmente se crea un nuevo cambio que restaura el estado anterior en Git.

El controlador puede consultar Git periódicamente o recibir notificaciones de cambios. Un webhook puede acelerar la detección, pero conviene mantener una reconciliación periódica para recuperarse de notificaciones perdidas.

## Componentes habituales

| Componente | Función |
|---|---|
| **Repositorio de configuración** | Guarda manifiestos, overlays, charts, parámetros por ambiente y políticas. |
| **Pull requests y controles** | Permiten revisar cambios y exigir pruebas, aprobaciones y validaciones antes del merge. |
| **Pipeline de CI** | Compila, prueba, escanea y publica artefactos. Puede proponer cambios de versiones en el repositorio de configuración. |
| **Controlador GitOps** | Lee la fuente declarada, calcula diferencias, aplica cambios y reporta el estado. |
| **Herramienta de renderizado** | Construye la configuración final desde plantillas o bases, por ejemplo Kustomize o Helm. |
| **Entorno objetivo** | Clúster de Kubernetes, plataforma o infraestructura gestionada. |
| **Observabilidad y auditoría** | Muestran sincronización, errores, salud, eventos y cambios aplicados. |
| **Gestión de secretos** | Proporciona credenciales sin exponer secretos en texto claro en Git. |

## GitOps y CI/CD

CI y GitOps se complementan. CI construye, prueba y publica artefactos; GitOps entrega la configuración aprobada al entorno mediante reconciliación. El pipeline puede actualizar el tag o digest de una imagen en Git, pero el controlador es quien observa ese cambio y sincroniza el entorno.

Una separación frecuente es mantener el código de la aplicación en un repositorio y la configuración de despliegue en otro. También pueden mantenerse juntos cuando el tamaño, los permisos y el ciclo de publicación del equipo lo permiten. Lo importante es que el repositorio y la revisión que gobiernan cada ambiente estén definidos con claridad.

## Herramientas

### Controladores de entrega

- **Argo CD:** plataforma declarativa de entrega continua para Kubernetes. Compara el estado declarado con el estado activo, muestra diferencias y puede sincronizar de forma manual o automática.
- **Flux:** conjunto extensible de controladores para mantener clústeres Kubernetes sincronizados con fuentes Git y otras fuentes soportadas. Incluye controladores para Kustomize, Helm, notificaciones y automatización de imágenes.

Ambas opciones cubren flujos GitOps para Kubernetes. La elección depende de la experiencia del equipo, el modelo de operación, la interfaz y las integraciones requeridas; conviene comparar las capacidades actuales en la documentación oficial antes de decidir.

### Herramientas complementarias

- **Kustomize:** organiza manifiestos y overlays sin requerir un lenguaje de plantillas propio.
- **Helm:** distribuye y parametriza aplicaciones Kubernetes mediante charts.
- **GitHub Actions, GitLab CI, Jenkins y otros sistemas CI:** ejecutan build, pruebas, análisis y publicación de artefactos. No sustituyen por sí mismos al controlador de reconciliación.
- **SOPS y gestores de secretos:** cifran secretos o los obtienen desde servicios externos. El control de acceso y las claves siguen necesitando administración segura.
- **Terraform u OpenTofu:** describen y aprovisionan infraestructura. Pueden integrarse en un flujo GitOps mediante automatización controlada, pero el archivo declarativo por sí solo no ofrece reconciliación continua del entorno.

## Casos de uso

- **Despliegue de aplicaciones Kubernetes:** mantener en Git los manifiestos, charts, valores y versiones aprobadas.
- **Configuración de clústeres:** gestionar namespaces, cuotas, políticas, operadores, control de acceso y componentes de plataforma.
- **Varios ambientes o clústeres:** promover cambios entre desarrollo, pruebas y producción mediante revisiones y configuración por ambiente.
- **Gestión de configuración de infraestructura:** versionar parámetros y cambios de infraestructura con trazabilidad y controles de revisión.
- **Recuperación y reconstrucción:** volver a aplicar una configuración conocida desde Git al crear o recuperar un entorno, sujeto a que los datos y dependencias externas también estén protegidos.
- **Auditoría de cambios:** relacionar una modificación desplegada con un commit, una revisión y sus aprobaciones.

## Beneficios

- Historial revisable y trazable de cambios.
- Menos credenciales de despliegue de largo plazo dentro de pipelines externos al clúster.
- Detección de diferencias entre el estado real y el declarado.
- Repetibilidad entre ambientes cuando la configuración y las dependencias están versionadas correctamente.
- Cambios más fáciles de revisar, aprobar y revertir mediante Git.
- Automatización continua del estado deseado y menor dependencia de intervención manual rutinaria.

## Limitaciones y riesgos

- **Git no protege automáticamente la infraestructura:** una mala configuración aprobada puede propagarse rápidamente, especialmente con sincronización automática.
- **Riesgo de borrado:** las políticas de poda pueden eliminar recursos que se quitan del repositorio; deben configurarse con cuidado.
- **Reconciliación eventual:** los cambios no siempre se aplican de inmediato; pueden existir demoras, errores de renderizado, fallas de red o permisos insuficientes.
- **Cambios manuales en el entorno:** pueden generar diferencias y ser revertidos por el controlador. Hay que definir cómo atender emergencias y registrar cualquier intervención.
- **Secretos:** guardar secretos sin cifrar en Git expone credenciales; el cifrado tampoco elimina la necesidad de proteger las claves y los permisos de lectura.
- **Bases de datos y efectos externos:** revertir un commit no necesariamente revierte una migración, una escritura externa, una eliminación de datos ni otros efectos no reversibles.
- **Escala y administración del repositorio:** muchos equipos y ambientes pueden requerir convenciones, separación de permisos, revisión de dependencias y límites claros de propiedad.
- **Dependencia del controlador:** requiere operación, actualizaciones, respaldo de su configuración, acceso a la fuente y permisos adecuados sobre los destinos.
- **Ámbito de uso:** Argo CD y Flux se orientan a Kubernetes. Para sistemas no Kubernetes se debe verificar si existe un reconciliador compatible o si conviene otra arquitectura.

## Recomendaciones

1. Definir qué repositorio, rama, revisión y ruta representan el estado deseado de cada ambiente.
2. Proteger ramas y exigir revisión por pares, CI y aprobaciones apropiadas para cambios de producción.
3. Mantener separados los permisos de CI y del controlador; limitar el acceso del controlador a los recursos que administra.
4. Usar tags inmutables o digests para imágenes y artefactos; evitar desplegar automáticamente una etiqueta mutable como `latest`.
5. Revisar cuidadosamente las políticas de eliminación automática y probar poda y reemplazo primero en ambientes no productivos.
6. Gestionar secretos con cifrado o referencias a un gestor externo; nunca almacenar credenciales en texto claro en Git.
7. Separar parámetros por ambiente y promover cambios mediante revisiones; evitar copiar configuraciones divergentes sin control.
8. Medir sincronización, estado de salud y fallos de reconciliación; configurar alertas para recursos fuera de sincronía durante períodos relevantes.
9. Documentar el procedimiento de emergencia para pausar reconciliación, realizar un cambio manual y devolver el entorno al control de Git.
10. Tratar migraciones de bases de datos y operaciones con efectos irreversibles como flujos especiales, con copia de seguridad, validación y aprobación.
11. Empezar con una aplicación y un ambiente de prueba, y ampliar el alcance después de validar permisos, políticas y recuperación.

## Ubicación en esta base

Este documento se ubica en `Operation/GitOps.md`, junto a [DevOps](DevOps.md), [DevSecOps](DevSecOps.md) y [Observabilidad](Observabilidad.md), porque GitOps describe cómo entregar y operar sistemas usando cambios declarativos y reconciliación continua.

## Referencias

- [OpenGitOps: principios](https://opengitops.dev/)
- [Argo CD: conceptos principales](https://argo-cd.readthedocs.io/en/stable/core_concepts/)
- [Argo CD: sincronización automática](https://argo-cd.readthedocs.io/en/stable/user-guide/auto_sync/)
- [Argo CD: automatización desde CI](https://argo-cd.readthedocs.io/en/stable/user-guide/ci_automation/)
- [Flux: documentación](https://fluxcd.io/flux/)
- [Flux: componentes del GitOps Toolkit](https://fluxcd.io/flux/components/)
- [Flux: Kustomize Controller](https://fluxcd.io/flux/components/kustomize/)
