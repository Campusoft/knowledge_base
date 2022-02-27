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

# CQRS - Sin librerias 


Understand CQRS pattern in a real world application
- Query Dispatcher and Command Dispatcher are used to choose a proper handler for a giving query/command and execute it.
https://abdelmajid-baco.medium.com/cqrs-pattern-with-c-a9aff05aae3f



# MediatR 

Simple mediator implementation in .NET

In-process messaging with no dependencies.
Supports request/response, commands, queries, notifications and events, synchronous and async with intelligent dispatching via C# generic variance.

MediatR has two kinds of messages it dispatches:

- Request/response messages, dispatched to a single handler
- Notification messages, dispatched to multiple handlers


Behaviors
-  It represents a similar pattern to filters in ASP.NET MVC/Web API or pipeline behaviors in NServiceBus.

***Publish strategies***

```
/// <summary>
/// Strategy to use when publishing notifications
/// </summary>
public enum PublishStrategy
{
    /// <summary>
    /// Run each notification handler after one another. Returns when all handlers are finished. In case of any exception(s), they will be captured in an AggregateException.
    /// </summary>
    SyncContinueOnException = 0,

    /// <summary>
    /// Run each notification handler after one another. Returns when all handlers are finished or an exception has been thrown. In case of an exception, any handlers after that will not be run.
    /// </summary>
    SyncStopOnException = 1,

    /// <summary>
    /// Run all notification handlers asynchronously. Returns when all handlers are finished. In case of any exception(s), they will be captured in an AggregateException.
    /// </summary>
    Async = 2,

    /// <summary>
    /// Run each notification handler on it's own thread using Task.Run(). Returns immediately and does not wait for any handlers to finish. Note that you cannot capture any exceptions, even if you await the call to Publish.
    /// </summary>
    ParallelNoWait = 3,

    /// <summary>
    /// Run each notification handler on it's own thread using Task.Run(). Returns when all threads (handlers) are finished. In case of any exception(s), they are captured in an AggregateException by Task.WhenAll.
    /// </summary>
    ParallelWhenAll = 4,

    /// <summary>
    /// Run each notification handler on it's own thread using Task.Run(). Returns when any thread (handler) is finished. Note that you cannot capture any exceptions (See msdn documentation of Task.WhenAny)
    /// </summary>
    ParallelWhenAny = 5,
}

```
***Pipeline Behaviours***

Posee IPipelineBehavior, para colocar tuberias (pipe), en los flujos de MediatR


Pipeline behaviours are a type of middleware that get executed before and after a request (only supports requests, not notifications). They can be useful for a number of different tasks, such as logging, error handling, request validation etc.


***Referencias***

MediatR supports different Publish strategies that you can use.
https://github.com/jbogard/MediatR/wiki#publish-strategies

CQRS Design Pattern C#
- Benefits of CQRS
- Refactoring Towards a Task-based Interface
- Segregating Commands and Queries
- Segregating Commands and Queries
- Decorators vs. ASP.NET Middleware
https://codewithshadman.com/cqrs-design-pattern-csharp/#cqrs-in-the-real-world

# CQRSlite

CQRSlite is a small CQRS and Eventsourcing Framework. It is written in C# and targets .NET 4.5.2 and .NET Core. CQRSlite originated as a CQRS sample project Greg Young and I did in the autumn of 2010. This code is located at http://github.com/gregoryyoung/m-r

CQRSlite has been made with pluggability in mind. So every standard implementation should be interchangeable with a custom one if needed.
https://github.com/gautema/CQRSlite

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