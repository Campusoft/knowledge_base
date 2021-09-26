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


# Database per Microservice

Here I am using the term database to show a logical separation of data, i.e., the Microservices can share the same physical database, but they should use separate Schema/collection/table

## data separation

- What happens when a service developed by a team requires a change of schema in a database shared by other services?


Read Only Data Aggregation In a Microservices Environment: A Real Life Use Case
- Have an incrementing version id of your entity to support stale data detection.
https://www.wix.engineering/post/read-only-data-aggregation-in-a-microservices-environment-a-real-life-use-case

- Event/subscription model

A good set of events must be integrated into the generating microservice and lost events are a possibility


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



## Resiliency / resiliente / Resistencia

Una app resiliente es una que continúa funcionando a pesar de tener fallas en los componentes del sistema. La resiliencia requiere planificación en todos los niveles de la arquitectura. Influye en el diseño de la infraestructura y la red, y en el diseño de la app y el almacenamiento de datos. La resiliencia también se extiende a las personas y la cultura.

## Event driven: 

En este patrón un microservicio publica un evento y otro microservicio lo consumirá.

## Aggregator o Proxy

Un cliente web necesita información que esta en varios microservicios. En este caso se invoca a un microservicio que agrega las llamadas a otros microservicios para obtener la respuesta.

## Asynchronous messaging


## Patrón Saga

El Patrón Saga es una secuencia de transacciones locales donde cada transacción actualiza información dentro de un servicio.

## Sidecar pattern  (Pronu: saidecar)


## Backends for Frontends pattern

Create separate backend services to be consumed by specific frontend applications or interfaces.
https://docs.microsoft.com/en-us/azure/architecture/patterns/backends-for-frontends

## Mediator 

Behavioral Pattern 

- Less coupling: Since the classes don’t have dependencies on each other, they are less coupled.

## CQRS 

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

***Traces***

Traces—or more precisely, “distributed traces”—are samples of causal chains of events (or transactions) between different components in a microservices ecosystem. And like events and logs, traces are discrete and irregular in occurrence.

***Metrics***
 
To put it simply, metrics are numeric measurements. Metrics can include:
- A numeric status at a moment in time (like CPU % used)
- Aggregated measurements (like a count of events over a one-minute time, or a rate of events-per-minute)
	


***OpenTelemetry***


An observability framework for cloud-native software.

OpenTelemetry is a collection of tools, APIs, and SDKs. You can use it to instrument, generate, collect, and export telemetry data (metrics, logs, and traces) for analysis in order to understand your software's performance and behavior.



***Zipkin***

Zipkin is a distributed tracing system. It helps gather timing data needed to troubleshoot latency problems in service architectures. Features include both the collection and lookup of this data.

Architecture Overview
https://zipkin.io/pages/architecture.html


# Audit Trail

Microservices shall generate and pass through microservice  call chain  a correlation  ID which  uniquely  identify  every  call  chain  and  help  grouping log messages to investigate them.

Logging  agent  shall  include  correlation  ID  in  every log message

# Authentication and Authorization

Microservices Authentication and Authorization Solutions
- 1. Distributed Session Management
- 2. Client Token
- 3. Single sign-on
- 4. Client Token with API Gateway
- 5. Third-party application access
https://medium.com/tech-tajawal/microservice-authentication-and-authorization-solutions-e0e5e74b248a

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

# Migration

Strategy
- Strategy 2 – Split Frontend and Backend
- Strategy 3 – Extract Services
- Strategy 1 – Stop Digging



8 Steps for Migrating Existing Applications to Microservices 
https://insights.sei.cmu.edu/blog/8-steps-for-migrating-existing-applications-to-microservices/


Migrating Monoliths to Microservices with Decomposition and Incremental Changes
https://www.infoq.com/articles/migrating-monoliths-to-microservices-with-decomposition/ 

Refactoring a Monolith into Microservices
- Strategy 1 – Stop Digging
- Strategy 2 – Split Frontend and Backend
- Strategy 3 – Extract Services
https://www.nginx.com/blog/refactoring-a-monolith-into-microservices/ 


Implement new functionality as services

A good way to begin the migration to microservices is to implement significant new functionality as services. This is sometimes easier than breaking apart of the monolith. It also demonstrates to the business that using microservices significantly accelerates software delivery.

Extract services from the monolith

While implementing new functionality as services is extremely useful, the only way of eliminating the monolith is to incrementally extract modules out of the monolith and convert them into services.
https://microservices.io/refactoring/


Application modernization patterns with Apache Kafka, Debezium, and Kubernetes 
- Menciona porque es importante utilizar Sidecar pattern
https://developers.redhat.com/articles/2021/06/14/application-modernization-patterns-apache-kafka-debezium-and-kubernetes#after_the_migration__modernization_challenges


Step 3: Migrate the database
- Having a separate database requires data synchronization. Once again, we can choose from a few common technology approaches.
  - Triggers
  - Queries. The changes are typically detected with implementation strategies such as timestamps, version numbers, or status column changes in the source database
  - Log readers


# Service Discovery

Today, most companies in the software industry are moving towards cloud platforms such as AWS, google cloud, and Azure. When your services are running in the cloud, you can scale up or down at any moment by utilizing vertical or horizontal scaling processes. You are only charged for the usage of the purchased resources. So, it is important to purchase and utilize in a cost-effective manner. This means you are dealing with a highly dynamic environment, where services can have different server addresses from time to time. 

"the server endpoints are subject to frequent change": Cloud, Microservices, traffic increases


important aspect of microservices: discovery

#  Ejemplos 


- All these services are deployed into the same monolithic runtime (such as application servers). When one service requires more CPU, while the other requires more memory. (monolith 



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

# Revisar

"smart microservices, dumb pipes” mantra 
 
Sidecar vs. Chassis for Microservices 
https://dzone.com/articles/ms-chassis-pattern
 
 
"Es buena idea implementar microservicios sin conocer el dominio?. Considerando que la separacion de un dominio en microservicios, su conocimiento es importante."

"Incluso si conocer el dominio, existe elementos que siempre o se recomienda que sean microservicios temas autentificacion, autorizacion" 


***Process for building a microservices architecture***

- Domain analysis. To avoid some common pitfalls when designing microservices, use domain analysis to define your microservice boundaries

- Design the services. Microservices require a different approach to designing and building applications. 

- Operate in production. Because microservices architectures are distributed, you must have robust operations for deployment and monitoring

