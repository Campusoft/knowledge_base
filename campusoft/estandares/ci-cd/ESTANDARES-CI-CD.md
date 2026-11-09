# Estándares CI/CD

> **Copia referencial.** Este documento conserva el contenido y estado de la propuesta de estándar CI/CD utilizada como base para esta referencia. Ver [guía de aplicación CI/CD](../../guias/ci-cd/README.md) para orientación por tecnología.

## Estado del documento

| Campo | Valor |
|---|---|
| Estado | Propuesta inicial para revisión y aprobación |
| Alcance | Proyectos de software |
| Tipo | Estándar transversal de ingeniería y operación |

Este documento establece objetivos y controles esperados para la integración, entrega y despliegue de software. No prescribe lenguajes, comandos, proveedores ni productos. Cada proyecto debe definir en documentación tecnológica cómo satisface estos controles y justificar la selección de mecanismos según su contexto.

## 1. Propósito

El proceso CI/CD debe permitir integrar cambios con confianza y desplegar versiones de forma controlada, trazable y recuperable. Debe detectar defectos y riesgos antes de producción, proteger credenciales y conservar evidencia del cambio validado y publicado.

## 2. Principios

- **Repetibilidad:** una misma revisión debe producir resultados consistentes bajo condiciones controladas.
- **Automatización:** las verificaciones repetibles deben ejecutarse automáticamente en los puntos definidos por el proyecto.
- **Trazabilidad:** los resultados y despliegues deben vincularse al cambio, versión o artefacto correspondiente.
- **Mínimo privilegio:** cada tarea recibe solo los accesos necesarios para su función.
- **Separación de responsabilidades:** validar código y publicar en ambientes protegidos son actividades con permisos diferenciados.
- **Proporcionalidad al riesgo:** la profundidad de controles considera criticidad, exposición y sensibilidad de datos.
- **Recuperabilidad:** el proyecto debe prever cómo detener o revertir una publicación fallida.

## 3. Integración continua

Los cambios deben validarse automáticamente antes de integrarse en las ramas protegidas. El conjunto aplicable se define para cada proyecto e incluye, según corresponda:

- convenciones y formato del código;
- análisis estático y validaciones de consistencia;
- pruebas automatizadas con cobertura adecuada al riesgo;
- compilación, empaquetado o validación equivalente;
- detección de secretos;
- revisión de dependencias y componentes externos;
- controles de seguridad del código y de la configuración;
- generación de resultados asociados al cambio revisado.

Los controles designados como obligatorios deben impedir la integración cuando fallen. Las excepciones requieren justificación, responsable, alcance limitado y fecha o condición de revisión.

## 4. Detección de secretos

Los proyectos deben establecer mecanismos para detectar credenciales y otros secretos en el código y en los cambios propuestos. La implementación debe cubrir los cambios antes de su integración y considerar el historial cuando exista posibilidad de exposición previa.

El control debe:

- hacer visibles los hallazgos a quienes revisan el cambio, sin revelar el valor secreto en registros o reportes;
- bloquear la integración ante un secreto confirmado;
- permitir exclusiones acotadas, justificadas, revisables y sin patrones amplios que oculten hallazgos;
- distinguir secretos reales de falsos positivos mediante un proceso documentado;
- proteger las credenciales usadas por los propios procesos de automatización.

Ante una exposición confirmada, se debe revocar o rotar la credencial, retirar el secreto del código y evaluar la exposición del historial y de los sistemas afectados. Eliminar el valor de la última revisión no invalida una credencial ya expuesta.

Las guías de cada tecnología o plataforma definirán la integración y configuración del mecanismo seleccionado. Los escáneres no sustituyen la gestión segura, rotación y revocación de credenciales.

## 5. Dependencias y cadena de suministro

Cada proyecto debe identificar y revisar los componentes externos de los que depende, incluidos los utilizados para construir, probar y desplegar. Debe establecer mecanismos para:

- detectar vulnerabilidades conocidas;
- priorizar hallazgos según severidad, exposición e impacto;
- asignar responsables y dar seguimiento hasta la corrección o aceptación documentada;
- mantener componentes dentro de versiones soportadas;
- evaluar procedencia, integridad y licencias cuando sea pertinente.

## 6. Seguridad del código y configuración

El proyecto debe seleccionar análisis adecuados a su lenguaje, arquitectura y superficie de ataque. Según corresponda, estos deben revisar código, configuraciones, infraestructura declarativa, imágenes y artefactos. Los hallazgos deben registrarse, priorizarse y resolverse mediante un proceso definido.

Los controles no deben limitarse a encontrar problemas: el proyecto debe indicar qué hallazgos bloquean integración o despliegue, quién puede aceptar excepciones y cómo se conserva esa decisión.

## 7. Entrega y despliegue

