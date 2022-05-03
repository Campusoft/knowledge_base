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

 
**Client**
A client is a piece of software that requests tokens from IdentityServer - either for authenticating a user (requesting an identity token) or for accessing a resource (requesting an access token). A client must be first registered with IdentityServer before it can request tokens.

Examples for clients are web applications, native mobile or desktop applications, SPAs, server processes etc.


**Resources**
Resources are something you want to protect with IdentityServer - either identity data of your users, or APIs.

The two fundamental resource types in IdentityServer are:

-    identity resources: represent claims about a user like user ID, display name, email address etc…
-    API resources: represent functionality a client wants to access. Typically, they are HTTP-based endpoints (aka APIs), but could be also message queuing endpoints or similar.

Identity Resources. An identity resource is a named group of claims that can be requested using the scope parameter.

API Resources
- support for the JWT aud claim. The value(s) of the audience claim will be the name of the API resource(s)
https://docs.identityserver.io/en/latest/topics/resources.html#api-resources

**Scopes**

**Grant Types**

Mapeo entre response_type / Grant Types "Flows". 

response_type			Flow
code			  		Authorization Code Flow. 
id_token  				Implicit Flow
id_token token  		Implicit Flow
code id_token  			Hybrid Flow
code token  			Hybrid Flow
code id_token token  	Hybrid Flow

Mapeo entre ResponseTypes, con las constantes GrantType IdentityServer
```
{ OidcConstants.ResponseTypes.Code, GrantType.AuthorizationCode },
{ OidcConstants.ResponseTypes.Token, GrantType.Implicit },
{ OidcConstants.ResponseTypes.IdToken, GrantType.Implicit },
{ OidcConstants.ResponseTypes.IdTokenToken, GrantType.Implicit },
{ OidcConstants.ResponseTypes.CodeIdToken, GrantType.Hybrid },
{ OidcConstants.ResponseTypes.CodeToken, GrantType.Hybrid },
{ OidcConstants.ResponseTypes.CodeIdTokenToken, GrantType.Hybrid }
```

https://github.com/IdentityServer/IdentityServer4/blob/main/src/IdentityServer4/src/Constants.cs
 
# Federation Gateway


your applications only need to know about the one token service (the gateway) and are shielded from all the details about connecting to the external provider(s). This also means that you can add or change those external providers without needing to update your applications.

