# Casos de migración

Registro de levantamientos y migraciones realizados sobre sistemas concretos. Cada caso conserva el **resumen sanitizado** del AS-IS, los riesgos, las decisiones y el estado de la migración; la evidencia cruda permanece en el repositorio o almacenamiento del proyecto.

## Estructura

```
casos/
├── README.md
└── <sistema>/
    ├── README.md              # alcance, propietario, fechas, estado
    ├── as-is.md               # arquitectura y hallazgos del estado actual
    ├── inventario-resumen.md  # métricas y componentes principales
    ├── riesgos.md             # riesgos, bloqueantes y mitigaciones
    └── decisiones/            # ADR de la migración
```

| Elemento | Contenido |
|---|---|
| `README.md` del caso | Nombre, propósito, responsables, fechas, versión analizada, estado y enlaces a la evidencia. |
| `as-is.md` | Contexto, contenedores, componentes, datos, despliegue, seguridad y operación del sistema actual. |
| `inventario-resumen.md` | Cifras y hallazgos relevantes; no replica el inventario completo. |
| `riesgos.md` | Riesgos, impacto, probabilidad, mitigación, responsable y estado. |
| `decisiones/` | ADR con alternativas, decisión y consecuencias. |

## Reglas

- **Un directorio por sistema**, con nombre en minúsculas y guiones: `facturacion`, `portal-clientes`.
- **Sin datos crudos ni secretos:** no incluir inventarios completos, cadenas de conexión, rutas personales, nombres de servidores ni datos de clientes.
- **Trazabilidad:** indicar commit o versión analizada, fecha del levantamiento, autor y versión de los scripts.
- **Evidencia externa:** enlazar al repositorio del proyecto, al gestor documental o al almacenamiento aprobado; no copiar volúmenes grandes.
- **Actualización:** registrar el estado en cada fase (levantamiento, plan, piloto, oleadas, cierre) y mantener el AS-IS alineado con el alcance real.
- **Sanitización:** revisar el contenido antes de publicarlo en la base de conocimiento compartida.

## Índice de casos

| Sistema | Estado | Fecha del AS-IS | Responsable |
|---|---|---|---|
| _Sin casos registrados_ | — | — | — |

## Cómo agregar un caso

1. Crear el directorio con el nombre del sistema.
2. Copiar la estructura y completar el `README.md` con alcance, estado y responsables.
3. Redactar `as-is.md` a partir del [levantamiento AS-IS](../tecnologias/microsoft/dotnet/levantamiento-as-is.md) y del [informe de ejemplo](../tecnologias/microsoft/dotnet/ejemplos/README.md).
4. Registrar riesgos y decisiones.
5. Añadir la fila al índice y enlazar el caso desde la guía de migración correspondiente.

## Referencias

- [Migraciones](../README.md)
- [Levantamiento AS-IS para .NET](../tecnologias/microsoft/dotnet/levantamiento-as-is.md)
- [Guía de migración .NET](../tecnologias/microsoft/dotnet/framework-a-dotnet.md)
- [Decisiones y documentación](../../decisiones-y-documentacion/README.md)
- [Guía central de arquitectura](../../architecture.md)
