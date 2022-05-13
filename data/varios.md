How to update millions of records in MySQL
https://www.startdataengineering.com/post/update-mysql-in-batch/

Kappa Architecture
The Kappa Architecture is a software architecture used for processing streaming data. The main premise behind the Kappa Architecture is that you can perform both real-time and batch processing, especially for analytics, with a single technology stack. It is based on a streaming architecture in which an incoming series of data is first stored in a messaging engine like Apache Kafka. From there, a stream processing engine will read the data and transform it into an analyzable format, and then store it into an analytics database for end users to query.
https://hazelcast.com/glossary/kappa-architecture/

# jitsu

The Open Source
Segment Alternative
Jitsu is a fully-scriptable data ingestion engine for modern data teams. Set-up a real-time data pipeline in minutes, not days


Jitsu Server Architecture
https://jitsu.com/docs/internals/jitsu-server


Event API

- Jitsu has API for direct event collection. You can use it for sending events directly from apps or backends.

Bulk API

- Jitsu has events bulk API. The endpoint can consume ~50,000 events in one HTTP request and store them into destinations synchronously.

GIF Pixel API

- Jitsu has a GIF Pixel API endpoint for tracking email opens, impressions of advertisements


https://jitsu.com/

## Architecture

Jitsu is written primarily in Go with the frontend written in JavaScript.

## Install


## Revision
Airbyte is an open-source ETL-framework. Jitsu supports Airbyte as an of the connector backend (the other one being Singer and native connectors