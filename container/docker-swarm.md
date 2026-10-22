# Docker Swarm

Docker Swarm (Swarm mode) es la función de orquestación integrada en Docker Engine. Permite agrupar varios hosts Docker en un clúster y administrar aplicaciones como servicios con un estado deseado. El manager programa tareas en los nodos disponibles y reconcilia el estado cuando hay fallos o cambios en la cantidad de réplicas.

> **Nota:** esta guía describe Swarm mode de Docker Engine, no el producto histórico Docker Swarm standalone.

## Conceptos

- **Swarm:** clúster de nodos Docker que trabajan como una unidad.
- **Manager:** mantiene el estado del clúster, programa tareas y acepta operaciones administrativas. Los managers también pueden ejecutar tareas de aplicación.
- **Worker:** ejecuta las tareas asignadas por los managers y no participa en el quorum Raft.
- **Servicio:** definición declarativa de una aplicación, incluida su imagen, réplicas, redes, secretos, recursos y política de actualización.
- **Tarea:** instancia de un servicio asignada a un nodo. Normalmente corresponde a un contenedor.
- **Stack:** grupo de servicios relacionados que se despliega desde un archivo Compose compatible mediante `docker stack deploy`.

## Características

### Estado deseado y recuperación

Se declara el número de réplicas deseado. El manager compara el estado real con esa definición y crea o reubica tareas para restaurar el número solicitado cuando un nodo o contenedor deja de estar disponible. Esto no garantiza que la aplicación conserve sesiones o datos locales: esas propiedades deben resolverse en la aplicación y su almacenamiento.

### Escalado y programación

Los servicios pueden escalarse cambiando la cantidad de réplicas. El scheduler distribuye las tareas entre los nodos que cumplen las restricciones y tienen recursos disponibles. Las restricciones y preferencias permiten influir en la ubicación de los servicios.

### Redes y publicación de puertos

Las redes overlay conectan servicios entre hosts. La red ingress ofrece routing mesh: un puerto publicado puede recibir tráfico en cualquier nodo y Swarm lo enruta a una tarea del servicio. Para evitar exponer un puerto en todos los nodos se puede publicar en modo host, considerando que el tráfico solo llega a los nodos que ejecutan la tarea.

### Actualizaciones graduales y reversión

Los servicios admiten actualizaciones graduales, con parámetros para controlar el orden, la cantidad de tareas simultáneas y la pausa ante fallos. Se puede configurar una política de rollback. La reversión de la definición del servicio no reemplaza una estrategia de migración compatible para el esquema o los datos.

### Configuración y secretos

Configs y secrets permiten distribuir configuración y datos sensibles a servicios. Los secrets se cifran en tránsito y se almacenan cifrados en el estado Raft. Debe limitarse qué servicios y operadores pueden acceder a ellos.

### Alta disponibilidad del plano de control

El estado del clúster se replica mediante Raft entre managers. Las operaciones de administración requieren quorum; los workers pueden continuar ejecutando tareas existentes si se pierde temporalmente ese quorum, pero no se podrán procesar cambios administrativos hasta recuperarlo.

## Casos de uso

Swarm puede ser adecuado cuando se necesita:

- Ejecutar servicios en varios hosts Docker con una operación sencilla y centrada en Docker CLI.
- Mantener un número declarado de réplicas y reprogramar tareas después de fallos de nodos.
- Desplegar aplicaciones compuestas por varios servicios mediante stacks.
- Distribuir tráfico entre réplicas usando redes overlay y routing mesh.
- Realizar actualizaciones graduales sin detener todas las instancias a la vez.
- Usar una plataforma de orquestación ligera en infraestructura propia o en entornos de tamaño moderado.

Conviene evaluar alternativas o una arquitectura complementaria cuando se necesitan un ecosistema amplio de operadores, políticas avanzadas de scheduling, autoscaling integrado, extensiones específicas de plataforma o servicios gestionados disponibles en Kubernetes.

## Limitaciones y consideraciones

