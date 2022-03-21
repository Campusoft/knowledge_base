# Security

How to implement security for microservices

- Securing north-south traffic (external consumers)
  - Implement security at each microservice level
  - Implement security using a sidecar
  - Implement security using a shared gateway
- Securing east-west traffic
  - Transport layer security
  - Transport layer security with message layer security using authentication
  - Transport layer security with message layer security using authentication and authorization
    https://medium.com/microservices-learning/how-to-implement-security-for-microservices-89b140d3e555

# Identity and permissions. (Authentication and Authorization)

**_Revisiones_**

Edge-level authorization. In simple scenario, authorization can happen only at the edge level (API gateway). The API gateway can be leveraged to centralize enforcement of authorization for all downstream microservices, eliminating the need to provide authentication and access control for each of the individual services.

We believe that authorization is part of domain and should be modelled accordingly within a bounded context, which corresponds to a microservice in our architecture.

How We Solved Authentication and Authorization in Our Microservice Architecture

- Enlase roto
  https://medium.com/technology-learning/how-we-solved-authentication-and-authorization-in-our-microservice-architecture-994539d1b6e6

Building a fine-grained permission system in a distributed environment: Architecture | Very Good Security

- UMA and XACML
- a team that manages identities and access (IAM) can work on developing a sidecar while another team can work on the business logic of the service.
- Policy-based control for cloud native environments Flexible, fine-grained control for administrators across the stack
  https://www.verygoodsecurity.com/blog/posts/building-a-fine-grained-permission-system-in-a-distributed-environment

Handling Authentication and Authorization in Microservices - Part 2

- Utilizar Claims-based authorization, se almacena un lista permisos en un claims, y los servicios verifica si un permiso X, se encuentra en el claims. "It�s a very easy mechanism to implement and works pretty well but also means that we�re sending back and forth a �fat� token, bloated with a lot of useless information for most of the calls"
- Crear un servicio que permite autorizar.
  https://www.davidguida.net/handling-authentication-and-authorization-in-microservices-part-2/

Microservices Security Cheat Sheet
(Interesante, tiene buena informacion). Tambien menciona temas de logs.
https://cheatsheetseries.owasp.org/cheatsheets/Microservices_security.html

Authentication & Authorization in Microservices Architecture - Part I
Authentication Strategy in a Microservice Architecture

- Authentication & Authorization on each service
- Global Authentication & Authorization Service
- Global Authentication (API Gateway) and authorization per service  
  https://dev.to/behalf/authentication-authorization-in-microservices-architecture-part-i-2cn0

Microservices Authentication and Authorization Solutions

- 1. Distributed Session Management
- 2. Client Token
- 3. Single sign-on
- 4. Client Token with API Gateway
- 5. Third-party application access
     https://medium.com/tech-tajawal/microservice-authentication-and-authorization-solutions-e0e5e74b248a

# Audit Trail

Microservices shall generate and pass through microservice call chain a correlation ID which uniquely identify every call chain and help grouping log messages to investigate them.

Logging agent shall include correlation ID in every log message

Audit Trails 201: Technical Deep Dive

- Data Store. Algunas razones porque utilizar no-sql mongodb
- Audit Service responsible  
  https://harness.io/blog/audit-trails-technical/

# Herramientas

**_oauth2-proxy_**

A reverse proxy and static file server that provides authentication using Providers (Google, GitHub, and others) to validate accounts by email, domain or group.

**_Open Policy Agent (OPA)_**

Policy-based control for cloud native environments Flexible, fine-grained control for administrators across the stack

Stop using a different policy language, policy model, and policy API for every product and service you use. Use OPA for a unified toolset and framework for policy across the cloud native stack.

Whether for one service or for all your services, use OPA to decouple policy from the service's code so you can release, analyze, and review policies (which security and compliance teams love) without sacrificing availability or performance.

https://www.openpolicyagent.org/

# Referencias

Authorizing multi-language microservices with oauth2-proxy

- Using Keycloak
- Deploy the application with oauth2-proxy sidecar
  https://developers.redhat.com/articles/2021/05/20/authorizing-multi-language-microservices-oauth2-proxy#

Dex is an identity service that uses OpenID Connect to drive authentication for other apps.

Dex acts as a portal to other identity providers through �connectors.� This lets Dex defer authentication to LDAP servers, SAML providers, or established identity providers like GitHub, Google, and Active Directory. Clients write their authentication logic once to talk to Dex, then Dex handles the protocols for a given backend.

https://dexidp.io/
