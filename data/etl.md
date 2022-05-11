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

## Referencias

How to do Change Data Capture (CDC), using Singer
https://www.startdataengineering.com/post/cdc-using-singer/

# Reverse ETL

## grouparoo

[Mas informacion grouparoo](/platform_campusoft/referencias/sync.grouparoo.md)

## Castled

Castled is a Reverse ETL tool which enables you to perodically sync the data from a source, the public cloud warehouse where you store all your data, to a destination which is your favorite operational tool there by enabling the sales,marketing or service teams.

https://docs.castled.io/