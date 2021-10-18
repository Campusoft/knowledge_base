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


# Proveedores


## Nlog

Nlog. (Configuracion net core)
https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-2

Configuraciones de rotación de archivos
https://github.com/nlog/NLog/wiki/File-target#size-based-file-archival

COnfiguracion de Nlog en console. 
https://github.com/NLog/NLog.Extensions.Logging/wiki/Getting-started-with-.NET-Core-2---Console-application

## Serilog 

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


***Serilog integration for ASP.NET Core***
https://github.com/serilog/serilog-aspnetcore
 
### Enrichers

Enrichers are simple components that add, remove or modify the properties attached to a log event.


Enrich.WithProcessId() and/or .WithProcessName() 
https://github.com/serilog/serilog-enrichers-process

Enriches Serilog events with information from the process environment.
https://github.com/serilog/serilog-enrichers-environment
https://github.com/serilog/serilog-enrichers-environment#included-enrichers


### Output templates

https://github.com/serilog/serilog/wiki/Configuration-Basics#output-templates

### Sinks

***Serilog.Sinks.File***
Write Serilog events to files in text and JSON formats, optionally rolling on time or size 
https://github.com/serilog/serilog-sinks-file#rolling-policies


***Serilog.Formatting.Compact***

A simple, compact JSON-based event format for Serilog. CompactJsonFormatter significantly reduces the byte count of small log events when compared with Serilog's default JsonFormatter, while remaining human-readable

https://github.com/serilog/serilog-formatting-compact




### Tools

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

### Referencias

Serilog Best Practices
https://benfoster.io/blog/serilog-best-practices/

### Revisiones

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

