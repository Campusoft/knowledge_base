# airbyte



# Architecture

Airbyte is conceptually composed of two parts: platform and connectors.

The platform provides all the horizontal services required to configure and run data movement operations e.g: the UI, configuration API, job scheduling, logging, alerting, etc. and is structured as a set of microservices.

Connectors are independent modules which push/pull data to/from sources and destinations. Connectors are built in accordance with the Airbyte Specification, which describes the interface with which data can be moved between a source and a destination using Airbyte. Connectors are packaged as Docker images, which allows total flexibility over the technologies used to implement them. 
https://docs.airbyte.com/assets/images/understanding_airbyte_high_level_architecture-ac5aa905ca8916521b41cdaecec4cd08.png

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


Why You Should NOT Build Your Data Pipeline on Top of Singer
- The Singer protocol does not specify how an integration should define what inputs it requires.
https://airbyte.com/blog/why-you-should-not-build-your-data-pipeline-on-top-of-singer


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


### Command Check


```
docker run --rm -i <source-image-name> check --config <config-file-path>
```

Para pasar el archivo de configuracion al contenedor utilizar volumnes
```
docker run --rm -v $(pwd)/secrets:/secrets airbyte/source-hubspot check --config /secrets/sample_config.json
```

Windows Test con WSL
- Crear una carpeta local 
- Utilizar el path absoluto a los archivos que se desea colocar en el volumens

```
docker run --rm -v c/temp/secrets:/secrets airbyte/source-hubspot check --config /secrets/sample_config.json
```


### Command Discover

```
docker run --rm -i <source-image-name> discover --config <config-file-path>
```

Ejemplo windows test con WSL
```
docker run --rm -v c/temp/secrets:/secrets airbyte/source-hubspot discover --config /secrets/sample_config.json
```

### Command Read

```
docker run --rm -i <source-image-name> read --config <config-file-path> --catalog <catalog-file-path> [--state <state-file-path>] > message_stream.json
```

docker run --rm -v $(pwd)/secrets:/secrets -v $(pwd)/sample_files:/sample_files airbyte/source-hubspot:dev read --config /secrets/config.json --catalog /sample_files/configured_catalog.json



**ConnectorSpecification**

- The UI reads the JsonSchema in this field in order to render the input fields for a user to fill in.
- This JsonSchema is also used to validate that the provided inputs are valid. e.g. If port is one of the fields and the JsonSchema in the connectorSpecification specifies that this filed should be a number, if a user inputs "airbyte", they will receive an error. Airbyte adheres to JsonSchema validation rules.

## AirbyteCatalog

An AirbyteCatalog describes the structure of data in a data source. It has a single field called streams that contains a list of AirbyteStreams. Each of these contain a name and json_schema field. The json_schema field accepts any valid JsonSchema and describes the structure of a stream. This data model is intentionally flexible.

# Syncing (Sync Modes)


https://docs.airbyte.com/understanding-airbyte/connections/#sync-modes

## Incremental Sync - Append

A cursor is the value used to track whether a record should be replicated in an incremental sync. A common example of a cursor would be a timestamp from an updated_at column in a database table.

A cursor field is the field or column in the data where that cursor can be found. Extending the above example, the updated_at column in the database would be the cursor field, while the cursor is the actual timestamp value used to determine if a record should be replicated.

We will refer to the set of records that the source identifies as being new or updated as a delta.

https://docs.airbyte.com/understanding-airbyte/connections/incremental-append

# Basic Normalization

A core tenet of ELT philosophy is that data should be untouched as it moves through the E and L stages so that the raw data is always accessible. If an unmodified version of the data exists in the destination, it can be retransformed without needing to sync data again.

The normalization rules are not configurable. They are designed to pick a reasonable set of defaults to hit the 80/20 rule of data normalization. We respect that normalization is a detail-oriented problem and that with a fixed set of rules, we cannot normalize your data in such a way that covers all use cases.


- En el proceso se crean dos trablas raw table "_airbyte_raw_<stream name>" y data table "<stream name>". 
- Arrays u objetos seran copiados a otra tabla 
- Basic Normalization expands arrays into separate tables
- If the nested items in the array are not objects then they are expanded into a string field of comma separated values
https://docs.airbyte.com/understanding-airbyte/basic-normalization#rules


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


# Temporal

Temporal is a scalable and reliable runtime for Reentrant Processes called Temporal Workflow Executions.

The Temporal Platform consists of a Temporal Cluster and Worker Processes. Together these components create a runtime for Workflow Executions.

Worker Processes are hosted by you and execute your code. They communicate with a Temporal Cluster via gRPC.

https://docs.temporal.io/




# Revisiones

Airbyte is an open-source data integration platform capable of moving data from OLTP databases such as MySQL to destinations such as Apache Kafka using change data capture (CDC) with low latency.

Scaling data pipelines on Kubernetes
- How to horizontally scale: use Kubernetes.
- Using socat to redirect STDIO between pods in different nodes
- Using the sidecar pattern to create a sidecar socat container
- How to pipe data between our two containers: use named pipes
https://airbyte.com/blog/scaling-data-pipelines-kubernetes


# Campusoft.Sync

airbyte posee un workflow orchestration 
https://airbyte.com/blog/scale-workflow-orchestration-with-temporal
