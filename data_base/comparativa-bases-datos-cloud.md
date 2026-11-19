# Comparativa: Amazon Aurora, AlloyDB for PostgreSQL y Azure HorizonDB

Fecha de revisión: 18 de noviembre de 2026.

## Alcance

Comparación de servicios relacionales administrados con cómputo y almacenamiento separados. Para una comparación entre motores PostgreSQL, seleccionar **Aurora PostgreSQL**; Aurora MySQL responde a otro requisito de compatibilidad. Aurora DSQL y AlloyDB Omni quedan fuera de este alcance.

Los detalles y sus fuentes están en [Amazon Aurora](../cloud/amazon/amazon-aurora.md), [AlloyDB](../cloud/google/alloydb-postgresql.md) y [Azure HorizonDB](../cloud/azure/azure-horizondb.md).

## Características y arquitectura

| Criterio | Amazon Aurora | AlloyDB for PostgreSQL | Azure HorizonDB |
|---|---|---|---|
| Proveedor | AWS | Google Cloud | Microsoft Azure |
| Motor compatible | PostgreSQL o MySQL, según edición | PostgreSQL | PostgreSQL |
| Escritura regional | Instancia primaria | Instancia primaria | Nodo writer |
| Escala lectora | Aurora Replicas | Read pools | Lectores del pool de lectura y escritura |
| Persistencia | Volumen distribuido entre zonas | Almacenamiento regional separado del cómputo | Servicios de WAL y datos respaldados por Blob Storage |
| Protección zonal | Almacenamiento redundante; agregar cómputo en otras zonas | Primaria HA con activo y reserva | Varios nodos y configuración de HA zonal |
| Opción regional de recuperación | Aurora Global Database | Clústeres secundarios asíncronos | Respaldo georredundante y restauración; sin lectores entre regiones actualmente |
| Aspecto para evaluar | Ediciones, Serverless v2 y modelo de E/S | Motor columnar y separación de analítica en read pools | Diseño basado en WAL e integración con Azure |

Fuentes: [Aurora: clústeres](https://docs.aws.amazon.com/AmazonRDS/latest/AuroraUserGuide/Aurora.Overview.html), [Aurora: durabilidad](https://docs.aws.amazon.com/rds/latest/auroraextendedcontent/aurora-faq-availability-and-durability.html), [AlloyDB: arquitectura](https://docs.cloud.google.com/alloydb/docs/high-availability), [AlloyDB: recuperación regional](https://docs.cloud.google.com/alloydb/docs/cross-region-replication/about-cross-region-replication) y [HorizonDB: arquitectura y restricciones](https://learn.microsoft.com/en-us/azure/horizondb/overview/overview).

## Casos de uso y recomendaciones de elección

Estas son recomendaciones de evaluación, no un ranking de rendimiento:

| Necesidad | Primera alternativa para evaluar | Prueba decisiva |
|---|---|---|
| Aplicación MySQL en AWS | Aurora MySQL | Compatibilidad y costo con la carga real |
| Aplicación PostgreSQL en AWS | Aurora PostgreSQL | Transacciones, extensiones y failover |
| Transacciones y reportes sobre datos recientes en Google Cloud | AlloyDB | Ganancia del motor columnar y aislamiento de reportes |
| Aplicación PostgreSQL integrada con Azure | HorizonDB y Azure Database for PostgreSQL | Compatibilidad, operación y restricciones de recuperación |
| Recuperación regional con objetivo exigente | Evaluar primero los mecanismos regionales de cada servicio | RPO y RTO medidos en un simulacro |
| Aplicación pequeña con presupuesto ajustado | Comparar también RDS, Cloud SQL y Azure Database for PostgreSQL | Costo total mínimo que cumpla los requisitos |

Ubicar aplicación y base de datos en el mismo entorno reduce complejidad de red y transferencia. Una ventaja de motor debe justificar cualquier cambio de proveedor o arquitectura.

## Limitaciones que deben evaluarse

- Escalar lectores no elimina el límite de escritura de la primaria.
- Compartir almacenamiento no garantiza que una réplica haya aplicado el último cambio.
- Compatibilidad PostgreSQL no implica igualdad de extensiones, privilegios y versiones.
- La alta disponibilidad entre zonas no cubre por sí sola la pérdida de una región.
- Un respaldo solo demuestra utilidad cuando se puede restaurar dentro del tiempo requerido.
- Rendimiento y precio dependen de consultas, conexiones, datos, región y topología. No comparar cifras comerciales obtenidas con pruebas distintas.

## Validación antes de decidir

1. Inventariar esquema, extensiones, procedimientos, volúmenes y crecimiento.
2. Definir latencia p95/p99, transacciones por segundo, concurrencia y consistencia requerida.
3. Ejecutar carga equivalente, con datos representativos y condiciones de caché documentadas.
4. Medir failover, restauración, pérdida de datos y comportamiento de reintentos.
5. Validar identidad, cifrado, conectividad, auditoría y residencia de datos.
6. Estimar cómputo, lectores, almacenamiento, E/S facturable, respaldos, red y recuperación regional.
7. Documentar portabilidad, migración de salida y dependencia de funciones propietarias.

## Costos y mantenimiento

Cotizar una topología equivalente, con la misma disponibilidad y recuperación. No comparar una instancia única con una configuración de varias zonas y otra región. Consultar [Aurora](https://aws.amazon.com/rds/aurora/pricing/), [AlloyDB](https://cloud.google.com/alloydb/pricing) y [HorizonDB](https://azure.microsoft.com/en-us/pricing/details/horizondb/).

Actualizar esta comparativa al cambiar versiones, límites, regiones, precios o capacidades de recuperación. Registrar en la decisión final la fecha, región, versión, configuración y resultados medidos.
