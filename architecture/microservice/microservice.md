# Microservices 

A microservices architecture is not just built around design principles. Some call it a  culture. It’s a result of many other collaborative efforts. Yes, the design is a key element, but we also need to worry about collaboration between developers and domain experts, the communication between teams and team members, continuous integration and delivery, and many other issues. 

When we develop microservices, we need to consider that not all microservices are similar



# Microservices Characteristics

James Lewis and Martin Fowler provided a reasonable common set of characteristics that fit most of the microservices architectures:

- Componentization via services
- Organized around business capabilities
- Products not projects
- Smart endpoints and dumb pipes
- Decentralized governance
- Decentralized data management
- Infrastructure automation
- Design for failure
- Evolutionary design



- Componentizacion via Servicios
- Organidos en base a las capacidades del negocio (Business Capabilities)
- Productos no Proyectos
- Endpoints inteligentes y pipes tontos (dumb pipes)
- Governanza descentralizada
- Gestión de datos decentralizada
- Automatización de infrastructura
- Diseño enfocado a fallas
- Diseño evolutivo (Evolutionary Design)


- They can be self-scaled individually.
- They are developed with choreography in mind and not orchestration



Retos de los microservicios

Aunque traen muchas ventajas, los microservicios son un concepto relativamente nuevo y, por lo cual, presentan bastante retos:

-   En primer lugar, la complejidad: Una aplicación basada en microservicios es más compleja que un monolito, ya que está compuesta por muchos servicios distintos e independientes. Se necesita por lo cual de una política de gobernanza adecuada.
- 	Además, manejar los fallos es más complicado, ya que se necesita monitorizar distintas piezas para detectar los posibles problemas.
-   Finalmente, no todos los profesionales de IT poseen los conocimientos necesarios para desarrollar y gestionar correctamente una arquitectura de microservicios.


Costo

- Distribution: Distributed systems are harder to program, since remote calls are slow and are always at risk of failure.
- Eventual Consistency: Maintaining strong consistency is extremely difficult for a distributed system, which means everyone has to manage eventual consistency.
- Operational Complexity: You need a mature operations team to manage lots of services, which are being redeployed regularly.


"The foundation of microservice architecture (MSA) is about developing a single application as a suite of small and independent services that are running in their own process, developed and deployed independently[1]. "

Microservices for the Enterprise: Designing, Developing, and Deploying 1st ed. Edition 


- Loose coupling: which means microservices should be able to be modified without requiring changes in other microservices.
- Problem locality: which means related problems should be grouped together (in other words, if a change requires an update in another part of the system, those parts should be close to each other).


# Design Principles 

- High cohesion. Services should work together with minimum communication.
- Low coupling. Services should have few, if any, interdependencies.
- Single responsibility scope. A service should set boundaries to reflect a specific business requirement, and do only one thing.
- Reliability. Design the services for maximum fault tolerance or resilience so a fault in one service does not disable the entire application.
- Don't share data. Each service should have/access its own database or storage volume.
- Use automation. Utilize automation tools to deploy and manage microservices components (often as part of a CI/CD pipeline).
- Use APIs. APIs are the go-to method for interaction and communication between services.
- Monitor. Use monitoring tools to oversee and report the performance or problems with services as well as manage traffic for best service performance.

Follow these 10 fundamental microservices design principles
https://searchapparchitecture.techtarget.com/tip/Follow-these-10-fundamental-microservices-design-principles




# Database per Microservice

Here I am using the term database to show a logical separation of data, i.e., the Microservices can share the same physical database, but they should use separate Schema/collection/table

## Data separation

- What happens when a service developed by a team requires a change of schema in a database shared by other services?


Read Only Data Aggregation In a Microservices Environment: A Real Life Use Case
- Have an incrementing version id of your entity to support stale data detection.
https://www.wix.engineering/post/read-only-data-aggregation-in-a-microservices-environment-a-real-life-use-case

- Event/subscription model

A good set of events must be integrated into the generating microservice and lost events are a possibility

