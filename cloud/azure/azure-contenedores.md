# Azure Kubernetes Service (AKS)

Es el servicio insignia de Azure para el despliegue y la gestión de clústeres de Kubernetes administrados. Si necesitas orquestar, escalar y gestionar aplicaciones complejas en contenedores, AKS es la opción más completa, ofreciendo portabilidad y flexibilidad.

# Azure Container Apps (ACA)

Ideal para crear e implementar microservicios y aplicaciones modernas basadas en contenedores con un enfoque sin servidor (serverless). Es más sencillo de usar que AKS y está optimizado para microservicios basados en eventos y aplicaciones web.

Azure Container Apps allows you to run containerized applications without worrying about orchestration or infrastructure.


- Escalado	Automático y basado en eventos. Soporte nativo para escalar hasta cero réplicas (ahorro máximo de costes) en base a tráfico HTTP, colas de mensajes (Kafka, Service Bus, etc.) o métricas de CPU/memoria.

Escenarios
- Carga de Trabajo	Microservicios, APIs web, y cualquier aplicación web de larga duración.



Utiliza Kubernetes por debajo, pero oculta toda su complejidad.


# Azure App Service (Web App for Containers)

Permite implementar aplicaciones web en contenedores de Docker (Linux y Windows) de forma fácil y rápida. Si buscas un servicio PaaS (Plataforma como Servicio) manejado para tu web o API en contenedores, es una excelente opción.

# Azure Container Instances (ACI)

Ofrece la forma más rápida y sencilla de ejecutar un único contenedor o grupos de contenedores en Azure sin tener que administrar la infraestructura (servidores o clústeres). Es perfecto para tareas sencillas, automatización o ráfagas rápidas de capacidad.


- Escalado	Manual. Para escalar a 5 instancias de contenedor, debes crear 5 instancias de ACI distintas (o automatizarlo tú mismo con otros servicios de Azure, como Logic Apps). No tiene autoescalado integrado.


Escenarios

- Carga de Trabajo	Tareas cortas, únicas o por lotes. Es perfecto para trabajos efímeros.


Run Docker containers on-demand in a managed, serverless Azure environment. Azure Container Instances is a solution for any scenario that can operate in isolated containers, without orchestration. Run event-driven applications, quickly deploy from your container development pipelines, and run data processing and build jobs.


https://learn.microsoft.com/en-us/azure/container-instances/


Quickstart: Deploy a container instance in Azure using the Azure portal
- Sign in to Azure
- Create a container instance
- View container logs
- Clean up resources

https://learn.microsoft.com/en-us/azure/container-instances/container-instances-quickstart-portal

# Azure Container Registry (ACR)

Es un registro de imágenes de Docker privado y administrado. Lo usas para construir, almacenar, proteger y replicar tus imágenes de contenedor (creadas con Docker) en la nube.