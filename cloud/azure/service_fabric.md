# Service Fabric

Azure Service Fabric is a distributed systems platform that makes it easy to package, deploy, and manage scalable and reliable microservices and containers. Service Fabric also addresses the significant challenges in developing and managing cloud native applications.

Service Fabric – the distributed services platform that runs many Microsoft services such as Event Hubs, Event Grid, Azure SQL Database, Azure Cosmos DB, Cortana, and the core Azure resource providers (Compute, Networking, Storage).


# Concepts

A named application is a collection of named services that performs a certain function or functions. A service performs a complete and standalone function (it can start and run independently of other services) and is composed of code, configuration, and data. 


An application type is the name/version assigned to a collection of service types. This is defined in an ApplicationManifest.xml file.

A service package is a disk directory containing the service type's ServiceManifest.xml file

There are two types of services: stateless and stateful. Stateless services do not store state within the service. Stateless services have no persistent storage at all or store persistent state in an external storage service such as Azure Storage, Azure SQL Database, or Azure Cosmos DB. A stateful service stores state within the service and uses Reliable Collections or Reliable Actors programming models to manage state.

![imagen](https://user-images.githubusercontent.com/222181/134396952-d4d3bd1f-c247-4f93-91dd-61faf2cda643.png)

# Service Fabric programming model overview

- Guest executables
- Containers
- Reliable Services
- ASP.NET Core

# Prerequisites

To build and run Azure Service Fabric applications on your Windows development machine, install the Service Fabric runtime, SDK, and tools.

# Reliable Services

Reliable Services is a light-weight framework for writing services that integrate with the Service Fabric platform and benefit from the full set of platform features. 


Get started with Reliable Services
https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-reliable-services-quick-start

ASP.NET Core in Azure Service Fabric Reliable Services
https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-reliable-services-communication-aspnetcore

# Revisiones


Stateful (Con estado)
Stateless (Sin estado)



Federation Subsystem.

The Federation Subsystem is responsible for giving a consistent answer to the core question: is the specific node part of the system? In the centralized approach discussed earlier, this answer was given by the API Server based on the heartbeats received from all the nodes. In Service Fabric, the nodes are organized in a ring and heartbeats are only sent to a small subset of nodes called the neighborhood.


# Referencias

Now that Kubernetes is on Azure, what is Service Fabric for?
https://www.ben-morris.com/azure-service-fabric-kubernetes/

Service Fabric and Kubernetes: community comparison, part 1 – Distributed Systems Architecture
https://docs.microsoft.com/en-us/archive/blogs/azuredev/service-fabric-and-kubernetes-comparison-part-1-distributed-systems-architecture


We have open-sourced parts of Service Fabric (reliable services framework, reliable actors framework, ASP.NET Core integration libraries, Service Fabric Explorer, and Service Fabric CLI) on GitHub and accept community contributions to those projects.
(Sfx is currently in the process of migrating from angularjs(Sfx v1) to angular 10(Sfx v2). The Sfx folder holds the previous version and SfxWeb is the new version. )
https://github.com/microsoft/service-fabric-explorer


Microservices reference architectures for Azure

-    Microservices architecture on Azure Kubernetes Service (AKS). Ref: https://docs.microsoft.com/en-us/azure/architecture/reference-architectures/containers/aks-microservices/aks-microservices
-    Microservices architecture on Azure Service Fabric. Ref: https://docs.microsoft.com/en-us/azure/architecture/reference-architectures/microservices/service-fabric

