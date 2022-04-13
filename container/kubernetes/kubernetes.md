# kubernetes


Es un orquestador de contenedores.

-	Web: https://kubernetes.io/es/
-	Documentación: https://kubernetes.io/es/docs/home/
-	https://kubernetes.io/es/docs/concepts/overview/what-is-kubernetes/

Kubernetes tiene varias características. Puedes pensar en Kubernetes como:
	-	una plataforma de contenedores
	-	una plataforma de microservicios
	-	una plataforma portable de nube
-	Es declarativo a través de manifiestos
-	Tiene la capacidad de conectarse a un api cloud
	-	Crear, destruir instancias
	-	Solicitar un disco virtual
-	Cluster Networking
	-	https://kubernetes.io/docs/concepts/cluster-administration/networking/

# Componentes

***Control plane***

Los componentes que forman el plano de control toman decisiones globales sobre el clúster (por ejemplo, la planificación) y detectan y responden a eventos del clúster, como la creación de un nuevo pod cuando la propiedad replicas de un controlador de replicación no se cumple.

- Api. "kube-apiserver" El servidor de la API es el componente del plano de control de Kubernetes que expone la API de Kubernetes. Se trata del frontend de Kubernetes, recibe las peticiones y actualiza acordemente el estado en etcd.

- Etcd. Almacén de datos persistente, consistente y distribuido de clave-valor utilizado para almacenar toda a la información del clúster de Kubernetes.

- Scheduler. Componente del plano de control que está pendiente de los Pods que no tienen ningún nodo asignado y seleciona uno donde ejecutarlo.

- Controller manager. Componente del plano de control que ejecuta los controladores de Kubernetes. Lógicamente cada controlador es un proceso independiente, pero para reducir la complejidad, todos se compilan en un único binario y se ejecuta en un mismo proceso.


- Cloud controller manager ejecuta controladores que interactúan con proveedores de la nube. El binario cloud-controller-manager es una característica alpha que se introdujo en la versión 1.6 de Kubernetes.


***Node***

Componentes de nodo

Los componentes de nodo corren en cada nodo, manteniendo a los pods en funcionamiento y proporcionando el entorno de ejecución de Kubernetes.

- Kubelet. 

Agente que se ejecuta en cada nodo de un clúster. Se asegura de que los contenedores estén corriendo en un pod.

- K-proxy permite abstraer un servicio en Kubernetes manteniendo las reglas de red en el anfitrión y haciendo reenvío de conexiones

- Runtime de contenedores

El runtime de los contenedores es el software responsable de ejecutar los contenedores. Kubernetes soporta varios de ellos: Docker, containerd, cri-o, rktlet y cualquier implementación de la interfaz de runtime de contenedores de Kubernetes, o Kubernetes CRI

# Objetos de Kubernetes

Cada objeto de Kubernetes incluye dos campos como objetos anidados que determinan la configuración del objeto: el campo de objeto spec y el campo de objeto status. El campo spec, que es obligatorio, describe el estado deseado del objeto -- las características que quieres que tenga el objeto. El campo status describe el estado actual del objeto, y se suministra y actualiza directamente por el sistema de Kubernetes. En cualquier momento, el Plano de Control de Kubernetes gestiona de forma activa el estado actual del objeto para que coincida con el estado deseado requerido.

# Servicios

-	Cluster IP
	-	IP fija dentro del cluster. Load balancer entre los pods del servicio
-	Node Port
	-	Crea un puerto en cada nodo que recibe el tráfico y lo dirige al pod determinado a través de kube-proxy
-	Load Balancer
	-	Servicio del proveedor de cloud. Redirecciona el tráfico a los pods a través de un balanceador creado por kubernetes en el proveedor de cloud

# Features

Ingress
Permite crear accesos a los servicios a través de un path
	-	https://kubernetes.io/docs/concepts/services-networking/ingress/

Se requiere tener instalado un controlador en el proveedor. Ej. nginx. https://www.hostinger.mx/tutoriales/que-es-nginx

Al instalar nginx se crea un loadBalancer y clusterIP

# ConfigMaps
-	https://kubernetes.io/es/docs/concepts/configuration/configmap/	
-	Un configmap es un objeto de la API utilizado para almacenar datos no confidenciales en el formato clave-valor. Los Pods pueden utilizar los ConfigMaps como variables de entorno, argumentos de la linea de comandos o como ficheros de configuración en un Volumen.

# Secret
-	https://kubernetes.io/es/docs/concepts/configuration/secret/
-	Los objetos de tipo Secret en Kubernetes te permiten almacenar y administrar información confidencial, como contraseñas, tokens OAuth y llaves ssh. Poniendo esta información en un Secret es más seguro y más flexible que ponerlo en la definición de un Pod o en un container image.
-	No están fuertemente cifrados solo codificados en base64.

