# Security


NWebsec consists of several security libraries for ASP.NET applications. These libraries work together to remove version headers, control cache headers, stop potentially dangerous redirects, and set important security headers
https://docs.nwebsec.com/en/latest/


# Audit

## Implementaciones


Creating Simple Audit Trail With Entity Framework Core
In this article, you will see how to do this inside the SaveChanges() method.
https://www.codeproject.com/Articles/5259677/Creating-Simple-Audit-Trail-With-Entity-Framework

## Entity Framework Core Plus Audit

 
Z.EntityFramework.Plus.EFCore. EF+ Audit easily tracks changes, exclude/include entity or property and auto save audit entries in the database.

https://entityframework-plus.net/ef-core-audit

***Errores***
---------------------
System.ArgumentException: Destination array was not long enough. Check the destination index, length, and the array's lower bounds.
Parameter name: destinationArray
   at System.Array.Copy(Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length, Boolean reliable)
   at System.Collections.Generic.List`1.CopyTo(T[] array, Int32 arrayIndex)
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at Z.EntityFramework.Plus.AuditConfiguration.Clone()

Solucion:
move the call of the audit configuration in a static constructor (called only once)
https://github.com/zzzprojects/EntityFramework-Plus/issues/559

## Audit.NET

With Audit.NET you can generate tracking information about operations being executed. It gathers environmental information such as the caller user id, machine name, method name, exceptions, including execution time and exposing an extensible mechanism to enrich the logs and handle the audit output.

Output extensions are provided to log to JSON Files, Event Log, SQL, MySQL, PostgreSQL, MongoDB, AzureBlob, AzureCosmos, Redis, Elasticsearch, DynamoDB, UDP datagrams and more.

Interaction extensions to audit different systems are provided, such as Entity Framework, MVC, WebAPI, WCF, File System, SignalR and HttpClient.

https://github.com/thepirat000/Audit.NET

***Data providers***

A data provider (or storage sink) contains the logic to handle the audit event output, where you define what to do with the audit logs.

***SqlDataProvider***
- Se guarda en una unica tabla.
- Se puede configurar las columnas especificas para guardar alguna informacion existente en el audit log.
- El audit log, se guarda en JSON en una columna que se configure.
https://github.com/thepirat000/Audit.NET/tree/master/src/Audit.NET.SqlServer#auditnetsqlserver

Extensions

***Audit.WebApi***

ASP.NET MVC Web API Audit Extension for Audit.NET library (An extensible framework to audit executing operations in .NET).

Generate Audit Trails for ASP.NET MVC Web API calls. This library provides a configurable infrastructure to log interactions with your Asp.NET (or Asp.NET Core) Web API.
https://github.com/thepirat000/Audit.NET/tree/master/src/Audit.WebApi#auditwebapi

***Audit.EntityFramework***
Automatically generates Audit Logs for EntityFramework's operations. Supporting EntityFramework and EntityFramework Core

Mode: To indicate the audit operation mode

- Opt-Out: All the entities are tracked by default, except those explicitly ignored. (Default)
- Opt-In: No entity is tracked by default, except those explicitly included.
https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/README.md#settings


***Laboratorio***

Utiliza Clean.Architecture.Solution.Template
dotnet new ca-sln


dotnet new clean-arch -o AuditNet.Lab

# Revisiones / TEMP. (Proyecto BIT)

●	https://www.owasp.org/index.php/Category:OWASP_Application_Security_Verification_Standard_Project
●	https://www.owasp.org/images/5/5e/OWASP-Top-10-2017-es.pdf

