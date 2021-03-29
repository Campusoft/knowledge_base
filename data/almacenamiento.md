# Delta Lake
 
El año pasado Databricks liberó a la comunidad su nuevo formato de persistencia de datos construido sobre almacenamientos del tipo ‘Write-Once’ ‘Read-Many’ (HDFS, S3, Blob storage) y basado en Apache Parquet.

Es una capa de almacenamiento open source que proporciona transacciones ACID a través de un control de concurrencia óptimo entre las escrituras y el aislamiento de snapshots para lecturas consistentes durante las escrituras.