![imagen](https://user-images.githubusercontent.com/222181/104109541-f42e8f80-529c-11eb-9476-d79d38f11dfc.png)

http://docs.identityserver.io/en/latest/topics/federation_gateway.html

# Flujos

## application scenarios

***Machine to Machine Communication***

This is the simplest type of communication. Tokens are always requested on behalf of a client, no interactive user is present.

In this scenario, you send a token request to the token endpoint using the client credentials grant type. The client typically has to authenticate with the token endpoint using its client ID and secret.

***Interactive Clients***
This is the most common type of client scenario: web applications, SPAs or native/mobile apps with interactive users.


## Interactive Applications with ASP.NET Core

add support for interactive user authentication via the OpenID Connect protocol

# Endpoint 

Con el endpoint de descubrimiento, se puede obtener todos los endpoint disponibles

/.well-known/openid-configuration

Los endpoint

- authorization_endpoint	"https://localhost:44332/connect/authorize"
- token_endpoint	"https://localhost:44332/connect/token"
- userinfo_endpoint	"https://localhost:44332/connect/userinfo"
- end_session_endpoint	"https://localhost:44332/connect/endsession"
- check_session_iframe	"https://localhost:44332/connect/checksession"
- revocation_endpoint	"https://localhost:44332/connect/revocation"
- introspection_endpoint	"https://localhost:44332/connect/introspect"
- device_authorization_endpoint	"https://localhost:44332/connect/deviceauthorization"


## End Session Endpoint

http://docs.identityserver.io/en/latest/endpoints/endsession.html


# Events

Events are structured data and include event IDs, success/failure information, categories and details. This makes it easy to query and analyze them and extract useful information that can be used for further processing.
http://docs.identityserver.io/en/latest/topics/events.html

IEventSink

Default implementation of the event service. Write events raised to the log.
https://github.com/IdentityServer/IdentityServer4/blob/main/src/IdentityServer4/src/Services/Default/DefaultEventSink.cs


# Firma y Validacion Token


Create Certificates for IdentityServer4 signing using .NET Core
https://damienbod.com/2020/02/10/create-certificates-for-identityserver4-signing-using-net-core/

***Laboratorio***


-----------------------
Crear un certificado con dotnet dev-certs. 

-p Password
-ep nombre del archivo del certificado

```
dotnet dev-certs https -ep identityserver.pfx -p Test@2021
```

Agregar configuracion

```
var builder = services.AddIdentityServer( ...
. 
.
.
var password = "Test@2021";
var certFile = Path.Combine(System.AppContext.BaseDirectory, "certificate", "identityserver.pfx");
var certificate = new X509Certificate2(certFile,password );

builder.AddSigningCredential(certificate);			
```
Ver archivo descubrimiento

"Host-IdentityServer"/.well-known/openid-configuration

En el archivo  de descubrimiento, se encuentra el enlace de las claves:
 
"Host-IdentityServer"/.well-known/openid-configuration/jwks


Nota.
Tambien se puede utilizar el sitio para generar certificados:
- CSR Options. (Self-Sign)
- En Download. (PKCS#12 Certificate and key)
https://certificatetools.com/



# Varios


**Entendimiento**



**Agregar claims personalizados**

Profile Service

Often IdentityServer requires identity information about users when creating tokens or when handling requests to the userinfo or introspection endpoints. By default, IdentityServer only has the claims in the authentication cookie to draw upon for this identity data.

http://docs.identityserver.io/en/latest/reference/profileservice.html#

Aplica para el flujo "AllowedGrantTypes = GrantTypes.Code".

Para agregar la personalizacion de IProfileService, utilizar el metodo AddProfileService.
```
services.AddIdentityServer()
    .AddProfileService<CustomProfileService>();
```
http://docs.identityserver.io/en/latest/topics/startup.html?highlight=AddProfileService#additional-services


	

Identity Server 4: adding claims to access token
https://newbedev.com/identity-server-4-adding-claims-to-access-token


AlwaysIncludeUserClaimsInIdToken
    When requesting both an id token and access token, should the user claims always be added to the id token instead of requiring the client to use the userinfo endpoint. Default is false.

The GetProfileDataAsync is being invoked correctly whenever I hit the /connect/token endpoint and I can verify that my email claim indeed does get added to the context.IssuedClaims collection. See the screenshot below:

-----------------

La interfaz IClaimsService, tambien permite agregar claims personalizados. 
Se puede crear una clase que herede de la implementacion por defecto DefaultClaimsService.

Se puede sobreescribir el metodo GetOptionalClaims, para agregar claims.






# Errores

Si el token posee una audicencias. En este caso "Base Public PersonRegistration", pero el cliente que valida el cliente valida otra audiencia que no se encuentra en las existentes en el token.
```
www-authenticate: Bearer error="invalid_token"error_description="The audience 'BasePublicPersonRegistration' is invalid" 
```

Se puede desactivar la validacion de la audicencia. En la configuracion de validacion del token "ValidateAudience = false"

```
 services
		.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.Authority = Configuration["AuthServer:Authority"];
			options.Audience = Configuration["AuthServer:Audience"]; 
			options.TokenValidationParameters = new TokenValidationParameters
			{ 
				ValidateAudience = false //No realizar validacion de la audiencia
			};
		});
				
```
---------------------------

Client cannot request OpenID scopes in client credentials flow


--------------------------






# Referencias

User Authentication and Identity with Angular, Asp.Net Core and IdentityServer
https://fullstackmark.com/post/21/user-authentication-and-identity-with-angular-aspnet-core-and-identityserver


Codigo fuente de Demo instance of IdentityServer4 demo.identityserver.io
https://github.com/IdentityServer/IdentityServer4.Demo


# Revisiones

Eventos
IdentityServer4.Events


El ejemplo IdentityServer4, posee la configuracion:
ValidateAudience = false
https://identityserver4.readthedocs.io/en/latest/quickstarts/1_client_credentials.html

.AddDeveloperSigningCredential()        //This is for dev only scenarios when you don’t have a certificate to use.

---------------------------

Grant Types. Soportados por IdentityServer4. Mapeo options.ResponseType 


-------------


sign out for openId
https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-protocols-oidc#send-a-sign-out-request

Sign-out of External Identity Providers
To detect that a user must be redirected to an external identity provider for sign-out is typically done by using a idp claim issued into the cookie at IdentityServer. 
http://docs.identityserver.io/en/latest/topics/signout_external_providers.html


External authentication cookie not saved in Identity.External
https://github.com/IdentityServer/IdentityServer4/issues/1826