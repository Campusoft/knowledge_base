# Docker



# Commandos

$ docker run [options] IMAGE [command] [args]
-i, --interactive This runs the container in interactive mode (keeps the stdin fileopen).
-t, --tty This allocates a pseudo tty flag (which is required if you want to attach to the container's terminal)
-p, --publish=[] This publishes a container's port to the host (ip:hostport:containerport)
•	Hostport. Puerto en la maquina host. (To make a port available to services outside of Docker)
•	Containerport. Puerto en el container

-p 8080:80 	Map TCP port 80 in the container to port 8080 on the Docker host
 
-P 		Publish all exposed ports to random ports


Ejemplo
9000 Puerto Contenedor, al 9000 puerto host, igual para 12201. Imagen:   graylog2/allinone

```
docker run -t -p 9000:9000 -p 12201:12201 graylog2/allinone
```


**Ejecutar commandos en un contenedor que se encuentra ejecutandose.**

```
docker exec -it [container-id] bash
```
-i, --interactive=false    Keep STDIN open even if not attached
-t, --tty=false            Allocate a pseudo-TTY

**Iniciar un contenedor existente**
``` 
Docker start [container-id]
```

Ejemplo:
Iniciar un contenedor visualizando la salida “STDIN”
docker start -i cassandra

**Detener un contenedor existente**
``` 
docker stop [OPTIONS] [container-id] [CONTAINER...]
``` 

Ejemplo:
docker stop my_container


---------------
Mysql

docker run --name some-mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=admin -d mysql 

Errores:

Status : Failure -Test failed: Access denied for user 'admin'@'172.17.0.1' (using password: YES)


----------------------

**Listar contenedores descargados**

Visualizar todas las imagenes
```
docker images
```

Primeros por fecha de creación 
```
docker images | head
```

Filtrados por nombre

```
docker images | grep [nombre]
```

**Listar contenedores corriendo**

Todos: 
```
docker ps
```

Histórico: 
```
docker ps -a
```

**Ejecutar un contenedor específico**
- Se ejecuta en background por lo que no mostrará una salida por consola
- Obtener su id: docker ps -a
- docker start [id]

**Ver logs**

- docker logs [id]
- docker logs [nombre del contenedor]
- Modo listener
  - docker logs -f [id] 

**Ejecutar un comando dentro de un contenedor**
- Interactivo Terminal: docker exec -it [id] [comando]
	- Linux, ejecutar shell: docker exec -it [id] sh
	


**Descarga de contenedores**
- Cada contenedor puede tener dependencias de otros contenedores así que docker solo baja los que no se han descargado
- Latest: docker pull [container]
- Custom version: docker pull [container]:[tag]
- Tag es la version específica a descargar

**Ejecutar**
- El comando run incluye el comando pull, si no está descargado el contenedor run también lo descargará
- Latest: docker run [container]
- Custom version: docker run [container]:[tag]
- Asignando un nombre al contenedor: docker run --name [nombre del contenedor] [container] 
- Ejecutar en background (detach): docker run -d [container]
- Es posible que el contenedor requiera variables de entorno para su ejecución.
	- Ej. postgres requiere un password para configurarse y se debe enviar a través de la opción -e
	- docker run -e POSTGRE_PASSWORD=[password] postgres
- Por defecto la red del container no es compartida y recharazá las peticiones por lo que hay que especificar el puerto
	- docker run -p 3000:3000 [name]
	


# Volumenes

- Anonymous volumes
- Named volumes
- Bind mounts

## Docker host-mounted volumes

Syntax: /host/path:/container/path

## Referencias

Setting Up Docker for Windows and WSL to Work Flawlessly
- Ensure Volume Mounts Work. WSL your volume paths in a format that matches this: /mnt/c/Users/nick/dev/myapp format. 
https://nickjanetakis.com/blog/setting-up-docker-for-windows-and-wsl-to-work-flawlessly#running-windows-10-1803


Docker Desktop WSL 2 backend
- Performance is much higher when files are bind-mounted from the Linux filesystem, rather than remoted from the Windows host. Therefore avoid docker run -v /mnt/c/users:/users (where /mnt/c is mounted from Windows).
https://docs.docker.com/desktop/windows/wsl/


Where are Docker volumes located when running WSL using Docker Desktop?
- docker run -ti -v host_dir:/app amazing-container will get mapped to /mnt/wsl/docker-desktop-data/data/docker/volumes/host_dir/_data/
https://localcoder.org/where-are-docker-volumes-located-when-running-wsl-using-docker-desktop


# Install

## Linux 

Install Docker Engine on Ubuntu
https://docs.docker.com/engine/install/ubuntu/

## Windows


Install Docker Desktop on Windows
https://docs.docker.com/docker-for-windows/install/


Docker Toolbox has been deprecated and is no longer in active development. Please use Docker Desktop instead. See Docker Desktop for Mac and Docker Desktop for Windows.
https://docs.docker.com/docker-for-windows/docker-toolbox/




Docker Desktop for Windows =>  Windows 10 Home 64-bit with WSL 2.

HowTo: Change Docker containers storage location with WSL2 on Windows 10
https://blog.codetitans.pl/post/howto-docker-over-wsl2-location/



Docker Desktop WSL 2 backend
- Best practices
https://docs.docker.com/desktop/windows/wsl/

**Docker Toolbox**

Oracle VM VirtualBox

Container is not available on localhost in Windows Docker Toolbox
If you want to access the container through your Windows host, you also need to forward port 80 of your Docker machine to that host.

I see you are using VirtualBox, which allows you to do that by adding an entry in Settings > Network > Advanced > Port Forwarding.

https://stackoverflow.com/questions/50592890/container-is-not-available-on-localhost-in-windows-docker-toolbox


# Repositorios Imagenes Docker. Registro (Registry)


Es el lugar central donde se almacenan y distribuyen las Imágenes. Es como YouTube, pero para Imágenes de Docker.

- Públicas (Docker Hub): El registro más grande y conocido es Docker Hub. Muchas empresas y proyectos publican sus imágenes oficiales aquí (como nginx, node, ubuntu).
- Privadas (Ej: Azure, AWS, Google): Son registros que tú o tu empresa gestionan para guardar imágenes que no quieres que sean públicas. Requieren un login y una contraseña para acceder



Various versions of ActiveMQ neatly packet into Docker images 
https://hub.docker.com/r/rmohr/activemq

# Orchestrators 

Kubernetes: is an open-source product originally designed by Google and now maintained by the Cloud Native Computing Foundation that provides functionality that ranges from cluster infrastructure and container scheduling to orchestrating capabilities. It lets you automate deployment, scaling, and operations of application containers across clusters of hosts. Kubernetes provides a container-centric infrastructure that groups application containers into logical units for easy management and discovery. Kubernetes is mature in Linux, less mature in Windows.

Docker Swarm: Docker Swarm lets you cluster and schedule Docker containers. By using Swarm, you can turn a pool of Docker hosts into a single, virtual Docker host. Clients can make API requests to Swarm the same way they do to hosts, meaning that Swarm makes it easy for applications to scale to multiple hosts. Docker Swarm is a product from Docker, the company. Docker v1.12 or later can run native and built-in Swarm Mode.
    
Mesosphere DC/OS: Mesosphere Enterprise DC/OS (based on Apache Mesos) is a production-ready platform for running containers and distributed applications. DC/OS works by abstracting a collection of the resources available in the cluster and making those resources available to components built on top of it. Marathon is usually used as a scheduler integrated with DC/OS. DC/OS is mature in Linux, less mature in Windows.

Azure Service Fabric: It is an orchestrator of services and creates clusters of machines. Service Fabric can deploy services as containers or as plain processes. It can even mix services in processes with services in containers within the same application and cluster. Service Fabric provides additional and optional prescriptive Service Fabric programming models like stateful services and Reliable Actors. Service Fabric is mature in Windows (years evolving in Windows), less mature in Linux. Both Linux and Windows containers are supported in Service Fabric since 2017.


## Swarm

Docker swarm is a container orchestration tool, meaning that it allows the user to manage multiple containers deployed across multiple host machines. 


# Aprendizaje

This repo contains Docker labs and tutorials authored both by Docker, and by members of the community. We welcome contributions and want to grow the repo.
https://github.com/docker/labs


# Crear un contenedor
- Ubicarse en el directorio del proyecto
- Crear el archivo Dockerfile
	- FROM [imagen base para la app]
		- Utilizar la misma versión en que se trabajó el proyecto
		- Ej. Node versión 12.22.1 con linux alpine: FROM node:12.22.1-alpine3.11
	- WORKDIR /[directorio de trabajo]
		- Ej. WORKDIR /app
	- COPY . .
		- Copiar los archivos del directorio actual en /app
	- RUN yarn install --production
	- CMD ó ENTRYPOINT
		- CMD [[comando],[source]]
			- Cuando queremos ejecutar directamente un comando al inicio. Este comando puede sustituirse por ENTRYPOINT
			- Ej. CMD ["node", "/app/src/index.js"]
		- ENTRYPOINT [[comando]]
			- Cuando queremos especificar el comando pero pasar el parámetro al momento de ejecutar el contenedor. Este comando puede sustituirse por CMD
				- Ej. ENTRYPOINT ["node"]
- Crear contenedor
	- Solo con id: docker build .
	- Con tag: docker build -t [nombre del tag] .
	

# Docker Engine SDKs

Docker provides an API for interacting with the Docker daemon (called the Docker Engine API), as well as SDKs for Go and Python. The SDKs allow you to build and scale Docker apps and solutions quickly and easily. If Go or Python don’t work for you, you can use the Docker Engine API directly.

By default, the Docker engine uses a Unix socket mounted under /var/run/docker.sock on the host OS:


Unofficial libraries
- c#: https://github.com/dotnet/Docker.DotNet

	
# Varios

Azure Container Registr y: recurso público para trabajar con imágenes de Docker y sus componentes en Azure. Esto proporciona un registro cercano a las implementaciones en Azure que le proporciona control sobre el acceso, lo que le permite usar los grupos y los permisos de Azure Active Directory

## Persistencia
- Se consigue a través de un volumen
- Es bidireccional local -] docker, docker -] local
- Se pueden realizar cambios en el código del contenedor y se ven reflejados en tiempo real, sin necesidad de reiniciar el contenedor
	- Ejemplo
		- Modificar un archivo de la app
		- Ejecutar el contenedor especificando que archivo se cambió
			- Ej. contenedor con persistencia, utilizando un puerto y el archivo upd.php modificado localmente
			- docker run -v [ruta local]:[ruta en el container] -p 3000:3000 -v [ruta del source del código modificado]:[ruta del source en el contenedor] [nombre del contenedor]
		- Modificar el archivo upd.php
		- Probar en reiniciar el contenedor
