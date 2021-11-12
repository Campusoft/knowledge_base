# Microservices with .net 

# client-to-microservice communication


Why consider API Gateways instead of direct 

Coupling: Without the API Gateway pattern, the client apps are coupled to the internal microservices. The client apps need to know how the multiple areas of the application are decomposed in microservices. When evolving and refactoring the internal microservices, those actions impact maintenance because they cause breaking changes to the client apps due to the direct reference to the internal microservices from the client apps. Client apps need to be updated frequently, making the solution harder to evolve.

The API Gateway pattern is also sometimes known as the “backend for frontend” (BFF) because you build it while thinking about the needs of the client app.




# Service-to-service communication


In fact, if your internal microservices are communicating by creating chains of HTTP requests as described, it could be argued that you have a monolithic application, but one based on HTTP between processes instead of intra-process communication mechanisms.


When constructing a cloud-native application, you'll want to be sensitive to how back-end services communicate with each other. Ideally, the less inter-service communication, the better. However, avoidance isn't always possible as back-end services often rely on one another to complete an operation.


***Service Aggregator Pattern***
Another option for eliminating microservice-to-microservice coupling is an Aggregator microservice

***service mesh***

A service mesh is a software layer that handles service-to-service communication

Right now, the main options for a service mesh in Kubernetes are linkerd and Istio.

***Referencias***
Service-to-service communication
https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/service-to-service-communication

# Ocelot

Ocelot is a lightweight API Gateway, recommended for simpler approaches. Ocelot is an Open Source .NET Core-based API Gateway especially made for microservices architectures that need unified points of entry into their systems. It’s lightweight, fast, and scalable and provides routing and authentication among many other features


# Tools


***tye***

Tye is a developer tool that makes developing, testing, and deploying microservices and distributed applications easier. Project Tye includes a local orchestrator to make developing microservices easier and the ability to deploy microservices to Kubernetes with minimal configuration.


# Referencias

outbox pattern
Improving microservices reliability – part 3: Outbox Pattern in action
https://www.davidguida.net/improving-microservices-reliability-part-3-outbox-pattern-in-action
