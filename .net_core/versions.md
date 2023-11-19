# Versions


# .NET 8.0

- Improvements in System.Text.Json serialization

Native AOT

.NET 8 brings improvements to the native ahead-of-time (AOT) compilation feature that was first introduced in .NET 7. Publishing an application as native AOT generates a self-contained version of the app that doesn’t require a runtime as everything is included in a single file.

The latest improvements to native AOT apps on Linux systems have resulted in significantly reduced application sizes.


.NET container images

.NET 8 also makes a few changes to the way .NET container images work. First, Debian 12 (Bookworm) is now the default Linux distribution in the container images.


.NET 8: Why .NET 8 preview doesn’t show up in Visual Studio 2022 ?
https://anthonygiretti.com/2023/06/10/net-8-why-net-8-preview-doesnt-show-up-in-visual-studio-2022/

.NET 7

Publish to a container

Containers are one of the easiest ways to distribute and run a wide variety of applications and services in the cloud. Container images are now a supported output type of the .NET SDK, and you can create containerized versions of your applications using dotnet publish.

# .net Core 6

.NET 6 is supported with Visual Studio 2022 and Visual Studio 2022 for Mac. It is not supported in Visual Studio 2019

Unified platform across browser, cloud, desktop, IoT, and mobile apps, all using the same .NET Libraries and the ability to share code easily.

Minimal APIs introduced in ASP.NET Core to simplify the getting started experience and improve the performance of HTTP services.

Hot Reload enables you to skip rebuilding and restarting your app to view a new change — while your app is running — supported in Visual Studio 2022 and from the .NET CLI, for C# and Visual Basic.


# .net Core 3.0

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
 
 

# .NET Standard 


.NET Standard exposes platform-specific APIs. Your code might compile without errors and appear to be portable to any platform even if it isn't portable. When it runs on a platform that doesn't have an implementation for a given API, you get run-time errors.

We recommend you target .NET Standard in the following scenarios:

- Use netstandard2.0 to share code between .NET Framework and all other implementations of .NET.
- Use netstandard2.1 to share code between Mono, Xamarin, and .NET Core 3.x.


# Migrate from ASP.NET Core 5.0 to 6.0 

Migrate from ASP.NET Core 5.0 to 6.0
https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60?view=aspnetcore-7.0&tabs=visual-studio

# Migraciones a .net core

OneService Journey to .NET 6
- 29% reduction in infrastructure costs.
- 30% CPU improvement (on average) for migrated services.
- 8-27% improvement in P95 latency for primary APIs.
- Reduced technical debt and can now easily upgrade to annual .NET releases.
- Happier and more productive team.
https://devblogs.microsoft.com/dotnet/one-service-journey-to-dotnet-6/


Incremental ASP.NET to ASP.NET Core Migration
- 
https://devblogs.microsoft.com/dotnet/incremental-asp-net-to-asp-net-core-migration/