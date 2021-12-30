# Kafka 
 
 Kafka is a distributed streaming platform with very good horizontal scaling capability. It allows applications to process and re-process streamed data on disk. Due to it's high throughput it's commonly used for real-time data streaming.
 
 The message queue in Kafka is persistent. The data sent is stored until a specified retention period has passed, either a period of time or a size limit. The message stays in the queue until the retention period/size limit is exceeded, meaning the message is not removed once it’s consumed. Instead, it can be replayed or consumed multiple times, which is a setting that can be adjusted. 
 
 Apache Kafka uses Apache ZooKeeper to maintain and coordinate the Apache Kafka brokers.
 
# Use Cases


Apache Kafka provides the broker itself and has been designed towards stream processing scenarios. Recently, it has added Kafka Streams, a client library for building applications and microservices. Apache Kafka supports use cases such as metrics, activity tracking, log aggregation, stream processing, commit logs and event sourcing.

The following messaging scenarios are especially suited for Kafka:

-    Streams with complex routing, throughput of 100K/sec events or more, with “at least once” partitioned ordering
-    Applications requiring a stream history, delivered in “at least once” partitioned ordering. Clients can see a ‘replay’ of the event stream.
-    Event sourcing, modeling changes to a system as a sequence of events.
-    Stream processing data in multi-stage pipelines. The pipelines generate graphs of real-time data flows.


Kafka vs. RabbitMQ: Architecture, Performance & Use Cases
https://www.upsolver.com/blog/kafka-versus-rabbitmq-architecture-performance-use-case


-    Messaging: Kafka is used for messaging. It works better than the traditional message brokers. Twitter uses it for sending and receiving tweets.
-   Stream processing: Many application using Kafka are using Kafka beyond messaging. Raw data consumed from Kafka is aggregated, enriched and sent to another Kafka topic. For example, Kafka is used for building real time recommendation system.
-    Website activity tracking: At Linkedin, rebuilding user activity tracking pipeline as a real time publish subscribe feeds was the original use case for Kafka. Website activity such as page views, searches are published to Kafka. These feeds are available for subscription by a range of use cases such as real time processing, monitoring and ingesting into Hadoop.
-    Metrics collection: Kafka is often used for operational monitoring data.
-    Log Aggregation: Kafka is used to collect logs from multiple servers. Thus make it available in standard format for multiple consumers.
-    Event sourcing: Event sourcing is a style of application, where state changes are logged in a time-order sequence of records. Kafka would be a best fit as a backend for this kind of applications.
-    Commit log: Kafka can be used as an external commit log for distributed systems. The log helps in replicating the data between nodes and acts as a re-syncing mechanism for failed nodes to restore their data.
Use cases of Kafka
https://medium.com/@jhansireddy007/use-cases-of-kafka-bf7b6d3ce4e2
	


When to use RabbitMQ over Kafka
https://stackoverflow.com/questions/42151544/when-to-use-rabbitmq-over-kafka


# Conceptos


Streams record history. Tables represent state.
https://cdn.confluent.io/wp-content/uploads/streams-record-history.gif


***Topics***

Topics in Kafka are always multi-producer and multi-subscriber: a topic can have zero, one, or many producers that write events to it, as well as zero, one, or many consumers that subscribe to these events. Events in a topic can be read as often as needed—unlike traditional messaging systems, events are not deleted after consumption. Instead, you define how long Kafka should retain your events through a per-topic configuration setting, after which old events are discarded. Kafka’s performance is effectively constant with respect to data size, so storing data for a long time is perfectly fine.

***Consumer Groups***

Intro to Kafka - Consumer groups. (Posee ilustraciones para explicar concepto de grupos de consumidores)
- Intro to Kafka - Topics and partitions
- Intro to Kafka - Producers
- Intro to Kafka - Consumers
- Intro to Kafka - Consumer groups
- Intro to Kafka - Ordering related records
https://lankydan.dev/intro-to-kafka-consumer-groups

