# cookies

#  SameSite

## SameSite=Strict: Prevención de CSRF

El atributo SameSite en una cookie le indica al navegador si debe enviar la cookie junto con las peticiones que se originan desde un sitio web diferente al sitio donde se estableció la cookie.

Cuando estableces una cookie con SameSite=Strict, le estás diciendo al navegador lo siguiente:

"Esta cookie SOLO debe enviarse si la solicitud se originó en el mismo sitio que la cookie."


1. Flujo SIN SameSite (Vulnerable a CSRF)

Paso | Acción del Usuario / Atacante | Resultado en el Navegador (Cookie de Sesión: SID=XYZ) | Vulnerabilidad
-- | -- | -- | --
A | La víctima inicia sesión en https://banco.com. | El servidor establece la cookie: SID=XYZ; SameSite=None (o no especificado). |  
B | La víctima visita el sitio web malicioso: https://sitio-malicioso.com. |   |  
C | sitio-malicioso.com tiene un formulario oculto o una etiqueta <img> que envía una solicitud a: https://banco.com/transferencia?monto=1000. | El navegador ADJUNTA la cookie SID=XYZ a la solicitud cross-site. | ¡Éxito CSRF! El banco procesa la transferencia porque la solicitud parece legítima y autenticada.

2. Flujo CON SameSite=Strict (Protegido contra CSRF)

 
Paso | Acción del Usuario / Atacante | Resultado en el Navegador (Cookie de Sesión: SID=XYZ) | Protección
-- | -- | -- | --
A | La víctima inicia sesión en https://banco.com. | El servidor establece la cookie: SID=XYZ; SameSite=Strict. |  
B | La víctima visita el sitio web malicioso: https://sitio-malicioso.com. |   |  
C | sitio-malicioso.com tiene un formulario oculto o una etiqueta <img> que envía una solicitud a: https://banco.com/transferencia?monto=1000. | El navegador detecta que es una solicitud cross-site. NO ADJUNTA la cookie SID=XYZ. | ¡Petición Rechazada! El servidor de banco.com no ve la cookie de sesión, trata la solicitud como no autenticada y rechaza la transferencia.

 
 
Consideraciones sobre el Modo 'Strict'

Aunque es la opción más segura, puede ser demasiado restrictiva en ciertos escenarios:

Si un usuario sigue un enlace normal desde un sitio externo (por ejemplo, Google, un email, o un blog) hacia tu sitio, el navegador trataría el enlace como una navegación cross-site.

En modo Strict, la cookie no se enviaría, y si la cookie es de sesión, el usuario aparecería como desconectado y se le pediría que iniciara sesión de nuevo, incluso al hacer clic en un enlace de entrada a tu sitio.

Por esta razón, muchas aplicaciones optan por el valor SameSite=Lax, que es una buena solución intermedia:

- Lax previene CSRF en la mayoría de los casos de métodos peligrosos (POST, PUT, DELETE).
- Lax SÍ permite el envío de la cookie solo en solicitudes GET de navegación de nivel superior (como hacer clic en un enlace) para mantener la experiencia del usuario (por ejemplo, no tener que iniciar sesión de nuevo al llegar de otro sitio).

