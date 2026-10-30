# Algoritmos de consenso

Los **algoritmos de consenso** permiten que un grupo de nodos acuerde un mismo valor o una misma secuencia de operaciones aunque algunos fallen, se retrasen o la red se degrade. Son la base de la **replicación de máquinas de estado** (*state machine replication*): si todas las réplicas aplican el mismo log de comandos en el mismo orden, alcanzan el mismo estado y el sistema puede sobrevivir a la pérdida de nodos.

En arquitectura, el consenso aparece donde se necesita una decisión única y durable: elección de líder, metadata y configuración de un clúster, coordinación de servicios, colas replicadas, transacciones distribuidas o el log de una base de datos replicada. **Raft** y la familia **Paxos** son las referencias más usadas; varias plataformas actuales —Kubernetes, Kafka o CockroachDB— dependen de ellos para su plano de control.

El consenso tiene un costo: cada escritura requiere la confirmación de una mayoría y, al menos, una ida y vuelta de red. Por eso se emplea para coordinar estado pequeño y crítico, no como mecanismo general para replicar grandes volúmenes de datos.

## El problema del consenso

Un sistema distribuido reúne nodos que se comunican por red y pueden fallar de forma independiente. El modelo habitual asume fallos de tipo *crash* o de omisión: un nodo deja de responder, pero no envía mensajes falsos. Los fallos bizantinos —nodos maliciosos o con comportamiento arbitrario— requieren otra familia de algoritmos.

La red puede particionarse, retrasar o reordenar mensajes, y los relojes no son confiables. En un modelo totalmente asíncrono no es posible garantizar al mismo tiempo seguridad y terminación (resultado de imposibilidad de FLP). Los algoritmos prácticos priorizan la **seguridad** y consiguen **vivacidad** cuando la red se comporta de forma suficientemente estable, usando temporizadores y reintentos.

Propiedades que debe ofrecer un algoritmo de consenso:

| Propiedad | Significado |
|---|---|
| **Acuerdo (safety)** | Dos nodos no confirman valores distintos para la misma decisión. |
| **Validez** | Solo se decide un valor propuesto; no se inventan valores. |
| **Terminación (liveness)** | Si la mayoría está disponible y la red permite comunicarse, el sistema progresa. |
| **Orden total** | Todos los nodos aplican las mismas operaciones en el mismo orden. |
| **Durabilidad** | Una decisión confirmada sobrevive a la caída y reinicio de nodos. |

El mecanismo central es el **quórum**: una decisión se confirma cuando la respalda una mayoría. Con `N` nodos, la mayoría es `⌊N/2⌋ + 1` y el sistema tolera `⌊(N−1)/2⌋` fallos. Dos mayorías siempre se solapan al menos en un nodo, y ese nodo impide que se confirmen dos decisiones contradictorias.

No todo problema de replicación requiere consenso. Los sistemas tipo Dynamo usan lectura y escritura por quórum sin orden total: son más disponibles y escalables, pero no garantizan una secuencia única y pueden requerir reconciliación. El consenso es necesario cuando el orden y la unicidad de la decisión importan, por ejemplo en un log de transacciones o en la elección de líder.

## Características comunes

Los algoritmos de consenso prácticos comparten estos elementos:

- **Réplicas con estado:** cada nodo mantiene una copia del log o del estado y lo aplica de forma determinista.
- **Quórum:** las decisiones se confirman por mayoría, no por unanimidad; así se toleran fallos sin bloquear el sistema.
- **Líder o coordinador:** un nodo dirige la propuesta para evitar conflictos y reducir rondas. Si falla, se elige otro.
- **Términos o épocas:** números crecientes que identifican cada liderazgo y permiten descartar mensajes de líderes antiguos.
- **Commit y aplicación:** una entrada confirmada se aplica a la máquina de estados en orden; las no confirmadas pueden descartarse.
- **Recuperación:** un nodo que se reincorpora se sincroniza desde el líder o desde un snapshot.
- **Cambios de configuración:** la membresía del clúster puede modificarse sin comprometer la seguridad.
- **Compactación:** el log se recorta mediante snapshots para no crecer indefinidamente.

