# Instalación de Ollama

Ollama permite descargar, administrar y ejecutar modelos de lenguaje de forma local. Puede instalarse directamente en el sistema operativo o ejecutarse dentro de un contenedor Docker.

## Opción 1: instalación nativa

### Windows

Abrir PowerShell y ejecutar:

```powershell
irm https://ollama.com/install.ps1 | iex
```

### macOS

Abrir una terminal y ejecutar:

```bash
curl -fsSL https://ollama.com/install.sh | sh
```

### Linux

Abrir una terminal y ejecutar:

```bash
curl -fsSL https://ollama.com/install.sh | sh
```

### Verificar la instalación

```bash
ollama -v
```

### Localizar Ollama en el disco

En PowerShell, se puede consultar la ruta exacta del ejecutable con cualquiera de estos comandos:

```powershell
(Get-Command ollama).Source
where.exe ollama
```

En Windows, la instalación predeterminada normalmente se encuentra en:

```text
%LOCALAPPDATA%\Programs\Ollama
```

Las rutas predeterminadas de los modelos son:

```text
Windows: C:\Users\<usuario>\.ollama\models
macOS:   ~/.ollama/models
Linux:   /usr/share/ollama/.ollama/models
```

Si está definida la variable de entorno `OLLAMA_MODELS`, Ollama utiliza esa ruta en lugar de la predeterminada.

### Cambiar la carpeta de los modelos en Windows

El siguiente ejemplo configura Ollama para guardar los modelos en `D:\Ollama\models`.

1. Cerrar Ollama desde el icono de la bandeja del sistema.
2. Crear la carpeta de destino:

   ```powershell
   New-Item -ItemType Directory -Path 'D:\Ollama\models' -Force
   ```

3. Crear la variable de entorno persistente para el usuario actual:

   ```powershell
   [Environment]::SetEnvironmentVariable('OLLAMA_MODELS', 'D:\Ollama\models', 'User')
   ```

4. Abrir Ollama nuevamente.
5. Comprobar el valor configurado:

   ```powershell
   [Environment]::GetEnvironmentVariable('OLLAMA_MODELS', 'User')
   ```

Los modelos descargados anteriormente no cambian de ubicación automáticamente. Si se desean conservar, hay que copiar el contenido de `C:\Users\<usuario>\.ollama\models` a `D:\Ollama\models` mientras Ollama está cerrado.

### Descargar y ejecutar un modelo

El siguiente comando descarga el modelo la primera vez y después inicia una conversación interactiva:

```bash
ollama run llama3.2
```

Para salir de la conversación, escribir:

```text
/bye
```

### Comandos básicos

```bash
# Ver los modelos instalados
ollama list

# Mostrar los modelos que se están ejecutando
ollama ps

# Eliminar un modelo local
ollama rm llama3.2
```

## Opción 2: ejecutar Ollama con Docker

Esta opción mantiene Ollama aislado en un contenedor. Requiere tener Docker instalado y en ejecución.

El volumen `ollama` conserva los modelos descargados aunque el contenedor sea reemplazado. El puerto `11434` permite acceder a la API de Ollama desde el equipo anfitrión.

### Usar solamente CPU

```bash
docker run -d \
  -v ollama:/root/.ollama \
  -p 11434:11434 \
  --name ollama \
  ollama/ollama
```

En PowerShell puede escribirse en una sola línea:

```powershell
docker run -d -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
```

Para almacenar los datos y modelos directamente en el disco `D:`, se puede utilizar una carpeta del equipo en lugar del volumen con nombre:

```powershell
New-Item -ItemType Directory -Path 'D:\Ollama\docker-data' -Force
docker run -d -v D:/Ollama/docker-data:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
```

### Usar una GPU NVIDIA

Antes de iniciar el contenedor, se necesita:

- Un controlador NVIDIA compatible.
- NVIDIA Container Toolkit configurado para Docker.
- Acceso de Docker a la GPU.

Comprobar que Docker puede acceder a la GPU:

```bash
docker run --rm --gpus all ubuntu nvidia-smi
```

Iniciar Ollama con acceso a todas las GPU NVIDIA disponibles:

```bash
docker run -d \
  --gpus=all \
  -v ollama:/root/.ollama \
  -p 11434:11434 \
  --name ollama \
  ollama/ollama
```

En PowerShell puede escribirse en una sola línea:

```powershell
docker run -d --gpus=all -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
```

### Descargar y ejecutar un modelo dentro del contenedor

```bash
docker exec -it ollama ollama run llama3.2
```

### Verificar el contenedor

```bash
docker ps
docker logs ollama
```

### Detener e iniciar nuevamente Ollama

```bash
docker stop ollama
docker start ollama
```

## Documentación oficial

- Ollama: https://github.com/ollama/ollama
- Ollama con Docker: https://github.com/ollama/ollama/blob/main/docs/docker.mdx
- Docker: https://docs.docker.com/
