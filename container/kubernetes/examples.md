# kubernetes - examples

Ejemplo de Patrón Sidecar en Kubernetes
- containers nginx
- volumen logs
- containers busybox en sidecar para extraer/ver logs 
 https://refactorizando.com/ejemplo-de-patron-sidecar-en-kubernetes/

Kubernetes: logs y sidecar containers
- tenemos los dos contenedores compartiendo un volumen (logs-data)
- Para esto utilizamos un container de Sumologic, desplegado como sidecar
https://blog.nicopaez.com/2021/01/25/kubernetes-logs-y-sidecar-containers/

Arquitectura ejemplo de una aplicacion frontend construido con Angular (servido por Nginx) y un backend construido con Net Core (aspnet/kestrel) que consume servicios WFC/SOAP que proveen acceso al core de la organización
- pods de front. (Nginx). filebeat  sidecar: leer los logs escritos por nuestra aplicación y mandarlos a Elastic/Kibana
- pods de backend. (aspnet/kestrel). filebeat  sidecar: leer los logs escritos por nuestra aplicación y mandarlos a Elastic/Kibana
https://blog.nicopaez.com/2020/03/06/llegamos-a-produccion/