## Raft

Raft fue presentado por Diego Ongaro y John Ousterhout en 2014 con un objetivo explícito de **comprensibilidad** frente a Paxos. Descompone el consenso en tres problemas separados —elección de líder, replicación de log y seguridad— y ha tenido una adopción amplia en infraestructura moderna.

### Conceptos clave

| Concepto | Descripción |
|---|---|
| **Término** | Periodo lógico numerado de forma monotónica. Cada elección inaugura un término nuevo y permite distinguir líderes vigentes de anteriores. |
| **Rol** | Cada nodo es **líder**, **seguidor** o **candidato**. Hay como máximo un líder por término. |
| **Log** | Secuencia ordenada de entradas; cada entrada tiene índice, término y comando. |
| **Índice de commit** | Mayor índice que el líder considera confirmado y seguro de aplicar. |
| **Heartbeat** | `AppendEntries` sin entradas que el líder envía periódicamente para mantener su autoridad. |
| **Snapshot** | Imagen del estado de la máquina de estados en un índice, usada para compactar el log y sincronizar nodos atrasados. |
| **Configuración** | Conjunto de nodos que forman el clúster; puede cambiar mediante un procedimiento controlado. |

### Cómo trabaja

#### 1. Elección de líder

1. Los nodos arrancan como **seguidores**. Si no reciben un heartbeat dentro de su *timeout* de elección, pasan a **candidatos**.
2. El candidato incrementa su término, se vota a sí mismo y envía `RequestVote` a los demás nodos.
3. Un nodo vota si el candidato tiene un log al menos tan actualizado como el suyo y no ha votado en ese término.
4. Si obtiene mayoría, se convierte en líder y envía heartbeats; si no, vuelve a seguidor o espera un nuevo *timeout*.
5. Los *timeouts* se eligen de forma aleatoria para reducir votos divididos; si dos candidatos empatan, el siguiente intento suele resolverlo.

#### 2. Replicación de entradas

1. El cliente envía un comando al líder, que lo añade a su log local.
2. El líder envía `AppendEntries` con la entrada y la referencia de la entrada anterior (`prevLogIndex`, `prevLogTerm`).
3. Cada seguidor comprueba que su log coincide en ese punto; si coincide, añade la entrada y responde con éxito. Si no, el líder retrocede hasta encontrar el punto de divergencia y reenvía.
4. Cuando la entrada está replicada en la mayoría, el líder avanza su índice de commit, la aplica a su máquina de estados y comunica el nuevo índice a los seguidores, que la aplican en orden.
5. La propiedad de **coincidencia de log** garantiza que, si dos logs contienen una entrada con el mismo índice y término, son idénticos hasta ese punto.

#### 3. Reglas de seguridad

- **Restricción de elección:** un candidato solo puede ganar si su log es al menos tan actualizado como el de cada nodo que le concede el voto. Así, un líder futuro contiene todas las entradas confirmadas.
- **Commit del término actual:** el líder solo confirma directamente entradas de su propio término. Las entradas de términos anteriores se confirman de forma indirecta cuando se confirma una del término actual. Esta regla evita confirmar entradas que un líder posterior podría sobrescribir.
- **Persistencia antes de responder:** el término actual, el voto emitido y las entradas del log deben escribirse de forma durable antes de contestar una petición. Si no, un reinicio podría violar la seguridad.
- **Un solo líder por término:** los votos y los términos impiden que dos líderes del mismo término confirmen entradas distintas.

#### 4. Compactación y recuperación

