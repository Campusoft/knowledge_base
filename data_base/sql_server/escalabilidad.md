# Escalabilidad en SQL Server

Guía para elegir e implementar técnicas de escalabilidad en SQL Server, desde SQL Server 2016 hasta SQL Server 2025 (17.x), la versión actual al revisar este documento (2026-11-01). La edición, el sistema operativo y el modelo de despliegue pueden cambiar qué opciones están disponibles.

## Resumen de las técnicas

| Técnica | Implementación en SQL Server | Qué resuelve | Qué no resuelve por sí sola |
|---|---|---|---|
| Escalado vertical | Asignar más CPU, memoria y capacidad de I/O a la instancia o ampliar el tamaño de servicio en Azure SQL | Más recursos para una instancia | No elimina el límite de capacidad de un solo nodo |
| Particionamiento | Dividir una tabla o índice en particiones mediante una función y un esquema | Administración de tablas grandes, retención y consultas que filtran por la clave de partición | No distribuye la carga entre instancias |
| Sharding | Dividir filas en bases de datos/instancias y enrutar cada solicitud a su shard | Escalado horizontal de almacenamiento y carga | No ofrece una transacción global simple ni consultas transparentes entre todos los shards |
| Replicación | Publicador y suscriptores (replicación SQL) o réplicas de disponibilidad (Always On) | Distribución de datos, alta disponibilidad, recuperación o lecturas en secundarias, según la opción | No es una copia de seguridad ni un sustituto universal de sharding |

## Escalado vertical

El escalado vertical (*scale up*) consiste en aumentar recursos del mismo servidor o instancia: CPU, memoria, almacenamiento y rendimiento de I/O. En SQL Server instalado en un servidor o máquina virtual se amplía el host; en Azure SQL se cambia el nivel de servicio o la capacidad asignada.

Antes de ampliar, revisar consultas costosas, índices, estadísticas, bloqueos y esperas de I/O. Más recursos pueden aliviar un límite de capacidad, pero no corrigen consultas ineficientes ni eliminan los límites de edición.

## Particionamiento de tablas e índices

SQL Server divide una tabla o índice de una misma base de datos usando:

1. Una **función de partición**, que define los límites y a qué partición corresponde cada valor.
2. Un **esquema de partición**, que asigna las particiones a grupos de archivos (*filegroups*).
3. Una tabla o índice asociado al esquema mediante su columna de partición.

Ejemplo mínimo con límites mensuales. Los filegroups deben existir antes de asignarlos; aquí todas las particiones usan `PRIMARY` para mantener el ejemplo autocontenido:

```sql
CREATE PARTITION FUNCTION pf_VentasPorFecha (date)
AS RANGE RIGHT FOR VALUES ('2025-02-01', '2025-03-01');
GO

CREATE PARTITION SCHEME ps_VentasPorFecha
AS PARTITION pf_VentasPorFecha ALL TO ([PRIMARY]);
GO

CREATE TABLE dbo.Ventas
(
    VentaId     bigint       NOT NULL,
    FechaVenta  date         NOT NULL,
    Importe     decimal(12,2) NOT NULL
)
ON ps_VentasPorFecha (FechaVenta);
GO
```

`RANGE RIGHT` asigna cada fecha límite al rango que comienza en esa fecha. En producción, planificar la creación de la siguiente partición antes de cargar datos y retirar particiones antiguas con procedimientos de mantenimiento controlados. Para cargas históricas o retención, `ALTER TABLE ... SWITCH PARTITION` puede mover una partición de forma eficiente si la tabla de destino, sus índices y restricciones cumplen los requisitos de compatibilidad.

Puntos de diseño:

- Elegir una columna que aparezca en filtros y represente una frontera útil, como fecha o tenant.
- Mantener índices alineados con la tabla cuando sea necesario administrar o cambiar particiones.
- Incluir la columna de partición en las claves únicas cuando el motor lo requiera para preservar unicidad.
- Medir el *partition elimination* con planes de ejecución; particionar no garantiza que una consulta sea más rápida.
- Separar filegroups puede ayudar a administración, almacenamiento o backup por archivo, pero no convierte una instancia en varios servidores.

## Sharding (particionamiento horizontal entre instancias)

SQL Server no convierte una tabla de una base de datos convencional en shards distribuidos transparentemente entre servidores con `CREATE PARTITION FUNCTION`. Para escalar horizontalmente se diseñan varias bases de datos con esquemas compatibles y se decide fuera de la tabla cuál contiene cada fila.

Implementación habitual a nivel de aplicación:

1. Elegir una clave de shard estable, por ejemplo `TenantId`.
2. Crear bases de datos con el mismo esquema y definir el mapa `TenantId → base de datos` (o rangos de claves → base de datos).
3. Enrutar cada conexión/consulta al shard correspondiente desde el servicio o una capa de acceso a datos.
4. Definir cómo migrar o rebalancear tenants cuando crece un shard, y cómo desplegar cambios de esquema en todos.
5. Diseñar aparte reportes globales, transacciones entre shards, backups, monitoreo y recuperación.

En instalaciones SQL Server locales, el mapa y el enrutamiento suelen ser responsabilidad de la aplicación o de una capa de acceso a datos propia. Las **vistas particionadas distribuidas** (*distributed partitioned views*) son una opción especializada que usa servidores vinculados y restricciones `CHECK`; están sujetas a requisitos del motor y de edición, por lo que no son un sharding automático para cualquier aplicación.

Azure SQL Database dispone de herramientas de elasticidad y bibliotecas de cliente para administrar mapas de shards y enrutamiento dependiente de datos. Esas herramientas son específicas de Azure SQL Database; validar su estado y compatibilidad antes de adoptarlas para un proyecto nuevo.