El proceso de entrega debe identificar qué cambio se publica, dónde y bajo qué autorización. Cada proyecto debe definir:

- los ambientes y el propósito de cada uno;
- las condiciones para promover cambios entre ambientes;
- los responsables y aprobaciones requeridas, especialmente para producción;
- cómo se identifica y conserva el artefacto desplegado;
- las verificaciones previas y posteriores al despliegue;
- el procedimiento para detener, revertir o recuperar una publicación fallida.

Cuando sea viable, se debe promover el mismo artefacto validado entre ambientes. Si el artefacto se construye de nuevo, se debe mantener evidencia de que corresponde al mismo cambio aprobado y documentar las diferencias relevantes.

Los despliegues de producción deben estar protegidos frente a activaciones accidentales y restringidos a cambios autorizados. Los despliegues automatizados no deben exponer credenciales a cambios o ejecuciones no confiables.

## 8. Credenciales y permisos

- Las credenciales no deben almacenarse en el código, artefactos públicos ni registros de ejecución.
- Los secretos deben gestionarse mediante mecanismos aprobados y separarse por ambiente cuando corresponda.
- Los permisos de automatización deben limitarse a las operaciones necesarias y revisarse periódicamente.
- Las tareas de validación no deben recibir permisos de publicación si no los necesitan.
- Las ejecuciones originadas en cambios no confiables no deben acceder a secretos de ambientes protegidos.
- Las credenciales expuestas o sospechosas deben revocarse o rotarse sin demora.

## 9. Migraciones y cambios de datos

Cuando un despliegue implique cambios de datos o esquemas, el proyecto debe documentar su secuencia, permisos, validación y recuperación. Debe evaluar compatibilidad entre versiones durante el despliegue, impacto sobre datos existentes, respaldo y posibilidad de reversión. Las migraciones de riesgo no deben quedar implícitas dentro de una publicación sin revisión y evidencia.

## 10. Trazabilidad y operación

Para cada ejecución y despliegue relevante se debe poder determinar:

- el cambio, versión o artefacto involucrado;
- los controles ejecutados y sus resultados;
- el ambiente y momento de publicación;
- la autorización o responsable correspondiente;
- el resultado de las verificaciones posteriores.

Cada proyecto debe definir responsables para fallos de CI/CD y hallazgos de seguridad, retención de registros y artefactos, comprobaciones de salud posteriores al despliegue y procedimientos de recuperación. La documentación debe permitir reproducir las verificaciones importantes en el entorno de desarrollo del equipo.

## 11. Herramientas, técnicas y mecanismos de referencia

Las siguientes opciones son ejemplos para orientar la selección de controles; no constituyen una lista de productos aprobados ni una obligación de usar herramientas específicas. Un proyecto puede utilizar otros mecanismos equivalentes, combinar controles o justificar que una técnica no aplica a su arquitectura.

| Fase o propósito | Técnicas y mecanismos posibles | Ejemplos de herramientas o enfoques |
|---|---|---|
| Calidad del cambio | Formato, análisis estático, revisión por pares, comprobaciones de tipos o consistencia | Linters, analizadores estáticos, revisiones obligatorias |
| Pruebas | Pruebas unitarias, integración, contrato, extremo a extremo, rendimiento o accesibilidad según riesgo | Frameworks de prueba del stack, entornos efímeros, datos de prueba controlados |
| Secretos | Escaneo del cambio y, cuando sea necesario, del historial; prevención antes de publicar; gestión y rotación de credenciales | Gitleaks, escáneres de secretos del repositorio o del proveedor, hooks locales; almacenes de secretos |
| Dependencias | Análisis de vulnerabilidades, actualización controlada, revisión de licencias y procedencia | Herramientas de análisis de composición de software, alertas de dependencias, inventarios de componentes |
| Código y configuración seguros | Análisis estático de seguridad, revisión de configuración, modelado de amenazas según criticidad | SAST, análisis de configuración, reglas de seguridad propias del lenguaje o plataforma |
| Infraestructura y contenedores | Análisis de manifiestos, imágenes, permisos y configuración de infraestructura | Escáneres de infraestructura como código, imágenes y configuración de despliegue |
| Artefactos y cadena de suministro | Inventario de componentes, procedencia, integridad y verificación de artefactos | SBOM, hashes, firmas, atestaciones y repositorios de artefactos controlados |
| Publicación | Aprobaciones, promoción entre ambientes, despliegue gradual y control de concurrencia | Estrategias canary, blue-green o rolling, según capacidad de la plataforma |
| Verificación y recuperación | Pruebas de humo, comprobaciones de salud, observabilidad y reversión | Health checks, métricas, registros, alertas y procedimientos de rollback |

