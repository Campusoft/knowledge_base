# Pattern implement .net core

# CQRS (*)


Implement CQRS Pattern in ASP.NET Core 5
- Implementa un servicio aplicacion CRUD, normalmente. 
- Luego implementa el mismo servicio utilizando CQRS, servicios separados Crear, actualizar, borrar, y para diferentes consultas
https://www.ezzylearning.net/tutorial/implement-cqrs-pattern-in-asp-net-core-5

CQRS & Mediator in .NET Core — “A piece of cake”
(Se explica con un ejemplo ambos patrones)
https://letienthanh0212.medium.com/cqrs-and-mediator-in-net-core-project-c0b477eab6e9




Simple CQRS implementation with raw SQL and DDD
- Write Model should be implemented with DDD approach. The level of DDD implementation should depend on level of domain complexity.
- reads utiliza directamente sql. (Sin intermediarios)
https://www.kamilgrzybek.com/design/simple-cqrs-implementation-with-raw-sql-and-ddd/


***MediatR***

Simple mediator implementation in .NET

In-process messaging with no dependencies.
Supports request/response, commands, queries, notifications and events, synchronous and async with intelligent dispatching via C# generic variance.

MediatR has two kinds of messages it dispatches:

- Request/response messages, dispatched to a single handler
- Notification messages, dispatched to multiple handlers


Behaviors
-  It represents a similar pattern to filters in ASP.NET MVC/Web API or pipeline behaviors in NServiceBus.

***Revision***

Posee IPipelineBehavior, para colocar tuberias (pipe), en los flujos de MediatR



# Saga

Implementation of saga pattern for .NET Core  
https://github.com/snatch-dev/Chronicle 


## OpenSleigh 

OpenSleigh is a distributed Saga management library, written in C# with .NET Core. It is intended to be reliable, fast, easy to use, configurable and extensible.
Persistence

-    MongoDB
-    MSSQL
-    PostgreSQL
-    CosmosDB with SQL API
-    CosmosDB with MongoDB API

Transport

-    Azure Service Bus
-    RabbitMQ
-    Kafka

https://github.com/mizrael/OpenSleigh


## MassTransit 

## NServiceBus

# Referencias

Guidance with examples of bad and good patterns of how to write asynchronous code.
https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md



***framework***
Async/await first CQRS+ES and DDD framework for .NET 
EventFlow is a basic CQRS+ES framework designed to be easy to use.
https://github.com/eventflow/EventFlow


***codigo***
 Sample .NET Core REST API CQRS implementation with raw SQL and DDD using Clean Architecture. 
https://github.com/kgrzybek/sample-dotnet-core-cqrs-api