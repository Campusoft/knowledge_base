# Enfoque arc42

arc42 es una plantilla práctica para documentar y comunicar la arquitectura de un sistema. Organiza la información en secciones independientes que pueden completarse según las necesidades del proyecto. No prescribe un estilo arquitectónico ni obliga a producir todos los diagramas.

## Cuándo usarlo

- Cuando se necesita una estructura común para explicar el contexto, las decisiones y las vistas de un sistema.
- Cuando la documentación debe evolucionar junto con el software.
- Cuando participan audiencias distintas —negocio, desarrollo, seguridad y operación— y cada una necesita encontrar información relevante.

Para iniciativas pequeñas se pueden omitir secciones sin contenido útil. Para sistemas críticos conviene documentar explícitamente requisitos de calidad, riesgos, decisiones, despliegue y operación.

## Secciones habituales

1. **Introducción y objetivos:** propósito, alcance, actores y objetivos de calidad prioritarios.
2. **Restricciones:** restricciones técnicas, organizativas, regulatorias y de negocio.
3. **Contexto y alcance:** usuarios, sistemas externos, entradas y salidas.
4. **Estrategia de solución:** principios y decisiones que dan forma a la solución.
5. **Vista de bloques de construcción:** componentes, responsabilidades, interfaces y relaciones.
6. **Vista de ejecución:** escenarios y flujos de interacción importantes.
7. **Vista de despliegue:** ambientes, nodos, redes y asignación de componentes.
8. **Conceptos transversales:** seguridad, persistencia, observabilidad, errores y otros mecanismos comunes.
9. **Decisiones arquitectónicas:** decisiones relevantes y sus consecuencias; registrar cada una como ADR si se requiere seguimiento independiente.
10. **Requisitos de calidad:** escenarios medibles y cómo se validan.
11. **Riesgos y deuda técnica:** incertidumbres, riesgos y asuntos pendientes.
12. **Glosario:** términos del negocio y técnicos.

Los nombres y el detalle pueden variar según la versión de arc42 y las necesidades del equipo. Usar la [plantilla de arquitectura de software](../plantillas/arquitectura-software.md) como punto de partida local, adaptando sus apartados a estas vistas.

## Diagramas

Se puede complementar el texto con C4 para contexto, contenedores y componentes, y con diagramas de secuencia, despliegue o datos cuando respondan preguntas concretas. Evitar diagramas que repitan información o que no tengan responsable de actualización.

## Mantenimiento

- Guardar la documentación cerca del código o en un repositorio conocido por los equipos responsables.
- Identificar propietario, estado, fecha de revisión y fuentes de información.
- Actualizarla como parte de los cambios arquitectónicos y revisar enlaces y diagramas periódicamente.
- Mantener las decisiones en ADR enlazados desde la sección de decisiones.

## Referencias

- [Documentación oficial de arc42](https://arc42.org/documentation/)
- [Resumen de las secciones](https://arc42.org/overview/)
- [Modelo C4: diagramas](https://c4model.com/diagrams)
