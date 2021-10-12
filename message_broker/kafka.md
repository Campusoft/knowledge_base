# Kafka 
 
 Kafka is a distributed streaming platform with very good horizontal scaling capability. It allows applications to process and re-process streamed data on disk. Due to it's high throughput it's commonly used for real-time data streaming.
 
 
 The message queue in Kafka is persistent. The data sent is stored until a specified retention period has passed, either a period of time or a size limit. The message stays in the queue until the retention period/size limit is exceeded, meaning the message is not removed once it’s consumed. Instead, it can be replayed or consumed multiple times, which is a setting that can be adjusted. 
 
 
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
 

# Client


 Confluent's Apache Kafka .NET client 
 https://github.com/confluentinc/confluent-kafka-dotnet/
 


# Referencias

The Cloud Vendors provide alternative solutions for Kafka’s storage layer. These solutions include Azure Event Hubs, and to some extent, AWS Kinesis Data Streams. There are also cloud-specific and open source alternatives to Kafka’s stream processing capabilities

 
 
# Revisiones


Apache Kafka: Pull-based approach

Kafka uses a pull model. Consumers request batches of messages from a specific offset. Kafka permits long-pooling, which prevents tight loops when there is no message past the offset.

A pull model is logical for Kafka because of its partitions. Kafka provides message order in a partition with no contending consumers. This allows users to leverage the batching of messages for effective message delivery and higher throughput.
