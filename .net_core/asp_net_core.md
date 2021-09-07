# asp net core

##Bootstrap  .net Core

ASP.NET Core web applications are configured through a pair of methods in the Startup.cs class on the root folder of the application.

The ConfigureServices method introduced in ASP.NET Core allows for the various ASP.NET Core framework services to be configured for the framework's built-in dependency injection container.


The Configure method introduces the concept of the HTTP pipeline to ASP.NET Core. In this method, we declare from top to bottom the Middleware that will handle every request sent to our application. Most of these features in the default configuration were scattered across the web forms configuration files and are now in one place for ease of reference.  


##templating 

 When the .razor file is compiled, the rendering logic is captured in a structured way in a .NET class. 
 
## Binder

Custom Model Binding in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-2.2
 
 
## View components

View components are similar to partial views, but they're much more powerful. View components don't use model binding, and only depend on the data provided when calling into it

A view component consists of two parts: the class (typically derived from ViewComponent) and the result it returns (typically a view).

View components in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-5.0#view-components 