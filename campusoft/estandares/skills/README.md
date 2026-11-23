# Skills Campusoft

Esta carpeta contiene los skills de Campusoft: paquetes de instrucciones reutilizables (carpeta con `SKILL.md`) que guían a un agente para aplicar los estándares en tareas concretas.

## Regla de fuente de verdad

Los skills son implementaciones derivadas. La fuente de verdad son las políticas, los estándares y las guías de `campusoft/`.

- No editar contenido copiado dentro de un skill. Proponer el cambio en la fuente y actualizar el skill desde ella.
- Cada `SKILL.md` indica su fuente, la versión y el estado de aprobación. Un skill no puede presentarse como vigente si su fuente es un borrador.
- Los skills resumen lo operativo y enlazan al documento fuente. Solo se copian los archivos que el skill necesita para funcionar de forma autónoma (por ejemplo, las plantillas).

## Catálogo

| Skill | Fuente | Versión de origen | Estado |
|---|---|---|---|
| [plantillas-documentacion](plantillas-documentacion/SKILL.md) | [Plantillas documentales v1.0.0](../documentacion-proyectos/plantillas/v1.0.0/documentos/README.md) | CAMP-DOC-STRUCT-001 v1.0.0 | Propuesta |
| [estructura-documentacion](estructura-documentacion/SKILL.md) | [Estándar de estructura de documentación](../documentacion-proyectos/ESTANDAR-ESTRUCTURA-DOCUMENTACION.md) | CAMP-DOC-STRUCT-001 v1.0.0 | Propuesta |

## Estructura de un skill

```text
<nombre-del-skill>/
├── SKILL.md          # frontmatter (name, description) e instrucciones
├── references/       # opcional: detalle que se carga bajo demanda
└── assets/           # opcional: plantillas y archivos que el skill entrega
```
