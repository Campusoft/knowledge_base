# Scripts de levantamiento AS-IS

Herramientas de PowerShell para inventariar soluciones, proyectos, dependencias, configuraciones y uso de APIs de un sistema .NET Framework antes de migrarlo. Su salida alimenta el [levantamiento AS-IS](../levantamiento-as-is.md) y las plantillas de [`plantillas/`](../plantillas/README.md).

Los scripts son de **solo lectura**: no modifican proyectos, no ejecutan `restore` ni `build` y no publican artefactos. Cualquier excepción debe ser explícita, documentada y autorizada.

## Estructura

```
scripts/
├── README.md                          # este archivo
├── Invoke-DotNetAsIsAssessment.ps1    # orquestador (previsto)
├── Get-DotNetSolutionInventory.ps1    # soluciones y proyectos (previsto)
├── Get-DotNetPackageInventory.ps1     # paquetes y ensamblados (previsto)
├── Get-DotNetDependencyGraph.ps1      # grafo de dependencias (previsto)
├── Get-DotNetApiUsage.ps1             # APIs y tecnologías (previsto)
├── Get-DotNetConfigInventory.ps1      # configuración y formatos (previsto)
├── Export-DotNetAsIsReport.ps1        # consolidación e informe (previsto)
├── lib/                               # módulo y funciones comunes
└── tests/                             # pruebas Pester
```

> **Estado:** la carpeta y su estructura están definidas; los scripts aún no se han implementado. Los nombres y responsabilidades de la tabla son el contrato previsto y pueden ajustarse antes de la primera versión.

## Scripts previstos

| Script | Responsabilidad | Salida principal |
|---|---|---|
| `Invoke-DotNetAsIsAssessment.ps1` | Orquestar el levantamiento completo, validar parámetros y encadenar los demás scripts. | Inventario consolidado e informe. |
| `Get-DotNetSolutionInventory.ps1` | Leer `.sln`, `.csproj`, `.vbproj` y `.fsproj`: rutas, tipo, `TargetFramework(s)`, `projectFormat` (`sdk`/`legacy`), salidas y referencias. | JSON y CSV de proyectos. |
| `Get-DotNetPackageInventory.ps1` | Extraer `PackageReference`, `packages.config`, `Directory.Packages.props`, referencias a ensamblados, COM y binarios locales. | JSON y CSV de dependencias. |
| `Get-DotNetDependencyGraph.ps1` | Construir el grafo proyecto a proyecto, detectar ciclos y componentes compartidos. | Mermaid o DGML. |
| `Get-DotNetApiUsage.ps1` | Buscar APIs y tecnologías relevantes: `System.Web`, WCF, Web Forms, WinForms, WPF, WF, Remoting, COM, P/Invoke, Registro, DTC, EF6. | JSON de hallazgos con evidencia. |
| `Get-DotNetConfigInventory.ps1` | Inventariar `app.config`, `web.config`, transforms, `.resx`, `.settings`, `.edmx`, `.pubxml`, `NuGet.config`, `global.json` y `Directory.Build.*`. | JSON y CSV de configuración. |
| `Export-DotNetAsIsReport.ps1` | Consolidar los JSON, generar el informe Markdown, métricas y resumen sanitizado. | Markdown y métricas. |

## Requisitos

- **PowerShell 7** recomendado; si se requiere compatibilidad con Windows PowerShell 5.1, debe probarse y documentarse.
- **MSBuild o `dotnet` SDK** solo si una función necesita evaluar proyectos; el análisis básico debe funcionar sin compilar.
- **Permisos de lectura** sobre el código y la configuración. La lectura de IIS o servicios puede requerir permisos adicionales y debe ser una función separada.
- **Pester 5** para ejecutar las pruebas de `tests/`.

## Convenciones

- Nombres con verbo aprobado de PowerShell: `Get-`, `Export-`, `Invoke-`, `Test-`.
- Parámetros mínimos: `-Path` (raíz del análisis), `-OutputPath`, `-Format` (`Json`, `Csv`, `Markdown`), `-Include`, `-Exclude`.
- Salida canónica en JSON con `schemaVersion`; el CSV y el Markdown se derivan de ella.
- Rutas relativas, orden determinista y nombres estables para permitir comparaciones entre ejecuciones.
- Sin acceso a red por defecto; si se consulta NuGet, debe ser una opción explícita.
- Errores no fatales registrados como hallazgos; nunca ignorados en silencio.
- Sin secretos: redactar cadenas de conexión, tokens y credenciales.
- Un archivo por responsabilidad; la lógica compartida va en `lib/`.

## Orden de ejecución

1. `Invoke-DotNetAsIsAssessment.ps1` valida el entorno y crea `output/`.
2. `Get-DotNetSolutionInventory.ps1` obtiene el mapa de proyectos.
3. `Get-DotNetPackageInventory.ps1` y `Get-DotNetConfigInventory.ps1` completan dependencias y formatos.
4. `Get-DotNetApiUsage.ps1` localiza incompatibilidades.
5. `Get-DotNetDependencyGraph.ps1` construye el grafo.
6. `Export-DotNetAsIsReport.ps1` consolida y genera el informe.

## Cómo agregar un script

1. Definir responsabilidad, entradas, salidas y límites en este README.
2. Implementar la lógica compartida en `lib/` y exportarla desde el módulo.
3. Añadir pruebas en `tests/` con un fixture sintético de [`ejemplos/`](../ejemplos/README.md).
4. Verificar que no modifica archivos ni requiere red por defecto.
5. Actualizar la tabla de scripts y el [levantamiento AS-IS](../levantamiento-as-is.md) si cambia el flujo.

## Referencias

- [Levantamiento AS-IS](../levantamiento-as-is.md)
- [Plantillas y esquemas](../plantillas/README.md)
- [Ejemplos y fixtures](../ejemplos/README.md)
- [Guía de migración](../framework-a-dotnet.md)
- [PowerShell](https://learn.microsoft.com/powershell/)
- [Pester](https://pester.dev/)
