# Authorization 

ASP.NET Core supports both role-based and policy-based authorization
 


# Policy-based Authorization.

In ASP.NET Core, the policy-based authorization framework is designed to decouple authorization and application logic

The policy-based security model is centered on three main concepts. These include policies, requirements, and handlers.


La autorización con políticas es el mecanismo moderno de ASP.NET Core para controlar qué usuarios pueden ejecutar qué acciones.

Se basa en tres conceptos:

- Requisitos (Requirements) → Lo que se necesita para acceder (ej. "debe tener 18 años").
- Policies (Políticas) → Conjunto de uno o varios requisitos con un nombre.
- Handlers (Manejadores) → Lógica que evalúa los requisitos y decide si el usuario cumple.



## Policy

A policy is composed of one or more requirements. 

## Requirements

A requirement comprises a collection of data parameters. These data parameters are used by a policy to evaluate the user identity. To create a requirement, you need to create a class that implements the IAuthorizationRequirement interface


The Authorization Requirement defines the collection of conditions that the policy must evaluate. For the Policy to be successful, it must satisfy all the requirements. It is similar to AND Condition. If one of the requirements fails. then the policy fails.


## Authorization Handlers

A requirement can have one or more handlers. An authorization handler is used to evaluate the properties of a requirement. To create an authorization handler, you should create a class that extends AuthorizationHandler<T> and implements the HandleRequirementAsync() method.

An Authorization Handler can return one of the three values
- Fail
- Succeded
- Do Nothing
	

## Process

The three “main” services involved in the authorization process are:

- AuthorizationMiddleware
- PolicyEvaluator
- DefaultAuthorizationService
	

***Custom Authorization Policy Providers***

 If we have a large number of policies, this is not a desirable way to register all policies. In such a case, we can use a custom policy provider (IAuthorizationPolicyProvider).
 
 The following scenarios may be a candidate for Custom Authorization Policy Provider:

-    Policy evaluation provided by the external service
-    For a large range of policies (i.e., a variety of policies are to provide access. It doesn't make any sense to add every policy to authorization options)
-    Create a Policy that uses runtime information to provide access

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/iauthorizationpolicyprovider?view=aspnetcore-6.0


## Referencias Policy

Policy-based Authorization in ASP.NET Core
- Simple Authorization Policies
- Applying the Authorization Policy
- Policy with Single Claim
- Custom Policy using a Func
- Custom Policy using requirement & Handlers
- Example of Requirement & Requirement handler 
https://www.tektutorialshub.com/asp-net-core/policy-based-authorization-in-asp-net-core/

Permission-Based Authorization in ASP.NET Core – Complete User Management Guide in .NET 5
-   What’s Role-Based Authorization?
-   Limitations of Role-Based Authorization
-   Permission-Based Authorization in ASP.NET Core
-   What we will build? (Screenshots Included)
        List of all Registered Users
        Manage User Roles
        Add New Roles
        Manage Permissions – UI
-   Getting Started with Permission-Based Authorization
-   Pre-Defined Roles
-   Seeding Users and Roles
-   Displaying a List of Registered Users
-   Role Management UI
-   Roles-Permission Management UI
-   Claims Helper
-   Permission Requirement
-   Authorization Handler
-   Permission Policy Provider
-   Registering the Service
-   Product Controller
-   Summary
	
https://codewithmukesh.com/blog/permission-based-authorization-in-aspnet-core/


# Resource-based authorization in ASP.NET Core

Authorization approach depends on the resource. For example, only the author of a document is authorized to update the document. Consequently, the document must be retrieved from the data store before authorization evaluation can occur.

https://learn.microsoft.com/en-us/aspnet/core/security/authorization/resourcebased?view=aspnetcore-7.0


# Row Level Security


A Fairly Capable Authorization Sub-System with Row-Level Security Capabilities (AFCAS)
https://www.codeproject.com/Articles/30380/A-Fairly-Capable-Authorization-Sub-System-with-Row



# Referencias

# Revisiones
 