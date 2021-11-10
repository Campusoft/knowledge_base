# Microservices with .net 

# Service-to-service communication

When constructing a cloud-native application, you'll want to be sensitive to how back-end services communicate with each other. Ideally, the less inter-service communication, the better. However, avoidance isn't always possible as back-end services often rely on one another to complete an operation.


***Service Aggregator Pattern***
Another option for eliminating microservice-to-microservice coupling is an Aggregator microservice

***service mesh***

A service mesh is a software layer that handles service-to-service communication

Right now, the main options for a service mesh in Kubernetes are linkerd and Istio.

***Referencias***
Service-to-service communication
https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/service-to-service-communication

# Tools


tye

Tye is a developer tool that makes developing, testing, and deploying microservices and distributed applications easier. Project Tye includes a local orchestrator to make developing microservices easier and the ability to deploy microservices to Kubernetes with minimal configuration.


# Referencias

outbox pattern
Improving microservices reliability – part 3: Outbox Pattern in action
https://www.davidguida.net/improving-microservices-reliability-part-3-outbox-pattern-in-action
