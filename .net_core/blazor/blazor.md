# Blazor
 

Blazor lets you build interactive web UIs using C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries. 

Blazor is a client-side web UI framework similar in nature to JavaScript front-end frameworks like Angular or React. Blazor handles user interactions and renders the necessary UI updates. Blazor isn't based on a request-reply model. User interactions are handled as events that aren't in the context of any particular HTTP request.


# Blazor app hosting models


- Client-side in the browser on WebAssembly.
- Server-side in an ASP.NET Core app.

With the Blazor Server hosting model, the app is executed on the server from within an ASP.NET Core app. UI updates, event handling, and JavaScript calls are handled over a SignalR connection.


One way to understand Blazor Server apps is to understand how it differs from traditional models for rendering UI in ASP.NET Core apps using Razor views or Razor Pages. Both models use the Razor language to describe HTML content, but they significantly differ in how markup is rendered.

Client/client-side
- The Client project of a hosted Blazor WebAssembly app.
- A Blazor WebAssembly app.
- Blazor script start configuration is found in the wwwroot/index.html file.
- The Program file is Program.cs.

Server/server-side
- The Server project of a hosted Blazor WebAssembly app.
- A Blazor Server app. Blazor script start configuration is found in Pages/_Host.cshtml.
- The Program file is Program.cs.



# Bootstrap Blazor

- In the Blazor Server app, the root component's host page is defined in the _Host.cshtml file.

- In the Blazor WebAssembly app, the host page is a simple static HTML file under wwwroot/index.html. 


# Blazor Server 

Provides support for hosting Razor components on the server in an ASP.NET Core app. UI updates are handled over a SignalR connection.


# Razor components

Components are implemented using a combination of C# and HTML markup in Razor component files with the .razor file extension.

By default, ComponentBase is the base class for components described by Razor component files. ComponentBase implements the lowest abstraction of components, the IComponent interface. ComponentBase defines component properties and methods for basic functionality, for example, to process a set of built-in component lifecycle events.


Blazor apps are built using components. A component is a self-contained chunk of user interface (UI), such as a page, dialog, or form. A component includes HTML markup and the processing logic required to inject data or respond to UI events. Components are flexible and lightweight. They can be nested, reused, and shared among projects.


The OnInitialized and OnInitializedAsync methods are used to initialize the component. A component is typically initialized after it’s first rendered. After a component is initialized, it may be rendered multiple times before it’s eventually disposed


**Child content**

Razor components can capture their child content as a RenderFragment and render that content as part of the component rendering. To capture child content, define a component parameter of type RenderFragment and name it ChildContent

```
<h1>Component with child content</h1>

<div>@ChildContent</div>

@code {
		
		[Parameter]
		public RenderFragment ChildContent { get; set; }
	
	 }
```


**Razor component lifecycle**

- SetParametersAsync.
- OnInitialized{Async}.
- OnParametersSet{Async}.

**Generic Type**

The @typeparam directive declares a generic type parameter for the generated component class:

```
@typeparam TEntity where TEntity : IEntity
```

Example:

```
@typeparam TExample

@if (ExampleList is not null)
{
    <ul>
        @foreach (var item in ExampleList)
        {
            <li>@item</li>
        }
    </ul>
}

@code {
    [Parameter]
    public IEnumerable<TExample>? ExampleList{ get; set; }
}
```


**@key directive **

The @key directive allows instructing Blazor to use a specific key to compare elements instead of using the index. Blazor will compare the existing items with the new ones using the value of the key. This way it will better detect additions/modifications/deletions.

Optimizing Blazor performance using the @key directive
- Posee una animacion, del efecto a utilizar @key o no, en los listados
https://www.meziantou.net/optimizing-blazor-performance-using-the-key-directive.htm

Retain element, component, and model relationships in ASP.NET Core Blazor
https://learn.microsoft.com/en-us/aspnet/core/blazor/components/element-component-model-relationships?view=aspnetcore-7.0


**templated components**

Templated components are components that receive one or more UI templates as parameters, which can be utilized in the rendering logic of the component.



## Pages, routing, and layouts

- In Blazor, a page is a top-level view that can be reached via a URL


To create a page in Blazor, create a component and add the @page Razor directive to specify the route for the component. The @page directive takes a single parameter, which is the route template to add to that component.

```
@page "/counter"

```

A Razor page is a self-contained view. We can include both the HTML and Razor view mark-up, and also any C# methods required for events etc. 

Finally, the @code section of the page is declared. This is where we write our properties / methods, event handlers, or whatever else we need. 

**Blazor layout**

