# Authentication

- Role based access control. RBAC
- Attribute-based access control (ABAC)



**Authentication Types***

In Stateful authentication, the server creates a session for the user after successfully authenticating. The session id is then stored as a cookie in the user's browser and the user session store in the cache or database. When the client tries to access the server with a given session id, the server attempts to load the user session context for the session store, checks if the session is valid, and decides if the client has to access the desired resource or rejects the request.

Stateless authentication stores the user session on the client-side. A cryptographic algorithm signs the user session to ensure the session data’s integrity and authority.
Each time the client requests a resource from the server, the server is responsible for verifying the token claims sent as a cookie.

Since the user session is stored on the client-side, this approach frees the overhead to maintain the user session state, and scaling doesn’t require additional effort. 



# BASIC AUTHENTICATION

HTTP Basic authentication implementation is the simplest technique for enforcing access controls to web resources because
it doesn’t require cookies, session identifiers, or login pages; rather, HTTP Basic authentication uses standard fields in the HTTP header, removing the need for handshakes.
To receive authorization, the client sends the userid and password, separated by a single colon (“:”) character, within a Base64 encoded string in the credentials.

Because Basic authentication involves the cleartext transmission of passwords, it should be used over TLS or SSL protocols (HTTPS) in order to protect sensitive or valuable information.

# Jwt Auth

JSON Web Tokens are an open, industry standard RFC 7519 method for representing claims securely between two parties.

JSON Web Token (JWT) is an open standard of transmitting information securely between two parties. As the tokens are digitally signed, the information is secured. The authentication and authorization process uses JWT access tokens. It is ideal to use JWT access tokens as API credentials because JWT access tokens can carry claims (data) that are used in order to authenticate and authorize requests.


- Token validation doesn’t require an additional trip and can be validated locally by each service
- Given  its  JSON  nature,  it  is  solely  based  on  claims  or  attributes  to  carry  authentication  andauthorization information about a subject.
- Given  its  JSON  nature,  processing  JWT  tokens  becomes  trivial  and  lightweight.


Mandatory attributes of a JWT access token

Header

- alg 	The algorithm used to sign the token (e.g., RS256).


Payload

Campos estandares

Código 	Nombre 	Descripción
-	iss 	Issuer 	Identifica el proveedor de identidad que emitió el JWT. (The claim identifies the principal that issued the JWT.) (Mandatory)
-	sub 	Subject 	Identifica el objeto o usuario en nombre del cual fue emitido el JWT (Mandatory)
-	aud 	Audience 	Identifica la audiencia o receptores para lo que el JWT fue emitido. Cada servicio que recibe un JWT para su validación tiene que controlar la audiencia a la que el JWT está destinado. Si el proveedor del servicio no se encuentra presente en el campo aud, entonces el JWT tiene que ser rechazado

The audience (aud), which represents the particular application which the token has been  issued for. It is very important to check this claim. As an app receives this token, the middleware used for validating it will compare what was configured to be the app identifier (in the case  of sign-in and ID tokens, that will correspond to the client ID of the app) with the audience claim. If there is a mismatch, that means that someone stole a token from somewhere else, 
and they're trying to trick the app into accepting it


-	exp 	Expiration time 	Identifica la marca temporal luego de la cual el JWT no tiene que ser aceptado.  (Mandatory)
-	nbf 	Not before 	Identifica la marca temporal en que el JWT comienza a ser válido. EL JWT no tiene que ser aceptado si el token es utilizando antes de este tiempo. 
-	iat 	Issued at 	Identifica la marca temporal en qué el JWT fue emitido.
-	jti 	JWT ID 	Identificador único del token incluso entre diferente proveedores de servicio.
-   SID		unique identifier of session of end user on a particular device/user agent, etc. 

https://es.wikipedia.org/wiki/JSON_Web_Token


JWT
The ID token consists of three main parts:

-    header. Metadata about the token and its cryptographic algorithm
-    payload. Claims about the issuer, the user and user authorization
-    signature. For verification of the integrity of the token
	
You can use the header and signature to verify the authenticity of the token, while the payload contains the information about the user requested by your client. 
 
Signature
The last part of the ID Token is the signature, which you use to to verify that the token was issued by the trusted issuer and not tampered with in route.

