# Estandares, Buenas Practicas, Listas Checkeo


## Patrones


GOF Design Patterns in C# with working examples inspired by food
https://github.com/wesdoyle/design-patterns-explained-with-food


SOLID principles

Single responsibility principle
Open/closed principle
Liskov substitution principle
Interface segregation principle
Dependency inversion principle

# Resiliente

Una app resiliente es una que continúa funcionando a pesar de tener fallas en los componentes del sistema. La resiliencia requiere planificación en todos los niveles de la arquitectura. Influye en el diseño de la infraestructura y la red, y en el diseño de la app y el almacenamiento de datos. La resiliencia también se extiende a las personas y la cultura.


## Web

The Front-End Checklist is an exhaustive list of all elements you need to have / to test before launching your website / HTML page to production.

https://github.com/thedaviddias/Front-End-Checklist

 Clean Code concepts adapted for TypeScript 
https://github.com/labs42io/clean-code-typescript


## Documentation

5 Steps to Create Technical Documentation That’s (Actually) Helpful
https://plan.io/blog/technical-documentation/


## Database

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



## Varios

### DMN - Decision Model and Notation

The Decision Model and Notation (DMN™) is a Standard by OMG® providing a common and visual notation readily understandable by all users and personas. With DMN, business analysts can define the initial decision requirements and then formalize more detailed decision models; technical developers can automate the decisions in any process with a portable execution semantic, while business stakeholders can manage and monitor those decisions. 

Soporta
- Drools
- Camunda


https://www.omg.org/spec/DMN/
