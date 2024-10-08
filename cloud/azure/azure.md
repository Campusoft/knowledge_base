# Azure


# Administracion

Azure Resource Manager (ARM) es el servicio de orquestación y la plataforma tecnológica
de Microsoft que fusiona todos los componentes descritos anteriormente

Con ARM, todo en Azure es un recurso. Algunos ejemplos de recursos son las máquinas
virtuales, las interfaces de red, las direcciones IP públicas, las cuentas de almacenamiento
y las redes virtuales


# Azure Event Grid 

Azure Event Grid allows you to easily build applications with event-based architectures

# Azure Event Hub

Event Hubs es un servicio de ingesta de datos en tiempo real totalmente administrado que es sencillo, confiable y escalable. Haga streaming de millones de eventos por segundo desde cualquier origen para crear canalizaciones de datos dinámicos y responder a los desafíos empresariales de inmediato


# Application Insights

Azure
Application Insights for monitoring and auditing the applications


# Azure DevOps


## Azure DevOps Services 

Azure DevOps. Azure DevOps Services for the SaaS version, hosted by Microsoft in Azure.

## Azure DevOps Server

Azure DevOps Server for the On-premise version, that you can install on your own servers.

Collaborative software development tools for the entire team

Previously known as Team Foundation Server (TFS), Azure DevOps Server is a set of collaborative software development tools, hosted on-premises. Azure DevOps Server integrates with your existing IDE or editor, enabling your cross-functional team to work effectively on projects of all sizes.

The on-premises offering, Azure DevOps Server, is built on a SQL Server back end. Customers usually choose the on-premises version when they need their data to stay within their network. Or, when they want access to SQL Server reporting services that integrate with Azure DevOps Server data and tools.

Azure DevOps Server 2019


# Adoption / Migration


Microsoft Cloud Adoption Framework for Azure

The Cloud Adoption Framework is a collection of documentation, implementation guidance, best practices, and tools that are proven guidance from Microsoft designed to accelerate your cloud adoption journey.

https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/


# Azure Kubernetes Service 



Azure Kubernetes Service (AKS) is a free container service that simplifies the deployment, management, and operations of Kubernetes as a fully managed Kubernetes container orchestrator service.

Paying for only the virtual machines, and associated storage and networking resources consumed makes AKS the most efficient and cost-effective container service on the market.


**Application Gateway Ingress Controller (AGIC)** 

The Application Gateway Ingress Controller (AGIC) is a Kubernetes application, which makes it possible for Azure Kubernetes Service (AKS) customers to leverage Azure's native Application Gateway L7 load-balancer to expose cloud software to the Internet. AGIC monitors the Kubernetes cluster it's hosted on and continuously updates an Application Gateway, so that selected services are exposed to the Internet.

# cloud provider cli - Azure CLI

The Azure command-line interface (Azure CLI) is a set of commands used to create and manage Azure resources. The Azure CLI is available across Azure services and is designed to get you working quickly with Azure, with an emphasis on automation. 

The Azure Command-Line Interface (CLI) is a cross-platform command-line tool to connect to Azure and execute administrative commands on Azure resources. It allows the execution of commands through a terminal using interactive command-line prompts or a script.


# Observabilidad

The first and most important rule of microservice logging is those logs should go to a single place.

Azure Monitor.


# Referencias

***Build microservices on Azure***


Besides for the services themselves, some other components appear in a typical microservices architecture:

Management/orchestration. This component is responsible for placing services on nodes, identifying failures, rebalancing services across nodes, and so forth. Typically this component is an off-the-shelf technology such as Kubernetes, rather than something custom built.

API Gateway. The API gateway is the entry point for clients. Instead of calling services directly, clients call the API gateway, which forwards the call to the appropriate services on the back end.

Advantages of using an API gateway include:

-    It decouples clients from services. Services can be versioned or refactored without needing to update all of the clients.

-    Services can use messaging protocols that are not web friendly, such as AMQP.

-    The API Gateway can perform other cross-cutting functions such as authentication, logging, SSL termination, and load balancing.

-    Out-of-the-box policies, like for throttling, caching, transformation, or validation.


# Revisiones

Azure Cognitive Service for Language ofrece ahora la posibilidad de resumir documentos y conversaciones. (build-2022)


Azure Form Recognizer
The AI-powered document extraction service that understands your forms


Azure Arc
