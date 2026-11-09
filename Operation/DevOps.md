# DevOps

> **Propósito:** presentar los principios, prácticas y capacidades que permiten a un equipo construir, entregar y operar software de forma colaborativa, segura y confiable. La guía es independiente de proveedores y herramientas; los nombres de productos son ejemplos sustituibles.

## Qué es DevOps

**DevOps** es una forma de organizar el trabajo y la responsabilidad entre quienes desarrollan, entregan y operan un producto o servicio. Busca reducir las barreras entre esas funciones y crear un ciclo continuo de cambios pequeños, validación, entrega, operación y aprendizaje.

DevOps combina **personas, procesos y tecnología**. No es un cargo, una herramienta, un equipo separado ni un sinónimo de automatización. La automatización ayuda a hacer repetibles las tareas, pero no reemplaza la colaboración, la responsabilidad compartida ni la mejora continua.

El resultado esperado es entregar valor con frecuencia y de forma predecible, manteniendo la calidad, la seguridad y la capacidad de responder a fallos y cambios.

## Alcance y conceptos relacionados

- **CI (integración continua):** integrar cambios pequeños con frecuencia y validar automáticamente que se compilen, cumplan controles y funcionen con el resto del sistema.
- **Entrega continua (continuous delivery):** mantener cada cambio validado en un estado que pueda publicarse. El paso a producción puede requerir una decisión o aprobación explícita.
- **Despliegue continuo (continuous deployment):** publicar automáticamente en producción los cambios que superan las validaciones y políticas definidas.
- **CI/CD:** conjunto de prácticas y automatizaciones para integrar cambios, producir versiones y entregarlas a los entornos. CI/CD es una capacidad importante de DevOps, pero por sí sola no cubre operación, cultura, diseño ni aprendizaje.
- **Infraestructura como código (IaC):** describir y versionar infraestructura mediante archivos y definiciones revisables, para validarla y reproducirla de forma controlada.
- **Observabilidad:** usar señales como métricas, registros y trazas para entender el comportamiento del sistema y diagnosticar problemas.
- **SRE (Site Reliability Engineering):** disciplina que aplica ingeniería y objetivos de confiabilidad a la operación de servicios. Puede complementar DevOps con objetivos de nivel de servicio y presupuestos de error.
- **DevSecOps:** integrar seguridad en las decisiones, el ciclo de entrega y la operación. Se desarrolla en la [guía de DevSecOps](DevSecOps.md).
- **GitOps:** gestionar configuración y cambios operativos declarados y versionados en Git. Se desarrolla en la [guía de GitOps](GitOps.md).

## Principios

1. **Responsabilidad compartida:** quienes crean un servicio participan en su calidad, entrega y comportamiento en producción, con el apoyo de operaciones, seguridad y plataforma.
2. **Colaboración y transparencia:** el trabajo, las decisiones, los cambios y el estado de los servicios deben ser visibles para las personas que los mantienen.
3. **Valor en entregas pequeñas:** dividir el trabajo en cambios acotados para reducir el riesgo, obtener comentarios pronto y facilitar la recuperación.
4. **Automatizar tareas repetibles:** automatizar compilación, validación, aprovisionamiento y publicación cuando mejore consistencia y reduzca errores manuales.
5. **Calidad desde el inicio:** integrar pruebas, revisión, controles de seguridad y requisitos operativos en el diseño y el desarrollo.
6. **Feedback rápido y útil:** mostrar resultados de validaciones y operación a quienes puedan actuar sobre ellos, con contexto suficiente para corregirlos.
7. **Sistemas reproducibles:** versionar código, configuración e infraestructura; producir artefactos identificables y promoverlos de forma consistente entre entornos.
8. **Seguridad y mínimo privilegio:** proteger datos y credenciales, limitar permisos y aplicar controles proporcionales al riesgo en todo el ciclo.
9. **Confiabilidad y recuperación:** diseñar para detectar fallos, limitar su impacto y restaurar el servicio con procedimientos conocidos.
10. **Aprendizaje sin culpa:** investigar incidentes para entender las condiciones que los hicieron posibles y mejorar el sistema, no para buscar culpables.
11. **Mejora continua basada en evidencia:** revisar resultados, incidentes, tiempos y comentarios de usuarios para priorizar cambios en el producto y el proceso.

