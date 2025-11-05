#  /.well-known/?


La carpeta .well-known es un estándar definido por la IETF (RFC 8615) que permite a los servidores exponer metadatos o configuraciones estandarizadas en una ubicación predecible. Ejemplos comunes:

/.well-known/security.txt → Información de contacto para reportar vulnerabilidades.
/.well-known/openid-configuration → Configuración de OpenID Connect.
/.well-known/apple-app-site-association → Para deep linking en iOS.
/.well-known/oauth-authorization-server → Metadatos de un servidor OAuth 2.0 (RFC 8414).
/.well-known/assetlinks.json → Asocia una app Android con un sitio web (para deep linking y verificación de propiedad).
/.well-known/carddav Descubrimiento de servicios CardDAV (contactos)
/.well-known/caldav Descubrimiento de servicios CalDAV (calendarios)
 

Well-Known URIs
La IANA mantiene un registro oficial de los nombres de recursos bien conocidos:

https://www.iana.org/assignments/well-known-uris/well-known-uris.xhtml