El log no puede crecer sin límite. Cuando supera un umbral, el nodo guarda un snapshot del estado aplicado hasta un índice y descarta las entradas anteriores. Un seguidor que se retrasó demasiado recibe el snapshot mediante `InstallSnapshot` y continúa desde ahí.

#### 5. Cambios de membresía

Agregar o quitar nodos no puede hacerse de golpe si el clúster sigue operando: durante la transición deben solaparse las mayorías de la configuración antigua y la nueva. Raft admite **consenso conjunto** (*joint consensus*) o cambios de **un servidor a la vez**, que son más simples y suficientes en la mayoría de los casos.

### Arquitectura de una implementación

Una implementación de Raft combina estado persistente, estado volátil, transporte y la máquina de estados de la aplicación:

| Componente | Responsabilidad |
|---|---|
| **Transporte RPC** | Enviar `RequestVote`, `AppendEntries` e `InstallSnapshot`; puede ser HTTP, gRPC o un protocolo propio. |
| **Estado persistente** | `currentTerm`, `votedFor` y entradas del log, con escritura durable antes de responder. |
| **Estado volátil** | `commitIndex`, `lastApplied`, y en el líder `nextIndex` y `matchIndex` por seguidor. |
| **Máquina de estados** | Aplicación determinista de las entradas confirmadas; el resultado debe ser reproducible en todas las réplicas. |
| **Almacenamiento** | Log, snapshots y metadatos; el rendimiento depende de la durabilidad y de la latencia de disco. |
| **Temporizadores** | Timeout de elección y heartbeat; su relación determina la estabilidad del liderazgo. |
| **Cliente/API** | Enviar comandos, consultar estado y, cuando se requiere lectura linealizable, usar *ReadIndex* o *lease read*. |

Sobre esa base se añaden optimizaciones habituales: **pre-vote** para evitar que un nodo aislado incremente términos y perturbe al clúster, **check-quorum** para que el líder renuncie si no alcanza mayoría, *batching* y *pipelining* para mejorar el rendimiento, y lecturas locales con *lease* para evitar una entrada de log por cada consulta.

### Características

- **Líder fuerte:** simplifica el diseño y concentra las escrituras, a costa de un posible cuello de botella.
- **Seguridad independiente de los relojes:** los términos y los quórums garantizan el acuerdo; los relojes solo afectan a los timeouts y a la vivacidad.
- **Diseño comprensible y modular:** elección, replicación y seguridad se pueden estudiar y probar por separado.
- **Tolerancia a fallos de minoría:** con 3 nodos tolera 1 fallo; con 5, tolera 2.
- **Recuperación clara:** un nodo atrasado se sincroniza con el líder o con un snapshot.
- **Ecosistema maduro:** existen implementaciones probadas en varios lenguajes y plataformas.

### Recomendaciones

1. **Usar una implementación probada** en lugar de escribir Raft desde cero; las sutilezas de seguridad y recuperación son difíciles de validar.
2. **Preferir 3 o 5 nodos** y evitar números pares: no aumentan la tolerancia y sí el costo. Para cargas de lectura, añadir nodos *learner* o réplicas sin voto.
3. **Distribuir los nodos en dominios de fallo distintos** (zonas, racks, hosts) y evitar que una sola falla afecte a la mayoría.
4. **Dimensionar discos y fsync:** la latencia de escritura durable domina el rendimiento del consenso. Usar discos locales rápidos y separar el log del almacenamiento de snapshots.
5. **Ajustar timeouts con margen:** el timeout de elección debe ser varias veces mayor que el heartbeat y que la latencia de red esperada, para evitar elecciones espurias.
6. **Planificar la compactación:** definir políticas de snapshot, retención y restauración; un log sin compactar degrada el arranque y el uso de disco.
7. **Cambiar la membresía de forma controlada:** un nodo a la vez o con consenso conjunto, verificando que las mayorías se solapen.
8. **Automatizar la recuperación de quórum:** documentar qué hacer si se pierde la mayoría (restaurar nodos, recuperar desde snapshot) y probarlo.
9. **Observar el clúster:** cambios de líder, latencia de commit, retraso de seguidores, tasa de elecciones, tamaño del log y errores de disco.
10. **Probar fallos reales:** particiones de red, caída del líder, reinicios, latencia alta y pérdida de disco; el consenso suele fallar en los bordes.

