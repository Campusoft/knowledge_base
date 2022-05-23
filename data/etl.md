# ETL

Data Pipeline vs ETL
While the terms “data pipeline” and ETL are often used interchangeably, there are some key differences between the two. Unlike traditional ETL systems, data pipelines don’t have to move data in batches. Data can be processed in real-time (or streamed) through a data pipeline. In addition, a data pipeline might not transform the data at all but simply move it across systems.

Types of Data Pipelines Solutions

Data pipelines can be categorized based on the type of analytics and workflows they support. Thus, the two main types of data pipelines are batch processing, which is used for traditional analytics, and streaming, which is used for real-time analytics.


# Seleccionar herramientas ETL / seleccionar procesos


Best Practices to Design a Data Ingestion Pipeline
- ‍Compare data ingestion tools
  - Data connectors available (Shopify, Azure Blob, Facebook, NetSuite, etc.) 
  - Capabilities of your team (time to set-up, time to maintain, skillset, etc.)
  - Budget (monthly costs to ingest expected volume of data)
  - Support (Slack communities, dedicated support agents, etc.)
- Document your data ingestion pipeline sources
- Keep a copy of all raw data in your warehouse at all times
https://airbyte.com/blog/best-practices-data-ingestion-pipeline


# Apache Airflow

Airflow.md

# Prefect 

https://www.prefect.io/


Prefect Server is an open source backend that makes it easy to monitor and execute your Prefect flows. 

Prefect Agents are lightweight processes for orchestrating flow runs. Agents run inside a user's architecture, and are responsible for starting and monitoring flow runs. During operation the agent process queries the Prefect API for any scheduled flow runs, and allocates resources for them on their respective deployment platforms.

Prefect supports several different agent types for deploying on different platforms.

- Local: The Local Agent executes flow runs as local processes.
- Docker: The Docker Agent executes flow runs in docker containers.
- Kubernetes: The Kubernetes Agent executes flow runs as Kubernetes Jobs
- GCP Vertex: The Vertex Agent executes flow runs as Vertex Custom Jobs
- AWS ECS: The ECS Agent executes flow runs as AWS ECS tasks
(on either ECS or Fargate).


##  Installation

Prefect requires Python 3.7+.
https://docs.prefect.io/core/getting_started/install.html


Archivo docker-compose para levantar manualmente todos los servicios. 
- Este archivo docker-compose posee instrucciones healthcheck
https://github.com/PrefectHQ/prefect/blob/master/src/prefect/cli/docker-compose.yml

##  Architecture

Prefect Server Architecture
- PostgreSQL
- Hasura
- Apollo
- Towel
https://docs.prefect.io/orchestration/server/architecture.html

## Prefect 2.0


https://orion-docs.prefect.io/

Introducing Prefect 2.0 
- Prefect 2.0 is loaded with new features and built on top of our second-generation orchestration engine, Orion. 
https://www.prefect.io/blog/introducing-prefect-2-0

## Referencias

Airflow Vs. Prefect — Workflow management for Data projects
- Both are built using python. 
- Workflows are an integral part of many production-related applications. It ranges from MLOps, automation, batch processing, portfolio tracking, etc. One system needs to process and send data to another system/task in sequential order in the data world.
- Airflow has a learning curve. You need to understand DAG and the different operators in it.
- Prefect is as simple as a primary python function. We need to wrap it under a with the flow.
- Versioned Workflows. An essential feature of any code-based system is the ability to version your code.
https://towardsdatascience.com/airflow-vs-prefect-workflow-management-for-data-projects-5d1a0c80f2e3


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

**Hightouch**

Hightouch is a Data Activation platform that connects and orchestrates data from sources to business tools. The platform manages the varying integrations and logic to activate data models from sources.
https://hightouch.io/

Models
In order for Hightouch to know what data to sync, a Model is defined. A Model organizes elements of data to be queried from a data source. For most Sources, a Model is defined with SQL; Hightouch sends the SQL directly to the Source to query data. Alternatively, a Model can be defined with dbt Models or Looker Looks to leverage existing data models.

Hightouch’s Visual Audience Builder can be used to segment a Model to build audiences or cohorts of data (with no code) before syncing the data to a destination tool. This process creates an Audience that generates SQL that acts as a segmented Model.

Regardless of how a Model is built, it is configured with a unique Primary Key that is used by Hightouch to search and keep track of records. This is important to ensure HIghtouch is only syncing new and updated data to a destination tool. How Hightouch manages difference checking will be covered in Diffing.

A single Model can be configured with multiple Syncs to different Destinations. For example, a Model containing customer data is commonly configured to sync between Sales (ie Salesforce), Marketing (ie Iterable), and Support (ie Zendesk) tools. Doing so enables all business tools to leverage the same source of truth.


# Referencias

Airflow, Prefect, and Dagster: An Inside Look
https://towardsdatascience.com/airflow-prefect-and-dagster-an-inside-look-6074781c9b77