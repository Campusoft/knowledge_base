# ANGULAR  - Autentificacion


# MSAL Angular


MSAL for Angular enables Angular web applications to authenticate users using Azure AD work and school accounts (AAD), Microsoft personal accounts (MSA) and social identity providers like Facebook, Google, LinkedIn, Microsoft accounts, etc. through Azure AD B2C service. It also enables your app to get tokens to access Microsoft Cloud services such as Microsoft Graph.
https://github.com/AzureAD/microsoft-authentication-library-for-js/tree/dev/lib/msal-angular




# angular-oauth2-oidc

Support for OAuth 2 and OpenId Connect (OIDC) in Angular.  
https://github.com/manfredsteyer/angular-oauth2-oidc


TODO: Documentar lo que se realiza en el CLIENTE.
- Azure.
- Adfs.

Configuracion. Clase AuthConfig
https://github.com/manfredsteyer/angular-oauth2-oidc/blob/master/projects/lib/src/auth.config.ts


## Laboratorio


**angular-oauth2-oidc**

Errores

Every url in discovery document has to start with the issuer url. Also see property strictDiscoveryDocumentValidation.

Solucion:

Configuracion:

// turn off validation that discovery document endpoints start with the issuer url defined above
strictDiscoveryDocumentValidation: false

https://manfredsteyer.github.io/angular-oauth2-oidc/docs/additional-documentation/using-an-id-provider-that-fails-discovery-document-validation.html


------------------------
Error:
Parameter jwks expected!




------------------


## Varios

- Estructura 
 