The header of the JWT contains information about the key and encryption method used to sign the token:

```
{
  "typ": "JWT",
  "alg": "RS256",
  "x5t": "iBjL1Rcqzhiy4fpxIxdZqohM2Yk",
  "kid": "iBjL1Rcqzhiy4fpxIxdZqohM2Yk"
}
```

The alg claim indicates the algorithm that was used to sign the token, while the kid claim indicates the particular public key that was used to validate the token.

**Validar con jwt.io, la firma del Token.**

Using jwt.io to verify the signature of a JWT token
- Requiere  x5c
https://blogs.aaddevsup.xyz/2019/03/using-jwt-io-to-verify-the-signature-of-a-jwt-token/


# SAML 


Security Assertion Markup Language (SAML) is an XML-based method for exchanging user security information between an SAML identity provider and a SAML service provider.


SAML 2.0 es un estándar que especifica cómo un Service Provider (SP) y un Identity Provider (IdP) intercambian información de identidad de usuario. Cuando configura su Firebox para el SSO de SAML, el Firebox funciona como el SP. El IdP es un servicio de terceros que usted especifica. 

Versiones:

- SAML V2.0 was approved as an OASIS Standard in March 2005. 
- SAML V1.1 was approved as an OASIS Standard in August 2003. 
https://wiki.oasis-open.org/security


# OAuth 2


In OAuth2, the term Grant Type refers to the way for a client application to acquire an access token depending on the type of the resource owner, type of the application and the trust relationship between the authorization server and the resource owner. 


Los flujos de OAuth también denominados grant types hacen referencia al modo en que una aplicación obtiene un access token que le permite acceder a los datos expuestos a través de una API.

Revision:

¿Cómo securizar tus APIs con OAuth?
https://www.paradigmadigital.com/dev/oauth-2-0-equilibrio-y-usabilidad-en-la-securizacion-de-apis/


OAuth 2.0 Authorization Code with PKCE Flow 
These security issues led to a reassessment of the value of the Implicit flow, and in November of 2018, new guidance was released that effectively deprecated this flow. 

## grants 

**Authorisation Code Grant**

The Flow (Part One)
- response_type: code

The Flow (Part Two)
- grant_type: authorization_code


Proof Key for Code Exchange
PKCE (RFC 7636) is an extension to the Authorization Code flow to prevent CSRF and authorization code injection attacks.

PKCE is not a replacement for a client secret, and PKCE is recommended even if a client is using a client secret.


**Resource owner credentials grant**

- grant_type: password

**Client credentials grant**

The simplest of all of the OAuth 2.0 grants, this grant is suitable for machine-to-machine authentication where a specific user’s permission to access data is not required.

This flow exchanges the client credentials for a token immediately, which is suitable for machine-to-machine applications

- grant_type: client_credentials


**Refresh token grant**

Access tokens eventually expire; however some grants respond with a refresh token which enables the client to get a new access token without requiring the user to be redirected.
- grant_type: refresh_token


A Guide To OAuth 2.0 Grants
- parameters  segun el tipo Grants
https://alexbilbie.com/guide-to-oauth-2-grants/

Una introducción a OAuth 2
- Posee ilustraciones tipos Grants
- Posee ejemplos con endpoint parameters response
https://www.digitalocean.com/community/tutorials/una-introduccion-a-oauth-2-es

## TODO: Revision.
On The Nature of OAuth2’s Scopes
Stretching OAuth2 scopes beyond intended usage leads to trouble in complex architectures.
https://auth0.com/blog/on-the-nature-of-oauth2-scopes/


# OpenID Connect

OpenID Connect 1.0 is a simple identity layer on top of the OAuth 2.0 protocol. It allows Clients to verify the identity of the End-User based on the authentication performed by an Authorization Server, as well as to obtain basic profile information about the End-User in an interoperable and REST-like manner. 
 
OpenID Connect describes a metadata document (RFC) that contains most of the information required for an app to do sign in. This includes information such as the URLs to use and the location of the service's public signing keys. You can find this document by appending the discovery document path to the authority URL:

Discovery document path: /.well-known/openid-configuration

