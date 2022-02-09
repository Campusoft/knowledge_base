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


# Organizar



Ejemplo, creación de un pod con las opciones mínimas (version del api, tipo, nombre y la definición de un contenedor)

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

Ejemplo, creación de un pod enviando:

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