# Gestion Dependencias



# Publicar paquetes

Importante:
- Un paquete no puede actualizarse. Se debe subir nuevas versiones
- Un paquete no puede eliminarse.
- Utilizar siempre en el nombre del paquetes y en los namespace el nombre de empresa
  - Empresa.Producto.Modulo/Capa. Ejemmplo Campusoft.Core.EntityFrameworkCore Campusoft.Sync.EntityFrameworkCore

Quickstart: Create and publish a NuGet package using Visual Studio (.NET Standard, Windows only)
- Configure package properties
- Run the pack command
- Publish with the dotnet CLI or nuget.exe CLI

```
dotnet nuget push AppLogger.1.0.0.nupkg --api-key <API-KEY-NUGET-ACCOUNT> --source https://api.nuget.org/v3/index.json
```

- Manage the published package

https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli

Publishing packages
- Publish with nuget push
https://docs.microsoft.com/en-us/nuget/nuget-org/publish-a-package


**Recomendaciones**

- Publicar los paquetes en release
- Utilizar un archivo comun para compartir informacion de paquete en todos los proyectos de una solucion. 

Archivo comun con informacion paquete:

```
<Project>
  <PropertyGroup>
    <LangVersion>latest</LangVersion>
    <Version>0.3.2</Version>
	<Copyright>Nombre comun cliente...</Copyright>
    <NoWarn>$(NoWarn);CS1591</NoWarn>
    <AbpProjectType>module</AbpProjectType>
  </PropertyGroup>

</Project>
```

Reutilizar archivo comun en un proyecto:
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

```

- En etapas de desarrollo utilizar versiones menores para actualizar cambios en paquetes parte del desarrollo. 
  -  Ejemplo: Verion 0.4.5,  0.4.6 ...


# Eliminar 

nuget.org does not support permanent deletion of packages. 
- Escenario. Si no se puede eliminr pasar el control a otro usuario (Ej. Al cliente)
https://docs.microsoft.com/en-us/nuget/nuget-org/policies/deleting-packages


# Nuget


You can also view folder locations using the dotnet nuget locals command:

```
dotnet nuget locals all --list
```

Managing the global packages, cache, and temp folders
https://learn.microsoft.com/en-us/nuget/consume-packages/managing-the-global-packages-and-cache-folders


Configuration files
NuGet.config files are located here:

```
User-specific: %APPDATA%\NuGet\
Machine-wide: %ProgramFiles(x86)%\NuGet\Config\
```

Ejemplo, cambiar la carpeta de paquetes globales de nugget
- <add key="globalPackagesFolder" value="E:\nuget" />

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <config>
        <add key="globalPackagesFolder" value="E:\nuget" />
    </config>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>
```



# Revision


Tener un repositorio privado de packages

Get started with NuGet packages
Developers can use Azure Artifacts to publish and consume NuGet packages both to and from feeds and public registries. A feed is an organizational construct that hosts packages. You can create public and private feeds, and you can control who can access your packages by modifying feed permissions.
https://docs.microsoft.com/en-us/azure/devops/artifacts/get-started-nuget?view=azure-devops&tabs=windows

