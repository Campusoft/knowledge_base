# Gestion Dependencias

### Publicar paquetes

Quickstart: Create and publish a NuGet package using Visual Studio (.NET Standard, Windows only)
- Configure package properties
- Run the pack command
- Publish with the dotnet CLI or nuget.exe CLI

```
dotnet nuget push AppLogger.1.0.0.nupkg --api-key qz2jga8pl3dvn2akksyquwcs9ygggg4exypy3bhxy6w6x6 --source https://api.nuget.org/v3/index.json
```

- Manage the published package

https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli