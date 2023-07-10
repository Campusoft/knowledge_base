# Varios


# Globalization and localization in ASP.NET Core

***Revision***
Configure portable object localization in ASP.NET Core

# Validaciones


IModelValidator 

## FluentValidation

FluentValidationModelValidator
FluentValidation's implementation of an ASP.NET Core model validator.
https://github.com/FluentValidation/FluentValidation.AspNetCore/blob/main/src/FluentValidation.AspNetCore/FluentValidationModelValidatorProvider.cs

return result.Errors.Select(x => new ModelValidationResult(x.PropertyName, x.ErrorMessage));


throw new ValidationException("foo");


ValidationActionFilter 
ValidationPipelineBehavior 
FluentValidationModelValidatorProvider

Deep Dive Into Different Validators with FluentValidation
https://code-maze.com/deep-dive-validators-fluentvalidation/


# Exceptions


Best practices for exceptions
https://docs.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions



# Configurations 


Multiple Ways To Access Configurations In .NET Applications. (appsettings)
- IConfiguration
- GetValue
- GetSection
- GetChildren
- Exists
- Bind
- Options Pattern
https://thecodeblogger.com/2021/04/20/multiple-ways-to-access-configurations-in-net-applications/




TimeSpan configuration values in .NET Core by Mark Seemann
- Leer numero entero. (Total segundos, minutos, o la medida que se establece que significa el numero)

Configuracion Json
```
{
  "SeatingDurationInSeconds": "9000"
}
```
Codigo
```
var seatingDuration = TimeSpan.FromSeconds(Configuration.GetValue<int>("SeatingDurationInSeconds"));
```

Use the [standard TimeSpan string](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-timespan-format-strings) representation instead:

Configuracion Json
```
{
  "SeatingDuration": "2:30:00"
}
```
Codigo
```
var seatingDuration = Configuration.GetValue<TimeSpan>("SeatingDuration");
```
https://blog.ploeh.dk/2019/11/25/timespan-configuration-values-in-net-core/ 


**Options pattern**

- Is registered as a Singleton and can be injected into any service lifetime.

Options pattern suggests two things:

- there should be a class, Options class, which is not abstract with public parameter-less constructor
- which has public read-write properties
	
IOptionsSnapshot:
When you use IOptionsSnapshot<TOptions>, options are computed once per request when accessed and are cached for the lifetime of the request. Changes to the configuration are read after the app starts when using configuration providers that support reading updated configuration values.

	
IOptionsMonitor:

Some file systems, such as Docker containers and network shares, may not reliably send change notifications. When using the IOptionsMonitor<TOptions> interface in these environments, set the DOTNET_USE_POLLING_FILE_WATCHER environment variable to 1 or true to poll the file system for changes. The interval at which changes are polled is every four seconds and is not configurable.	

## Revisiones

IOption<T> o IOptionsSnapshot<T> o tal vez IOptionsMonitor<T> 

# date and time


nodatime
Noda Time is an alternative date and time API for .NET. It helps you to think about your data more clearly, and express operations on that data more precisely.

https://github.com/nodatime/nodatime


# Channels

Channel basically is a data structure which supports the communication between a producer and consumer, it’s thread safe and has an excellent notification framework in both directions.

The System.Threading.Channels namespace provides us with the necessary constructs to make building a pipeline of producers/consumers easier, without having to worry about locking and other potential concurrency issues. It also provides bounded and unbounded “channels”

- System.Threading.Channels is always available in .NET Core
- Thread safe
- Concurrency


Now, a Channel can be bounded or unbounded:

- Bounded Channels have a finite capacity for incoming messages, meaning that a Producer can publish only a specific amount of times before fulfilling the space. Then it will have to wait for the Consumers to execute their work and free up some space for new messages.
- Unbounded Channels instead don’t have this limitation, meaning that Publishers can publish as many times as they want, hoping that the Consumers are able to keep up.


Reading from a Channel

