# Podman

Podman (Pod Manager) es un motor de contenedores de código abierto para buscar, descargar, construir, ejecutar y administrar imágenes y contenedores compatibles con OCI. Su interfaz de línea de comandos se parece a Docker y también administra pods, volúmenes y redes.

## Características principales

- **Sin daemon central obligatorio:** los comandos administran los contenedores directamente. Se puede activar un servicio API para clientes remotos o integraciones; en ese caso sí hay un servicio escuchando.
- **Ejecución rootless:** un usuario normal puede ejecutar contenedores sin privilegios de administrador. Los espacios de nombres de usuario aíslan las identidades; el usuario `root` del contenedor se mapea al usuario y rangos configurados en el host.
- **CLI familiar:** muchos comandos tienen equivalentes a los de Docker. La compatibilidad facilita migraciones, aunque no garantiza que todas las opciones, extensiones y flujos de trabajo de Docker funcionen sin cambios.
- **Pods:** agrupa contenedores que comparten recursos de red y ciclo de vida, siguiendo un concepto conocido de Kubernetes.
- **Imágenes OCI y Docker:** puede descargar y ejecutar imágenes OCI, construir imágenes con `Containerfile` o `Dockerfile`, y trabajar con registros.
- **Integración con systemd:** Quadlet permite declarar contenedores, pods, redes, imágenes y volúmenes como unidades administradas por systemd.
- **Interoperabilidad con Kubernetes:** puede reproducir ciertos manifiestos Kubernetes en local y generar YAML a partir de pods; no sustituye a un clúster Kubernetes.
- **Cliente remoto y Podman Desktop:** permite administrar un host Linux remoto. En Windows y macOS, Podman ejecuta los contenedores Linux en una máquina virtual administrada con `podman machine`; Podman Desktop ofrece interfaz gráfica.

## Arquitectura

Podman no tiene un daemon monolítico que deba permanecer activo para cada contenedor. El CLI coordina componentes especializados del ecosistema de contenedores:

```text
CLI podman / cliente remoto
          |
          v
      libpod ---------------------- API REST (opcional)
       /   |   \
      v    v    v
 storage  red  conmon
 (imágenes/  (Netavark y  (supervisa el
  capas)     pasta, según  proceso del
             configuración) contenedor)
                      |
                      v
                runtime OCI
                (crun o runc)
                      |
                      v
                  kernel Linux
```

- **libpod** coordina el ciclo de vida de contenedores y pods.
- **Runtime OCI** como `crun` o `runc` crea y ejecuta los contenedores usando las interfaces del kernel Linux.
- **conmon** supervisa cada contenedor y gestiona aspectos como el proceso, los registros y la conexión de terminal.
- **Almacenamiento** conserva imágenes y capas. Podman y Buildah pueden compartir almacenamiento de imágenes, pero no comparten los contenedores.
- **Red**: el backend depende de la versión y configuración. En instalaciones actuales es común Netavark; `pasta` u otras herramientas pueden participar en la conectividad rootless.
- **Buildah** aporta funcionalidad de construcción de imágenes utilizada por `podman build`.
- **API remota**: un cliente puede conectarse a un host Linux, normalmente mediante el socket de Podman y SSH. La API debe habilitarse explícitamente cuando se necesita.

En Linux, Podman usa el kernel del host. En Windows y macOS se necesita una VM Linux porque los contenedores Linux dependen del kernel Linux. Esto añade consumo y una capa de red/almacenamiento virtualizados.

## Casos de uso

1. **Desarrollo local:** iniciar bases de datos, colas y servicios auxiliares aislados sin instalar cada dependencia directamente en el equipo.
2. **CI/CD:** construir y ejecutar imágenes en pipelines, especialmente cuando se busca un flujo rootless o se quiere evitar un daemon Docker compartido.
3. **Servicios en un host Linux:** administrar servicios individuales con Quadlet y systemd, con reinicio y arranque controlados por el sistema.
4. **Laboratorios y aprendizaje:** practicar contenedores, pods y manifiestos Kubernetes en una sola máquina.
5. **Administración remota:** usar un cliente local para controlar contenedores de un servidor Linux.
6. **Migración desde Docker:** reutilizar imágenes y muchos comandos existentes, validando scripts, redes, volúmenes, Compose y herramientas de terceros antes del cambio.

