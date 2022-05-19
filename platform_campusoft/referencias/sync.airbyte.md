# airbyte

Technical Stack

- Airbyte Core Backend
- Java 17
- Framework: Jersey
- API: OAS3
- Databases: PostgreSQL
- Unit & E2E testing: JUnit 5
- Orchestration: Temporal
  https://docs.airbyte.io/understanding-airbyte/tech-stack


At Airbyte, we use Java as our main language for backend development, Gradle as our build tool, jOOQ as the object-relational mapping library (ORM), and Flyway to manage database migrations.


High-level View
A high level view of Airbyte's components.

- Airbyte is conceptually composed of two parts: platform and connectors. The platform provides all the horizontal services required to configure and run data movement operations e.g: the UI, configuration API, job scheduling, logging, alerting, etc. and is structured as a set of microservices.

- Connectors are independent modules which push/pull data to/from sources and destinations. Connectors are built in accordance with the Airbyte Specification, which describes the interface with which data can be moved between a source and a destination using Airbyte. Connectors are packaged as Docker images, which allows total flexibility over the technologies used to implement them.

https://docs.airbyte.io/understanding-airbyte/high-level-view

# Airbyte Specification

The specification is Docker-based; this allows a developer to write a connector in any language they want.

## Source

A source is implemented as a Docker container. 
Existe 4 commandos

### Command Spec

The objective of the spec command is to pull information about how to use a source. The ConnectorSpecification contains this information

Como se llama a la imagen de docker, para ejecutar el commando
 
```
docker run --rm -i <source-image-name> spec
```

Ejemplo

```
docker run --rm -i airbyte/source-hubspot spec
```

```
docker run --rm -i airbyte/source-woocommerce spec
```

**ConnectorSpecification**

- The UI reads the JsonSchema in this field in order to render the input fields for a user to fill in.
- This JsonSchema is also used to validate that the provided inputs are valid. e.g. If port is one of the fields and the JsonSchema in the connectorSpecification specifies that this filed should be a number, if a user inputs "airbyte", they will receive an error. Airbyte adheres to JsonSchema validation rules.

## AirbyteCatalog

An AirbyteCatalog describes the structure of data in a data source. It has a single field called streams that contains a list of AirbyteStreams. Each of these contain a name and json_schema field. The json_schema field accepts any valid JsonSchema and describes the structure of a stream. This data model is intentionally flexible.

# Licencias

Airbyte Licensing Overview
- Airbyte Core is licensed under the Elastic License 2.0 (ELv2).
- Airbyte Protocol is open sourced and available under the MIT License.
https://docs.airbyte.com/project-overview/licenses/license-faq

# Connector

## HubSpot

Supported Streams
https://docs.airbyte.com/integrations/sources/hubspot/#supported-streams


# Install



# Revisiones

Airbyte is an open-source data integration platform capable of moving data from OLTP databases such as MySQL to destinations such as Apache Kafka using change data capture (CDC) with low latency.


# Campusoft.Sync

airbyte posee un workflow orchestration 
https://airbyte.com/blog/scale-workflow-orchestration-with-temporal
