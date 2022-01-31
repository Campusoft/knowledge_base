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

***Event Sourcing***

Event Sourcing is a design pattern in which results of business operations are stored as a series of events.

It is an alternative way to persist data. In contrast with state-oriented persistence that only keeps the latest version of the entity state, Event Sourcing stores each state change as a separate event.

Thanks for that, no business data is lost. Each operation results in the event stored in the database. That enables extended auditing and diagnostics capabilities (both technically and business-wise). What's more, as events contains the business context, it allows wide business analysis and reporting.

Revision
CQRS and Event Sourcing in Event Driven Architecture of Ordering Microservices
https://medium.com/aspnetrun/cqrs-and-event-sourcing-in-event-driven-architecture-of-ordering-microservices-fb67dc44da7a




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
