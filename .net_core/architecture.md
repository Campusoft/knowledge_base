# C# Coding Style


C# Coding Style
- The general rule we follow is "use Visual Studio defaults".
https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md


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
- Posee linea commandos. (Cli)
- ASP.NET Core 6
- Persistencia
  - Entity Framework Core 6	
  - No utiliza repositorios utiliza directamente DbContext
- UI
  - Angular 12
  - Utiliza NSwag  para generar los proxys del Clientes API REST
- MediatR
- AutoMapper
- FluentValidation
  - RFC 7807 provides - details from HTTP APIs
- NUnit, FluentAssertions, Moq & Respawn
- Docker 
- NSwag.AspNetCore. (Configurado en "HostAplicacion"/api)
- Domain Events (MediatR)
  - DbContext lanza  DispatchEvents. 
  - Las entidades deben implementar IHasDomainEvent para lanza eventos.
  - Lanza eventos por medio de un servicio DomainEventService : IDomainEventService. Esta implementacion utiliza MediatR
- Posee IPipelineBehavior, para colocar tuberias (pipe), en los flujos de MediatR. 
  - AuthorizationBehaviour. (Para autorizacion, con mediatR)
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

**StudentCourseManagement**
Fullstack Hub is developed to help students and professionals to quickly learn the industry standard applications development for FREE. - Yaseer Mumtaz
- Mediator. Crea los IRequestHandler como subclases de  IRequest
- CQRS
- ExceptionFilterAttribute
- Persistencia
  - UnitOfWork. Simple. En el IUnitOfWork, posee las entidades. El UnitOfWork crea los objetos repository.
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
  - Consume Kafka mensajes  y luego publish event to internal event bus  (MediatR)
- CQRS and Domain Driven Development stack - sending and handling commands, queries, events (with usage of MediatR library),
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