# REST


# IHttpClientFactory 

An IHttpClientFactory can be registered and used to configure and create HttpClient instances in an app

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0

Typed clients

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients-1

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


## Referencias

Comparing RestSharp and Flurl.Http while consuming a web service in .NET Core
https://jeremylindsayni.wordpress.com/2018/12/27/comparing-restsharp-and-flurl-http-while-consuming-a-web-service-in-net-core/