# Kustomization
-	https://kubernetes.io/docs/tasks/manage-kubernetes-objects/kustomization/
-	Permite con un cliente generar manifiestos

# Providers

Aunque los archivos yaml son estándar, hay que revisar la documentación de cada proveedor por temas de nombres de objetos o clases. Ej. Digital Ocean storageClassName: do-block-storage , AKS storageClassName: default

## Digital Ocean
- 	https://www.digitalocean.com/
-	Proveedor de cluster en la nube. Gratis $100 de prueba pero pide tarjeta de crédito.
	-	Crear nuevo kubernates
	-	Descargar config file
		-	Contiene información del cluster creado para poder conectarse
	- 	Para cada comando kubectl se requiere utilizar el archivo de configuración. Para evitar eso se puede crear un alias a través de un archivo .cmd
		-	Crear archivo .cmd
		-	Agregar los comandos:
			-	@echo off
			-	doskey [alias]=kubectl --kubeconfig=[ruta de directorio .kube]\[clustername]-kubeconfig.yaml $*
		-	Agregar alias en el registro de windows
			-	[HKEY_LOCAL_MACHINE\Software\Microsoft\Command Processor]
			-	Llave tipo cadena con el nombre AutoRun y como valor la ruta completa al archivo .cmd
		-	Volver a abrir la terminal
		-	Utilizar directamente [alias] [comando]. Ej. kctl get nodes
## AKS
-	Gratis $200 de prueba pero pide tarjeta de crédito.
-	https://azure.microsoft.com/es-mx/
-	Crear cluster
	-	https://k21academy.com/microsoft-azure/az-104/create-aks-cluster-step-by-step-procedure/
-	Azure CLI
	-	https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli
	-	https://docs.microsoft.com/es-es/cli/azure/
-	Login
	-	az login
-	Set cluster subscription
	-	az account set --subscription [subscription id]
-	Set credentials
	-	az aks get-credentials --resource-group [resource group] --name [cluster name]		

# Manifiestos

Archivos que permiten crear componentes a través de un archivo yaml. kubectl convierte esa información a JSON cuando realiza la llamada a la API.

Campos requeridos

En el archivo .yaml del objeto de Kubernetes que quieras crear, obligatoriamente tendrás que indicar los valores de los siguientes campos (como mínimo):

- apiVersion - Qué versión de la API de Kubernetes estás usando para crear este objeto
- kind - Qué clase de objeto quieres crear
- metadata - Datos que permiten identificar unívocamente al objeto, incluyendo una cadena de texto para el name, UID, y opcionalmente el namespace

También deberás indicar el campo spec del objeto. El formato del campo spec es diferente según el tipo de objeto de Kubernetes, y contiene campos anidados específicos de cada objeto.


# Kubectl

Herramienta de línea de comandos para administar kubernates

- Descarga, instalación y configuración: https://kubernetes.io/es/docs/tasks/tools/install-kubectl/
	- https://kubernetes.io/docs/tasks/tools/install-kubectl-windows/
- Cliente para gestionar un cluster de kubernetes
- **Comandos**
-	[alias] representa la herramienta kubectl, en caso de conectarse con Digital Ocean, si se está utilizando AKS se utiliza directamente kubectl ya que azure cli permite establecer el cluster a través de su id. Ej. 
	-	Listar nodos
		-	Digital Ocean
			-	[alias] get nodes
		-	AKS
			-	kubectl get nodes
-	Obtener versión
	-	[alias] version --client
-	Listado de comandos
	-	[alias] --help
-	Listar nodos a partir de archivo de configuración descargado desde Digital Ocean
	-	[alias] --kubeconfig=[ruta de directorio .kube]\[clustername]-kubeconfig.yaml get nodes
-	Listar nodos (de aquí en adelante se utilizará el alias)
	-	[alias] get nodes
-	Listar contextos
	-	[alias] config get-contexts
-	Listar namespaces
	-	[alias] get ns
		```
		NAME              STATUS   AGE
		default           Active   45m
		kube-node-lease   Active   45m
		kube-public       Active   45m
		kube-system       Active   45m		
		```
-	Listar pods de un namespace
	-	Si no se especifica un namespace listará los contenidos en el namespace default
	-	NAME: nombre con un sufijo
	-	READY: cantidad de contenedores existentes y corriendo
	-	STATUS: En que estado esta
	-	RESTARTS: Número de veces que se ha reiniciado
	-	AGE: Tiempo de vida
	-	[alias] -n [namespace] get pods
		```
		NAME                               READY   STATUS    RESTARTS   AGE
		cilium-gmw58                       1/1     Running   0          46m
		cilium-operator-68958686d9-b4szp   1/1     Running   0          49m
		...
		```
	-	[alias] -n [namespace] get pods -o wide
	-	Aparte de la información anterior muestra:
		-	IP: dirección del contenedor, puede cambiar si el pod muere.
		-	NODE: Nombre del nodo
		-	NOMINATED NODE 
		-	READINESS GATES
	Examples:
	   kubectl get pods --output=wide
	