Option: ReadAllAsync
Another option ("Option 1" in the code) is to use "ReadAllAsync".

```
// Read person objects from the channel until the channel is closed
    // OPTION 1: Using ReadAllAsync (IAsyncEnumerable)
    await foreach (Person person in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"{person.ID}: {person}");
    }
```

 "ReadAllAsync" is a method on the channel "Reader" property. This returns an "IAsyncEnumerable<Person>". This combines the power of enumeration ("foreach") with the concurrency of "async".

The "foreach" loop condition looks pretty normal -- we iterate on the result of "ReadAllAsync" and use the "Person" object from that iteration.

What is a little different is that there is an "await" before the "foreach". This means that the "foreach" loop may pause between iterations if it needs to wait for an item to be populated in the channel.

The effect is that the "foreach" will continue to provide values until the channel is closed and empty. Inside the loop, we write the value to the console. 


Option: WaitToReadAsync / TryRead
"Option 2" uses a couple of "while" loops to read from the channel.

```
    // Read person objects from the channel until the channel is closed
    // OPTION 2: Using WaitToReadAsync / TryRead
    while (await channel.Reader.WaitToReadAsync())
    {
      while (channel.Reader.TryRead(out Person person))
      {
        Console.WriteLine($"{person.ID}: {person}");
      }
    }
```

Closing a Channel

When we are done writing to a channel, we should close it. This tells anyone reading from the channel that there will be no more values, and they can stop reading. We'll see why this is important when we look at reading from a channel. 




## Referencias

Producer/consumer pipelines with System.Threading.Channels
- Explicacion con un escanario
- El ejemplo son una serie pasos que se colocan en pipeline
- producer-consumer pattern
https://blog.maartenballiauw.be/post/2020/08/26/producer-consumer-pipelines-with-system-threading-channels.html


An Introduction to Channels in C# 
- Formas de leer desde un canal.
https://jeremybytes.blogspot.com/2021/02/an-introduction-to-channels-in-c.html


# CancellationToken 


Utilizar HttpContext.RequestAborted, como CancellationToken

```
public class AspNetUserManager<TUser> : UserManager<TUser>, IDisposable where TUser : class
{
    private readonly CancellationToken _cancel;

    /// <summary>
    /// Constructs a new instance of <see cref="AspNetUserManager{TUser}"/>.
    /// </summary>
    /// <param name="store">The persistence store the manager will operate over.</param>
    /// <param name="optionsAccessor">The accessor used to access the <see cref="IdentityOptions"/>.</param>
    /// <param name="passwordHasher">The password hashing implementation to use when saving passwords.</param>
    /// <param name="userValidators">A collection of <see cref="IUserValidator{TUser}"/> to validate users against.</param>
    /// <param name="passwordValidators">A collection of <see cref="IPasswordValidator{TUser}"/> to validate passwords against.</param>
    /// <param name="keyNormalizer">The <see cref="ILookupNormalizer"/> to use when generating index keys for users.</param>
    /// <param name="errors">The <see cref="IdentityErrorDescriber"/> used to provider error messages.</param>
    /// <param name="services">The <see cref="IServiceProvider"/> used to resolve services.</param>
    /// <param name="logger">The logger used to log messages, warnings and errors.</param>
    public AspNetUserManager(IUserStore<TUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<TUser> passwordHasher,
        IEnumerable<IUserValidator<TUser>> userValidators,
        IEnumerable<IPasswordValidator<TUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<TUser>> logger)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
        _cancel = services?.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted ?? CancellationToken.None;
    }

    /// <summary>
    /// The cancellation token associated with the current HttpContext.RequestAborted or CancellationToken.None if unavailable.
    /// </summary>
    protected override CancellationToken CancellationToken => _cancel;
}
```

https://github.com/dotnet/aspnetcore/blob/main/src/Identity/Core/src/AspNetUserManager.cs



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