## Ciclo de trabajo

El ciclo es continuo. Las etapas pueden variar según el tipo de producto; sus resultados deben alimentar las decisiones siguientes.

```text
Planificar → Desarrollar → Integrar y validar → Empaquetar → Entregar y desplegar
     ↑                                                            ↓
     └────────────── Operar, observar y aprender ─────────────────┘
```

| Etapa | Propósito | Prácticas habituales |
|---|---|---|
| **Planificar** | Acordar el problema, el valor esperado y las condiciones de éxito. | Priorizar trabajo, identificar dependencias, definir requisitos funcionales, de seguridad y operación. |
| **Desarrollar** | Crear cambios pequeños y comprensibles. | Control de versiones, revisión por pares, estándares de código y gestión segura de secretos. |
| **Integrar y validar** | Detectar problemas antes de distribuir un cambio. | Compilación, pruebas automáticas, análisis estático y validaciones de políticas. |
| **Empaquetar** | Producir una unidad de entrega identificable. | Versionar artefactos, registrar procedencia y conservar resultados de validación. |
| **Entregar y desplegar** | Promover la versión a los entornos y ponerla a disposición de usuarios. | Gestión de configuración, aprobaciones según riesgo, despliegues graduales y reversión o corrección. |
| **Operar y observar** | Mantener el servicio útil, seguro y confiable. | Métricas, registros, trazas, alertas accionables, soporte e incidentes. |
| **Aprender** | Incorporar lo observado en el producto y en el proceso. | Revisiones de incidentes, análisis de tendencias y ajustes priorizados. |

## Capacidades y componentes

Una implementación de DevOps reúne capacidades. No todas requieren productos separados ni una arquitectura concreta.

| Capacidad | Qué resuelve | Ejemplos de prácticas o componentes |
|---|---|---|
| **Gestión del trabajo** | Hace visible la demanda, las prioridades y el estado del trabajo. | Backlog, límites de trabajo en curso, criterios de aceptación y acuerdos entre equipos. |
| **Control de cambios** | Conserva historial y permite revisar y rastrear cambios. | Control de versiones, ramas o integración basada en troncal, revisiones y etiquetas de versión. |
| **Integración y entrega** | Valida cambios y los promueve de forma repetible. | Pipelines, compilación, pruebas, aprobación, publicación y promoción de artefactos. |
| **Pruebas y calidad** | Comprueba comportamiento y atributos relevantes. | Pruebas unitarias, integración, extremo a extremo, rendimiento, accesibilidad y análisis de calidad. |
| **Gestión de configuración e infraestructura** | Mantiene entornos consistentes y reproducibles. | Parámetros por entorno, IaC, gestión de configuración y detección de diferencias. |
| **Seguridad y cumplimiento** | Reduce riesgos y facilita demostrar controles. | Gestión de identidades y secretos, análisis de dependencias, políticas, registros de auditoría y controles automatizados. |
| **Gestión de artefactos** | Almacena versiones entregables con trazabilidad. | Repositorios de paquetes o imágenes, versionado, firmas y metadatos de procedencia. |
| **Despliegue y operación** | Cambia servicios de forma controlada y los mantiene disponibles. | Gestión de releases, despliegues graduales, capacidad de reversión, respaldo y recuperación. |
| **Observabilidad y respuesta** | Detecta degradaciones y orienta el diagnóstico. | Métricas, registros, trazas, alertas, tableros, guardias y procedimientos de respuesta. |
| **Plataforma y autoservicio** | Reduce trabajo repetido y ofrece caminos seguros para los equipos. | Plantillas, entornos bajo demanda, documentación operativa y servicios internos con guardrails. |

## Casos de uso

