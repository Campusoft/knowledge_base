# OpenID Connect
 
# Secure an API

You use AddJwtBearer to secure an API, meaning that the client of the API sends JWT-tokens to access the API and there is otherwise no human interaction.

# OpenID Connect authentication to the MVC application

AddOpenIdConnect you use to secure a web-application, where you have human interaction (login/logout...), because you typically redirect your user to your identity provider.

To add support for OpenID Connect authentication to the MVC application, you first need to add the nuget package containing the OpenID Connect handler to your project, e.g.:

```
dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect
```

***Claims***

Para recuperar claims especificos que existan en el token access, se debe configurar
- Utilizar ClaimActions.MapJsonKey, para mapear el claim X del token, a los claim identity
- GetClaimsFromUserInfoEndpoint=true. Para indicar que obtenga claim adicionales desde "user info endpoint [/connect/userinfo]", luego crear la identidad from id_token recibido. (The default is 'false')


```
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
	options.Authority = "...";

	options.ClientId = "..."; 

	options.GetClaimsFromUserInfoEndpoint = true; 
	
	options.ClaimActions.MapJsonKey("FooClaim", "FooClaim"); 
});
```

# Labatororios

## Azure AD

Single-page application: App registration

Indica como utilizar autentificacion, utilizando Azure AD.

MSAL.js 2.0 with auth code flow
MSAL.js 1.0 with implicit flow
https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-spa-app-registration


Quickstart: Register an application with the Microsoft identity platform
https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app

## Okta

## IdentityServer

## openiddict

Alternativa a  IdentityServer

OpenIddict aims at providing a versatile solution to implement an OpenID Connect server and token validation in any ASP.NET Core 2.1, 3.1 and 5.0 application, and starting in OpenIddict 3.0, any ASP.NET 4.x application using Microsoft.Owin too.

https://github.com/openiddict/openiddict-core


# Errores comunes

```
2022-01-20 17:39:57.738 -05:00 [ERR] scope is missing
```

Se requiere establecer un scope.

------------

# Referencias

ASP.NET Core and JSON Web Tokens - where are my claims?
https://mderriey.com/2019/06/23/where-are-my-jwt-claims/

