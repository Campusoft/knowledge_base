# PWA (Progressive Web Apps)

Aplicaciones web que utilizan tecnologías web modernas para ofrecer una
experiencia similar a la de una aplicación nativa: instalables, rápidas,
confiables y con acceso a funcionalidades del dispositivo.

https://developer.mozilla.org/es/docs/Web/Progressive_web_apps

# Concepto

Una PWA es una aplicación web normal (HTML, CSS, JavaScript) que mejora su
capacidad aprovechando las APIs del navegador. Se pueden instalar en el
dispositivo sin pasar por una tienda de aplicaciones, funcionan sin conexión y
se sienten como aplicaciones nativas.

Características principales:

- **Instalable**: se agrega a la pantalla de inicio con un icono propio.
- **Confiables**: carga instantánea y funcionan sin conexión (offline).
- **Rápidas**: responden rápidamente a la interacción del usuario.
- **Enlazables**: se comparten mediante una URL.
- **Progresivas**: funcionan en cualquier navegador, y mejoran si el navegador
  lo soporta.
- **Actualizables**: siempre están actualizadas (service worker).
- **Seguras**: se sirven mediante HTTPS.

# Tecnologías requeridas

- **HTTPS**: obligatorio para las funcionalidades PWA (excepto en localhost).
- **Manifest (manifest.json)**: archivo JSON que define el nombre, iconos,
  colores, pantalla de inicio, orientación y modo de visualización.
- **Service Worker**: script de JavaScript que se ejecuta en segundo plano y
  permite el funcionamiento offline, la cache de recursos y las notificaciones
  push. Es el corazón de una PWA.
- **Web App Manifest** + **Service Worker** son los dos requisitos mínimos
  para que un navegador ofrezca la instalación.

APIs complementarias:

- **Notifications API / Push API**: notificaciones push.
- **Cache API**: almacenamiento de respuestas HTTP para offline.
- **IndexedDB**: base de datos local para grandes cantidades de datos.
- **Storage API**: gestión del almacenamiento disponible.
- **Background Sync API**: sincronización de datos en segundo plano cuando hay
  conexión.
- **Geolocation API**: ubicación del dispositivo.
- **Web Share API / Share Target API**: compartir contenido nativo.
- **Media Session API**: control de reproducción multimedia.
- **Payment Request API**: pagos dentro de la web.

## Service Worker (conceptos clave)

- Se registra desde la página web y se ejecuta como un "proxy" entre la app y
  la red.
- Ciclo de vida: **install** → **activate** → **fetch** / **sync** / **push**.
- Estrategias de cache comunes:
  - **Cache first**: sirve desde cache y actualiza en segundo plano (ideal para
    recursos estáticos).
  - **Network first**: intenta red, si falla usa cache (ideal para contenido
    dinámico).
  - **Stale-while-revalidate**: sirve cache e inmediatamente actualiza.
  - **Network only**: siempre red.
- Al actualizar un service worker, el nuevo debe tomar control para servir las
  nuevas versiones de la app.

# Casos de uso

- **E-commerce y retail**: catálogos, carritos y compras con carga rápida y
  modo offline.
- **Noticias y contenido**: lectura sin conexión y notificaciones push de
  novedades.
- **Dashboards y herramientas de trabajo**: acceso rápido y disponible offline.
- **Aplicaciones de viajes / transporte**: consulta de itinerarios offline.
- **Apps de streaming / multimedia**: reproducción y control desde pantalla.
- **Formularios y encuestas**: captura de datos sin conexión con sincronización
  posterior.
- **Apps con gran alcance** en mercados donde el tráfico móvil es limitado o
  costoso.
- **Progresión hacia app nativa**: para probar el mercado antes de invertir en
  una app nativa.

Ejemplos reales: Twitter Lite, Pinterest, Starbucks, Spotify, Trivago, Uber,
MakeMyTrip.

# Recomendaciones

- Usar **HTTPS** desde el inicio.
- Definir una **estrategia de cache** acorde al tipo de contenido.
- **Pruebas en múltiples navegadores y dispositivos** (Chrome, Safari, Edge,
  Firefox).
- Medir y optimizar el rendimiento con **Lighthouse** (auditoría integrada de
  PWA).
- Mantener el **manifest** y los **iconos** actualizados y bien configurados.
- Planear la **estrategia de actualización** del service worker para no servir
  versiones obsoletas.
- Considerar el **almacenamiento offline** (IndexedDB) para datos sensibles y
  sincronización.
- Evitar animar el render con pantallas de splash obsoletas; usar el splash
  nativo definido en el manifest.
- Implementar **notificaciones push** solo cuando aportan valor real al
  usuario (evitar spam).

# Limitaciones

- **Soporte desigual de Safari/iOS**: algunas APIs (push, instalación) están
  limitadas o restringidas en iOS.
- **Acceso limitado al hardware**: no todas las APIs de dispositivo están
  disponibles (Bluetooth, NFC, etc.).
- **No todas las funciones nativas**: la integración profunda con el sistema
  (calendario, contactos, etc.) puede requerir app nativa.
- **Almacenamiento limitado y gestionable por el navegador**: el espacio offline
  puede ser liberado por el navegador.
- **Descubrimiento**: no están en las tiendas de aplicaciones (Apple y Google
  no las listan de forma natural), aunque la instalación desde el navegador
  mejora la distribución.
- **Rendimiento en dispositivos de gama baja** dependiendo de la complejidad de
  la app.
- **Bloqueo de notificaciones**: muchos usuarios rechazan el permiso de
  notificaciones.
- **Dependencia del navegador**: las actualizaciones de APIs dependen del
  navegador y no del desarrollador.

# PWA vs nativa vs híbrida

| Aspecto | PWA | Nativa | Híbrida (Ionic/Flutter web) |
| --- | --- | --- | --- |
| Instalación | Desde el navegador | Tienda de apps | Tienda de apps |
| Tiendas | No (pero soporte parcial) | Sí | Sí |
| Offline | Service Worker | Sí (storage local) | Variable |
| Acceso a hardware | Limitado | Completo | Vía plugins |
| Costo de desarrollo | Bajo (una base web) | Alto (por plataforma) | Medio (una base web) |
| Descubrimiento | SEO / URLs | Tiendas | Tiendas |
| Notificaciones push | Sí (con permiso) | Sí | Sí |

# Herramientas

- **Lighthouse**: auditoría de calidad PWA integrada en DevTools de Chrome.
- **Workbox**: librería de Google para generar service workers.
- **PWABuilder**: herramienta para crear y publicar PWAs en tiendas.
  https://www.pwabuilder.com/
- **web.dev**: guías oficiales de Google para PWAs.
  https://web.dev/progressive-web-apps
- **Chrome DevTools**: debugging de manifest, service workers, cache y
  almacenamiento.
- **Fugu API Tracker**: seguimiento del soporte de APIs en navegadores.

# Referencias

- MDN Web Docs - Progressive web apps:
  https://developer.mozilla.org/es/docs/Web/Progressive_web_apps
- web.dev - Progressive Web Apps:
  https://web.dev/progressive-web-apps
- Google Developers - Introduction to PWA:
  https://developer.chrome.com/docs/devtools/progressive-web-apps/
- PWABuilder: https://www.pwabuilder.com/
- Angular PWA (en esta base de conocimiento): ver `angular/angular.md`
