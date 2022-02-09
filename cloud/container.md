#container 


El cloud computing y los microservicios se vuelven cada vez más populares, Y lo mismo sucede con la organización de aplicaciones en contenedores, ya sea con o sin estado. Los contenedores son unidades de código para una aplicación que se empaquetan junto con sus bibliotecas y dependencias. Esto les permite trasladarse con facilidad y funcionar en cualquier entorno, tanto en la infraestructura de TI tradicional de una computadora de escritorio como en la nube. 


# Docker

# Kubernetes 


# Service Mesh

La malla de servicios (Service Mesh) es una práctica de arquitectura para administrar y visualizar conjuntos de múltiples microservicios basados en contenedores.

Los microservicios requieren un conjunto de funcionalidades comunes como autenticación, políticas de seguridad, protección contra intrusos y ataques de denegación de servicio (DDoS), balanceo de carga, enrutamiento, monitoreo y gestión de fallos.

En una aplicación monolítica, implementamos estos requerimientos una sola vez, pero en entornos con decenas o cientos de contenedores no es práctico repetirlo en cada uno.


Un service mesh proporciona servicios de vigilancia, escalabilidad y alta disponibilidad a través de una API de servicios, en lugar de obligar su implementación en cada microservicio. El beneficio está en reducir la complejidad operativa asociada con las aplicaciones modernas de microservicios.


Open Service Mesh (OSM) is Microsoft’s implementation of the SMI in an actual Service Mesh. This is an Open Source project and a CNCF sandbox project. OSM is intended to be a simple, lightweight Service Mesh and so focusses on providing just the features of the SMI.


Service Mesh - Arquitectura de Microservicios 
-  Un service mesh proporciona servicios de vigilancia, escalabilidad y alta disponibilidad a través de una API de servicios, en lugar de obligar su implementación en cada microservicio. El beneficio está en reducir la complejidad operativa asociada con las aplicaciones modernas de microservicios.
- ¿Cómo hace una malla de servicios para abstraer esta complejidad en cada microservicio? Para entenderlo, revisemos el patrón de arquitectura conocido como sidecar.
- El contenedor sidecar funciona como proxy, e implementa las funcionalidades comunes como proxy, autenticación, monitoreo, etc, dejando los microservicios libres para enfocarse en su funcionalidad específica. Un controlador central (control plane) organiza las conexiones, dirige el flujo de tráfico entre los proxies y el plano de control recolectando las métricas de rendimiento.
https://www.aplyca.com/es/blog/service-mesh 


"A Service Mesh Is Not an ESB"

## Referencias

Do I Need an API Gateway if I Use a Service Mesh?
https://blog.christianposta.com/microservices/do-i-need-an-api-gateway-if-i-have-a-service-mesh/


Service Mesh vs API Gateway
https://medium.com/microservices-in-practice/service-mesh-vs-api-gateway-a6d814b9bf56


How eBPF will solve Service Mesh - Goodbye Sidecars
- What is Service Mesh?
- Library Service Mesh Model
- service meshes are commonly implemented using an architecture called the sidecar model.
- Kernel Service Mesh with eBPF
https://isovalent.com/blog/post/2021-12-08-ebpf-servicemesh

## Cilium

Cilium is an open source project to provide networking, security, and observability for cloud native environments such as Kubernetes clusters and other container orchestration platforms.

https://cilium.io/learn/