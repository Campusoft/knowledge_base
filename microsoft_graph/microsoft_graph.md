
# Microsoft Graph

Microsoft Graph is the gateway to data and intelligence in Microsoft 365. It provides a unified programmability model that you can use to access the tremendous amount of data in Office 365, Windows 10, and Enterprise Mobility + Security. Use the wealth of data in Microsoft Graph to build apps for organizations and consumers that interact with millions of users.

Microsoft Graph is a RESTful web API that enables you to access Microsoft Cloud service resources. 


Call a REST API method

```
{HTTP method} https://graph.microsoft.com/{version}/{resource}?{query-parameters}
```

The components of a request include:

- {HTTP method} - The HTTP method used on the request to Microsoft Graph.
- {version} - The version of the Microsoft Graph API your application is using.
- {resource} - The resource in Microsoft Graph that you're referencing.
- {query-parameters} - Optional OData query options or REST method parameters that customize the response.



Data is retrieved from Microsoft Graph through a REST API or using one of the various native SDKs provided by Microsoft.


## change notifications (webhooks) & track changes (delta query) i

What types of entities support change notifications in Microsoft Graph?
Not all entities support change notifications in Microsoft Graph



## SDK - Client


Create a Microsoft Graph client
https://docs.microsoft.com/en-us/graph/sdks/create-client?view=graph-rest-1.0&tabs=CS


## Manuales

Build .NET Core apps with Microsoft Graph

- Register the app in the portal. https://aad.portal.azure.com/
- Permisos

https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core?view=graph-rest-1.0


[Informacion para consumir API. (Autentificacion, etc)](access.md)


Announcing “30 Days of Microsoft Graph” Blog Series. (Excelente enlace)

Throughout the month of November 2018, we are publishing daily articles (30 total) that aim to introduce developers to Microsoft Graph.  We’ll have content that covers 0-level to 200-level topics.  Each post should take you 5-15 mins to read and try out the sample exercises.  No prior knowledge of Microsoft Graph is required.  We hope that beginners will quickly pick up the content and that experts will also learn a few new things.

https://developer.microsoft.com/en-us/graph/blogs/announcing-30-days-of-microsoft-graph-blog-series/#



## Implementar


Build ASP.NET Core MVC apps with Microsoft Graph
https://docs.microsoft.com/en-us/graph/tutorials/aspnet-core


Quickstart: Acquire a token and call Microsoft Graph API using console app's identity.
demonstrates how a .NET Core console application can get an access token to call the Microsoft Graph API and display a list of users in the directory.
https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-netcore-daemon