- **Validar cada cambio:** ejecutar compilación, pruebas y controles al proponer o integrar código para encontrar errores cerca de su origen.
- **Entregar una aplicación web o API:** generar una versión identificable, desplegarla primero en un entorno de validación y promoverla con controles apropiados.
- **Aprovisionar entornos repetibles:** describir infraestructura y configuración de forma versionada para crear entornos coherentes de desarrollo, prueba y producción.
- **Publicar cambios de bajo riesgo con frecuencia:** automatizar el despliegue cuando las pruebas, la observabilidad y la capacidad de recuperación son suficientes.
- **Coordinar cambios de mayor riesgo:** añadir validaciones, ventanas, aprobaciones, despliegues por etapas y planes de recuperación según el impacto.
- **Operar sistemas heredados o híbridos:** aplicar control de cambios, automatización progresiva y observabilidad incluso cuando no se puede automatizar todo el ciclo.
- **Gestionar microservicios o varios equipos:** establecer interfaces, ownership, contratos y mecanismos comunes de entrega sin obligar a todos los servicios a usar idéntica implementación.
- **Responder y aprender de incidentes:** detectar impacto, coordinar respuesta, restaurar el servicio e incorporar acciones de mejora al trabajo priorizado.

## Herramientas por capacidad

Elija herramientas por necesidades, restricciones y capacidades operativas. Los ejemplos de la tabla son ilustrativos, no recomendaciones ni requisitos.

| Necesidad | Tipos de herramientas | Ejemplos intercambiables |
|---|---|---|
| Control de versiones y revisión | Repositorios Git y revisión de cambios | GitHub, GitLab, Bitbucket, Gitea |
| Automatización de CI/CD | Motores de pipeline o servidores de automatización | GitHub Actions, GitLab CI/CD, Jenkins, Buildkite, Azure Pipelines |
| Compilación y pruebas | Compiladores, gestores de paquetes y frameworks de prueba | Dependen del lenguaje, plataforma y arquitectura del producto |
| Artefactos y dependencias | Registros y repositorios de paquetes o imágenes | OCI registries y repositorios de paquetes públicos o privados |
| Infraestructura y configuración | Herramientas de IaC, aprovisionamiento y configuración | OpenTofu, Terraform, Ansible, Pulumi |
| Contenedores y ejecución | Motores de contenedores y plataformas de orquestación | Podman, Docker, Kubernetes y servicios gestionados equivalentes |
| Despliegue declarativo | Controladores de reconciliación y gestores de configuración | Argo CD, Flux, Helm, Kustomize |
| Observabilidad | Herramientas de métricas, logs, trazas y alertas | OpenTelemetry, Prometheus, Grafana, Elastic y servicios equivalentes |
| Seguridad | Análisis de código, dependencias, secretos, imágenes y políticas | Gitleaks, Trivy, Semgrep, herramientas SAST/SCA y gestores de secretos |
| Gestión del trabajo y documentación | Tableros, catálogos de servicios y documentación | Jira, Linear, Backstage y documentación como código |

Al comparar opciones, considere integración con el entorno existente, facilidad de mantenimiento, seguridad, control de acceso, portabilidad de datos, extensibilidad, costos totales, soporte, habilidades del equipo y requisitos regulatorios. Evite elegir por popularidad o acumular herramientas que dupliquen funciones sin resolver una necesidad clara.

Para una referencia específica de automatización con GitHub Actions, consulte [GitHub Actions](../git/github.md). Para el inventario de herramientas de plataforma disponible en esta carpeta, consulte [Tools](Tools.md). Para detalles de la plataforma Microsoft, consulte [Azure DevOps](../microsoft/azure-devops/azure-devops.md), [Azure Pipelines](../microsoft/azure-devops/azure-pipelines.md) y [Azure DevOps Server](../microsoft/azure-devops/azure-devops-server.md).

## Buenas prácticas

### Flujo y cambios

- Mantener cambios pequeños, revisables y frecuentes; reducir ramas de larga duración y lotes grandes de trabajo.
- Definir qué condiciones permiten integrar, publicar y desplegar; hacer visibles los resultados y responsables.
- Construir el artefacto una vez y promover esa misma versión entre entornos, aplicando configuración externa según el entorno.
- Mantener trazabilidad entre solicitud, cambio, validaciones, artefacto y despliegue.
- Separar los permisos de construcción y despliegue según la necesidad; evitar credenciales amplias y de larga duración.

### Pruebas y controles

- Usar varias capas de pruebas automatizadas y mantenerlas rápidas y deterministas donde sea posible.
- Probar también infraestructura, migraciones, configuración, restauración y procedimientos operativos relevantes.
- Aplicar controles de seguridad y calidad durante el flujo, con excepciones explícitas, justificadas, limitadas y revisables.
- Proteger secretos con mecanismos dedicados; no almacenarlos en código, logs, imágenes ni artefactos.

