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


# Platform

## Dapr

Dapr is a portable, event-driven runtime that makes it easy for enterprise developers to build resilient, stateless and stateful microservice applications that run on the cloud and edge and embraces the diversity of languages and developer frameworks.

Developer language SDKs and frameworks
To make using Dapr more natural for different languages, it also includes language specific SDKs for Go, Java, JavaScript, .NET and Python. These SDKs expose the functionality in the Dapr building blocks, such as saving state, publishing an event or creating an actor, through a typed, language API rather than calling the http/gRPC API. This enables you to write a combination of stateless and stateful functions and actors all in the language of their choice. And because these SDKs share the Dapr runtime, you get cross-language actor and functions support

https://dapr.io/


# Referencias

Orchestration vs Choreography

Articulo que menciona los terminos, con productos de google

In Orchestration, a central service defines and controls the flow of communication between services. With centralization, it becomes easier to change and monitor the flow and apply consistent timeout and error policies. 

In Choreography, each service registers for and emits events as they need. There’s usually a central event broker to pass messages around, but it does not define or direct the flow of communication. This allows services that are truly independent at the expense of less traceable and manageable flow and policies. 

https://cloud.google.com/blog/topics/developers-practitioners/better-service-orchestration-workflows