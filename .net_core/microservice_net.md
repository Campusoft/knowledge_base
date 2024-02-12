# Microservices with .net 

# Client-to-microservice communication


Why consider API Gateways instead of direct 

Coupling: Without the API Gateway pattern, the client apps are coupled to the internal microservices. The client apps need to know how the multiple areas of the application are decomposed in microservices. When evolving and refactoring the internal microservices, those actions impact maintenance because they cause breaking changes to the client apps due to the direct reference to the internal microservices from the client apps. Client apps need to be updated frequently, making the solution harder to evolve.

The API Gateway pattern is also sometimes known as the “backend for frontend” (BFF) because you build it while thinking about the needs of the client app.


# Service-to-service communication


In fact, if your internal microservices are communicating by creating chains of HTTP requests as described, it could be argued that you have a monolithic application, but one based on HTTP between processes instead of intra-process communication mechanisms.


When constructing a cloud-native application, you'll want to be sensitive to how back-end services communicate with each other. Ideally, the less inter-service communication, the better. However, avoidance isn't always possible as back-end services often rely on one another to complete an operation.

Point-to-point style — Invoking services directly

Obviously this model works for relatively simple microservices based applications but as the number of services increases, this will become overwhelmingly complex.

- The non-functional requirements such as end-user authentication, throttling, monitoring etc. has to be implemented at each and every microservice level.
- As a result of duplicating common functionalities, each microservice implementation can become complex.
- There is no control what so ever on the communication between the services and clients (even for monitoring, tracing or filtering)

- Often the direct communication style is considered as an microservice anti-pattern for large scale microservice implementations.



**Service Aggregator Pattern**

Another option for eliminating microservice-to-microservice coupling is an Aggregator microservice

The pattern isolates an operation that makes calls to multiple back-end microservices, centralizing its logic into a specialized microservice.
 

**Materialized View pattern**



**service mesh**
A service mesh is a software layer that handles service-to-service communication

Right now, the main options for a service mesh in Kubernetes are linkerd and Istio.

**Referencias**
Service-to-service communication
https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/service-to-service-communication

# Ocelot

Ocelot is a lightweight API Gateway, recommended for simpler approaches. Ocelot is an Open Source .NET Core-based API Gateway especially made for microservices architectures that need unified points of entry into their systems. It’s lightweight, fast, and scalable and provides routing and authentication among many other features



# Saga

(Codigo) It is a Saga pattern implementation reference through an orchestration approach in a serverless architecture on Azure. The solution leverages Azure Functions for the implementation of Saga participants, Azure Durable Functions for the implementation of the Saga orchestrator, Azure Event Hubs as the data streaming platform and Azure Cosmos DB as the database service.
https://github.com/Azure-Samples/saga-orchestration-serverless


# Observabilidad

Microservice — Tracing Log in the Distributed System
- HttpContext.TraceIdentifier
- x-request-id 
- Web API, gRpc, Message queue
https://betterprogramming.pub/microservice-tracing-log-in-the-distributed-system-96f49bcb7bd

***Revisiones***

TraceId


# Resilient 

Polly 

Polly is a .NET resilience and transient-fault-handling library that allows developers to express policies such as Retry, Circuit Breaker, Timeout, Bulkhead Isolation, and Fallback in a fluent and thread-safe manner.


 .NET Core: Use HttpClientFactory and Polly to build rock solid services 
https://dev.to/rickystam/net-core-use-httpclientfactory-and-polly-to-build-rock-solid-services-2edh

# Tools


**tye**

Tye is a developer tool that makes developing, testing, and deploying microservices and distributed applications easier. Project Tye includes a local orchestrator to make developing microservices easier and the ability to deploy microservices to Kubernetes with minimal configuration.

https://github.com/dotnet/tye

Nota: This repository has been archived by the owner on Nov 20, 2023. It is now read-only.

Commandos

Run the tye command line in the folder file tye.yaml

```
tye run
``


Ejecutar con debug

```
tye run -v Debug
```

tye build

Ejecutar con un archivo yaml especifico

```
tye run tye.Repositorio.Cliente.yaml
```


Install tye via the following command:

```
dotnet tool install -g Microsoft.Tye --version "0.11.0-alpha.22111.1"
```

Errores
----------


Evaluated project metadata file could not be found for service

Los servicios, verificar si estan compilando.

Otras opciones:
- https://github.com/dotnet/tye/issues/766

--------------------

**YARP**

YARP - A toolkit for developing high-performance HTTP reverse proxy applications

Many of the existing proxies were built to support HTTP/1.1, but with workloads changing to include gRPC traffic, they require HTTP/2 support which requires a significantly more complex implementation. By using YARP the projects get to customize the routing and handling behavior without having to implement the http protocol.

YARP is built on .NET using the infrastructure from ASP.NET and .NET (.NET Core 3.1, .NET 5, and .NET 6). The key differentiator for YARP is that it's been designed to be easily customized and tweaked via .NET code to match the specific needs of each deployment scenario.

https://microsoft.github.io/reverse-proxy/index.html

# Referencias

outbox pattern
Improving microservices reliability – part 3: Outbox Pattern in action
https://www.davidguida.net/improving-microservices-reliability-part-3-outbox-pattern-in-action




This is my (very opinionated) roadmap for .NET developers that want to focus on backend and specifically work with microservices.
- API SDK library. Refit, RestSharp NSwag
- Task Scheduling. Native BackgroudService Hangfire
- Design Pattern: CORS. Event Sourcing. Strategy. Builder
https://github.com/Elfocrash/.NET-Backend-Developer-Roadmap


# Varios


## DotNetCore.CAP

Distributed transaction solution in micro-service base on eventually consistency, also an eventbus with Outbox pattern

CAP is a library based on .Net standard, which is a solution to deal with distributed transactions, has the function of EventBus, it is lightweight, easy to use, and efficient.


https://github.com/dotnetcore/CAP

# Revisiones


 The concept of minimal API is more applicable in Microservices or Serverless Architecture. As we are coming from a mindset of a well-organized monolith approach it's hard to digest asp.net core throwing away all of its charms just to look like a trending concept. In the aspect of scalability and performance, microservices and serverless models outperform monolith. Nowadays people don't care about code duplication and reusability rather they focus on how to scale out. If your application demand high scalability picks the microservice model. If it's required very high scalability go for serverless otherwise monolith is good. 
 
 
 
 