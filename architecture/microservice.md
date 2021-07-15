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




# Database per Microservice

Here I am using the term database to show a logical separation of data, i.e., the Microservices can share the same physical database, but they should use separate Schema/collection/table

# Pattern


API Gateway / Backends for Frontends
https://microservices.io/patterns/apigateway.html


# Observabilidad 

- Logging
- Trazabilidad en logs
- Alertas


La Era de la Observabilidad
Por qué el futuro es abierto, conectado y programable
(Buen articulo)
"Yuri Shkuro, autor e ingeniero de software de Uber Technologies, explica la diferencia de esta manera: el monitoreo mide lo que uno decide con anterioridad que es importante, mientras que la observabilidad es la capacidad de hacer preguntas que uno desconoce de antemano acerca del sistema."
https://newrelic.com/resources/ebooks/what-is-observability-es


Medición de DevOps: supervisión y observabilidad
- La supervisión es una herramienta o una solución técnica que permite a los equipos observar y comprender el estado de sus sistemas. La supervisión se basa en la recopilación de conjuntos predefinidos de métricas o registros.

- La observabilidad es una herramienta o una solución técnica que permite a los equipos depurar de forma activa su sistema. La observabilidad se basa en la exploración de las propiedades y los patrones que no se definen con anticipación.

https://cloud.google.com/architecture/devops/devops-measurement-monitoring-and-observability?hl=es-419

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
https://www.nginx.com/blog/refactoring-a-monolith-into-microservices/ 


Implement new functionality as services

A good way to begin the migration to microservices is to implement significant new functionality as services. This is sometimes easier than breaking apart of the monolith. It also demonstrates to the business that using microservices significantly accelerates software delivery.

Extract services from the monolith

While implementing new functionality as services is extremely useful, the only way of eliminating the monolith is to incrementally extract modules out of the monolith and convert them into services.

https://microservices.io/refactoring/


Application modernization patterns with Apache Kafka, Debezium, and Kubernetes 
https://developers.redhat.com/articles/2021/06/14/application-modernization-patterns-apache-kafka-debezium-and-kubernetes#after_the_migration__modernization_challenges


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
