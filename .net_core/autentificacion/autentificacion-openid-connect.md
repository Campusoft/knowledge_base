# OpenID Connect
 
# Secure an API

You use AddJwtBearer to secure an API, meaning that the client of the API sends JWT-tokens to access the API and there is otherwise no human interaction.



La propiedad TokenValidationParameters, del AddJwtBearer para configurar las validacion de los token, posee una propiedad "ClockSkew", que por defecto es 300 segundos.

- Este valor permite configurar un rango entre la verificacion de la fecha de caducidad del token, y fecha de no utilizarse antes.


Ejemplo

```
.AddJwtBearer(options =>
	{
		options.Authority = Configuration["AuthServer:Authority"];
		options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["AuthServer:RequireHttpsMetadata"]);
		options.Audience = Configuration["AuthServer:Audience"];
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ClockSkew = TimeSpan.FromSeconds(300) // 5 min.
		};

	});
```


# OpenID Connect authentication to the MVC application

AddOpenIdConnect you use to secure a web-application, where you have human interaction (login/logout...), because you typically redirect your user to your identity provider.

To add support for OpenID Connect authentication to the MVC application, you first need to add the nuget package containing the OpenID Connect handler to your project, e.g.:

```
dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect
```

**Claims**

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
	
	options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub"); 
	
});
```

Mapeo de algunos claim
- El claim "ClaimTypes.NameIdentifier", por lo general se mapea con "sub" en un JWT. El sub  "Subject", identifica el objeto o usuario en nombre del cual fue emitido 


**Pre - Rellenar el usuario/email, en la pagina de inicio de sesion.** 

En el Challenge, agregar una propiedad con el usuario. Ejemplo "userId"

```
var provider = OpenIdConfiguracion.ProveedorNombre;
var redirectUrl = Url.Page("./Login", pageHandler: "ExternalLoginCallback", values: new { ReturnUrl, ReturnUrlHash });
var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
properties.Items["scheme"] = provider;

//Use for LoginHint
properties.Items["userId"] = Usuario;

return await Task.FromResult(Challenge(properties, provider));
```

En la configuracion del AddOpenIdConnect, con el evento "OnRedirectToIdentityProvider", agregar ProtocolMessage.LoginHint con el valor de la propiedad establecida en el Challenge "userId"
-  

```
 context.Services.AddAuthentication()
            .AddOpenIdConnect(openIdConfiguracion.ProveedorNombre, openIdConfiguracion.NombreVisualizar, options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                .
				.
				.
				
                //Paremtros adicionales
                options.Events.OnRedirectToIdentityProvider = ctx =>
                {
                    if (ctx.Properties.Items.TryGetValue("userId", out var userId))
                    {
                        ctx.ProtocolMessage.LoginHint = userId;
                    }

                    return Task.CompletedTask;
                };
            });
```

 


# Labatororios

# Azure AD

Single-page application: App registration

Indica como utilizar autentificacion, utilizando Azure AD.

MSAL.js 2.0 with auth code flow
MSAL.js 1.0 with implicit flow
https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-spa-app-registration


Quickstart: Register an application with the Microsoft identity platform
https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app


Archivo de descubrimiento, para cuentas de Azure AD. (OpenId Connect).
- <TENANT-NAME>, nombre del tenant o el Identificador del tenant
https://login.microsoftonline.com/<TENANT-NAME>/.well-known/openid-configuration


Authority, en Azure AD: 
- En el archivo https://login.microsoftonline.com/{Tenant}/.well-known/openid-configuration, la propiedad "issuer"


# Okta

# IdentityServer

[IdentityServer](IdentityServer.md)

# Openiddict

Alternativa a  IdentityServer

OpenIddict aims at providing a versatile solution to implement an OpenID Connect server and token validation in any ASP.NET Core 2.1, 3.1 and 5.0 application, and starting in OpenIddict 3.0, any ASP.NET 4.x application using Microsoft.Owin too.

OpenIddict is little bit more low-level than IdentityServer. Identity Server gives you a running solution out-of-the-box, where for OpenIddict to work you need to implement some details yourself, like creating claim identities and setting up the correct endpoints. 


OpenIddict natively supports Entity Framework Core, Entity Framework 6 and MongoDB out-of-the-box and custom stores can be implemented to support other providers.

Informacion importante
- OpenIddict encrypts the access token by default.


Application permissions

OpenIddict includes a built-in feature codenamed "application permissions" that allows controlling and limiting the OAuth 2.0/OpenID Connect features each registered client application is able to use.

4 categories of permissions are currently supported:
- Endpoint permissions.
- Grant type permissions.
- Scope permissions.
- Response type permissions (introduced in OpenIddict 3.0).
https://documentation.openiddict.com/configuration/application-permissions.html


## Referencias

Source Code
https://github.com/openiddict/openiddict-core


Server with OpenIddict
The articles in this series will guide you through the process of setting up an OAuth2 + OpenID Connect authorization server on the the ASPNET Core platform using OpenIddict.
-    Part I: Introduction
-    Part II: Create ASPNET project
-    Part III: Client Credentials Flow
-    Part IV: Authorization Code Flow
-    Part V: OpenID Connect
-    Part VI: Refresh tokens
https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-i-introduction-4jid



# Errores comunes

```
2022-01-20 17:39:57.738 -05:00 [ERR] scope is missing
```

Se requiere establecer un scope.



AADSTS50011: The redirect URI 'http://localhost:44332/signin-azuread-oidc' specified in the request does not match the redirect URIs configured for the application '45d1eeda-660d-4ae9-9ccc-d04e28605c2b'. Make sure the redirect URI sent in the request matches one added to your application in the Azure portal. Navigate to https://aka.ms/redirectUriMismatchError to learn more about how to fix this. 


AADSTS50020: User account 'juan.saavedra@grupobusiness.it' from identity provider 'https://sts.windows.net/24884996-6863-4925-a1ba-f1a160b581e2/' does not exist in tenant 'MINISTERIO DE RELACIONES EXTERIORES MRE CANCILLERIA' and cannot access the application '45d1eeda-660d-4ae9-9ccc-d04e28605c2b'(Visas regulación QA) in that tenant. The account needs to be added as an external user in the tenant first. Sign out and sign in again with a different Azure Active Directory user account.


------------

# Referencias

ASP.NET Core and JSON Web Tokens - where are my claims?
https://mderriey.com/2019/06/23/where-are-my-jwt-claims/


 

Listado de codigos (IDX10000 - IDX10999)
Microsoft.IdentityModel.Tokens
Range: 10000 - 10999 
Ej:
- IDX10222: Lifetime validation failed. The token is not yet valid. ValidFrom: '{0}', Current time: '{1}'.
- IDX10223: Lifetime validation failed. The token is expired. ValidTo: '{0}', Current time: '{1}'.
https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/dev/src/Microsoft.IdentityModel.Tokens/LogMessages.cs


Listado de codigos (IDX10000 - IDX10999)
System.IdentityModel.Protocols
Range: 20000 - 20999
Ej:
- IDX20803 = "IDX20803: Unable to obtain configuration from: '{0}'."
- IDX20804 = "IDX20804: Unable to retrieve document from: '{0}'."
- IDX20807 = "IDX20807: Unable to retrieve document from: '{0}'. HttpResponseMessage: '{1}', HttpResponseMessage.Content: '{2}'."

https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/dev/src/Microsoft.IdentityModel.Protocols/LogMessages.cs


