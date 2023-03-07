# Security


- principle of least privilege or deny by default


Autenticación de Factor Único - solo en una contraseña
Autenticación Multifactor (MFA).
Autenticación de Doble Factor (2FA).


La prueba de seguridad de aplicaciones dinámicas (DAST) es una metodología de prueba de seguridad de caja negra en la que una aplicación se prueba desde el exterior. Un evaluador que usa DAST examina una aplicación cuando se está ejecutando e intenta piratearla como lo haría un atacante. En el otro extremo del espectro está la prueba de seguridad de aplicaciones estáticas (SAST), que es una metodología de prueba de caja blanca. Un evaluador que usa SAST examina la aplicación desde adentro, buscando en su código fuente condiciones que indiquen que podría haber una vulnerabilidad de seguridad.

# Rest API

Checklist of the most important security countermeasures when designing, testing, and releasing your API 
- Authentication
- JWT (JSON Web Token)
- OAuth
- Access
- Input
- Processing
- Output
- CI & CD
https://github.com/shieldfy/API-Security-Checklist

# OWASP

OWASP es un proyecto de código abierto dedicado a determinar y combatir las causas que hacen que el software sea inseguro
https://owasp.org/www-project-top-ten/


## Broken Access Control 

- Stateful session identifiers should be invalidated on the server after logout. Stateless JWT tokens should rather be short-lived so that the window of opportunity for an attacker is minimized. For longer lived JWTs it's highy recommended to follow the OAuth standards to revoke access.

https://owasp.org/Top10/A01_2021-Broken_Access_Control/

## CSRF

El ataque CSRF funciona porque el servidor receptor no comprueba de dónde procede la solicitud. Es decir, no queda claro si la solicitud HTTP ha sido creada por la propia página web o si su origen es externo. En este contexto, el atacante se aprovecha de una laguna de seguridad del navegador; transmite las solicitudes sin evaluar las consecuencias.

## Cross-site scripting (XSS)


X-XSS-Protection - Preventing Cross-Site Scripting Attacks

- Posee un digrama
- Enable in Nginx
- Enable in Apache
- Enable on IIS
- An important thing to keep in mind is that the X-XSS-Protection header is pretty much being replaced with the new Content Security Policy (CSP) reflected-xss directive
https://www.keycdn.com/blog/x-xss-protection

## X-Frame-Options

X-Frame-Options
https://developer.mozilla.org/en-US/docs/web/http/headers/x-frame-options

## X-Content-Type-Options

X-Content-Type-Options
https://developer.mozilla.org/es/docs/Web/HTTP/Headers/X-Content-Type-Options

## Content Security Policy

The new Content-Security-Policy HTTP response header helps you reduce XSS risks on modern browsers by declaring which dynamic resources are allowed to load.



Content Security Policy (CSP). Quick Reference Guide 
https://content-security-policy.com/


Content-Security-Policy
https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy

## CORS (Cross-Origin Resource Sharing) 

# Audit Trail

[Audit](Audit.md)

# Varios

cookies  httpOnly

Información Personal Identificable (PII)
PII es cualquier información sobre un individuo mantenida por una agencia, incluyendo (1) cualquier información que pueda usarse para distinguir o rastrear la identidad de un individuo, tal como nombre, número de seguridad social, fecha y lugar de nacimiento, apellido de soltera de la madre o registros biométricos; y (2) cualquier otra información que esté vinculada o que pueda vincularse con un individuo, como información médica, educativa, financiera y de empleo.

  
# Tool 

Security Code Scan - static code analyzer for .NET
https://security-code-scan.github.io/


# Blockchain

Ethereum funciona como una plataforma de código abierto basada en la tecnología blockchain.

El blockchain de ethereum es similar al del bitcoin en el sentido de que funciona también como registro del historial de transacciones

# Normativas

## Reglamento General de Protección de Datos - GDPR

## PCI DSS

Estándar de seguridad de datos de la industria de pagos con tarjeta


## HIPAA

## (LOPD) La Ley Orgánica de Protección de Datos Personales de Ecuador

Con fecha de 26 de mayo de 2021 se ha publicó La Ley Orgánica de Protección de Datos de Ecuador, una vez que el texto fue aprobado por la Asamblea Nacional tras los correspondientes debates y sancionado por el señor Presidente de la República. Cabe destacar que las empresas cuentan desde ese momento con un período de adaptación de dos años con el objetivo de poder adecuar todos sus procesos a lo exigido por esta nueva normativa

# X.509 certificate

Defined in RFC 5280

Certificates may be distributed in a multitude of other formats as well. What is important to understand is that regardless of the encoding format—DER, PEM, PFX or something else—all certificates are basically the same when they are decoded. Tools, such as OpenSSL, are able to read or convert any of these formats easily.


The certificate encodes two very important pieces of information: the server's public key and a digital signature that can be used to confirm the certificate's authenticity. Additionally, the certificate will include metadata used by the CA to track the certificate and provide guidelines on how the public key can be used. 

Understanding Public Key Infrastructure and X.509 Certificates
https://www.linuxjournal.com/content/understanding-public-key-infrastructure-and-x509-certificates 

**CertificateTools.com**
CertificateTools.com X509 Certificate Generator
https://certificatetools.com/

# PFX

Los archivos con extensión .PFX contienen datos de certificados encriptados que se utilizan para autenticar personas y dispositivos como servidores web u ordenadores.

RSA Lab published multiple standards in cryptography. Which in general are called PKCS. PKCS #12 defines an archive file format for storing many cryptography objects as a single file. The filename extension for PKCS #12 files is .p12 or .pfx

PFX file, is a single, password protected or password-less certificate archive which contains the certificate chain and the private key. You can think of it as an archive that stores everything you need to deploy a certificate.