- **El quorum limita la administración:** si la mayoría de managers queda inaccesible, no se pueden confirmar cambios en el clúster. Recuperar el quorum puede requerir restaurar managers o ejecutar un procedimiento de recuperación.
- **Más managers no siempre significan más rendimiento:** cada actualización del estado debe replicarse. Se recomienda una cantidad impar de managers; 3 toleran la pérdida de 1 y 5 toleran la pérdida de 2, siempre que la mayoría permanezca conectada.
- **No sustituye la alta disponibilidad de la aplicación:** Swarm reinicia tareas, pero no convierte una aplicación con estado local en una aplicación tolerante a fallos. Bases de datos y almacenamiento requieren diseño, replicación y copias de seguridad propios.
- **El balanceo no resuelve toda la entrada de tráfico:** para TLS, reglas HTTP avanzadas, WAF, observabilidad o políticas de entrada complejas puede requerirse un proxy o balanceador dedicado.
- **Los volúmenes locales están ligados al nodo:** una tarea reprogramada en otro host puede no encontrar los mismos datos. Use almacenamiento compartido o una solución de volúmenes compatible con el clúster cuando corresponda.
- **Las redes overlay tienen requisitos de conectividad:** los nodos deben poder comunicarse por los puertos requeridos por Swarm; firewalls, MTU y segmentación de red deben configurarse correctamente.
- **Compatibilidad de archivos Compose:** `docker stack deploy` no implementa todas las opciones del formato Compose moderno. Verifique las opciones soportadas por la versión de Docker Engine del clúster.
- **La operación sigue siendo responsabilidad del equipo:** monitoreo, logs centralizados, gestión de certificados, actualizaciones de Engine, backups del estado y respuesta a incidentes no quedan resueltos automáticamente por el orquestador.

## Recomendaciones

1. **Use tres o cinco managers** en dominios de fallo separados. Mantenga un número impar y evite operar con un único manager en producción.
2. **Proteja el acceso administrativo.** Restrinja el socket/API de Docker, aplique controles de acceso a los nodos y no exponga puertos de administración a redes públicas.
3. **Use imágenes versionadas e inmutables.** Evite `latest` en producción; publique una versión o digest conocido y mantenga un registro de imágenes confiable.
4. **Defina límites y reservas de recursos** para ayudar al scheduler a distribuir las tareas y reducir la contención.
5. **Despliegue varias réplicas** para servicios que deban tolerar la pérdida de una instancia. Distribúyalas entre hosts y zonas cuando la infraestructura lo permita.
6. **Diseñe las aplicaciones para ser reemplazables.** Guarde estado fuera del contenedor, use health checks y haga que el apagado de la aplicación permita finalizar solicitudes en curso.
7. **Separe el tráfico con redes overlay** y publique únicamente los puertos necesarios. Use cifrado de overlay cuando el modelo de amenaza lo requiera.
8. **Gestione secretos como secretos.** No los incluya en imágenes, repositorios, argumentos de línea de comandos ni variables que terminen en logs.
9. **Configure límites de despliegue y rollback**, y despliegue primero en un entorno de validación. Asegure que las migraciones de base de datos toleren versiones coexistentes.
10. **Haga backup y ensaye recuperación** del estado de managers. Un backup que nunca se ha restaurado no valida un plan de recuperación.
11. **Monitoree el clúster y la aplicación:** capacidad de nodos, disponibilidad de tareas, errores de health check, uso de recursos y métricas de negocio.
12. **Valide compatibilidad y conectividad** antes de actualizar Docker Engine o cambiar redes, firewalls y rangos IP.

## Comandos básicos

Inicializar un Swarm en el nodo manager:

```bash
docker swarm init --advertise-addr <IP_DEL_MANAGER>
```

Consultar el comando de unión para workers o managers:

```bash
docker swarm join-token worker
docker swarm join-token manager
```

Listar nodos (en un manager):

```bash
docker node ls
```

Desplegar un stack desde un archivo Compose compatible:

```bash
docker stack deploy -c compose.yaml mi_aplicacion
```

Consultar servicios y tareas:

```bash
docker stack services mi_aplicacion
docker service ps mi_aplicacion_web
```

Escalar un servicio:

```bash
docker service scale mi_aplicacion_web=3
```

> Los comandos administrativos del clúster deben ejecutarse desde un manager. Revisa primero la sintaxis y opciones disponibles en la versión instalada con `docker swarm --help`, `docker service --help` y `docker stack --help`.

## Referencias

- [Docker Swarm mode overview](https://docs.docker.com/engine/swarm/)
- [Administer and maintain a swarm](https://docs.docker.com/engine/swarm/admin_guide/)
- [Manage nodes](https://docs.docker.com/engine/swarm/manage-nodes/)
- [Deploy services to a swarm](https://docs.docker.com/engine/swarm/services/)
- [Manage swarm service networks](https://docs.docker.com/engine/swarm/networking/)
- [Deploy a stack to a swarm](https://docs.docker.com/engine/swarm/stack-deploy/)
- [Manage sensitive data with Docker secrets](https://docs.docker.com/engine/swarm/secrets/)

