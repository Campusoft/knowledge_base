# GitHub Container Registry (GHCR)

Comandos

Login

`docker login ghcr.io -u TU_USUARIO_GITHUB
`

Clave generar un token


`GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)
`


Login Succeeded


**Estructura imagenes**


`ghcr.io/<OWNER>/<IMAGE_NAME>:<TAG>
`

 
Parte | Significado
-- | --
OWNER | Usuario u organización GitHub
IMAGE_NAME | Normalmente el repo
TAG | Versión, demo, fecha, etc.



**Errores**

```
buildx failed with: ERROR: failed to build: invalid tag "ghcr.io/Campusoft/Docker-Labs:latest": repository name must be lowercase
```