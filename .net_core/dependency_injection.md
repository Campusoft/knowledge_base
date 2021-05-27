
# Dependency injection

At the heart of the ASP.NET Core dependency injection abstraction is the `IServiceProvider` interface. This interface is actually part of the base class library, in the `System` namespace.


Algunos puntos

Acceder IServiceProvider instance, desde HttpContext 

```
var foo = HttpContext.RequestServices.GetService<IFoo>();
```



## Autofac

Autofac is an addictive Inversion of Control container for .NET Core, ASP.NET Core, .NET 4.5.1+, Universal Windows apps, and more. 

 

## Referencias

Informacion inyeccion dependencias, para diferentes componentes en asp.net core
[ASP.NET Core Dependency Injection Deep Dive](https://joonasw.net/view/aspnet-core-di-deep-dive)
 
 


