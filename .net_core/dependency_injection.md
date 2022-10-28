
# Dependency injection

At the heart of the ASP.NET Core dependency injection abstraction is the `IServiceProvider` interface. This interface is actually part of the base class library, in the `System` namespace.


Algunos puntos

Acceder IServiceProvider instance, desde HttpContext 

```
var foo = HttpContext.RequestServices.GetService<IFoo>();
```


Register groups of services with extension methods

The ASP.NET Core framework uses a convention for registering a group of related services. The convention is to use a single Add{GROUP_NAME} extension method to register all of the services required by a framework feature. For example, the AddControllers extension method registers the services required for MVC controllers.


Note: Each services.Add{GROUP_NAME} extension method adds and potentially configures services. For example, AddControllersWithViews adds the services MVC controllers with views require, and AddRazorPages adds the services Razor Pages requires. We recommended that apps follow the naming convention of creating extension methods in the Microsoft.Extensions.DependencyInjection namespace. Creating extension methods in the Microsoft.Extensions.DependencyInjection namespace:

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0



# Autofac

Autofac is an addictive Inversion of Control container for .NET Core, ASP.NET Core, .NET 4.5.1+, Universal Windows apps, and more. 



# Util

Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection
https://github.com/khellang/Scrutor 

# Should injected dependencies be readonly fields or private properties
https://stackoverflow.com/questions/57498480/should-injected-dependencies-be-readonly-fields-or-private-properties
https://medium.com/@farshadjahanmanesh/constructor-dependency-injection-vs-property-dependency-injection-85ab0f0f26b1

# Referencias

Informacion inyeccion dependencias, para diferentes componentes en asp.net core
- Razor views
- Injection in MVC Core
- View components
[ASP.NET Core Dependency Injection Deep Dive](https://joonasw.net/view/aspnet-core-di-deep-dive)
 
 
Dependency Injection With Multiple Implementations Of The Same Interface
https://www.c-sharpcorner.com/article/dependency-injection-with-multiple-implementations-of-the-same-interface/


5 Ways Selecting Injected Instance from Multiple Instances of Same Interface on ASP.NET Core  | DevKimchi
https://devkimchi.com/2020/07/01/5-ways-injecting-multiple-instances-of-same-interface-on-aspnet-core/