### Despliegue y operación

- Preferir estrategias de despliegue que permitan observar el impacto y limitar exposición, como despliegues graduales o por grupos, cuando la arquitectura lo permita.
- Diseñar cambios compatibles con versiones que puedan coexistir; planificar migraciones de datos y cambios de interfaces.
- Definir cómo detectar fallos, detener una promoción, restaurar el servicio o aplicar una corrección. Una reversión de código no siempre revierte cambios de datos.
- Establecer niveles de servicio y alertas que reflejen impacto para usuarios; asignar responsables para responder.
- Practicar respaldos, restauración, recuperación ante desastres y respuesta a incidentes; medir los tiempos reales.

### Organización y mejora

- Asignar ownership claro para servicios, pipelines, infraestructura y alertas.
- Proveer caminos recomendados y autoservicio con guardrails, evitando bloquear innecesariamente la autonomía de cada equipo.
- Documentar procedimientos que se usan en operación y mantenerlos cerca del servicio.
- Revisar incidentes de forma constructiva y convertir hallazgos en acciones priorizadas, con seguimiento.
- Medir resultados del sistema de entrega y la experiencia de los equipos; no usar métricas para incentivar atajos o comparar personas.

## Métricas y señales de mejora

Las métricas sirven para entender tendencias y orientar experimentos. Conviene combinarlas y segmentarlas por servicio, evitando convertirlas en cuotas individuales.

- **Frecuencia de despliegue:** con qué frecuencia se publica una versión en un entorno relevante.
- **Tiempo de entrega del cambio:** tiempo desde que el trabajo comienza o el cambio se integra hasta que queda disponible.
- **Tasa de cambios fallidos:** proporción de despliegues que causan degradación, incidente o reversión.
- **Tiempo de recuperación:** cuánto tarda el servicio en recuperarse tras un fallo relacionado con un cambio.
- **Calidad del flujo:** duración y tasa de fallos de compilaciones, pruebas y despliegues; tiempo de espera y retrabajo.
- **Confiabilidad y experiencia:** disponibilidad, latencia, errores y resultados observados por los usuarios, comparados con objetivos de servicio.
- **Seguridad y mantenimiento:** tiempo para corregir vulnerabilidades relevantes, deuda operativa, cumplimiento de controles y exposición de credenciales.

Las cuatro primeras son dimensiones habitualmente asociadas con DORA. Deben interpretarse en el contexto del producto, el tamaño del servicio y los riesgos; aumentar la frecuencia por sí sola no demuestra una mejora.

## Antipatrones

- Confundir DevOps con comprar una herramienta, crear un equipo de despliegue o renombrar operaciones.
- Automatizar un proceso frágil sin corregir sus causas, o automatizar sin observabilidad y recuperación.
- Convertir el pipeline en un flujo lento, opaco y con controles que no corresponden al riesgo.
- Usar aprobaciones manuales como sustituto de pruebas, propiedad clara o controles automatizados.
- Medir productividad por cantidad de commits, tareas o despliegues individuales.
- Compartir credenciales entre equipos o dar a los pipelines privilegios mayores de los necesarios.
- Mantener ambientes únicos e irreproducibles o permitir cambios manuales sin registro ni reconciliación.
- Culpar a una persona por incidentes sin corregir condiciones sistémicas que pueden repetirse.

## Lecturas relacionadas

- [DevSecOps](DevSecOps.md): prácticas de seguridad integradas en el ciclo de entrega.
- [GitOps](GitOps.md): operación y despliegues guiados por estado declarado y versionado.
- [Observabilidad](Observabilidad.md): señales, monitoreo y diagnóstico de sistemas.
- [GitHub Actions](../git/github.md): ejemplo de automatización CI/CD específico de una plataforma.
- [Gitleaks](../git/gitleaks.md): detección de secretos y ejemplo de integración en CI.
- [Training DevOps](Training_DevOps.md): ejercicios y recursos de formación.
- [Docker y Kubernetes](../cloud/container.md): notas sobre contenedores y orquestación.
