# Varios


Roadmap to becoming a data engineer in 2020 
https://github.com/datastacktv/data-engineer-roadmap
 
 
Apache Arrow Overview
A critical component of Apache Arrow is its in-memory columnar format, a standardized, language-agnostic specification for representing structured, table-like datasets in-memory. This data format has a rich data type system (included nested and user-defined data types) designed to support the needs of analytic database systems, data frame libraries, and more.
http://arrow.apache.org/overview/


Dremio


Dremio is a next-generation data lake engine that liberates your data with live, interactive queries directly on cloud data lake storage.

Dremio es un motor de lago de datos en la nube que proporciona consultas interactivas sobre almacenes de lagos de datos alojados en la nube. Con Dremio no hay que manejar pipelines de datos para extraer y transformar datos dentro de un  almacén de datos separado para alcanzar un rendimiento predictivo. Dremio crea conjuntos de datos virtuales a partir de los datos ingeridos dentro del lago de datos y proporciona una visión uniforme a los consumidores. Presto popularizó la técnica de separar el almacenamiento de la capa  de computación y Dremio la lleva más lejos mejorando el rendimiento y optimizando los costos operativos.

https://www.dremio.com/


Apache Kylin, servidor analítico distribuido sobre Hadoop

Dado que este Volumen de datos es demasiado grande para analizarlo con un rendimiento aceptable con los sistemas  OLAP (R-OLAP y M-OLAP)  tradicionales, hemos decidido probar la tecnología  Apache Kylin , la cual promete tiempos de respuesta de unos pocos segundos para Volúmenes que pueden superar los 10 billones de filas en la tabla de hechos o medidas. 



scikit-learn
Machine Learning in Python
https://scikit-learn.org/stable/#


Apache Beam Overview

Apache Beam is an open source, unified model for defining both batch and streaming data-parallel processing pipelines. Using one of the open source Beam SDKs, you build a program that defines the pipeline. The pipeline is then executed by one of Beam’s supported distributed processing back-ends, which include Apache Flink, Apache Spark, and Google Cloud Dataflow.


Great talks on #DataStreaming and the #Beam magic that lets you write your data pipelines once and run them anywhere! #Spark #Flink #Dataflow 


Apache Spark, an open-source analytics engine for processing big data. It's designed to process large amounts of data in memory to provide better performance than other solutions that rely on persistent storage.

Apache Spark supports Java, Scala, Python, R, and SQL out of the box. Microsoft created .NET for Spark to add support for .NET.

Spark implementa el procesamiento de datos haciendo uso intensivo de la Memoria Ram del clúster, en lugar de hacer uso intensivo de disco como MapReduce.

De esta forma se consigue mejorar en gran medida el rendimiento de las aplicaciones Big Data, siendo adecuado para la implementación de algoritmos iterativos de Machine Learning (MLib), análisis estadístico (módulo R) o el análisis de datos en tiempo real (Spark Streaming)



Varios


Hive is a prominent open source data warehouse built on Hadoop’s Distributed File System (HDFS). It is used for — data storage, data summarization, data query and analysis on large data systems. Hive is easy to use for novice, as you need to simply write SQL like query. Hive converts SQL queries into Mapreduce / Tez / Spark job depending on admin configured settings.


Hadoop




Apache Parquet is a columnar storage format available to any project in the Hadoop ecosystem


Amundsen:

Amundsen Metadata service can use Apache Atlas as a backend. Some of the benefits of using Apache Atlas instead of Neo4j is that Apache Atlas offers plugins to several services (e.g. Apache Hive, Apache Spark) that allow for push based updates. It also allows to set policies on what metadata is accesible and editable by means of Apache Ranger.

# Apache Avro

# prestodb

Distributed SQL Query Engine for Big Data

Presto allows querying data where it lives, including Hive, Cassandra, relational databases or even proprietary data stores. A single Presto query can combine data from multiple sources, allowing for analytics across your entire organization. 

 
https://prestodb.io/

## AWS Athena 

AWS Athena is a serverless interactive analytics service offered by Amazon that can be readily used to gain insights on data residing in S3. Under to hood, Athena used a distributed SQL engine called Presto, which is used to run the SQL queries.

