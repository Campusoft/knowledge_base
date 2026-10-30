# Herramientas y validación

Guía práctica para ejecutar la conversión de proyectos y validar bibliotecas compartidas en varios destinos. Complementa [Conversión a SDK-style](conversion-a-sdk-style.md) y [.NET Standard 2.0 y multi-targeting](netstandard2.0-y-multitargeting.md).

La validación no termina cuando el proyecto compila: debe demostrar que los consumidores reales restauran, compilan, ejecutan y se comportan igual que antes.

## 1. Herramientas

| Herramienta | Uso | Precauciones |
|---|---|---|
| **`dotnet` CLI** | Compilar, probar, empaquetar, listar paquetes y consultar el SDK instalado. | Fijar la versión con `global.json`; no asumir que la SDK local coincide con la del pipeline. |
| **MSBuild / Visual Studio** | Compilar proyectos clásicos, preprocesar el proyecto, revisar propiedades y abrir diseñadores. | El comportamiento puede variar entre versiones de Visual Studio y MSBuild. |
| **Migración de `packages.config` de Visual Studio** | Convertir paquetes a `PackageReference`. | Revisar dependencias transitivas, contenido y *binding redirects*. |
| **.NET Upgrade Assistant** | Analizar y aplicar cambios comunes en proyectos .NET. | Microsoft lo marca como obsoleto; usar solo si el agente recomendado no está disponible y revisar cada cambio. |
| **GitHub Copilot modernization/upgrade agent** | Evaluación asistida, plan y cambios automatizados. | Es un asistente: revisar plan, diffs, dependencias y resultados; trabajar en rama. |
| **App and Code Assessment for .NET** | Inventario y análisis de preparación, especialmente para evaluaciones de Azure. | Complementa el análisis; no sustituye pruebas ni una migración real. |
| **.NET Portability Analyzer** | Detectar APIs no disponibles en el destino. | El análisis estático no cubre todo el comportamiento en runtime. |
| **Analizadores y compilador** | Detectar APIs obsoletas, advertencias y errores de configuración. | Ajustar `AnalysisLevel` y `TreatWarningsAsErrors` con criterio. |
| **NuGet** | Verificar activos compatibles y dependencias transitivas. | Una dependencia puede compilar y fallar en runtime. |
| **CI/CD** | Reproducir la matriz de compilación y pruebas. | El pipeline debe reflejar el proceso real de publicación. |
| **Pester** | Probar los scripts de levantamiento AS-IS. | Mantener fixtures sintéticos y pruebas independientes. |

## 2. Comandos útiles

```powershell
# Información del SDK y runtimes instalados
dotnet --info

# Compilar una solución completa
dotnet build .\MiSolucion.sln -c Release

# Compilar un proyecto para un destino concreto
dotnet build .\MiBiblioteca.csproj -c Release -f netstandard2.0

# Ejecutar pruebas en un destino ejecutable
dotnet test .\MiBiblioteca.Tests.csproj -c Release -f net8.0

# Empaquetar y revisar el resultado
dotnet pack .\MiBiblioteca.csproj -c Release -o .\artifacts

# Ver paquetes directos y transitivos
dotnet list .\MiBiblioteca.csproj package --include-transitive

# Preprocesar un proyecto clásico para revisar imports y propiedades
msbuild .\ProyectoClasico.csproj /pp:preprocesado.xml

# Generar un binlog de MSBuild para diagnóstico
msbuild .\ProyectoClasico.csproj /bl:build.binlog
```

En Windows, ejecutar MSBuild desde el *Developer Command Prompt* o indicar la ruta completa. Para compilar `net48` fuera de Windows se pueden usar los ensamblados de referencia, pero ejecutar pruebas de .NET Framework requiere Windows o una alternativa compatible.

## 3. Matriz de validación

