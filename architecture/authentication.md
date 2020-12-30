# Authentication

## BASIC AUTHENTICATION

HTTP Basic authentication implementation is the simplest technique for enforcing access controls to web resources because
it doesn’t require cookies, session identifiers, or login pages; rather, HTTP Basic authentication uses standard fields in the HTTP header, removing the need for handshakes.
To receive authorization, the client sends the userid and password, separated by a single colon (“:”) character, within a Base64 encoded string in the credentials.

Because Basic authentication involves the cleartext transmission of passwords, it should be used over TLS or SSL protocols (HTTPS) in order to protect sensitive or valuable information.

# Jwt Auth

JSON Web Tokens are an open, industry standard RFC 7519 method for representing claims securely between two parties.

Campos estandares

Código 	Nombre 	Descripción
-	iss 	Issuer 	Identifica el proveedor de identidad que emitió el JWT
-	sub 	Subject 	Identifica el objeto o usuario en nombre del cual fue emitido el JWT
-	aud 	Audience 	Identifica la audiencia o receptores para lo que el JWT fue emitido. Cada servicio que recibe un JWT para su validación tiene que controlar la audiencia a la que el JWT está destinado. Si el proveedor del servicio no se encuentra presente en el campo aud, entonces el JWT tiene que ser rechazado
-	exp 	Expiration time 	Identifica la marca temporal luego de la cual el JWT no tiene que ser aceptado. 
-	nbf 	Not before 	Identifica la marca temporal en que el JWT comienza a ser válido. EL JWT no tiene que ser aceptado si el token es utilizando antes de este tiempo. 
-	iat 	Issued at 	Identifica la marca temporal en qué el JWT fue emitido.
-	jti 	JWT ID 	Identificador único del token incluso entre diferente proveedores de servicio.

https://es.wikipedia.org/wiki/JSON_Web_Token

# OAuth 2


# OpenID Connect, OAuth 2.0 and SAML 2.0


Build a web application using OpenID Connect with AD FS 2016
https://docs.microsoft.com/en-us/windows-server/identity/ad-fs/development/enabling-openid-connect-with-ad-fs

