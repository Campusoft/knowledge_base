# Data Warehouse


# Data Profiling. Perfilado de datos

Data profiling is the process of collecting statistics and other information about the data available in different source systems. The obtained information is invaluable for the further design of your data warehouse and ETL processes. Data profiling is also an important part of any data quality initiative; before quality can be improved, a baseline has to be established indicating what the current state of the data is. Profiling can be performed at three different levels:

Column profile—Collects statistics about the data in a single column
Dependency profile—Checks for dependencies within a table between different columns
Join profile—Checks for dependencies between different tables

Herramientas
•	tool named DataCleaner developed by the open source community eobjects.org. The software can be obtained from http://datacleaner .eobjects.org and is very easy to install. On Windows, you simply unzip the package and start datacleaner.exe.

•	Talend Open Studio for Data Quality

# OLAP

The idea of an OLAP database is to use an optimized storage format for analyzing data in a multi-dimensional format to offer the user flexibility and very fast access. The speed offered by OLAP databases is caused by the fact that most totals and subtotals (a.k.a. aggregations) are precalculated and stored in the OLAP cube. Although an OLAP cube can have many more than three dimensions, an OLAP database is often visualized as a Rubik’s cube, hence the name cube.

OLAP (Online Analytical Processing) Procesamiento analitico en linea tiene como objetivo agilizar la consulta de grandes volumenes de informacion. Para ello utiliza estructura multidimensionales, conocidas como cubos OLAP, que contienen datos pre calculados y agregados.
Un cubo OLAP estra estructurado en dimensiones, que son las diferentes perspectivas desde las que queremos analizar la informacion, y en medidas, que son los diferentes hechos con valores concretos que solicita el usuario
MDX (MultiDimensional eXpressions) El MDX es un lenguaje de consulta creado especificamente para hacer consultas sobre cubos OLAP.


MOLAP (Multidimensional OLAP)—The original OLAP format in which the data is stored in a proprietary multidimensional format. All detail data and aggregates are stored inside the cube file. A good example of an open source MOLAP database is PALO, developed by the German company Jedox.
ROLAP (Relational OLAP)—In this case, the data and all aggregates are stored in a standard relational database. The ROLAP engine translates multidimensional queries into optimized SQL and usually adds caching capabilities as well to speed up subsequent analytical queries. Pentaho Mondrian is a perfect example of a ROLAP engine.
HOLAP (Hybrid OLAP)—In HOLAP, aggregates and navigational data are stored in a MOLAP structure but detailed data is kept in the relational database. To date, there is no open source HOLAP solution available, but some of the advantages have been incorporated in Mondrian with the addition of automatically generated aggregate tables to speed up queries.

Multi-Dimensional Databases
•	SAP BW
•	Microsoft Analysis Services
•	Hyperion EssBase

Mondrian
Microsoft SQL Server Analysis

# Data Mining

# The Staging Area

Every data warehouse solution should use a staging area where extracted data is stored and possibly transformed before loading the data into the central warehouse.

Regla de oro

“Staging = lo mínimo indispensable para que el ETL posterior pueda limpiar y transformar sin mirar hacia atrás.”


Las tablas de aterrizaje (también conocidas como landing tables, staging tables o tablas de staging) son un componente clave en la arquitectura de un data warehouse (almacén de datos). Se utilizan como una capa temporal de almacenamiento durante el proceso de integración de datos, especialmente en el marco del proceso ETL (Extract, Transform, Load) o ELT (Extract, Load, Transform).

¿Qué son las tablas de aterrizaje?

Las tablas de aterrizaje son estructuras de base de datos donde se cargan los datos tal como vienen de las fuentes, sin aplicar transformaciones ni validaciones complejas. Actúan como una zona de recepción o "área de aterrizaje" para los datos antes de que sean procesados y cargados al data warehouse final.

Características principales
Datos en bruto (raw data):

- Se almacenan los datos exactamente como se extraen de las fuentes (bases de datos operativas, archivos CSV, APIs, etc.).
- No se limpian, normalizan ni transforman.

Temporalidad:
- Son tablas temporales. Una vez que los datos han sido procesados y cargados al data warehouse, suelen eliminarse o archivarse.

Estructura flexible:
- Pueden tener columnas adicionales para auditoría (fecha de carga, origen del dato, estado del proceso, etc.).
-  A menudo se usan tipos de datos genéricos (como VARCHAR) para manejar cualquier formato entrante.

Desacoplamiento:
- Permiten desacoplar el sistema fuente del data warehouse. Si hay un problema en el proceso de transformación, los datos originales siguen disponibles.

Facilitan la trazabilidad y el re-procesamiento:
- Si ocurre un error en el ETL, puedes volver a procesar los datos desde la tabla de aterrizaje sin necesidad de volver a extraer de la fuente.


Ejemplo de flujo con tablas de aterrizaje

Extracción (Extract):
- Se extraen datos de una base de datos de ventas (por ejemplo, ventas_brutas.csv).
Carga a tabla de aterrizaje:
- Se cargan los datos directamente en una tabla como stg_ventas_brutas.
Transformación (Transform):
- Se limpian, validan, convierten formatos, se unifican con otras fuentes, etc.
Carga al data warehouse (Load):
- Los datos transformados se cargan a tablas dimensionales o hechos, como fact_ventas.


# Tools

Ptyhon
An open source and collaborative framework for extracting the data you need from websites.
In a fast, simple, yet extensible way.
https://scrapy.org/

# Metodogias

## DMBOK



