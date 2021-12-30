# Referencias Arquitecturas


Clean Architecture Solution Template: A starting point for Clean Architecture with ASP.NET Core 
- Now available as a project template within Visual Studio.
- The goal of this repository is to provide a basic solution structure that can be used to build Domain-Driven Design (DDD)-based or simply well-factored, SOLID applications using .NET Core
- Posee linea commandos. (Cli)
- Utiliza Autofac
- MediatR
- Domain Events (MediatR)
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
- Posee IPipelineBehavior, para colocar tuberias (pipe), en los flujos de MediatR. 
  - AuthorizationBehaviour. (Para autorizacion, con mediatR)
https://github.com/jasontaylordev/CleanArchitecture 



ASP.NET Core Hero Boilerplate - .NET 5 Clean Architecture
- Onion Architecture - WebAPI & MVC
- CQRS with MediatR Library
- Entity Framework Core – Code First
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


# Revisiones

Revisar. Sample Projects
https://github.com/mjebrahimi/Awesome-Microservices-NetCore#sample-projects