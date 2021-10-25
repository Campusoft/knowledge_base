# Gestion Dependencias



# Publicar paquetes

Quickstart: Create and publish a NuGet package using Visual Studio (.NET Standard, Windows only)
- Configure package properties
- Run the pack command
- Publish with the dotnet CLI or nuget.exe CLI

```
dotnet nuget push AppLogger.1.0.0.nupkg --api-key <API-KEY-NUGET-ACCOUNT> --source https://api.nuget.org/v3/index.json
```

- Manage the published package

https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli


# Revision

Tener un repositorio privado de packages

Get started with NuGet packages
Developers can use Azure Artifacts to publish and consume NuGet packages both to and from feeds and public registries. A feed is an organizational construct that hosts packages. You can create public and private feeds, and you can control who can access your packages by modifying feed permissions.
https://docs.microsoft.com/en-us/azure/devops/artifacts/get-started-nuget?view=azure-devops&tabs=windows

