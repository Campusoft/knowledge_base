# MassTransit


MassTransit is a lightweight service bus for building distributed .NET applications. 
The main goal is to provide a consistent, .NET friendly abstraction over the message transport (whether it is RabbitMQ, Azure Service Bus, etc.). 
To meet this goal, MassTransit brings a lot of the application-specific logic closer to the developer in an easy to configure and understand manner.

An open-source lightweight message bus framework for .NET. MassTransit is useful for routing messages over MSMQ, RabbitMQ, TIBCO, and ActiveMQ service busses, with native support for MSMQ and RabbitMQ. MassTransit also supports multicast, versioning, encryption, sagas, retries, transactions, distributed systems, and other features.


https://masstransit-project.com/

##  Producers

 If your message goes to a known receiver, just send it. You should publish your message when you have multiple receivers or the number of receivers is dynamically changing.
 

https://masstransit-project.com/usage/producers.html

## Consumer


## Sagas

In MassTransit, a saga is a stateful consumer that allows multiple messages to be correlated to a single consumer instance. 
A saga, sometimes called a workflow, is typically used to orchestrate other consumers or services in a distributed system while maintaining state between messages.
MassTransit persists a saga's state using a saga repository, and automatically creates a new or uses an existing state instance as messages are received.


##  Containers

MassTransit supports several dependency injection containers.



## Laboratorios

### publisher/subscriber 

Para configurar un subscriber, utilizar la siguiente configuracion. Con el metodo "ReceiveEndpoint", pasar como parametro el host que conecta al broker message, con esta configuracion se creara una suscripcion para mensajes de tipo "GradeBook.Core.Application.GradeImportNotificationDto" que esta configurado en "GradeImportNotificationConsumer"

MassTransit, creara una cola asociada a "GradeBook.Core.Application.GradeImportNotificationDto", para recibir las publicaciones a este mensaje 

```
 var host = cfg.Host(hostAddress, configure =>
                {
                    configure.Username(TestUsername);
                    configure.Password(TestPassword);

                    //h.UseSsl();
                });

                 
                cfg.ReceiveEndpoint(host, configure =>
                {

                    configure.Consumer(typeof(GradeImportNotificationConsumer), f =>
                    {
                        return new GradeImportNotificationConsumer(output);
                    });

                });
```


![imagen](https://user-images.githubusercontent.com/222181/106158378-a9908c80-6151-11eb-9a88-6420100c5941.png)

GradeImportNotificationConsumer
![imagen](https://user-images.githubusercontent.com/222181/106158719-0a1fc980-6152-11eb-884f-7d08158e1a20.png)


Para evitar que cada inicio proceso de este consumer, cree una nueva cola "con nombres diferentes", se puede establecer el nombre de la cola en "ReceiveEndpoint", este nombre sera concatenado al tipo "GradeBook.Core.Application.GradeImportNotificationDto"

![imagen](https://user-images.githubusercontent.com/222181/106159039-64208f00-6152-11eb-9954-cdc9fc98cd08.png)


Referencias
A simple Pub/Sub Scenario with MassTransit 6.2 + RabbitMQ +.NET Core 3.1 + Elasticsearch + MSSQL
https://medium.com/@alikzlda/a-simple-pub-sub-scenario-with-masstransit-6-2-rabbitmq-net-core-3-1-elasticsearch-mssql-5a65c993b2fd


# Referencias

MassTransit with ASP.Net Core 2.1
https://ngohungphuc.wordpress.com/2018/07/22/masstransit-with-asp-net-core-2-1/
 


Obtener configuracion (Informacion endpoint)
https://masstransit-project.com/troubleshooting/show-config.html