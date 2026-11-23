# Algoritmos de balanceo de carga

Resumen de ocho algoritmos que un balanceador de carga puede usar para decidir a qué servidor envía cada solicitud. Incluye una comparación para elegir entre ellos.

## Video

Animación con los ocho algoritmos:

[Ver video: algoritmos-balanceo-de-carga.mp4](algoritmos-balanceo-de-carga/algoritmos-balanceo-de-carga.mp4)

<video src="algoritmos-balanceo-de-carga/algoritmos-balanceo-de-carga.mp4" controls muted loop width="640"></video>

## Resumen

Un balanceador reparte las solicitudes entre varios servidores para evitar sobrecargas y mejorar la disponibilidad. Los algoritmos se agrupan según la información que usan para decidir:

- **Sin estado del servidor:** Round Robin, Weighted Round Robin y Random.
- **Con estado del servidor:** Least Connections, Least Response Time y Power of Two Choices.
- **Basados en hash:** IP Hash y Consistent Hashing.

## Los ocho algoritmos

### 1. Round Robin

- Distribuye las solicitudes de forma uniforme entre los servidores, en un orden fijo.
- Es simple de implementar y funciona bien cuando los servidores tienen capacidades similares.

### 2. Weighted Round Robin

- Asigna un peso a cada servidor según su capacidad.
- Los servidores con mayor peso reciben una parte mayor de las solicitudes.

### 3. Random

- Envía cada solicitud a un servidor elegido al azar.
- Es fácil de implementar y no requiere conocer el estado de los servidores.

### 4. Least Connections

- Envía la solicitud al servidor con menos conexiones activas.
- Funciona bien cuando las conexiones tienen duraciones muy distintas.

### 5. Least Response Time

- Dirige las solicitudes hacia los servidores con menor tiempo de respuesta.
- Ayuda a reducir la latencia y a evitar servidores sobrecargados.

### 6. Power of Two Choices

- Elige dos servidores al azar y envía la solicitud al que tenga menos carga.
- Reparte bien la carga con poco costo de coordinación.

### 7. IP Hash

- Usa la dirección IP del cliente para elegir siempre el mismo servidor.
- Es útil para afinidad de sesión (session affinity), cuando las solicitudes de un mismo cliente deben llegar al mismo servidor.

### 8. Consistent Hashing

- Ubica servidores y solicitudes en un anillo de hash.
- Reduce la cantidad de solicitudes que cambian de servidor cuando se agregan o quitan servidores.
- Se usa con frecuencia en cachés distribuidas y otros sistemas distribuidos.

## Comparación

| Algoritmo | Información que necesita | Fortaleza | Limitación | Uso típico |
|---|---|---|---|---|
| Round Robin | Ninguna | Simple y predecible | Ignora capacidad y carga real | Servidores homogéneos, solicitudes de duración similar |
| Weighted Round Robin | Peso de cada servidor | Considera servidores de distinta capacidad | Los pesos son estáticos y hay que mantenerlos | Servidores heterogéneos |
| Random | Ninguna | Sin estado, trivial | Con pocas solicitudes el reparto puede ser desigual | Entornos simples o con muchos balanceadores independientes |
| Least Connections | Conexiones activas por servidor | Se adapta a conexiones largas y variables | Requiere contar conexiones | WebSockets, descargas, sesiones largas |
| Least Response Time | Latencia medida por servidor | Evita servidores lentos | Requiere medir; las mediciones pueden ser ruidosas | Servicios sensibles a la latencia |
| Power of Two Choices | Carga de dos servidores por solicitud | Buen reparto con bajo costo; evita consultar todos los servidores | Necesita una métrica de carga | Sistemas grandes con muchos servidores |
| IP Hash | IP del cliente | Afinidad de sesión sin tabla de sesiones | Muchos clientes tras una misma IP (NAT) generan desbalance; cambiar el número de servidores reasigna clientes | Aplicaciones con sesión en el servidor |
| Consistent Hashing | Clave de la solicitud y anillo de servidores | Pocas reasignaciones al cambiar servidores | Reparto desigual si no se usan nodos virtuales | Cachés y almacenes distribuidos |

## Cómo elegir

1. Empezar con **Round Robin** si los servidores son iguales y las solicitudes son parecidas.
2. Usar **Weighted Round Robin** si los servidores tienen capacidades distintas.
3. Usar **Least Connections** o **Least Response Time** si la duración o el costo de las solicitudes varía mucho.
4. Usar **Power of Two Choices** si hay muchos servidores y consultar el estado de todos resulta costoso.
5. Usar **IP Hash** si necesitas afinidad de sesión y no puedes externalizar la sesión. Si puedes, guardar la sesión fuera del servidor evita la dependencia.
6. Usar **Consistent Hashing** si el servidor debe conservar datos asociados a la clave, como en una caché, y los servidores cambian con frecuencia.

## Consideraciones

- **Salud de los servidores:** todos los algoritmos deben combinarse con comprobaciones de salud para dejar de enviar tráfico a servidores caídos.
- **Estado compartido:** los algoritmos que miden carga o conexiones dependen de datos que pueden estar desactualizados, sobre todo con varios balanceadores.
- **Afinidad frente a equilibrio:** IP Hash y Consistent Hashing priorizan que la misma clave llegue al mismo servidor, aunque eso cause un reparto menos parejo.
- **Nodos virtuales:** en Consistent Hashing, asignar varias posiciones del anillo a cada servidor mejora el reparto.

## Material de apoyo

- [Video del resumen](algoritmos-balanceo-de-carga/algoritmos-balanceo-de-carga.mp4)

## Referencias

- Resumen de ocho algoritmos y animación proporcionados por el propietario del repositorio. El video proviene de una publicación en X; la URL del archivo original es `https://video.twimg.com/tweet_video/HXyPy1vaEAAR44e.mp4`. No se identificó el autor.
- Las secciones "Comparación", "Cómo elegir" y "Consideraciones" amplían el resumen original con criterios generales y no provienen de esa publicación.