### Limitaciones

- El líder concentra las escrituras; el rendimiento escala mejor con particiones o grupos de consenso independientes.
- Cada escritura requiere al menos una ida y vuelta a la mayoría; en clústeres geográficos la latencia se nota.
- No tolera fallos bizantinos: un nodo que miente puede romper las garantías.
- Sin mayoría no hay progreso: no se pueden confirmar nuevas entradas ni servir lecturas linealizables; solo quedan lecturas locales que pueden estar desactualizadas.
- La corrupción o pérdida de estado persistente puede requerir restaurar desde un snapshot y perder las entradas no respaldadas.
- Los snapshots y la sincronización de nodos nuevos consumen red, disco y tiempo.

### Implementaciones

| Producto | Uso del consenso |
|---|---|
| **etcd** | Metadata y estado de Kubernetes; descubrimiento, configuración y coordinación. |
| **Consul** | Descubrimiento de servicios, clave-valor y estado del clúster. |
| **CockroachDB** | Un grupo Raft por rango de datos; replicación y transacciones distribuidas. |
| **TiKV / TiDB** | Un grupo Raft por región; almacenamiento distribuido y consistente. |
| **Apache Kafka (KRaft)** | Quórum de metadata del clúster, en sustitución de ZooKeeper. |
| **RabbitMQ (quorum queues)** | Colas replicadas con confirmación por mayoría. |
| **HashiCorp Vault / Nomad** | Almacenamiento integrado y estado del clúster. |
| **Docker Swarm** | Estado de los nodos *manager*. |
| **MongoDB** | Protocolo propio de replica set, conceptualmente cercano a Raft (pv1). |

## Multi-Paxos

Paxos es la familia de algoritmos de consenso más influyente; **Multi-Paxos** es su variante práctica para acordar una secuencia de valores —un log— en lugar de un único valor. Fue descrito por Leslie Lamport y es la base de sistemas como Chubby y Spanner.

### Conceptos clave

| Concepto | Descripción |
|---|---|
| **Proponente (proposer)** | Nodo que propone un valor para una posición del log. |
| **Aceptor (acceptor)** | Nodo que vota y guarda los valores aceptados; el quórum se forma entre aceptores. |
| **Aprendiz (learner)** | Nodo que aprende el valor decidido para aplicarlo, sin participar necesariamente en la votación. |
| **Número de propuesta** | Identificador único y creciente que ordena las propuestas y evita conflictos. |
| **Fase 1 (prepare/promise)** | El proponente reserva la posición y descubre si ya existe un valor aceptado. |
| **Fase 2 (accept/accepted)** | El proponente envía su valor; si una mayoría lo acepta, queda decidido. |
| **Líder estable** | Proponente que ejecuta la fase 1 una vez y luego solo la fase 2 para cada nueva entrada. |

### Cómo trabaja

En **Paxos básico**, para decidir un valor:

1. El proponente elige un número de propuesta `n` y envía `Prepare(n)` a los aceptores.
2. Cada aceptor promete no aceptar propuestas menores que `n` y responde con el valor de mayor número que haya aceptado.
3. Si el proponente recibe mayoría, elige como valor el de mayor número recibido o, si no hay ninguno, el suyo propio; luego envía `Accept(n, v)`.
4. Los aceptores aceptan si no han prometido nada superior; cuando una mayoría acepta, el valor queda decidido.
5. Los aprendices se enteran del valor decidido y lo aplican.

