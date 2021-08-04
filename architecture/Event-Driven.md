# Event-Driven - An event-driven architecture (EDA)
 
Los eventos son sucesos o cambios considerables en el estado del hardware o el software de un sistema. Un evento y su notificación no son lo mismo: la segunda es un mensaje que el sistema envía para comunicar a otra parte del sistema que se produjo cierto evento.

Esta arquitectura está compuesta por consumidores y productores de eventos. El productor detecta los eventos y los representa como mensajes. No conoce al consumidor del evento ni el resultado que generará este último. 

Una vez que se detecta un evento, este se transmite del productor a los consumidores a través de canales de eventos, donde se procesa de manera asíncrona con una plataforma para este fin. Ni bien se produce un evento, se debe informar a los consumidores, quienes podrían procesarlo o simplemente verse afectados por él. 


An event driven architecture can use a pub/sub model or an event stream model.

-    Pub/sub: The messaging infrastructure keeps track of subscriptions. When an event is published, it sends the event to each subscriber. After an event is received, it cannot be replayed, and new subscribers do not see the event.

-    Event streaming: Events are written to a log. Events are strictly ordered (within a partition) and durable. Clients don't subscribe to the stream, instead a client can read from any part of the stream. The client is responsible for advancing its position in the stream. That means a client can join at any time, and can replay events.


An event-driven API must offer two capabilities to its consumers.

- A mechanism to allow consumers to subscribe to events of their interest.
- Deliver events to subscribed consumers in an asynchronous manner.
	
# Technology 
	
- Webhooks
- WebSockets
- Server-Sent Events (SSE)

## Webhooks

A Webhook is a publicly accessible HTTP POST endpoint managed by an event consumer. An event producer, such as an API server, can send event notifications to a webhook when something interesting happens.

Consumers can subscribe to your API by registering a Webhook URL as the callback.

Moreover, Webhooks can’t be used to push event notifications to end-user consumers such as mobile and Single Page Applications (SPA) as they can not in possession of an HTTP endpoint.

Despite the above drawbacks, Webhooks can still be ideal for implementing a server-to-server event notification mechanism.

# Documenting 

A good API definition is complemented by comprehensive documentation and a set of language-specific code generators. REST APIs are meeting that need with the OpenAPI specification. Fortunately, for event-driven APIs, we have the AsyncAPI specification.

The AsyncAPI specification is a machine-readable document that documents and describes your event-driven APIs. It is not only just a specification but a rich ecosystem full of code generators, validators, and test generators.

AsyncAPI is designed along with the same elements of OpenAPI and shares many common constructs to simplify the adoption, but it also comes with additional features to accommodate eventing. It supports a wide variety of messaging protocols and transports (such as AMQP, MQTT, WebSockets, Kafka, JMS, STOMP, HTTP, etc.) and event schema formats. Therefore, the API definition will contain the event payload definition, channel name, application/transport headers, protocols, and other eventing semantics to connect, publish, and subscribe to the API.




# Frameworks 

Dapr is a portable, event-driven runtime that makes it easy for any developer to build resilient, stateless, and stateful applications that run on the cloud and edge and embraces the diversity of languages and developer frameworks

# Messaging patterns

**Claim Check**

Store the entire message payload into an external service, such as a database. Get the reference to the stored payload, and send just that reference to the message bus. The reference acts like a claim check used to retrieve a piece of luggage, hence the name of the pattern. Clients interested in processing that specific message can use the obtained reference to retrieve the payload, if needed.
https://www.enterpriseintegrationpatterns.com/patterns/messaging/StoreInLibrary.html

**Pipes and Filters**

Decompose a task that performs complex processing into a series of separate elements that can be reused. This can improve performance, scalability, and reusability by allowing task elements that perform the processing to be deployed and scaled independently.


Una tubería (pipeline o cauce) consiste en una cadena de procesos conectados de forma tal que la salida de cada elemento de la cadena es la entrada del próximo. Permiten la comunicación y sincronización entre procesos. Es común el uso de búfer de datos entre elementos consecutivos. 


**Guidance**
 
Message design is not object-oriented design. Messages should contain state, not behavior. Behavior should be in a separate class or service.



# Revision (TODO)

Well, that is the basis of “Event-enabled” APIs. They are often called “asynchronous,” “push,” or “streaming




# Referencias

Event-driven APIs — Understanding the Principles
https://medium.com/event-driven-utopia/event-driven-apis-understanding-the-principles-c3208308d4b2

	
	