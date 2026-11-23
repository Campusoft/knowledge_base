# Escalamiento de bases de datos: 10 conceptos clave

Resumen de diez técnicas para que una base de datos atienda más datos, usuarios o solicitudes. Incluye una comparación para decidir por dónde empezar. Para un análisis más profundo de escalado, particionamiento, sharding y replicación, ver [Escalabilidad de bases de datos](../../datos/escalabilidad.md).

## Video

Animación con los diez conceptos:

[Ver video: escalamiento-de-bases-de-datos.mp4](escalamiento-de-bases-de-datos/escalamiento-de-bases-de-datos.mp4)

<video src="escalamiento-de-bases-de-datos/escalamiento-de-bases-de-datos.mp4" controls muted loop width="640"></video>

## Resumen

Las técnicas se agrupan según el problema que atacan:

- **Hacer más eficiente lo que ya existe:** indexación, optimización de consultas y connection pooling.
- **Reducir la carga sobre la base de datos:** caching, desnormalización y vistas materializadas.
- **Aumentar la capacidad:** escalado vertical, replicación y sharding.
- **Reorganizar los datos:** particionamiento vertical.

## Los diez conceptos

### 1. Indexación

- Acelera las consultas de lectura creando índices sobre las columnas que se consultan con más frecuencia.
- Costo: ocupa espacio y hace más lentas las escrituras, porque cada cambio debe actualizar los índices.

### 2. Escalado vertical

