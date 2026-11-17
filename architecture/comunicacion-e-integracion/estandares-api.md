# Estándares y diseño de APIs

Estas referencias orientan el diseño de contratos. Selecciona el protocolo, formato y política de compatibilidad según los consumidores y el contexto de integración.

## Guías de diseño

- [Microsoft REST API Guidelines](https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md)
- [Google API Design Guide](https://cloud.google.com/apis/design/) — aplica a APIs REST y RPC, con atención a gRPC.
- [APIs del repositorio](api.md)

## Errores HTTP

[RFC 7807 — Problem Details for HTTP APIs](https://www.rfc-editor.org/rfc/rfc7807) definió un formato común para detalles de errores HTTP. La especificación actualizada es [RFC 9457](https://www.rfc-editor.org/rfc/rfc9457), que obsoleta RFC 7807. Para diseños nuevos, consultar RFC 9457.

## Formatos de especificación

| Formato | Uso o representación habitual |
|---|---|
| OpenAPI | Descripción de APIs HTTP en JSON o YAML. |
| RAML | Descripción de APIs en YAML. |
| Postman Collection | Colecciones ejecutables de solicitudes, en JSON. |
| Insomnia | Exportación de colecciones y solicitudes; el formato depende de la versión. |
| HAR | Registro de tráfico HTTP en JSON. |
| API Blueprint | Descripción de API en Markdown. |
| WADL | Descripción de servicios HTTP en XML; formato histórico. |
| WSDL | Descripción de servicios web SOAP en XML. |
| Google Discovery | Descripción JSON usada por APIs de Google. |
| APIMATIC | Herramientas de transformación y generación para descripciones de API. |

La tabla es orientativa: verificar versiones y compatibilidad en la herramienta elegida. Para contratos OpenAPI del repositorio, consultar [Formatos y esquemas](formatos.md).
