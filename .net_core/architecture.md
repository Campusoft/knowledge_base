# C# Coding Style


C# Coding Style
- The general rule we follow is "use Visual Studio defaults".
https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md


# Multi Tenant


**Opción Simple: Shared Database, Shared Schema (Base de Datos y Esquema Compartidos)**


Concepto: Se añade una columna de TenantId a casi todas las tablas que contienen datos de tenant.

.NET C#	Fácil de implementar con Entity Framework Core y filtros de consulta globales (Query Filters).

{Request} ->{Middleware (Identifica TenantId)} ->{Servicio Scoped} ->{DbContext/EF Core (Aplica Aislamiento)}$$


**Opción Media: Shared Database, Separate Schemas (Base de Datos Compartida, Esquemas Separados)**

Concepto: El aislamiento se logra a nivel de esquema de base de datos.


.NET C#	Requiere una lógica de conexión/mapeo dinámico en EF Core para apuntar al esquema correcto.


**Opción Compleja: Separate Databases (Bases de Datos Separadas)**

Concepto: La aplicación sabe a qué base de datos conectarse basándose en el TenantId (generalmente extraído del hostname o token de autenticación).


.NET C#	Requiere un patrón de fábrica de DbContext o inyección dinámica de dependencias para el connection string correcto.


 
Tipo | Tamaño | Seguridad | Fácil de recordar | Recomendado para
-- | -- | -- | -- | --
int | 4 bytes | baja | sí | multiempresa pequeña
long | 8 bytes | media | sí | más de 2B tenants
Guid | 16 bytes | alta | no | SaaS multi-tenant moderno
string | variable | alta | depende | tenants por dominio/subdominio

 
 
Aumenta tamaño de índices. Tipo de dato para el tenantId

- Guid = 16 bytes
- int = 4 bytes
- string 36 char = 36 bytes

Si tu volumen es gigante (millones de registros → por tenant), podría ser relevante.





# Referencias Arquitecturas


Clean Architecture Solution Template: A starting point for Clean Architecture with ASP.NET Core 
- Now available as a project template within Visual Studio.
- The goal of this repository is to provide a basic solution structure that can be used to build Domain-Driven Design (DDD)-based or simply well-factored, SOLID applications using .NET Core
- Posee linea commandos. (Cli)
- Utiliza Autofac
- MediatR
- Domain Events (MediatR)
- Posee https://github.com/ardalis/Specification. Base class with tests for adding specifications to a DDD model. Also includes a default generic Repository base class with support for EF6 and EF Core
- Persistencia
  - Tiene repositorios, que son una libreria. Ardalis.Specification.EntityFrameworkCore
  - Los repositorios, llaman en cada metodo (CRUD), al SaveChangesAsync
  - No tiene UnitOfWork
https://github.com/ardalis/CleanArchitecture
 
 
This is a solution template for creating a Single Page App (SPA) with Angular and ASP.NET Core following the principles of Clean Architecture. Create a new project based on this template by clicking the above Use this template button or by installing and running the associated NuGet package (see Getting Started for full details).
- Posee linea commandos. (Cli). dotnet new.
- ASP.NET Core 6
- Persistencia
  - Entity Framework Core 6	
  - No utiliza repositorios utiliza directamente DbContext
  - Utiliza SaveChangesInterceptor, para agregar valores a propiedades generales. *Ej. Valores de auditoria. 
- UI
  - Angular 15 or React 18
  - Utiliza NSwag  para generar los proxys del Clientes API REST
- MediatR
- AutoMapper
  - Utiliza Profile para las configuraciones de mapeo.
  - Utiliza metodo para inyectar todos los profile existente en un proyecto
 
`services.AddAutoMapper(Assembly.GetExecutingAssembly());`
  
- FluentValidation
  - RFC 7807 provides - details from HTTP APIs
- NUnit, FluentAssertions, Moq & Respawn
- Docker 
- NSwag.AspNetCore. (Configurado en "HostAplicacion"/api)
- Api Rest
  - Utiliza minimal api.
  - Posee una clase base EndpointGroupBase, para facilitar el mapeo de minimal api.  
- Domain Events (MediatR)
  - DbContext lanza  DispatchEvents. 
  - Las entidades deben implementar IHasDomainEvent para lanza eventos.
  - Lanza eventos por medio de un servicio DomainEventService : IDomainEventService. Esta implementacion utiliza MediatR
  - Version 8.x. Use DispatchDomainEventsInterceptor : SaveChangesInterceptor
