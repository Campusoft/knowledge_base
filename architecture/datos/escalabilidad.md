# Escalabilidad de bases de datos

La **escalabilidad** de una base de datos es su capacidad para atender más datos, usuarios o solicitudes manteniendo niveles aceptables de rendimiento, disponibilidad y costo. Las técnicas se eligen según el cuello de botella y los requisitos de consistencia; distribuir datos añade complejidad operativa y no sustituye un buen modelado o uso de índices.

## Escalado vertical y horizontal

- **Escalado vertical (scale up):** aumentar CPU, memoria, almacenamiento o capacidad de I/O de un servidor. Es simple de operar, pero tiene límites físicos y puede requerir una instancia más costosa.
- **Escalado horizontal (scale out):** repartir carga o datos entre varios nodos. Puede aumentar capacidad y tolerancia a fallos, a cambio de más complejidad de coordinación, enrutamiento y operación.

## Particionamiento

El **particionamiento** divide una tabla o conjunto de datos en partes más pequeñas llamadas particiones. Las particiones pueden residir en una misma instancia; el motor puede procesar solo las relevantes, facilitar mantenimiento y permitir políticas distintas por partición. No implica por sí mismo distribuir datos entre servidores.

Formas habituales:

- **Por rango:** intervalos ordenados, por ejemplo fechas mensuales. Facilita consultas por intervalo y retención, pero puede concentrar escrituras en la partición más reciente.
- **Por lista:** grupos explícitos, por ejemplo región o estado.
- **Por hash:** una función distribuye claves entre particiones; suele repartir la carga, aunque dificulta consultas por rango.
- **Compuesto:** combina criterios, como rango por fecha y hash dentro de cada rango.

## Sharding

El **sharding** distribuye particiones horizontales de los datos entre varias instancias o servidores. Cada shard contiene una parte de las filas y atiende una porción de la carga. Es una forma de particionamiento horizontal distribuido.

La **clave de shard** determina dónde se guarda cada registro. Una buena clave reparte datos y tráfico, y coincide con los patrones de consulta. Una clave desequilibrada produce *hot spots*; cambiarla después puede requerir migrar grandes volúmenes. Las consultas que abarcan varios shards suelen necesitar coordinación y combinar resultados.

El enrutamiento puede estar en la aplicación, en un proxy o en el propio motor. El diseño debe contemplar crecimiento, rebalanceo, transacciones entre shards, índices, copias de seguridad y recuperación.

## Replicación

La **replicación** mantiene copias de los mismos datos en varios nodos. Se usa para disponibilidad, recuperación ante fallos, distribución geográfica y, en algunos diseños, para repartir lecturas. No divide el conjunto lógico de datos como el particionamiento o el sharding.

- **Primario y réplicas:** las escrituras llegan al primario y se propagan a las réplicas; las lecturas pueden servirse desde ellas si la aplicación tolera retraso.
- **Síncrona:** el cambio espera confirmación de una o más réplicas. Reduce la pérdida de escrituras confirmadas, pero puede aumentar latencia o afectar disponibilidad.
- **Asíncrona:** el primario confirma antes de que todas las réplicas reciban el cambio. Reduce latencia, pero puede haber lecturas desactualizadas o pérdida de cambios recientes ante un fallo.
- **Multi-primary:** más de un nodo acepta escrituras. Requiere resolver conflictos y definir cómo se mantiene la consistencia.

Una réplica no equivale a un backup: los borrados, errores lógicos o corrupción también pueden propagarse. Se necesitan copias de seguridad y pruebas de restauración.

## Comparación

| Técnica | Qué distribuye o copia | Uso típico | Coste principal |
|---|---|---|---|
| Particionamiento | Filas o rangos dentro de una tabla; puede estar en una instancia | Mantenimiento, retención y consultas acotadas | Elegir particiones y claves adecuadas |
| Sharding | Particiones entre varios nodos | Capacidad de almacenamiento o escritura más allá de un nodo | Enrutamiento, rebalanceo y operaciones distribuidas |
| Replicación | Copias de los mismos datos | Disponibilidad, recuperación y lecturas distribuidas | Retraso, consistencia y gestión de failover |

En resumen: **particionar divide**, **sharding distribuye las divisiones entre nodos** y **replicar copia los datos**. Se pueden combinar; por ejemplo, cada shard puede tener réplicas.

## Cómo elegir

1. Medir el cuello de botella: CPU, memoria, I/O, almacenamiento, contención o consultas costosas.
2. Optimizar primero consultas, índices, esquema y capacidad de una instancia cuando eso resuelva el problema.
3. Usar particionamiento cuando el tamaño, la retención o los patrones de consulta se beneficien de separar tablas por rangos o claves.
4. Añadir réplicas cuando se necesite disponibilidad o más capacidad de lectura, definiendo la tolerancia a lecturas retrasadas y el procedimiento de failover.
5. Adoptar sharding cuando un solo nodo ya no satisfaga requisitos de capacidad o rendimiento y exista una clave que distribuya bien los datos y las consultas.
6. Definir y probar recuperación, consistencia, monitoreo y rebalanceo antes de depender de la arquitectura distribuida.

## Ver también

- [Consistencia de datos](consistencia/README.md): replicación, coordinación y modelos de consistencia.
- [Diseño de modelos de base de datos](db/design.model.db.md)
- [Escalabilidad como atributo de calidad](../fundamentos/atributos-de-calidad.md)
