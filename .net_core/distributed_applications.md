# distributed applications



# Aspire

.NET Aspire is an opinionated, cloud ready stack for building observable, production ready, distributed applications. .NET Aspire is delivered through a collection of NuGet packages that handle specific cloud-native concerns.


.NET Aspire is designed to improve the experience of building .NET cloud-native apps. It provides a consistent, opinionated set of tools and patterns that help you build and run distributed apps. .NET Aspire is designed to help you with:

Orchestration: .NET Aspire provides features for running and connecting multi-project applications and their dependencies.
Components: .NET Aspire components are NuGet packages for commonly used services, such as Redis or Postgres, with standardized interfaces ensuring they connect consistently and seamlessly with your app.
Tooling: .NET Aspire comes with project templates and tooling experiences for Visual Studio and the dotnet CLI help you create and interact with .NET Aspire apps.


- Docker Compose is excellent but is unproductive when all you want to do is run several projects or executables
- .NET Aspire can be considered the evolution of the Project Tye experiment.
https://learn.microsoft.com/en-us/dotnet/aspire/reference/aspire-faq




## Errores

---------------------------------------

```
Severity	Code	Description	Project	File	Line	Suppression State
Error	CS0246	The type or namespace name 'Projects' could not be found (are you missing a using directive or an assembly reference?)	AspireApp1.AppHost	E:\temp\AspireApp1\AspireApp1.AppHost\Program.cs	3	Active
```

Ejemplo

```
  <ItemGroup>
    <PackageReference Include="Aspire.Hosting" Version="8.0.0-preview.1.23557.2" />
  </ItemGroup>
```
Update all Aspire PackageReferences (especially the Aspire.Hosting reference in the AppHost.csproj) to match the version of the workload you are updating to.

Visualizar la version workload instalada
>dotnet workload list

Presenta:
- aspire                     8.0.0-preview.3.24105.21/8.0.100      SDK 8.0.200, VS 17.10.34607.79

Utilizar esta informacion para actualizar las referencias

```
  <ItemGroup>
    <PackageReference Include="Aspire.Hosting" Version="8.0.0-preview.3.24105.21" />
  </ItemGroup>
```

https://github.com/dotnet/aspire/issues/1944


-----------------------------------------
