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

# Workflow
## Workflow Core


## Elsa Core
Elsa Core is an open-source workflows library that can be used in any kind of .NET Core application. Using such a workflow library can be useful to implement business rules visually or programmatically.