Similar to OAuth 2.0, OpenID Connect also uses the scopes concept. Again, scopes represent something you want to protect and that clients want to access. In contrast to OAuth, scopes in OIDC don’t represent APIs, but identity data like user id, name or email address.

openid,

The contents are fully described in the OpenID Connect specification.

https://openid.net/specs/openid-connect-discovery-1_0.html#rfc.section.4.2


OpenID Connect has defined flows to issue ID tokens by extending the specification of the response_type request parameter.

the value of response_type is either code or token. OpenID Connect has added a new value, id_token, and allowed any combination of code, token and id_token. A special value, none, has been added, too. 

response_type parameter:

- code
- token
- id_token
- id_token token
- code id_token
- code token
- code id_token token
- none

Mapeo entre response_type / Grant Types
response_type			Flow
code			  		Authorization Code Flow
id_token  				Implicit Flow
id_token token  		Implicit Flow
code id_token  			Hybrid Flow
code token  			Hybrid Flow
code id_token token  	Hybrid Flow


Depending on the response_type in the OIDC protocol, some claims are transferred via the id_token and some via the userinfo endpoint. 


Authentication Request
- login_hint. OPTIONAL. Hint to the Authorization Server about the login identifier the End-User might use to log in (if necessary). 
https://openid.net/specs/openid-connect-core-1_0.html#AuthRequest


**Implicit Flow**

The Implicit flow is intended for applications where the confidentiality of the client secret can't be guaranteed. In this flow, the client doesn't make a request to the /token endpoint, but instead receives the access token directly from the /authorize endpoint. The client must be capable of interacting with the resource owner's user agent and also capable of receiving incoming requests (through redirection) from the authorization server.

En response_type, utilizar algun flow "Implicit Flow", ejemplo "id_token%20token"

https://{openID_server}/authorize?response_type=id_token%20token&client_id={cliente_id}&redirect_uri={redirect_uri}&scope=openid%20profile


En .well-known/openid-configuration, existe la propiedad "jwks_uri", el cual permite
recuperar las claves para firmar los token.

The JSON Web Key Set (JWKS) is a set of keys containing the public keys used to verify any JSON Web Token (JWT) issued by the authorization server and signed using the RS256 signing algorithm. 


The JWKS above contains a single key. Each property in the key is defined by the JWK specification RFC 7517 Section 4. We will use these properties to determine which key was used to sign the JWT. Here is a quick breakdown of what each property represents:

-    alg: is the algorithm for the key
-    kty: is the key type. Key type . Family of algorithm used(RSA , HSA).
-    use: is how the key was meant to be used. For the example above, sig represents signature verification. Usage. ‘sig’ for signing keys, ‘enc’ for encryption keys.
-    x5c: is the x509 certificate chain. Chain of certificates used for verification. Usually x5c[0] is use for token verification.
-    e: is the exponent for a standard pem
-    n: is the moduluos for a standard pem
-    kid: is the unique identifier for the key
-    x5t: is the thumbprint of the x.509 cert (SHA-1 thumbprint). Used to identify specific certificates. Is commonly used when the certificate is pre-distributed to the clients, and kid when JWKS is used.

Here are the steps for validating the JWT:

-    Retrieve the JWKS and filter for potential signature verification keys.
-    Extract the JWT from the request's authorization header.
-    Decode the JWT and grab the kid property from the header.
-    Find the signature verification key in the filtered JWKS with a matching kid property.
-    Using the x5c property build a certificate which will be used to verify the JWT signature.
-    Ensure the JWT contains the expected audience, issuer, expiration, etc.



OpenID specifications 
https://openid.net/developers/specs/

## OpenID Connect (OIDC) authentication protocol

Similar to OAuth 2.0, OpenID Connect also uses the scopes concept. Again, scopes represent something you want to protect and that clients want to access. In contrast to OAuth, scopes in OIDC don’t represent APIs, but identity data like user id, name or email address.


OpenID Connect (OIDC) scopes are used by an application during authentication to authorize access to a user's details, like name and picture. Each scope returns a set of user attributes, which are called claims. The scopes an application should request depend on which user attributes the application needs.


The basic (and required) scope for OIDC is openid, which indicates that an application intends to use the OIDC protocol to verify a user's identity.

