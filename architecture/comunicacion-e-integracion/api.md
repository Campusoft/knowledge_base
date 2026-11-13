# Concept

In APIs idempotency is a powerful concept. An idempotent endpoint is one that can be called any number of times while guaranteeing that the side effects will occur only once. In a messy world where clients and servers that may occasionally crash or have their connections drop partway through a request, it’s a huge help in making systems more robust to failure. Clients that are uncertain whether a request succeeded or failed can simply keep retrying it until they get a definitive response.


# Rest API




## Manejo de errores

Las API HTTP deben comunicar los errores mediante el código de estado HTTP apropiado y una representación que permita entender el problema. RFC 9110 define la semántica de los códigos de estado; RFC 9457 estandariza el formato de los detalles del problema para las API HTTP.

Cuando se devuelvan detalles estructurados, se recomienda usar el tipo de medio `application/problem+json` y el objeto Problem Details de RFC 9457:

```http
HTTP/1.1 400 Bad Request
Content-Type: application/problem+json
```

```json
{
  "type": "https://api.example.com/problems/invalid-request",
  "title": "La solicitud no es válida",
  "status": 400,
  "detail": "Uno o más campos tienen valores inválidos.",
  "instance": "/orders/123",
  "errors": [
    {
      "pointer": "/email",
      "detail": "Debe tener un formato de correo válido."
    }
  ]
}
```

Los miembros estándar son:

- `type`: URI que identifica la clase de problema. Es el identificador principal; si se omite, se asume `about:blank`.
- `title`: resumen legible del tipo de problema, estable entre ocurrencias salvo localización.
- `status`: código HTTP asociado al problema. El código de la respuesta HTTP es el que determina el resultado para los clientes y debe coincidir con este valor cuando se incluya.
- `detail`: explicación de esta ocurrencia, orientada a ayudar al cliente a corregirla. Los clientes no deben analizar este texto para tomar decisiones.
- `instance`: URI que identifica esta ocurrencia concreta.

Las API pueden añadir miembros de extensión, como `errors` para comunicar errores de validación por campo. Deben documentarse y mantenerse estables como parte del contrato de la API. Para identificar una condición de error, los clientes deben apoyarse en `type` y en los miembros estructurados, no en el texto de `title` o `detail`.

### Convenciones

- Elegir el código de estado según la semántica HTTP definida en RFC 9110; no devolver `200 OK` para indicar que una operación falló.
- Mantener coherencia en los tipos de problema y en la estructura de las respuestas de error entre operaciones.
- Documentar en OpenAPI los códigos de estado, el tipo de contenido `application/problem+json`, el esquema y ejemplos de los errores que puede devolver cada operación.
- No exponer trazas, consultas, secretos ni detalles internos de implementación en `detail` u otros miembros.
- En respuestas `401 Unauthorized`, incluir el desafío de autenticación requerido por HTTP (`WWW-Authenticate`).

Referencias:

- [RFC 9110 — HTTP Semantics](https://www.rfc-editor.org/rfc/rfc9110.html)
- [RFC 9457 — Problem Details for HTTP APIs](https://www.rfc-editor.org/rfc/rfc9457.html)
- [OpenAPI Specification](https://spec.openapis.org/oas/latest.html)

# GraphQL

GraphQL entre nuestros equipos por su habilidad de agregar información provista por diferentes recursos. Esta vez queremos advertir sobre el uso de Apollo Federation y su soporte sólido para un grafo de datos único y unificado para tu compañía. Si bien, a primera vista, la idea de tener conceptos ubicuos para toda la organización es tentadora, debemos tener en cuenta intentos similares ya propuestos en la industria, como MDM y modelos de datos canónicos, entre otros, que han expuesto las dificultades de este  enfoque. Los retos pueden ser significativos, especialmente cuando el dominio en el que nos encontramos es lo suficientemente complejo como para crear un modelo único y unificado.

Ref: thoughtworks.com/radar. Radar-vol-23-es.pdf 



# Referencias


Comparing API Architectural Styles: SOAP vs REST vs GraphQL vs RPC

Two separate applications need an intermediary to talk to each other. So, developers often build bridges – Application Programming Interfaces – to allow one system to access the information or functionality of another.

- graficos years aparen los estilos de apis
- ventajas desvantajas cadb uno
https://www.altexsoft.com/blog/soap-vs-rest-vs-graphql-vs-rpc/
 
