# redis 


¿Qué es Redis?

Redis, que significa Remote Dictionary Server (servidor de diccionarios remoto), es un rápido almacén de datos clave-valor en memoria de código abierto que se puede utilizar como base de datos, caché, agente de mensajes y cola. El proyecto se inició cuando Salvatore Sanfilippo, el desarrollador original de Redis, trataba de mejorar la escalabilidad de su startup italiana. Redis ofrece ahora tiempos de respuesta inferiores al milisegundo, lo que permite que se realicen millones de solicitudes por segundo para aplicaciones en tiempo real de videojuegos, tecnología publicitaria, servicios financieros, sanidad e IoT. Redis es una opción muy habitual en aplicaciones de almacenamiento en caché, administración de sesiones, videojuegos, tablas de clasificación, análisis en tiempo real, datos geoespaciales, servicios de vehículos compartidos, chat/mensajería, streaming de contenido multimedia y publicación/suscripción.


Replicación y persistencia

Redis utiliza una arquitectura con servidor principal y réplica y admite la replicación asíncrona en la que los datos se replican en numerosos servidores de réplicas. De este modo, se logra un mejor nivel de rendimiento de lectura (ya que las solicitudes se pueden repartir entre varios servidores) y menores tiempos de recuperación cuando el servidor principal sufre un corte. Por una cuestión de persistencia, Redis admite copias de seguridad puntuales (copia el conjunto de datos Redis en el disco).


# pipelining 

# Instalar

***docker***

redis:alpine

This image is based on the popular Alpine Linux project, available in the alpine official image. Alpine Linux is much smaller than most distribution base images (~5MB), and thus leads to much slimmer images in general.

# Command

Connect to redis
By default redis-cli connects to the server at the address 127.0.0.1 with port 6379.

Connect to a different host and port
```
$ redis-cli -h redis15.localnet.org -p 6390
```

List all keys using the KEYS command:

```
$ redis-cli KEYS '*'
```

Delete all keys from all Redis databases:

```
$ redis-cli FLUSHALL
```

redis-cli, the Redis command line interface
https://redis.io/topics/rediscli



How to list all databases in Redis?

```
$ redis-cli INFO keyspace
```

Select the Redis logical database having the specified zero-based numeric index. New connections always use the database 0.

```
$ redis-cli SELECT index

```

https://redis.io/commands/select/


# Client


## .NET


https://github.com/StackExchange/StackExchange.Redis/



# Redis Pub/Sub

Redis Pub/Sub es un sistema de publicación/suscripción integrado en Redis que permite que clientes publiquen mensajes en canales (topics) y que otros clientes se suscriban a esos canales para recibirlos en tiempo real. Es un mecanismo simple de mensajería push (servidor → cliente) orientado a baja latencia y comunicación en tiempo real.

Componentes básicos

- Publisher: cliente que envía (PUBLISH) un mensaje a un channel.
- Subscriber: cliente que se suscribe (SUBSCRIBE o PSUBSCRIBE) a uno o varios channels y recibe los mensajes automáticamente.
- Channel: nombre lógico del canal donde se envían mensajes. También existen pattern subscriptions (PSUBSCRIBE) para suscribirse por patrones (ej. chat.*).

Diferencias y ventajas frente a un software de cola de mensajes (RabbitMQ, Kafka, SQS, etc.)

Ventajas de Redis Pub/Sub

- Latencia muy baja: Redis es extremadamente rápido — ideal para notificaciones en tiempo real, presencia, chat, actualizaciones en UI.
- Simplicidad: API minimalista, fácil de integrar.
- Ligero y embebible: si ya usas Redis, no necesitas desplegar otro sistema.
- Push a múltiples consumidores: un mensaje se entrega simultáneamente a todos los suscriptores activos (broadcast).

Limitaciones / Desventajas (frente a colas de mensajes)

- Sin persistencia: los mensajes NO se guardan en disco (por defecto). Si no hay un subscriber conectado en el momento del publish, el mensaje se pierde.
- Sin confirmaciones (ACKs): no hay mecanismo para confirmar recepción; no hay reintentos automáticos.
- No hay consumer groups (en Pub/Sub clásico): no permite reparto de carga entre consumidores (cada suscriptor recibe copia del mensaje).
- No hay replay ni lectura histórica: no puedes re-procesar mensajes pasados.
- Escalado y entrega cross-node: en despliegues distribuidos/cluster, pub/sub tiene limitaciones y puede requerir topologías especiales o Redis Streams.
- Sin control de backpressure: si los subscribers no procesan lo suficientemente rápido, Redis seguirá enviando mensajes; no hay cola por consumidor.
- Modelo “fire-and-forget”: adecuado para eventos efímeros, no para trabajo crítico que requiere garantía de entrega.


Qué sí ofrecen los sistemas de colas (RabbitMQ/Kafka/SQS)

- Durabilidad/persistencia: mensajes almacenados en disco.
- ACKs y reintentos: garantiza entrega al menos una vez / exactamente una vez (según configuración).
- Grupos de consumidores: reparto de carga (cada mensaje entregado a uno de varios consumidores).
- Replay: Kafka y otros permiten re-leer mensajes pasados
- Orden, particionado y tolerancia a fallos: más control sobre orden y resiliencia en grandes arquitecturas.
- Inspección y monitoreo de colas: herramientas maduras para administración.


Característica | Redis Pub/Sub | Cola de mensajes (RabbitMQ/Kafka/SQS)
-- | -- | --
Persistencia | ❌ (no por defecto) | ✅
Delivery guarantees | No (fire-and-forget) | ✅ (al menos una vez / exactamente una vez)
Consumer groups / reparto | ❌ (todos reciben copia) | ✅ (un mensaje → un consumidor del grupo)
Replay/retención | ❌ | ✅ (Kafka, SQS retención, etc.)
Latencia | Muy baja | Variable (baja a media)
Complejidad | Muy simple | Más complejo, más capacidades
Casos recomendados | Notificaciones en tiempo real, chat, invalidación de cache | Procesamiento de trabajo, pipelines de datos, eventos críticos

 
 
 

# Valkey

## ¿Qué es Valkey?

Valkey es un almacén de estructuras de datos en memoria, de código abierto y alto rendimiento. Puede utilizarse como base de datos clave-valor, caché, broker de mensajes y motor de streaming.

El proyecto nació como una continuación comunitaria de Redis OSS 7.2.4 y está gobernado por la Linux Foundation. Se distribuye con licencia BSD de 3 cláusulas y mantiene compatibilidad con los protocolos, configuraciones y formatos RDB/AOF de Redis OSS hasta la versión 7.2.x.

## Características principales

- Alto rendimiento y baja latencia al trabajar principalmente en memoria.
- Estructuras de datos como strings, hashes, listas, sets, sorted sets, bitmaps, HyperLogLogs, índices geoespaciales y streams.
- Persistencia mediante snapshots RDB y archivos AOF.
- Replicación y alta disponibilidad con Valkey Sentinel.
- Particionado automático y escalado horizontal con Valkey Cluster.
- Compatibilidad con numerosos clientes y herramientas creados para Redis OSS.
- Proyecto neutral respecto de proveedores, mantenido por una comunidad abierta bajo la Linux Foundation.

## Valkey frente a Redis

Característica | Valkey | Redis
-- | -- | --
Origen | Continuación de Redis OSS 7.2.4 | Proyecto original
Gobernanza | Linux Foundation y comunidad abierta | Redis Ltd.
Licencia | BSD de 3 cláusulas | Depende de la versión y distribución de Redis
Compatibilidad | Compatible con Redis OSS hasta 7.2.x | Compatibilidad nativa dentro de su propia línea de versiones
Uso recomendado | Alternativa abierta para caché, mensajería y almacenamiento en memoria | Ecosistema y productos oficiales de Redis

Valkey suele ser una buena opción cuando se busca mantener una solución completamente abierta y compatible con aplicaciones basadas en Redis OSS 7.2 o anteriores. Antes de migrar desde Redis 7.4 o versiones posteriores se debe revisar la compatibilidad, porque sus archivos RDB/AOF no se pueden usar directamente en Valkey.

## Instalar Valkey con Docker

La imagen oficial está disponible como `valkey/valkey`. El siguiente comando inicia Valkey, publica el puerto predeterminado `6379` y conserva los datos en un volumen de Docker:

```bash
docker run -d \
  --name valkey \
  -p 6379:6379 \
  -v valkey-data:/data \
  valkey/valkey:latest \
  valkey-server --save 60 1 --loglevel warning
```

Para verificar que el servidor responde:

```bash
docker exec valkey valkey-cli PING
```

La respuesta esperada es:

```text
PONG
```

También se puede guardar y consultar un valor:

```bash
docker exec valkey valkey-cli SET saludo "Hola Valkey"
docker exec valkey valkey-cli GET saludo
```

Para detener e iniciar nuevamente el contenedor:

```bash
docker stop valkey
docker start valkey
```

En proyectos y ambientes productivos es recomendable fijar una versión concreta de la imagen en lugar de usar `latest`.

## Docker Compose

```yaml
services:
  valkey:
    image: valkey/valkey:latest
    container_name: valkey
    command: valkey-server --save 60 1 --loglevel warning
    ports:
      - "6379:6379"
    volumes:
      - valkey-data:/data
    restart: unless-stopped

volumes:
  valkey-data:
```

Guardar la configuración en `compose.yaml` y ejecutar:

```bash
docker compose up -d
docker compose exec valkey valkey-cli PING
```

No se debe publicar una instancia sin autenticación directamente en Internet. En producción se deben configurar autenticación, red privada, límites de memoria, persistencia y copias de seguridad según las necesidades de la aplicación.


# referencias

Redis for .NET Developers. Continue una serie articulos, muy interesantes. 
http://taswar.zeytinsoft.com/intro-to-redis-for-net-developers/