- openid 	(required) Returns the sub claim, which uniquely identifies the user. In an ID Token, iss, aud, exp, iat, and at_hash claims will also be present. To learn more about the ID Token claims, read ID Token Structure.

- profile 	Returns claims that represent basic profile information, including name, family_name, given_name, middle_name, nickname, picture, and updated_at.

- email 	Returns the email claim, which contains the user's email address, and email_verified, which is a boolean indicating whether the email address was verified by the user.
	
- offline_access When a user approves the offline_access scope, your app can receive refresh tokens from the Microsoft identity platform token endpoint. Refresh tokens are long-lived. Your app can get new access tokens as older ones expire.
	

## Library or Product

In the identity space, we have a lot of different offerings. They range from libraries where you have to add and implement a lot of functionality by yourself to fully managed products where even the hosting is done for you.

![imagen](https://user-images.githubusercontent.com/222181/132995450-e1cecef2-87f5-4eda-8a5c-7f584a00386a.png)

While libraries usually give you the most flexibility, the cost of implementation is higher than with products or SaaS offerings. Besides the client integration, you also need to build the server and integrate it into your codebase. On the other hand, products and SaaS offerings are mostly ready to run, and you are able to integrate them very quickly, which gives you a short "time to first token". This can give you a boost in development since you don't need to spend a lot of time implementing your token server. But with products, and especially SaaS offerings, you have less flexibility, and you have to rely on extensions points given by the product.

Ref: https://www.thinktecture.com/en/identity/three-alternatives-to-identityserver/

## Librerias

-------------------

oidc-client
https://github.com/IdentityModel/oidc-client-js/wiki

 A simple demonstration of using IdentityModel/oidc-client with angular 2 
https://github.com/jmurphzyo/Angular2OidcClient/tree/ng4

-------------------

MSAL.js

## Referencias

The OIDC playground is for developers to test and work with OpenID Connect calls step-by-step, giving them more insight into how OpenID Connect works.
https://openidconnect.net/


Demo que posee algunos tipos grant type
https://demo.identityserver.io/


Diagrams of All The OpenID Connect Flows
- code
- token
- id_token
- id_token token
- code id_token
- code token
- code id_token token
- none
https://darutk.medium.com/diagrams-of-all-the-openid-connect-flows-6968e3990660

Certificate Decoder
https://www.sslshopper.com/certificate-decoder.html


Build a web application using OpenID Connect with AD FS 2016
https://docs.microsoft.com/en-us/windows-server/identity/ad-fs/development/enabling-openid-connect-with-ad-fs


Standard Claims
This specification defines a set of standard Claims. 
https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims


# Claim

A claim is a key-value pair that represents a subject, i.e., name, age, passportnumber, drivinglicense, passport, nationality, dateofbirth, etc. So, if dateofbirth is the claim name, the claim value would be the date of birth, i.e., 1st January 1970.




# Single sign-on (SSO)

Single sign-on (SSO) is an authentication scheme that allows a user to log in with a single ID and password to any of several related, yet independent, software systems. 

Terminos:
- Identity provider (IdP)
- Service Provider (SP)


Estandares:

- OpenID Connect
- SAML2
- WS-Federation
- Central Authentication Service (CAS)

Propietarios
- Facebook Connect Single Sign On Authentication 



# Multifactor authentication (MFA)


# OTP - One-time password

A one-time password (OTP) is an automatically generated sequence of numeric or alphanumeric characters that will authenticate a user for a single login or transaction. It is used in a multifactor authentication (MFA) process to secure access to data.

## Passwordless

Passwordless connections do not require the user to remember a password. Instead, another mechanism is used to prove identity, such as a one-time code sent through email or SMS, every time the user logs in.

# Revoking Access


Revoking Access
- Token Database
- Self-Encoded Tokens
https://www.oauth.com/oauth2-servers/listing-authorizations/revoking-access/

# Referencias




# REVISIONES

Refresh tokens can be revoked by the server due to a change in credentials, or due to use or admin action.
https://docs.microsoft.com/en-us/azure/active-directory/develop/access-tokens#revocation


Generar certificados x509, para pruebas


