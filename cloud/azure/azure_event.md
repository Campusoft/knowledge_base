# Azure Event Hubs

Azure Event Hubs es un motor de streaming de eventos de varios protocolos que admite de forma nativa los protocolos AMQP, Apache Kafka y HTTPs. Como es compatible con Apache Kafka, puede traer cargas de trabajo de Kafka a Azure Event Hubs sin hacer ningún cambio de código. 

Previous
Next
Azure Event Hub vs. Azure Service Bus: A comprehensive comparison
Azure Event Hub vs. Azure Service Bus: A comprehensive comparison
AUTHOR : Romil Kansara   POSTED : August 17th, 2023
linkedin sharing buttontwitter sharing buttonfacebook sharing button
Azure
When we talk about messaging and event-driven architectures in the Azure ecosystem, two popular services stand out: Azure Event Hub and Azure Service Bus. While both services offer reliable messaging capabilities, they have distinct features and use cases. In this blog post, we’ll explore the core differences between Azure Event Hub and Azure Service Bus and delve into their key components and usage scenarios.

What is Azure Event Hub?
Azure Event Hub is a fully managed event streaming platform that enables the collection, storage and analysis of massive amounts of data. This data can be generated by applications, devices and IoT endpoints. It is designed for high-throughput scenarios, making it ideal for real-time event processing and big data streaming.

Event Hub follows a “pub/sub” model, where events are published to the hub and multiple consumers can process the events concurrently. With its partitioning and consumer group capabilities, Event Hub provides scalability and load balancing.

Here, are the key components of Event Hub:


Producers:
- The maximum size of a single event or a batch of events is 1 MB. Events larger than this threshold will be rejected.

Consumer group:
- Consumer groups enable multiple applications or services to independently consume events from a single Event Hub. Each consumer group maintains its own offset, allowing different applications to progress at their own pace.

Partitions:
- Event Hub divides the event stream into multiple partitions. Each partition is an ordered sequence of events. Multiple consumer instances can read from different partitions in parallel, providing high scalability and throughput.


# Referencias

Azure Event Hubs vs Service Bus
- If you are implementing a pattern which uses a Request/Reply message, then probably you should use Azure Service Bus.
- Azure Event Hubs is more likely to be used if you’re implementing patterns with Event Messages.
https://www.serverless360.com/blog/azure-event-hubs-vs-service-bus