-	Eliminar pod
	-	[alias] -n [nombre del namespace] delete pod [nombre del pod]
	-	Esto causa que inmediatamente kubernates cree un nuevo nodo de reemplazo. Esto se puede revisar por el valor en AGE. Esto solo sucede cuando al crear un pod se especifica que siempre debe existir ese pod, en caso contrario, al eliminarlo, no se reiniciará por si solo.
-	Ejecutar manifiestos
	-	[alias] apply -f [archivo yaml]
-	Ejecutar comandos dentro del pod
	-	[alias] exec -it [nombre del pod] -- [comando]
-	Ver el manifiesto completo del pod, incluidos los valores por defecto que agrega kubernates
	-	[alias] get pod [nombre del pod] -o yaml
-	Eliminar deployment 
	-	[alias] delete deployment [nombre del deployment]
-	Eliminar deployment utilizando el yaml
	-	[alias] delete -f [archivo yaml]
-	Obtener pods, servicios y otros
	-	[alias] get all
-	Obtener la descripción de un servicio
	-	[alias] describe service [nombre del servicio]
	-	También se puede utilizar srv en lugar de service
	-	Entre otras cosas lista las ips (endpoints) y puertos de cada contenedor
-	Obtener la descripción de un pod
	-	[alias] describe pod [nombre del pod]
	-	Muestra los eventos de ese pod
-	Obtener un pvc
	-	[alias] get pvc [nombre del pvc]
	-	Si no se suministra un nombre listará todos
-	Eliminar pvc
	-	[alias] delete pvc [nombre del pvc]
-	Obtener la descripción de un pod
	-	[alias] describe pvc [nombre del pod]
	-	Si no se suministra un nombre listará todos
-	Obtener statefulset
	-	[alias] get statefulset (ó sts)
-	Eliminar statefulset
	-	[alias] delete sts [nombre del statefulset]
-	Obtener ingress
	-	[alias] get ingress (ó ing)
-	Generar kustomize
	-	El archivo kustomize debe llamarse kustomization.yaml
	-	kubectl kustomize [path archivo yaml]
	-	Si se ejecuta el comando en el mismo directorio en que está el archivo kustomization.yaml
	
-   Connect to terminal in a pod
    kubectl exec [pod_name] -it -- sh
    Example:
    	kubectl exec base-deploy-7d5fff4f77-m467b -it -- sh
		
		
# Kube context


# Service Discovery

In the Kubernetes environment, when you call one service from another, you don’t need to worry about the actual location of your service. Kubernetes by default uses DNS names to discover the pods. Therefore, if you want to call the bar service from the foo service, in the foo service’s code, you can just refer to http://bar:<port> as the service endpoint. Kubernetes will resolve and map the name to the actual endpoint. 

Kubernetes internally uses etcd as its distributed key-value store.

# Local kubernetes development

Local kubernetes development
https://kapernikov.com/local-kubernetes-development/


**minikube**

Errores con virtualbox

Exiting due to RSRC_DOCKER_MEMORY: Docker Desktop tiene solo 985MiB disponible
s, menos que los 1800MiB requeridos por Kubernetes


**K3s**

- Lightweight Kubernetes
- The certified Kubernetes distribution built for IoT & Edge computing

https://k3d.io/



**Skaffold**

Local Kubernetes Development. 

Skaffold handles the workflow for building, pushing and deploying your application, allowing you to focus on what matters most: writing code. 

https://skaffold.dev/
 



# KEDA - Kubernetes-based Event Driven Autoscaler

KEDA is a Kubernetes-based Event Driven Autoscaler. With KEDA, you can drive the scaling of any container in Kubernetes based on the number of events needing to be processed.

KEDA is a single-purpose and lightweight component that can be added into any Kubernetes cluster. KEDA works alongside standard Kubernetes components like the Horizontal Pod Autoscaler and can extend functionality without overwriting or duplication. With KEDA you can explicitly map the apps you want to use event-driven scale, with other apps continuing to function. This makes KEDA a flexible and safe option to run alongside any number of any other Kubernetes applications or frameworks.

# helm

The package manager for Kubernetes
Helm is the best way to find, share, and use software built for Kubernetes.

If Kubernetes is the operating  system of the internet,  then Helm is a package manager for that OS.

• Simple install and upgrade
• Tracks versions and changes
• Easy rollbacks
• Distribute dependencies
• Templating with dynamic  values


https://helm.sh/

 
# Referencias

etcd is the backend for service discovery and stores cluster state and configuration