- Posee IPipelineBehavior, para colocar tuberias (pipe), en los flujos de MediatR. 
  - AuthorizationBehaviour. (Para autorizacion, con mediatR)
  - ValidationBehaviour. Para aplicar validaciones con FluentValidation
- Posee un archivo "DependencyInjection" por cada proyecto para las configuraciones de inyeccion de dependencias
  - Posee archivos con global using
- Version .net (8.0.100-preview)
  - ASP.NET Core Clean Architecture Template v8 Released.
  https://ardalis.com/aspnetcore-clean-architecture-template-version-8/
  
https://github.com/jasontaylordev/CleanArchitecture 



ASP.NET Core Hero Boilerplate - .NET 5 Clean Architecture
- Onion Architecture - WebAPI & MVC
- CQRS with MediatR Library
  - PaginatedList: (PageNumber, PageSize) https://github.com/aspnetcorehero/Boilerplate/blob/master/AspNetCoreHero.Boilerplate.Application/Extensions/QueryableExtensions.cs
- Persistencia
  - Entity Framework Core – Code First. Posee UnitOfWork (Incompleto Transacciones)
  - En los repositorios, utiliza IDistributedCache 
  - Tiene un RepositoryAsync<T>, para CRUD, posee un metodo para obtener listados paginados. Esta clase posee una dependencia inyectada en el constructor del dbContexto. (ApplicationDbContext dbContext)
  - Tiene un UnitOfWork que implementa IDisposable, utiliza dbContext.SaveChangesAsync, para el Commit
- Permission Management
- MediatR Pipeline Logging & Validation
- Serilog
- API 
  - Swagger UI
  - API Versioning
- Response Wrappers
- Entity Framework Core - Audit Logs.
  - https://github.com/aspnetcorehero/EntityFrameworkCore.AuditTrail
- Pagination
- Microsoft Identity with JWT Authentication
- Role based Authorization
- Database Seeding
- Custom Exception Handling Middleware.
- Complete User Management Module (Register / Generate Token / Forgot Password / Confirmation Mail)
- Last commits. Mar 21, 2021
https://codewithmukesh.com/project/aspnet-core-hero-boilerplate/
https://github.com/aspnetcorehero/Boilerplate


**CoolStore Web Application**


CoolStore Website is a containerised microservices application consisting of services based on .NET Core running on Dapr. It demonstrates how to wire up small microservices into a larger application using microservice architectural principals.
- All .NET microservices are developed by using NetCoreKit library.
- Dapr
- MediatR
- Scrutor - Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection
- React. (Remix is a web framework that helps you build better websites with React)
- WebApiGateway. (YARP Reverse Proxy)
https://github.com/vietnam-devs/coolstore-microservices



**FullStackHero**

FullStackHero .NET 9 Starter Kit 

With ASP.NET Core Web API & Blazor Client

FullStackHero .NET Starter Kit is a starting point for your next .NET 9 Clean Architecture Solution that incorporates the most essential packages and features your projects will ever need including out-of-the-box Multi-Tenancy support. This project can save well over 200+ hours of development time for your team.


Technologies
- .NET 9
- Entity Framework Core 9
- Blazor
- MediatR
- PostgreSQL
- Redis
- FluentValidation

https://github.com/fullstackhero/dotnet-starter-kit

**clean-architecture-dotnet**

- DDD
- Command and Query Responsibility Segregation (a.k.a CQRS) (Use CQRSlite)
- MediatR - Simple, unambitious mediator implementation in .NET
- YARP - A toolkit for developing high-performance HTTP reverse proxy applications
- API 
  - RestEase - Easy-to-use typesafe REST API client library for .NET Standard 1.1 and .NET Framework 4.5 and higher, which is simple and customisable
- Blazor - WASM - Client web apps with C#
- opentelemetry-dotnet - The OpenTelemetry .NET Client
- In medium and large software projects, we normally implement the CRUD actions over and over again. And it might take around 40-50% codebase just to do CRUD in the projects. The question is can we make standardized CRUD APIs, then we can use them in potential projects in the future?
https://github.com/thangchung/clean-architecture-dotnet

**Clean Architecture Solution .NET 6**
- NET 6 / C#
- ASP.NET Core 6
- Entity Framework Core 6
- Angular 12
- CQRS
  - Posee interfaces para lanzar eventos, IDomainEventService. La implementacion es con MediatR
  - Events. Crea un objeto INotification "MediatR" con un generico de clase de eventos "DomainEvent"
- MediatR
  - Db.SaveChangesAsync lanzan event domain. Las entidades posee IHasDomainEvent, para establecer una lista de eventos 

```
      public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
```

