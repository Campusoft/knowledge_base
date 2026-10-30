# Portainer

**Portainer** es una plataforma de gestión de contenedores con interfaz web y API REST. Permite administrar desde una sola consola entornos **Docker**, **Docker Swarm**, **Podman** y **Kubernetes**: contenedores, imágenes, volúmenes, redes, stacks, secretos, configuración, usuarios y clústeres.

No es un orquestador ni un runtime: se apoya en el motor o clúster que ya existe y actúa como plano de gestión. Su propuesta es reducir la dependencia de comandos y archivos de configuración para las tareas operativas habituales, sin impedir el uso de la CLI cuando hace falta.

Portainer se distribuye en dos ediciones:

- **Community Edition (CE):** edición abierta y gratuita, orientada a laboratorios, aprendizaje y proyectos personales.
- **Business Edition (BE):** edición comercial con RBAC, SSO, GitOps, auditoría, gobierno de flotas y soporte.

Las capacidades concretas dependen de la edición y la versión; conviene verificar la matriz vigente antes de adoptarlo.

## Características

- **Gestión de recursos:** contenedores, imágenes, volúmenes, redes, secretos, configuraciones y stacks de Compose; en Kubernetes, namespaces, workloads, servicios, ingress, configuración, volúmenes y charts de Helm.
- **Múltiples entornos:** un servidor puede administrar varios hosts Docker, clústeres Swarm y clústeres Kubernetes, con una vista centralizada.
- **Interfaz web y API REST:** la UI cubre el trabajo diario y la API permite automatizar tareas e integrar herramientas.
- **Despliegue de aplicaciones:** stacks desde archivos Compose, plantillas de aplicaciones, repositorios Git y charts de Helm.
- **Ciclo de vida:** iniciar, detener, reiniciar, recrear, inspeccionar logs, abrir consola, consultar estadísticas y actualizar imágenes.
- **Control de acceso:** usuarios y autenticación básica en CE; equipos, RBAC granular, LDAP/Active Directory, OAuth/SSO y auditoría en BE.
- **Gestión de edge:** agentes que conectan entornos remotos mediante un túnel TLS saliente, útil para sucursales, IoT y sitios sin acceso entrante.
- **Operación:** webhooks para redespliegue, notificaciones, etiquetas, plantillas y tareas de mantenimiento según edición.
- **Instalación ligera:** un contenedor para el servidor, agentes por entorno y almacenamiento local para la configuración.

| Capacidad | Community Edition | Business Edition |
|---|---|---|
| Docker, Swarm, Podman y Kubernetes | Sí | Sí |
| UI y API REST | Sí | Sí |
| Usuarios y autenticación básica | Sí | Sí |
| RBAC granular y equipos | No | Sí |
| LDAP/AD, OAuth/SSO | No | Sí |
| GitOps y actualizaciones automáticas | Limitado | Sí |
| Auditoría y gobierno de flotas | No | Sí |
| Edge avanzado y flotas grandes | Limitado | Sí |
| Soporte | Comunidad | Comercial |

La tabla es orientativa: la edición, la versión y el plan contratado cambian el alcance. Revisar la comparación oficial antes de decidir.

## Arquitectura

Portainer se compone de dos elementos:

- **Portainer Server:** contenedor central que sirve la UI y la API, guarda la configuración, usuarios, entornos y credenciales, y coordina las operaciones. Requiere persistencia.
- **Portainer Agent:** contenedor sin estado desplegado en cada nodo o clúster que se quiere gestionar. Recibe las operaciones del servidor y devuelve la información del entorno.

El servidor puede conectarse a un entorno local mediante el socket de Docker, o a entornos remotos mediante agentes. Un solo servidor admite muchos agentes; en cambio, **no se admite ejecutar varios servidores gestionando los mismos clústeres**.

```
Navegador / API
       │ HTTPS 9443
       ▼
Portainer Server ─── volumen /data (base de datos y configuración)
       │
       ├── socket de Docker ──► Docker local
       │
       ├── agente clásico :9001 ──► nodo o clúster en la misma red
       │
       └── túnel TLS :8000 ◄── Edge Agent ──► entorno remoto
```

### Modos de conexión

| Modo | Dirección de la conexión | Cuándo usarlo |
|---|---|---|
| **Socket local** | Servidor → Docker local | Gestión del propio host donde corre Portainer. |
| **Agente clásico** | Servidor → agente (puerto 9001) | Entornos en la misma red y con acceso directo desde el servidor. |
| **Edge Agent** | Agente → servidor (túnel TLS, puerto 8000) | Entornos remotos o detrás de firewall; no requiere exponer el agente a Internet. |
| **Kubernetes** | Servidor → API del clúster o agente desplegado | Clústeres Kubernetes, con Helm o manifiestos. |

### Puertos habituales

