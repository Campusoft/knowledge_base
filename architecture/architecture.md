

 A component is an encapsulated part of a software system. A component has an interface. Components serve as the building blocks for the structure of a system. At a programming-language level, components may be represented as modules, classes, objects or a set of related functions.
 
 
# Recomendaciones

- Se consistente. Si hace algo de cierta manera, haga todas las cosas similares de la misma manera
- Comunicacion.

# Varios

To identify and select the optimal model(s), architects must understand advantages and disadvantages of each model 


# Monolith

Types of monoliths

- modular monolith
- monolith primarily as a unit of deployment.


The problem with this is that people tend to be poor at defining module boundaries — more to the point, even if they are good at defining module boundaries, they are not good at having the discipline to maintain those boundaries. 

Modular Monolith with DDD
https://github.com/kgrzybek/modular-monolith-with-ddd
 
 
# Error handling


# Referencias Arquitecturas .Net (Codigo)

ASP.NET Boilerplate - Web Application Framework 


# Serverless 
How to choose a cloud serverless platform
https://www.infoworld.com/article/3605129/how-to-choose-a-cloud-serverless-platform.html?utm_source=twitter&utm_medium=social&utm_campaign=organic&utm_content=content


# Change Data Capture

data sync problem
 
have microservices using different kinds of databases based on the capabilities of each database

Change data capture (CDC) means identifying and tracking what has changed in a database so that you can take action, like updating your data warehouse or generating other outputs. The idea is to replicate a dataset using incremental updates so that you don’t have to copy your entire database every time a table gets updated.

# Observabilidad 

- Logging
- Trazabilidad en logs
- Alertas

## Metrics

A metric is a measurement about a service, captured at runtime. Logically, the moment of capturing one of these measurements is known as a metric event which consists not only of the measurement itself, but the time that it was captured and associated metadata.

Application and request metrics are important indicators of availability and performance. Custom metrics can provide insights into how availability indicators impact user experience or the business. Collected data can be used to alert of an outage or trigger scheduling decisions to scale up a deployment automatically upon high demand.

## Traces

Traces track the progression of a single request, called a trace, as it is handled by services that make up an application. The request may be initiated by a user or an application. Distributed tracing is a form of tracing that traverses process, network and security boundaries. Each unit of work in a trace is called a span; a trace is a tree of spans. Spans are objects that represent the work being done by individual services or components involved in a request as it flows through a system. A span contains a span context, which is a set of globally unique identifiers that represent the unique request that each span is a part of. A span provides Request, Error and Duration (RED) metrics that can be used to debug availability as well as performance issues.

## Logs

A log is a timestamped text record, either structured (recommended) or unstructured, with metadata.

Structured Logging

## OpenTelemetry 

An observability framework for cloud-native software.

OpenTelemetry is a collection of tools, APIs, and SDKs. You can use it to instrument, generate, collect, and export telemetry data (metrics, logs, and traces) for analysis in order to understand your software's performance and behavior.

OpenTelemetry, which is the next major version of the OpenTracing and OpenCensus projects. The leadership of those two projects have come together to create OpenTelemetry, which combines the best parts of OpenTracing and OpenCensus to create one open source project to help with your instrumentation needs.

OpenTelemetry is a set of APIs, SDKs, tooling and integrations that are designed for the creation and management of telemetry data such as traces, metrics, and logs. The project provides a vendor-agnostic implementation that can be configured to send telemetry data to the backend(s) of your choice. It supports a variety of popular open-source projects including Jaeger and Prometheus.

# Referencias

A proven approach to helping every development organization become an integration agile organization. The Reference Architecture for Agility is a technology neutral logical architecture based on a disaggregated cloud-based model. It can be applied in incremental stages to create an integration agile foundation for any digital enterprises - deployable in private, public or hybrid cloud environments.
(Arquitecturas Referencias)
https://github.com/wso2/reference-architecture


 Full Modular Monolith application with Domain-Driven Design approach. 
https://github.com/kgrzybek/modular-monolith-with-ddd



 A curated list of awesome articles, videos, and other resources to learn and practice about software architecture, patterns, and principles. 
 https://github.com/mehdihadeli/awesome-software-architecture
 
 ## book
 
 Software Engineering at Google

In March, 2020, we published a book titled “Software Engineering at Google” curated by Titus Winters, Tom Manshreck and Hyrum Wright.

The Software Engineering at Google book (“SWE Book”) is not about programming, per se, but about the engineering practices utilized at Google to make their codebase sustainable and healthy. (These practices are paramount for common infrastructural code such as Abseil.)
 https://abseil.io/resources/swe-book