**Multi-Paxos** aprovecha que el sistema decide muchas entradas seguidas: un líder estable ejecuta la fase 1 una sola vez para un rango de posiciones y después usa solo la fase 2 para cada entrada, lo que reduce las rondas. Los huecos se rellenan con valores nulos o se recuperan consultando a los aceptores.

La seguridad se apoya en la intersección de mayorías: si un valor ya fue decidido, cualquier propuesta posterior debe adoptarlo, porque la fase 1 obliga al proponente a descubrir el valor aceptado de mayor número. La vivacidad no está garantizada en un modelo asíncrono: dos proponentes pueden competir (*dueling*) y bloquearse; se resuelve con un líder estable, esperas aleatorias o un servicio de arrendamiento.

### Características

- **Seguridad demostrada y muy general:** sirve para un valor o para un log, y admite variantes de quórum.
- **Tolerancia a fallos de minoría:** con `2f + 1` aceptores tolera `f` fallos.
- **Líder estable para rendimiento:** evita la fase 1 en cada escritura.
- **Diseño más abstracto y difícil:** la especificación original es minimalista; los detalles de implementación (huecos, recuperación, líder) quedan fuera del algoritmo.
- **No incluye elección de líder:** hay que añadirla o implementarla con el propio Paxos.
- **Sensible a la latencia:** cada entrada requiere mayoría y, sin líder estable, rondas adicionales.

### Recomendaciones

- Usar una implementación madura —Chubby, Spanner o una librería probada— antes que una implementación propia.
- Mantener un líder estable y renovar su autoridad con un arrendamiento para evitar propuestas competidoras.
- Persistir las promesas y los valores aceptados antes de responder; de lo contrario se pierde la seguridad tras un reinicio.
- Planificar la recuperación de huecos y la compactación del log desde el inicio.
- Validar el comportamiento ante particiones y caída del líder; la vivacidad es la parte más frágil.

### Implementaciones

| Sistema | Uso |
|---|---|
| **Google Chubby** | Servicio de bloqueo y coordinación; descrito en *Paxos Made Live*. |
| **Google Spanner** | Grupos Paxos por partición, con relojes sincronizados (TrueTime). |
| **Apache Cassandra (LWT)** | Transacciones ligeras mediante Paxos. |
| **PaxosStore (WeChat)** | Almacenamiento distribuido basado en Paxos. |

## Otros algoritmos relevantes

| Algoritmo | Idea principal | Cuándo considerarlo |
|---|---|---|
| **ZAB** (ZooKeeper Atomic Broadcast) | Difusión atómica con épocas y un líder primario; orden total y recuperación por historial más actualizado. | Coordinación y metadata con ZooKeeper. |
| **Viewstamped Replication** | Réplica primaria con números de vista; precursor conceptual de Raft. | Estudio y sistemas que ya lo implementan. |
| **EPaxos** | Sin líder; cada comando declara dependencias y se ejecuta en un grafo; camino rápido en un solo viaje. | Distribución geográfica con conflictos bajos; complejidad de recuperación alta. |
| **Flexible Paxos** | Solo exige que los quórums de fase 1 y fase 2 se solapen, no que ambos sean mayorías. | Optimizar latencia o disponibilidad con quórums asimétricos. |
| **PBFT / Tendermint / HotStuff** | Consenso bizantino tolerante a fallos maliciosos. | Blockchain y entornos con nodos no confiables. |

## Comparación

| Criterio | Raft | Multi-Paxos | ZAB |
|---|---|---|---|
| **Líder** | Elegido por elección con términos y timeouts. | Líder estable añadido sobre Paxos. | Líder primario con épocas. |
| **Enfoque** | Log replicado y máquina de estados. | Acuerdo por posiciones de log. | Difusión atómica primario-respaldo. |
| **Comprensibilidad** | Alta; diseñado para ser entendible. | Media; la especificación es abstracta. | Media. |
| **Madurez** | Muy alta y amplia adopción. | Muy alta en grandes plataformas. | Alta, ligada a ZooKeeper. |
| **Complejidad de implementación** | Moderada. | Alta. | Moderada. |
| **Uso típico** | Metadata, coordinación, bases distribuidas. | Infraestructura a gran escala. | Coordinación con ZooKeeper. |

