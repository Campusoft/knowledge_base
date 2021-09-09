# kubernetes
-	Es un orquestador de contenedores.
-	Web: https://kubernetes.io/es/
-	Documentación: https://kubernetes.io/es/docs/home/
-	https://kubernetes.io/es/docs/concepts/overview/what-is-kubernetes/
-	Kubernetes tiene varias características. Puedes pensar en Kubernetes como:
	-	una plataforma de contenedores
	-	una plataforma de microservicios
	-	una plataforma portable de nube
-	Es declarativo a través de manifiestos
-	Tiene la capacidad de conectarse a un api cloud
	-	Crear, destruir instancias
	-	Solicitar un disco virtual
-	Cluster Networking
	-	https://kubernetes.io/docs/concepts/cluster-administration/networking/
## Componentes
- Control plane
	- Scheduler
	- Cloud controller manager
		- Se conecta al api del cloud provider api
	- Controller manager
	- Api
	- Etcd
		- Especie de base de datos key-value que guarda el estado del cluster
- Node
	- Kubelet
		- Vigila los pods
	- K-proxy
		- Maneja el tráfico
## Servicios
-	Cluster IP
	-	IP fija dentro del cluster. Load balancer entre los pods del servicio
-	Node Port
	-	Crea un puerto en cada nodo que recibe el tráfico y lo dirige al pod determinado a través de kube-proxy
-	Load Balancer
	-	Servicio del proveedor de cloud. Redirecciona el tráfico a los pods a través de un balanceador creado por kubernetes en el proveedor de cloud
## Features
-	Ingress
	-	Permite crear accesos a los servicios a través de un path
	-	https://kubernetes.io/docs/concepts/services-networking/ingress/
	-	Se requiere tener instalado un controlador en el proveedor. Ej. nginx. https://www.hostinger.mx/tutoriales/que-es-nginx
	-	Al instalar nginx se crea un loadBalancer y clusterIP
## ConfigMaps
-	https://kubernetes.io/es/docs/concepts/configuration/configmap/	
-	Un configmap es un objeto de la API utilizado para almacenar datos no confidenciales en el formato clave-valor. Los Pods pueden utilizar los ConfigMaps como variables de entorno, argumentos de la linea de comandos o como ficheros de configuración en un Volumen.
## Secret
-	https://kubernetes.io/es/docs/concepts/configuration/secret/
-	Los objetos de tipo Secret en Kubernetes te permiten almacenar y administrar información confidencial, como contraseñas, tokens OAuth y llaves ssh. Poniendo esta información en un Secret es más seguro y más flexible que ponerlo en la definición de un Pod o en un container image.
-	No están fuertemente cifrados solo codificados en base64.
## Kustomization
-	https://kubernetes.io/docs/tasks/manage-kubernetes-objects/kustomization/
-	Permite con un cliente generar manifiestos
## K3s
- Lightweight Kubernetes
- The certified Kubernetes distribution built for IoT & Edge computing
## Providers
-	Aunque los archivos yaml son estándar, hay que revisar la documentación de cada proveedor por temas de nombres de objetos o clases. Ej. Digital Ocean storageClassName: do-block-storage , AKS storageClassName: default
### Digital Ocean
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
### AKS
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
## Manifiestos
-	Archivos que permiten crear componentes a través de un archivo yaml
-	Ejemplo, creación de un pod con las opciones mínimas (version del api, tipo, nombre y la definición de un contenedor)
	```
		apiVersion: v1
		kind: Pod
		metadata:
		  name: nginx
		spec:
		  containers:
		  - name: nginx
			image: nginx:alpine
	```
