# Docker compose

Compose is a tool for defining and running multi-container Docker applications. With Compose, you use a YAML file to configure your application’s services. Then, with a single command, you create and start all the services from your configuration


Permite agrupar configuraciones para reducir utilización de línea de comandos 
- https://docs.docker.com/compose/

Opciones del commando "Overview of docker-compose CLI"
https://docs.docker.com/compose/reference/

Comentarios con #

Crear el archivo docker-compose.yaml:
	- Version de composer
		- version: "3.7"
	- Lista de servicios a ejecutar
		- services:
		- Nombre del servicio que será tambien el alias
		- Dependiendo de la necesidad se usa ports o volumes
		- app:
			- image: jpchsubzero/app-test:v1
			- ports:
				- ```- 3000:3000```
			- volumes:
				- ```- ./todo-mysql-data:/var/lib/mysql```
			- environment:
				- [key]: [value]

Ejemplos:

Ejecutar el compose
- docker-compose up -d

Detener
- docker-compose down

Especificar el nombre del archivo *.yml, que no es por defecto "docker-compose.yml" con la opcion -f

```
docker-compose -f docker-compose.with.sql-server.yml up
```	

Construir las imagenes de los docker, de los servicios que se encuentra configuradas
```	
docker-compose up --build
```

**environment**

Compose supports declaring default environment variables in an environment file named .env placed in the folder docker-compose command is executed from (current working directory).

Compose expects each line in an env file to be in VAR=VAL format. Lines beginning with # (i.e. comments) are ignored, as are blank lines.

Note: Values present in the environment at runtime will always override those defined inside the .env file. Similarly, values passed via command-line arguments take precedence as well.



**Multiple Compose files**

By default, Compose reads two files, a docker-compose.yml and an optional docker-compose.override.yml file. By convention, the docker-compose.yml contains your base configuration. The override file, as its name implies, can contain configuration overrides for existing services or entirely new services.

https://docs.docker.com/compose/extends/#understanding-multiple-compose-files


# Networking in Compose

By default, Docker Compose creates a single network for each container defined in the compose file. All the containers defined in the compose file connect and communicate through the default network.


You can inspect it by using the Docker inspect command:
```
docker network inspect mynetwork
```

Explicacion oficial de redes
- acceder servicio, que se encuentra en la misma red.
- se utiliza los nombres de los servicios dentro de la red, para acceder a la IP
https://docs.docker.com/compose/networking/


Understanding Docker Networking 
- What Is a Docker Network?
- What Are Docker Network Drivers?
  - The Bridge Driver
  - The Host Driver
  - The None Driver
  - The Overlay Driver
- Basic Docker Networking Commands
- Connecting a Container to a Network
- Creating a Network
https://earthly.dev/blog/docker-networking/

How to link multiple docker-compose services via network
- Ejemplo como crear dos archivos docker-compose.yml, y que sus servicios se comuniquen entre ellos.
- If you want define services in multiple docker-compose.yml files, and also have network connectivity between the services, you need to configure your services to use the same network.
https://tjtelan.com/blog/how-to-link-multiple-docker-compose-via-network/

Communication between multiple docker-compose projects
 - Ejemplos comuicacion entre varios docker-compose.xml
https://medium.com/@matayoshi.mariano/communication-between-multiple-docker-compose-projects-d79a68af3348


Errores

```
warning: network default: network.external.name is deprecated in favor of network.name
```

warning :

```
networks: 
  default: 
    external: 
      name: base  
```
use the external option

```      
networks:
  default:
    name: base
    external: true   
```

https://docs.docker.com/compose/networking/#use-a-pre-existing-network


# Volumenes



El problema se origina por path relativos utilizados en los volumenes del archivo docker-compose. En Windows con WSL no es soportado

```
Note: Relative host paths MUST only be supported by Compose implementations that deploy to a local container runtime. This is because the relative path is resolved from the Compose file’s parent directory which is only applicable in the local case. Compose Implementations deploying to a non-local platform MUST reject Compose files which use relative host paths with an error. To avoid ambiguities with named volumes, relative paths SHOULD always begin with . or ..
```
https://docs.docker.com/compose/compose-file/#volumes



# Install Docker compose


Errores Linux
--------------

Docker Compose can't execute: "Command 'docker-compose' not found"

Docker Compose V2 has the command syntax docker compose (compose is a subcommand of the docker command).

Since you have installed Docker Compose V2 branch, you can't use docker-compose up -d, but should instead use the correct V2 syntax:

```
docker compose up -d
```
https://askubuntu.com/questions/1396689/docker-compose-cant-execute-command-docker-compose-not-found

------------------

Solving Docker permission denied while trying to connect to the Docker daemon socket

```
docker: Got permission denied while trying to connect to the Docker daemon socket at unix:///var/run/docker.sock: Post 
```

Solucion:
The error message tells you that your current user can’t access the docker engine. As a temporary solution, you can use sudo to run the failed command as root (e.g. sudo docker ps).

https://dhananjay4058.medium.com/solving-docker-permission-denied-while-trying-to-connect-to-the-docker-daemon-socket-2e53cccffbaa

------------------

# Referencias



Is Docker-Compose Suited For Production?
- If you’re deploying to a single machine, and want to use docker-compose to build up your containers, you’re fine.
- If you need high availability, you can choose the right architecture to handle the load balancing and redundancy by having multiple identical machines behind a load balancer.
https://vsupalov.com/docker-compose-production/


