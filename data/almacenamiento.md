# Delta Lake
 
El año pasado Databricks liberó a la comunidad su nuevo formato de persistencia de datos construido sobre almacenamientos del tipo ‘Write-Once’ ‘Read-Many’ (HDFS, S3, Blob storage) y basado en Apache Parquet.

Es una capa de almacenamiento open source que proporciona transacciones ACID a través de un control de concurrencia óptimo entre las escrituras y el aislamiento de snapshots para lecturas consistentes durante las escrituras.


Delta Lake is the optimized storage layer that provides the foundation for storing data and tables in the Databricks Lakehouse Platform. Delta Lake is open source software that extends Parquet data files with a file-based transaction log for ACID transactions and scalable metadata handling. Delta Lake is fully compatible with Apache Spark APIs, and was developed for tight integration with Structured Streaming, allowing you to easily use a single copy of data for both batch and streaming operations and providing incremental processing at scale.