# IdentityServer

IdentityServer4 is an OpenID Connect and OAuth 2.0 framework for ASP.NET Core. IdentityServer4 enables the following security features:

-  Authentication as a Service (AaaS)
-  Single sign-on/off (SSO) over multiple application types
-  Access control for APIs
-  Federation Gateway

Caracteristicas

- Federation Gateway
- Support for external identity providers like Azure Active Directory, Google, Facebook etc. This shields your applications from the details of how to connect to these external providers.


# Concept

 
***Client***
A client is a piece of software that requests tokens from IdentityServer - either for authenticating a user (requesting an identity token) or for accessing a resource (requesting an access token). A client must be first registered with IdentityServer before it can request tokens.

Examples for clients are web applications, native mobile or desktop applications, SPAs, server processes etc.


***Resources***
Resources are something you want to protect with IdentityServer - either identity data of your users, or APIs.

 
# Federation Gateway


your applications only need to know about the one token service (the gateway) and are shielded from all the details about connecting to the external provider(s). This also means that you can add or change those external providers without needing to update your applications.

![imagen](https://user-images.githubusercontent.com/222181/104109541-f42e8f80-529c-11eb-9476-d79d38f11dfc.png)

http://docs.identityserver.io/en/latest/topics/federation_gateway.html


# Referencias


User Authentication and Identity with Angular, Asp.Net Core and IdentityServer
https://fullstackmark.com/post/21/user-authentication-and-identity-with-angular-aspnet-core-and-identityserver


