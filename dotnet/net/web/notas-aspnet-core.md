# Notas de ASP.NET Core

## Globalization and localization in ASP.NET Core

***Revision***
Configure portable object localization in ASP.NET Core

## Validaciones


IModelValidator 

### FluentValidation

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

## Release

ASP.Net Core Reverse Proxy with different root
https://stackoverflow.com/questions/45311393/asp-net-core-reverse-proxy-with-different-root


.NET Core hosted on subdirectories in Nginx
https://www.billbogaiv.com/posts/net-core-hosted-on-subdirectories-in-nginx

## Files


IFileProvider

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/file-providers?view=aspnetcore-5.0#physical-file-provider

## FileProviders

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

## Kestrel

Kestrel is a cross-platform web server for ASP.NET Core.
Kestrel supports the following scenarios:

- HTTPS
- HTTP/2 (except on macOS†)
- Opaque upgrade used to enable WebSockets
- Unix sockets for high performance behind Nginx

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-6.0