## Shared Static Data

For example, data such as U.S. states, list of countries, etc., is often used as the shared static data. 


One would think that having another microservices with the static data would solve this problem, but it is overkill to have a service just to get some static information that does not change over time. Hence, sharing static data is often done with shared libraries. For example, if a given service wants to use the static metadata, it has to import the shared library into the service code


Also, you can cache any service-level metadata (configurations or static data) during the service startup.

# Pattern


API Gateway / Backends for Frontends
https://microservices.io/patterns/apigateway.html


Microservice Architecture and Design Patterns for Microservices
- Decomposition Patterns
- Integration Patterns
- Database Patterns
- Observability Patterns
- Cross-Cutting Concern Patterns
https://medium.com/@madhukaudantha/microservice-architecture-and-design-patterns-for-microservices-e0e5013fd58a

There are 6 Data Management patterns that can help you manage your data effectively.
- Database Per Service:
- Shared Database:
- Saga Pattern:
- API Composition:
- CQRS:
- Event Sourcing:
https://dzone.com/articles/6-data-management-patterns-for-microservices-1


## Resiliency / resiliente / Resistencia

Una app resiliente es una que continúa funcionando a pesar de tener fallas en los componentes del sistema. La resiliencia requiere planificación en todos los niveles de la arquitectura. Influye en el diseño de la infraestructura y la red, y en el diseño de la app y el almacenamiento de datos. La resiliencia también se extiende a las personas y la cultura.

## Event driven: 

En este patrón un microservicio publica un evento y otro microservicio lo consumirá.

## Aggregator o Proxy

Un cliente web necesita información que esta en varios microservicios. En este caso se invoca a un microservicio que agrega las llamadas a otros microservicios para obtener la respuesta.

## Asynchronous messaging

## Request/Reply Pattern

Another approach for decoupling synchronous HTTP messages is a Request-Reply Pattern, which uses 
queuing communication. Communication using a queue is always a one-way channel, with a producer 
sending the message and consumer receiving it. With this pattern, both a request queue and response 
queue are implemented

## Patrón Saga

El Patrón Saga es una secuencia de transacciones locales donde cada transacción actualiza información dentro de un servicio.

Saga is just another name for Process Manager, and so a saga is nothing but the implementation of a state machine whose transitions between states are determined by messages.

***Choreography and Orchestration***

Choreography

Choreography is a way to coordinate sagas where participants exchange events without a centralized point of control. With choreography, each local transaction publishes domain events that trigger local transactions in other services

Orchestration

Orchestration is a way to coordinate sagas where a centralized controller tells the saga participants what local transactions to execute. The saga orchestrator handles all the transactions and tells the participants which operation to perform based on events. The orchestrator executes saga requests, stores and interprets the states of each task, and handles failure recovery with compensating transactions.


Pattern: Saga
- Example: Choreography-based saga
- Example: Orchestration-based saga
https://microservices.io/patterns/data/saga.html

Microservices Architecture Pattern - SAGA

-    What is SAGA Pattern?
-    Real World use Case
-    Types of SAGA pattern
-    Choreography – Basic
-    Orchestration - Basic
https://www.c-sharpcorner.com/article/microservices-architecture-pattern-saga/


## Sidecar pattern  (Pronu: saidecar)


## Backends for Frontends pattern

Create separate backend services to be consumed by specific frontend applications or interfaces.
https://docs.microsoft.com/en-us/azure/architecture/patterns/backends-for-frontends

## Mediator 

Behavioral Pattern 

- Less coupling: Since the classes don’t have dependencies on each other, they are less coupled.

## CQRS 


The returned data (ViewModel) can be the result of joining data from multiple entities or tables in the database, or even across multiple aggregates defined in the domain model for the transactional area. In this case, because you are creating queries independent of the domain model, the aggregates boundaries and constraints are ignored and you’re free to query any table and column you might need. This approach provides great flexibility and productivity for the developers creating or updating the queries.


Introduction to CQRS — In Microservices

