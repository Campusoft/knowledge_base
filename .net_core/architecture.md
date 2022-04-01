# Referencias Arquitecturas


Clean Architecture Solution Template: A starting point for Clean Architecture with ASP.NET Core 
- Now available as a project template within Visual Studio.
- The goal of this repository is to provide a basic solution structure that can be used to build Domain-Driven Design (DDD)-based or simply well-factored, SOLID applications using .NET Core
- Posee linea commandos. (Cli)
- Utiliza Autofac
- MediatR
- Domain Events (MediatR)
- Posee https://github.com/ardalis/Specification. Base class with tests for adding specifications to a DDD model. Also includes a default generic Repository base class with support for EF6 and EF Core
- No tiene UnitOfWork
https://github.com/ardalis/CleanArchitecture
 
 
This is a solution template for creating a Single Page App (SPA) with Angular and ASP.NET Core following the principles of Clean Architecture. Create a new project based on this template by clicking the above Use this template button or by installing and running the associated NuGet package (see Getting Started for full details).
- Posee linea commandos. (Cli)
- ASP.NET Core 6
- Entity Framework Core 6
- Angular 12
- MediatR
- AutoMapper
- FluentValidation
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
- Entity Framework Core – Code First. Posee UnitOfWork (Incompleto Transacciones)
- Permission Management
- MediatR Pipeline Logging & Validation
- Serilog
- Swagger UI
- Response Wrappers
- Entity Framework Core - Audit Logs.
- Pagination
- Microsoft Identity with JWT Authentication
- Role based Authorization
- Database Seeding
- Custom Exception Handling Middleware.
- API Versioning
- Complete User Management Module (Register / Generate Token / Forgot Password / Confirmation Mail)
https://codewithmukesh.com/project/aspnet-core-hero-boilerplate/
https://github.com/aspnetcorehero/Boilerplate


***CoolStore Web Application***


CoolStore Website is a containerised microservices application consisting of services based on .NET Core running on Dapr. It demonstrates how to wire up small microservices into a larger application using microservice architectural principals.
- All .NET microservices are developed by using NetCoreKit library.
- Dapr
- MediatR
- Scrutor - Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection
- React
- WebApiGateway. (YARP Reverse Proxy)
https://github.com/vietnam-devs/coolstore-microservices


***clean-architecture-dotnet***

- DDD
- Command and Query Responsibility Segregation (a.k.a CQRS) (Use CQRSlite)
- MediatR - Simple, unambitious mediator implementation in .NET
- YARP - A toolkit for developing high-performance HTTP reverse proxy applications
- RestEase - Easy-to-use typesafe REST API client library for .NET Standard 1.1 and .NET Framework 4.5 and higher, which is simple and customisable
- Blazor - WASM - Client web apps with C#
- opentelemetry-dotnet - The OpenTelemetry .NET Client
- In medium and large software projects, we normally implement the CRUD actions over and over again. And it might take around 40-50% codebase just to do CRUD in the projects. The question is can we make standardized CRUD APIs, then we can use them in potential projects in the future?
https://github.com/thangchung/clean-architecture-dotnet

***StudentCourseManagement***
Fullstack Hub is developed to help students and professionals to quickly learn the industry standard applications development for FREE. - Yaseer Mumtaz
- Mediator. Crea los IRequestHandler como subclases de  IRequest
- CQRS
- ExceptionFilterAttribute
- UnitOfWork
- Redis
- RabbitMQ 
- API Gateway project is using the Ocelot
https://github.com/fullstackhub-io/StudentCourseManagement
https://fullstackhub.io/asp-net-core-microservices-with-angular11/


***Distributed-eStore***
Containerized .NET Core Online Store application with a microservices architecture and a React and Redux frontend solution. Technologies used - RabbitMQ, .NET Core, .NET Core MVC, MongoDB, React, Redux, Docker, Consul, Fabio, JWT Authentication, Swagger, Vault.
- Forwarded headers
https://github.com/evgenirusev/Distributed-eStore

***Convey***

Convey - a simple recipe for .NET Core microservices

-    Authentication JWT with secret key & certificates extensions
-    CQRS basic abstractions
-    Consul service registry integration
-    Swagger extensions
-    RestEase extensions
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
-    Web API extensions (minimal routing-based API, CQRS support)
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

***GoldenEye***
- Messaging infrastructure - both internal based on MediatR and external with Kafka,
  - Consume Kafka mensajes  y luego publish event to internal event bus  (MediatR)
- CQRS and Domain Driven Development stack - sending and handling commands, queries, events (with usage of MediatR library),
- Validation flow with FluentValidation.NET,
- Examples of complete usage (Cinema Ticket Reservations),
https://github.com/oskardudycz/GoldenEye


***.NET Microservices Sample Reference Application***
Sample .NET Core reference application, powered by Microsoft, based on a simplified microservices architecture and Docker containers.
- Domain events: design and implementation
  - The reference app uses MediatR to propagate domain events synchronously across aggregates, within a single transaction. 
- Posee UnitOfWork. La implementacion IUnitOfWork se la realiza en DbContext
  - UnitOfWork esta como propiedad en los IRepository
- Posee IPipelineBehavior
  - Un TransactionBehaviour para Transaction
  - LoggingBehavior para logs los Handling "LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>"
  - ValidatorBehavior para aplicar validaciones FluentValidation
- Angular
  - Posee un servicio base "DataService" que gestiona los errores (console) de los api rest
https://github.com/dotnet-architecture/eShopOnContainers



book, .NET Microservices: Architecture for Containerized .NET Applications.
https://docs.microsoft.com/en-us/dotnet/architecture/microservices/

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