# Desarrollo y herramientas de .NET

## Migration

Move your .NET Framework applications to .NET 5
The .NET Upgrade Assistant is a .NET global tool that helps you incrementally upgrade your .NET Framework-based Windows applications.
https://dotnet.microsoft.com/platform/upgrade-assistant

The .NET Portability Analyzer
https://docs.microsoft.com/en-us/dotnet/standard/analyzers/portability-analyzer


.NET Standard 2.0 was the last version to support .NET Framework.

Porting existing ASP.NET apps to .NET Core
https://aka.ms/aspnet-porting-ebook

## CommandLine - (CLI)

 Command line parsing, invocation, and rendering of terminal output. 
https://github.com/dotnet/command-line-api

features 
- Soporta Subcommands
https://github.com/dotnet/command-line-api/blob/main/docs/Features-overview.md

### CliWrap

CliWrap is a library for interacting with external command line interfaces. It provides a convenient model for launching processes, redirecting input and output streams, awaiting completion, handling cancellation, and more.
https://github.com/Tyrrrz/CliWrap


Para direcioncar el Console.In a un commando con CliWrap, utilizar streams con Console.OpenStandardInput()

```
Stream inputStream = Console.OpenStandardInput();
await (inputStream | Cli.Wrap(command)).ExecuteAsync();
```

## Building

### MSBuild 
 
Directory.Build.props and Directory.Build.targets

You can add a new property to every project by defining it in a single file called Directory.Build.props in the root folder that contains your source. When MSBuild runs, Microsoft.Common.props searches your directory structure for the Directory.Build.props file (and Microsoft.Common.targets looks for Directory.Build.targets). If it finds one, it imports the file and reads the properties defined within it. Directory.Build.props is a user-defined file that provides customizations to projects under a directory. 
https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019
