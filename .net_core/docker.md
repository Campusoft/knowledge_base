
# docker

The Microsoft Container Registry (MCR, mcr.microsoft.com) is a syndicate of Docker Hub - which hosts publicly accessible containers.
 

# Laboratorios


Docker will process each line in the Dockerfile. The . in the docker build command tells Docker to use the current folder to find a Dockerfile. This command builds the image and creates a local repository named counter-image that points to that image

```
docker build -t counter-image -f Dockerfile .
```

This command builds the image and creates a local repository named counter-image that points to that image. After this command finishes, run docker images to see a list of images installed



Otros ejemplos con nombres archivos Dockerfile diferentes
```
docker build -t base-api -f Dockerfile.Base.Api .
docker build -t base-identity-server -f Dockerfile.Base.IdentityServer .
```

Opciones de docker build
https://docs.docker.com/engine/reference/commandline/build/#options

**Errores**

 Failed to download packag nupkg Received an unexpected EOF or 0 bytes from the transport stream.

-----------------------

```
COPY failed: stat nuget.config: no such file or directory
```



---------------------------------

docker .net core Could not execute because the application was not found or a compatible .NET SDK is not installed.

Informacion posible solucion

https://stackoverflow.com/questions/66988943/could-not-execute-because-the-application-was-not-found-or-a-compatible-net-sdk

----------------------------

.net restore Received an unexpected EOF or 0 bytes from the transport stream.

--------------------

# 

--entrypoint

# Visual Studio

Compilación de aplicaciones en contenedores con Visual Studio
- Indica el proceso compilacion de visual studio, el rendimiento que utiliza.
- Indica como deshabilitar la optimización del rendimiento y compilar como se especifica en el archivo Dockerfile
- No se ejecutará ningún paso personalizado colocado en las fases build, publish o final del archivo Dockerfile, debe colocar las modificaciones en la primera fase
https://docs.microsoft.com/es-es/visualstudio/containers/container-build?view=vs-2019&WT.mc_id=visualstudio_containers_aka_containerfastmode


Crear las imagenes, con los Dockerfile que genera visual studio. El contexto "../../" depende donde se encuentre el archivo Dockerfile, con respecto al archivo de la solucion *.sln

```
docker build -f Dockerfile ../..
```

# Referencias

What is Docker, Why use it? | Docker and .NET Core 101 [1 of 3]
https://www.youtube.com/watch?v=vmnvOITMoIg&list=PLdo4fOcmZ0oUvXP_Pt2zOgk8dTWagGs_P


# Revisiones


 --no-cache 
 
------------
Trabajar con Visual Studio.

See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


Revisiones/Ejemplos:


Sin target, para volver a generar totalmente la imagen.
```
docker build -f "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification\host\Mre.Sb.Notification.HttpApi.Host\Dockerfile" --force-rm -t mresbnotificationhttpapihost:dev   --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Mre.Sb.Notification.HttpApi.Host" "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification"
```

```
docker build -f "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification\host\Mre.Sb.Notification.HttpApi.Host\Dockerfile" --force-rm -t mresbnotificationhttpapihost:dev --target base  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Mre.Sb.Notification.HttpApi.Host" "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification"
```

Crear un contenedor desde la imagen.

```
docker run -dt -v "C:\Users\L E N O V O\vsdbg\vs2017u5:/remote_debugger:rw" -v "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification\host\Mre.Sb.Notification.HttpApi.Host:/app" -v "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification:/src/" -v "C:\Users\L E N O V O\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\L E N O V O\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -v "C:\Users\L E N O V O\.nuget\packages\:/root/.nuget/fallbackpackages3" -v "C:\Program Files (x86)\Microsoft\Xamarin\NuGet\:/root/.nuget/fallbackpackages" -v "C:\Program Files\dotnet\sdk\NuGetFallbackFolder:/root/.nuget/fallbackpackages2" -e "DOTNET_USE_POLLING_FILE_WATCHER=1" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -e "NUGET_PACKAGES=/root/.nuget/fallbackpackages3" -e "NUGET_FALLBACK_PACKAGES=/root/.nuget/fallbackpackages;/root/.nuget/fallbackpackages2;/root/.nuget/fallbackpackages3" -P --name Mre.Sb.Notification.HttpApi.Host --entrypoint tail mresbnotificationhttpapihost:dev -f /dev/null
```

Si esta activa el contenedor
```
docker exec -i b9015b493e4185a6a904f793ca16e8c1ed4b9b6159166e200ec714c9e461a33e /bin/sh -c "if PID=$(pidof dotnet); then kill $PID; fi"
```

