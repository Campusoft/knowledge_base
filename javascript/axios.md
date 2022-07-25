# axios

Axios es un Cliente HTTP basado en promesas para node.js y el navegador. 

 En el lado del servidor usa el modulo nativo http de node.js, mientras que en el lado del cliente (navegador) usa XMLHttpRequests.

Caracteristicas
- Soporta el API de Promesa
- Intercepta petición y respuesta
- Transforma petición y datos de respuesta
- Cancela peticiones
- Transformacion automatica de datos JSON
- Soporte para proteger al cliente contra XSRF

Api

axios.request(config)
axios.get(url[, config])
axios.delete(url[, config])
axios.head(url[, config])
axios.options(url[, config])
axios.post(url[, data[, config]])
axios.put(url[, data[, config]])
axios.patch(url[, data[, config]])

Instalacion

$ npm install axios