# Política de seguridad de software

| Campo | Valor |
|---|---|
| ID | CAMP-POL-SEC-001 |
| Versión | 0.1.0 |
| Fecha de versión | 2026-11-08 |
| Estado | Borrador para revisión y aprobación |
| Propietario | Por designar |
| Autoridad aprobadora | Por designar |
| Fecha de aprobación / vigencia | Pendiente |
| Próxima revisión | Pendiente de aprobación |
| Ámbito | Software creado, adquirido o mantenido por Campusoft |

## 1. Propósito

Establecer requisitos mínimos para identificar y reducir riesgos de seguridad durante el diseño, desarrollo, entrega y mantenimiento de software.

## 2. Requisitos

1. **Riesgo y diseño.** Los sistemas deben identificar activos, límites de confianza, datos sensibles, superficies de ataque y escenarios de abuso de acuerdo con su criticidad. Los riesgos importantes deben tener una decisión y responsable documentados.
2. **Desarrollo seguro.** Los proyectos deben aplicar prácticas de codificación segura, revisión de cambios y controles automáticos apropiados al lenguaje y contexto.
3. **Análisis del código (SAST).** Debe definirse dónde y con qué criterios se analiza el código propio. Los hallazgos deben validarse, priorizarse, asignarse y seguirse hasta remediación o excepción aprobada.
4. **Dependencias (SCA).** Los proyectos deben conocer las dependencias directas y transitivas, detectar vulnerabilidades conocidas, evaluar actualizaciones y gestionar excepciones. Los componentes críticos deben mantenerse en versiones soportadas cuando sea posible.
5. **Secretos.** No se deben almacenar credenciales activas en código, configuración pública, artefactos o logs. Los secretos deben gestionarse con mecanismos aprobados, permisos mínimos y rotación; una exposición confirmada requiere revocación/rotación y análisis de impacto.
6. **Configuración e infraestructura.** Las configuraciones, permisos e infraestructura declarativa que afecten seguridad deben revisarse y validarse antes del despliegue según riesgo.
7. **Pruebas de seguridad.** Los sistemas expuestos o críticos deben contar con verificaciones de seguridad apropiadas al riesgo, que pueden incluir pruebas dinámicas, pruebas de autorización, análisis de API o evaluaciones especializadas.
8. **Gestión de hallazgos.** Cada hallazgo debe registrarse con severidad, contexto, responsable, fecha objetivo y estado. Deben definirse plazos de respuesta por severidad y criterios de escalamiento; esos valores están pendientes de aprobación organizacional.
9. **Excepciones y falsos positivos.** Deben documentarse con evidencia, alcance acotado, mitigaciones, responsable y fecha de revisión. Una exclusión no debe ocultar otras instancias potencialmente vulnerables.
10. **Cadena de suministro.** Para sistemas cuyo riesgo lo requiera, se debe mantener inventario de componentes y evidencia de integridad/procedencia de artefactos. Los controles específicos se establecerán en estándares asociados.

## 3. Responsabilidades

Los responsables de producto/sistema aseguran la evaluación de riesgos y priorización; los equipos de desarrollo corrigen hallazgos y mantienen dependencias; seguridad asesora sobre impacto y excepciones; operaciones/plataforma protegen credenciales, agentes y entornos. La asignación formal de roles queda pendiente de definición organizacional.

## 4. Estándares y guías relacionadas

- [DevSecOps](../../Operation/DevSecOps.md): ciclo de controles y principios de seguridad integrada.
- [SAST](../../architecture/seguridad/seguridad-aplicativa/sast.md): conceptos y herramientas de análisis estático de seguridad.
- [Semgrep para .NET y C#](../../dotnet/net/calidad-y-pruebas/semgrep.md): guía específica SAST.
- [SCA de dependencias .NET](../../dotnet/net/calidad-y-pruebas/sca-dependencias.md): NuGet Audit y OWASP Dependency-Check.
- [Gitleaks](../../git/gitleaks.md): escaneo de secretos en repositorios.
- [Estándares CI/CD](../estandares/ci-cd/ESTANDARES-CI-CD.md): controles de seguridad en integración y despliegue.

## 5. Historial de cambios

| Versión | Fecha | Cambio |
|---|---|---|
| 0.1.0 | 2026-11-08 | Creación del borrador inicial. |
