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



JwtRegisteredClaimNames.Sub

- Estándar JWT (RFC 7519)
- Interoperabilidad con otros sistemas
- Claim corto y limpio


ClaimTypes.NameIdentifier

- Requerido por ASP.NET Core Identity
- UserManager.GetUserAsync() lo busca por defecto
- Compatibilidad con middleware de autorización



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


AspNetUserLogins

La tabla AspNetUserLogins sirve para almacenar la información de los proveedores de autenticación externos que un usuario utiliza para iniciar sesión en tu aplicación.

En lugar de que el usuario ingrese un nombre de usuario y contraseña directamente en tu sistema, usa servicios como Google, Facebook, Microsoft o Twitter. Esta tabla crea el vínculo entre la identidad del usuario en tu base de datos (AspNetUsers) y su identidad externa.


La tabla contiene los siguientes campos clave que definen la vinculación:

Columna | Tipo de Dato | Propósito
-- | -- | --
LoginProvider | nvarchar(450) | Identifica el servicio externo (e.g., Google, Facebook, Microsoft).
ProviderKey | nvarchar(450) | Es el identificador único del usuario dentro del proveedor externo (e.g., el ID numérico que Google asigna a ese usuario). Es la clave de la autenticación.
ProviderDisplayName | nvarchar(max) | El nombre legible del proveedor (opcional, útil para la interfaz de usuario).
UserId | nvarchar(450) | Foreign Key que apunta al Id del usuario en la tabla AspNetUsers.


# Escenarios
 
## Cierre de sesión en todos los dispositivos	Permitir que el usuario invalide todas las sesiones activas (logout global).

ASP.NET Identity genera un SecurityStamp para cada usuario. Este sello de seguridad se almacena dentro de la cookie de autenticación.

Cuando el SecurityStamp cambia, todas las cookies/ tokens existentes quedan inválidos automáticamente.

User.SecurityStamp

 So the primary purpose of the SecurityStamp is to enable sign out everywhere. The basic idea is that whenever something security related is changed on the user, like a password, it is a good idea to automatically invalidate any existing sign in cookies, so if your password/account was previously compromised, the attacker no longer has access.
 
 
IUserSecurityStampStore<IdentityUser>
IUserSecurityStampStore<TUser, string>


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



## Identity .net core / Mongo

AspNetCore.Identity.Mongo

https://github.com/matteofabbri/AspNetCore.Identity.Mongo

***Errores***
```
InvalidOperationException: No service for type 'Microsoft.AspNetCore.Identity.UserManager`1[Microsoft.AspNetCore.Identity.IdentityUser]' has been registered.
```

Solucionar actual. Cambiar todas las referecias IdentityUser con MongoUser, agregando el codigo Identity Scaffolding, para realizar los cambios.


------------------------------------

## Casbin.NET

Casbin.NET is a powerful and efficient open-source access control library for .NET (C#) projects. It provides support for enforcing authorization based on various access control models.
- based on the PERM metamodel (Policy, Effect, Request, Matchers).


An authorization library that supports access control models like ACL, RBAC, ABAC in .NET (C#) 
 
https://github.com/casbin/Casbin.NET

# Referencias

Basic Authentication with Middleware in ASP.NET Core 3 Web API
- Middleware
https://learningprogramming.net/net/asp-net-core-3-web-api/basic-authentication-with-middleware-in-asp-net-core-3-web-api/



Build Secure ASP.NET Core API with JWT Authentication – Detailed Guide
- Generate JWT Token
- Consume JWT Token
https://codewithmukesh.com/blog/aspnet-core-api-with-jwt-authentication/

ASP.NET Core: Identity Scaffolding
ASP.NET Core provides ASP.NET Core Identity as a Razor Class Library. Applications that include Identity can apply the scaffolder to selectively add the source code contained in the Identity Razor Class Library (RCL). You might want to generate source code so you can modify the code and change the behavior.
https://elanderson.net/2019/04/asp-net-core-identity-scaffolding/

User Registration with Angular and ASP.NET Core Identity
In this article, we are going to learn about User Registration with Angular and ASP.NET Core Web API. 
https://code-maze.com/user-registration-angular-aspnet-identity/

Angular Authentication Functionality with ASP.NET Core Identity
https://code-maze.com/angular-authentication-aspnet-identity/

Password Reset with ASP.NET Core Identity
https://code-maze.com/password-reset-aspnet-core-identity/

Creating custom password validators for ASP.NET Core Identity 
- Mecanismo para agregar nuevas validaciones de claves.
- Validacion ejemplo, para validar que la clave no sea igual al usuario 
https://andrewlock.net/creating-custom-password-validators-for-asp-net-core-identity-2/


How to Implement JWT Authentication in Web API Using .Net 6.0, Asp.Net Core
- Generar token Jwt. Con claims
- Configurar validacion tokens Jwt. 
https://www.c-sharpcorner.com/article/how-to-implement-jwt-authentication-in-web-api-using-net-6-0-asp-net-core/

Implement JWT Authentication in Asp.net Core 5 Web API [Token Base Auth] 
- Understanding JWT Authentication Workflow. (Diagrama)
- Install NuGet Package (JwtBearer) 
https://codepedia.info/jwt-authentication-in-aspnet-core-web-api-token

**Books**

Pro ASP.NET Core Identity
Under the Hood with Authentication and Authorization in ASP.NET Core 5 and 6 Applications


# Revisiones



 
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
 

Swagger
- Basic Authentication in Swagger (Open API) .Net 5
https://www.c-sharpcorner.com/article/basic-authentication-in-swagger-open-api-net-5/

ASP.NET Core Swagger UI Authorization using IdentityServer4
https://www.scottbrady91.com/identity-server/aspnet-core-swagger-ui-authorization-using-identityserver4

Swagger UI (OpenApi) with Authorization code flow + PKCE using Swashbuckle ASP.NET Core
https://lurumad.github.io/swagger-ui-with-pkce-using-swashbuckle-asp-net-core

Swashbuckle.AspNetCore - Add Security Definitions and Requirements
https://github.com/domaindrivendev/Swashbuckle.AspNetCore#add-security-definitions-and-requirements
