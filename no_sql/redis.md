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

List all keys using the KEYS command:

```
$ redis-cli KEYS '*'
```


redis-cli, the Redis command line interface
https://redis.io/topics/rediscli

# Client


## .NET


https://github.com/StackExchange/StackExchange.Redis/


# referencias

Redis for .NET Developers. Continue una serie articulos, muy interesantes. 
http://taswar.zeytinsoft.com/intro-to-redis-for-net-developers/