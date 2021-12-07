# Event-Bus

ABP Framework provides two type of event buses;

- Local Event Bus is suitable for in-process messaging.
- Distributed Event Bus is suitable for inter-process messaging, like microservices publishing and subscribing to distributed events.
https://docs.abp.io/en/abp/latest/Event-Bus


# Local Event

Si existe dos handler, uno general "Change", y otro especifico "Creacion", unicamente se ejecuta el evento general. 

ILocalEventHandler<EntityChangedEventData<Plantilla>>
ILocalEventHandler<EntityCreatedEventData<Plantilla>>


# Kafka 

Distributed Event Bus Kafka Integration
https://docs.abp.io/en/abp/latest/Distributed-Event-Bus-Kafka-Integration


TODO:
- La integracion con kafka no lanza error, realiza reintento infinito.. 


TODO:
- Como trabajar con  confirmation (ACK).  



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



# Revision

***Personalizar***

IDistributedEventBus 