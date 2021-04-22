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


# Database per Microservice

Here I am using the term database to show a logical separation of data, i.e., the Microservices can share the same physical database, but they should use separate Schema/collection/table

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


# Referencias

Orchestration vs Choreography

Articulo que menciona los terminos, con productos de google

In Orchestration, a central service defines and controls the flow of communication between services. With centralization, it becomes easier to change and monitor the flow and apply consistent timeout and error policies. 

In Choreography, each service registers for and emits events as they need. There’s usually a central event broker to pass messages around, but it does not define or direct the flow of communication. This allows services that are truly independent at the expense of less traceable and manageable flow and policies. 

https://cloud.google.com/blog/topics/developers-practitioners/better-service-orchestration-workflows

Microservice Architecture and its 10 Most Important Design Patterns
Microservice Architecture, Database per Microservice, Event Sourcing, CQRS, Saga, BFF, API Gateway, Strangler, Circuit Breaker, Externalize Configuration, Consumer-Driven Contract Testing
https://towardsdatascience.com/microservice-architecture-and-its-10-most-important-design-patterns-824952d7fa41