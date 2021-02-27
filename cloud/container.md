#container 


El cloud computing y los microservicios se vuelven cada vez más populares, Y lo mismo sucede con la organización de aplicaciones en contenedores, ya sea con o sin estado. Los contenedores son unidades de código para una aplicación que se empaquetan junto con sus bibliotecas y dependencias. Esto les permite trasladarse con facilidad y funcionar en cualquier entorno, tanto en la infraestructura de TI tradicional de una computadora de escritorio como en la nube. 


## Docker

## Kubernetes 


## Service Mesh

La malla de servicios (Service Mesh) es una práctica de arquitectura para administrar y visualizar conjuntos de múltiples microservicios basados en contenedores.

Los microservicios requieren un conjunto de funcionalidades comunes como autenticación, políticas de seguridad, protección contra intrusos y ataques de denegación de servicio (DDoS), balanceo de carga, enrutamiento, monitoreo y gestión de fallos.

En una aplicación monolítica, implementamos estos requerimientos una sola vez, pero en entornos con decenas o cientos de contenedores no es práctico repetirlo en cada uno.


Un service mesh proporciona servicios de vigilancia, escalabilidad y alta disponibilidad a través de una API de servicios, en lugar de obligar su implementación en cada microservicio. El beneficio está en reducir la complejidad operativa asociada con las aplicaciones modernas de microservicios.


Open Service Mesh (OSM) is Microsoft’s implementation of the SMI in an actual Service Mesh. This is an Open Source project and a CNCF sandbox project. OSM is intended to be a simple, lightweight Service Mesh and so focusses on providing just the features of the SMI.


Service Mesh - Arquitectura de Microservicios 
https://www.aplyca.com/es/blog/service-mesh 