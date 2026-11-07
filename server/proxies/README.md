# Proxies inversos y servidores de borde

Documentación sobre proxies inversos, balanceadores y servidores de borde que reciben tráfico HTTP/TCP/UDP y lo enrutan hacia aplicaciones internas. Los documentos cubren características, casos de uso, arquitectura y recomendaciones de operación.

## Documentos

| Documento | Enfoque |
|---|---|
| [Nginx](nginx.md) | Servidor web y proxy inverso con caché, balanceo y proxy TCP/UDP. Amplio ecosistema y configuración declarativa. |
| [Envoy](envoy.md) | Proxy L4/L7 dinámico y de alto rendimiento, orientado a service mesh y configuración vía xDS. |
| [Traefik](traefik.md) | Proxy de aplicaciones cloud-native con descubrimiento automático de servicios y TLS integrado. |
| [Caddy](caddy.md) | Servidor web y proxy con HTTPS automático, configuración simple y binario único sin dependencias. |

## Cómo elegir

| Necesidad | Opción sugerida |
|---|---|
| Servir sitios estáticos y hacer proxy con caché en una infraestructura tradicional | Nginx |
| Exponer contenedores o servicios de Kubernetes con despliegues frecuentes y poca configuración manual | Traefik |
| Implementar un service mesh, mTLS, gRPC o control fino de tráfico con configuración dinámica | Envoy |
| Publicar sitios en un VPS o servidor propio con HTTPS automático y configuración mínima | Caddy |
| Necesidades mixtas de servidor web, proxy y balanceo con máxima madurez operativa | Nginx, o Traefik si el entorno es contenedorizado |

Ninguna opción es excluyente: es común usar Traefik o Envoy como ingress y Nginx o Caddy detrás para servir contenido estático o aplicaciones concretas. La elección depende del entorno de despliegue, la frecuencia de cambios, el equipo que opera la plataforma y el nivel de control requerido.

## Criterios de comparación

- **Descubrimiento de servicios:** Traefik y Envoy destacan por integrarse con orquestadores y control planes; Nginx y Caddy suelen requerir configuración explícita o automatización externa.
- **TLS:** Caddy lo automatiza por defecto; Traefik lo integra mediante certificate resolvers ACME; Nginx y Envoy requieren configuración y renovación adicionales.
- **Complejidad operativa:** Caddy y Nginx son los más directos de operar; Traefik reduce trabajo en entornos dinámicos; Envoy ofrece el mayor control a cambio de mayor complejidad.
- **Rendimiento:** los cuatro son adecuados para producción. La diferencia real suele estar en la configuración, el modelo de conexiones y el uso de caché, no en el producto por sí solo.
- **Extensibilidad:** Envoy y Traefik tienen filtros/plugins; Nginx usa módulos (algunos comerciales); Caddy usa módulos compilados con `xcaddy`.

## Mantenimiento

Al agregar o mover documentos, actualiza este índice y conserva los enlaces relativos a archivos y subsecciones. Verifica las versiones y enlaces oficiales al momento de ampliar cada guía.
