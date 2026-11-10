# Contraseñas de un solo uso (OTP)

## Objetivo y alcance

OTP (*One-Time Password*) es una contraseña o código que debe aceptarse una sola vez, normalmente dentro de un periodo o flujo limitado. El término puede referirse tanto a códigos derivados de un algoritmo como, en el uso común, a códigos enviados por un canal. Este documento explica las variantes y sus controles; para conceptos de factores, consultar [MFA](mfa.md).

## Conceptos y variantes

- **HOTP:** deriva el código de un secreto y un contador que aumenta con cada uso (RFC 4226).
- **TOTP:** sustituye el contador por un valor derivado del tiempo (RFC 6238). Ver [TOTP](totp.md).
- **Código de desafío o fuera de banda:** el servicio genera un código aleatorio para una operación y lo entrega, por ejemplo, por SMS o correo. Es un patrón de generación y entrega diferente de TOTP/HOTP.

En todos los casos, el servidor necesita comprobar que el valor corresponde al usuario y al propósito, que sigue vigente y que no fue aceptado antes. Un código OTP no cifra el canal ni la información de la operación.

## Características

- Reduce la utilidad de una contraseña reutilizada si el código no se ha usado y sigue vigente.
- Tiene un espacio de búsqueda limitado, especialmente si es corto; por eso requiere límites de intentos.
- HOTP depende de sincronizar contadores; TOTP depende de relojes razonablemente sincronizados.
- Los códigos enviados por canales pueden ser interceptados o redirigidos según el canal y la cuenta asociada.
- La introducción manual de un OTP permite que un atacante lo retransmita en tiempo real, por lo que OTP no es resistente al phishing.

## Casos de uso

- Segundo factor de acceso, habitualmente con TOTP.
- Confirmación de una acción o transacción concreta con un código aleatorio ligado a esa operación.
- Recuperación o verificación de contacto, sujeto a una evaluación del riesgo y a controles más sólidos para cambios sensibles.
- Dispositivos o sistemas con soporte HOTP cuando el esquema de contador sea adecuado.

No asumir que un OTP enviado por correo o SMS constituye MFA por sí solo: si el mismo canal también da acceso a la cuenta o no representa un factor independiente, puede no aportar un factor distinto.

## Cómo se implementa

### OTP aleatorio ligado a un desafío

1. Generar el código con un generador criptográficamente seguro y asociarlo a usuario, propósito y desafío/operación.
2. Guardar una representación verificable y protegida con una clave del servidor (por ejemplo, HMAC); un hash simple permite probar fuera de línea todas las combinaciones de un código corto. Registrar también la fecha de expiración y el estado de uso.
3. Entregarlo mediante el canal elegido y limitar la cantidad y frecuencia de envíos.
4. Al recibirlo, comparar de forma segura, verificar usuario, propósito, vencimiento e intentos.
5. Marcarlo como consumido de forma atómica para impedir dos validaciones concurrentes del mismo código.
6. Invalidar el código anterior al emitir uno nuevo cuando la política lo requiera.

### HOTP o TOTP

El autenticador y el verificador comparten un secreto. El autenticador calcula el OTP y el servidor calcula los valores candidatos para compararlos. Se debe evitar escribir el secreto en logs o exponerlo a componentes que no lo necesitan. La especificación detallada está en [TOTP](totp.md).

## Recomendaciones de seguridad

- Usar códigos impredecibles y una vigencia corta apropiada al propósito; evitar reutilización y aceptar cada valor una sola vez.
- Definir límites de intentos por cuenta y por contexto, aplicar demoras y alertar ante patrones anómalos.
- Vincular los códigos de desafío a la operación y usuario específicos; no aceptar un código de una operación para otra.
- Proteger el transporte con TLS y proteger los datos almacenados; no guardar códigos en texto claro cuando no sea necesario.
- No registrar códigos, secretos, URI de configuración ni QR de enrolamiento.
- Limitar envíos para reducir abuso, spam y costos; las respuestas de error no deben revelar si una cuenta existe.
- Ofrecer alternativas de recuperación y revocación seguras.
- Para proteger contra phishing, considerar FIDO2/WebAuthn en lugar de códigos introducidos manualmente.

## Herramientas

- **Bibliotecas de OTP:** seleccionar una implementación mantenida que declare compatibilidad con RFC 4226 o RFC 6238 y que permita configurar algoritmo, dígitos y paso conforme a las necesidades.
- **Generación aleatoria:** usar el generador criptográficamente seguro provisto por la plataforma para códigos de desafío.
- **Entrega:** proveedores de SMS/correo pueden transportar códigos aleatorios, pero no implementan por sí mismos HOTP ni TOTP.
- **Autenticación:** un IdP/IAM puede gestionar factores, políticas, enrolamiento y recuperación.
- **Ejemplos de implementación:** [Keycloak OTP policy](https://www.keycloak.org/docs/latest/server_admin/#_otp) y [Microsoft Entra OATH tokens](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-authentication-oath-tokens) documentan configuraciones de OTP/OATH en sus plataformas. Verificar compatibilidad y condiciones vigentes antes de adoptarlas.

Validar cualquier biblioteca contra los vectores de prueba del RFC correspondiente y revisar su mantenimiento y manejo de secretos antes de producción.

## Referencias

- [RFC 4226: HOTP](https://www.rfc-editor.org/rfc/rfc4226.html)
- [RFC 6238: TOTP](https://www.rfc-editor.org/rfc/rfc6238.html)
- [NIST SP 800-63B-4: Authentication and Authenticator Management](https://pages.nist.gov/800-63-4/sp800-63b.html)
- [NIST: ejemplos y clases de autenticadores](https://pages.nist.gov/800-63-4-Implementation-Resources/authenticator/)

