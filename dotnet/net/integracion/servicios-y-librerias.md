# Servicios y bibliotecas de integración

## Emails

Envio correos

The easiest way to send email from .NET and .NET Core. Use Razor for email templates and send using SendGrid, MailGun, SMTP and more.
https://github.com/lukencode/FluentEmail

 A cross-platform .NET library for IMAP, POP3, and SMTP. 
https://github.com/jstedfast/MailKit


### MailKit

ASP.NET Core 3.1 - Send Emails via SMTP with MailKit
- gmail
- office 365
https://jasonwatmore.com/post/2020/07/15/aspnet-core-3-send-emails-via-smtp-with-mailkit

How can I create a message with attachments
https://github.com/jstedfast/MailKit/blob/master/FAQ.md#CreateAttachments

### Herramientas

SMTP para probar envíos de correos electrónicos, el cual registra una petición de envío de correo 
electrónico sin llegar a enviarlo realmente. Existen múltiples herramientas de código abierto en este 
ámbito, incluyendo fake-smtp-server, el cual representa correos electrónicos dentro de una interfaz 
de usuario web para pruebas visuales, y mountebank, que expone los correos electrónicos enviados 
a través de una API REST para realizar pruebas de integración. Recomendamos explorar esta técnica 
para reducir el riesgo y mejorar la eficiencia de las pruebas.

## LDAP

Utiliza ldap4net

https://github.com/flamencist/ldap4net

Online LDAP Test Server.
(Servidor LDAP para realizar pruebas de conexion)
https://www.forumsys.com/tutorials/integration-how-to/ldap/online-ldap-test-server/

Atributos
- cn CN=Guy Thomas. En realidad, este atributo LDAP está conformado por givenName unido con SN. 
- dc Se refiere al componente del dominio, ya sea un componente, una etiqueta o un nombre de dominio DNS. Ejemplo: DC=cp, DC=com
- givenName Primer nombre.
- SN SN = Thomas. Esto será referido como apellido. 
- uid
- mail
 
***Filtros buscar usuarios***

Utilizar  userPrincipalName
(&(objectClass=user)(userPrincipalName=nombre-usuario))
 
Utiizar sAMAccountName
(&(objectClass=user)(sAMAccountName={nombre-usuario}))
 
Uttilizar uid
(&(uid=nombre-usuario))
 

Autenticación de usuarios con registros LDAP en un bosque de Microsoft Active Directory
- menciona que es mejor utilizar userPrincipalName, que el sAMAccountName
https://www.ibm.com/docs/es/was/9.0.5?topic=umada-authenticating-users-ldap-registries-in-microsoft-active-directory-forest


Active Directory Explorer
Active Directory Explorer (AD Explorer) is an advanced Active Directory (AD) viewer and editor. You can use AD Explorer to easily navigate an AD database, define favorite locations, view object properties and attributes without having to open dialog boxes, edit permissions, view an object's schema, and execute sophisticated searches that you can save and re-execute.
https://docs.microsoft.com/en-us/sysinternals/downloads/adexplorer

## Storage / Cloud

Download Files from Azure with .NET Core Web API and Blazor WebAssembly
https://code-maze.com/download-files-from-azure-with-net-core-web-api-and-blazor-webassembly/#download-web-api

## Mapping Objects


**Automapper**

Profile Instances

A good way to organize your mapping configurations is with profiles. Create classes that inherit from Profile and put the configuration in the constructor:
https://docs.automapper.org/en/latest/Configuration.html


If you are using ASP.NET Core there is a helper extension to register all Profiles in Startup.ConfigureServices

// UI project
services.AddAutoMapper(Assembly.GetExecutingAssembly());

Open Generics

AutoMapper can support an open generic type map. Create a map for the open generic types
https://docs.automapper.org/en/stable/Open-Generics.html#open-generics

**Mapster**
Mapster - The Mapper of Your Domain
Writing mapping methods is a machine job. Do not waste your time, let Mapster do it.
https://github.com/MapsterMapper/Mapster

## elasticsearch-net ( Elasticsearch.Net & NEST)

NEST

NEST is the official high-level .NET client of Elasticsearch.

It aims to be a solid, strongly typed client with a very concise API. The client internally uses the low-level Elasticsearch.Net client.

https://github.com/elastic/elasticsearch-net

***Mapping***


https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/mapping.html

## web crawler

Abot Build Status

 Cross Platform C# web crawler framework built for speed and flexibility. Please star this project! +1. 
https://github.com/sjdirect/abot