- AutoMapper
  - Crea DTO, asociados a las entidades como genericos. Esto se utiliza para realizar mapeos de las clases detectando esta combinacion. 
  ```
  PowerDto : IMapFrom<Power>
  ```
- FluentValidation
- NUnit, FluentAssertions, Moq & Respawn
- Docker
https://github.com/arbems/Clean-Architecture-Solution


**StudentCourseManagement**
Fullstack Hub is developed to help students and professionals to quickly learn the industry standard applications development for FREE. - Yaseer Mumtaz
- Mediator. Crea los IRequestHandler como subclases de  IRequest
  - Posee clases implementan IRequestPreProcessor. LoggingBehaviour
  - Posee clases implementan IPipelineBehavior. UnhandledExceptionBehaviour, ValidationBehaviour con FluentValidation

```
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
```  
  
- CQRS
  - IRequestHandler, subclases de los commandos.
  - AddXCommand. Retornar identificador registro Creado
  - DeleteCourseCommand, UpdateCourseCommand retorna bool
- ExceptionFilterAttribute
- Persistencia
  - UnitOfWork. Simple. En el IUnitOfWork, posee las entidades. El UnitOfWork crea los objetos repository.
  - UnitOfWork se inyecta AddTransient
  - Libreria. https://github.com/DapperLib/Dapper.Contrib
- Redis
- Mensajes
  - RabbitMQ 
  - Desde command llaman: eventBusRabbitMQProducer.PublishCoursesCheckout
- API REST
  - Api con controller de asp.net core
  - No posee seguridad. Autentificacion, Autorizacion en los API Rest
  - Retorna los metodos con Task<ActionResult<Entidad>>. Ej: Task<ActionResult<List<CourseBasketVM>>>
- UI
  - Angular 11
  - Material
  - Utiliza un servicio generico para llamar a los API REST. 
- Otros
  - AutoMapper	
  - Validaciones FluentValidation.
	- services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
  - API Gateway project is using the Ocelot  
https://github.com/fullstackhub-io/StudentCourseManagement
https://fullstackhub.io/asp-net-core-microservices-with-angular11/


**Distributed-eStore**
Containerized .NET Core Online Store application with a microservices architecture and a React and Redux frontend solution. Technologies used - RabbitMQ, .NET Core, .NET Core MVC, MongoDB, React (Typescript), Redux, Docker, Consul, Fabio, JWT Authentication, Swagger, Vault.
- Forwarded headers
https://github.com/evgenirusev/Distributed-eStore

**Convey**
Convey - a simple recipe for .NET Core microservices

-    Authentication JWT with secret key & certificates extensions
-    CQRS basic abstractions
-    Consul service registry integration
-    API 
	 -    Swagger extensions
	 -    RestEase extensions
	 -    Web API extensions (minimal routing-based API, CQRS support)
-    Fabio load balancer integration
-    Logging extensions for Serilog & integration with Seq, ELK, Loki
-    Message brokers abstractions & CQRS support
-    RabbitMQ integration
-    Inbox + Outbox implementation for EF Core, Mongo
-    AppMetrics extensions
-    Prometheus integration
-    MongoDB extensions
-    OpenStack OCS support
-    Redis extensions
-    Vault secrets engine (settings, dynamic credentials, PKI etc.) integration
-    Security extensions (certificates, mTLS, encryption etc.)
-    Jaeger tracing integration
-    Scrutor - Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection 
https://github.com/snatch-dev/Convey

Pacco

- Pacco does use an event-driven approach in order to asynchronously integrate between the microservices.
- Use Convey 
- Operations Service. Subscribes to all messages and inform the user about operations status via web socets push notifications.
https://github.com/devmentors/Pacco

FeedR

Sample (and rather simple) .NET6 microservices solution which acts as the data aggregator for the different feeds. The purpose of this solution is to explore the latest framework and play with different patterns, tools & libraries that can be useful when building distributed applications (but not only)
- Creado devmentors. (Existe videos)
https://github.com/devmentors/FeedR

**GoldenEye**
- Messaging infrastructure - both internal based on MediatR and external with Kafka,
  - Consume Kafka mensajes  y luego publish event to internal event bus  (MediatR). (Referencias de Codigo)
  - Utilizando informacion del mensaje, se obtiene el tipo de mensaje. 
  - Busca en las dll el tipo. Luego desealiza el tipo desde Json. 
  - Utiliza IHostedService, para escuchar los mensajes. 