How is exactly the CQRS pattern related to microservices
Without a doubt, microservices has changed the way applications are conceived. The monolithic model has transitioned to a decentralized architecture where each service can be scaled and optimized independently. CQRS adds another layer of scalability, performance, and flexibility to microservices by allowing granular read-write optimizations at the database level.

The Command and Query Responsibility Segregation (CQRS) pattern propose separating the write data model from the read data model. This separation of responsibilities would provide the flexibility to decide whether the read and write services should coexist in the same data store or be managed in completely different databases.

https://danielckv.medium.com/introduction-to-cqrs-in-microservices-70e4759d9ecc



Why we failed to implement CQRS in Microservice architecture.
- Apply CQRS for All the services. This is one of the main mistake we did in implementing CQRS.  Greg Young said in one of his video (as I remember) that we have to find areas where we can get a benefit from using CQRS
- Read service handles all the read requests. No business logic is applied here.
- How we are going to update UI, We don’t know whether record is successfully saved or not
- We can implement CQRS without using Asynchronous communication.
- We can implement CQRS without using Asynchronous communication.
https://faun.pub/why-we-failed-implementing-cqrs-in-microservice-architecture-5c39a2d0b2dd

## Circuit Breaker

# Error handling

During message processing there might be a chance that an exception will be thrown. We can distinguish two types of exceptions:

- domain exception - informs that message cannot be further processed due to some domain logic like. PasswordToShortException
- infrastructure exception - informs that message cannot be further processed due to infrastructure issues like. connecting to database etc.

In the first scenario, it’s better not to retry the processing (wrong password is not going to be better once we try again). n the second one, we can try a few times before we give up
https://convey-stack.github.io/documentation/messaging/

"Error responses are classified as “retryable” or “non-retryable”"


# Observabilidad 

- Logging
- Trazabilidad en logs
- Alertas

To achieve observability, you need to instrument everything and view all your telemetry data in one place


La Era de la Observabilidad
Por qué el futuro es abierto, conectado y programable
(Buen articulo)
"Yuri Shkuro, autor e ingeniero de software de Uber Technologies, explica la diferencia de esta manera: el monitoreo mide lo que uno decide con anterioridad que es importante, mientras que la observabilidad es la capacidad de hacer preguntas que uno desconoce de antemano acerca del sistema."
https://newrelic.com/resources/ebooks/what-is-observability-es


Medición de DevOps: supervisión y observabilidad
- La supervisión es una herramienta o una solución técnica que permite a los equipos observar y comprender el estado de sus sistemas. La supervisión se basa en la recopilación de conjuntos predefinidos de métricas o registros.

- La observabilidad es una herramienta o una solución técnica que permite a los equipos depurar de forma activa su sistema. La observabilidad se basa en la exploración de las propiedades y los patrones que no se definen con anticipación.

https://cloud.google.com/architecture/devops/devops-measurement-monitoring-and-observability?hl=es-419


Distributed logging is one other challenging thing. In a monolithic application, request process inside a single application because of that all the logs are in a one log file. When it comes to the microservices, request can span multiple services. We must have a way to trace that request across multiple services. We usually use a correlation id for that.
https://faun.pub/why-microservices-fail-eda3e25069a0

Microservices logging best practices every team should know
- Use a correlation ID
- Structure logs appropriately
- Provide informative application logs
- Visualize log data
- Use centralized log storage
- Query logs
- Handle failures
https://www.techtarget.com/searchapparchitecture/tip/5-essential-tips-for-logging-microservices

logging
- excludePaths - optional endpoints that should be excluded from logging (e.g. while performing the health checks by other services).


**Traces**
Traces—or more precisely, “distributed traces”—are samples of causal chains of events (or transactions) between different components in a microservices ecosystem. And like events and logs, traces are discrete and irregular in occurrence.

**Metrics**
To put it simply, metrics are numeric measurements. Metrics can include:
- A numeric status at a moment in time (like CPU % used)
- Aggregated measurements (like a count of events over a one-minute time, or a rate of events-per-minute)
	


