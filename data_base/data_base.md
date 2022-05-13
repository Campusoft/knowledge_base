
**Why Naming Conventions Are Important**

Names Are Contracts

Database objects are referenced by their names, thus object names are part of the contract for an object. In a way you can consider your database table and column names to be the API to your data model.

Once they are set, changing them may break dependent applications. This is all the more reason to name things properly before the first use.


Developer Context Switching

Having consistent naming conventions across your data model means that developers will need to spend less time looking up the names of tables, views, and columns. Writing and debugging 

# IDs

## Snowflake IDs

Snowflake is a service used to generate unique IDs for objects within Twitter (Tweets, Direct Messages, Users, Collections, Lists etc.). These IDs are unique 64-bit unsigned integers, which are based on time, instead of being sequential. The full ID is composed of a timestamp, a worker number, and a sequence number.

# Types

## Relational database management systems (RDBS)

## Time Series Database TSDB

A time-series database (TSDB) can be defined simply as a database optimized for storing and using time-stamped or time-series data. You don’t need to use a TSDB to work with time-series data. Any relational or NoSQL database or a key-value-store will do, e.g. MongoDB or redis. However, when dealing with time-series data (e.g. temperature, air pressure or car velocity data), a TSDB makes your life as a developer a hell of a lot easier.


Common time series database use cases
- Accessing IoT data
- Monitoring web services, applications and infrastructure
- Understanding financial trends
- Processing self-driving car data
https://aiven.io/blog/an-introduction-to-time-series-databases

# The CAP theorem



# Tools


DBeaver

Free multi-platform database tool for developers, SQL programmers, database administrators and analysts.
Supports any database which has JDBC driver (which basically means - ANY database). Commercial versions also support non-JDBC datasources such as MongoDB, Cassandra, Couchbase, Redis, BigTable, DynamoDB, etc


https://github.com/dbeaver/dbeaver


# CockroachDB

CockroachDB se basa una arquitectura descentralizada peer to peer (P2P) de nodos simétricos que conforman un cluster, en el que cada uno de ellos tiene exactamente el mismo conjunto de responsabilidades que el resto. Es decir, no hay distintas tipologías de nodos que ejerzan funciones dedicadas, si bien algunos de ellos pueden desempeñar adicionalmente ligeras tareas de coordinación.


La capa SQL se encarga de exponer una API SQL compatible con PostgreSQL con la que interactúan las aplicaciones, convirtiendo dichas sentencias en operaciones clave-valor, que son las que a la postre utiliza internamente la base de datos. Recordar que, tal y como se comentó en la introducción, CockroachDB emplea un sistema de almacenamiento distribuido clave-valor basado basado en RocksDB, una variante de LevelDB.


CockroachDB in detail
https://mikeldeltio.com/2022/01/25/distributed-databases-cockroachdb/