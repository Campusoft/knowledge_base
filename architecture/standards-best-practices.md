# Estandares, Buenas Practicas, Listas Checkeo


# Patrones


GOF Design Patterns in C# with working examples inspired by food
https://github.com/wesdoyle/design-patterns-explained-with-food


***SOLID principles***

Single responsibility principle
Open/closed principle
Liskov substitution principle
Interface segregation principle
Dependency inversion principle


Refactoring.Guru te ayuda a descubrir todo lo que necesitas saber sobre la refactorización, los patrones de diseño, los principios SOLID y otros temas de la programación inteligente.
(Posee conceptos, ilustraciones)
https://refactoring.guru/es

## Mediator Pattern

The mediator pattern is a behavioral design pattern that helps to reduce chaotic dependencies between objects. The pattern restricts direct communications between the objects and forces them to collaborate only via a mediator object. Mediator is used to reduce communication complexity between multiple objects or classes. This pattern provides a mediator class which normally handles all the communications between different classes and supports easy maintenance of the code by loose coupling.


## CQRS - Command and Query Responsibility Segregation

The Command and Query Responsibility Segregation (CQRS) pattern propose separating the write data model from the read data model. This separation of responsibilities would provide the flexibility to decide whether the read and write services should coexist in the same data store or be managed in completely different databases.

In the CQRS context, commands are methods whose sole purpose is performing an action. Simply put, commands in our example would be responsible for creating, updating, and deleting tasks (write operations). In the CQRS architecture, commands cannot return data, since that functionality is unique to queries. In that regard, queries are methods that are only able to read and return data without modifying it.

The CQRS pattern is a great expression of the single responsibility principle. It states that we should have separate models for Queries and Commands because in the real-world application the requirements for read operations are normally different than the write operations. If you are using separate models, then you can handle complex scenarios without worrying about disturbing the other operations. Without this separation, we can easily end up with domain models that are full of state, commands, and queries and harder to maintain over time.
 

Command Query Responsibility Segregation (CQRS) pattern
(Excelete articulo, contiene niveles aplicacion del CQRS). 
(Elementos adicionales change data capture (CDC))

- Stage 0: Typical application data access
- Stage 1: Separate read and write APIs
- Stage 2: Separate read and write models
- Stage 3: Separate read and write databases
https://www.ibm.com/cloud/architecture/architectures/event-driven-cqrs-pattern/

***Queries***

Queries are used to get data from the database. Queries objects only return data and do not make any changes.

Queries will only contain the methods for getting data. They are used to read the data in the database to return the DTOs to the client, which will be displayed in the user interface.

***Command***

Commands represent the intention of changing the state of an entity. They execute operations like Insert, Update, Delete. Commands objects alter state and do not return data.

Commands represent a business operation and are always in the imperative tense, because they are always telling the application server to do something.


The command handler is in fact the heart of the application layer in terms of CQRS and DDD. However, all the domain logic should be contained in the domain classes—within the aggregate roots (root entities), child entities, or domain services, but not within the command handler, which is a class from the application layer.

https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api



***Event Sourcing***

Event Sourcing is a design pattern in which results of business operations are stored as a series of events.

It is an alternative way to persist data. In contrast with state-oriented persistence that only keeps the latest version of the entity state, Event Sourcing stores each state change as a separate event.

Thanks for that, no business data is lost. Each operation results in the event stored in the database. That enables extended auditing and diagnostics capabilities (both technically and business-wise). What's more, as events contains the business context, it allows wide business analysis and reporting.



**Refencias**
Can command return a value?
-  generate an identifier In the controller method and create the command based on that. The same identifier can then be returned to the Created status.
- For error handling, I usually make a processing error by throwing an appropriate exception and mapping it to HTTP status, e.g. via middleware
https://event-driven.io/en/can_command_return_a_value/

Tackling Complexity in CQRS
- Complexity Trap #1: One-Way Commands, or Overzealous Segregation
- Complexity Trap #2: Event sourcing
- Complexity Trap #3: Too Much of a Good Thing
- 
https://vladikk.com/2017/03/20/tackling-complexity-in-cqrs/

