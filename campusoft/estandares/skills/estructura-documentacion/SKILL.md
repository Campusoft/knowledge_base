---
name: estructura-documentacion
description: Crea o adapta la estructura de carpetas doc/ de un proyecto de software de Campusoft (producto, requerimientos, arquitectura, desarrollo, plantillas, seguridad, calidad, operación, capacitación, guías) y decide dónde va cada documento. Úsalo al iniciar la documentación de un proyecto, al reorganizar doc/ o al preguntar en qué carpeta colocar un documento.
---

# Estructura de documentación de proyectos Campusoft

> **Copia derivada. No editar aquí.** Fuente de verdad: `campusoft/estandares/documentacion-proyectos/` (`CAMP-DOC-STRUCT-001` v1.0.0, borrador para revisión y aprobación). Los cambios se proponen en la fuente y esta copia se actualiza desde ella.

## Cuándo usar

- Proyecto nuevo que necesita `doc/`.
- Proyecto existente que quiere alinearse a la estructura.
- Duda sobre en qué área va un documento.

## Áreas

| Área | Contenido |
|---|---|
| `producto/` | Propósito, objetivos, alcance, actores, procesos, restricciones, glosario |
| `requerimientos/` | Por etapa: `00-propuestas` (opcional), `01-requisitos`, `02-analisis`, `03-planes-implementacion`, `04-estado-sistema`, `90-insumos` |
| `arquitectura/` | `vistas/` (diagramas, componentes, flujos) y `decisiones/` (ADR) |
| `desarrollo/` | `build/` y `convenciones/` |
| `plantillas/` | Formatos reutilizables del proyecto; registrar la versión usada |
| `seguridad/` | `arquitectura/` y `controles/`; sin secretos |
| `calidad/` | `escenarios/`, `datos/`, `ejecuciones/` |
| `operacion/` | `infraestructura/`, `instalacion/` (con `guias/`), `despliegue/`, `soporte/`, `observabilidad/` |
| `capacitacion/` | Material de formación, con público, objetivo y versión del sistema |
| `guias/` | Guías prácticas que no encajan en otra área |

Distinción clave en operación: infraestructura = dónde funciona; instalación = cómo dejarlo preparado; despliegue = cómo publicar cambios.

## Pasos: proyecto nuevo

1. Pregunta qué áreas aplican. Las que no apliquen se omiten; no se crean carpetas vacías sin contenido.
2. Copia desde `assets/doc/` solo las áreas elegidas, con sus `README.md` (índices), al `doc/` del proyecto. Copia también `assets/doc/.estructura.txt` (manifiesto, archivo oculto) y borra de él las áreas no elegidas.
3. Completa `doc/README.md` como índice y enlaza los índices de las áreas usadas.
4. Si se adoptan las plantillas de documentos, copiarlas a `doc/plantillas/` y registrar la versión (usar el skill `plantillas-documentacion`).
5. Instala el control: copia el contenido de `assets/control/` a la raíz del repositorio (`scripts/`, `.github/`) y reemplaza el responsable en `.github/CODEOWNERS`. Ver `assets/control/README.md`.
6. Ejecuta `./scripts/validar-estructura.ps1`; debe terminar sin errores.
7. Registra en el índice del proyecto cualquier adaptación a la estructura.

## Pasos: proyecto existente

1. Evalúa el beneficio en búsqueda, trazabilidad y mantenimiento. No muevas documentos solo para ajustarse a la plantilla.
2. Si se mueve algo, propón el listado de cambios y actualiza los enlaces afectados. Pide confirmación antes de mover.
3. Si el proyecto adopta el control, crea `doc/.estructura.txt` con las carpetas que ya existen, instala `assets/control/` y valida con `./scripts/validar-estructura.ps1` antes de subir los cambios.

## Control de la estructura

- No crees carpetas en `doc/` que no estén en `doc/.estructura.txt`. Ejecuta `./scripts/validar-estructura.ps1` antes y después de crear o mover documentos.
- Si una carpeta nueva es necesaria: declárala en el manifiesto, regístrala como adaptación en `doc/README.md` y propón el cambio en un PR. No lo apliques sin confirmación del usuario.
- El validador comprueba: `doc/README.md` y manifiesto presentes, carpetas declaradas, `README.md` en cada área y ausencia de archivos sueltos en `doc/`.
- Con GitHub Free y repositorios privados el workflow **detecta, pero no bloquea**: marca el PR en rojo sin impedir la fusión. No presentes el control como bloqueante.
- No modifiques el validador ni el workflow en un proyecto para hacer pasar una validación. Si falla, corrige la estructura o declara la adaptación.
## Reglas

- Carpetas en minúsculas y con guiones; identificadores estables en nombres de documentos.
- Organizar requerimientos por etapa primero y por módulo o plataforma después, solo si aporta valor.
- Separar necesidades, análisis, planes y estado implementado. Existir un documento no implica aprobación ni implementación.
- Una fuente vigente por tema; enlazar en lugar de duplicar.
- `historicos/` solo dentro del área que lo necesite; no es parte fija de la estructura.
- Requisitos de seguridad también se registran en `requerimientos/01-requisitos/` y se enlazan desde `seguridad/`.
- No incluir secretos, credenciales ni datos sensibles innecesarios.

## Referencias

- `campusoft/estandares/documentacion-proyectos/ESTANDAR-ESTRUCTURA-DOCUMENTACION.md` (sección 6, Control de cumplimiento)
- `campusoft/estandares/documentacion-proyectos/plantillas/v1.0.0/README.md`