| Puerto | Uso |
|---|---|
| `9443` | UI y API por HTTPS (puerto principal). |
| `8000` | Túnel para Edge Agents; solo si se usa esa función. |
| `9000` | HTTP heredado; evitar salvo compatibilidad. |
| `9001` | Agente clásico. |

### Persistencia y despliegue

El servidor guarda su estado en el volumen `/data` (base de datos embebida, configuración y credenciales de entornos). Los agentes no tienen estado: la información del entorno vive en el servidor. Un respaldo del volumen permite reconstruir la instalación, siempre que se conserven también los archivos de despliegue.

El despliegue típico es un contenedor con `docker run` o Compose, y en Kubernetes un chart de Helm o manifiestos. En Docker se monta el socket para gestionar el host local; en remoto se prefiere un agente para no exponer el socket.

### Seguridad

- Montar `/var/run/docker.sock` otorga al contenedor un nivel de privilegio equivalente a **root** sobre el host. En producción, limitar el acceso al servidor y preferir agentes para hosts remotos.
- Publicar únicamente `9443`; el puerto `8000` solo es necesario para Edge Agents y `9001` para agentes clásicos en red interna.
- Usar un certificado propio o corporativo en lugar del autogenerado, y renovarlo antes de que caduque.
- Restringir quién administra Portainer: comprometer la consola equivale a comprometer los entornos gestionados.
- Segmentar la red de gestión, aplicar firewall y evitar exponer la consola directamente a Internet sin protección adicional.
- En instalaciones nuevas, completar el *setup token* inicial y cambiar las credenciales por defecto.

## Casos de uso

| Escenario | Aporte de Portainer |
|---|---|
| **Laboratorio o homelab** | Interfaz rápida para desplegar contenedores, stacks y probar imágenes sin memorizar comandos. |
| **Pymes y equipos pequeños** | Administración visual de uno o varios hosts Docker o Swarm, con despliegues desde Compose. |
| **Múltiples entornos** | Vista única de hosts, clústeres y agentes; cambio de contexto sin conectarse a cada máquina. |
| **Kubernetes para equipos no especialistas** | Namespaces, workloads, configuración y Helm desde una UI guiada, sin sustituir a `kubectl` para casos avanzados. |
| **Sucursales, IoT y edge** | Edge Agents conectan sitios remotos mediante túnel saliente y permiten gestionar flotas desde un servidor central. |
| **Delegación y gobierno** | Equipos, RBAC y auditoría en BE para separar responsabilidades por entorno o aplicación. |
| **Formación y demostraciones** | Entorno reproducible y visual para enseñar contenedores, Swarm o Kubernetes. |
| **Operación diaria** | Logs, consola, estadísticas, reinicios y actualizaciones de imágenes desde un solo lugar. |

**Cuándo no es la mejor opción:** plataformas grandes que exigen GitOps estricto, infraestructura como código para cada cambio, multi-tenencia fuerte o automatización masiva. En esos casos conviene evaluar Rancher, Argo CD, Flux, `kubectl`/Helm o la CLI del runtime como mecanismo principal, y usar Portainer solo como consola de apoyo.

## Limitaciones

- **No sustituye al orquestador:** sin Docker, Swarm o Kubernetes no hay nada que gestionar; Portainer no programa cargas ni reemplaza la API del clúster.
- **Superficie de ataque:** el servidor concentra credenciales y, si monta el socket, privilegios equivalentes a root. Es un objetivo de alto valor.
- **Alta disponibilidad limitada:** no se soporta ejecutar varios servidores sobre los mismos clústeres. El servidor es un punto único de gestión y su recuperación depende del respaldo de `/data`.
- **CE acotada:** sin RBAC granular, SSO, auditoría ni gobierno de flotas. Además, CE continúa sobre la línea 2.x mientras la edición 3.x se orienta a empresa; no hay paridad de funciones.
- **Soporte:** CE se ofrece tal cual, sin SLA; la respuesta depende de la comunidad. BE añade soporte comercial, pero requiere licencia y renovación.
- **Deriva de configuración:** los cambios hechos por UI no quedan versionados por sí solos. Si el equipo exige GitOps o IaC, hay que integrar Git y evitar la administración manual como fuente de verdad.
- **Escala de gestión:** está pensado para operación asistida por personas; para flotas muy grandes o automatización intensiva, la API y las herramientas de infraestructura como código deben llevar el peso.
- **Cobertura variable:** algunas funciones de Kubernetes, objetos personalizados (CRD) o características del runtime pueden requerir `kubectl`, Helm o la CLI; la UI no cubre todos los casos avanzados.
- **Dependencia de versiones:** el soporte sigue políticas de ciclo de vida para Docker, Kubernetes y la propia Portainer. Actualizar sin revisar notas de versión puede romper integraciones.
- **Edge condicionado:** requiere conectividad saliente, certificados válidos y planificación de red; las capacidades de flota grandes y *air-gapped* avanzadas son de BE.

## Licencias

