# Política marco de ingeniería de software

| Campo | Valor |
|---|---|
| ID | CAMP-POL-ENG-001 |
| Versión | 0.1.0 |
| Fecha de versión | 2026-11-08 |
| Estado | Borrador para revisión y aprobación |
| Propietario | Por designar |
| Autoridad aprobadora | Por designar |
| Fecha de aprobación / vigencia | Pendiente |
| Próxima revisión | Pendiente de aprobación |
| Ámbito | Proyectos de software de Campusoft |

## 1. Propósito

Establecer el marco común para crear, aprobar, comunicar, aplicar y revisar las políticas de ingeniería de software de Campusoft. Las políticas buscan que los proyectos gestionen de forma consistente la entrega, la seguridad y la arquitectura, con requisitos proporcionales a su criticidad y riesgo.

## 2. Alcance

Aplica a los productos, servicios, aplicaciones, APIs, componentes y repositorios desarrollados, adquiridos o mantenidos por Campusoft, así como a equipos internos y proveedores que contribuyan a ellos. Los responsables deben determinar y documentar la aplicabilidad cuando una disposición no corresponda a un sistema concreto.

## 3. Principios

1. Los requisitos obligatorios deben ser verificables y tener responsable.
2. La criticidad, exposición, datos y consecuencias de fallo determinan la profundidad de controles.
3. Los requisitos se mantienen separados de las guías de herramientas y de sus implementaciones.
4. Los controles repetibles deben automatizarse cuando sea razonable; las decisiones de riesgo requieren responsabilidad humana.
5. Las excepciones son explícitas, acotadas, aprobadas y revisables.
6. Las decisiones y evidencias deben permitir explicar qué requisito aplicó, cómo se cumplió y quién aceptó los riesgos residuales.

## 4. Jerarquía documental

- **Política:** requisito organizacional, alcance, responsabilidades y reglas de cumplimiento.
- **Estándar:** criterios técnicos o de proceso verificables que concretan una o varias políticas.
- **Guía:** orientación de aplicación, ejemplos y herramientas para un stack o plataforma.
- **Procedimiento o registro del proyecto:** evidencia de cómo un sistema cumple los requisitos, con decisiones y excepciones específicas.

Las guías pueden cambiar al evolucionar herramientas, pero no pueden eliminar ni rebajar un requisito aprobado. Si una guía y una política se contradicen, se aplica la política y se corrige la guía.

## 5. Responsabilidades

- **Propietario de política:** mantiene contenido, propone cambios y revisa vigencia.
- **Autoridad aprobadora:** acepta requisitos, alcance, excepciones organizacionales y entrada en vigor.
- **Responsable del proyecto/sistema:** determina aplicabilidad, documenta implementación y mantiene evidencia.
- **Equipos de ingeniería, arquitectura y seguridad:** asesoran, revisan controles y ayudan a resolver desviaciones.
- **Responsable de excepción:** implementa mitigaciones y cumple la fecha de cierre o reevaluación aprobada.

Los cargos y nombres organizacionales concretos quedan pendientes de asignación por Campusoft.

## 6. Creación y ciclo de vida

Cada política debe incluir ID estable, versión, fecha de versión, estado, propietario, aprobador, alcance, requisitos, excepciones, referencias y registro de cambios. Debe someterse a revisión técnica y de los responsables afectados antes de aprobación.

La aprobación debe registrar autoridad, fecha y vigencia. El propietario debe programar revisiones periódicas y también revisarla ante cambios importantes de riesgo, arquitectura, regulación o modelo operativo. Los documentos retirados deben marcarse como obsoletos y conservar el historial según la política de retención aplicable.

## 7. Excepciones y cumplimiento

Una excepción debe identificar el requisito, sistema y alcance; explicar por qué no puede cumplirse; evaluar impacto y riesgo; definir controles compensatorios; asignar aprobador y responsable; e indicar expiración o fecha de revisión. No se deben conceder excepciones indefinidas ni convertir una excepción de un proyecto en una exención general sin modificar la política.

El responsable del sistema debe conservar evidencia suficiente de cumplimiento y comunicar incumplimientos o riesgos residuales mediante el proceso organizacional definido. El mecanismo de auditoría, escalamiento, plazos y sanciones requiere definición organizacional y no queda establecido por este borrador.

## 8. Políticas de dominio

- [Política CI/CD](POLITICA-CI-CD.md)
- [Política de seguridad de software](POLITICA-SEGURIDAD-SOFTWARE.md)
- [Política de arquitectura de software](POLITICA-ARQUITECTURA-SOFTWARE.md)

## 9. Historial de cambios

| Versión | Fecha | Cambio |
|---|---|---|
| 0.1.0 | 2026-11-08 | Creación del borrador inicial. |
