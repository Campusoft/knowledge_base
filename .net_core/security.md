# Security

NWebsec consists of several security libraries for ASP.NET applications. These libraries work together to remove version headers, control cache headers, stop potentially dangerous redirects, and set important security headers
https://docs.nwebsec.com/en/latest/




- NWebsec features can be enabled through middleware or MVC filter attributes.
- Strict-Transport-Security
- X-Content-Type-Options
- X-Download-Options
- X-Frame-Options
- X-Xss-Protection
- Content-Security-Policy
- X-Robots-Tag



Revizar
The NWebsec.SessionSecurity library improves ASP.NET session security by enforcing a strong binding between an authenticated user’s identity and the user’s session identifier.
http://docs.nwebsec.com/projects/SessionSecurity/en/latest/

# Audit

Configure ASP.NET Core to work with proxy servers and load balancers
- Forwarded headers
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-6.0

**_REvisiones_**
The X-Forwarded-\* headers are set by proxies in asp .net core
https://www.iaspnetcore.com/blog/blogpost/5a068d74e42370369cf0b718

## Implementaciones

Creating Simple Audit Trail With Entity Framework Core
In this article, you will see how to do this inside the SaveChanges() method.
https://www.codeproject.com/Articles/5259677/Creating-Simple-Audit-Trail-With-Entity-Framework

## Entity Framework Core Plus Audit

Z.EntityFramework.Plus.EFCore. EF+ Audit easily tracks changes, exclude/include entity or property and auto save audit entries in the database.

https://entityframework-plus.net/ef-core-audit

**_Errores_**

System.ArgumentException: Destination array was not long enough. Check the destination index, length, and the array's lower bounds.
Parameter name: destinationArray
at System.Array.Copy(Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length, Boolean reliable)
at System.Collections.Generic.List`1.CopyTo(T[] array, Int32 arrayIndex) at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
at Z.EntityFramework.Plus.AuditConfiguration.Clone()

Solucion:
move the call of the audit configuration in a static constructor (called only once)
https://github.com/zzzprojects/EntityFramework-Plus/issues/559

## Audit.NET

With Audit.NET you can generate tracking information about operations being executed. It gathers environmental information such as the caller user id, machine name, method name, exceptions, including execution time and exposing an extensible mechanism to enrich the logs and handle the audit output.

Output extensions are provided to log to JSON Files, Event Log, SQL, MySQL, PostgreSQL, MongoDB, AzureBlob, AzureCosmos, Redis, Elasticsearch, DynamoDB, UDP datagrams and more.

Interaction extensions to audit different systems are provided, such as Entity Framework, MVC, WebAPI, WCF, File System, SignalR and HttpClient.

https://github.com/thepirat000/Audit.NET



Campos

- UserName

**_Data providers_**

A data provider (or storage sink) contains the logic to handle the audit event output, where you define what to do with the audit logs.

**_FileDataProvider_**

**_SqlDataProvider_**

- Se guarda en una unica tabla.
- Se puede configurar las columnas especificas para guardar alguna informacion existente en el audit log.
- El audit log, se guarda en JSON en una columna que se configure.


Table constraints
- The table should exists.
- The table should have a single ID column (Unique or Primary key).
- The type of the ID column should be convertible to NVARCHAR.

Script para crear la tabla.
https://github.com/thepirat000/Audit.NET/tree/master/src/Audit.NET.SqlServer#table-constraints

Informacion Audit.NET.SqlServer
https://github.com/thepirat000/Audit.NET/tree/master/src/Audit.NET.SqlServer#auditnetsqlserver


Ejemplo/Labs

Table:

```
CREATE TABLE [AuditEvent]
(
    [EventId] BIGINT IDENTITY(1,1) NOT NULL,
    [InsertedDate] DATETIME NOT NULL DEFAULT(GETUTCDATE()),
    [LastUpdatedDate] DATETIME NULL,
    [JsonData] NVARCHAR(MAX) NOT NULL,
    [EventType] NVARCHAR(100) NOT NULL,
	[User] NVARCHAR(100),
	[CorrelationId] NVARCHAR(100),
	[Entity] NVARCHAR(MAX)
    CONSTRAINT PK_Event PRIMARY KEY (EventId)
)
GO
```

Configuracion Audit.NET.SqlServer:
- Conexiones
- Tabla, y campos.
- Establecer campos en la tabla, con campos personalizados. "CorrelationId"

```
 Audit.Core.Configuration.Setup()
    .UseSqlServer(config => config
        .ConnectionString("Server=(LocalDb)\\MSSQLLocalDB;Database=AuditNet.Labs;Trusted_Connection=True;")
        .Schema("dbo")
        .TableName("AuditEvent")
        .IdColumnName("EventId")
        .JsonColumnName("JsonData")
        .LastUpdatedColumnName("LastUpdatedDate")
        .CustomColumn("EventType", ev => ev.EventType)
        .CustomColumn("User", ev => ev.Environment.UserName)
        //.CustomColumn("User", ev => ev.Environment.DomainName)
        .CustomColumn("CorrelationId", ev => ev.CustomFields.ContainsKey("CorrelationId")? ev.CustomFields["CorrelationId"]:null)
    );

