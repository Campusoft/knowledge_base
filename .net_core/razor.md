# Razor 

Razor es una sintaxis ASP.NET que se usa para crear páginas web dinámicas con C#. Cuando un servidor lee una página de Razor, el código de C# se ejecuta antes de representar el HTML. 


## Reusable UI using the Razor class library 

El proyecto que tiene los objetos UI que seran compartidos, la estructura del proyecto  debe ser similar a:
```
 <Project Sdk="Microsoft.NET.Sdk.Razor">
 
   <PropertyGroup>
     <TargetFramework>netstandard2.0</TargetFramework>   </PropertyGroup>
 
   <ItemGroup>
     <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.2.0" />   
	</ItemGroup> 
  </Project>
```

Tiene que tener Sdk="Microsoft.NET.Sdk.Razor" en elemento Project



Referencias
Create reusable UI using the Razor class library project in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-2.2&tabs=visual-studio

RCL 
.NET Core 3.0, RCL can include static assets without much effort

https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-3.0&tabs=visual-studio#create-an-rcl-with-static-assets

Example:
Project: Campusoft.Core.Application

File folder wwwroot:  view-resources/Views/Setting/_EditSetting.js

Use file other project:


<script src="~/_content/Campusoft.Core.Application/view-resources/Views/Setting/_EditSetting.js"></script>
    <script>




**Referencias**

Including Static Resources In Razor Class Libraries In ASP.NET Core
(Manual complete)
https://www.mikesdotnetting.com/article/330/including-static-resources-in-razor-class-libraries-in-asp-net-core


# RazorPage

Pages

The key difference between Razor Pages implementation of the MVC pattern and ASP.NET Core MVC is that Razor Pages uses the Page Controller pattern instead of the Front Controller pattern.

Request Processing link

Request processing in a PageModel is performed within handler methods which are analogous to Action methods on an ASP.NET MVC controller. By convention, handler method selection is based on matching the HTTP verb that was used for the request with the name of the handler method using the pattern On<verb> with Async appended optionally to denote that the method is intended to run asynchronously.
https://www.learnrazorpages.com/razor-pages/pagemodel#request-processing

## Named Handler Methods link


# Referencias
Razor Pages includes a feature called "named handler methods". This feature enables you to specify multiple methods that can be executed for a single verb

Manuales
https://www.learnrazorpages.com/