## Ejemplos básicos

```bash
# Consultar la instalación
podman info

# Descargar y ejecutar una imagen temporal
podman run --rm docker.io/library/hello-world

# Ejecutar un servicio en segundo plano y publicar un puerto
podman run -d --name web -p 8080:80 docker.io/library/nginx:alpine

# Ver contenedores y sus registros
podman ps
podman logs web

# Detener y eliminar el contenedor
podman stop web
podman rm web

# Construir una imagen desde el directorio actual
podman build -t mi-aplicacion:dev .
```

Al especificar el registro, como `docker.io/library/nginx:alpine`, se reduce la ambigüedad de búsqueda de imágenes. Para servicios persistentes en Linux, conviene evaluar Quadlet en vez de depender de una sesión de terminal.

## Licencia

El código fuente del proyecto Podman se distribuye bajo **Apache License 2.0**. Esta licencia permite usar, modificar y redistribuir el software bajo sus condiciones, entre ellas conservar los avisos y el texto de licencia aplicables. La licencia de Podman no determina la licencia de las imágenes descargadas, del contenido que se ejecuta ni de todos sus componentes y dependencias: revisa sus avisos y obligaciones por separado.

Esta sección es informativa y no sustituye una revisión legal para distribución de productos o cumplimiento empresarial.

## Recomendaciones y límites

- En Linux, empieza con ejecución **rootless** y cambia a rootful solo si una necesidad concreta lo exige. Comprueba previamente los rangos de UID/GID subordinados, cgroups, firewall y configuración de red de la distribución.
- Fija etiquetas o, en despliegues reproducibles, digests de imágenes; evita depender de `latest`. Verifica el origen y la política de actualización de las imágenes.
- Trata los volúmenes y montajes del host como parte de la frontera de seguridad. Limita rutas y permisos; no montes el socket de Podman dentro de un contenedor sin entender que puede otorgar control amplio sobre el entorno.
- No expongas la API de Podman a una red sin protección. Prefiere el acceso remoto mediante SSH y sigue la guía oficial de seguridad.
- Valida compatibilidad antes de reemplazar Docker: scripts, Compose, sockets, plugins, opciones de red, montaje de volúmenes y herramientas que esperan un daemon Docker pueden requerir ajustes.
- En Windows y macOS, incorpora la VM `podman machine` a los cálculos de memoria, disco, puertos y acceso a archivos.
- Podman es apropiado para contenedores en un host, desarrollo y algunos flujos CI. Para alta disponibilidad, programación distribuida, autoscaling y operación multi-nodo, usa una plataforma de orquestación adecuada como Kubernetes.
- Mantén Podman, el runtime y las imágenes actualizados; aplica controles de acceso y escaneo de imágenes según la criticidad del servicio.

## Fuentes

- [Documentación oficial de Podman](https://docs.podman.io/en/latest/)
- [Manual oficial de Podman](https://docs.podman.io/en/latest/markdown/podman.1.html)
- [Podman Machine para Windows y macOS](https://docs.podman.io/en/latest/markdown/podman-machine.1.html)
- [Quadlet y unidades systemd](https://docs.podman.io/en/latest/markdown/podman-systemd.unit.5.html)
- [Código fuente y licencia Apache 2.0](https://github.com/containers/podman)
- [Podman en Windows y macOS: guía de instalación](https://podman.io/docs/installation)

> La arquitectura y opciones disponibles pueden variar por versión, distribución y configuración. Consulta los manuales de la versión instalada antes de automatizar procedimientos de producción.