**OpenTelemetry**

An observability framework for cloud-native software.

OpenTelemetry is a collection of tools, APIs, and SDKs. You can use it to instrument, generate, collect, and export telemetry data (metrics, logs, and traces) for analysis in order to understand your software's performance and behavior.



**Zipkin**

Zipkin is a distributed tracing system. It helps gather timing data needed to troubleshoot latency problems in service architectures. Features include both the collection and lookup of this data.

Architecture Overview
https://zipkin.io/pages/architecture.html


**Health checks**

Health monitoring can allow near-real-time information about the state of your containers and microservices. Health monitoring is critical to multiple aspects of operating microservices and is especially important when orchestrators perform partial application upgrades in phases, as explained later.

Microservices-based applications often use heartbeats or health checks to enable their performance monitors, schedulers, and orchestrators to keep track of the multitude of services. If services cannot send some sort of "I'm alive" signal, either on demand or on a schedule, your application might face risks when you deploy updates, or it might just detect failures too late and not be able to stop cascading failures that can end up in major outages.

# Microservices Frameworks


Top Microservices Frameworks
- JAVA
	- Spring Boot 

https://dzone.com/articles/top-microservices-frameworks

# Platform

## Dapr

Dapr is a portable, event-driven runtime that makes it easy for enterprise developers to build resilient, stateless and stateful microservice applications that run on the cloud and edge and embraces the diversity of languages and developer frameworks.

Dapr codifies the best practices for building microservice applications into open, independent, building blocks that enable you to build portable applications with the language and framework of your choice. Each building block is completely independent and you can use one, some, or all of them in your application.

In addition Dapr is platform agnostic meaning you can run your applications locally, on any Kubernetes cluster, and other hosting environments that Dapr integrates with. This enables you to build microservice applications that can run on the cloud and edge.

Developer language SDKs and frameworks
To make using Dapr more natural for different languages, it also includes language specific SDKs for Go, Java, JavaScript, .NET and Python. These SDKs expose the functionality in the Dapr building blocks, such as saving state, publishing an event or creating an actor, through a typed, language API rather than calling the http/gRPC API. This enables you to write a combination of stateless and stateful functions and actors all in the language of their choice. And because these SDKs share the Dapr runtime, you get cross-language actor and functions support

https://dapr.io/




# Service Discovery

Today, most companies in the software industry are moving towards cloud platforms such as AWS, google cloud, and Azure. When your services are running in the cloud, you can scale up or down at any moment by utilizing vertical or horizontal scaling processes. You are only charged for the usage of the purchased resources. So, it is important to purchase and utilize in a cost-effective manner. This means you are dealing with a highly dynamic environment, where services can have different server addresses from time to time. 

"the server endpoints are subject to frequent change": Cloud, Microservices, traffic increases

important aspect of microservices: discovery


Service registry

A service registry provides a database that applications can use to implement the Service Discovery pattern, one of the key tenets of a microservices-based architecture. Trying to hand-configure each client of a service or adopt some form of access convention can be difficult and prove to be brittle in production. Instead, applications can use a service registry to dynamically discover and call registered services.


Client-side Discovery

In this approach the client or the API-GW obtains the location of a service instance by querying a Service Registry.

Server-side Discovery

With this approach, clients/API-GW sends the request to a component(such as a Load balancer) that runs on a well-known location. That component calls the service registry and determine the absolute location of the microservice.




**Consul**

**Eureka**

**Zookeeper**

**Etcd**

 A distributed, reliable key-value store for the most critical data of a distributed system 

- Store data in hierarchically organized directories, as in a standard filesystem
- Watch specific keys or directories for changes and react to changes in values


Note that because etcd’s performance is heavily dependent upon storage disk speed, it’s highly recommended to use SSDs in etcd environments

etcd is a distributed reliable key-value store for the most critical data of a distributed system, with a focus on being:

