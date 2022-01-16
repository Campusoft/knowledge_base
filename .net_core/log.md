# Logs
Logging in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1

Logs, xunit
https://stackoverflow.com/questions/46169169/net-core-2-0-configurelogging-xunit-test


The semantics of ILogger.BeginScope()
Microsoft.Extensions.Logging’s ILogger adds context to log events with BeginScope():
https://nblumhardt.com/2016/11/ilogger-beginscope/

# Tips

// Don't:
Log.Information("The time is " + DateTime.Now);

Instead, always use template properties to include variables in messages:


// Do:
Log.Information("The time is {Now}", DateTime.Now);



# Nlog

Nlog. (Configuracion net core)
https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-2

Configuraciones de rotación de archivos
https://github.com/nlog/NLog/wiki/File-target#size-based-file-archival

COnfiguracion de Nlog en console. 
https://github.com/NLog/NLog.Extensions.Logging/wiki/Getting-started-with-.NET-Core-2---Console-application

# Serilog 

Serilog provides sinks for writing log events to storage in various formats.

To use the file sink with Microsoft.Extensions.Configuration, for example with ASP.NET Core or .NET Core, use the Serilog.Settings.Configuration package.

A Serilog configuration provider that reads from Microsoft.Extensions.Configuration
Serilog.Settings.Configuration

System.ArgumentException: Buffered writes are not available when file sharing is enabled. (Parameter 'buffered')

***configuration***

A Serilog settings provider that reads from Microsoft.Extensions.Configuration sources, including .NET Core's appsettings.json file.
- By default, configuration is read from the Serilog section.

Agregar nuget:
```
Install-Package Serilog.Settings.Configuration -Version 3.3.0
```
https://github.com/serilog/serilog-settings-configuration


Algunos ejemplos configuraciones appsettings.json
https://stackoverflow.com/questions/54715142/serilog-not-writing-to-file-net-core-2-2


 
Agregar campos con valores estaticos, en los logs, desde appsettings. Agregar campo "Application", con el valor "Sample"

```
"Serilog": {
    ...
    "Properties": {
      "Application": "Sample"
    }
}
``` 

***Serilog integration for ASP.NET Core***

Serilog logging for ASP.NET Core. This package routes ASP.NET Core log messages through Serilog, so you can get information about ASP.NET's internal operations written to the same Serilog sinks as your application events.

- package Serilog.AspNetCore
- During request processing, additional properties can be attached to the completion event using IDiagnosticContext.Set():
https://github.com/serilog/serilog-aspnetcore

Request logging
The package includes middleware for smarter HTTP request logging. 

```
[11:47:33 INF] HTTP GET / responded 200 in 910.9299 ms
[11:47:36 INF] HTTP GET /Home/Privacy responded 200 in 17.6987 ms
[11:47:48 INF] HTTP GET /Identity/Account/Manage responded 200 in 1994.1863 ms
[11:47:59 INF] HTTP GET /Identity/Account/Manage/TwoFactorAuthentication responded 200 in 106.7120 ms
[11:48:01 INF] HTTP GET /Identity/Account/Manage/ChangePassword responded 200 in 45.2777 ms
```
By default, Serilog ignores providers, since there are usually equivalent Serilog sinks available, and these work more efficiently with Serilog's pipeline. If provider support is needed, it can be optionally enabled.
https://github.com/serilog/serilog-aspnetcore#enabling-microsoftextensionsloggingiloggerproviders
 

##  Enrichers

Enrichers are simple components that add, remove or modify the properties attached to a log event.


Enrich.WithProcessId() and/or .WithProcessName() 
https://github.com/serilog/serilog-enrichers-process

Enriches Serilog events with information from the process environment.
- WithMachineName() - adds MachineName based on either %COMPUTERNAME% (Windows) or $HOSTNAME (macOS, Linux)
- WithEnvironmentUserName() - adds EnvironmentUserName based on USERNAME and USERDOMAIN (if available)
https://github.com/serilog/serilog-enrichers-environment
https://github.com/serilog/serilog-enrichers-environment#included-enrichers

enricher packages

- Serilog.Enrichers.CorrelationId - WithCorrelationId() will add a CorrelationId property to produced events
- Serilog.Enrichers.ClientInfo - WithClientIp() and WithClientAgent() will add properties with client IP and UserAgent
  - https://github.com/mo-esmp/serilog-enrichers-clientinfo

##  Output templates

https://github.com/serilog/serilog/wiki/Configuration-Basics#output-templates

##  Sinks

***Serilog.Sinks.File***
Write Serilog events to files in text and JSON formats, optionally rolling on time or size 
https://github.com/serilog/serilog-sinks-file#rolling-policies

***Serilog.Sinks.Async***
An asynchronous wrapper for other Serilog sinks. Use this sink to reduce the overhead of logging calls by delegating work to a background thread. This is especially suited to non-batching sinks like the File and RollingFile sinks that may be affected by I/O bottlenecks.

Note: many of the network-based sinks (CouchDB, Elasticsearch, MongoDB, Seq, Splunk...) already perform asynchronous batching natively and do not benefit from this wrapper.

https://github.com/serilog/serilog-sinks-async




***Serilog.Formatting.Compact***

A simple, compact JSON-based event format for Serilog. CompactJsonFormatter significantly reduces the byte count of small log events when compared with Serilog's default JsonFormatter, while remaining human-readable
- Install-Package Serilog.Formatting.Compact
https://github.com/serilog/serilog-formatting-compact




##  Tools

Roslyn-based analysis for code using the Serilog logging library. Checks for common mistakes and usage problems.
https://marketplace.visualstudio.com/items?itemName=Suchiman.SerilogAnalyzer

### Laboratorios


Añadimos Serilog obteniendo la configuración desde Microsoft.Extensions.Configuration

```
.UseSerilog((HostBuilderContext context, LoggerConfiguration loggerConfiguration) =>
{
	loggerConfiguration.ReadFrom.Configuration(context.Configuration);
})
```

##  Referencias

Serilog Best Practices
https://benfoster.io/blog/serilog-best-practices/

##  Revisiones

- TODO: Colocar porque serilog es diferente a los tipicos liberias de logs.
- TODO: A Serilog event sink that writes to Microsoft Teams 
- TODO: Formatos: outputTemplate


# Trace

Now you need to choose your Trace provider (for example: Application Insights, OpenTrace) and configure to read metrics from your DiagnosticSource.
Application Insights
OpenTrace

open source tracing protocols
- Jaeger
- Zipkin
- OpenTelemetry.
 

# Laboratorio

Activar Logs en DbContext

FileNotFoundException: Could not find file 'D:\proyectos\CodigoFuente\Utpl.Integrador\src\applications\cal.admin.Web.Host\bin\Debug\netcoreapp2.2\log4net.config'.

# Herramientas

## seq

Seq creates the visibility you need to quickly identify and diagnose problems in complex applications and microservices.


# Referencias

Crear logs en clases estaticas. 
https://stackoverflow.com/questions/48676152/asp-net-core-web-api-logging-from-a-static-class

