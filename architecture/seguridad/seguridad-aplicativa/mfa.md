# Autenticación multifactor (MFA)

## Objetivo y alcance

MFA (*Multi-Factor Authentication*) verifica la identidad de una persona mediante dos o más factores de categorías distintas. Este documento describe el concepto, sus usos y controles de implementación. No sustituye los requisitos de seguridad o cumplimiento aplicables a cada sistema.

## Conceptos

Los factores se agrupan normalmente en:

- **Algo que sabes:** contraseña, PIN o frase secreta.
- **Algo que tienes:** teléfono, llave de seguridad o dispositivo autenticador.
- **Algo que eres:** característica biométrica, como huella o rostro.

Dos pasos no siempre significan dos factores. Por ejemplo, dos contraseñas siguen siendo factores de conocimiento. Una contraseña junto con un código TOTP normalmente combina conocimiento y posesión del dispositivo que genera el código.

**2FA** es MFA con exactamente dos factores. **Verificación en dos pasos** describe dos pasos de un flujo, pero no garantiza por sí sola que sean factores distintos.

## Características

- Añade una comprobación ante el robo o reutilización de una contraseña.
- Puede aplicarse en cada inicio de sesión o ante acciones de riesgo (MFA adaptativa o *step-up*).
- La seguridad depende del método, del enrolamiento, de la recuperación y de la protección de la sesión.
- Los códigos OTP introducidos manualmente pueden ser capturados y retransmitidos en tiempo real; no son resistentes al phishing.
- MFA no elimina la necesidad de proteger contraseñas, sesiones, dispositivos y procesos de recuperación.

## Casos de uso

- Acceso a cuentas administrativas, infraestructura y datos sensibles.
- Inicio de sesión remoto, acceso desde un dispositivo nuevo o desde una ubicación inusual.
- Confirmación de operaciones de alto impacto, como cambios de credenciales o transferencias.
- Acceso de personal interno, proveedores y operadores con privilegios.

La política debe ponderar el riesgo y la experiencia de usuario. Para acciones críticas, se puede requerir una nueva autenticación incluso si la sesión ya está activa.

## Flujo de implementación

1. **Elegir métodos permitidos:** priorizar autenticadores resistentes al phishing, como FIDO2/WebAuthn o llaves de seguridad, cuando el riesgo lo justifique. TOTP puede servir como alternativa ampliamente compatible.
2. **Enrolar el autenticador:** comprobar la identidad con un método confiable, asociar el autenticador a la cuenta y pedir una prueba inicial antes de activarlo.
3. **Verificar en el inicio de sesión:** validar el primer factor y luego el autenticador secundario, manteniendo límites de intentos y protección contra automatización.
4. **Emitir la sesión:** crear la sesión solo después de completar todos los factores requeridos. Marcar el nivel o la fecha de la autenticación si el sistema la usa para autorizar acciones sensibles.
5. **Recuperar y revocar:** permitir revocar autenticadores perdidos, gestionar dispositivos alternativos y notificar cambios importantes.

## Recomendaciones

- Preferir autenticación resistente al phishing cuando se necesite protección contra sitios impostores. NIST aclara que los OTP ingresados manualmente no son resistentes al phishing.
- No tratar SMS, correo electrónico y TOTP como métodos equivalentes: tienen canales, riesgos y mecanismos de generación distintos.
- Proteger el enrolamiento y la recuperación al menos con el mismo cuidado que el inicio de sesión.
- Ofrecer códigos de recuperación de alta entropía, de un solo uso y almacenados de forma segura; regenerarlos invalida los anteriores.
- Limitar intentos, aplicar demoras progresivas y monitorear fallos sin facilitar denegación de servicio por bloqueo abusivo.
- Proteger las sesiones con cookies seguras, expiración apropiada y reautenticación para cambios de alto impacto.
- Registrar eventos de enrolamiento, uso, fallo y revocación sin guardar códigos OTP ni secretos de autenticador.
- Dar al usuario un proceso claro para registrar más de un autenticador y recuperar la cuenta.

## Herramientas

- **Estándares resistentes al phishing:** FIDO2, WebAuthn y llaves de seguridad.
- **OTP:** aplicaciones autenticadoras compatibles con TOTP, autenticadores de hardware, y bibliotecas que implementen RFC 4226/RFC 6238.
- **Gestión de identidad:** proveedores de identidad (IdP) o plataformas IAM con políticas de MFA, enrolamiento, recuperación y auditoría.
- **Ejemplos de plataformas:** [Keycloak](https://www.keycloak.org/docs/latest/server_admin/#_otp) permite configurar políticas OTP; [Microsoft Entra ID](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-authentication-oath-tokens) documenta métodos OATH. Confirmar las capacidades, licencias y disponibilidad para la versión/tenant usados.
- **Pruebas:** entornos de desarrollo y vectores de prueba publicados por el estándar; nunca usar secretos reales en pruebas o capturas.

La herramienta se debe evaluar por sus métodos soportados, protección de secretos, opciones de recuperación, auditoría, accesibilidad y capacidad de integrarse con las políticas de sesión.

## Relación con otros conceptos

- [OTP](otp.md) describe contraseñas de un solo uso y sus variantes.
- [TOTP](totp.md) es una variante de OTP basada en tiempo que puede usarse como factor de posesión dentro de MFA.

## Referencias

- [NIST SP 800-63B-4: Authentication and Authenticator Management](https://pages.nist.gov/800-63-4/sp800-63b.html)
- [NIST: Authenticator Examples](https://pages.nist.gov/800-63-4-Implementation-Resources/authenticator/)
- [RFC 6238: TOTP](https://www.rfc-editor.org/rfc/rfc6238.html)
- [RFC 4226: HOTP](https://www.rfc-editor.org/rfc/rfc4226.html)