## Replicación y alta disponibilidad

En SQL Server, “replicar” puede referirse a mecanismos distintos. Elegir según el objetivo:

### Replicación SQL

SQL Server Replication publica cambios o una instantánea de artículos/tablas desde un **Publisher** hacia uno o más **Subscribers**, mediante un **Distributor** según la topología.

- **Snapshot:** distribuye el estado de los datos en un momento; útil para inicialización o datos que cambian poco.
- **Transactional:** propaga cambios en orden, normalmente con baja latencia; útil para distribución selectiva de datos e integración.
- **Merge:** permite cambios en Publisher y Subscribers y reconcilia cambios; requiere resolver conflictos y tiene más complejidad.
- **Peer-to-peer:** topología basada en replicación transaccional para varias instancias; requiere diseñar para evitar conflictos y verificar edición/versión.

La configuración se realiza en SSMS o mediante procedimientos de replicación: configurar distribuidor, habilitar la base como publicadora, crear publicación y artículos, crear suscripciones, inicializar y supervisar agentes. La matriz de versiones y ediciones varía por rol (Publisher, Distributor, Subscriber) y tipo de replicación; comprobar la matriz oficial antes de diseñar la topología.

### Always On Availability Groups

Para alta disponibilidad y recuperación ante desastres de una base de datos completa, evaluar **Always On Availability Groups**. Las réplicas mantienen bases secundarias y admiten failover según el modo de disponibilidad y configuración. Algunas ediciones ofrecen grupos básicos con límites de bases y réplicas; las funciones avanzadas y réplicas legibles dependen de versión y edición.

Un grupo de disponibilidad puede ayudar a distribuir lecturas si la aplicación se conecta explícitamente a una secundaria legible y acepta el posible retraso. No divide una tabla ni sustituye un diseño de shards.

### Copias de seguridad

La replicación y las réplicas de alta disponibilidad no sustituyen backups: un borrado o cambio lógico erróneo puede propagarse. Mantener backups con retención y probar restauraciones.

## Consideraciones de versión: SQL Server 2016 a 2025

| Versión | Consideración relevante |
|---|---|
| SQL Server 2016 RTM | Confirmar edición y nivel de actualización antes de usar particionamiento; varias capacidades se ampliaron con SP1. |
| SQL Server 2016 SP1 en adelante | Microsoft extendió el particionamiento a Standard, Web y Express, además de Enterprise. Verificar funciones paralelas y límites propios de cada edición. |
| SQL Server 2017, 2019 y 2022 | Particionamiento disponible en varias ediciones; límites de rendimiento, alta disponibilidad y replicación siguen dependiendo de la edición y la topología. |
| SQL Server 2025 (17.x) | Versión actual al revisar esta guía. La matriz oficial mantiene el particionamiento de tablas e índices en Enterprise, Standard y Express; las capacidades de Availability Groups y los límites de recursos varían por edición. |

SQL Server 2016 terminó su soporte extendido el **14 de julio de 2026**. Microsoft ofrece Extended Security Updates (ESU) para clientes elegibles hasta 2029; esto no convierte 2016 en la versión recomendada para nuevos despliegues. Para sistemas que continúan en 2016, revisar la elegibilidad ESU y planificar actualización o migración.

Antes de aplicar cualquiera de estas técnicas, comprobar en la documentación oficial la versión exacta, edición, sistema operativo, límites de recursos, requisitos de clúster y licenciamiento. La compatibilidad del motor no implica que todas las capacidades tengan el mismo nivel de servicio en cada edición.

## Guía rápida de decisión

1. **Una instancia está saturada:** identificar el recurso limitante y optimizar consultas; si persiste, escalar verticalmente.
2. **Una tabla crece y se administra por períodos o rangos:** evaluar particionamiento y mantenimiento por partición.
3. **Se requiere distribuir filas y escrituras entre servidores:** diseñar sharding con clave, mapa, enrutamiento y plan de rebalanceo.
4. **Se requiere continuidad ante fallo:** evaluar Availability Groups, Failover Cluster Instances o log shipping según RTO/RPO y edición.
5. **Se requiere distribuir cambios de tablas a otros sistemas:** evaluar replicación SQL, definiendo latencia, dirección de escritura y manejo de conflictos.

## Referencias oficiales

- [Tablas e índices particionados](https://learn.microsoft.com/sql/relational-databases/partitions/partitioned-tables-and-indexes)
- [CREATE PARTITION FUNCTION](https://learn.microsoft.com/sql/t-sql/statements/create-partition-function-transact-sql)
- [ALTER TABLE ... SWITCH](https://learn.microsoft.com/sql/t-sql/statements/alter-table-transact-sql)
- [Novedades de SQL Server 2016: SP1 y particionamiento](https://learn.microsoft.com/sql/sql-server/sql-server-2016-release-notes)
- [Ediciones y características compatibles con SQL Server 2025](https://learn.microsoft.com/sql/sql-server/editions-and-components-of-sql-server-2025)
- [Replicación de SQL Server](https://learn.microsoft.com/sql/relational-databases/replication/sql-server-replication)
- [Tipos de replicación](https://learn.microsoft.com/sql/relational-databases/replication/types-of-replication)
- [Always On Availability Groups](https://learn.microsoft.com/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [Escalado elástico y sharding en Azure SQL Database](https://learn.microsoft.com/azure/azure-sql/database/elastic-scale-introduction)
- [Ciclo de vida de SQL Server 2016](https://learn.microsoft.com/lifecycle/products/sql-server-2016)
- [Preguntas frecuentes sobre Extended Security Updates](https://learn.microsoft.com/sql/sql-server/end-of-support/extended-security-updates-frequently-asked-questions)
