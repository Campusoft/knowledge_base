
Logging in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1

Logs, xunit
https://stackoverflow.com/questions/46169169/net-core-2-0-configurelogging-xunit-test

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


https://github.com/serilog/serilog-settings-configuration


Algunos ejemplos configuraciones appsettings.json
https://stackoverflow.com/questions/54715142/serilog-not-writing-to-file-net-core-2-2

### Revisiones

 A Serilog event sink that writes to Microsoft Teams 
 

# Trace

Now you need to choose your Trace provider (for example: Application Insights, OpenTrace) and configure to read metrics from your DiagnosticSource.
Application Insights
OpenTrace


# Laboratorio

Activar Logs en DbContext

FileNotFoundException: Could not find file 'D:\proyectos\CodigoFuente\Utpl.Integrador\src\applications\cal.admin.Web.Host\bin\Debug\netcoreapp2.2\log4net.config'.

# Referencias

Crear logs en clases estaticas. 
https://stackoverflow.com/questions/48676152/asp-net-core-web-api-logging-from-a-static-class