-	Ejemplo, creación de un pod enviando:
	-	Variables (env)
		-	Explícitas (name, value) 
		-	Con valores obtenidos desde el cluster (fieldPath: status.hostIP permite obtener el ip en el que está el contenedor)
	-	Asignación de recursos (resources)
		-	https://geekflare.com/es/kubernetes-best-practices/
		-	Mínimos (requests): 64 mebibytes y 200 milicores 
		-	Máximo (limits): 128 mebibytes y 500 milicores 
	-	Indicar a kubernetes que el pod está listo para usarse (readinessProbe)
	-	Indicar a kubernetes que el pod está vivo (livenessProbe)
		-	The periodSeconds field specifies that the kubelet should perform a liveness probe every 5 seconds. The initialDelaySeconds field tells the kubelet that it should wait 5 seconds before performing the first probe.
	-	Puerto expuesto (ports)
	```
		apiVersion: v1
		kind: Pod
		metadata:
		  name: nginx
		spec:
		  containers:
		  - name: nginx
			image: nginx:alpine
			env:
			- name: MI_VARIABLE
			  value: "pelado"
			- name: MI_OTRA_VARIABLE
			  value: "pelade"
			- name: DD_AGENT_HOST
			  valueFrom:
				fieldRef:
				  fieldPath: status.hostIP
			resources:
			  requests:
				memory: "64Mi"
				cpu: "200m"
			  limits:
				memory: "128Mi"
				cpu: "500m"
			readinessProbe:
			  httpGet:
				path: /
				port: 80
			  initialDelaySeconds: 5
			  periodSeconds: 10
			livenessProbe:
			  tcpSocket:
				port: 80
			  initialDelaySeconds: 15
			  periodSeconds: 20
			ports:
			- containerPort: 80
	```
-	Ejemplo crear deployment
	-	Manifiesto para crear pods especificando las réplicas que necesitemos
	-	En este caso, si borramos un pod y quedan menos de los que especificamos, kubernates va a crear uno nuevo
	```
		apiVersion: apps/v1
		kind: Deployment
		metadata:
		  name: nginx-deployment
		spec:
		  selector:
			matchLabels:
			  app: nginx
		  replicas: 2
		  template:
			metadata:
			  labels:
				app: nginx
			spec:
			  containers:
			  - name: nginx
				image: nginx:alpine
				env:
				- name: MI_VARIABLE
				  value: "pelado"
				- name: MI_OTRA_VARIABLE
				  value: "pelade"
				- name: DD_AGENT_HOST
				  valueFrom:
					fieldRef:
					  fieldPath: status.hostIP
				resources:
				  requests:
					memory: "64Mi"
					cpu: "200m"
				  limits:
					memory: "128Mi"
					cpu: "500m"
				readinessProbe:
				  httpGet:
					path: /
					port: 80
				  initialDelaySeconds: 5
				  periodSeconds: 10
				livenessProbe:
				  tcpSocket:
					port: 80
				  initialDelaySeconds: 15
				  periodSeconds: 20
				ports:
				- containerPort: 80
	```
-	Ejemplo crear daemonset
	-	Crea una réplica del pod en cada nodo existente
	-	Se puede utilizar para monitoreo o logs
	```
		apiVersion: apps/v1
		kind: DaemonSet
		metadata:
		  name: nginx-deployment
		spec:
		  selector:
			matchLabels:
			  app: nginx
		  template:
			metadata:
			  labels:
				app: nginx
			spec:
			  containers:
			  - name: nginx
				image: nginx:alpine
				env:
				- name: MI_VARIABLE
				  value: "pelado"
				- name: MI_OTRA_VARIABLE
				  value: "pelade"
				- name: DD_AGENT_HOST
				  valueFrom:
					fieldRef:
					  fieldPath: status.hostIP
				resources:
				  requests:
					memory: "64Mi"
					cpu: "200m"
				  limits:
					memory: "128Mi"
					cpu: "500m"
				readinessProbe:
				  httpGet:
					path: /
					port: 80
				  initialDelaySeconds: 5
				  periodSeconds: 10
				livenessProbe:
				  tcpSocket:
					port: 80
				  initialDelaySeconds: 15
				  periodSeconds: 20
				ports:
				- containerPort: 80
	```