La selección debe considerar compatibilidad con el stack, cobertura real, mantenimiento, falsos positivos, integración con el flujo del equipo, costo y capacidad para atender los hallazgos. Las herramientas deben configurarse para producir resultados accionables y no generar una apariencia de control sin responsables ni proceso de remediación.

## 12. Requisitos de adaptación por proyecto

La documentación específica del proyecto debe traducir este estándar a su stack y plataforma, indicando:

1. qué controles ejecuta, cuándo y sobre qué alcance;
2. cuáles son obligatorios y cuáles bloquean integración o despliegue;
3. cómo se administran dependencias, secretos, permisos y excepciones;
4. qué ambientes existen y cómo se autorizan sus despliegues;
5. cómo se identifican, conservan y promueven los artefactos;
6. cómo se gestionan migraciones, fallos y recuperación;
7. qué registros y evidencias se conservan y por cuánto tiempo.

Las guías tecnológicas explican cómo cumplir los requisitos; no deben reducir ni contradecir los controles aprobados en este estándar. Toda excepción debe quedar documentada con su justificación, responsable, alcance y revisión prevista.

## 13. Temas para completar la política organizacional

Antes de declarar este estándar aprobado, Campusoft debe definir los criterios comunes para:

- controles obligatorios para todos los proyectos y controles adicionales según riesgo;
- reglas de integración, revisión y protección de ramas;
- ambientes estándar y autorizaciones de producción;
- prioridades y plazos de atención de vulnerabilidades;
- retención de artefactos, registros y evidencias;
- gestión de excepciones y aceptación de riesgos;
- indicadores comunes de confiabilidad, frecuencia de despliegue y recuperación.

## 14. Terminología

| Término | Descripción | Concepto |
|---|---|---|
| **CI (Continuous Integration / Integración continua)** | Práctica de integrar cambios pequeños con frecuencia y validarlos automáticamente. | Busca detectar incompatibilidades y defectos pronto, cuando corregirlos suele ser más sencillo y menos costoso. |
| **CD (Continuous Delivery / Continuous Deployment)** | Prácticas para preparar cambios validados y llevarlos a ambientes de ejecución. *Delivery* mantiene el cambio listo para publicar mediante una decisión o aprobación; *deployment* automatiza además su publicación según las reglas definidas. | Extiende la integración continua hasta la entrega o publicación controlada del software. El nivel de automatización y aprobación depende del riesgo y del ambiente. |
| **SAST (Static Application Security Testing)** | Análisis estático de seguridad de aplicaciones. Examina código o representaciones del programa sin ejecutarlo para identificar patrones que podrían causar vulnerabilidades. | Permite encontrar problemas de seguridad durante el desarrollo, antes de que el software se ejecute en un ambiente. Sus resultados requieren revisión para distinguir riesgos reales de falsos positivos. |
| **SCA (Software Composition Analysis)** | Análisis de composición de software y sus dependencias. | Identifica componentes externos y puede ayudar a detectar vulnerabilidades, licencias o versiones con riesgos conocidos en la cadena de suministro. |
| **SBOM (Software Bill of Materials / Inventario de componentes de software)** | Registro estructurado de los componentes que forman parte de un producto o artefacto. | Ofrece visibilidad sobre dependencias directas y transitivas para evaluar exposición, procedencia, licencias y alcance de vulnerabilidades. |
| **Infraestructura como código (IaC)** | Descripción declarativa o programática de recursos y configuración de infraestructura. | Permite revisar, versionar y reproducir cambios de infraestructura como parte del ciclo de software, además de analizar errores de configuración antes de aplicarlos. |
| **Artefacto** | Resultado versionado y desplegable generado a partir de una revisión del código. | Es la unidad que se prueba, conserva y promueve entre ambientes, con trazabilidad hacia el cambio que lo produjo. |
| **Despliegue gradual** | Publicación progresiva de una versión, en lugar de reemplazar todo el servicio al mismo tiempo. | Limita el alcance de una falla y permite observar el comportamiento antes de ampliar la publicación. Las estrategias canary, blue-green y rolling son variantes posibles. |
| **Prueba de humo** | Conjunto breve de comprobaciones básicas ejecutadas después de una publicación. | Confirma que las funciones esenciales responden y ayuda a decidir si el despliegue puede continuar o debe detenerse o revertirse. |
| **Rollback (reversión)** | Acción de volver a una versión o estado operativo anterior conocido. | Es un mecanismo de recuperación ante una publicación fallida; debe considerar también cambios de datos, compatibilidad y efectos que no se revierten automáticamente. |
| **Falso positivo** | Hallazgo reportado por un control que, tras revisión, no representa el riesgo señalado en ese contexto. | No debe descartarse sin análisis; su clasificación y cualquier exclusión deben quedar justificadas para evitar ocultar hallazgos reales. |