Any content you intend to act as a layout template for pages must descend from the LayoutComponentBase class. To indicate where you want the content of your page to appear you simply output the contents of the Body property.

**Razor directives**

Razor directives are component markup used to add C# inline with HTML. With directives, developers can define single statements, methods, or larger code blocks.

- The @page directive: This directive provides a route template to Blazor. 
- The @code directive: This directive declares that the text in the following block is C# code. 

## Data binding

Razor data binding

Within Razor components, you can data bind HTML elements to C# fields, properties, and Razor expression values. Data binding allows two-way synchronization between HTML and Microsoft .NET.

Data is pushed from HTML to .NET when a component is rendered. Components render themselves after event-handler code executes, which is why property updates are reflected in the UI immediately after an event handler is triggered.

You can use @bind markup to bind a C# variable to an HTML object. You define the C# variable by name as a string in the HTML.


Data binding and events
https://learn.microsoft.com/en-us/training/modules/build-blazor-webassembly-visual-studio-code/6-csharp-razor-binding



A good place to call the service and obtain data is in the OnInitializedAsync method. This event fires when the component's initialization is complete and it has received initial parameters but before the page is rendered and displayed to the user. 


## Component parameters


Share data in Blazor applications
https://learn.microsoft.com/en-us/training/modules/interact-with-data-blazor-web-apps/6-share-data-in-blazor-applications



## Event handlers


In Blazor, you can register handlers for DOM UI events directly using directive attributes of the form @on{event}. The {event} placeholder represents the name of the event. For example, you can listen for button clicks like this:

```
<button @onclick="OnClick">Click me!</button>

@code {
	void OnClick()
	{
		Console.WriteLine("The button was clicked!");
	}
}
```

## Communication between Blazor Components

Communication between Blazor Components using EventCallback

https://www.ezzylearning.net/tutorial/communication-between-blazor-components-using-eventcallback


## Blazor CascadingValue Component

Blazor has a built-in component called CascadingValue. We can pass a value to this component. This value is then cascaded down its component tree to all of its descendants. 
https://www.pragimtech.com/blog/blazor/blazor-cascading-values-parameters/



ASP.NET Core Blazor cascading values and parameters
https://learn.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-7.0

## Code-behind


## State management

For transient data that the user is actively creating, a commonly used storage location is the browser's localStorage and sessionStorage collections:

- localStorage is scoped to the browser's window. If the user reloads the page or closes and reopens the browser, the state persists. If the user opens multiple browser tabs, the state is shared across the tabs. Data persists in localStorage until explicitly cleared.

- sessionStorage is scoped to the browser tab. If the user reloads the tab, the state persists. If the user closes the tab or the browser, the state is lost. If the user opens multiple browser tabs, each tab has its own independent version of the data.

Generally, sessionStorage is safer to use. sessionStorage avoids the risk that a user opens multiple tabs and encounters the following:

- Bugs in state storage across tabs.
- Confusing behavior when a tab overwrites the state of other tabs.

**ProtectedSessionStorage**



**ProtectedLocalStorage**

- The data isn't stored in plain text but rather is protected using ASP.NET Core Data Protection. 
- ProtectedLocalStorage automatically serializes and deserializes JSON data to store complex state objects.




**Referencias**

ASP.NET Core Blazor state management
- Browser storage

https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-7.0&pivots=server


# Forms and validation

Blazor supports the sharing of validation logic between both the client and the server. ASP.NET provides pre-built JavaScript implementations of many common server validations. In many cases, the developer still has to write JavaScript to fully implement their app-specific validation logic. The same model types, data annotations, and validation logic can be used on both the server and client.


Blazor provides a set of input components. The input components handle binding field data to a model and validating the user input when the form is submitted.

Input component Rendered HTML element
- Component: InputCheckbox. Rendered HTML element: <input type="checkbox">
- Component: InputDate. Rendered HTML element: <input type="date">
- Component: InputNumber. Rendered HTML element: <input type="number">
- Component: InputSelect. Rendered HTML element: <select>
- Component: InputText. Rendered HTML element: <input>
- Component: InputTextArea. Rendered HTML element: <textarea>

The EditForm component wraps these input components and orchestrates the validation process through an EditContext. When creating an EditForm, you specify what model instance to bind to using the Model parameter. 



Here we have used the EditForm component of Blazor. The 4 important attributes of the EditForm component are:

- Model – is used to provide the EditForm component with the object that the form is used to edit and validate.
- OnValidSubmit – is used to call an event which is triggered when the form is submitted and the form data passes validation.
- OnInvalidSubmit – is used to call an event which is triggered when the form is submitted and the form data fails validation.
- OnSubmit – is used to call an event which is triggered when the form is submitted and before validations are performed.


