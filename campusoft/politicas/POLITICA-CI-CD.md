# Política de integración, entrega y despliegue continuo

| Campo | Valor |
|---|---|
| ID | CAMP-POL-CICD-001 |
| Versión | 0.1.0 |
| Fecha de versión | 2026-11-08 |
| Estado | Borrador para revisión y aprobación |
| Propietario | Por designar |
| Autoridad aprobadora | Por designar |
| Fecha de aprobación / vigencia | Pendiente |
| Próxima revisión | Pendiente de aprobación |
| Ámbito | Repositorios y servicios de software Campusoft |

## 1. Propósito

Establecer requisitos mínimos para validar cambios, producir artefactos trazables y desplegar software de manera controlada, segura y recuperable.

## 2. Requisitos

1. **Validación previa a integración.** Todo cambio debe pasar por revisión y comprobaciones automatizadas definidas para el proyecto antes de integrarse en ramas protegidas. Los controles obligatorios y sus criterios de bloqueo deben documentarse.
2. **Calidad y pruebas.** Cada proyecto debe definir validaciones relevantes a su stack, que pueden incluir formato, análisis estático, compilación o verificación equivalente y pruebas automatizadas adecuadas al riesgo.
3. **Seguridad.** El flujo debe proteger secretos y credenciales, revisar dependencias y aplicar análisis de código/configuración de acuerdo con la política de seguridad. Los jobs no confiables no deben recibir credenciales de despliegue.
4. **Reproducibilidad.** Las dependencias, herramientas y condiciones de build deben controlarse para obtener resultados repetibles. Los proyectos deben documentar versiones y fuentes críticas de configuración.
5. **Artefactos.** Los artefactos entregables deben identificarse con su cambio o versión, almacenarse con permisos adecuados y asociarse a resultados de validación. Cuando sea viable, el mismo artefacto validado debe promoverse entre ambientes.
6. **Despliegues protegidos.** Los ambientes y aprobaciones deben corresponder al riesgo. Producción debe estar protegida frente a publicación accidental y restringida a cambios autorizados.
7. **Credenciales y permisos.** Los pipelines deben utilizar mínimo privilegio, credenciales gestionadas de forma segura y separación entre tareas de validación y publicación.
8. **Trazabilidad.** Debe poder determinarse qué cambio y artefacto fueron validados, quién autorizó el despliegue, a qué ambiente se publicó y cuál fue el resultado.
9. **Verificación y recuperación.** El proyecto debe definir comprobaciones posteriores al despliegue y cómo detener, revertir o recuperar una publicación fallida. Los cambios de datos deben considerar compatibilidad y recuperación.
10. **Excepciones.** Toda excepción debe registrarse según la [política marco](POLITICA-MARCO-INGENIERIA-SOFTWARE.md), con riesgo, mitigación, responsable, aprobador y revisión/expiración.

## 3. Aplicación proporcional al riesgo

Todo proyecto debe satisfacer los requisitos aplicables. Los proyectos críticos, expuestos externamente, financieros o que procesen datos sensibles deben documentar una evaluación de riesgo que determine controles reforzados, separación de ambientes, aprobaciones, evidencias y requisitos de recuperación. La clasificación y los umbrales organizacionales están pendientes de aprobación.

## 4. Estándares y guías

- [Estándares CI/CD](../estandares/ci-cd/ESTANDARES-CI-CD.md): criterios transversales de integración, entrega y despliegue. El documento enlazado permanece en estado de propuesta hasta su aprobación.
- [Guía de aplicación CI/CD](../guias/ci-cd/README.md): referencias para aplicar requisitos por tecnología y plataforma.
- [DevOps](../../Operation/DevOps.md) y [DevSecOps](../../Operation/DevSecOps.md): conceptos y prácticas generales en la base de conocimiento.
- [Azure Pipelines](../../microsoft/azure-devops/azure-pipelines.md): implementación específica en Azure Pipelines.

## 5. Evidencias mínimas esperadas

El proyecto debe poder presentar configuración versionada del pipeline, revisiones y resultados de los controles, referencia al artefacto publicado, autorizaciones requeridas, ambiente de destino y resultado de la verificación posterior. La retención y formato de evidencia quedan sujetos a definición organizacional.

## 6. Historial de cambios

| Versión | Fecha | Cambio |
|---|---|---|
| 0.1.0 | 2026-11-08 | Creación del borrador inicial. |
