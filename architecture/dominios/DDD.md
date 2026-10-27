# DDD
 
Bounded Context. Un dominio, está dividido en subdominios más pequeños conocidos como bounded contexts. Un subdominio es una división lógica de un dominio en contextos más pequeños. Un bounded context es un espacio lógico en el que compartimos un lenguaje ubicuo. Es decir, en este espacio lógico, todos utilizamos las mismas palabras para referirnos a los mismos elementos. Es un limite logico, similar a un modulo en un sistema. 


# Domain Service

A Domain Service is a stateless service that implements core business rules of the domain. It is useful to implement domain logic that depends on multiple aggregate (entity) type or some external services.



# Domain Event

A Domain Event is a way of informing other services in a loosely coupled manner, when a domain specific event occurs.

# Aggregates

Single Unit

An aggregate is retrieved and saved as a single unit, with all the sub-collections and properties.


# Specifications

A specification is a named, reusable, combinable and testable class to filter the Domain Objects based on the business rules.

Specification is an interface that defines one method: IsSatisfiedBy which return boolean to assert that the specification is satisfied. So, every single business rule should be represented by a class that implements the ISpecification interface.


Implement validations in the domain model layer Validations are usually implemented in domain entity constructors or in methods that can update the entity. There are multiple ways to implement validations, such as verifying data and raising exceptions. if the validation fails. There are also more advanced patterns such as using the Specification pattern for validations, and the Notification pattern to return a collection of errors instead of returning an exception for each validation as it occurs

 
## Referencias

This is a practical guide for implementing the Domain Driven Design (DDD). While the implementation details rely on the ABP Framework infrastructure, core concepts, principles and patterns are applicable in any kind of solution, even if it is not a .NET solution
(Interesante documento, aplicando los elementos al framework DDD)
https://docs.abp.io/en/abp/latest/Domain-Driven-Design-Implementation-Guide