blazor provides 3 Validation components to perform data validations, and show Validation messages. These are:

- DataAnnotationsValidator	This component integrates the validation attributes applied to Model Class i.e. Data Annotations into the Blazor Validation system.
- ValidationMessage	It displays validation error messages for a single property.
- ValidationSummary	It displays validation error messages for the entire form.

![image](https://github.com/Campusoft/Campusoft.Generate/assets/222181/80309537-fa50-4abf-8637-7482d232b826)


```
<EditForm Model="@Model" OnValidSubmit="@Submit">
    <DataAnnotationsValidator />
    <ValidationSummary />
    <InputText @bind-Value="Model!.Id" />
    <button type="submit">Submit</button>
</EditForm>
```

ASP.NET Core Blazor forms and input components
- An EditForm component bound to an object or model that can use data annotations
- OnSubmit is replaced with OnValidSubmit, which processes assigned event handler if the form is valid when submitted by the user.
https://learn.microsoft.com/en-us/aspnet/core/blazor/forms-and-input-components?view=aspnetcore-7.0



Blazor forms and validation
- Blazor Form Components
https://www.yogihosting.com/blazor-forms-validation/



# Routing 


**Navigation**

Blazor provides a NavigationManager service that can be used to:
- Get the current browser address
- Get the base address
- Trigger navigations
- Get notified when the address changes

To navigate to a different address, use the NavigateTo method:

```
@page "/"
@inject NavigationManager NavigationManager

<button @onclick="Navigate">Navigate</button>

@code {
	void Navigate() {
		NavigationManager.NavigateTo("counter");
	}
}
```


# REST
Call a web API from ASP.NET Core Blazor
https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-3.1

## Cross-origin resource sharing (CORS)


Using multiple APIs in Blazor with Azure AD authentication
https://damienbod.com/2020/12/14/using-multiple-apis-in-blazor-with-azure-ad-authentication/


# Handle errors


Razor components with server interactivity enabled are stateful on the server. While users interact with the component on the server, they maintain a connection to the server known as a circuit. The circuit holds active component instances, plus many other aspects of state, such as:

- The most recent rendered output of components.
- The current set of event-handling delegates that could be triggered by client-side events.

If a user opens the app in multiple browser tabs, the user creates multiple independent circuits.

Blazor treats most unhandled exceptions as fatal to the circuit where they occur. If a circuit is terminated due to an unhandled exception, the user can only continue to interact with the app by reloading the page to create a new circuit. 


https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/handle-errors?view=aspnetcore-7.0#detailed-circuit-errors



# JavaScript interoperability (JS interop)


Blazor provides an injectable service that implements the IJSRuntime interface. Blazor implements this interface so that we can invoke any JavaScript function from .NET code. To do this the service provides two main methods: InvokeAsync() and InvokeVoidAsync().


- InvokeVoidAsync. We are calling the InvokeVoidAsync() method which is intended for invocations when no results are returned from the JavaScript function. The first parameter is the function identifier, which simply specifies which JavaScript function you intend to call. The function identifier is relative to the global scope, meaning the window object in JavaScript. After the function identifier, we can add as many parameters as needed to match the expected parameters in the JavaScript function itself. 

- InvokeAsync. So, what if we need to get a result back from the JavaScript function? In that case we call the generic InvokeAsync() method, which takes the same form of parameters but returns a result.


```
@page "/javascript"

@inject IJSRuntime JsRuntime

<h3>Javascript</h3>

<button class="btn btn-primary" @onclick="Alert">Alert Javascript</button>

@code {
     
    private async Task Alert(MouseEventArgs e)
    {
        await JsRuntime.InvokeVoidAsync("alert", "C# says hello");
 
    }
}
```


**JavaScript Isolation in Blazor Components**

JS isolation is based on standard ES modules. ES modules can be loaded on-demand using let mymodule = import("path") and the functions they expose are only accessible from the returned object. 
https://www.meziantou.net/javascript-isolation-in-blazor-components.htm

# Blazor authentication and authorization


## Authentication

Blazor uses the existing ASP.NET Core authentication mechanisms to establish the user's identity. The exact mechanism depends on how the Blazor app is hosted, server-side or client-side.

Server-side Blazor operates over a SignalR connection with the client. Authentication in SignalR-based apps is handled when the connection is established. Authentication can be based on a cookie or some other bearer token, but authentication is managed via the SignalR hub and entirely within the circuit.


## Authorization


- The AuthorizeView component supports role-based or policy-based authorization.

 

# App configuration


# Framework

A Modular Application Framework for Blazor
Modern, Flexible, and Open Source!
https://www.oqtane.org/


# Blazor Hybrid

Hybrid apps use a blend of native and web technologies. A Blazor Hybrid app uses Blazor in a native client app. Razor components run natively in the .NET process and render web UI to an embedded Web View control using a local interop channel. WebAssembly isn't used in Hybrid apps.


# IDE


Visual Studio 2022 version 17.9 Preview 1
- We have added the ability to scaffold views in .NET 8 Blazor web projects. This includes scaffolding views that use Entity Framework for CRUD (Create-Read-Update-Delete) operations.



# Referencias



## Create components



Blazor Toast Notifications using only C#, HTML and CSS
- In this post, I’m going to show you how to build toast notifications for your Blazor/Razor Components applications.
https://chrissainty.com/blazor-toast-notifications-using-only-csharp-html-css/


Modal Dialog component with Bootstrap in Blazor
https://steven-giesel.com/blogPost/5fc5b957-d62e-40e6-b0c4-f2a0df5c8aa1



## Manuales, Tutoriales

Blazor is a single-page app framework for building client-side web apps using .NET and WebAssembly. In this workshop we will build a complete Blazor app and learn about the various Blazor framework features along the way.
https://github.com/dotnet-presentations/blazor-workshop


Create A Blazor Server SPA With Dapper
https://www.c-sharpcorner.com/article/create-a-blazor-server-spa-with-dapper/


Blazor tutorial
- Contiene varias paginas, que cubren varios temas blazor
https://www.pragimtech.com/blog/blazor/what-is-blazor/

## Training

Blazor - app building workshop
https://github.com/dotnet-presentations/blazor-workshop/



Building Blazor Server Apps with Clean Architecture
- This post is about implementing the clean architecture in Blazor server apps using CQRS, Mediatr, Entity Framework Core, and repository patterns.  
https://www.ezzylearning.net/tutorial/building-blazor-server-apps-with-clean-architecture



Blazor Server Series

- Blazor Server – Basics Part 1. https://blog.matrixpost.net/blazor-server-basics-part-i/
- Blazor Server – Basics Part 2. https://blog.matrixpost.net/blazor-server-basics-part-ii/
- Blazor Server – Basics Part 3 – Custom Layout. https://blog.matrixpost.net/blazor-server-basics-part-iii-custom-layout/
- Blazor Server – Basics Part 4 – Program.cs File. https://blog.matrixpost.net/blazor-server-basics-part-iv-program-cs-file/
- Blazor Server – Basics Part 5 – Authentication and Authorization. https://blog.matrixpost.net/blazor-server-basics-part-v-authentication-and-authorization/
- Blazor Server – Basics Part 6 – Query the on-premise Active Directory. https://blog.matrixpost.net/blazor-server-basics-part-vi-query-the-on-premise-active-directory/
- Blazor Server – Basics Part 7 – C# Events, Delegates and the EventCallback Class. https://blog.matrixpost.net/blazor-server-basics-part-vii-c-events-and-delegates/
- Blazor Server – Basics Part 8 – JavaScript interoperability (JS interop). https://blog.matrixpost.net/blazor-server-basics-part-viii-javascript-interoperability-js-interop/
- Blazor Server – Basics Part 9 – Responsive Tags and Chips. https://blog.matrixpost.net/blazor-server-basics-part-ix-responsive-tags-and-chips/
- Blazor Server – Basics Part 10 – MS SQL Server Access and Data Binding. https://blog.matrixpost.net/blazor-server-basics-part-10-ms-sql-server-access-and-data-binding/
- Blazor Server – Basics Part 11 – Create a Native Blazor UI Toggle Switch Component. https://blog.matrixpost.net/blazor-server-basics-part-11-native-blazor-toggle-switch-by-using-the-eventcallback-class-and-css/
- Blazor Server – Basics Part 12 – Create a Native Blazor UI Toggle Button Component. https://blog.matrixpost.net/blazor-server-basics-part-12-create-a-native-blazor-ui-toggle-button-component/


## Libros

Blazor for ASP.NET Web Forms Developers
https://docs.microsoft.com/en-us/dotnet/architecture/blazor-for-web-forms-developers/
 

## Referencias Codigo, Aplicaciones con codigo

Sample ASP.NET Core 7.0 reference application, powered by Microsoft, demonstrating a layered application architecture with monolithic deployment model. Download the eBook PDF from docs folder.
- components: Toast, spinner
- use modal (crud)
https://github.com/dotnet-architecture/eShopOnWeb



