# Migraciones .NET

Material para evaluar y ejecutar migraciones de aplicaciones .NET Framework 4.x hacia versiones modernas de .NET (6, 8 o 10). La sección combina una guía metodológica, el método de levantamiento del estado actual y las herramientas para obtener evidencia.

## Guías

- [Migrar de .NET Framework a .NET moderno](framework-a-dotnet.md): selección de versión destino, estrategias, metodología por fases, riesgos, pruebas, herramientas y listas de verificación.
- [Levantamiento AS-IS](levantamiento-as-is.md): cómo inventariar soluciones, proyectos, dependencias, formatos, despliegue y riesgos del sistema actual antes de migrar.

## Carpetas

| Carpeta | Contenido |
|---|---|
| [anexos/](anexos/README.md) | Guías detalladas: conversión a SDK-style, .NET Standard 2.0/multi-targeting y herramientas de validación. |
| [scripts/](scripts/README.md) | Scripts de PowerShell para inventariar soluciones, proyectos, paquetes, dependencias, APIs y configuración. |
| [plantillas/](plantillas/README.md) | Esquema del inventario, plantilla del informe AS-IS, registro de riesgos y plan de oleadas. |
| [ejemplos/](ejemplos/README.md) | Solución sintética, salidas de ejemplo e informes de referencia para pruebas y documentación. |

## Estructura de la sección

```
dotnet/
├── README.md                  # este índice
├── framework-a-dotnet.md      # guía de migración: fases, riesgos y validación
├── levantamiento-as-is.md     # método de inventario y arquitectura actual
├── .gitignore                 # excluye output/ con la evidencia cruda
├── anexos/                    # guías detalladas de ejecución
│   ├── README.md
│   ├── conversion-a-sdk-style.md
│   ├── netstandard2.0-y-multitargeting.md
│   └── herramientas-y-validacion.md
├── scripts/                   # herramientas PowerShell
│   ├── README.md
│   ├── lib/                   # módulo y funciones comunes
│   └── tests/                 # pruebas Pester
├── plantillas/                # esquemas y formatos reutilizables
│   └── README.md
└── ejemplos/                  # datos sintéticos y salidas de referencia
    └── README.md
```

> **Estado:** la estructura está definida y documentada. Los scripts, plantillas y ejemplos se incorporarán de forma incremental; cada carpeta indica qué archivos contendrá y con qué reglas.

## Orden de lectura recomendado

1. [Levantamiento AS-IS](levantamiento-as-is.md) para obtener y organizar la evidencia.
2. [Guía de migración](framework-a-dotnet.md) para elegir estrategia, fases, riesgos y validaciones.
3. [Anexos](anexos/README.md) para el detalle de conversión, .NET Standard y validación.
4. [Plantillas](plantillas/README.md) para dar formato homogéneo a los entregables.
5. [Scripts](scripts/README.md) para automatizar el inventario.
6. [Ejemplos](ejemplos/README.md) para contrastar la salida esperada.

## Relación con otras secciones

- Los resúmenes sanitizados por sistema se registran en [Casos de migración](../../../casos/README.md).
- Las decisiones de arquitectura se documentan como ADR en [Decisiones y documentación](../../../../decisiones-y-documentacion/README.md).
- Las vistas del AS-IS y del TO-BE siguen la [guía central de arquitectura](../../../../architecture.md).