Background tasks with hosted services in ASP.NET Core
In ASP.NET Core, background tasks can be implemented as hosted services.
- BackgroundService is a base class for implementing a long running IHostedService.
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio

# Release

ASP.Net Core Reverse Proxy with different root
https://stackoverflow.com/questions/45311393/asp-net-core-reverse-proxy-with-different-root


.NET Core hosted on subdirectories in Nginx
https://www.billbogaiv.com/posts/net-core-hosted-on-subdirectories-in-nginx


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



# Emails

Envio correos

The easiest way to send email from .NET and .NET Core. Use Razor for email templates and send using SendGrid, MailGun, SMTP and more.
https://github.com/lukencode/FluentEmail

 A cross-platform .NET library for IMAP, POP3, and SMTP. 
https://github.com/jstedfast/MailKit


## MailKit

ASP.NET Core 3.1 - Send Emails via SMTP with MailKit
- gmail
- office 365
https://jasonwatmore.com/post/2020/07/15/aspnet-core-3-send-emails-via-smtp-with-mailkit

How can I create a message with attachments
https://github.com/jstedfast/MailKit/blob/master/FAQ.md#CreateAttachments

## Herramientas

SMTP para probar envíos de correos electrónicos, el cual registra una petición de envío de correo 
electrónico sin llegar a enviarlo realmente. Existen múltiples herramientas de código abierto en este 
ámbito, incluyendo fake-smtp-server, el cual representa correos electrónicos dentro de una interfaz 
de usuario web para pruebas visuales, y mountebank, que expone los correos electrónicos enviados 
a través de una API REST para realizar pruebas de integración. Recomendamos explorar esta técnica 
para reducir el riesgo y mejorar la eficiencia de las pruebas.

# LDAP

Utiliza ldap4net

https://github.com/flamencist/ldap4net

Online LDAP Test Server.
(Servidor LDAP para realizar pruebas de conexion)
https://www.forumsys.com/tutorials/integration-how-to/ldap/online-ldap-test-server/

Atributos
- cn CN=Guy Thomas. En realidad, este atributo LDAP está conformado por givenName unido con SN. 
- dc Se refiere al componente del dominio, ya sea un componente, una etiqueta o un nombre de dominio DNS. Ejemplo: DC=cp, DC=com
- givenName Primer nombre.
- SN SN = Thomas. Esto será referido como apellido. 
- uid
- mail
 
***Filtros buscar usuarios***

Utilizar  userPrincipalName
(&(objectClass=user)(userPrincipalName=nombre-usuario))
 
Utiizar sAMAccountName
(&(objectClass=user)(sAMAccountName={nombre-usuario}))
 
Uttilizar uid
(&(uid=nombre-usuario))
 

Autenticación de usuarios con registros LDAP en un bosque de Microsoft Active Directory
- menciona que es mejor utilizar userPrincipalName, que el sAMAccountName
https://www.ibm.com/docs/es/was/9.0.5?topic=umada-authenticating-users-ldap-registries-in-microsoft-active-directory-forest


Active Directory Explorer
Active Directory Explorer (AD Explorer) is an advanced Active Directory (AD) viewer and editor. You can use AD Explorer to easily navigate an AD database, define favorite locations, view object properties and attributes without having to open dialog boxes, edit permissions, view an object's schema, and execute sophisticated searches that you can save and re-execute.
https://docs.microsoft.com/en-us/sysinternals/downloads/adexplorer


# Thread

Semaphore and SemaphoreSlim

Async Waiting inside C# Locks
https://blog.cdemi.io/async-waiting-inside-c-sharp-locks/


# String Formats

SmartFormat
SmartFormat is a string composition library written in C# which is basically compatible with string.Format. More than that SmartFormat can format data with named placeholders, lists, pluralization and other smart extensions.
https://github.com/axuno/SmartFormat


# Schedule 

## Hangfire


## Quartz.NET

Open-source job scheduling system for .NET 
https://www.quartz-scheduler.net/

 
##  Cron expressions

