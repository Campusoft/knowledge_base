# Contenedores

Notas, guías y referencias sobre contenedores y las plataformas que los ejecutan y administran. El contenido es material de consulta y práctica —comandos, conceptos, ejemplos y enlaces—, no una guía lineal.

## Contenido

| Documento | Tema |
|---|---|
| [docker.md](docker.md) | Comandos y conceptos básicos de Docker: `run`, puertos, opciones y ejemplos. |
| [crear.dockerfile.md](crear.dockerfile.md) | Apuntes sobre instrucciones de Dockerfile (por ejemplo, `COPY`). |
| [docker-compose.md](docker-compose.md) | Definición de aplicaciones multicontenedor con Docker Compose. |
| [docker-swarm.md](docker-swarm.md) | Orquestación con Swarm mode: servicios, stacks, réplicas y administración del clúster. |
| [registry.md](registry.md) | Registro de imágenes: login y uso de GitHub Container Registry (GHCR). |
| [portainer.md](portainer.md) | Gestión de Docker, Swarm, Podman y Kubernetes con Portainer: arquitectura, casos de uso, límites y licencias. |
| [Istio.md](Istio.md) | Service mesh con Istio: tráfico, seguridad y políticas. |
| [varios.md](varios.md) | Enlaces y notas breves sobre Linkerd, Kuma y otros service meshes. |

### Kubernetes

La subcarpeta [kubernetes/](kubernetes/) agrupa el material específico de Kubernetes:

| Documento | Tema |
|---|---|
| [kubernetes.md](kubernetes/kubernetes.md) | Introducción, conceptos y características de Kubernetes. |
| [architecture.md](kubernetes/architecture.md) | Plano de control, nodos y arquitectura de un clúster. |
| [cloud.kubernates.md](kubernetes/cloud.kubernates.md) | Kubernetes administrado en Azure (AKS). |
| [examples.md](kubernetes/examples.md) | Ejemplos prácticos, como el patrón sidecar y recolección de logs. |
| [training.md](kubernetes/training.md) | Cursos, laboratorios y recursos de aprendizaje. |

## Organización

- **Documentos por tecnología o tema:** cada archivo de la raíz cubre una herramienta o una tarea concreta (Docker, Compose, Swarm, Registry, Portainer, Istio).
- **Subcarpeta por plataforma:** `kubernetes/` concentra los documentos de Kubernetes y se enlaza desde este índice.
- **Formato de apunte:** los documentos combinan explicación breve, comandos, ejemplos y enlaces externos. No siguen todos la misma plantilla; al ampliarlos, conviene acercarlos a un formato común.
- **Referencias cruzadas:** cuando un tema se relaciona con otro, se enlaza el documento correspondiente en lugar de duplicar contenido.
- **Idioma:** la carpeta mezcla español e inglés; conviene escribir los documentos nuevos en español y conservar los términos técnicos en su idioma original.

## Cómo agregar un documento

1. Crear el archivo en la raíz de `container/` si es un tema general, o en una subcarpeta si pertenece a una plataforma concreta.
2. Usar un nombre descriptivo en minúsculas (`portainer.md`, `docker-compose.md`); reservar mayúsculas solo para nombres ya existentes.
3. Incluir al menos: qué es, para qué sirve, conceptos o comandos principales, limitaciones y referencias.
4. Añadir el enlace en la tabla de contenido de este README y, si aplica, en el documento relacionado.
5. Verificar que los enlaces relativos resuelvan y que el archivo se guarde en UTF-8.

## Referencias

- [Docker Documentation](https://docs.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Docker Swarm mode](https://docs.docker.com/engine/swarm/)
- [Kubernetes Documentation](https://kubernetes.io/docs/home/)
- [Portainer Documentation](https://docs.portainer.io/)
- [Istio Documentation](https://istio.io/latest/docs/)