## Criterios para elegir

1. **Usar consenso para estado pequeño y crítico:** metadata, configuración, liderazgo, coordinación o el log de una base de datos. No para el plano de datos de alto volumen.
2. **Preferir Raft por defecto** en proyectos nuevos: documentación, implementaciones y herramientas de operación son abundantes.
3. **Elegir la familia Paxos** cuando ya existe una plataforma que la usa o cuando se requiere una variante específica de quórum o latencia.
4. **No implementar el algoritmo desde cero** salvo que sea el producto principal; es fácil violar la seguridad en los casos límite.
5. **Dimensionar la latencia:** cada escritura espera a la mayoría; ubicar los nodos cerca reduce el costo.
6. **Diseñar para la pérdida de quórum:** definir respaldo, restauración y procedimientos de recuperación antes de necesitarlos.
7. **Separar el consenso del dominio:** el log ordena comandos; la lógica de negocio debe ser determinista y validarse con pruebas.
8. **Proteger el clúster:** autenticación mutua, cifrado en tránsito, control de acceso y registros de auditoría. El consenso no aporta esas garantías por sí solo.

## Caso de uso: metadata de un clúster

Un orquestador de contenedores necesita un registro único de nodos, servicios, configuración y leases. Tres instancias de un almacén con Raft mantienen ese estado:

- El líder recibe cada cambio, lo replica y lo confirma cuando dos de las tres réplicas lo tienen.
- Si el líder falla, una elección con timeouts aleatorizados designa uno nuevo y el clúster continúa.
- Si dos de los tres nodos quedan inaccesibles, el clúster pierde la mayoría y deja de aceptar escrituras para no arriesgar decisiones contradictorias.
- Un nodo nuevo se sincroniza desde un snapshot y luego recibe las entradas posteriores.

El mismo patrón se repite en etcd para Kubernetes, en Consul para descubrimiento o en KRaft para la metadata de Kafka. La decisión de arquitectura no es solo el algoritmo: también importa distribuir los nodos, dimensionar el disco, definir snapshots y monitorear el liderazgo.

## Referencias externas

- [In Search of an Understandable Consensus Algorithm (Raft)](https://raft.github.io/raft.pdf) — Ongaro y Ousterhout, USENIX ATC 2014.
- [Raft: sitio del proyecto](https://raft.github.io/) — especificación, visualización y recursos.
- [Paxos Made Simple](https://lamport.azurewebsites.net/pubs/paxos-simple.pdf) — Leslie Lamport.
- [Paxos Made Live](https://research.google/pubs/pub35605/) — experiencia de implementación en Chubby.
- [Zab: High-performance broadcast for primary-backup systems](https://www.usenix.org/system/files/conference/atc11/atc11-paper-junqueira.pdf) — Junqueira, Reed y Serafini.
- [etcd: documentación de consenso](https://etcd.io/docs/) — implementación de Raft y operación.
- [Consul: consenso y Raft](https://developer.hashicorp.com/consul/docs/architecture/consensus) — arquitectura del clúster.
- [CockroachDB: replication layer](https://www.cockroachlabs.com/docs/stable/architecture/replication-layer) — Raft por rango.
- [Apache Kafka: KRaft](https://kafka.apache.org/documentation/#kraft) — quórum de metadata.

## Referencias internas

- [Datos](../README.md)
- [Consistencia](README.md)
- [Atributos de calidad](../../fundamentos/atributos-de-calidad.md)
- [Observabilidad](../../fundamentos/observabilidad.md)
- [Microservicios](../../estilos-y-patrones/microservicios/README.md)
- [Docker Swarm](../../../container/docker-swarm.md)
