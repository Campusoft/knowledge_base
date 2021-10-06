# Varios

# CancellationToken 



# Generic Host

Host definition

A host is an object that encapsulates an app's resources, such as:

-    Dependency injection (DI)
-    Logging
-    Configuration
-    IHostedService implementations


Hosted services are long running asynchronous services.
https://wildermuth.com/2020/08/02/NET-Core-Console-Apps---A-Better-Way

Building a Console App with .NET Generic Host
https://dfederm.com/building-a-console-app-with-.net-generic-host/

IHostApplicationLifetime

Inject the IHostApplicationLifetime (formerly IApplicationLifetime) service into any class to handle post-startup and graceful shutdown tasks. Three properties on the interface are cancellation tokens used to register app start and app stop event handler methods. The interface also includes a StopApplication method.
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-5.0#ihostapplicationlifetime


# Files


IFileProvider

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/file-providers?view=aspnetcore-5.0#physical-file-provider

# CloudEvents 

Serializar un CloudEvents

Si se utiliza System.Text, la serializacion no es adecuada
string message = JsonSerializer.Serialize(cloudEvent);

TODO: Revisar la forma serializar el objeto. 

Forma 1. Use CloudEventContent

var content = new CloudEventContent(cloudEvent,ContentMode.Structured, new JsonEventFormatter());
 var message =  content.ReadAsStringAsync().Result;


CloudNative.CloudEvents.Amqp 	AMQP protocol binding using AMQPNetLite

(mayo-2021) Existe una version nueva 2.x, que se es trabajando, 
- Soporta: System.Text.Json


stable 2.0.0 release within the summer of 2021 (May/June/July).
https://github.com/cloudevents/sdk-csharp/blob/master/docs/changes-since-1x.md

# Linq

**MoreLINQ**

LINQ to Objects is missing a few desirable features. This project enhances LINQ to Objects with extra methods, in a manner which keeps to the spirit of LINQ.

https://github.com/morelinq/MoreLINQ


https://www.tutorialsteacher.com/linq/linq-tutorials


# Emails

Envio correos

The easiest way to send email from .NET and .NET Core. Use Razor for email templates and send using SendGrid, MailGun, SMTP and more.

https://github.com/lukencode/FluentEmail


# Thread

Semaphore and SemaphoreSlim

Async Waiting inside C# Locks
https://blog.cdemi.io/async-waiting-inside-c-sharp-locks/


# String Formats

SmartFormat
SmartFormat is a string composition library written in C# which is basically compatible with string.Format. More than that SmartFormat can format data with named placeholders, lists, pluralization and other smart extensions.
https://github.com/axuno/SmartFormat


# Schedule 


##  Cron expressions

Cronos is a .NET library for parsing Cron expressions and calculating next occurrences. It was designed with time zones in mind, and intuitively handles Daylight saving time (also known as Summer time) transitions as in *nix Cron.

https://github.com/HangfireIO/Cronos



## Referencias

Schedule Cron Jobs using HostedService in ASP.NET Core
https://github.com/dotnet-labs/ServiceWorkerCronJob



# Migration

Move your .NET Framework applications to .NET 5
The .NET Upgrade Assistant is a .NET global tool that helps you incrementally upgrade your .NET Framework-based Windows applications.
https://dotnet.microsoft.com/platform/upgrade-assistant

The .NET Portability Analyzer
https://docs.microsoft.com/en-us/dotnet/standard/analyzers/portability-analyzer


.NET Standard 2.0 was the last version to support .NET Framework.

Porting existing ASP.NET apps to .NET Core
https://aka.ms/aspnet-porting-ebook

# Storage / Cloud

Download Files from Azure with .NET Core Web API and Blazor WebAssembly
https://code-maze.com/download-files-from-azure-with-net-core-web-api-and-blazor-webassembly/#download-web-api


# Codigo Referencias


Tiene varios metodos que se puede utilizar en varios escenarios. (La clase original es internal).

Metodo: IDictionary<string, object?> ObjectToDictionary(object? value)

Given an object, adds each instance property with a public get method as a key and its associated value to a dictionary.

El helper: HtmlHelper.ObjectToDictionary, utiliza internamente este  metodo:

```
public static IDictionary<string, object> ObjectToDictionary(object value)
{
	return PropertyHelper.ObjectToDictionary(value);
}
```

https://github.com/dotnet/aspnetcore/blob/main/src/Shared/PropertyHelper/PropertyHelper.cs

# Async

Sync over async

Understanding Async, Avoiding Deadlocks in C#
https://medium.com/rubrikkgroup/understanding-async-avoiding-deadlocks-e41f8f2c6f5d

Avoid GetAwaiter().GetResult() at all cost
https://gsferreira.com/archive/2020/08/avoid-getawaiter-getresult-at-all-cost/

# CommandLine

 Command line parsing, invocation, and rendering of terminal output. 
https://github.com/dotnet/command-line-api

features 
- Soporta Subcommands
https://github.com/dotnet/command-line-api/blob/main/docs/Features-overview.md

# Workflow
## Workflow Core


## Elsa Core
Elsa Core is an open-source workflows library that can be used in any kind of .NET Core application. Using such a workflow library can be useful to implement business rules visually or programmatically.


# FileProviders

Microsoft.Extensions.FileProviders.Embedded

Tiene problemas:

Posee errores en archivos con doble extension.
Nombre: Foo.ts.cs
ManifestFile.ResourcePath: Campusoft.Generate.Cli.Templates.Foo.cs

Correcto:
Nombre: Foo.json.scriban
ManifestFile.ResourcePath:Campusoft.Generate.Cli.Templates.Foo.json.scriban

Upload files in ASP.NET Core
ASP.NET Core supports uploading one or more files using buffered model binding for smaller files and unbuffered streaming for larger files.
https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1


# Application Referencias

nopCommerce
The most popular open-source eCommerce shopping cart solution based on ASP.NET Core 
https://github.com/nopSolutions/nopCommerce

Full Modular Monolith application with Domain-Driven Design approach. 
- Posee codigo
- Posee diagramas
- Posee documentacion 
https://github.com/kgrzybek/modular-monolith-with-ddd

Northwind Traders is a sample application built using ASP.NET Core and Entity Framework Core. 

- .NET Core 3
- ASP.NET Core 3
- Entity Framework Core 3
- Angular 8
https://github.com/jasontaylordev/NorthwindTraders


# Building

## MSBuild 
 
Directory.Build.props and Directory.Build.targets

You can add a new property to every project by defining it in a single file called Directory.Build.props in the root folder that contains your source. When MSBuild runs, Microsoft.Common.props searches your directory structure for the Directory.Build.props file (and Microsoft.Common.targets looks for Directory.Build.targets). If it finds one, it imports the file and reads the properties defined within it. Directory.Build.props is a user-defined file that provides customizations to projects under a directory. 
https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019 

