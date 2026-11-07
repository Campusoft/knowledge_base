# Wolverine 



Wolverine is a toolset for command execution and message handling within .NET Core applications. The killer feature of Wolverine (we think) is its very efficient command execution pipeline that can be used as:

- An inline "mediator" pipeline for executing commands
- A local message bus within .NET applications
- A full fledged asynchronous messaging framework for robust communication and interaction between services when used in conjunction with low level messaging infrastructure tools like RabbitMQ,
- With the WolverineFx.Http library, Wolverine's execution pipeline can be used directly as an alternative ASP.Net Core Endpoint provider


Wolverine uses a naming convention to automatically discover message handler actions in your application assembly. In the Handle() method, the first argument is always assumed to be the message type for the handler action. 



https://wolverine.netlify.app/



There's certainly some value in Wolverine just being a command bus running inside of a single process, Wolverine also allows you to both publish and process messages received through external infrastructure like Rabbit MQ or Pulsar.


# Message Handlers

Rules for Message Handlers
- Handler type names should be suffixed with either Handler or Consumer
- Handler method names should be either Handle() or Consume()
- The first argument of the handler method must be the message type

https://wolverine.netlify.app/guide/handlers/#rules-for-message-handlers



Assembly Discovery

https://wolverine.netlify.app/guide/handlers/discovery.html#assembly-discovery

# Transports

- Rabbit MQ
- Azure Service Bus
- Amazon SQS
- Sql Server Transport
- Kafka

# Durable Inbox and Outbox Messaging