- Asignar una ruta local: docker run -v [ruta local]:[ruta en el container] [nombre del contenedor]
- En caso de tener un puerto expuesto se debe agregar
	- docker run -v [ruta local]:[ruta en el container] -p 3000:3000 [nombre del contenedor]

## Actualizar un contenedor

- docker build -t [nombre del tag del contenedor a actualizar]:[version] .

TODO: Revisar
How to properly override the ENTRYPOINT using docker run
- https://oprearocks.medium.com/how-to-properly-override-the-entrypoint-using-docker-run-2e081e5feb9d

Opciones
https://docs.docker.com/engine/reference/commandline/run/#options

## Compartir imagen
- Docker Hub
	- Permite alojar imagenes con ciertas restricciones
	- La versión gratis obliga que todos los repositorios sean públicos. https://www.docker.com/pricing
	- Crear cuenta gratis
	- Loguearse
		- Docker desktop
			- Menu contextual
			- Log In
		- CMD
			- Login: docker login
	- Asignarle un tag único basado en el usuario
		- Al manejar un repositorio público se debe evitar repetir identificadores por lo que se basa en el usuario que es único
		- docker tag [id] [user]/[nombre del contenedor]:[version]
		- Al revisar las imagenes con docker images | head notaremos que el mismo [id] tiene dos nombres
	- Subir la imagen al repositorio
		- La identificación del contenedor la hacemos con la configuración del tag anterior
		- docker push [user]/[nombre del contenedor]:[version]
## Crear red docker
- Permite comunicar varios contenedores entre si a través de la misma red
- docker network create [nombre]



# Referencias

Tutorial
https://www.youtube.com/watch?v=CV_Uf3Dq-EU

