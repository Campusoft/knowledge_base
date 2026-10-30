# Plantillas y esquemas

Contratos y formatos reutilizables para que todos los levantamientos AS-IS produzcan información comparable. Aquí se define **qué campos** debe tener el inventario y **cómo** se estructura el informe, el registro de riesgos y el plan de oleadas.

## Contenido previsto

```
plantillas/
├── README.md
├── inventario-esquema.json    # JSON Schema del inventario canónico (previsto)
├── inventario.csv             # encabezados y ejemplo de revisión (previsto)
├── informe-as-is.md           # esqueleto del informe de arquitectura (previsto)
├── registro-riesgos.md        # registro de riesgos y bloqueantes (previsto)
└── plan-oleadas.md            # plan de migración por oleadas (previsto)
```

> **Estado:** las plantillas aún no se han creado. Esta estructura define su propósito y el contrato que deberán cumplir.

## Para qué sirve cada plantilla

| Plantilla | Uso | Consumidor |
|---|---|---|
| `inventario-esquema.json` | Definir tipos, campos obligatorios, enumeraciones y versión del inventario. | Scripts, validaciones, informes. |
| `inventario.csv` | Revisar y filtrar el inventario con negocio o arquitectura. | Equipo de migración. |
| `informe-as-is.md` | Documentar contexto, contenedores, componentes, datos, despliegue, hallazgos y métricas. | Arquitectura y patrocinador. |
| `registro-riesgos.md` | Registrar riesgo, evidencia, impacto, probabilidad, mitigación y responsable. | Gestión del proyecto. |
| `plan-oleadas.md` | Ordenar aplicaciones y componentes por dependencia, riesgo y valor. | Planificación. |

## Convenciones del inventario

- **Formato canónico:** JSON, con `schemaVersion` obligatorio.
- **Claves en inglés y `camelCase`** para facilitar el consumo por scripts y herramientas; las etiquetas y los informes se escriben en español.
- **Identificadores estables:** cada sistema, solución, proyecto, dependencia y hallazgo tiene un `id` que no cambia entre ejecuciones.
- **Evidencia:** todo hallazgo incluye `source` (archivo, configuración o runtime), `confidence` y, cuando aplique, `line`.
- **Clasificación:** usar las categorías `compatible`, `adjustable`, `replaceable`, `blocking` y `unknown`.
- **Sin datos sensibles:** las plantillas y los ejemplos no contienen secretos, cadenas de conexión reales ni nombres de clientes.

## Reglas de mantenimiento

1. Todo cambio de esquema incrementa `schemaVersion` y se documenta aquí.
2. Las plantillas deben poder validarse sin depender de un sistema concreto.
3. Los ejemplos de [`../ejemplos/`](../ejemplos/README.md) se actualizan cuando cambia el esquema.
4. Los scripts no deben inventar campos fuera del esquema; si falta un dato, se registra como `null` con una observación.
5. El informe no duplica el inventario completo: resume y referencia el JSON.

## Cómo agregar una plantilla

1. Definir su propósito, consumidor y relación con el [levantamiento AS-IS](../levantamiento-as-is.md).
2. Crear el archivo con datos de ejemplo sintéticos.
3. Documentar los campos o secciones en este README.
4. Añadir una validación o prueba cuando sea posible.
5. Enlazarla desde el documento principal y desde el índice de la sección.

## Referencias

- [Levantamiento AS-IS](../levantamiento-as-is.md)
- [Scripts](../scripts/README.md)
- [Ejemplos](../ejemplos/README.md)
- [Guía de migración](../framework-a-dotnet.md)
- [JSON Schema](https://json-schema.org/)
