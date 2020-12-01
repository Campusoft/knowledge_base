
Web Services Federation (WS-Federation or WS-Fed) is part of the larger WS-Security framework and an extension to the functionality of WS-Trust. The features of WS-Federation can be used directly by SOAP applications and web services. WS-Fed is a protocol that can be used to negotiate the issuance of a token. You can use this protocol for your applications (such as a Windows Identity Foundation-based app) and for identity providers (such as Active Directory Federation Services or Azure AppFabric Access Control Service).


Authenticate users with WS-Federation in ASP.NET Core.

**Terminos:**

- Wtrealm: Requerido. El identificador del usuario de confianza de Active Directory FS. Ejemplo: https://portal.contoso.com/. 
- MetadataAddress: Requerido. La dirección URL de metadatos de WS-Federation del servidor de AD FS (STS). Comúnmente termina con la ruta de acceso: /FederationMetadata/2007-06/FederationMetadata.xml . Ejemplo: https://adfs.contoso.com/FederationMetadata/2007-06/FederationMetadata.xml.
- Wreply: Requerido. El extremo de AD FS WS-Federation Passive (URL de nuestro sitio). Ejemplo: https://portal.contoso.com/signin-federation.

**Soluciones:**


Si una aplicacion tiene un proxy delante, y la direccion publica sea diferente a la interna (publica https, interna http) el direccionamiento que se realice desde la aplicacion, causara que las cookies no se establezcan correctamente. 

Para solucionar establecer ProtocolMessage.Wreply
![imagen](https://user-images.githubusercontent.com/222181/92297689-9ad9dc80-ef07-11ea-86c9-617db88bbe43.png)
-----

Exception: Correlation failed.

Posibles origines error:

El Set-Cookie,  para ".AspNetCore.Correlation.WsFederation", tiene que ser samesite=none

The SameSite=Lax setting works for most application cookies. Some forms of authentication like OpenID Connect (OIDC) and WS-Federation default to POST based redirects. The POST based redirects trigger the SameSite browser protections, so SameSite is disabled for these components "samesite=none"

https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1

Cookies that assert SameSite=None must also be marked as Secure. "Set the secure flag, which Chrome's changes will require for SameSite none."


----
En Chrome, no se funciona. 

Algunas posibles origenes.

Cookies that assert SameSite=None must also be marked as Secure.

Si no se establece las cookies que son "SameSite=None", con la marca "secure", Chrome no estable  la cookie 

    this set-cookie was blocked it had the "samesite=none" atribute but dit not have the "secure" attribute, which is required in order to use "samesite=none"

TODO: En el caso que no se establece la marca "secure" al momento crear las cookies, con las configuraciones estandares. Hacer forzamiento para eventos de "CookiePolicyOptions", si una cookie tiene establecido "samesite=none", establecer explicitamente el valor "secure = true".

![imagen](https://user-images.githubusercontent.com/222181/92298819-556edc80-ef12-11ea-9a99-58c43109b358.png)


----------




**Referencia:**
https://docs.microsoft.com/es-es/powerapps/maker/portals/configure/configure-ws-federation-settings

Authenticate users with WS-Federation in ASP .NET Core 
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/ws-federation?view=aspnetcore-3.0





