# Blazor
 

Blazor lets you build interactive web UIs using C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries. 

Blazor is a client-side web UI framework similar in nature to JavaScript front-end frameworks like Angular or React. Blazor handles user interactions and renders the necessary UI updates. Blazor isn't based on a request-reply model. User interactions are handled as events that aren't in the context of any particular HTTP request.


## Blazor app hosting models


- Client-side in the browser on WebAssembly.
- Server-side in an ASP.NET Core app.

With the Blazor Server hosting model, the app is executed on the server from within an ASP.NET Core app. UI updates, event handling, and JavaScript calls are handled over a SignalR connection.


One way to understand Blazor Server apps is to understand how it differs from traditional models for rendering UI in ASP.NET Core apps using Razor views or Razor Pages. Both models use the Razor language to describe HTML content, but they significantly differ in how markup is rendered.



## Bootstrap Blazor

- In the Blazor Server app, the root component's host page is defined in the _Host.cshtml file.

- In the Blazor WebAssembly app, the host page is a simple static HTML file under wwwroot/index.html. 


## UI components with Blazor

### Component parameters

### Event handlers

### Data binding

### Child content

### Code-behind


## Pages, routing, and layouts

## State management


## Forms and validation

Blazor supports the sharing of validation logic between both the client and the server. ASP.NET provides pre-built JavaScript implementations of many common server validations. In many cases, the developer still has to write JavaScript to fully implement their app-specific validation logic. The same model types, data annotations, and validation logic can be used on both the server and client.


## Work with data

### REST
Call a web API from ASP.NET Core Blazor
https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-3.1

#### Cross-origin resource sharing (CORS)


Using multiple APIs in Blazor with Azure AD authentication
https://damienbod.com/2020/12/14/using-multiple-apis-in-blazor-with-azure-ad-authentication/

## App configuration


# Framework

A Modular Application Framework for Blazor
Modern, Flexible, and Open Source!
https://www.oqtane.org/

# Referencias

## Manuales, Tutoriales

Blazor is a single-page app framework for building client-side web apps using .NET and WebAssembly. In this workshop we will build a complete Blazor app and learn about the various Blazor framework features along the way.
https://github.com/dotnet-presentations/blazor-workshop


Create A Blazor Server SPA With Dapper
https://www.c-sharpcorner.com/article/create-a-blazor-server-spa-with-dapper/

## Libros

Blazor for ASP.NET Web Forms Developers
https://docs.microsoft.com/en-us/dotnet/architecture/blazor-for-web-forms-developers/
 

## Referencias Codigo, Aplicaciones con codigo

Sample ASP.NET Core 5.0 reference application, powered by Microsoft, demonstrating a layered application architecture with monolithic deployment model
https://github.com/dotnet-architecture/eShopOnWeb