- A distributed, reliable key-value store for the most critical data of a distributed system
- https://etcd.io/

- Site Reliability Engineering: https://sre.google/sre-book/table-of-contents/
	- La ingeniería de confiabilidad del sitio (SRE) es un enfoque de ingeniería de software para las operaciones de TI. Los equipos de SRE utilizan el software para gestionar los sistemas, resolver los problemas y automatizar las tareas operativas.
	

# Glosario

https://kubernetes.io/docs/reference/glossary/?fundamental=true

**Pods:** Es un set de contenedores. Puede tener uno o más contenedores. https://kubernetes.io/es/docs/concepts/workloads/pods/pod/

**Namespaces:** División lógica del cluster. Permite separar la carga. https://kubernetes.io/es/docs/concepts/overview/working-with-objects/namespaces/


**Milicores:** In Kubernetes each CPU core is allocated in units of one "milicore" meaning one Virtual Core (on a virtual machine) can be divided into 1000 shares of 1 milicore. Allocating 1000 milicores will give a pod one full CPU. Giving more will require the code in the pod to able to utilize more than one core.

**Mebibyte (MiB)**: is a unit of measurement used in computer data storage. The prefix mebi comes from the binary system of data measurement that is based on powers of two. A mebibyte equals 220 or 1,048,576 bytes.

**Deployment:** Template para crear pods (.yaml). https://kubernetes.io/es/docs/concepts/workloads/controllers/deployment/

**DaemonSet:** Un DaemonSet garantiza que todos (o algunos) de los nodos ejecuten una copia de un Pod. Conforme se añade más nodos al clúster, nuevos Pods son añadidos a los mismos. Conforme se elimina nodos del clúster, dichos Pods se destruyen. Al eliminar un DaemonSet se limpian todos los Pods que han sido creados. https://kubernetes.io/es/docs/concepts/workloads/controllers/daemonset/

**PVC:** Persistence volume claim

**StatefulSets:** Un StatefulSet es el objeto de la API workload que se usa para gestionar aplicaciones con estado. https://kubernetes.io/es/docs/concepts/workloads/controllers/statefulset/


**Service:** An abstract way to expose an application running on a set of Pods as a network service. With Kubernetes you don't need to modify your application to use an unfamiliar service discovery mechanism. Kubernetes gives Pods their own IP addresses and a single DNS name for a set of Pods, and can load-balance across them. https://kubernetes.io/docs/concepts/services-networking/service/

**Volumes:** On-disk files in a container are ephemeral, which presents some problems for non-trivial applications when running in containers. One problem is the loss of files when a container crashes. The kubelet restarts the container but with a clean state. A second problem occurs when sharing files between containers running together in a Pod. The Kubernetes volume abstraction solves both of these problems. Familiarity with Pods is suggested. https://kubernetes.io/docs/concepts/storage/volumes/

**ReplicaSet:** El objeto de un ReplicaSet es el de mantener un conjunto estable de réplicas de Pods ejecutándose en todo momento. Así, se usa en numerosas ocasiones para garantizar la disponibilidad de un número específico de Pods idénticos. https://kubernetes.io/es/docs/concepts/workloads/controllers/replicaset/

**Jobs:** Un Job crea uno o más Pods y se asegura de que un número específico de ellos termina de forma satisfactoria. Conforme los pods terminan satisfactoriamente, el Job realiza el seguimiento de las ejecuciones satisfactorias. Cuando se alcanza un número específico de ejecuciones satisfactorias, la tarea (esto es, el Job) se completa. Al eliminar un Job se eliminan los Pods que haya creado. https://kubernetes.io/es/docs/concepts/workloads/controllers/jobs-run-to-completion/

**Cluster:** A set of worker machines, called nodes, that run containerized applications. Every cluster has at least one worker node. The worker node(s) host the Pods that are the components of the application workload. The control plane manages the worker nodes and the Pods in the cluster. In production environments, the control plane usually runs across multiple computers and a cluster usually runs multiple nodes, providing fault-tolerance and high availability.

**Kubelet:** An agent that runs on each node in the cluster. It makes sure that containers are running in a Pod. The kubelet takes a set of PodSpecs that are provided through various mechanisms and ensures that the containers described in those PodSpecs are running and healthy. The kubelet doesn't manage containers which were not created by Kubernetes.


# Revisiones

Manage TLS Certificates in a Cluster
https://kubernetes.io/docs/tasks/tls/managing-tls-in-a-cluster/


**GitOps**

GitOps allows you to store configuration and policy in Git and to apply it across your Kubernetes infrastructure using agents. The advantages over devops include consistency, avoiding configuration drift, and ensuring that security, testing, and policy checks are initiated early in the CI/CD process. You may have encountered GitOps with Flux or Argo CD, two open source projects that allow you to implement GitOps.
 
 