# IdentityServer

IdentityServer4 is an OpenID Connect and OAuth 2.0 framework for ASP.NET Core. IdentityServer4 enables the following security features:

-  Authentication as a Service (AaaS)
-  Single sign-on/off (SSO) over multiple application types
-  Access control for APIs
-  Federation Gateway



Caracteristicas

Federation Gateway
Support for external identity providers like Azure Active Directory, Google, Facebook etc. This shields your applications from the details of how to connect to these external providers.



 
# Federation Gateway


your applications only need to know about the one token service (the gateway) and are shielded from all the details about connecting to the external provider(s). This also means that you can add or change those external providers without needing to update your applications.

![imagen](https://user-images.githubusercontent.com/222181/104109541-f42e8f80-529c-11eb-9476-d79d38f11dfc.png)




http://docs.identityserver.io/en/latest/topics/federation_gateway.html


# OpenIddict

Alternativa a  IdentityServer

OpenIddict aims at providing a versatile solution to implement an OpenID Connect server and token validation in any ASP.NET Core 2.1, 3.1 and 5.0 application, and starting in OpenIddict 3.0, any ASP.NET 4.x application using Microsoft.Owin too.

https://github.com/openiddict/openiddict-core