**Revision**

CQRS and Event Sourcing in Event Driven Architecture of Ordering Microservices
https://medium.com/aspnetrun/cqrs-and-event-sourcing-in-event-driven-architecture-of-ordering-microservices-fb67dc44da7a

- Porque un CQRS commad no retorna valores. Should the Command Handler return a value?
- Should the Command Handler throw an exception?


## Resiliency / resiliente / Resistencia

Una app resiliente es una que continúa funcionando a pesar de tener fallas en los componentes del sistema. La resiliencia requiere planificación en todos los niveles de la arquitectura. Influye en el diseño de la infraestructura y la red, y en el diseño de la app y el almacenamiento de datos. La resiliencia también se extiende a las personas y la cultura.

## Retry pattern

Enable an application to handle transient failures when it tries to connect to a service or network resource, by transparently retrying a failed operation


Before writing custom retry logic, consider using a general framework such as Polly for .NET or Resilience4j for Java.

# Web

The Front-End Checklist is an exhaustive list of all elements you need to have / to test before launching your website / HTML page to production.

https://github.com/thedaviddias/Front-End-Checklist

 Clean Code concepts adapted for TypeScript 
https://github.com/labs42io/clean-code-typescript


# Documentation

5 Steps to Create Technical Documentation That’s (Actually) Helpful
https://plan.io/blog/technical-documentation/


# Database

- Learn as much as you can about problem domain. You can't create good data model without knowing what you're designing for
- Use singular for table names (i.e. use StudentCourse instead of StudentCourses). Table represents a collection of entities, there is no need for plural names.
- Each table must has primary key.
- Natural key vs artificial key (e.g. surrogate key/GUID): First prefer to use the natural key since it's more  meaningful and to avoid duplications (reuse existing column). But there are cases when you need an artificial key: when the natural key is not unique (e.g. names) or if you need to change the value.
- Use separate tables for domain objects rather than cramming them into a single table. This allows proper column types, constraints and relationships. An “Allowed Values” table is just muck and has no place in a data model.
- Consider whether to use Unicode data type  (e.g. NCHAR,NVARCHAR,NTEXT) if you need to support internationalization, but Unicode will take twice as much space. (*****)
- Provide documentation, describe all tables & relationships, DDL. The application programmers should be able to find documentations about any triggers, constraints & store procedures in your database.
- Use application name/domain/department/datasource as prefix (e.g. Billing_tablename), use prefix "General" if the object is non application specific.


Database Guidelines (RDBMS/SQL) 
http://soa-java.blogspot.com/2013/01/database-guidelines-rdbmssql.html
 
Tips for Better Database Design
https://vertabelo.com/blog/9-tips-for-better-database-design/

# Information Standard


***Country***

The International Standard for country codes and codes for their subdivisions

The purpose of ISO 3166 is to define internationally recognized codes of letters and/or numbers that we can use when we refer to countries and their subdivisions. However, it does not define the names of countries – this information comes from United Nations sources (Terminology Bulletin Country Names and the Country and Region Codes for Statistical Use maintained by the United Nations Statistics Divisions).

# REST API 

[Microsoft REST API Guidelines](https://github.com/Microsoft/api-guidelines/blob/vNext/Guidelines.md)


# Varios

## Twelve-factor methodology. (12-factor)

In the modern era, software is commonly delivered as a service: called web apps, or software-as-a-service. The twelve-factor app is a methodology for building software-as-a-service apps that:
https://12factor.net/



## DMN - Decision Model and Notation

The Decision Model and Notation (DMN™) is a Standard by OMG® providing a common and visual notation readily understandable by all users and personas. With DMN, business analysts can define the initial decision requirements and then formalize more detailed decision models; technical developers can automate the decisions in any process with a portable execution semantic, while business stakeholders can manage and monitor those decisions. 

Soporta
- Drools
- Camunda


https://www.omg.org/spec/DMN/