- CQRS and Domain Driven Development stack - sending and handling commands, queries, events (with usage of MediatR library).
  - Posee un wrapper, de mediatR. CommandBus: ICommandBus
  - Posee una clase para lanzar eventos.  EventBus: IEventBus. 
  - Posee IExternalEventConsumer. Sin implementacion, para lanzar eventos externos
- Validation flow with FluentValidation.NET,
- Examples of complete usage (Cinema Ticket Reservations),
https://github.com/oskardudycz/GoldenEye


**.NET Microservices Sample Reference Application**
Sample .NET Core reference application, powered by Microsoft, based on a simplified microservices architecture and Docker containers.
- Domain events: design and implementation
  - The reference app uses MediatR to propagate domain events synchronously across aggregates, within a single transaction. 
- Posee UnitOfWork. La implementacion IUnitOfWork se la realiza con DbContext. Posee el metodo Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)), el cual es un metodo del DBContext.
  - UnitOfWork esta como propiedad en los IRepository. Y se asigna por el parametro que se recibe en el constructor. (XContext context)
  - La transaccion se maneja a nivel de un IPipelineBehavior. 
- Posee IPipelineBehavior
  - Un TransactionBehaviour para Transaction
  - LoggingBehavior para logs los Handling "LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>"
  - ValidatorBehavior para aplicar validaciones FluentValidation
- Angular
  - Posee un servicio base "DataService" que gestiona los errores (console) de los api rest
- book, .NET Microservices: Architecture for Containerized .NET Applications.
  - https://docs.microsoft.com/en-us/dotnet/architecture/microservices/
https://github.com/dotnet-architecture/eShopOnContainers
https://github.com/dotnet/eShop. (New Repository)



Full Modular Monolith application with Domain-Driven Design approach. 
- Posee codigo
- Posee diagramas
- Posee documentacion 
- C4 model is a lean graphical notation technique for modelling the architecture of software systems. 
- Module: API, User Access, Meetings, Administration , Payments.
  - Module initialization. Each module has a static Initialize method which is invoked in the API Startup class.
  - Each Module has Clean Architecture
  - Module Requests Processing via CQRS
- Modules Integration
  - To ensure maximum reliability, the Outbox / Inbox pattern is used. 
- Security
  - Authentication is implemented using JWT Token and Bearer scheme using IdentityServer.
  - Authorization is achieved by implementing RBAC (Role Based Access Control) using Permissions
- Varios
  - Usa Autofac
- All Architectural Decisions (AD) are documented in the Architecture Decision Log (ADL).  
https://github.com/kgrzybek/modular-monolith-with-ddd


nopCommerce
The most popular open-source eCommerce shopping cart solution based on ASP.NET Core 
- Linq2DB is an open-source ORM framework for .NET applications. It is a .NET Foundation project. 
- Fluent Validation
  - Posee una configuracion, para determinar si se aplica validaciones de propiedades (telefonos, email,nombres, apellidos, etc) de clientes. 
  - Posee una clase base "BaseNopValidator<TModel> : AbstractValidator<TModel> where TModel : class"
- Razor View Engine
- AutoMapper
- JQuery
- Kendo UI
- Microsoft SQL Server, MySQL Server, PostgreSQL
- Redis (cache)
- nopCommerce 4.5.x: We've migrated nopCommerce to .NET 6
https://github.com/nopSolutions/nopCommerce


Orchard Core
- Orchard Core Framework: An application framework for building modular, multi-tenant applications on ASP.NET Core.
- Orchard Core CMS: A Web Content Management System (CMS) built on top of the Orchard Core Framework.
https://docs.orchardcore.net/en/main/


Northwind Traders is a sample application built using ASP.NET Core and Entity Framework Core. 

- .NET Core 3
- ASP.NET Core 3
- Entity Framework Core 3
- Validation with FluentValidation
- Object-Object Mapping with AutoMapper
- Angular 8
- Open API with NSwag
- Using CQRS + MediatR simplifies your overall design
  - MediatR 
  - Query : IRequest
  - Command : IRequest
- Security using ASP.NET Core Identity + IdentityServer 
- Domain
  - List child private set;
- Automated testing with xUnit.net, Moq, and Shouldly
- This repository has been archived by the owner on Jul 1, 2023. It is now read-only. For an updated version of this project, you can check out the Clean Architecture Solution Template. The Clean Architecture Solution Template is a new project that demonstrates the simplest approach to Clean Architecture with .NET.
https://github.com/jasontaylordev/NorthwindTraders



PiranhaCMS
- Tiene Repository, con DbContext de entity framework. En cada metodo (Save), se llama a SaveChangesAsync
```
await _db.SaveChangesAsync().ConfigureAwait(false);
```
- Soporta Postgre, SQLite, SQLServer, MySql
https://github.com/PiranhaCMS

