ASP.NET Core supports both role-based and policy-based authorization



# Identity on ASP.NET Core

ASP.NET Core Identity:

-    Is an API that supports user interface (UI) login functionality.
-    Manages users, passwords, profile data, roles, claims, tokens, email confirmation, and more.


The primary package for Identity is Microsoft.AspNetCore.Identity. This package contains the core set of interfaces for ASP.NET Core Identity, and is included by Microsoft.AspNetCore.Identity.EntityFrameworkCore.


Identity model customization in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0


Configure ASP.NET Core Identity
ASP.NET Core Identity uses default values for settings such as password policy, lockout, and cookie configuration. These settings can be overridden in the Startup class.
- Claims Identity
  - RoleClaimType 	Gets or sets the claim type used for a role claim.	
  - SecurityStampClaimType 	Gets or sets the claim type used for the security stamp claim.
  - UserIdClaimType 	Gets or sets the claim type used for the user identifier claim.
  - UserNameClaimType 	Gets or sets the claim type used for the user name claim
- Lockout  
- Password
- Sign-in
  - RequireConfirmedEmail 
  - RequireConfirmedPhoneNumber  
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-5.0


***Claims***
- Role 	
  - A role that the user has 	
  - http://schemas.microsoft.com/ws/2008/06/identity/claims/role
- Name Identifier 	 
  - The SAML name identifier of the user 	
  - http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier



# Authentication and authorization for SPAs

Authentication and authorization for SPAs
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-5.0


Use cookie authentication without ASP.NET Core Identity
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-5.0


User Authentication and Identity with Angular, Asp.Net Core and IdentityServer
https://fullstackmark.com/post/21/user-authentication-and-identity-with-angular-aspnet-core-and-identityserver


# External provider authentication 



Ejemplos:

Add sign-in with Microsoft to an ASP.NET Core web app
https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-aspnet-core-webapp


 

# Policy-based Authorization.

The policy-based security model is centered on three main concepts. These include policies, requirements, and handlers


## Policy

A policy is composed of one or more requirements. 

## Requirements

A requirement comprises a collection of data parameters. These data parameters are used by a policy to evaluate the user identity. To create a requirement, you need to create a class that implements the IAuthorizationRequirement interface


The Authorization Requirement defines the collection of conditions that the policy must evaluate. For the Policy to be successful, it must satisfy all the requirements. It is similar to AND Condition. If one of the requirements fails. then the policy fails.


## Authorization Handlers

A requirement can have one or more handlers. An authorization handler is used to evaluate the properties of a requirement. To create an authorization handler, you should create a class that extends AuthorizationHandler<T> and implements the HandleRequirementAsync() method

## Process

The three “main” services involved in the authorization process are:

- AuthorizationMiddleware
- PolicyEvaluator
- DefaultAuthorizationService
	


## Referencias

Policy-based Authorization in ASP.NET Core
- Simple Authorization Policies
- Applying the Authorization Policy
- Custom Policy using a Func
- Custom Policy using requirement & Handlers
- Example of Requirement & Requirement handler 
https://www.tektutorialshub.com/asp-net-core/policy-based-authorization-in-asp-net-core/

Permission-Based Authorization in ASP.NET Core – Complete User Management Guide in .NET 5
https://codewithmukesh.com/blog/permission-based-authorization-in-aspnet-core/


# Framework / Librerias

Balea is an authorization framework for ASP.NET Core developers. 
 
Authentication and authorization might sound similar, but both are distinct security processes in the world of identity and access management and understanding the difference between these two concepts is the key to successfully implementing a good IAM solution.
https://github.com/Xabaril/Balea

https://www.youtube.com/watch?v=UnbJrC0WN1U

# Referencias

Basic Authentication with Middleware in ASP.NET Core 3 Web API
- Middleware
https://learningprogramming.net/net/asp-net-core-3-web-api/basic-authentication-with-middleware-in-asp-net-core-3-web-api/


Build Secure ASP.NET Core API with JWT Authentication – Detailed Guide
https://codewithmukesh.com/blog/aspnet-core-api-with-jwt-authentication/

# Revisiones


User.SecurityStamp

 So the primary purpose of the SecurityStamp is to enable sign out everywhere. The basic idea is that whenever something security related is changed on the user, like a password, it is a good idea to automatically invalidate any existing sign in cookies, so if your password/account was previously compromised, the attacker no longer has access.
 
 
IUserSecurityStampStore<IdentityUser>
IUserSecurityStampStore<TUser, string>
 
------------------------- 

That is because the AuthorizeFilter added to the pipeline for every [Authorize] attribute requires users to be authenticated.

If you look at the source code, you will see that even without calling any policy it is making sure the user is authenticated:

```
// Note: Default Anonymous User is new ClaimsPrincipal(new ClaimsIdentity())
if (httpContext.User == null ||
    !httpContext.User.Identities.Any(i => i.IsAuthenticated) ||
    !await authService.AuthorizeAsync(httpContext.User, context, Policy))
{
    context.Result = new ChallengeResult(Policy.AuthenticationSchemes.ToArray());
}
```
