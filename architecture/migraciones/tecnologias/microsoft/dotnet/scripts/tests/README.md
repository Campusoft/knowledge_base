# Pruebas de los scripts

Pruebas automatizadas con **Pester** para validar que los scripts de levantamiento producen resultados correctos, deterministas y seguros sobre soluciones de ejemplo.

## Contenido previsto

```
tests/
├── README.md
├── Get-DotNetSolutionInventory.Tests.ps1   # previsto
├── Get-DotNetPackageInventory.Tests.ps1    # previsto
├── Get-DotNetDependencyGraph.Tests.ps1     # previsto
├── Get-DotNetApiUsage.Tests.ps1            # previsto
├── Export-DotNetAsIsReport.Tests.ps1       # previsto
└── fixtures/                               # enlace o copia del fixture sintético (previsto)
```

> **Estado:** las pruebas aún no se han implementado. La estructura y el enfoque quedan definidos en este documento.

## Qué se debe probar

| Área | Casos mínimos |
|---|---|
| Inventario de proyectos | Solución con proyectos SDK y legados; proyectos multi-target; bibliotecas compartidas con distintos consumidores; proyectos sin compilar; rutas con espacios; referencias rotas. |
| Paquetes y dependencias | `PackageReference`, `packages.config`, `Directory.Packages.props`, ensamblados locales y paquetes duplicados. |
| Grafo | Dependencias directas, transitivas, ciclos y proyectos compartidos. |
| APIs | Presencia y ausencia de `System.Web`, WCF, Web Forms, COM y P/Invoke; evitar falsos positivos por comentarios o cadenas. |
| Configuración | `web.config` con transforms, claves sensibles y archivos ausentes. |
| Salida | JSON válido, `schemaVersion`, orden estable, CSV coherente y Markdown generado. |
| Seguridad | Cadenas de conexión y tokens redactados; sin escritura fuera de `output/`. |
| Errores | Permisos insuficientes, archivos corruptos y rutas inexistentes registrados como hallazgos. |

## Convenciones

- Un archivo `*.Tests.ps1` por script o módulo.
- Datos de prueba **sintéticos**: sin nombres de clientes, rutas personales ni secretos reales.
- Ejecutar con Pester 5:

```powershell
Invoke-Pester -Path ./tests -Output Detailed
```

- Cada prueba debe ser independiente y limpiar su directorio temporal.
- Comparar resultados contra expectativas explícitas, no contra la salida de la misma ejecución.
- Mantener el fixture pequeño y versionado en [`../../ejemplos/`](../../ejemplos/README.md).

## Cómo agregar una prueba

1. Identificar el comportamiento y el caso de error que se quiere proteger.
2. Crear o ampliar el fixture sintético.
3. Escribir la prueba antes o junto con la implementación.
4. Verificar que falla sin el cambio y pasa con él.
5. Actualizar este README si se agrega un área nueva.

## Referencias

- [Scripts de levantamiento](../README.md)
- [Librería común](../lib/README.md)
- [Ejemplos y fixtures](../../ejemplos/README.md)
- [Pester](https://pester.dev/)
