# Conversión de proyectos a SDK-style

La conversión de un proyecto del **formato clásico de MSBuild** al **formato SDK-style** moderniza el sistema de proyectos: cambia la forma de declarar el destino, los archivos, los paquetes y la compilación. No cambia por sí sola el runtime ni migra una aplicación a .NET moderno; es un habilitador que suele ser necesario para apuntar a `netstandard2.0` o para usar multi-target.

Esta guía asume que la decisión ya se tomó en la Fase 2 de la [guía de migración](../framework-a-dotnet.md). Si lo que se evalúa es el destino de una biblioteca compartida, revisar primero [.NET Standard 2.0 y multi-targeting](netstandard2.0-y-multitargeting.md).

## 1. Qué cambia

| Aspecto | Formato clásico | Formato SDK-style |
|---|---|---|
| Elemento raíz | `<Project ToolsVersion="..." xmlns="...">` con imports | `<Project Sdk="Microsoft.NET.Sdk">` |
| Destino | `TargetFrameworkVersion` (`v4.8`) | `TargetFramework` (`net48`) o `TargetFrameworks` |
| Archivos | `<Compile Include="..." />` explícito | Inclusión implícita por convención (`**/*.cs`) |
| Paquetes | `packages.config` y carpeta `packages/` | `PackageReference` y `obj/project.assets.json` |
| Versión del ensamblado | `Properties/AssemblyInfo.cs` | Generada por el SDK (configurable) |
| Imports | `Microsoft.Common.props`, `Microsoft.CSharp.targets` | Provistos por el SDK |
| Salida | `bin/Debug/` | `bin/Debug/<TargetFramework>/` |
| Proyecto web | Aplicación web ASP.NET clásica con `Microsoft.WebApplication.targets` | `Microsoft.NET.Sdk.Web` es para **ASP.NET Core**, no para ASP.NET Framework |
| C++/CLI | `.vcxproj` clásico | No soportado en SDK-style |

El formato SDK-style es el estándar de .NET moderno y también puede usarse con proyectos que siguen apuntando a .NET Framework (bibliotecas, consola, WPF, Windows Forms y pruebas), con limitaciones según el tipo de proyecto.

## 2. Cuándo convertir y cuándo no

**Buenos candidatos:**

- Bibliotecas de clases, consola, procesos por lotes y servicios Worker.
- Proyectos de prueba.
- Bibliotecas WPF o Windows Forms cuando el diseñador y los controles de terceros estén validados.
- Bibliotecas compartidas que se quieren llevar a `netstandard2.0` o multi-target.

**Casos que requieren cautela o no deben convertirse:**

- **Aplicaciones web ASP.NET Framework / Web Forms:** `Microsoft.NET.Sdk.Web` es ASP.NET Core. Convertir el `.csproj` no migra `System.Web`. Mantener el proyecto clásico hasta reemplazar la plataforma.
- **C++/CLI:** no admite SDK-style; conservar `.vcxproj`.
- **Proyectos con imports, targets o herramientas de compilación propietarias** que dependen del formato clásico.
- **Proyectos con diseñadores o componentes de terceros** sin soporte comprobado.
- **Proyectos que no se van a tocar** y no bloquean la migración: convertir sin beneficio agrega riesgo.

## 3. Antes de empezar

1. Congelar una línea base: compilación limpia, pruebas y versión de paquetes.
2. Crear una rama y conservar copia del `.csproj`, `packages.config`, `app.config`, `web.config` y scripts de compilación.
3. Verificar el SDK instalado (`dotnet --info`) y la versión de Visual Studio.
4. Revisar si hay `global.json`, `Directory.Build.props`, `Directory.Build.targets` o `NuGet.config` que afecten al proyecto.
5. Identificar el tipo de proyecto, sus consumidores y si necesita `net48`, `netstandard2.0` o multi-target.
6. Confirmar que la restauración de paquetes funciona y que las fuentes NuGet están disponibles.

## 4. Procedimiento

### Paso 1: comparar y preservar propiedades

Crear un proyecto SDK vacío del mismo tipo o editar el `.csproj` en una rama. Antes de borrar nada, anotar:

- `OutputType`, `RootNamespace`, `AssemblyName`.
- `PlatformTarget`, `Prefer32Bit`, `LangVersion`.
- Firma de ensamblado, `InternalsVisibleTo` y recursos.
- Referencias a ensamblados, COM, proyectos y paquetes.
- Eventos de compilación, targets e imports personalizados.
- Propiedades de publicación y de web.

