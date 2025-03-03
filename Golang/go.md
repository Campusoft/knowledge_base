# Go - Golang

Go, also known as Golang, is an open-source, compiled, and statically typed programming language designed by Google. It is built to be simple, high-performing, readable, and efficient.

# microservice

## Kratos

Kratos is a microservice-oriented governance framework implemented by Golang, which offers convenient capabilities to help you quickly build a bulletproof application from scratch.
https://go-kratos.dev/en/



Features

- Metrics: Prometheus integrated by default. Furthermore, with the uniform metric interfaces, you can implement your own metric system more flexibly.
- Tracing: The OpenTelemetry is conformed to achieve the tracing of microservices chains.
- Transport: The uniform plugins for Middleware are supported by HTTP/gRPC.


**Error Handling**

The design of error handling was settled after a long discussion. The main design concepts are as follows:

- code The semantics are similar to the HTTP Status Code (for example, 400 is used for parameter errors), and it is also used as a major type of error. The advantage is that the gateway layer can trigger corresponding policies (retry, current limit, fuse, etc.) according to this code.
- reason The specific error code of the service. A readable string that should be unique within the same service.
- message Messages are user-readable and can be used as user prompts.
- metadata Meta-information, which adds additional extensible information for errors.
	
https://go-kratos.dev/en/docs/intro/design#error-handling
	
	
# aplicaciones with go


Ory Kratos is the first cloud native Identity and User Management System in the world. Finally, it is no longer necessary to implement a User Login process for the umpteenth time!

https://github.com/ory/kratos


Go Client/Server for Celery Distributed Task Queue
https://github.com/gocelery/gocelery

## Benthos

Benthos is a declarative data streaming service that solves a wide range of data engineering problems with simple, chained, stateless processing steps. It implements transaction based resiliency with back pressure, so when connecting to at-least-once sources and sinks it's able to guarantee at-least-once delivery without needing to persist messages during transit.
https://www.benthos.dev/

Input
- An input is a source of data piped through an array of optional processors:

Processors

- Benthos processors are functions applied to messages passing through a pipeline. The function signature allows a processor to mutate or drop messages depending on the content of the message. There are many types on offer but the most powerful is the bloblang processor.


Outputs

- An output is a sink where we wish to send our consumed data after applying an optional array of processors. Only one output is configured at the root of a Benthos config. However, the output can be a broker which combines multiple outputs under a chosen brokering pattern, or a switch which is used to multiplex against different outputs.
