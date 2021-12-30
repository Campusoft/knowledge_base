
# Dependency injection

At the heart of the ASP.NET Core dependency injection abstraction is the `IServiceProvider` interface. This interface is actually part of the base class library, in the `System` namespace.


Algunos puntos

Acceder IServiceProvider instance, desde HttpContext 

```
var foo = HttpContext.RequestServices.GetService<IFoo>();
```



# Autofac

Autofac is an addictive Inversion of Control container for .NET Core, ASP.NET Core, .NET 4.5.1+, Universal Windows apps, and more. 

# Util

Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection
https://github.com/khellang/Scrutor 

# Referencias

Informacion inyeccion dependencias, para diferentes componentes en asp.net core
[ASP.NET Core Dependency Injection Deep Dive](https://joonasw.net/view/aspnet-core-di-deep-dive)
 
 
Dependency Injection With Multiple Implementations Of The Same Interface
https://www.c-sharpcorner.com/article/dependency-injection-with-multiple-implementations-of-the-same-interface/


5 Ways Selecting Injected Instance from Multiple Instances of Same Interface on ASP.NET Core  | DevKimchi
https://devkimchi.com/2020/07/01/5-ways-injecting-multiple-instances-of-same-interface-on-aspnet-core/
