# ETL

Data Pipeline vs ETL
While the terms “data pipeline” and ETL are often used interchangeably, there are some key differences between the two. Unlike traditional ETL systems, data pipelines don’t have to move data in batches. Data can be processed in real-time (or streamed) through a data pipeline. In addition, a data pipeline might not transform the data at all but simply move it across systems.

Types of Data Pipelines Solutions

Data pipelines can be categorized based on the type of analytics and workflows they support. Thus, the two main types of data pipelines are batch processing, which is used for traditional analytics, and streaming, which is used for real-time analytics.


# Apache Airflow

Airflow.md


# Singer

Simple, Composable, Open Source ETL

Singer powers data extraction and consolidation for all of your organization’s tools.
https://www.singer.io/


Singer describes how data extraction scripts—called “taps” —and data loading scripts—called “targets”— should communicate, allowing them to be used in any combination to move data from any source to any destination. Send data between databases, web APIs, files, queues, and just about anything else you can think of. 



Why You Should NOT Build Your Data Pipeline on Top of Singer
- The Singer protocol does not specify how an integration should define what inputs it requires.
https://airbyte.com/blog/why-you-should-not-build-your-data-pipeline-on-top-of-singer



Meltano is built on top of Singer, which is open source (AGPL).


Talend (acquirer of StitchData) seems to have stopped investing in maintaining Singer’s community and connectors. As most connectors see schema changes several times a year, more and more Singer’s taps and targets are not actively maintained and are becoming outdated. 

## Referencias

How to do Change Data Capture (CDC), using Singer
https://www.startdataengineering.com/post/cdc-using-singer/

# Reverse ETL

## grouparoo

[Mas informacion grouparoo](/platform_campusoft/referencias/sync.grouparoo.md)

## Castled

Castled is a Reverse ETL tool which enables you to perodically sync the data from a source, the public cloud warehouse where you store all your data, to a destination which is your favorite operational tool there by enabling the sales,marketing or service teams.

https://docs.castled.io/

El repositorio github es vacio. (mayo-2022)
https://github.com/orgs/castledio/repositories

**Revisiones**

https://www.getcensus.com/



**RudderStack**

RudderStack is a stand-alone system dependent only on a database (PostgreSQL). Its backend is written in Go, with a rich UI written in React.js.

RudderStack's architecture consists of 2 major components: the control plane and data plane

https://www.rudderstack.com/docs/get-started/rudderstack-architecture/


RudderStack Open Source. No soportados
- Reverse ETL