-    Simple: well-defined, user-facing API (gRPC)
-    Secure: automatic TLS with optional client cert authentication
-    Fast: benchmarked 10,000 writes/sec
-    Reliable: properly distributed using Raft


Por diseño, etcd almacena datos como un par clave-valor de múltiples versiones; lo que significa es que cualquier operación en sus datos no actualiza los datos existentes, sino que creará una nueva versión, de ahí el término multi-versión. Si crea algunos datos en etcd, es la versión 1, entonces cuando realiza alguna operación en esos mismos datos, se crea la versión 2 y la versión 1 permanece como está. Entonces, etcd es efectivamente inmutable. Todas las versiones de datos siguen siendo accesibles y visibles todo el tiempo.


# Crosscutting Features

# deployed

Having one repository per microservice helps  release it independently from other microservices


## Cache

Where Is My Cache? Architectural Patterns for Caching Microservices. 
(Relacionado con Hazelcast Platform, como referencia es interesante)
- Pattern 1: Embedded Cache
- Pattern 1*: Embedded Distributed Cache
- Pattern 2: Client-Server Cache
- Pattern 2*: Cloud Cache
- Pattern 3: Sidecar Cache
- Pattern 4: Reverse Proxy Cache
- Pattern 4*: Reverse Proxy Sidecar Cache

https://hazelcast.com/blog/architectural-patterns-for-caching-microservices/


Revisiones
One of the most important aspects of caching is not to use a central caching layer 
that is shared between microservices. However, the instances of a given microservice will 
all have the same data requirements, so it makes sense to share a caching layer across 
these instances


#  Ejemplos 


