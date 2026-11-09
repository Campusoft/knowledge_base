# Política de arquitectura de software

| Campo | Valor |
|---|---|
| ID | CAMP-POL-ARCH-001 |
| Versión | 0.1.0 |
| Fecha de versión | 2026-11-08 |
| Estado | Borrador para revisión y aprobación |
| Propietario | Por designar |
| Autoridad aprobadora | Por designar |
| Fecha de aprobación / vigencia | Pendiente |
| Próxima revisión | Pendiente de aprobación |
| Ámbito | Sistemas y productos de software de Campusoft |

## 1. Propósito

Establecer cómo se toman, documentan y revisan decisiones de arquitectura para que los sistemas satisfagan sus requisitos funcionales, de seguridad, operabilidad y calidad durante su ciclo de vida.

## 2. Requisitos

1. **Contexto y atributos de calidad.** Cada sistema debe documentar propósito, límites, actores, integraciones, restricciones y atributos de calidad relevantes, como seguridad, disponibilidad, rendimiento, mantenibilidad y escalabilidad.
2. **Vistas y documentación.** La arquitectura debe representarse con un nivel de detalle proporcional a la complejidad y riesgo. Diagramas, descripciones y modelos deben mantenerse suficientemente actualizados para apoyar desarrollo, revisión y operación.
3. **Decisiones significativas.** Las decisiones que afecten límites de componentes, almacenamiento, integración, seguridad, disponibilidad, tecnología principal o costo operativo deben registrar contexto, opciones, decisión, consecuencias y estado, por ejemplo mediante ADR.
4. **Límites y dependencias.** Los componentes deben tener responsabilidades y dependencias comprensibles. Deben identificarse dependencias prohibidas, datos compartidos, contratos e implicaciones de cambio.
5. **Seguridad y datos.** Las decisiones deben considerar límites de confianza, autorización, clasificación y flujo de datos, secretos, exposición, amenazas y requisitos de cumplimiento aplicables.
6. **Operabilidad.** La arquitectura debe considerar despliegue, configuración, observabilidad, recuperación, actualización, migraciones y soporte durante la operación.
7. **Evaluación de cambios.** Cambios que alteren decisiones importantes o atributos de calidad deben evaluarse antes de implementarse y actualizar la documentación afectada.
8. **Revisión proporcional.** Los sistemas críticos o con impacto financiero, regulatorio, privacidad o alta exposición deben someter cambios arquitectónicos de impacto a revisión de las partes interesadas correspondientes. Criterios y autoridad de revisión quedan pendientes de aprobación.
9. **Excepciones.** Las desviaciones respecto de principios o estándares arquitectónicos deben documentar necesidad, riesgos, mitigaciones, dueño y fecha de revisión conforme a la política marco.

## 3. Gobierno de decisiones

La revisión arquitectónica debe ayudar a resolver riesgos y dependencias, no sustituir la responsabilidad del equipo que construye y opera el sistema. La autoridad, composición del foro, umbrales de revisión y proceso de aprobación deben definirse por Campusoft antes de declarar esta política vigente.

## 4. Estándares y guías relacionadas

- [Arquitectura](../../architecture/README.md): conceptos, atributos de calidad y prácticas arquitectónicas en la base de conocimiento.
- [Seguridad aplicada](../../architecture/seguridad/README.md): material sobre seguridad de sistemas y aplicaciones.
- [Pruebas de arquitectura con ArchUnitNET](../../dotnet/net/calidad-y-pruebas/archunitnet.md): mecanismo para codificar reglas arquitectónicas en soluciones .NET.
- [Política marco de ingeniería de software](POLITICA-MARCO-INGENIERIA-SOFTWARE.md): gobierno y gestión de excepciones.

## 5. Historial de cambios

| Versión | Fecha | Cambio |
|---|---|---|
| 0.1.0 | 2026-11-08 | Creación del borrador inicial. |
