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


# Installation

Prefect requires Python 3.7+.
https://docs.prefect.io/core/getting_started/install.html


Archivo docker-compose para levantar manualmente todos los servicios. 
- Este archivo docker-compose posee instrucciones healthcheck
https://github.com/PrefectHQ/prefect/blob/master/src/prefect/cli/docker-compose.yml

# Architecture

Prefect Server Architecture
- UI: the user interface that provides a visual dashboard for mutating and querying metadata
- Apollo: the main endpoint for interacting with the server
- PostgreSQL: the database persistence layer where metadata is stored
- Hasura: the GraphQL API that layers on top of Postgres for querying metadata
- GraphQL: the server's business logic that exposes GraphQL mutations
- Towel: runs utilities that are responsible for server maintenance
  - Scheduler: schedules and creates new flow runs
  - Zombie Killer: marks task runs as failed if they fail to heartbeat
  - Lazarus: reschedules flow runs that maintain an unusual state for a period of time
https://docs.prefect.io/orchestration/server/architecture.html

# Prefect 2.0


https://orion-docs.prefect.io/

Introducing Prefect 2.0 
- Prefect 2.0 is loaded with new features and built on top of our second-generation orchestration engine, Orion. 
- Prefect 2.0 is actually its license: Apache 2.0 
- Prefect 2.0 introduces a completely new UI.
https://www.prefect.io/blog/introducing-prefect-2-0

# Referencias

Airflow Vs. Prefect — Workflow management for Data projects
- Both are built using python. 
- Workflows are an integral part of many production-related applications. It ranges from MLOps, automation, batch processing, portfolio tracking, etc. One system needs to process and send data to another system/task in sequential order in the data world.
- Airflow has a learning curve. You need to understand DAG and the different operators in it.
- Prefect is as simple as a primary python function. We need to wrap it under a with the flow.
- Versioned Workflows. An essential feature of any code-based system is the ability to version your code.
https://towardsdatascience.com/airflow-vs-prefect-workflow-management-for-data-projects-5d1a0c80f2e3
