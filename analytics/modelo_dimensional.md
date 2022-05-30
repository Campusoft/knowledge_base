# Dimensional Modeling (modelo dimensional)

Dimensional modeling (dm) is a data structure technique optimized for data storage in a Data warehouse. The purpose of dimensional model is to optimize the database for fast retrieval of data. The concept of Dimensional Modelling was developed by **Ralph Kimball** and consists of "**fact**" and "**dimension**" tables. 

- Modelamiento dimensional. Una técnica para diseñar el modelo lógico de la bodega de datos. Permite alto rendimiento en el momento de acceder a los datos (orientado a consultas)
- Hechos.  algo que ocurre en el tiempo (operación o actividad)
- Medidas. Valores numéricos que describen el hecho que se está analizando.


# Metodología de Kimbal


La elaboración de modelos toma como referencia la Metodología de Kimbal, el cual se especifica que la creación de un modelo dimensional es un proceso dinámico y altamente iterativo. Un esquema general para su creación es:
•	Establecer el nivel de granularidad. La granularidad significa especificar el nivel de detalle. La elección de la granularidad depende de los requerimientos del negocio y lo que es posible a partir de los datos actuales.  La sugerencia general es comenzar a diseñar al mayor nivel de detalle posible, ya que se podría luego realizar agrupamientos al nivel deseado. En caso contrario no sería posible abrir (drill-down) las sumarizaciones en caso de que el nivel de detalle no lo permita. 
•	Elegir las dimensiones. Las dimensiones permiten establecer el contexto de la información, se les puede denominar los filtros o agrupamientos que son permitidos presentar la información en los indicadores. Una forma de identificar las tablas de dimensiones es que sus atributos son posibles candidatos para ser encabezado en los informes, tablas, pivot, etc., o cualquier forma de visualización. 
•	Identificar las tablas de hechos y medidas. El último paso consiste en identificar las medidas que surgen de los procesos de negocios.  Una medida es un atributo (campo) de una tabla que se desea analizar, sumarizando o agrupando sus datos, usando los criterios de corte conocidos como dimensiones. Las medidas habitualmente se vinculan con el nivel de granularidad indicado en los enunciados anteriores. Cada tabla de hechos tiene como atributos una o más medidas de un proceso organizacional, de acuerdo con los requerimientos.  Un registro   contiene   una   medida   expresada   en   números, como   ser   cantidad, tiempo, dinero, etc., sobre la cual se desea realizar una operación de agregación (promedio, conteo, suma, etc.)

A continuación, se presenta un caso referencial, para crear un modelo:
Establecer Nivel Granularidad. En el contexto de UTPL, en el proceso registro de Calificaciones, el nivel gradualidad podría ser totales de calificaciones registradas por paralelo e ítem calificación; considerando su origen y si tiene autorización asociada.
Elegir las dimensiones. En el contexto de UTPL, en el proceso registro de Calificaciones, las dimensiones puede ser periodo académico, nivel académico, modalidad, asignaturas, paralelos, áreas formación, paralelos, etc. 
Tablas Hechos y Medidas. En el contexto de UTPL, en el proceso registrado de Calificaciones, la tabla hechos podría contener las medidas: número estudiantes, numero docentes, totales paralelos con registro calificaciones, totales estudiantes con calificaciones a nivel paralelo e ítem calificación, etc. 



#  Diseño de Modelos

- Utilizar nombres cercanos a los usuarios finales. No replicar convenciones técnicas en los nombres de columnas. (Ejemplo ent_id “técnico”, utilizar persona_id)
- No es recomendable tener una sola tabla plana para toda la información que se requiera en el modelo, para generar los indicadores. Siempre dividir la información descriptiva en las tablas de dimensiones, y los valores en las tablas de hechos. 



# Reference

[What is Dimensional Modeling in Data Warehouse?](https://www.guru99.com/dimensional-model-data-warehouse.html)
  
[¿Qué es y en qué consiste la Analítica Predictiva?](https://www.sistel.es/en-que-consiste-la-analitica-predictiva)

[analítica avanzada](https://decidesoluciones.es/analitica-avanzada/)


Including an excellent section of design tips: http://www.kimballgroup.com/category/design-tips/






# Revisiones

- Data Warehouse
- The Staging Area. Every data warehouse solution should use a staging area where extracted data is stored and possibly transformed before loading the data into the central warehouse.
