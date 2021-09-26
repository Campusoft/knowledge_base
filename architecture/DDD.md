# DDD
 
Bounded Context. Un dominio, está dividido en subdominios más pequeños conocidos como bounded contexts. Un subdominio es una división lógica de un dominio en contextos más pequeños. Un bounded context es un espacio lógico en el que compartimos un lenguaje ubicuo. Es decir, en este espacio lógico, todos utilizamos las mismas palabras para referirnos a los mismos elementos. Es un limite logico, similar a un modulo en un sistema. 



# Domain Service

A Domain Service is a stateless service that implements core business rules of the domain. It is useful to implement domain logic that depends on multiple aggregate (entity) type or some external services

# Domain Event

A Domain Event is a way of informing other services in a loosely coupled manner, when a domain specific event occurs.

# Aggregates

Single Unit

An aggregate is retrieved and saved as a single unit, with all the sub-collections and properties.


# Specifications

A specification is a named, reusable, combinable and testable class to filter the Domain Objects based on the business rules.




 
## Referencias

This is a practical guide for implementing the Domain Driven Design (DDD). While the implementation details rely on the ABP Framework infrastructure, core concepts, principles and patterns are applicable in any kind of solution, even if it is not a .NET solution
(Interesante documento, aplicando los elementos al framework DDD)
https://docs.abp.io/en/abp/latest/Domain-Driven-Design-Implementation-Guide
