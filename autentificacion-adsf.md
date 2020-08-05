

Authenticate users with WS-Federation in ASP.NET Core

Terminos:
Wtrealm: Requerido. El identificador del usuario de confianza de Active Directory FS. Ejemplo: https://portal.contoso.com/. 
MetadataAddress: Requerido. La dirección URL de metadatos de WS-Federation del servidor de AD FS (STS). Comúnmente termina con la ruta de acceso: /FederationMetadata/2007-06/FederationMetadata.xml . Ejemplo: https://adfs.contoso.com/FederationMetadata/2007-06/FederationMetadata.xml.
Wreply: Requerido. El extremo de AD FS WS-Federation Passive (URL de nuestro sitio). Ejemplo: https://portal.contoso.com/signin-federation.

Referencia:
https://docs.microsoft.com/es-es/powerapps/maker/portals/configure/configure-ws-federation-settings

Implementar/utilizar ws-federation en .net core
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/ws-federation?view=aspnetcore-3.0




