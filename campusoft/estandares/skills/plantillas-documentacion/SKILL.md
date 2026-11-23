---
name: plantillas-documentacion
description: Crea documentos de proyecto de Campusoft (ADR, SRS, PLAN, POC, QA/bug, TAR) a partir de las plantillas corporativas y los ubica en la carpeta correcta de doc/. Úsalo cuando se pida redactar una decisión de arquitectura, un requerimiento, un plan de implementación, una prueba de concepto, un reporte de defecto o una tarea para agente.
---

# Plantillas de documentación Campusoft

> **Copia derivada. No editar aquí.** Fuente de verdad: `campusoft/estandares/documentacion-proyectos/` (paquete `CAMP-DOC-STRUCT-001` v1.0.0, borrador para revisión y aprobación). Los cambios se proponen en la fuente y esta copia se actualiza desde ella.

## Cuándo usar

Cuando el usuario pida crear uno de los documentos de la tabla siguiente.

## Tipos y destino

| Tipo | Plantilla (`assets/`) | Usar para | Destino en `doc/` |
|---|---|---|---|
| ADR | `ADR_plantilla.decisiones.arquitectura.v.1.0.md` | Decisión de arquitectura, alternativas e impactos | `arquitectura/decisiones/` |
| SRS | `SRS_plantilla.requerimientos.v.1.0.md` | Un requisito funcional o no funcional con criterios de aceptación | `requerimientos/01-requisitos/` |
| PLAN | `PLAN_plantilla.implementacion.v.1.0.md` | Implementación de cambios aprobados o autorizados | `requerimientos/03-planes-implementacion/` |
| POC | `POC_plantilla.pruebas.concepto.v.1.0.md` | Hipótesis, criterios de éxito y ejecución de una prueba de concepto | No definido en el estándar; preguntar |
| QA | `QA_plantilla.bug.v.1.0.md` | Defecto con pasos de reproducción, entorno y severidad | No definido en el estándar; preguntar (el estándar indica enlazar defectos desde `calidad/ejecuciones/` si el proyecto tiene otro registro oficial) |
| TAR | `TAR_plantilla.tarea.agente.v.1.0.md` | Encargo a un agente; uso opcional | `doc/tareas-agentes/TAR-<NNN>-descripcion.md`, según la propia plantilla (esa carpeta no está en la estructura base) |

## Pasos

1. Identifica el tipo. Si es ambiguo, pregunta.
2. Lee la plantilla de `assets/` y úsala tal cual: mismos encabezados y orden. No la reescribas de memoria.
3. Pide solo los datos que falten. No inventes contenido, fechas ni responsables; deja el marcador `[...]` en lo desconocido.
4. Si el proyecto tiene `doc/plantillas/`, usa la versión registrada allí. Si no, usa la de este skill y menciónalo.
5. Guarda el archivo en el destino de la tabla. Si no existe la carpeta, créala solo si el estándar la define; en otro caso pregunta.
6. Usa identificadores estables: `REQ`, `ANA`, `PLAN`, `EST` para la etapa de requerimientos; `ADR-<n>` y `TAR-<NNN>` según la plantilla. Revisa el siguiente número libre en la carpeta.
7. Enlaza los documentos relacionados (requisito ↔ análisis ↔ plan) para conservar trazabilidad.
8. Entrega el archivo e indica qué secciones quedaron pendientes.

## Reglas

- Estado inicial: el primero del ciclo de la plantilla (por ejemplo "Propuesto" o "Pendiente"). Nunca "Aprobado".
- Un documento nuevo no convierte propuestas o insumos en requisitos aprobados.
- No incluir credenciales, secretos ni datos sensibles (especialmente en QA/bug).
- Mantener una sola fuente vigente por tema; enlazar en lugar de duplicar.
- Las plantillas tienen emojis y marcadores propios; conservarlos.

## Referencias

- `campusoft/estandares/documentacion-proyectos/ESTANDAR-ESTRUCTURA-DOCUMENTACION.md`
- `campusoft/estandares/documentacion-proyectos/plantillas/v1.0.0/documentos/README.md`
