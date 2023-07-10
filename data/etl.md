# ETL

Data Pipeline vs ETL
While the terms “data pipeline” and ETL are often used interchangeably, there are some key differences between the two. Unlike traditional ETL systems, data pipelines don’t have to move data in batches. Data can be processed in real-time (or streamed) through a data pipeline. In addition, a data pipeline might not transform the data at all but simply move it across systems.

Types of Data Pipelines Solutions

Data pipelines can be categorized based on the type of analytics and workflows they support. Thus, the two main types of data pipelines are batch processing, which is used for traditional analytics, and streaming, which is used for real-time analytics.


# Seleccionar herramientas ETL / seleccionar procesos


Best Practices to Design a Data Ingestion Pipeline
- Compare data ingestion tools
  - Data connectors available (Shopify, Azure Blob, Facebook, NetSuite, etc.) 
  - Capabilities of your team (time to set-up, time to maintain, skillset, etc.)
  - Budget (monthly costs to ingest expected volume of data)
  - Support (Slack communities, dedicated support agents, etc.)
- Document your data ingestion pipeline sources
- Keep a copy of all raw data in your warehouse at all times
https://airbyte.com/blog/best-practices-data-ingestion-pipeline

# AWS Data Pipeline 

# Apache Airflow

Airflow.md

# Prefect 

Prefect.md

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

# Pentaho Data Integration ( ETL ) a.k.a Kettle


# AWS Glue

El primer paso de un proyecto de análisis o ML consiste en preparar los datos para obtener resultados de calidad. AWS Glue es un servicio de integración de datos sin servidor que vuelve más fácil, rápido y barato el proceso de preparación de datos. Puede detectar y conectarse a más de 70 orígenes de datos distintos, administrar los datos en un catálogo centralizado, además de visualizar, crear, ejecutar y supervisar las canalizaciones de ETL para cargar los datos en los lagos de datos correspondientes
https://aws.amazon.com/es/glue/


--------------
Amazon Web Services (AWS) is a Niche Player in this Magic Quadrant; it is a new entrant this year. It is headquartered in Seattle, Washington. It offers AWS Glue as its data integration tool. Its customers use AWS Glue when their primary target data stores reside on the AWS cloud. Gartner estimates thousands of customers using AWS Glue data integration jobs and catalog service. Its operations are global and across various sectors.
Strengths

- Native integration reduces complexity and improves interoperability within the AWS data ecosystem: AWS Glue’s native integration with other AWS cloud services (like S3, Redshift, Athena and Lake Formation) makes it the most suitable data integration tool choice when selecting an AWS-native data ecosystem.
- Innovation to support data delivery demands: AWS Glue scores well on innovation for its serverless data integration service that goes beyond scaling infrastructure for data processing. It supports ML-based data cleansing and PII detection at scale. It even extends data processing for other applications (such as Notebooks and IDEs) through its interactive sessions API.
- Centralizes metadata in its data catalog: AWS Glue offers a rich set of metadata capabilities such as cataloging, search, lineage, data quality checks, PII detection and data access logging for auditability. For example, AWS Glue metadata can be accessed within Athena for exploring the underlying data in S3.

Cautions

- Limited connectors beyond AWS-native services: AWS Glue has a wide range of native connectors from the AWS marketplace for on-premises and other clouds. However, for certain non-AWS sources (like Mainframes, SAP, Azure or GCP) customers will need third-party options, some of which are available from AWS Marketplace (like CData, tCVision and Qlik) to compensate for this deficit.
- Steep learning curve: AWS Glue is not friendly to beginners — according to Gartner Peer Insights reviews, its product documentation and sample code libraries are not enough for complex data engineering tasks. There is a high reliance on technical expertise for coding (Python or Scala) and debugging, and implementing an enterprise integration framework requires expertise in serverless architectures. In addition to recent product improvements, AWS Glue has added no-code tools like Glue Studio and Glue Databrew to mitigate these challenges.
- Practitioners report several operational issues: Gartner Peer Insights reviews highlight several operational issues such as high startup times for cluster spin and new jobs, lack of idle timeout settings leading to high costs, and occasional performance bottlenecks during peak hours of data processing. However, the latest AWS Glue 3.0 release should alleviate most of these issues.

 Magic Quadrant for Data Integration Tools	
https://www.gartner.com/doc/reprints?id=1-2AWA2A6O&ct=220822&st=sb	

# SQL Server Integration Services

SQL Server Integration Services is a platform for building enterprise-level data integration and data transformations solutions. Use Integration Services to solve complex business problems by copying or downloading files, loading data warehouses, cleansing and mining data, and managing SQL Server objects and data.

# Azure Data Factory - ADF


Microsoft positions ADF and SSIS for technical roles with its data engineering capabilities, which include curating trusted data assets and managing the full life cycle of data pipelines. It positions Power Query for citizen roles with its core data preparation capabilities and ease of use to simplify data access.


Azure Data Factory es un servicio en la nube administrado creado para estos complejos proyectos híbridos de extracción, transformación y carga (ETL), extracción, carga y transformación (ELT) e integración de datos.

Organización, supervisión y administración del rendimiento de la canalización

El mantenimiento de canalizaciones con un panorama de datos de rápida evolución puede llevar mucho tiempo, lo que implica intervenciones manuales. En Azure Data Factory, no solo puede supervisar visualmente todas las ejecuciones de actividad, sino que también puede mejorar la productividad operativa mediante la configuración de alertas de forma proactiva para supervisar las canalizaciones. Estas alertas pueden aparecer en los grupos de alertas de Azure, para garantizar que pueda recibir notificaciones puntuales para evitar problemas descendentes o ascendentes antes de que se produzcan.



