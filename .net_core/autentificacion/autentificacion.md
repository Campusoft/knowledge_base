

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

 

# Framework / Librerias

## Belea

Balea is an authorization framework for ASP.NET Core developers. 
 
Authentication and authorization might sound similar, but both are distinct security processes in the world of identity and access management and understanding the difference between these two concepts is the key to successfully implementing a good IAM solution.

- Applications. Allow you to manage multiple different software projects, for example. Each application has its own unique set of roles and delegations.
- Roles. Are similar to security groups, to which users can become members and acquire a level of security that gives them the ability to perform some business operations. Roles can contain permissions, subjects and mappings

- Store. A mechanism that allow you to store persistent the Balea’s object model such as applications, roles, permissions, delegations. Balea provides out-of-the-box two stores:
  - ASP.NET Core JSON Configuration Provider.
  - Entity Framework Core.


https://github.com/Xabaril/Balea

https://www.youtube.com/watch?v=UnbJrC0WN1U

***Revision Codigo***

Tiene la siguiente clase para crear las politicas en runtime.
- Una ves creada la politica, la guarda AuthorizationOptions

AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider


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

-----------------------

Swagger 
Authentication And Authorization In .NET Core Web API Using JWT Token And Swagger UI
- Configurar swagger UI, para ingresar un token para autentificar.
https://www.c-sharpcorner.com/article/authentication-authorization-using-net-core-web-api-using-jwt-token-and/


ASP.NET Core Swagger UI Authorization using IdentityServer4
https://www.scottbrady91.com/identity-server/aspnet-core-swagger-ui-authorization-using-identityserver4
