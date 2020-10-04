## Versions

### .net Core 3.0

After .NET Core 3.0 we will not port any more features from .NET Framework. If you are a Web Forms developer and want to build a new application on .NET Core, we would recommend Blazor which provides the closest programming model. If you are a remoting or WCF Server developer and want to build a new application on .NET Core, we would recommend either ASP.NET Core Web APIs or gRPC, which provides cross platform and cross programming language contract based RPCs). If you are a Windows Workflow developer there is an open source port of Workflow to .NET Core.  


Migracion 2.2 => 3.x

If you're using Visual Studio, you need Visual Studio 2019, as Visual Studio 2017 doesn't support .NET Standard 2.1 or .NET Core 3.0.

Migrate from ASP.NET Core 2.2 to 3.0
https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.1&tabs=visual-studio


Entity Framework
The syntax has been changed a little bit in EF Core 3

Why was Relational() extention method removed in .net core 3?
https://stackoverflow.com/questions/59114236/why-was-relational-extention-method-removed-in-net-core-3


Reference:

https://devblogs.microsoft.com/dotnet/net-core-is-the-future-of-net/
 
 


