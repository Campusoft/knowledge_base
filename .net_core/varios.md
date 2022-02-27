# Varios

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

# Certificados SSL



Use dotnet dev-certs to create self-signed certificates for development and testing.

Generar certificados. Primero limpiar, luego generar
```
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```
Si un certificado se caduco, y en el navegador continuea utilizando el certificado caducado a pesar de haber eliminado. En el siguiente enlace se menciona algunos elementos
- dotnet dev-certs https --clean Fails

https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-3.1&tabs=visual-studio#all-platforms---certificate-not-trusted-1

Errores relacionados

```
[13:54:44 ERR] Exception occurred while processing message.
System.InvalidOperationException: IDX20803: Unable to obtain configuration from:
 'System.String'.
 ---> System.IO.IOException: IDX20804: Unable to retrieve document from: 'System
.String'.
 ---> System.Net.Http.HttpRequestException: The SSL connection could not be esta
blished, see inner exception.
 ---> System.Security.Authentication.AuthenticationException: The remote certifi
cate is invalid because of errors in the certificate chain: UntrustedRoot

```


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
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio


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

 A cross-platform .NET library for IMAP, POP3, and SMTP. 
https://github.com/jstedfast/MailKit


## MailKit

ASP.NET Core 3.1 - Send Emails via SMTP with MailKit
- gmail
- office 365
https://jasonwatmore.com/post/2020/07/15/aspnet-core-3-send-emails-via-smtp-with-mailkit

How can I create a message with attachments
https://github.com/jstedfast/MailKit/blob/master/FAQ.md#CreateAttachments

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

# Mapping Objects


Automapper

Profile Instances

A good way to organize your mapping configurations is with profiles. Create classes that inherit from Profile and put the configuration in the constructor:
https://docs.automapper.org/en/latest/Configuration.html


If you are using ASP.NET Core there is a helper extension to register all Profiles in Startup.ConfigureServices

// UI project
services.AddAutoMapper(Assembly.GetExecutingAssembly());

Open Generics

AutoMapper can support an open generic type map. Create a map for the open generic types
https://docs.automapper.org/en/stable/Open-Generics.html#open-generics



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