| Edición | Licencia | Costo | Soporte | Uso previsto |
|---|---|---|---|---|
| **Community Edition (CE)** | zlib (código abierto) | Gratuita, sin clave de licencia | Comunidad, sin SLA | Laboratorios, aprendizaje, proyectos personales y entornos no críticos. |
| **Business Edition (BE)** | Comercial | Suscripción por nodo; incluye 3 nodos gratuitos | Comercial | Producción, empresas y flotas con requisitos de gobierno. |
| **Home & Student** | Comercial, no comercial | Plan anual | Comunidad | Uso personal y educativo, con límite de nodos. |

Puntos a tener en cuenta:

- **CE usa licencia zlib**, no Apache ni GPL. Es gratuita y el código está disponible, pero se entrega sin garantías ni soporte.
- **BE es propietaria.** Se licencia por **nodo**, entendido como cada servidor, VM o dispositivo que ejecuta contenedores y es gestionado por Portainer; una licencia cubre Docker, Swarm y Kubernetes.
- **"Gratis" no significa "open source":** el plan de 3 nodos de BE es gratuito pero sigue siendo comercial y requiere clave y renovación.
- **Precios y planes cambian.** Los planes por volumen, el límite del plan gratuito y las condiciones de renovación deben consultarse en la página oficial antes de presupuestar.
- **Portainer 3.x:** la edición 3.x se orienta a empresa y no se publica como CE; CE permanece en 2.x y la línea 2.45 LTS recibe correcciones. Verificar el estado vigente y la ruta de actualización.
- **Componentes de terceros:** la distribución incluye código de otros proyectos con sus propias licencias; revisar `ATTRIBUTIONS` en el repositorio.

## Recomendaciones

1. Desplegar el servidor en un nodo de gestión dedicado y usar agentes en los demás nodos.
2. Preferir **Edge Agent** para entornos remotos y reservar el agente clásico para redes internas de confianza.
3. Exponer solo `9443` con TLS corporativo; restringir `8000` y `9001` a las redes que los necesiten.
4. Evitar montar el socket de Docker cuando no sea imprescindible; en hosts remotos, usar agente.
5. Versionar los stacks y manifiestos en Git aunque se desplieguen desde la UI, para poder reconstruir el entorno.
6. Respaldar periódicamente `/data` y probar la restauración; documentar la versión y la configuración del servidor.
7. Fijar la versión de la imagen y revisar las notas de versión antes de actualizar; usar la línea LTS cuando la estabilidad sea prioritaria.
8. Aplicar mínimo privilegio: cuentas nominales, RBAC y SSO en BE, y auditoría de accesos.
9. Monitorear el servidor y sus agentes: disponibilidad, uso de disco, errores de conexión y caducidad de certificados.
10. Definir desde el inicio si Portainer será la fuente de verdad o solo una consola; mezclar ambos modelos genera deriva.

## Alternativas

| Herramienta | Enfoque | Cuándo considerarla |
|---|---|---|
| **Rancher** | Gestión multi-clúster de Kubernetes | Plataformas grandes con gobierno y múltiples clústeres. |
| **Lens / OpenLens** | IDE de escritorio para Kubernetes | Uso individual, sin servidor central. |
| **Kubernetes Dashboard** | UI oficial básica del clúster | Administración simple integrada en Kubernetes. |
| **Docker Desktop** | Entorno local de desarrollo | Desarrollo en la estación de trabajo, no operación centralizada. |
| **Argo CD / Flux** | GitOps para Kubernetes | Despliegue declarativo y continuo desde Git. |
| **`kubectl`, Helm y CLI de Docker** | Automatización e infraestructura como código | Equipos que priorizan reproducibilidad y pipelines. |
| **Cockpit** | Administración ligera de servidores Linux | Hosts individuales con necesidad mínima de contenedores. |

## Referencias

- [Portainer: documentación oficial](https://docs.portainer.io/)
- [Arquitectura de Portainer](https://docs.portainer.io/start/architecture)
- [Instalación de Portainer CE con Docker](https://docs.portainer.io/start/install-ce/server/docker/linux)
- [Requisitos y prerrequisitos](https://docs.portainer.io/start/requirements-and-prerequisites)
- [Comparación de ediciones y precios](https://www.portainer.io/pricing)
- [Licencia de Portainer CE (zlib)](https://github.com/portainer/portainer/blob/develop/LICENSE)
- [Repositorio en GitHub](https://github.com/portainer/portainer)
- [Notas de versión](https://github.com/portainer/portainer/releases)

## Referencias internas

- [Contenedores](README.md)
- [Docker](docker.md)
- [Docker Compose](docker-compose.md)
- [Docker Swarm](docker-swarm.md)
- [Kubernetes](kubernetes/kubernetes.md)
- [Registry](registry.md)
- [Istio](Istio.md)
- [Infraestructura](../architecture/infraestructura/README.md)
- [Operación](../architecture/operacion/README.md)
- [Seguridad](../architecture/seguridad/README.md)
