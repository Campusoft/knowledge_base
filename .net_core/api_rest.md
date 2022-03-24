# REST


***ApiController attribute***

The [ApiController] attribute can be applied to a controller class to enable the following opinionated, API-specific behaviors:

- Attribute routing requirement
- Automatic HTTP 400 responses
- Binding source parameter inference
- Multipart/form-data request inference
- Problem details for error status codes
https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0#apicontroller-attribute


# IHttpClientFactory 

An IHttpClientFactory can be registered and used to configure and create HttpClient instances in an app

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0

Typed clients
- Provide the same capabilities as named clients without the need to use strings as keys.
- Work with DI and can be injected where required in the app.
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients-1


Outgoing request middleware
- DelegatingHandler

# Upload File API REST

Sending Files & JSON using multipart/form-data
-  Large Image as Base64 using JSON is not a good idea. It will take a lot of memory & time for converting back to the actual image for copying on the Server.
https://dottutorials.net/dotnet-core-web-api-multipart-form-data-upload-file/


Upload files in ASP.NET Core
- Validation
- File signature validation
https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0


# API versioning

# CORS 

ASP.NET Core API - Allow CORS requests from any origin and with credentials
https://jasonwatmore.com/post/2020/05/20/aspnet-core-api-allow-cors-requests-from-any-origin-and-with-credentials



# Client


***Nswag***

(Generar CLient)

- Genera dto.
	- Con DataAnnotations
- Generar multiples clientes, metodos
	- desde pathSegments, u operationId
- Utiliza HttpClient
	- En el constructor.
	- En una clase base CreateHttpClientAsync
- Partial class
  - Permite generar varios tipos de estilos de Clases. (Modelos)
  - POCO.
  - Record
  - Inpc.
  - RaisePropertyChanged	
- Mejoras
  - Problemas con la generación de Enumerables.


***Flurl***

Informacion
Flurl is a modern, fluent, asynchronous, testable, portable, buzzword-laden URL builder and HTTP client library for .NET.



api.postcodes.io – no authentication required, uses GET and POST verbs
api.nasa.gov – authentication via an API key passed in the query string
api.github.com – Basic Authentication required to access private repo information


 
**refit**
  
The automatic type-safe REST library for .NET Core, Xamarin and .NET. Heavily inspired by Square's Retrofit library, Refit turns your REST API into a live interface. 
https://github.com/reactiveui/refit 

- Refit 6 makes System.Text.Json the default JSON serializer.


***RestEase***
To use it, you define an interface which represents the endpoint you wish to communicate with (more on that in a bit), where methods on that interface correspond to requests that can be made on it. RestEase will then generate an implementation of that interface for you, and by calling the methods you defined, the appropriate requests will be made
- Using RestEase.SourceGenerator
The advantages of using a Source Generator are:  "You will need to be using the .NET 5 SDK (or higher) to make use of source generators. "
-    Compile-time error checking. Find out if your RestEase interface has an error at compile-time, rather than runtime.
-    Supports platforms which don't support System.Reflection.Emit, such as iOS and .NET Native.
-    Faster: no need to generate implementations at runtime.
https://github.com/canton7/RestEase


## Referencias

Comparing RestSharp and Flurl.Http while consuming a web service in .NET Core
https://jeremylindsayni.wordpress.com/2018/12/27/comparing-restsharp-and-flurl-http-while-consuming-a-web-service-in-net-core/