### Paso 2: cambiar el elemento raíz y el destino

Antes (clásico, simplificado):

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <OutputType>Library</OutputType>
    <RootNamespace>MiBiblioteca</RootNamespace>
    <AssemblyName>MiBiblioteca</AssemblyName>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="Class1.cs" />
    <Reference Include="System" />
    <Reference Include="System.Core" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
```

Después (SDK-style, mismo destino):

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <RootNamespace>MiBiblioteca</RootNamespace>
    <AssemblyName>MiBiblioteca</AssemblyName>
  </PropertyGroup>
</Project>
```

`OutputType` es `Library` por defecto. Si el proyecto es una aplicación, declarar `<OutputType>Exe</OutputType>` o `WinExe` según corresponda.

### Paso 3: eliminar imports y propiedades redundantes

El SDK aporta los imports de compilación, las propiedades comunes y los targets. Eliminar:

- `Microsoft.Common.props` y `Microsoft.CSharp.targets`.
- Propiedades que el SDK calcula, salvo que se necesite un valor explícito.
- Referencias a ensamblados del framework que el destino ya incluye (`System`, `System.Core`, etc.).

Conservar imports y targets personalizados, revisando que no dependan de rutas o propiedades del formato clásico.

### Paso 4: manejar la inclusión implícita de archivos

El SDK incluye por convención archivos `.cs`, recursos y contenido. Si el proyecto clásico tenía listas explícitas, pueden aparecer duplicados. Opciones:

```xml
<PropertyGroup>
  <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
</PropertyGroup>
```

O excluir archivos concretos:

```xml
<ItemGroup>
  <Compile Remove="Legacy\**" />
  <None Remove="Legacy\**" />
</ItemGroup>
```

Revisar también contenido que debe copiarse al directorio de salida:

```xml
<ItemGroup>
  <None Update="plantillas\**\*">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

### Paso 5: migrar `packages.config` a `PackageReference`

1. Usar la migración de Visual Studio para `packages.config` cuando esté disponible, o convertir manualmente cada paquete.
2. Revisar dependencias transitivas, versiones y paquetes con contenido o scripts de instalación.
3. Eliminar la carpeta `packages/` del control de versiones cuando ya no se use.
4. En proyectos .NET Framework consumidores, verificar los *binding redirects*; suelen requerir `AutoGenerateBindingRedirects` y `GenerateBindingRedirectsOutputType`.

No se debe asumir que la versión migrada es compatible con el destino. Validar cada paquete contra `net48`, `netstandard2.0` o el destino elegido.

### Paso 6: resolver `AssemblyInfo`

El SDK genera los atributos de ensamblado. Si el proyecto conserva `Properties/AssemblyInfo.cs`, pueden aparecer duplicados (`CS0579`). Opciones:

- Eliminar los atributos generados del archivo y conservar solo los personalizados.
- Desactivar la generación automática:

```xml
<PropertyGroup>
  <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
</PropertyGroup>
```

Mantener los atributos que el SDK no genera, como algunos `InternalsVisibleTo` personalizados, y revisar firma, versión y cultura.

### Paso 7: configuración, recursos y diseñadores

- **`app.config`:** se conserva en proyectos .NET Framework. En .NET moderno no se usa igual; evaluar `appsettings.json` o `System.Configuration.ConfigurationManager`.
- **`web.config`:** pertenece a ASP.NET Framework; no se traslada a `Microsoft.NET.Sdk.Web` sin migrar a ASP.NET Core.
- **`.resx`:** suele funcionar, pero revisar herramientas personalizadas, espacios de nombres y generación de código.
- **Diseñadores:** WPF y Windows Forms pueden funcionar en SDK-style, pero los controles de terceros y las herramientas antiguas pueden fallar. Probar apertura, edición y compilación en Visual Studio.
- **T4 y generadores:** validar que se ejecutan en compilación y que no dependen de rutas clásicas.

### Paso 8: proyectos especiales

- **WPF:** `<UseWPF>true</UseWPF>` y destino `net48` o `net8.0-windows`.
- **Windows Forms:** `<UseWindowsForms>true</UseWindowsForms>` y destino Windows.
- **Pruebas:** agregar `Microsoft.NET.Test.Sdk` y el adaptador del framework de pruebas; el proyecto de pruebas suele apuntar al runtime moderno, no a `netstandard2.0`.
- **Servicios Windows:** migrar a Worker Service o conservar el modelo con `<OutputType>Exe</OutputType>` y las referencias necesarias.
- **COM y componentes nativos:** validar plataforma, registro y arquitectura; SDK-style no elimina esas dependencias.

### Paso 9: eventos y targets

Convertir eventos `PreBuildEvent`/`PostBuildEvent` a targets cuando sea posible:

```xml
<Target Name="CopiarPlantillas" AfterTargets="Build">
  <Copy SourceFiles="@(Plantillas)" DestinationFolder="$(OutDir)plantillas" />
