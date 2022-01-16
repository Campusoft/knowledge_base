# Kafka for .net 



# Confluent.Kafka



Getting Started with Apache Kafka and .NET
Step-by-step guide to building a .NET client application for Kafka
https://developer.confluent.io/get-started/dotnet/


El objeto para publicar mensajes es Message<TKey, TValue>. El cual define una clave, y un valor de un tipo especifico.


***Configuration***

bootstrap.servers is a comma-separated list of host and port pairs that are the addresses of the Kafka brokers in a "bootstrap" Kafka cluster that a Kafka client connects to initially to bootstrap itself.

A host and port pair uses : as the separator.

- localhost:9092
- localhost:9092,another.host:9092


