# Implementar TOTP con ASP.NET Core Identity y React

## Objetivo y alcance

Esta guía muestra cómo integrar TOTP como segundo factor en una aplicación web con backend ASP.NET Core y frontend React. Usa ASP.NET Core Identity para gestionar el secreto, validar códigos y emitir códigos de recuperación. React solo presenta el flujo y envía los datos al backend; no implementa el algoritmo TOTP.

Para el concepto y las recomendaciones independientes de plataforma, consulta [TOTP](../../../../architecture/seguridad/seguridad-aplicativa/totp.md) y [MFA](../../../../architecture/seguridad/seguridad-aplicativa/mfa.md).

## Arquitectura

```text
Aplicación React  -- HTTPS -->  API ASP.NET Core + ASP.NET Core Identity
       |                                  |
       | muestra QR y envía código        | crea/verifica el secreto TOTP
       |                                  | emite sesión y códigos de recuperación
       +---------- app autenticadora -----+
```

El secreto TOTP es equivalente a una credencial: durante el enrolamiento se comparte con la aplicación autenticadora y el servidor. La API debe entregarlo solo a la sesión autenticada que está configurando el factor, por HTTPS y sin persistirlo en logs o telemetría.

## Flujo de enrolamiento

1. El usuario inicia sesión y abre la sección de seguridad de su cuenta.
2. React solicita el inicio del enrolamiento a una ruta protegida del backend.
3. El backend obtiene o genera la clave del autenticador mediante ASP.NET Core Identity y devuelve la clave compartida o el URI `otpauth://` necesario para crear el QR.
4. React presenta el QR con un componente de generación de códigos QR. También ofrece la clave manualmente para dispositivos que no puedan escanearlo.
5. El usuario introduce el código de seis dígitos de su aplicación autenticadora.
6. React envía el código a una ruta protegida de confirmación. El backend lo valida con Identity y solo entonces habilita MFA para la cuenta.
7. El backend entrega códigos de recuperación de un solo uso. React los muestra una vez e indica guardarlos de forma segura.

La API Identity de ASP.NET Core documenta `POST /manage/2fa`: una solicitud inicial obtiene el `sharedKey`; otra con `enable: true` y `twoFactorCode` activa el factor y puede devolver códigos de recuperación. La ruta debe estar disponible solo para el usuario autenticado.

## Flujo de inicio de sesión

1. React envía usuario y contraseña al endpoint de inicio de sesión.
2. Si MFA no está habilitado, el servidor completa la autenticación normal.
3. Si MFA está habilitado, el backend solicita un TOTP o un código de recuperación y no emite la sesión final todavía.
4. React muestra el formulario del segundo factor y envía el código al backend.
5. El backend valida el código con Identity y emite la sesión o token únicamente si los factores requeridos son correctos.

Con Identity API, `POST /login` admite `twoFactorCode` o `twoFactorRecoveryCode` cuando la cuenta requiere el segundo factor. Para cookies, configure el cliente para incluir credenciales y proteja las operaciones contra CSRF; para bearer tokens, adjunte el token de acceso según la política de la API.

## Contrato de API sugerido

Se pueden exponer las rutas estándar de Identity API o crear una capa de rutas de aplicación que las envuelva. Mantener contratos explícitos ayuda a no acoplar React a los detalles internos de Identity.

| Operación | Solicitud | Respuesta esperada |
| --- | --- | --- |
| Iniciar enrolamiento | `POST /manage/2fa` con `{}` | Estado MFA y clave compartida de aprovisionamiento |
| Confirmar enrolamiento | `POST /manage/2fa` con `{ "enable": true, "twoFactorCode": "123456" }` | MFA habilitado y códigos de recuperación, que se muestran una sola vez |
| Iniciar sesión | `POST /login` con credenciales y, si corresponde, `twoFactorCode` | Sesión/cookie o token solo tras validar el segundo factor |
| Recuperación | `POST /login` con `twoFactorRecoveryCode` | Sesión/cookie o token y código de recuperación invalidado |
| Revocar/reiniciar factor | Ruta protegida de gestión de cuenta | Estado actualizado y notificación de seguridad |

Los endpoints integrados requieren que la aplicación haya configurado ASP.NET Core Identity API endpoints. Si el proyecto usa MVC/Razor Identity o una arquitectura de API propia, implementar el mismo flujo con `UserManager` y `SignInManager`, conservando la validación y el almacenamiento del secreto dentro del backend.

## Ejemplo de interacción en React

El siguiente ejemplo muestra la forma del intercambio. `api.post` representa el cliente HTTP de la aplicación; `QrCode` representa un componente de una biblioteca QR elegida por el proyecto. El URI o clave de aprovisionamiento solo se mantiene en memoria durante el enrolamiento.

```tsx
function buildTotpProvisioningUri(sharedKey: string, issuer: string, account: string) {
  const label = `${encodeURIComponent(issuer)}:${encodeURIComponent(account)}`;
  const query = new URLSearchParams({
    secret: sharedKey.replace(/\s/g, ""),
    issuer,
    algorithm: "SHA1",
    digits: "6",
    period: "30",
  });
  return `otpauth://totp/${label}?${query.toString()}`;
}

