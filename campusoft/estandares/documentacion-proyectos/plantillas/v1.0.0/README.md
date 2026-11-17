# Plantilla de documentación de proyectos Campusoft v1.0.0

| Campo | Valor |
|---|---|
| Identificador | CAMP-DOC-STRUCT-001 |
| Versión de plantilla | 1.0.0 |
| Fecha | 2026-11-14 |
| Estado | Borrador para revisión y aprobación |

## Uso

Copiar el contenido de `estructura/doc/` al repositorio del proyecto. Completar los índices y eliminar las áreas que no apliquen. Las plantillas documentales reutilizables están reunidas en [`documentos/`](documentos/README.md); copiarlas juntas a `doc/plantillas/` si el proyecto las utiliza. Registrar la versión de estructura y de formatos utilizada en `doc/README.md`.

Esta plantilla define las áreas `producto`, `requerimientos`, `arquitectura`, `desarrollo`, `plantillas`, `seguridad`, `operacion`, `capacitacion` y `guias`. La estructura corporativa completa está descrita en el [estándar](../../ESTANDAR-ESTRUCTURA-DOCUMENTACION.md).

## Árbol incluido

```text
doc/
├── README.md
├── producto/
│   └── README.md
├── requerimientos/
│   ├── README.md
│   ├── 00-propuestas/README.md
│   ├── 01-requisitos/README.md
│   ├── 02-analisis/README.md
│   ├── 03-planes-implementacion/README.md
│   ├── 04-estado-sistema/README.md
│   └── 90-insumos/README.md
├── arquitectura/
│   ├── README.md
│   ├── vistas/README.md
│   └── decisiones/README.md
├── desarrollo/
│   ├── README.md
│   ├── build/README.md
│   └── convenciones/README.md
├── plantillas/
│   ├── README.md
│   ├── arquitectura/README.md
│   └── requerimientos/README.md
├── seguridad/
│   ├── README.md
│   ├── arquitectura/README.md
│   └── controles/README.md
├── operacion/
│   ├── README.md
│   ├── infraestructura/README.md
│   ├── instalacion/README.md
│   │   └── guias/README.md
│   ├── despliegue/README.md
│   ├── soporte/README.md
│   └── observabilidad/README.md
├── capacitacion/
│   └── README.md
└── guias/
    └── README.md
```

La plantilla incluye índices para orientar el uso de las carpetas. Las áreas no aplicables pueden eliminarse. No incluye carpetas `historicos/` fijas: pueden crearse dentro de cualquier área cuando sea necesario conservar allí documentos reemplazados.
