# Kafka for .net 



# Confluent.Kafka



Getting Started with Apache Kafka and .NET
Step-by-step guide to building a .NET client application for Kafka
https://developer.confluent.io/get-started/dotnet/


El objeto para publicar mensajes es Message<TKey, TValue>. El cual define una clave, y un valor de un tipo especifico.

**Commit**

By default, the .NET Consumer will commit offsets automatically. This is done periodically by a background thread at an interval specified by the AutoCommitIntervalMs config property.
https://docs.confluent.io/clients-confluent-kafka-dotnet/current/overview.html#auto-offset-commit


**Configuration**

bootstrap.servers is a comma-separated list of host and port pairs that are the addresses of the Kafka brokers in a "bootstrap" Kafka cluster that a Kafka client connects to initially to bootstrap itself.

A host and port pair uses : as the separator.

- localhost:9092
- localhost:9092,another.host:9092


# Framework 

An Event Stream Processing Micro-Framework for Apache Kafka
https://blog.tonysneed.com/2020/06/25/event-stream-processing-micro-framework-apache-kafka/

# Revisiones

Kafka Resiliency — Retry/Delay Topic, Dead Letter Queue (DLQ)
https://medium.com/@shesh.soft/kafka-resiliency-retry-delay-topic-dead-letter-queue-dlq-fa2434688d22