type TotpSetup = {
  sharedKey: string;
  isTwoFactorEnabled: boolean;
};

function TotpEnrollment() {
  const [setup, setSetup] = useState<TotpSetup | null>(null);
  const [code, setCode] = useState("");
  const [recoveryCodes, setRecoveryCodes] = useState<string[]>([]);
  const [error, setError] = useState<string | null>(null);

  async function beginEnrollment() {
    setError(null);
    const result = await api.post<TotpSetup>("/manage/2fa", {});
    setSetup(result);
  }

  async function confirmEnrollment(event: React.FormEvent) {
    event.preventDefault();
    setError(null);
    try {
      const result = await api.post<TotpSetup & { recoveryCodes?: string[] }>(
        "/manage/2fa",
        { enable: true, twoFactorCode: code.replace(/\s/g, "") }
      );
      setRecoveryCodes(result.recoveryCodes ?? []);
      setSetup(result);
      setCode("");
    } catch {
      setError("No se pudo verificar el código. Comprueba la hora del dispositivo e inténtalo de nuevo.");
    }
  }

  if (recoveryCodes.length > 0) {
    return <RecoveryCodes codes={recoveryCodes} />;
  }

  return (
    <section>
      {!setup ? (
        <button type="button" onClick={beginEnrollment}>
          Configurar aplicación autenticadora
        </button>
      ) : (
        <>
          <QrCode value={buildTotpProvisioningUri(setup.sharedKey, "MiAplicacion", currentUser.email)} />
          <p>Escanea el código con tu aplicación autenticadora.</p>
          <form onSubmit={confirmEnrollment}>
            <label htmlFor="totp-code">Código de verificación</label>
            <input
              id="totp-code"
              inputMode="numeric"
              autoComplete="one-time-code"
              pattern="[0-9]{6}"
              maxLength={6}
              value={code}
              onChange={(event) => setCode(event.target.value)}
              required
            />
            <button type="submit">Activar MFA</button>
          </form>
          {error && <p role="alert">{error}</p>}
        </>
      )}
    </section>
  );
}
```

`buildTotpProvisioningUri` debe construir el formato `otpauth://totp/...` con `issuer`, cuenta, secreto y parámetros compatibles con el servidor. Codificar correctamente los componentes URI y mantener emisor, algoritmo, cantidad de dígitos y periodo coherentes. No enviar esta URI a analítica, logs ni servicios externos de generación de QR.

## Recomendaciones de seguridad

- Usar HTTPS en todos los entornos fuera de desarrollo local.
- Exigir autenticación reciente para enrolar, reemplazar o desactivar un autenticador.
- Entregar el secreto solo mientras el usuario autenticado configura el factor; no devolverlo en llamadas posteriores.
- Usar un secreto único por usuario/dispositivo y almacenamiento protegido en el backend.
- Limitar intentos fallidos, aceptar el código una sola vez por periodo válido y permitir solo una ventana temporal pequeña.
- No emitir cookie, token de acceso ni refresh token definitivo antes de completar el desafío MFA.
- Si se usan cookies, activar protección CSRF y configurar `SameSite`, `Secure` y CORS para los orígenes esperados. No combinar cookies de sesión con CORS abierto.
- Los códigos de recuperación se muestran una sola vez; almacenarlos de forma segura y consumir cada código atómicamente.
- Registrar enrolamiento, activación, fallos, uso de recuperación y revocación sin registrar códigos, claves ni URI `otpauth`.
- Permitir revocar el autenticador perdido y notificar al usuario por un canal previamente verificado.
- TOTP no es resistente al phishing. Si el sistema requiere resistencia al phishing, ofrecer WebAuthn/FIDO2 como método preferente.

## Herramientas

- **Backend:** ASP.NET Core Identity y sus Identity API endpoints, o la integración Identity existente del proyecto.
- **Frontend:** React para formularios y estados de pantalla; una biblioteca QR mantenida para mostrar el URI de aprovisionamiento.
- **Gestión de claves:** Data Protection, KMS/HSM o un gestor de secretos, según el diseño de almacenamiento y despliegue.
- **Pruebas de interoperabilidad:** vectores del [RFC 6238](https://www.rfc-editor.org/rfc/rfc6238.html) y aplicaciones autenticadoras compatibles.

No implementar HMAC/TOTP manualmente en React. El cliente envía el código ingresado y el backend es el único componente que decide si es válido.

## Referencias

- [ASP.NET Core Identity API authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization)
- [QR codes for authenticator apps in a Blazor WebAssembly app](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/standalone-with-identity/qrcodes-for-authenticator-apps)
- [React: formularios](https://react.dev/reference/react-dom/components/form)
- [RFC 6238: TOTP](https://www.rfc-editor.org/rfc/rfc6238.html)
- [MFA](../../../../architecture/seguridad/seguridad-aplicativa/mfa.md)
- [OTP](../../../../architecture/seguridad/seguridad-aplicativa/otp.md)
- [TOTP](../../../../architecture/seguridad/seguridad-aplicativa/totp.md)
