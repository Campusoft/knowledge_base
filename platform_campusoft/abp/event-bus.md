# Event-Bus

ABP Framework provides two type of event buses;

- Local Event Bus is suitable for in-process messaging.
- Distributed Event Bus is suitable for inter-process messaging, like microservices publishing and subscribing to distributed events.
https://docs.abp.io/en/abp/latest/Event-Bus


# Local Event

Si existe dos handler, uno general "Change", y otro especifico "Creacion", unicamente se ejecuta el evento general. 

ILocalEventHandler<EntityChangedEventData<Plantilla>>
ILocalEventHandler<EntityCreatedEventData<Plantilla>>

# Distributed Event Bus

Distributed Event bus system allows to publish and subscribe to events that can be transferred across application/service boundaries. 
https://docs.abp.io/en/abp/latest/Distributed-Event-Bus

- Los objetos "Eto", deben tener el mismo namespace, para que los suscriptores recepten los eventos emitidos con este "Eto" 

***Event Name***


https://docs.abp.io/en/abp/latest/Distributed-Event-Bus#event-name

Revisar.
- GenericEventNameAttribute
```
[GenericEventName(Postfix = ".Created")]
```


***Exception Handling***

When an exception occurs, it will retry every three seconds up to the maximum number of retries(default is 3) and move to dead letter queue, you can change the number of retries, retry interval and dead letter queue name:

https://docs.abp.io/en/abp/latest/Distributed-Event-Bus#exception-handling


# Kafka 

Distributed Event Bus Kafka Integration
https://docs.abp.io/en/abp/latest/Distributed-Event-Bus-Kafka-Integration

- En kafka, si existe 3 mensajes de diferentes ETO. Y unicamnete un ETO existe en manejador los otros 2 ETOS se ignoran y se consideran procesados. 

TODO:
- La integracion con kafka no lanza error, realiza reintento infinito.. 


TODO:
- Como trabajar con  confirmation (ACK).  



***Detalles Implementacion***

Utiliza el tipo mensaje Message<string, byte[]>. 
- key: Nombre Evento, el cual puede ser establecido como atributo en ETO, si no existe atributo se utilizar el tipo de la clase ETO. Ej. Si nombre evento es el tipo de una clase. Mre.Sb.Notificacion.EmailNotificacionEto
- Value: Primero el objeto es serializado a JSON, luego convertido a bytes



***Configuracion***

You can use any of the ClientConfig properties as the connection properties.

```
{
  "Kafka": {
    "Connections": {
      "Default": {
        "BootstrapServers": "123.123.123.123:9092",
        "SocketTimeoutMs": 60000
      }
    }
  }
}

```

Errores
------------------------

No finaliza el proceso al momento publicar un evento con Kafka.

Se espera un tiempo algo. 
Luego lanza error 
```
Local: Message timed out
```

```
%3|1634274264.037|FAIL|rdkafka#producer-18| [thrd:8c5f15226541:9092/1001]: 8c5f15226541:9092/1001: Failed to resolve '8c5f15226541:9092': No such host is known.  (after 2297ms in state CONNECT)
%3|1634274264.038|ERROR|rdkafka#producer-18| [thrd:app]: rdkafka#producer-18: 8c5f15226541:9092/1001: Failed to resolve '8c5f15226541:9092': No such host is known.  (after 2297ms in state CONNECT)
%3|1634274267.360|FAIL|rdkafka#producer-18| [thrd:8c5f15226541:9092/1001]: 8c5f15226541:9092/1001: Failed to resolve '8c5f15226541:9092': No such host is known.  (after 2309ms in state CONNECT, 1 identical error(s) suppressed)
```

Solucion:
Si se utiliza la imagen docker "bitnami/kafka", utilizar docker-compose.yml que se encuentra en la seccion  "Kafka development setup example"

-----------------------------

***Revisiones Kafka***

IKafkaSerializer 


# Revision

***Personalizar***

IDistributedEventBus 