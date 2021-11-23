
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

**Errores**

 Failed to download packag nupkg Received an unexpected EOF or 0 bytes from the transport stream.

-----------------------

```
COPY failed: stat nuget.config: no such file or directory
```


# Referencias

What is Docker, Why use it? | Docker and .NET Core 101 [1 of 3]
https://www.youtube.com/watch?v=vmnvOITMoIg&list=PLdo4fOcmZ0oUvXP_Pt2zOgk8dTWagGs_P


