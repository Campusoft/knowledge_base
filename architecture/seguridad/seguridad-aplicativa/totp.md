# TOTP: OTP basado en tiempo

## Objetivo y alcance

TOTP (*Time-Based One-Time Password*) es una variante de OTP que deriva un código a partir de un secreto compartido y del tiempo. La especificación es [RFC 6238](https://www.rfc-editor.org/rfc/rfc6238.html), que extiende HOTP (RFC 4226). Se usa comúnmente como un factor de posesión en MFA.

## Conceptos y funcionamiento

El autenticador (aplicación o dispositivo) y el verificador (servidor) conocen un secreto único por autenticador. Ambos convierten el tiempo Unix actual en un contador de pasos y aplican HOTP:

```text
T = floor((tiempo_unix - T0) / X)
TOTP = HOTP(secreto, T)
```

RFC 6238 establece un paso predeterminado `X` de 30 segundos y permite HMAC-SHA-1, HMAC-SHA-256 o HMAC-SHA-512. El RFC exige que cliente y servidor usen parámetros compatibles, que cada autenticador tenga un secreto único y que el tiempo soporte valores mayores de 32 bits.

El código cambia al cambiar el paso de tiempo. El mismo código puede aparecer en más de un periodo por casualidad; por ello el servidor debe aceptar el código recibido solo una vez durante su vigencia.

## Características

- Se genera sin conexión de red en el teléfono, aunque se necesita acceso al servidor para iniciar sesión.
- No requiere enviar el código por SMS o correo.
- Requiere proteger el secreto compartido en el dispositivo y en el servidor.
- Desfase del reloj o retraso al introducir el código puede causar rechazos.
- No es resistente al phishing: un sitio impostor puede capturar el código y retransmitirlo dentro de su ventana válida.

## Casos de uso

- Segundo factor para cuentas personales o corporativas.
- Acceso administrativo cuando se requiere una opción compatible con una amplia variedad de aplicaciones autenticadoras.
- Factor alternativo cuando no está disponible una llave FIDO2/WebAuthn.

Cuando la amenaza principal incluye phishing dirigido, priorizar WebAuthn/FIDO2; TOTP puede mantenerse como alternativa de recuperación según el nivel de riesgo.

## Cómo se implementa

### Enrolamiento

1. Exigir una sesión autenticada y, para cuentas sensibles, una reautenticación o método de mayor seguridad.
2. Generar un secreto criptográficamente aleatorio y único por usuario/autenticador.
3. Mostrarlo una sola vez como QR de aprovisionamiento `otpauth://totp/...` o como clave manual. No incluirlo en telemetría, analítica, logs o capturas de soporte.
4. Mantener el factor en estado pendiente y pedir un código válido para comprobar que la aplicación quedó configurada correctamente.
5. Activar el autenticador tras la verificación y notificar el cambio al usuario.
6. Entregar códigos de recuperación de un solo uso y explicar cómo revocar el autenticador.

### Verificación

1. Recuperar el secreto protegido para la cuenta y calcular el paso correspondiente a la hora del servidor.
2. Comparar el código en tiempo constante con una ventana pequeña que contemple desfase y latencia.
3. Tras una validación exitosa, registrar el paso/código consumido para rechazar la reutilización durante la ventana.
4. Aplicar límites de intentos por cuenta y por origen/contexto, evitando bloqueos que permitan denegación de servicio.
5. Registrar resultado, factor e identificador de autenticador, sin guardar el OTP ni el secreto.

## Recomendaciones

- Usar el paso predeterminado de 30 segundos salvo una razón documentada para cambiarlo; una ventana más amplia aumenta el tiempo disponible para atacar.
- Permitir solo el desfase estrictamente necesario. RFC 6238 recomienda como máximo un paso de tiempo para retraso de transmisión y una tolerancia explícita de sincronización.
- Usar una hora de servidor sincronizada y un tipo de dato que evite el desbordamiento del contador después de 2038.
- Aceptar cada OTP una sola vez mientras sea válido, incluso ante solicitudes concurrentes.
- Cifrar secretos en reposo con gestión de claves adecuada; restringir accesos, rotar claves de cifrado y evitar exponer secretos en respaldos o logs.
- Proteger el canal de verificación con TLS y limitar intentos de adivinación.
- Vincular el secreto a la cuenta y permitir revocación, reemplazo y auditoría de autenticadores.
- Tratar pérdida del teléfono y recuperación de cuenta como parte del diseño de autenticación, no como un proceso de soporte informal.
- No describir TOTP como “a prueba de phishing” ni como suficiente por sí solo para cumplir un requisito de resistencia al phishing.

## Herramientas

- **Aplicaciones autenticadoras:** cualquier aplicación compatible con TOTP/RFC 6238; verificar las opciones de respaldo, sincronización y protección local antes de recomendarla a usuarios.
- **Bibliotecas:** usar implementaciones mantenidas con soporte de RFC 6238, validarlas con los vectores de prueba del RFC y no implementar el algoritmo criptográfico desde cero.
- **Gestión de secretos:** KMS/HSM o un gestor de secretos que limite el descifrado al componente verificador.
- **Identidad:** IdP/IAM con enrolamiento y políticas TOTP integradas, si cumple las necesidades de control y auditoría.
- **Ejemplos:** [Keycloak OTP policy](https://www.keycloak.org/docs/latest/server_admin/#_otp) y [Microsoft Entra OATH tokens](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-authentication-oath-tokens) describen compatibilidad TOTP/OATH. Son referencias de producto, no una recomendación única; confirmar versiones y políticas aplicables.

## Relación con otros conceptos

- [MFA](mfa.md) explica los factores y las políticas de autenticación.
- [OTP](otp.md) describe la familia de códigos de un solo uso, incluyendo HOTP y códigos de desafío.
- Para una implementación de referencia, consulta [TOTP con ASP.NET Core Identity y React](../../../dotnet/net/web/autenticacion/totp-aspnet-core-react.md).

## Referencias

- [RFC 6238: TOTP](https://www.rfc-editor.org/rfc/rfc6238.html)
- [RFC 4226: HOTP](https://www.rfc-editor.org/rfc/rfc4226.html)
- [NIST SP 800-63B-4: Authentication and Authenticator Management](https://pages.nist.gov/800-63-4/sp800-63b.html)
- [NIST: Authenticator Examples](https://pages.nist.gov/800-63-4-Implementation-Resources/authenticator/)