-	Ejemplo crear statefulset
	-	https://kubernetes.io/es/docs/concepts/workloads/controllers/statefulset/
	-	Cuando necesitamos un volumen para almacenar algo. Ej. archivos, logs, db
	-	storageClassName permite que kubernetes indique al cluster que cree un volumen en lugar de tener que crearlo manualmente
	-	Este proceso demora más que la creación regular de pods
	```
		apiVersion: apps/v1
		kind: StatefulSet
		metadata:
		  name: my-csi-app-set
		spec:
		  selector:
			matchLabels:
			  app: mypod
		  serviceName: "my-frontend"
		  replicas: 1
		  template:
			metadata:
			  labels:
				app: mypod
			spec:
			  containers:
			  - name: my-frontend
				image: busybox
				args:
				- sleep
				- infinity
				volumeMounts:
				- mountPath: "/data"
				  name: csi-pvc
		  volumeClaimTemplates:
		  - metadata:
			  name: csi-pvc
			spec:
			  accessModes:
			  - ReadWriteOnce
			  resources:
				requests:
				  storage: 5Gi
			  storageClassName: do-block-storage	
	```
-	Ejemplo crear cluster IP
	-	Al hacer peticiones nos va a responder uno de los 3 pods que se van a crear
	-	https://kubernetes.io/docs/concepts/services-networking/service/
	```
		apiVersion: apps/v1
		kind: Deployment
		metadata:
		  name: hello
		spec:
		  replicas: 3
		  selector:
			matchLabels:
			  role: hello
		  template:
			metadata:
			  labels:
				role: hello
			spec:
			  containers:
			  - name: hello
				image: gcr.io/google-samples/hello-app:1.0
				ports:
				- containerPort: 8080

		---
		apiVersion: v1
		kind: Service
		metadata:
		  name: hello
		spec:
		  ports:
		  - port: 8080
			targetPort: 8080
		  selector:
			role: hello	
	```
-	Ejemplo crear nodePort
	-	Al hacer peticiones apuntando a un puerto concreto (nodePort: 30000) nos va a responder uno de los 3 pods que se van a crear
	-	https://kubernetes.io/docs/concepts/services-networking/service/
	```
		apiVersion: apps/v1
		kind: Deployment
		metadata:
		  name: hello
		spec:
		  replicas: 3
		  selector:
			matchLabels:
			  role: hello
		  template:
			metadata:
			  labels:
				role: hello
			spec:
			  containers:
			  - name: hello
				image: gcr.io/google-samples/hello-app:1.0
				ports:
				- containerPort: 8080

		---
		apiVersion: v1
		kind: Service
		metadata:
		  name: hello
		spec:
		  type: NodePort
		  ports:
		  - port: 8080
			targetPort: 8080
			nodePort: 30000
		  selector:
			role: hello
		
	```
-	Ejemplo crear LoadBalancer
	-	Kubernetes le pide al proveeder crear un load balancer por lo que la obtención del IP demora más
	-	Una sola ip y puerto y el balanceador reponde con uno u otro pod
	-	https://kubernetes.io/docs/concepts/services-networking/service/
	```
		apiVersion: apps/v1
		kind: Deployment
		metadata:
		  name: hello
		spec:
		  replicas: 3
		  selector:
			matchLabels:
			  role: hello
		  template:
			metadata:
			  labels:
				role: hello
			spec:
			  containers:
			  - name: hello
				image: gcr.io/google-samples/hello-app:1.0
				ports:
				- containerPort: 8080

		---
		apiVersion: v1
		kind: Service
		metadata:
		  name: hello
		spec:
		  type: LoadBalancer
		  ports:
		  - port: 8080
			targetPort: 8080
		  selector:
			role: hello	
	```
-	Ejemplo crear Ingress
	```
		apiVersion: networking.k8s.io/v1
		kind: Ingress
		metadata:
		  name: hello-app
		spec:
		  rules:
		  - http:
			  paths:
			  - path: /v1
				pathType: Prefix
				backend:
				  service:
					name: hello-v1
					port:
					  number: 8080
			  - path: /v2
				pathType: Prefix
				backend:
				  service:
					name: hello-v2
					port:
					  number: 8080	
	```