```

Agregar un campo personalizado "CorrelationId"

```
var app = builder.Build();
.
.
.
IHttpContextAccessor ctxAccesor = app.Services.GetRequiredService<IHttpContextAccessor>();
Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
{
  var httpContext = ctxAccesor.HttpContext;
  if (httpContext != null)
	scope.Event.CustomFields["CorrelationId"] = httpContext.TraceIdentifier;
});
```

**_Kafka_**
Apache Kafka Server provider for Audit.NET library (An extensible framework to audit executing operations in .NET).
https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.NET.Kafka/README.md


**_Elasticsearch_**
Elasticsearch provider for Audit.NET library (An extensible framework to audit executing operations in .NET).
https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.NET.ElasticSearch/README.md

Errores

---

ElasticsearchClientException: Request failed to execute. Call: Status code 400 from: PUT /GET.Home.Index/\_create/18bccbbf-962a-4f53-bd60-1c22677c9ddc. ServerError: Type: invalid_index_name_exception Reason: "Invalid index name [GET.Home.Index], must be lowercase"

Relacionado, al problema Invalid index name, must be lowercase
https://github.com/thepirat000/Audit.NET/issues/266

Elasticsearch.Net.ElasticsearchClientException: Request failed to execute. Call: Status code 400 from: PUT /AppDbContext/\_create/99063695-d9f1-4953-897d-0b0777844a85. ServerError: Type: invalid_index_name_exception Reason: "Invalid index name [AppDbContext], must be lowercase"

Establecer en minusculas.

```
mvcOptions.AddAuditFilter(config => config
           ...
          .WithEventType(ctx => $"{ctx.HttpContext.Request.Method.ToLower()}.{(ctx.ActionDescriptor as ControllerActionDescriptor)?.ControllerName.ToLower()}.{(ctx.ActionDescriptor as ControllerActionDescriptor)?.ActionName.ToLower()}")
          ...
```

---

**_Extensions_**

**_Audit.WebApi_**

ASP.NET MVC Web API Audit Extension for Audit.NET library (An extensible framework to audit executing operations in .NET).

Generate Audit Trails for ASP.NET MVC Web API calls. This library provides a configurable infrastructure to log interactions with your Asp.NET (or Asp.NET Core) Web API.
https://github.com/thepirat000/Audit.NET/tree/master/src/Audit.WebApi#auditwebapi

**_Audit.EntityFramework_**

Automatically generates Audit Logs for EntityFramework's operations. Supporting EntityFramework and EntityFramework Core

Mode: To indicate the audit operation mode

- Opt-Out: All the entities are tracked by default, except those explicitly ignored. (Default)
- Opt-In: No entity is tracked by default, except those explicitly included.
  https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/README.md#settings

- Exclude properties (columns)
  https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/README.md#exclude-properties-columns

Configuration (https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/README.md#settings)

- IncludeEntityObjects: To indicate if the output should contain the complete entity object graphs. (Default is false)

**_Custom Actions_**

**_Laboratorio_**

Utiliza Clean.Architecture.Solution.Template
dotnet new ca-sln

dotnet new clean-arch -o AuditNet.Lab

**_Varios_**

Using ASP.NET Idenity User instead of Environment.UserName
https://github.com/thepirat000/Audit.NET/issues/299

# Varios

ASP.NET Core Anti-Forgery Explained
- The CookieToken stored in .AspNetCore.Antiforgery.xxxxxxxx cookie is marked by the framework as samesite=strict and httponly. So if the front end app and API are hosted in different domain, the browser would not send the cookie. Thus the anti-forgery validation would fail.
https://jason-ge.medium.com/asp-net-core-anti-forgery-explained-9549edfae926


# Revisiones / TEMP. (Proyecto BIT)

● https://www.owasp.org/index.php/Category:OWASP_Application_Security_Verification_Standard_Project
● https://www.owasp.org/images/5/5e/OWASP-Top-10-2017-es.pdf