Cronos is a .NET library for parsing Cron expressions and calculating next occurrences. It was designed with time zones in mind, and intuitively handles Daylight saving time (also known as Summer time) transitions as in *nix Cron.

https://github.com/HangfireIO/Cronos


## Gofer.NET: Easy distributed tasks/jobs for .NET Core

This is a distributed job runner for .NET Standard 2.0 Applications.

Inspired by Celery for Python, it allows you to quickly queue code execution on a worker pool.

- Use natural expression syntax to queue jobs for execution.
- Queued jobs are persisted, and automatically run by the first available worker.
- Scale your worker pool by simply adding new nodes.
- Backed by Redis, all tasks are persistent.


https://github.com/brthor/Gofer.NET

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

# CommandLine - (CLI)

 Command line parsing, invocation, and rendering of terminal output. 
https://github.com/dotnet/command-line-api

features 
- Soporta Subcommands
https://github.com/dotnet/command-line-api/blob/main/docs/Features-overview.md

## CliWrap

CliWrap is a library for interacting with external command line interfaces. It provides a convenient model for launching processes, redirecting input and output streams, awaiting completion, handling cancellation, and more.
https://github.com/Tyrrrz/CliWrap


Para direcioncar el Console.In a un commando con CliWrap, utilizar streams con Console.OpenStandardInput()

```
Stream inputStream = Console.OpenStandardInput();
await (inputStream | Cli.Wrap(command)).ExecuteAsync();
```


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


 

# Mapping Objects


**Automapper**

Profile Instances

A good way to organize your mapping configurations is with profiles. Create classes that inherit from Profile and put the configuration in the constructor:
https://docs.automapper.org/en/latest/Configuration.html


If you are using ASP.NET Core there is a helper extension to register all Profiles in Startup.ConfigureServices

// UI project
services.AddAutoMapper(Assembly.GetExecutingAssembly());

Open Generics

AutoMapper can support an open generic type map. Create a map for the open generic types
https://docs.automapper.org/en/stable/Open-Generics.html#open-generics

**Mapster**
Mapster - The Mapper of Your Domain
Writing mapping methods is a machine job. Do not waste your time, let Mapster do it.
https://github.com/MapsterMapper/Mapster

# Building

## MSBuild 
 
Directory.Build.props and Directory.Build.targets

You can add a new property to every project by defining it in a single file called Directory.Build.props in the root folder that contains your source. When MSBuild runs, Microsoft.Common.props searches your directory structure for the Directory.Build.props file (and Microsoft.Common.targets looks for Directory.Build.targets). If it finds one, it imports the file and reads the properties defined within it. Directory.Build.props is a user-defined file that provides customizations to projects under a directory. 
https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019 

# Errores

## Unable to connect to web server 'IIS Express' 
 
- Posible problema con puertos, verificar y cambiar el puerto utilizado en: launchSettings.json utilizando el comando: netsh interface ipv4 show excludedportrange protocol=tcp para listar los puertos ocupados
- Fuente: https://www.variablenotfound.com/2020/06/como-solucionar-el-error-unable-to.html

# elasticsearch-net ( Elasticsearch.Net & NEST)

NEST

NEST is the official high-level .NET client of Elasticsearch.

It aims to be a solid, strongly typed client with a very concise API. The client internally uses the low-level Elasticsearch.Net client.

https://github.com/elastic/elasticsearch-net

***Mapping***


https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/mapping.html 


# Kestrel

Kestrel is a cross-platform web server for ASP.NET Core.
Kestrel supports the following scenarios:

- HTTPS
- HTTP/2 (except on macOS†)
- Opaque upgrade used to enable WebSockets
- Unix sockets for high performance behind Nginx

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-6.0


# web crawler

Abot Build Status

 Cross Platform C# web crawler framework built for speed and flexibility. Please star this project! +1. 
https://github.com/sjdirect/abot