-	Ejemplo configMap
	-	Definición del configMap
	-	Crear un configMap llamado game-demo con propiedades:
		-	player_initial_lives
		-	ui_properties_file_name
		-	game.properties
		-	user-interface.properties
		```
			apiVersion: v1
			kind: ConfigMap
			metadata:
			  name: game-demo
			data:
			  # property-like keys; each key maps to a simple value
			  player_initial_lives: "3"
			  ui_properties_file_name: "user-interface.properties"
			  #
			  # file-like keys
			  game.properties: |
				enemy.types=aliens,monsters
				player.maximum-lives=5
			  user-interface.properties: |
				color.good=purple
				color.bad=yellow
				allow.textmode=true	
		```
	-	Uso de configMap
		-	Al crear pods se puede referenciar al configMap para que obtenga los valores de variables de entorno.
		-	Además de setear valores se pueden almacenar en un archivo (storage) dentro del cluster.
		```
			apiVersion: v1
			kind: Pod
			metadata:
			  name: nginx
			spec:
			  containers:
				- name: nginx
				  image: nginx:alpine
				  env:
					# Define the environment variable
					- name: PLAYER_INITIAL_LIVES # Nombre de la variable
					  valueFrom:
						configMapKeyRef:
						  name: game-demo           # El confimap desde donde vienen los valores
						  key: player_initial_lives # La key que vamos a usar
					- name: UI_PROPERTIES_FILE_NAME
					  valueFrom:
						configMapKeyRef:
						  name: game-demo
						  key: ui_properties_file_name
				  volumeMounts:
				  - name: config
					mountPath: "/config"
					readOnly: true
			  volumes:
				- name: config
				  configMap:
					name: game-demo # el nombre del configmap que queremos montar
					items: # Un arreglo de keys del configmap para crear como archivos
					- key: "game.properties"
					  path: "game.properties"
					- key: "user-interface.properties"
					  path: "user-interface.properties"
		```
-	Ejemplo crear secret
	-	Definición del secret
		```
			apiVersion: v1
			kind: Secret
			metadata:
			  name: db-credentials
			type: Opaque
			data:
			  username: YWRtaW4=
			  password: c3VwM3JwYXNzdzByZAo=
		```
	-	Uso del secret
		```
			apiVersion: v1
			kind: Pod
			metadata:
			  name: nginx
			spec:
			  containers:
			  - name: nginx
				image: nginx:alpine
				env:
				- name: MI_VARIABLE
				  value: "pelado"
				- name: MYSQL_USER
				  valueFrom:
					secretKeyRef:
					  name: db-credentials
					  key: username
				- name: MYSQL_PASSWORD
				  valueFrom:
					secretKeyRef:
					  name: db-credentials
					  key: password
				ports:
				- containerPort: 80
		```
-	Ejemplo de kustomization
	-	Genera un manifiesto para generar pods a partir de un yaml, asignarle un label, un secret y actualiza la imagen en el pod
	-	Al crear los secrets le agrega un sufijo para identificar a que secret debe hacer referencia
	```
		apiVersion: kustomize.config.k8s.io/v1beta1
		kind: Kustomization

		commonLabels:
		  app: ejemplo

		resources:
		- 15-pod-secret.yaml

		secretGenerator:
		- name: db-credentials
		  literals:
		  - username=admin
		  - password=secr3tpassw0rd!

		images:
		- name: nginx
		  newTag: latest	
	```
## Kubectl
-	Herramienta de línea de comandos para administar kubernates
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
## Kube context

## Referencias
- etcd is the backend for service discovery and stores cluster state and configuration
- A distributed, reliable key-value store for the most critical data of a distributed system
- https://etcd.io/
- Site Reliability Engineering: https://sre.google/sre-book/table-of-contents/
	- La ingeniería de confiabilidad del sitio (SRE) es un enfoque de ingeniería de software para las operaciones de TI. Los equipos de SRE utilizan el software para gestionar los sistemas, resolver los problemas y automatizar las tareas operativas.
	