</Target>
```

Revisar que las rutas usen `$(OutDir)`, `$(TargetFramework)` y no rutas absolutas.

### Paso 10: compilar, probar y comparar

1. Restaurar y compilar en limpio.
2. Ejecutar pruebas y comparar resultados con la línea base.
3. Publicar o empaquetar y revisar el contenido.
4. Consumir la biblioteca desde una aplicación real, no solo desde el proyecto de pruebas.
5. Revisar el diff del `.csproj` y de los archivos eliminados.

## 5. Errores frecuentes

| Síntoma | Causa probable | Acción |
|---|---|---|
| `CS0579: duplicate attribute` | `AssemblyInfo.cs` convive con atributos generados. | Eliminar duplicados o desactivar `GenerateAssemblyInfo`. |
| `CS0101` o `NETSDK1022` por elementos duplicados | Inclusión implícita más `<Compile Include>`. | Quitar los `Include` o desactivar elementos por defecto. |
| Archivos que no llegan a la salida | Cambió el tratamiento de `Content`/`None`. | Declarar `CopyToOutputDirectory` o el item correcto. |
| `Could not load file or assembly` | Paquete o *binding redirect* no migrado. | Revisar `PackageReference`, versiones y redirects. |
| `MSB3644` o faltan ensamblados de referencia | Falta el *targeting pack* del framework. | Instalar el pack o usar el destino correcto. |
| El diseñador no abre | Proyecto o control no soportado. | Validar en Visual Studio; considerar mantener el proyecto clásico. |
| El proyecto web no compila como SDK | `Microsoft.NET.Sdk.Web` es ASP.NET Core. | Conservar ASP.NET Framework o planificar el reemplazo. |
| Cambian las rutas de salida | SDK agrega el TFM a `bin/`. | Ajustar scripts y pipelines. |
| Advertencias nuevas como errores | Analizadores y reglas del SDK. | Revisar `AnalysisLevel`, `TreatWarningsAsErrors` y suprimir con justificación. |

## 6. Validación

- [ ] El proyecto compila en limpio en la configuración y plataforma acordadas.
- [ ] Las pruebas pasan y los resultados coinciden con la línea base.
- [ ] El paquete o artefacto contiene los archivos esperados.
- [ ] Los consumidores restauran y compilan con la nueva versión.
- [ ] Los recursos, configuraciones y contenido se copian correctamente.
- [ ] La firma, versión y atributos del ensamblado son los esperados.
- [ ] Los diseñadores y herramientas del equipo siguen funcionando.
- [ ] El pipeline usa las rutas y comandos actualizados.
- [ ] El diff fue revisado por otra persona.

## 7. Reversión

Mantener en la rama el `.csproj` anterior, `packages.config`, configuraciones y scripts hasta que la conversión esté validada. Si falla, restaurar el proyecto clásico, la carpeta `packages/` si aplica y el pipeline anterior. No eliminar la versión antigua del artefacto hasta completar la oleada.

## 8. Referencias

- [Descripción general de los SDK de proyecto de .NET](https://learn.microsoft.com/dotnet/core/project-sdk/overview)
- [Portar de .NET Framework a .NET](https://learn.microsoft.com/dotnet/core/porting/framework-overview)
- [Migrar `packages.config` a `PackageReference`](https://learn.microsoft.com/nuget/consume-packages/migrate-packages-config-to-package-reference)
- [Referencia de MSBuild](https://learn.microsoft.com/visualstudio/msbuild/msbuild)
- [global.json](https://learn.microsoft.com/dotnet/core/tools/global-json)
- [Conversión a SDK-style: guía relacionada](netstandard2.0-y-multitargeting.md)
- [Herramientas y validación](herramientas-y-validacion.md)