***Partitions***

 Kafka generates a hash of the key and maps it to a specific partition (i.e., all messages produced with a given key reside on the same partition). Partitions are the primary mechanism in Kafka for parallelizing consumption and scaling a topic beyond the throughput limits of a single node. Each  partition can be hosted in different nodes


# Components

***Zookeeper***

Kafka uses Zookeeper for most of the coordination tasks


Removing the Apache ZooKeeper Dependency
https://www.confluent.io/blog/removing-zookeeper-dependency-in-kafka/



# Command

Apache Kafka Quickstart

Create a topic to store your events:
```
$ bin/kafka-topics.sh --create --topic quickstart-events --bootstrap-server localhost:9092
```

Show  details  of one topic: 
```
kafka-topics.sh --describe --topic quickstart-events --bootstrap-server localhost:9092
```

Read the events
```
kafka-console-consumer.sh --topic quickstart-events --from-beginning --bootstrap-server localhost:9092
```

Cluster name

https://kafka.apache.org/quickstart 


Listar los topics:

```
$ kafka-topics.sh --list --bootstrap-server localhost:9092
```

# Client


 Confluent's Apache Kafka .NET client 
 https://github.com/confluentinc/confluent-kafka-dotnet/
 
 
# Install


There are two options to install Apache Kafka using Confluent Platform :

- Local: https://docs.confluent.io/platform/current/quickstart/ce-quickstart.html
- Docker: https://docs.confluent.io/platform/current/quickstart/ce-docker-quickstart.html
 
 
Docker (Proveedor Alternativo). bitnami
- Posee zookeeper
- kafka

https://hub.docker.com/r/bitnami/kafka
 
# Kafka Streams


# Tools


Confluent REST APIs
The Confluent REST Proxy provides a RESTful interface to a Apache Kafka® cluster, making it easy to produce and consume messages, view the state of the cluster, and perform administrative actions without using the native Kafka protocol or clients.
https://docs.confluent.io/platform/current/kafka-rest/index.html


This article provides an overview of such UI tools, including:
- AKHQ
- Kowl
- Kafdrop
- UI for Apache Kafka
- Lenses
- CMAK
- Confluent CC
- Conduktor
https://towardsdatascience.com/overview-of-ui-tools-for-monitoring-and-management-of-apache-kafka-clusters-8c383f897e80

Open-Source Web GUI for Apache Kafka Management 
https://github.com/provectus/kafka-ui

This repository is a collection of tools for working with Apache Kafka.
https://github.com/linkedin/kafka-tools

# Referencias

The Cloud Vendors provide alternative solutions for Kafka’s storage layer. These solutions include Azure Event Hubs, and to some extent, AWS Kinesis Data Streams. There are also cloud-specific and open source alternatives to Kafka’s stream processing capabilities

 
 
Top 5 Things Every Apache Kafka Developer Should Know 
https://www.confluent.io/blog/5-things-every-kafka-developer-should-know/
 
 
My Python/Java/Spring/Go/Whatever Client Won’t Connect to My Apache Kafka Cluster in Docker/AWS/My Brother’s Laptop. Please Help!
https://www.confluent.io/blog/kafka-client-cannot-connect-to-broker-on-aws-on-docker-etc/


 
 
# Revisiones


Apache Kafka: Pull-based approach

Kafka uses a pull model. Consumers request batches of messages from a specific offset. Kafka permits long-pooling, which prevents tight loops when there is no message past the offset.

A pull model is logical for Kafka because of its partitions. Kafka provides message order in a partition with no contending consumers. This allows users to leverage the batching of messages for effective message delivery and higher throughput.

***ksqlDB***

- streaming database like ksqlDB

ksqlDB
The database purpose-built for stream processing applications.

ksqlDB enables you to build event streaming applications leveraging your familiarity with relational databases. Three categories are foundational to building an application: collections, stream processing, and queries.



What use cases is ksqlDB a good fit for?


Materialized cache
Build and serve incrementally-updated stateful views.


Streaming ETL pipeline
Manipulate in-flight data to connect arbitrary sources and sinks.


Event-driven microservices
Trigger changes based on observed patterns of events in a stream.

https://ksqldb.io/