## Glosario
- https://kubernetes.io/docs/reference/glossary/?fundamental=true
- **Pods:** Es un set de contenedores. Puede tener uno o más contenedores. https://kubernetes.io/es/docs/concepts/workloads/pods/pod/
- **Namespaces:** División lógica del cluster. Permite separar la carga. https://kubernetes.io/es/docs/concepts/overview/working-with-objects/namespaces/
- **Milicores:** In Kubernetes each CPU core is allocated in units of one "milicore" meaning one Virtual Core (on a virtual machine) can be divided into 1000 shares of 1 milicore. Allocating 1000 milicores will give a pod one full CPU. Giving more will require the code in the pod to able to utilize more than one core.
- **Mebibyte (MiB)**: is a unit of measurement used in computer data storage. The prefix mebi comes from the binary system of data measurement that is based on powers of two. A mebibyte equals 220 or 1,048,576 bytes.
- **Deployment:** Template para crear pods (.yaml). https://kubernetes.io/es/docs/concepts/workloads/controllers/deployment/
- **DaemonSet:** Un DaemonSet garantiza que todos (o algunos) de los nodos ejecuten una copia de un Pod. Conforme se añade más nodos al clúster, nuevos Pods son añadidos a los mismos. Conforme se elimina nodos del clúster, dichos Pods se destruyen. Al eliminar un DaemonSet se limpian todos los Pods que han sido creados. https://kubernetes.io/es/docs/concepts/workloads/controllers/daemonset/
- **PVC:** Persistence volume claim
- **StatefulSets:** Un StatefulSet es el objeto de la API workload que se usa para gestionar aplicaciones con estado. https://kubernetes.io/es/docs/concepts/workloads/controllers/statefulset/




- **Service:** An abstract way to expose an application running on a set of Pods as a network service. With Kubernetes you don't need to modify your application to use an unfamiliar service discovery mechanism. Kubernetes gives Pods their own IP addresses and a single DNS name for a set of Pods, and can load-balance across them. https://kubernetes.io/docs/concepts/services-networking/service/
- **Volumes:** On-disk files in a container are ephemeral, which presents some problems for non-trivial applications when running in containers. One problem is the loss of files when a container crashes. The kubelet restarts the container but with a clean state. A second problem occurs when sharing files between containers running together in a Pod. The Kubernetes volume abstraction solves both of these problems. Familiarity with Pods is suggested. https://kubernetes.io/docs/concepts/storage/volumes/
- **ReplicaSet:** El objeto de un ReplicaSet es el de mantener un conjunto estable de réplicas de Pods ejecutándose en todo momento. Así, se usa en numerosas ocasiones para garantizar la disponibilidad de un número específico de Pods idénticos. https://kubernetes.io/es/docs/concepts/workloads/controllers/replicaset/
- **Jobs:** Un Job crea uno o más Pods y se asegura de que un número específico de ellos termina de forma satisfactoria. Conforme los pods terminan satisfactoriamente, el Job realiza el seguimiento de las ejecuciones satisfactorias. Cuando se alcanza un número específico de ejecuciones satisfactorias, la tarea (esto es, el Job) se completa. Al eliminar un Job se eliminan los Pods que haya creado. https://kubernetes.io/es/docs/concepts/workloads/controllers/jobs-run-to-completion/
- **Cluster:** A set of worker machines, called nodes, that run containerized applications. Every cluster has at least one worker node. The worker node(s) host the Pods that are the components of the application workload. The control plane manages the worker nodes and the Pods in the cluster. In production environments, the control plane usually runs across multiple computers and a cluster usually runs multiple nodes, providing fault-tolerance and high availability.
- **Kubelet:** An agent that runs on each node in the cluster. It makes sure that containers are running in a Pod. The kubelet takes a set of PodSpecs that are provided through various mechanisms and ensures that the containers described in those PodSpecs are running and healthy. The kubelet doesn't manage containers which were not created by Kubernetes.