- Agrega CPU, memoria RAM o almacenamiento al servidor de base de datos para soportar más carga.
- Es la opción más simple de operar, pero tiene límites físicos y puede resultar costosa.
- Más información: [Escalado vertical y horizontal](../../datos/escalabilidad.md#escalado-vertical-y-horizontal) y [Escalabilidad en SQL Server](../../../data_base/sql_server/escalabilidad.md#escalado-vertical).

### 3. Caching

- Usa almacenes en memoria, como Redis, para servir con más rapidez los datos de uso frecuente y reducir la carga sobre la base de datos.
- Hay que definir expiración e invalidación para evitar datos desactualizados.
- Más información: [Redis](../../../data_base/nosql/redis.md), [Cache en .NET](../../../dotnet/net/operacion/cache.md) y la fila "Caché" del [catálogo de componentes](../../componentes/catalogo.md).

### 4. Sharding

- Divide la base de datos en fragmentos (shards) más pequeños e independientes y los distribuye entre servidores. Permite el escalado horizontal.
- La clave de shard determina dónde se guarda cada registro; una mala clave genera zonas de carga desigual.
- Más información: [Sharding](../../datos/escalabilidad.md#sharding) y [Sharding en SQL Server](../../../data_base/sql_server/escalabilidad.md).

### 5. Replicación

- Crea varias copias (réplicas) de la base de datos en distintos servidores para repartir las lecturas y mejorar la disponibilidad.
- Las réplicas asíncronas pueden devolver datos con retraso. Una réplica no sustituye a una copia de seguridad.
- Más información: [Replicación](../../datos/escalabilidad.md#replicación), [Consistencia de datos](../../datos/consistencia/README.md) y [Replicación y alta disponibilidad en SQL Server](../../../data_base/sql_server/escalabilidad.md).

### 6. Optimización de consultas

- Ajusta las consultas SQL, elimina operaciones costosas y aprovecha bien los índices para que se ejecuten más rápido.
- Conviene revisar el plan de ejecución antes de aumentar la capacidad del servidor.
- Más información: el paso 2 de [Cómo elegir](../../datos/escalabilidad.md#cómo-elegir) y [Rendimiento como atributo de calidad](../../fundamentos/atributos-de-calidad.md).

### 7. Connection pooling

- Reduce el costo de abrir y cerrar conexiones reutilizando las que ya existen.
- Hay que dimensionar el límite del pool y los tiempos de espera para no agotar las conexiones de la base de datos.
- Más información: [Pool de conexiones en Oracle](../../../data_base/oracle/pool.md), [pooling de DbContext en EF Core](../../../dotnet/net/datos/orm.md), la fila "pool de conexiones" del [catálogo de componentes](../../componentes/catalogo.md) y [Hyperdrive de Cloudflare](../../../cloud/Cloudflare/Servicios-Cloudflare.md).

### 8. Particionamiento vertical

- Divide una tabla grande en tablas más pequeñas, cada una con un subconjunto de las columnas originales.
- Es útil, por ejemplo, para separar columnas de uso frecuente de columnas grandes o poco consultadas.
- No confundir con el escalado vertical (concepto 2). El particionamiento vertical reorganiza columnas; el escalado vertical aumenta los recursos del servidor.
- Relacionado: el [particionamiento](../../datos/escalabilidad.md#particionamiento) horizontal por filas se describe en la guía de escalabilidad.

### 9. Desnormalización

- Almacena datos de forma redundante pero estructurada para evitar uniones (joins) complejas y acelerar cargas con muchas lecturas.
- Costo: más espacio, y las actualizaciones deben mantener consistentes las copias de los datos.

### 10. Vistas materializadas

- Calcula de antemano el resultado de consultas complejas y lo guarda como una tabla separada, para evitar recalcularlo en cada consulta.
- Hay que definir cómo y cuándo se refresca para controlar el desfase respecto a los datos originales.

## Comparación

| # | Técnica | Qué mejora | Costo o riesgo principal | Cuándo considerarla |
|---|---|---|---|---|
| 1 | Indexación | Lecturas | Espacio y escrituras más lentas | Consultas lentas sobre columnas filtradas con frecuencia |
| 2 | Escalado vertical | Capacidad general | Límite físico y costo | Cuello de botella claro de CPU, memoria o I/O |
| 3 | Caching | Lecturas y latencia | Datos desactualizados e invalidación | Datos que se leen mucho y cambian poco |
| 4 | Sharding | Capacidad de escritura y almacenamiento | Complejidad de enrutamiento y consultas entre shards | Un solo nodo ya no alcanza |
| 5 | Replicación | Lecturas y disponibilidad | Retraso de réplicas y failover | Muchas lecturas o necesidad de alta disponibilidad |
| 6 | Optimización de consultas | Tiempo de ejecución | Esfuerzo de análisis y mantenimiento | Siempre, antes de ampliar recursos |
| 7 | Connection pooling | Costo de conexión | Agotamiento del pool si se dimensiona mal | Muchas conexiones cortas |
| 8 | Particionamiento vertical | Tamaño de filas y E/S por consulta | Más uniones al reconstruir la fila completa | Tablas con columnas grandes o poco usadas |
| 9 | Desnormalización | Lecturas con joins | Redundancia y consistencia | Cargas de lectura intensiva con joins costosos |
| 10 | Vistas materializadas | Consultas agregadas o complejas | Refresco y datos desfasados | Reportes o agregados costosos de recalcular |

## Cómo empezar

1. Medir primero dónde está el cuello de botella: CPU, memoria, I/O, bloqueos o consultas concretas.
2. Aplicar primero las técnicas de bajo costo y bajo riesgo: **indexación**, **optimización de consultas** y **connection pooling**.
3. Reducir la carga de lectura con **caching**, **vistas materializadas** o **desnormalización**, aceptando cierto desfase o redundancia.
4. Aumentar capacidad con **escalado vertical** mientras sea viable y económico.
5. Añadir **réplicas** para repartir lecturas y mejorar disponibilidad.
6. Recurrir a **sharding** cuando un solo nodo ya no cubra la capacidad o la escritura requerida, porque agrega más complejidad operativa.

## Material de apoyo

- [Video del resumen](escalamiento-de-bases-de-datos/escalamiento-de-bases-de-datos.mp4)
- [Escalabilidad de bases de datos](../../datos/escalabilidad.md)
- [Datos: índice del área](../../datos/README.md)

## Referencias

- Resumen de diez conceptos y animación proporcionados por el propietario del repositorio. El video proviene de una publicación en X; la URL del archivo original es `https://video.twimg.com/tweet_video/HW0dKdDbkAAyvDe.mp4`. No se identificó el autor. La publicación original enlazaba a lecciones de algomaster.io, que no se incluyen aquí.
- Las secciones "Comparación", "Cómo empezar" y los costos o riesgos señalados en cada concepto amplían el resumen original con criterios generales y no provienen de esa publicación.
- No existen en el repositorio documentos específicos sobre indexación, desnormalización, vistas materializadas ni particionamiento vertical. Si se crean, enlazarlos en los conceptos 1, 8, 9 y 10.