----------------
Si en visual studio, se encuentra en release. Se crea otra imagen. "mresbnotificationhttpapihost", si se encuentra en debug la imagen posee un sufijo "mresbnotificationhttpapihost:dev"
```
docker build -f "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification\host\Mre.Sb.Notification.HttpApi.Host\Dockerfile" --force-rm -t mresbnotificationhttpapihost  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Mre.Sb.Notification.HttpApi.Host" "D:\PROYECTOS\BIT_CANCILLERIA\app\Microservice\Notification"
```

---------------
Production


```
docker run --rm -it -p 8443:443/tcp -p 8080:80/tcp 0994595d5fda
```

```
docker run -p 8443:443 -e "ASPNETCORE_URLS=https://+:443;http://+:80" --name Mre.Sb.Notification.HttpApi.Host  mresbnotificationhttpapihost 
```

```
docker run -p 8443:443 -v "C:\Users\L E N O V O\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\L E N O V O\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -e "ASPNETCORE_ENVIRONMENT=Production" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -P --name Mre.Sb.Notification.HttpApi.Host  mresbnotificationhttpapihost 
```


Commando, con las opciones "--entrypoint tail -f /dev/null". Sin esta opcion la imagen trata iniciar pero se detiene.
- El punto de entrada es tail -f /dev/null, que es una espera infinita para mantener el contenedor en ejecución. Ref: https://docs.microsoft.com/es-es/visualstudio/containers/container-build?view=vs-2019&WT.mc_id=visualstudio_containers_aka_containerfastmode

```
docker run  -v "C:\Users\L E N O V O\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\L E N O V O\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -e "ASPNETCORE_ENVIRONMENT=Production" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -P --name Mre.Sb.Notification.HttpApi.Host --entrypoint tail mresbnotificationhttpapihost -f /dev/null
```


```
dotnet Mre.Sb.Notification.HttpApi.Host.dll -v
```

```
docker run --rm -it -dt -v "C:\Users\L E N O V O\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\L E N O V O\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -e "ASPNETCORE_ENVIRONMENT=Production" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -P --name Mre.Sb.Notification.HttpApi.Host --entrypoint tail mresbnotificationhttpapihost -f /dev/null
```

```
docker run -dt -v "C:\Users\L E N O V O\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\L E N O V O\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -e "ASPNETCORE_ENVIRONMENT=Production" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -P --name Mre.Sb.Notification.HttpApi.Host --entrypoint tail mresbnotificationhttpapihost -f /dev/null
```

```
docker run -dt -v "C:\Users\L E N O V O\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\L E N O V O\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -e "ASPNETCORE_ENVIRONMENT=Production" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -P --name Mre.Sb.Notification.HttpApi.Host --entrypoint tail mresbnotificationhttpapihost:dev -f /dev/null
```


```
docker run -dt -v "C:\Users\L E N O V O\vsdbg\vs2017u5:/remote_debugger:rw" -v "C:\Users\L E N O V O\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\L E N O V O\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -e "ASPNETCORE_ENVIRONMENT=Production" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -P --name Mre.Sb.Notification.HttpApi.Host --entrypoint tail mresbnotificationhttpapihost -f /dev/null



# Docker compose

Sobreescribir configuraciones de appsettings.json, con variables de entorno en Docker compose

Si se necesita sobrescribir la clave "ConnectionStrings:Default", que se encuentra en el appsettings:

```
{
  "ConnectionStrings": {
    "Default": "from config"
  },
  "MyConfig": {
    "Key": "from config"
  }
}

``` 

override it in environment variables you need to provide it with specific name: 
ConnectionStrings__Default=<new_connection>. 

It's pretty simple, you need to flatten your JSON configuration and use __ (double underscore) as a separator



# Imagenes


image: mcr.microsoft.com/mssql/server

Se debe colocar

```
version: '3'
services:
 
  sqlserver:
    image: mcr.microsoft.com/mssql/server
    environment:
      - SA_PASSWORD=Pass@word
      - ACCEPT_EULA=Y
```


SQL Server 2019 will run as non-root by default.

This container is running as user mssql.

To learn more visit https://go.microsoft.com/fwlink/?linkid=2099216.

The SQL Server End-User License Agreement (EULA) must be accepted before SQL

Server can start. The license terms for this product can be downloaded from

http://go.microsoft.com/fwlink/?LinkId=746388.


You can accept the EULA by specifying the --accept-eula command line option,

setting the ACCEPT_EULA environment variable, or using the mssql-conf tool.


Cliente Sql-Server

```
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pass@word"
```



Referencias:
Quickstart: Run SQL Server container images with Docker
https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash
