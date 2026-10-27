# Guía para migrar de .NET Framework a .NET moderno

## 1. Propósito y alcance

Esta guía ayuda a evaluar, planificar, ejecutar y operar la migración de aplicaciones construidas con .NET Framework hacia .NET moderno (la plataforma que desde .NET 5 se denomina simplemente .NET). Cubre aplicaciones web, escritorio, servicios, bibliotecas y procesos por lotes.

Una migración no consiste únicamente en cambiar el `TargetFramework`. Pueden cambiar el modelo de aplicación, las APIs, el sistema de proyectos, las dependencias, la configuración, la seguridad, el despliegue y la operación. El trabajo requerido depende del tipo de aplicación y de las tecnologías que utiliza.

La migración técnica debe distinguirse de la modernización posterior. Primero se busca una versión compatible, desplegable y soportable con comportamiento funcional equivalente. Después se pueden abordar contenedores, nube, microservicios, cambios de UX u otras transformaciones, salvo que sean requisitos explícitos del proyecto.

## 2. Selección de versión destino

La versión destino debe estar soportada durante el horizonte previsto del producto. Según la política oficial consultada el **27 de octubre de 2026**:

| Versión | Estado a la fecha de esta guía | Recomendación |
|---|---|---|
| .NET 10 LTS | Soportada hasta el 14 de noviembre de 2028 | Destino recomendado para una nueva migración, sujeto a compatibilidad y política corporativa. |
| .NET 8 LTS | Fin de soporte el 10 de noviembre de 2026 | No elegir para una migración nueva salvo que exista una transición temporal aprobada y con fecha de salida. |
| .NET 6 | Fuera de soporte desde el 12 de noviembre de 2024 | No usar como destino de producción. |
| .NET 11 | Versión preliminar/Go-Live antes de su disponibilidad general | Reservar para evaluación y pruebas controladas; comprobar su estado antes de decidir. |

Las fechas cambian con el ciclo de versiones. Confirmar la [política oficial de soporte de .NET](https://dotnet.microsoft.com/en-us/platform/support/policy) al aprobar el proyecto y durante su mantenimiento. .NET Framework es una plataforma distinta y su soporte depende del ciclo de vida del sistema operativo Windows.

## 3. Principios de migración

1. **Inventariar antes de estimar:** incluir proyectos, tecnologías, dependencias, integraciones, entornos y operación.
2. **Elegir por compatibilidad y vida útil:** no por ser la versión más conocida ni por minimizar solo el cambio inicial.
3. **Mantener una línea base funcional:** capturar pruebas, métricas y comportamiento del sistema actual.
4. **Reducir el tamaño de cada cambio:** avanzar por proyectos, capas o capacidades y revisar cada resultado.
5. **Mantener trazabilidad:** vincular hallazgos, riesgos, decisiones, cambios y criterios de aceptación.
6. **Revisar toda automatización:** herramientas y agentes pueden acelerar trabajo, pero sus cambios requieren validación humana.
7. **Planificar el retorno:** definir cómo detener o revertir el despliegue sin perder datos ni transacciones.

## 4. Evaluación inicial

### 4.1 Inventario técnico

Registrar al menos:

- Solución, proyectos, lenguaje, versión de .NET Framework, tipo de proyecto y sistema de compilación.
- Dependencias NuGet, referencias directas a ensamblados, paquetes privados y sus licencias.
- APIs específicas de Windows, COM, P/Invoke, componentes nativos y arquitectura x86/x64.
- Tecnologías de aplicación: ASP.NET MVC/Web API/Web Forms, WCF, WPF, Windows Forms, servicios Windows, WF u otras.
- Acceso a datos, proveedor, ORM, transacciones, procedimientos almacenados y versión de base de datos.
- Autenticación, autorización, certificados, cifrado, cookies y administración de secretos.
- Configuración, archivos, tareas programadas, colas, correo, reportes, impresión y servicios externos.
- Infraestructura y despliegue: IIS, servidores, instaladores, GAC, registro de Windows, permisos y rutas locales.
- Propietarios técnicos y de negocio, criticidad, ventanas de mantenimiento y objetivos de recuperación.

### 4.2 Línea base

Antes de modificar el sistema:

- Obtener una compilación reproducible desde un entorno limpio.
- Ejecutar y documentar pruebas unitarias, integración, aceptación y regresión disponibles.
- Capturar flujos críticos, contratos externos, tiempos de respuesta, consumo de recursos y errores conocidos.
- Revisar vulnerabilidades, deuda técnica y defectos existentes para no atribuirlos erróneamente a la migración.
- Confirmar acceso al código fuente, artefactos, certificados, configuraciones, datos de prueba y entornos.

### 4.3 Análisis de compatibilidad

Clasificar cada elemento como:

- **Compatible:** funciona en el destino con cambios pequeños o nulos.
- **Compatible con ajustes:** requiere cambios identificados y estimables.
- **Reemplazable:** hay una alternativa viable con costo y riesgo conocidos.
- **Bloqueante:** no existe una ruta aceptable sin rediseño, sustitución o permanencia temporal en .NET Framework.

Analizar código, paquetes de terceros, APIs, herramientas de compilación y dependencias operativas. La compilación por sí sola no demuestra compatibilidad funcional ni operacional.

## 5. Ruta según el tipo de aplicación

| Tipo | Consideraciones y ruta de evaluación |
|---|---|
| Bibliotecas, consola y procesos por lotes | Suelen ser buenos candidatos iniciales. Migrar primero bibliotecas con pocas dependencias y validar APIs, paquetes y comportamiento. |
| WPF y Windows Forms | Pueden ejecutarse sobre .NET moderno, pero siguen siendo aplicaciones Windows. Comprobar controles de terceros, diseñador, instalador, COM, impresión y dependencias nativas. |
| ASP.NET MVC, Web API y Web Forms sobre `System.Web` | ASP.NET Core tiene un modelo de aplicación diferente. Web Forms no se ejecuta en ASP.NET Core. Evaluar migración incremental o reimplementación de módulos; incluir autenticación, sesión, caché, filtros, configuración y despliegue. |
| WCF | Evaluar por separado cliente y servidor. Para servicios existentes, considerar compatibilidad temporal, CoreWCF donde aplique, o migración del contrato a HTTP APIs/gRPC. Revisar bindings, seguridad, transacciones y clientes consumidores. |
| Windows Services | Migrar al modelo de Worker Service/servicio hospedado u otra plataforma operativa. Revisar instalación, identidad, recuperación, logging, tareas y dependencias del sistema. |
| WF, .NET Remoting, AppDomains y COM+ | No asumir disponibilidad directa en .NET moderno. Identificar la función de negocio y comparar sustitutos, procesos aislados o rediseño. |
| Bibliotecas compartidas con aplicaciones antiguas | Considerar multi-targeting solo cuando haya consumidores que lo justifiquen y el costo de mantener varios destinos sea aceptable. |

La lista no es exhaustiva. Revisar la [guía oficial de portabilidad desde .NET Framework](https://learn.microsoft.com/en-us/dotnet/core/porting/framework-overview) y la documentación específica de cada modelo.

## 6. Estrategias de migración

### 6.1 Migración directa por aplicación

Se cambia el proyecto y sus dependencias en una rama controlada, se resuelven incompatibilidades y se despliega como una versión nueva. Es apropiada para aplicaciones pequeñas, desacopladas y con buena cobertura de pruebas. El riesgo aumenta cuando muchos cambios cruzados se acumulan en una sola entrega.

### 6.2 Migración incremental o coexistencia

Se migra una capacidad o ruta a la vez y se mantienen temporalmente componentes antiguos y nuevos. Para aplicaciones web puede utilizarse un proxy o enrutamiento gradual hacia módulos ASP.NET Core. Requiere contratos claros, observabilidad de ambos lados, estrategia de identidad/sesión y consistencia de datos. La guía oficial de [migración de ASP.NET Framework a ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/migration/fx-to-core/?view=aspnetcore-10.0) describe rutas incrementales e in-place.

### 6.3 Reemplazo planificado

Se conserva el comportamiento de negocio y se implementa una solución nueva cuando el modelo anterior no tiene una ruta viable. Debe dividirse por capacidades, priorizar flujos de mayor valor y comparar el costo de coexistencia y retiro. Evitar una reescritura total sin pruebas de concepto y entregas parciales.

### 6.4 Actualización intermedia de .NET Framework

Puede reducir deuda técnica y preparar paquetes o código para una futura migración. No convierte una aplicación en .NET moderno ni resuelve tecnologías no disponibles. Tratarla como una etapa con entregable y plazo propios, y no como sustituto indefinido de la migración.

## 7. Metodología por fases

### Fase 0: Mandato y alcance

- Acordar objetivos: soporte, seguridad, plataforma, rendimiento, costos o fin de vida.
- Definir qué queda dentro y fuera, aplicaciones, ambientes, equipos y responsables.
- Establecer restricciones regulatorias, tecnológicas, de calendario y proveedores.
- Identificar indicadores de éxito y autoridad para aceptar riesgos.

**Salida:** alcance aprobado, patrocinador, responsables y criterios de éxito medibles.

### Fase 1: Descubrimiento y evaluación

- Completar inventario y línea base.
- Ejecutar análisis de dependencias y compatibilidad.
- Identificar tecnologías bloqueantes y hacer pruebas de concepto tempranas.
- Clasificar aplicaciones por complejidad, criticidad, valor y dependencias entre sí.

**Salida:** informe de evaluación, mapa de dependencias, lista priorizada de brechas y estimación por rangos.

### Fase 2: Decisión de arquitectura y plan

- Elegir versión destino y comprobar su soporte.
- Seleccionar estrategia de migración por aplicación o capacidad.
- Definir arquitectura de convivencia, límites, contratos, datos e identidad cuando aplique.
- Registrar alternativas y consecuencias en un ADR.
- Preparar backlog, presupuesto, recursos, hitos, plan de pruebas, despliegue y retorno.

**Salida:** ADR, plan por oleadas, criterios de aceptación y registro de riesgos.

### Fase 3: Preparación

- Crear rama de migración, pipeline reproducible y entornos representativos.
- Actualizar primero la línea base .NET Framework solo si reduce brechas y no agrega riesgos injustificados.
- Reducir advertencias, dependencias abandonadas y referencias manuales innecesarias.
- Separar lógica de negocio de APIs específicas del framework cuando el diseño lo permita.
- Resolver dependencias críticas o probar sustitutos antes del cambio masivo.

**Salida:** entorno y código preparados, con pruebas y procedimiento de compilación reproducible.

### Fase 4: Migración piloto

- Seleccionar una aplicación o biblioteca representativa, de alcance limitado y con propietario disponible.
- Aplicar conversión de proyecto y cambios de API en incrementos pequeños.
- Validar la estrategia de coexistencia, despliegue, monitoreo y reversión.
- Actualizar estimaciones y riesgos con la evidencia obtenida.

**Salida:** patrón repetible, decisiones técnicas refinadas y estimación actualizada para las siguientes oleadas.

### Fase 5: Implementación por oleadas

- Migrar primero componentes hoja y bibliotecas compartidas de menor riesgo; luego capas con dependencias externas y finalmente interfaz/servicios cuando convenga.
- Mantener cambios pequeños y compilables; revisar cada paquete y API sustituida.
- Validar continuamente contratos, datos, seguridad, rendimiento y compatibilidad de despliegue.
- Evitar cambios funcionales no necesarios dentro del mismo conjunto de trabajo.

**Salida:** cada oleada compila, pasa las pruebas acordadas y tiene evidencia para avanzar.

### Fase 6: Validación y salida a producción

- Ejecutar regresión funcional, integración, seguridad, carga, resiliencia y recuperación.
- Hacer despliegue piloto/canario o por segmentos cuando la plataforma lo permita.
- Comparar métricas, errores y resultados de negocio con la línea base.
- Confirmar respaldo, migraciones de datos, monitoreo, soporte y procedimiento de reversión.

**Salida:** aceptación formal y decisión documentada de avanzar, pausar o revertir.

### Fase 7: Estabilización y retiro

- Vigilar errores, rendimiento, colas, trabajos, costos e incidentes durante el periodo acordado.
- Retirar componentes antiguos solo cuando consumidores, datos, certificados y tareas estén identificados y transferidos.
- Cerrar vulnerabilidades, documentación, capacitación, inventario y deuda temporal de coexistencia.
- Programar actualizaciones de parches y la siguiente revisión del ciclo de soporte.

**Salida:** operación asumida, componentes antiguos retirados o con excepción y fecha de salida aprobada.

## 8. Herramientas

| Herramienta | Uso | Límites y recomendaciones |
|---|---|---|
| GitHub Copilot modernization/upgrade agent | Evaluación asistida, plan, cambios automatizados y validación para proyectos .NET. | Funciona como asistente: revisar plan, cambios, dependencias y resultados. Trabajar en rama y conservar capacidad de revertir. Confirmar requisitos y disponibilidad de la edición usada. |
| .NET Upgrade Assistant | Herramienta anterior para analizar y aplicar cambios comunes. | Microsoft la marca oficialmente como obsoleta y recomienda el agente de modernización de GitHub Copilot para nuevos trabajos. Usarla solo si el agente no está disponible y aceptar que no está en desarrollo activo. |
| App and Code Assessment for .NET | Inventario y análisis de preparación, dependencias y escenarios de modernización, especialmente para evaluaciones de Azure. | Complementa el análisis; no sustituye una prueba de migración ni las pruebas funcionales. Confirmar versión, capacidades y requisitos vigentes antes de incorporarla. |
| Compilador, SDK y analizadores de .NET | Detectar incompatibilidades, APIs obsoletas, errores y problemas de configuración en cada compilación. | El análisis estático no detecta todos los cambios semánticos, operativos ni de datos. |
| CI/CD y repositorio Git | Reproducir compilaciones, ejecutar validaciones, revisar cambios y promover artefactos entre ambientes. | Pipeline debe reflejar el proceso real de publicación; no confiar solo en la máquina local. |
| Pruebas automatizadas y herramientas de carga | Comparar comportamiento, integración, seguridad, rendimiento y tolerancia a fallos. | Completar pruebas automatizadas con validación exploratoria de flujos críticos y telemetría en ambientes reales. |

Microsoft indica que [Upgrade Assistant está obsoleto](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-install) y recomienda el agente de modernización de GitHub Copilot. La [guía del agente](https://learn.microsoft.com/en-us/dotnet/core/porting/github-copilot-upgrade/overview) describe sus flujos de evaluación, planificación y ejecución. No aceptar resultados automáticos sin revisión técnica y funcional.

## 9. Riesgos principales y tratamiento

| Riesgo | Efecto posible | Tratamiento recomendado |
|---|---|---|
| APIs o tecnologías no disponibles | Bloqueo, rediseño o sustitución tardía | Inventario temprano; pruebas de concepto; alternativas y estimación antes de comprometer fecha. |
| Paquete de terceros incompatible o sin mantenimiento | Vulnerabilidades, falta de soporte o dependencia bloqueante | Confirmar compatibilidad y ciclo de vida con el proveedor; evaluar reemplazo, aislamiento o plan de salida. |
| Cambios de modelo web (`System.Web`, Web Forms) | Reescritura de módulos y riesgo en sesión/autenticación | Separar rutas y capacidades; definir estrategia incremental; probar cookies, identidad y contratos. |
| Diferencias de serialización y contratos | Integraciones rotas o incompatibilidad de mensajes | Pruebas contractuales, muestras reales, versionado y pruebas cruzadas con consumidores antiguos. |
| Configuración o secretos mal migrados | Fallos de arranque o exposición de credenciales | Separar configuración por ambiente; centralizar secretos; revisar permisos, certificados y rotación. |
| Comportamiento de base de datos distinto | Errores transaccionales, rendimiento o datos inconsistentes | Comparar consultas y transacciones; medir carga; respaldar; probar cambios y recuperación con datos representativos. |
| APIs específicas de Windows/COM/nativas | Fallos en Linux/contenedores o instalación | Decidir si el destino seguirá siendo Windows; probar arquitectura, permisos, drivers y runtimes nativos. |
| Cambio de identidad y autorización | Acceso indebido o usuarios bloqueados | Revisar inicio de sesión, tokens, roles, cookies, sesiones y flujos de renovación con pruebas de seguridad. |
| Rendimiento distinto | Degradación de latencia o capacidad | Crear línea base y pruebas comparables; perfilar; validar configuraciones y límites del host. |
| Coexistencia temporal prolongada | Costo y complejidad operativa crecientes | Asignar propietario, métricas de cierre y fecha límite a cada puente temporal. |
| Migración big bang | Corte prolongado, retorno difícil y riesgo de negocio | Reducir alcance, migrar por oleadas y ensayar despliegue y recuperación. |
| Herramienta automatizada aplica cambio incorrecto | Errores sutiles o deuda técnica | Revisar cada diff, ejecutar pruebas y hacer commits pequeños con revisión humana. |
| Destino próximo a fin de soporte | Repetición temprana de la migración | Elegir versión soportada por el horizonte requerido y mantener parches mensuales. |

## 10. Estrategia de pruebas

Definir pruebas antes de portar para poder comparar:

- **Caracterización/regresión:** reglas existentes, cálculos, validaciones, formatos y flujos críticos.
- **Integración y contratos:** APIs, mensajes, autenticación, colas, archivos, base de datos y sistemas consumidores.
- **Seguridad:** permisos, identidad, secretos, manejo de errores, dependencias y configuraciones de producción.
- **Rendimiento:** latencia, throughput, consumo, concurrencia y comportamiento bajo carga sostenida o picos.
- **Resiliencia:** caída de servicios externos, reintentos, timeouts, idempotencia, degradación y recuperación.
- **Despliegue y operación:** instalación, arranque, configuración, logs, métricas, alertas, backups y rollback.
- **Compatibilidad funcional:** comparar respuestas y datos entre la versión antigua y la nueva con escenarios equivalentes.

Los criterios de aceptación deben expresar umbrales, por ejemplo: flujos críticos pasan; contratos son compatibles; no hay vulnerabilidades críticas abiertas; latencia no supera el presupuesto acordado; y restauración/despliegue fueron ensayados.

## 11. Ejemplo de decisión: .NET Framework 4.6.x

Para una aplicación en .NET Framework 4.6.x:

1. Confirmar el tipo de aplicación y su dependencia de Windows, `System.Web`, WCF, COM, APIs nativas y bibliotecas de terceros.
2. Inventariar proyectos y consumidores; definir pruebas de caracterización y línea base.
3. Evaluar .NET 10 LTS como destino de referencia a la fecha de esta guía. No adoptar .NET 6 como destino productivo y evitar iniciar una migración nueva a .NET 8 dado su fin de soporte inminente.
4. Si la aplicación es una biblioteca o proceso desacoplado, probar conversión y migración directa en un piloto.
5. Si es ASP.NET Framework, definir migración incremental o reemplazo por módulos; incluir sesión, identidad y rutas como riesgos de primera clase.
6. Si es WPF/Windows Forms, mantener Windows como requisito de plataforma salvo que el alcance incluya sustituir la interfaz.
7. Mantener .NET Framework en convivencia solo con propietario, controles compensatorios y fecha de retiro aprobada.

Esta secuencia es una guía de decisión, no una promesa de compatibilidad. La evaluación técnica de la aplicación determina la ruta final.

## 12. Artefactos del proyecto

- Inventario de aplicaciones, proyectos, dependencias e integraciones.
- Matriz de compatibilidad con bloqueos, alternativas, responsables y estimaciones.
- ADR de versión destino y estrategia de migración.
- Backlog desglosado por aplicación, proyecto o capacidad.
- Línea base, estrategia y evidencia de pruebas.
- Plan de despliegue, monitoreo, respaldo y reversión.
- Registro de riesgos, excepciones y fecha de salida de componentes temporales.
- Runbook, documentación de soporte y transferencia operativa.

## 13. Lista de verificación

### Antes de iniciar

- [ ] Alcance, patrocinador, propietarios y criterios de éxito definidos.
- [ ] Inventario de proyectos, paquetes, integraciones y entornos completo.
- [ ] Versión destino confirmada con política de soporte vigente.
- [ ] Tecnologías bloqueantes y dependencias críticas probadas o con alternativa.
- [ ] Línea base y pruebas de flujos críticos disponibles.
- [ ] Estrategia, ADR, plan de oleadas, riesgos y retorno documentados.

### Antes de producción

- [ ] Build reproducible y artefactos aprobados en pipeline.
- [ ] Pruebas funcionales, contractuales, seguridad, rendimiento y resiliencia aceptadas.
- [ ] Configuración, secretos, permisos, certificados y telemetría revisados.
- [ ] Migraciones de datos probadas y respaldos/restauración verificados.
- [ ] Despliegue piloto y rollback ensayados.
- [ ] Soporte, alertas, responsables y ventana de observación confirmados.

### Al cerrar

- [ ] Deuda temporal y componentes antiguos retirados o con fecha aprobada.
- [ ] Documentación operativa y de arquitectura actualizada.
- [ ] Parches, soporte y próxima actualización incluidos en el ciclo de mantenimiento.
- [ ] Resultados, riesgos residuales y lecciones aprendidas registrados.

## 14. Fuentes oficiales

- [Política de soporte de .NET](https://dotnet.microsoft.com/en-us/platform/support/policy)
- [Portar de .NET Framework a .NET](https://learn.microsoft.com/en-us/dotnet/core/porting/framework-overview)
- [Descripción general de actualización de aplicaciones .NET](https://learn.microsoft.com/en-us/dotnet/core/porting/)
- [Migrar de ASP.NET Framework a ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/migration/fx-to-core/?view=aspnetcore-10.0)
- [Instalar .NET Upgrade Assistant: aviso de obsolescencia y alternativa recomendada](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-install)
- [GitHub Copilot upgrade: descripción general](https://learn.microsoft.com/en-us/dotnet/core/porting/github-copilot-upgrade/overview)
- [Cómo ejecutar una actualización .NET con GitHub Copilot](https://learn.microsoft.com/en-us/dotnet/core/porting/github-copilot-upgrade/dotnet-how-to-upgrade-with-github-copilot)
