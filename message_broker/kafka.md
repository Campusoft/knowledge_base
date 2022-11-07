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

# Architecture


Apache Kafka in detail
https://mikeldeltio.com/2020/05/20/distributed-databases-kafka/

# Command

Apache Kafka Quickstart

Create a topic to store your events:
```
$ kafka-topics --create --topic quickstart-events --bootstrap-server localhost:9092
```

Show  details  of one topic: 
```
kafka-topics --describe --topic quickstart-events --bootstrap-server localhost:9092
```

Read the events
```
kafka-console-consumer --topic quickstart-events --from-beginning --bootstrap-server localhost:9092
```

Cluster name

https://kafka.apache.org/quickstart 


Listar los topics:

```
$ kafka-topics --list --bootstrap-server localhost:9092
```

# Client


 Confluent's Apache Kafka .NET client 
 https://github.com/confluentinc/confluent-kafka-dotnet/
 
 
# Install


There are two options to install Apache Kafka using Confluent Platform :

- Local: https://docs.confluent.io/platform/current/quickstart/ce-quickstart.html
- Docker: https://docs.confluent.io/platform/current/quickstart/ce-docker-quickstart.html
 
 
Indica las diferentes opciones de docker-compose existents
https://docs.confluent.io/platform/current/tutorials/build-your-own-demos.html?utm_source=github&utm_medium=demo&utm_campaign=ch.examples_type.community_content.cp-all-in-one
 
 
Docker (Proveedor Alternativo). bitnami
- Posee zookeeper
- kafka

https://hub.docker.com/r/bitnami/kafka

# Schema Registry

Confluent Schema Registry provides a serving layer for your metadata. It provides a RESTful interface for storing and retrieving your Avro®, JSON Schema, and Protobuf schemas.

 
# Kafka Streams

Kafka Streams se basa en la mensajería de Kafka para permitir procesar datos en tiempo real. Pero mientras un productor Kafka sólo publica datos en un topic, y un consumidor únicamente consume datos de topics, las aplicaciones Kafka Streams pueden utilizar uno o varios topics como entrada, realizar algún tipo de transformación o procesado de esos datos y dejar el resultado como salida en otro u otros topics.



# Kafka Connectors

Kafka Connect is a framework for connecting Kafka with external systems such as databases, key-value stores, search indexes, and file systems, using so-called Connectors.

Kafka Connect is a free, open-source component of Apache Kafka® that works as a centralized data hub for simple data integration between databases, key-value stores, search indexes, and file systems. 

A source connector collects data from a system. Source systems can be entire databases, streams tables, or message brokers. A source connector could also collect metrics from application servers into Kafka topics, making the data available for stream processing with low latency.

A sink connector delivers data from Kafka topics into other systems, which might be indexes such as Elasticsearch, batch systems such as Hadoop, or any kind of database.


Inside Kafka Connect

- Connectors are responsible for the interaction between Kafka Connect and the external technology being integrated with
- Converters handle the serialization and deserialization of data
- Transformations can optionally apply one or more transformations to the data passing through the pipeline

## Converters

Converters are responsible for the serialization and deserialization of data flowing between Kafka Connect and Kafka itself.  Common converters include:

- JSON
- Avro
- Protobuf

## Transformations - Single Message Transforms (SMTs)

The third key component with Kafka Connect is Single Message Transforms (SMT). Unlike connectors and converters, these are entirely optional. You can use them to modify data from a source connector before it is written to Kafka, and modify data read from Kafka before it’s written to the sink.

For more complex transformations, including aggregations and joins to other topics or lookups to other systems, a full stream processing layer in ksqlDB or Kafka Streams is recommended.


## Connectors

**tasks**


Kafka Connect Deep Dive – JDBC Source Connector
https://www.confluent.io/blog/kafka-connect-deep-dive-jdbc-source-connector/


Add Connectors or Software
create a Docker image that extends from one of Confluent’s Kafka Connect images but which contains a custom set of connectors.
https://docs.confluent.io/home/connect/self-managed/extending.html

## Error Handling in Kafka Connect

Kafka Connect supports several error-handling patterns, including fail fast, silently ignore, and dead letter queues.
https://developer.confluent.io/learn-kafka/kafka-connect/error-handling-and-dead-letter-queues/


# REST Proxy API


Confluent REST Proxy API Reference
https://docs.confluent.io/platform/current/kafka-rest/api.html


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


AKHQ
AKHQ is a GUI for monitoring & managing Apache Kafka topics, topics data, consumers group etc.
https://akhq.io/


Open-Source Web GUI for Apache Kafka Management
Kafka UI for Apache Kafka is a simple tool that makes your data flows observable, helps find and troubleshoot issues faster and deliver optimal performance. Its lightweight dashboard makes it easy to track key metrics of your Kafka clusters - Brokers, Topics, Partitions, Production, and Consumption. 
https://github.com/provectus/kafka-ui

This repository is a collection of tools for working with Apache Kafka.
https://github.com/linkedin/kafka-tools


# Hosting  Cloud

Serverless Data for Kafka
- Free Max 10,000 Messages Daily
- No permite crear topic con los clientes. (Se debe crear en panel upstash)
https://upstash.com/#section-pricing


https://confluent.cloud/welcome




# ksqlDB

ksqlDB is a database purpose-built to help developers create stream processing applications on top of Apache Kafka®.

ksqlDB can be described as a real-time event-streaming database built on top of Apache Kafka and Kafka Streams. It combines powerful stream processing with a relational database model using SQL syntax.


ksqlDB enables you to build event streaming applications leveraging your familiarity with relational databases. Three categories are foundational to building an application: collections, stream processing, and queries.

What use cases is ksqlDB a good fit for?

- Materialized cache. Build and serve incrementally-updated stateful views.
- Streaming ETL pipeline. Manipulate in-flight data to connect arbitrary sources and sinks.
- Event-driven microservices. Trigger changes based on observed patterns of events in a stream.

https://ksqldb.io/


Interacting with ksqlDB
- Command Line Interface (CLI). ksqlDB CLI
- Web UI. One of the simplest ways to interact with ksqlDB is via the web UI in Confluent Cloud.
- REST API. The ksqlDB REST API provides programmatic access to ksqlDB for managing objects and querying them
- Client Libraries

## Use Case


ksqlDB allows us to read, filter, transform, or otherwise process streams and tables of events, which are backed by Kafka topics. 

Some key ksqlDB use cases include:

- Materialized caches
- Streaming ETL pipelines
- Event-driven microservices


## Examples

Transforming Data with ksqlDB
ksqlDB allows you to transform events in one stream and then send them to a new stream.
https://developer.confluent.io/learn-kafka/ksqldb/transform-data/#transforming-data-with-ksqldb

Stateful Aggregations (Materialized Views)
ksqlDB can filter, join, and enrich events as they happen. But you may wish to look beyond individual events to a composite view.
https://developer.confluent.io/learn-kafka/ksqldb/stateful-aggregations/



## ksqlDB CLI

Si se utilizo docker-compose para instalar ksqlDb.

-  ksqldb-server. Es el nombre del servicio en el archivo docker-compose.yml para la imagen "cp-ksqldb-server"

```
docker exec -it ksqldb-cli ksql http://ksqldb-server:8088
```

## Clients

ksqlDb.RestApi.Client is a C# LINQ-enabled client API for issuing and consuming ksqlDB push queries and executing statements. SqlServer.Connector is a client API for consuming row-level table changes (CDC - Change Data Capture) from Sql Server databases with the Debezium connector streaming platform. 
 
https://github.com/tomasfabian/ksqlDB.RestApi.Client-DotNet
 

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