Azure Data Factory. Disfrute del único servicio totalmente compatible que facilita el traslado de todos los paquetes de SSIS a la nube.

This is an infographic speaking to the 5 benefits of migrating SSIS to ADF in the cloud.
- 
https://azure.microsoft.com/es-mx/resources/benefits-of-migrating-ssis-to-the-cloud/



# Talend

[Mas informacion talend](/talend/talend.md)

# Apache Hop

The Hop Orchestration Platform, or Apache Hop, aims to facilitate all aspects of data and metadata orchestration.

Hop is an open-source data integration tool, which is a fork of Pentaho Data Integration (PDI) or Kettle. It offers a visual development tool that can make developers more productive and less daunting for those who prefer building their pipelines without writing any code. Hop workflow and pipeline can be run on various engines including its own native Hop engine, Spark, Flink, Google Dataflow, or AWS EMR through Beam. Hop is one of the first GUI based designers out there for building Apache Beam pipeline.


## Apache Hop Architecture 
https://hop.apache.org/docs/architecture/



# Apache Beam

Apache Beam is an open source, unified model for defining both batch and streaming data-parallel processing pipelines. Using one of the open source Beam SDKs, you build a program that defines the pipeline. The pipeline is then executed by one of Beam’s supported distributed processing back-ends, which include Apache Flink, Apache Spark, and Google Cloud Dataflow.

## SDKs

Beam supports multiple language-specific SDKs for writing pipelines against the Beam Model.

Currently, this repository contains SDKs for Java, Python and Go




# RudderStack 

RudderStack is a stand-alone system dependent only on a database (PostgreSQL). Its backend is written in Go, with a rich UI written in React.js.

RudderStack's architecture consists of 2 major components: the control plane and data plane

Control plane

The control plane offers a UI to configure your event data sources and destinations. It consists of:

- RudderStack web app: The front-end application where you set up and configure your data pipelines in RudderStack.
- Configuration backend: The web app leverages this module to store all the relevant information around your configured sources, destinations, and the connections between them.
	
Data plane

The data plane (backend) is RudderStack's core engine responsible for:

- Receiving and processing event data
- Transforming events in the required destination format
- Relaying events to the destination

The RudderStack data plane consists of three major components:

- RudderStack server (rudder-server)
- Transformations module
- Standalone streaming database (PostgreSQL) for event data
	
https://www.rudderstack.com/docs/static/a188699e9ddc1f56f525fa14a08bac80/aa440/rudderstack-backend-architecture.png	
	
https://www.rudderstack.com/docs/get-started/rudderstack-architecture/

## licensing

The core of RudderStack - the components that make up our Event Stream feature - is open source.

A majority of RudderStack’s third-party destination integrations live in the rudder-transformer repository. They are open source as well, licensed under the MIT License.

Features licensed under our enterprise license include:

- Reverse ETL
- ETL
- Transformations
- Event Replay
- SSO (Single Sign-On)
https://www.rudderstack.com/blog/rudderstacks-licensing-explained/




# Reverse ETL

## grouparoo

[Mas informacion grouparoo](/platform_campusoft/referencias/sync.grouparoo.md)

## Castled

Castled is a Reverse ETL tool which enables you to perodically sync the data from a source, the public cloud warehouse where you store all your data, to a destination which is your favorite operational tool there by enabling the sales,marketing or service teams.

https://docs.castled.io/

El repositorio github es vacio. (mayo-2022)
https://github.com/orgs/castledio/repositories


## Hightouch

Hightouch is a Data Activation platform that connects and orchestrates data from sources to business tools. The platform manages the varying integrations and logic to activate data models from sources.
https://hightouch.io/

Models
In order for Hightouch to know what data to sync, a Model is defined. A Model organizes elements of data to be queried from a data source. For most Sources, a Model is defined with SQL; Hightouch sends the SQL directly to the Source to query data. Alternatively, a Model can be defined with dbt Models or Looker Looks to leverage existing data models.

Hightouch’s Visual Audience Builder can be used to segment a Model to build audiences or cohorts of data (with no code) before syncing the data to a destination tool. This process creates an Audience that generates SQL that acts as a segmented Model.

Regardless of how a Model is built, it is configured with a unique Primary Key that is used by Hightouch to search and keep track of records. This is important to ensure HIghtouch is only syncing new and updated data to a destination tool. How Hightouch manages difference checking will be covered in Diffing.

A single Model can be configured with multiple Syncs to different Destinations. For example, a Model containing customer data is commonly configured to sync between Sales (ie Salesforce), Marketing (ie Iterable), and Support (ie Zendesk) tools. Doing so enables all business tools to leverage the same source of truth.


# dbt 

dbt enables analytics engineers to transform data in their warehouses by simply writing select statements. dbt handles turning these select statements into tables and views.

dbt does the T in ELT (Extract, Load, Transform) processes – it doesn’t extract or load data, but it’s extremely good at transforming data that’s already loaded into your warehouse.


Models are written in the mix of SQL and JINJA templating which is friendly for non-data engineers, while makes it quite expressive and fairly easy for data engineers

Materializations are strategies for persisting dbt models in a warehouse. There are four types of materializations built into dbt. They are: - table- view- incremental - ephemeral

# Referencias

Airflow, Prefect, and Dagster: An Inside Look
https://towardsdatascience.com/airflow-prefect-and-dagster-an-inside-look-6074781c9b77


**Revisiones**

https://www.getcensus.com/