| Destino | Compilar | Probar | Consumir desde | Empaquetar |
|---|---|---|---|---|
| `net48` | Sí, en Windows o con reference assemblies. | Sí, en Windows. | Aplicación .NET Framework. | Sí. |
| `netstandard2.0` | Sí, en cualquier plataforma. | No se ejecuta directamente: probar mediante un consumidor. | Aplicación .NET Framework y aplicación .NET moderna. | Sí. |
| `net8.0` / `net10.0` | Sí. | Sí. | Aplicación .NET moderna. | Sí. |
| Multi-target | Cada destino por separado. | Cada destino ejecutable. | Un consumidor por destino. | Sí, revisar `lib/<tfm>`. |

Recomendaciones:

- Probar como mínimo un consumidor Framework y uno moderno cuando la biblioteca aspire a ambos.
- No considerar suficiente que el proyecto de pruebas de la biblioteca compile: el consumo real valida selección de activos, dependencias y *binding redirects*.
- Ejecutar la matriz en CI, no solo en la máquina del desarrollador.
- Mantener un artefacto por combinación relevante y conservar logs.

## 4. Evidencias

Registrar por ejecución:

- Commit, rama, fecha y versión del SDK (`global.json`).
- Comandos ejecutados y configuración usada.
- Resultados de compilación y pruebas (`.trx`, binlog, logs de CI).
- Contenido del paquete (`.nupkg` y carpetas `lib/`).
- Resultado de consumo desde cada aplicación de prueba.
- Diferencias conocidas y decisiones tomadas.

Las evidencias permiten comparar antes y después, explicar fallos y demostrar que la validación se realizó.

## 5. Automatización

Un pipeline mínimo para una biblioteca compartida debería:

1. Restaurar con la versión de SDK fijada.
2. Compilar cada destino de la matriz.
3. Ejecutar pruebas en los destinos ejecutables.
4. Empaquetar y publicar el artefacto.
5. Consumir el paquete desde proyectos de prueba Framework y modernos.
6. Publicar logs y resultados como evidencia.

Ejemplo ilustrativo de matriz (adaptar al proveedor de CI):

```yaml
strategy:
  matrix:
    tfm: [net48, netstandard2.0, net8.0]
steps:
  - checkout
  - setup-dotnet with version from global.json
  - run: dotnet build -c Release -f ${{ matrix.tfm }}
  - if: matrix.tfm != 'netstandard2.0'
    run: dotnet test -c Release -f ${{ matrix.tfm }}
  - run: dotnet pack -c Release -o artifacts
```

Para `net48`, usar un runner Windows o instalar los reference assemblies; para `netstandard2.0`, limitar a compilación y empaquetado.

## 6. Recomendaciones

1. Fijar el SDK con `global.json` y la versión de las herramientas en la documentación.
2. Compilar en limpio y con la misma configuración del pipeline.
3. Validar cada destino y cada consumidor, no solo el proyecto principal.
4. Revisar el `.nupkg` antes de publicarlo.
5. Mantener los artefactos y logs de la línea base para comparar.
6. Convertir y validar en ramas pequeñas; evitar mezclar conversión de formato con cambios funcionales.
7. No eliminar la versión anterior hasta completar la oleada y confirmar la reversión.
8. Documentar limitaciones conocidas por destino (diseñadores, APIs, paquetes, binding redirects).

## 7. Referencias

- [CLI de .NET](https://learn.microsoft.com/dotnet/core/tools/)
- [MSBuild](https://learn.microsoft.com/visualstudio/msbuild/msbuild)
- [.NET Portability Analyzer](https://learn.microsoft.com/dotnet/standard/analyzers/portability-analyzer)
- [Empaquetar y publicar una biblioteca con `dotnet pack`](https://learn.microsoft.com/dotnet/core/tools/dotnet-pack)
- [Conversión a SDK-style](conversion-a-sdk-style.md)
- [.NET Standard 2.0 y multi-targeting](netstandard2.0-y-multitargeting.md)
- [Guía de migración](../framework-a-dotnet.md)