Clean Architecture with .NET Core & React+Redux
- Manga is a Virtual Wallet Solution in which the customer register an account then manage the balance by Deposit, Withdraw and Transfer operations.
https://github.com/ivanpaulovich/clean-architecture-manga

Raytha 
- Raytha is a powerful CMS for .NET developers with an easy-to-use interface and fast performance. It offers custom content types, a template engine, and various access controls.
- .NET 6+
- SQL Server Express. No utiliza repositorios, directamente utiliza datacontext
- MediatR. CQRS 
- Fluid is an open-source .NET template engine based on the Liquid template language. 
https://github.com/RaythaHQ/raytha


Restaurant App 🍔 is a sample open-source e-Commerce 🛒 application for ordering foods, powered by polyglot microservices architecture and cross-platform development including mobile and web


https://github.com/chayxana/Restaurant-App


Sample ASP.NET Core 7.0 reference application, powered by Microsoft, demonstrating a layered application architecture with monolithic deployment model. Download the eBook PDF from docs folder.
- The main application in the eShopOnWeb solution is a monolithic ASP.NET Core web app. It is organized according to Clean Architecture principles, such that dependencies on infrastructure concerns are minimized throughout the application. 
- Usa MinimalApi.Endpoint, para configurar minimal apis. https://www.nuget.org/packages/MinimalApi.Endpoint
- Usa Ardalis.Specification.EntityFrameworkCore
https://github.com/dotnet-architecture/eShopOnWeb




Modular Monolith with DDD
- C4 Model. Files architecture decision
- Modules
  - Event-driven communication between modules
- Autofac
- CQRS
  - Class Wrapper MediatR
  - Query. Use Dapper
  - Command. DDD and EntityFramework. With Repository
  - implement CQRS pattern using raw sql scripts as Read Model side processing and DDD approach as Write Model side implementation. https://www.kamilgrzybek.com/blog/posts/simple-cqrs-implementation-raw-sql-ddd
https://github.com/kgrzybek/modular-monolith-with-ddd
 
 



Clean Architecture with CQRS Pattern
- .NET 7.
- CQRS sin librerias, crea el pattern manualmente. Sin utilizar librerias tipo MediatR
  - Utiliza record, para los DTO-Command-RequestAPI 
- API
  -  Post. Utiliza CreatedAtAction para crear una respuesta con el url get recurso, mas el identificador. Los commandos no deben retornar datos
- DDD
  - Posee IDomainEvent. Una lista de eventos, asociados a la entidad. No existe implementacion de lanzar los eventos. 
- Varios
  - Utiliza extensiones, para IServiceCollection, en las diferentes proyectos, y subcarpetas de estos proyectos.
  - Scrutor
https://github.com/Rezakazemi890/Clean-Architecture-CQRS


**serenity**

Serenity is an ASP.NET Core / TypeScript application platform that is built on open-source technologies.

It aims to make development easier while reducing maintenance costs by avoiding boilerplate code, reducing the time spent on repetitive tasks, and applying the best software design practices.

Serene is our free, open-source starter application template based on the Serenity platform. We'll mainly use Serene for our tutorial and other samples through this documentation.

StartSharp is the premium application template we offer to our paid customers. It contains everything in Serene, in addition to a more polished theme, and some extra features. They are both based on the Serenity platform.


https://github.com/serenity-is/Serenity


**fullstacktemplates**


# Exceptions 

Exceptions should be uncommon. In comparison to other code flow patterns, the catch and throw of exceptions is slow. Exceptions are not used to control the program’s flow. Consider the program’s logic when identifying and resolving exception-prone scenarios.

# Revisiones

Revisar. Sample Projects

"We came across the decision to orchestrate our microservices by using a "god" like service that controls the business logic or a choreographed approach where the microservices basically pass messages, In microservice architecture choreography is preferred over orchestration."

https://github.com/mjebrahimi/Awesome-Microservices-NetCore#sample-projects



There is a couple of microservices which implemented e-commerce modules over Catalog, Basket, Discount and Ordering microservices with NoSQL (MongoDB, Redis) and Relational databases (PostgreSQL, Sql Server) with communicating over RabbitMQ Event Driven Communication and using Ocelot API Gateway.
- gRPC Communication
- Microservices Resilience Implementations
https://github.com/aspnetrun/run-aspnetcore-microservices

A collection of awesome training series, articles, videos, books, courses, sample projects, and tools for Microservices in .NET
https://github.com/mjebrahimi/Awesome-Microservices-DotNet