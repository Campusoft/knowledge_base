ASP.NET Core supports both role-based and policy-based authorization



# Identity on ASP.NET Core

The primary package for Identity is Microsoft.AspNetCore.Identity. This package contains the core set of interfaces for ASP.NET Core Identity, and is included by Microsoft.AspNetCore.Identity.EntityFrameworkCore.


Identity model customization in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0


Configure ASP.NET Core Identity
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-5.0

# Authentication and authorization for SPAs

Authentication and authorization for SPAs
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-5.0


Use cookie authentication without ASP.NET Core Identity
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-5.0


User Authentication and Identity with Angular, Asp.Net Core and IdentityServer
https://fullstackmark.com/post/21/user-authentication-and-identity-with-angular-aspnet-core-and-identityserver


# External provider authentication 
 

# Policy-based Authorization.

The policy-based security model is centered on three main concepts. These include policies, requirements, and handlers


## Requirements

A requirement comprises a collection of data parameters. These data parameters are used by a policy to evaluate the user identity. To create a requirement, you need to create a class that implements the IAuthorizationRequirement interface


## Authorization Handlers

A requirement can have one or more handlers. An authorization handler is used to evaluate the properties of a requirement. To create an authorization handler, you should create a class that extends AuthorizationHandler<T> and implements the HandleRequirementAsync() method


## Referencias



# Framework / Librerias

Balea is an authorization framework for ASP.NET Core developers. 
 
Authentication and authorization might sound similar, but both are distinct security processes in the world of identity and access management and understanding the difference between these two concepts is the key to successfully implementing a good IAM solution.
https://github.com/Xabaril/Balea

https://www.youtube.com/watch?v=UnbJrC0WN1U


# Revisiones


User.SecurityStamp

 So the primary purpose of the SecurityStamp is to enable sign out everywhere. The basic idea is that whenever something security related is changed on the user, like a password, it is a good idea to automatically invalidate any existing sign in cookies, so if your password/account was previously compromised, the attacker no longer has access.
 
 
IUserSecurityStampStore<IdentityUser>
IUserSecurityStampStore<TUser, string>
 
 
