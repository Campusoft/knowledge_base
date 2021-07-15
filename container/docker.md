# docker

# Windows

Docker Toolbox has been deprecated and is no longer in active development. Please use Docker Desktop instead. See Docker Desktop for Mac and Docker Desktop for Windows.
https://docs.docker.com/docker-for-windows/docker-toolbox/


Install Docker Desktop on Windows
https://docs.docker.com/docker-for-windows/install/

Docker Desktop for Windows =>  Windows 10 Home 64-bit with WSL 2.


# Repositorios Imagenes Docker

Various versions of ActiveMQ neatly packet into Docker images 
https://hub.docker.com/r/rmohr/activemq

# Commandos

$ docker run [options] IMAGE [command] [args]
-i, --interactive This runs the container in interactive mode (keeps the stdin fileopen).
-t, --tty This allocates a pseudo tty flag (which is required if you want to attach to the container's terminal)
-p, --publish=[] This publishes a container's port to the host (ip:hostport:containerport)
•	Hostport. Puerto en la maquina host. (To make a port available to services outside of Docker)
•	Containerport. Puerto en el container

-p 8080:80 	Map TCP port 80 in the container to port 8080 on the Docker host
 

Ejemplo
9000 Puerto Contenedor, al 9000 puerto host, igual para 12201. Imagen:   graylog2/allinone
docker run -t -p 9000:9000 -p 12201:12201 graylog2/allinone

## Ejemplos:

---------------
Mysql

docker run --name some-mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=admin -d mysql 

Errores:

Status : Failure -Test failed: Access denied for user 'admin'@'172.17.0.1' (using password: YES)


----------------------

# Aprendizaje

This repo contains Docker labs and tutorials authored both by Docker, and by members of the community. We welcome contributions and want to grow the repo.
https://github.com/docker/labs

# Varios

Azure Container Registr y: recurso público para trabajar con imágenes de Docker y sus componentes en
Azure. Esto proporciona un registro cercano a las implementaciones en Azure que le proporciona control sobre
el acceso, lo que le permite usar los grupos y los permisos de Azure Active Directory

