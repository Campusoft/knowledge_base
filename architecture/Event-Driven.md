# Event-Driven - An event-driven architecture (EDA)
 
Los eventos son sucesos o cambios considerables en el estado del hardware o el software de un sistema. Un evento y su notificación no son lo mismo: la segunda es un mensaje que el sistema envía para comunicar a otra parte del sistema que se produjo cierto evento.

Esta arquitectura está compuesta por consumidores y productores de eventos. El productor detecta los eventos y los representa como mensajes. No conoce al consumidor del evento ni el resultado que generará este último. 

Una vez que se detecta un evento, este se transmite del productor a los consumidores a través de canales de eventos, donde se procesa de manera asíncrona con una plataforma para este fin. Ni bien se produce un evento, se debe informar a los consumidores, quienes podrían procesarlo o simplemente verse afectados por él. 


An event driven architecture can use a pub/sub model or an event stream model.

-    Pub/sub: The messaging infrastructure keeps track of subscriptions. When an event is published, it sends the event to each subscriber. After an event is received, it cannot be replayed, and new subscribers do not see the event.

-    Event streaming: Events are written to a log. Events are strictly ordered (within a partition) and durable. Clients don't subscribe to the stream, instead a client can read from any part of the stream. The client is responsible for advancing its position in the stream. That means a client can join at any time, and can replay events.


