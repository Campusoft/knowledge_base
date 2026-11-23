# Estructura de documentación de proyectos Campusoft

| Campo | Valor |
|---|---|
| Identificador | CAMP-DOC-STRUCT-001 |
| Versión | 1.0.0 |
| Fecha de versión | 2026-11-14 |
| Estado | Borrador para revisión y aprobación |
| Alcance | Estructura inicial de documentación para proyectos de software Campusoft |

## Propósito

Definir una estructura común, versionada y reutilizable para organizar la documentación de proyectos. La estructura documentada contiene diez áreas: `producto`, `requerimientos`, `arquitectura`, `desarrollo`, `plantillas`, `seguridad`, `calidad`, `operacion`, `capacitacion` y `guias`.

La estructura se basa en patrones observados en proyectos Campusoft. Es un punto de partida adaptable; no exige crear subcarpetas sin contenido ni reorganizar proyectos existentes automáticamente.

## Paquete v1.0.0

- [Estándar de estructura](ESTANDAR-ESTRUCTURA-DOCUMENTACION.md): alcance, definiciones, reglas y criterios de uso.
- [Plantilla v1.0.0](plantillas/v1.0.0/README.md): paquete listo para copiar a un repositorio de proyecto.
- [Plantillas documentales](plantillas/v1.0.0/documentos/README.md): formatos reutilizables mantenidos juntos en una sola carpeta.
- [Control de estructura](plantillas/v1.0.0/control/README.md): manifiesto, validador, workflow y CODEOWNERS para detectar carpetas no declaradas en `doc/`.

## Historial

| Versión | Fecha | Cambio |
|---|---|---|
| 1.0.0 | 2026-11-14 | Primera versión propuesta con nueve áreas documentales y archivo de históricos opcional por área. |

## Registro de cambios posteriores

| Fecha | Cambio |
|---|---|
| 2026-11-17 | Se agregó el área `calidad/` para organizar escenarios, datos y ejecuciones de pruebas funcionales manuales, automatizadas o realizadas por agentes IA. Se actualizaron los documentos de referencia existentes, sin publicar una nueva versión de la plantilla. |
| 2026-11-23 | Se agregó el control de cumplimiento de la estructura: manifiesto `doc/.estructura.txt`, validador, workflow y CODEOWNERS en `plantillas/v1.0.0/control/`. Sin publicar una nueva versión de la plantilla. |