- All these services are deployed into the same monolithic runtime (such as application servers). When one service requires more CPU, while the other requires more memory. (monolith 

# Referencias Aplicaciones - Microservices


Online Google

Online Boutique is a cloud-native microservices demo application. Online Boutique consists of a 10-tier microservices application. The application is a web-based e-commerce app where users can browse items, add them to the cart, and purchase them.
- Online Boutique is composed of 11 microservices written in different languages that talk to each other over gRPC. 
- Go C#  Node.js Python Java
- Tool Skaffold
https://github.com/GoogleCloudPlatform/microservices-demo.git





# Referencias

Orchestration vs Choreography

Articulo que menciona los terminos, con productos de google

In Orchestration, a central service defines and controls the flow of communication between services. With centralization, it becomes easier to change and monitor the flow and apply consistent timeout and error policies. 

In Choreography, each service registers for and emits events as they need. There’s usually a central event broker to pass messages around, but it does not define or direct the flow of communication. This allows services that are truly independent at the expense of less traceable and manageable flow and policies. 

https://cloud.google.com/blog/topics/developers-practitioners/better-service-orchestration-workflows

Microservice Architecture and its 10 Most Important Design Patterns
Microservice Architecture, Database per Microservice, Event Sourcing, CQRS, Saga, BFF, API Gateway, Strangler, Circuit Breaker, Externalize Configuration, Consumer-Driven Contract Testing
https://towardsdatascience.com/microservice-architecture-and-its-10-most-important-design-patterns-824952d7fa41

 Microservice Architecture with Spring Boot, Spring Cloud and Docker 
https://github.com/sqshq/piggymetrics


Microservices in Practice - Key Architectural Concepts of an MSA 
 
8. Deployment

When it comes to MSA, the deployment of microservices plays a critical role and has the following key requirements:

    Ability to deploy/undeploy independently of other microservices
    Must be able to scale at each microservices level (a given service may get more traffic than other services)
    Deploying microservices quickly
    Failure in one microservice must not affect any of the other services

https://wso2.com/whitepapers/microservices-in-practice-key-architectural-concepts-of-an-msa/

Microservice Principles: Smart Endpoints and Dumb Pipes
Types of Microservice Communication
- Request-Response
- Observer
https://medium.com/@nathankpeck/microservice-principles-smart-endpoints-and-dumb-pipes-5691d410700f


The microservices architectures (or microservices) are increasingly closer to becoming a standard when developing applications and systems. Microservices allow complex applications to be broken down into small independent components, improving systems and facilitating maintenance and new integrations.
https://www.chakray.com/ebooks/learn-implement-architecture-microservices/



Building Twelve Factor Apps with .Net Core
the methodology is based on twelve tenants that provide a mixture of process guidelines and implementation detail. Although the methodology has been around for years, it’s only really been achievable in the Microsoft world since the advent of .Net Core.
https://www.ben-morris.com/building-twelve-factor-apps-with-net-core/

An A-to-Z guide to a microservices architecture transition
This comprehensive guide to microservices explains everything: comparisons to monolithic architectures, the role of APIs and containers, and design and deployment best practices.
https://searchapparchitecture.techtarget.com/An-A-to-Z-guide-to-a-microservices-architecture-transition


All You Need to Know About Microservices Database Management
5 common data-related patterns and 1 anti-pattern

- The Database-per-Service pattern
- The Saga pattern 
- The API Composition pattern
- The CQRS pattern
- The Event Sourcing pattern
- The Shared Database anti-pattern
https://relevant.software/blog/microservices-database-management/


 A curated list of Microservice Architecture related principles and technologies. 
https://github.com/mfornos/awesome-microservices



Panel: What Have We Learned over the Last Decade of Microservices?

"the essence of what a microservice architecture is about is not really technical or infrastructure related. It's all about correctly identifying service boundaries, service responsibilities, their APIs, their collaborations." 
https://www.infoq.com/presentations/panel-microservices-architecture/


# Libros

Microservices Patterns. -Chris Richardson

# Revisar

"smart microservices, dumb pipes” mantra 

In SOA implementations, the inter-service communication between services is facilitated with an Enterprise Service Bus(ESB) and most of the business logic resides in the intermediate layer (message routing, transformation and orchestration). However, Microservices architecture promotes to eliminate the central message bus/ESB and move the ‘smart-ness’ or business logic to the services and client(known as ‘Smart Endpoints’).

Since microservices use standard protocols such as HTTP, JSON etc. the requirement of integrating with disparate protocol is minimal when it comes to the communication among microservices. Another alternative approach in Microservice communication is to use a lightweight message bus or gateway with minimal routing capabilities and just acting as a ‘dumb pipe’ with no business logic implemented on gateway. Based on these styles there are several communication patterns that has emerged in microservices architecture.

 
Sidecar vs. Chassis for Microservices 
https://dzone.com/articles/ms-chassis-pattern
 
 
"Es buena idea implementar microservicios sin conocer el dominio?. Considerando que la separacion de un dominio en microservicios, su conocimiento es importante."

"Incluso si conocer el dominio, existe elementos que siempre o se recomienda que sean microservicios temas autentificacion, autorizacion" 


***Process for building a microservices architecture***

- Domain analysis. To avoid some common pitfalls when designing microservices, use domain analysis to define your microservice boundaries

- Design the services. Microservices require a different approach to designing and building applications. 

- Operate in production. Because microservices architectures are distributed, you must have robust operations for deployment and monitoring


Composite UIs for Microservices

-    A primer
-    Composition options
-    Client composition
-    Server composition
-    Data composition
-    Vertical Slice APIs
https://jimmybogard.com/composite-uis-for-microservices-vertical-slice-apis/


# Ideas/Conceptualizaciones


Porque .net core facilita microservicios?. Para ejecutarse no requiere un componente centralizado tipo IIS sino posee un componente integrado para exponerse. Kestrel server.  Esto facilita la aplicaciones independientes que fluye en arquitectura microservicios. Ademas son cross-platform. . Tambien Spring Boot, a popular Java-based  microservices framework, lets you build microservices as self contained, self-executable JAR files. 

The twelve-factor app” es una metodología para construir aplicaciones SaaS. VII. Asignación de puertos. Las aplicaciones “twelve factor” son completamente auto-contenidas y no dependen de un servidor web en ejecución para crear un servicio web público. 