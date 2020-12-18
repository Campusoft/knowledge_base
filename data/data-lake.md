# data lake


Un data lake es un repositorio de almacenamiento centralizado que contiene big data de varias fuentes en un formato granular y sin procesar. Puede guardar datos estructurados, semiestructurados o no estructurados, lo que significa que los datos pueden conservarse en un formato más flexible para usarlos en un futuro. Al guardar datos, un data lake los asocia con identificadores y etiquetas de metadatos para poder extraerlos más rápidamente.

El término «data lake» fue acuñado por James Dixon, director tecnológico de Pentaho, y hace referencia a la naturaleza particular de los datos de un data lake, a diferencia de los datos limpios y procesados guardados en los sistemas tradicionales de almacenes de datos.

Los data lakes suelen estar configurados sobre un clúster de hardware de consumo económico y escalable. Esto permite volcar los datos al data lake por si se necesitan más adelante sin tener que preocuparse por la capacidad de almacenamiento. Los clústeres pueden existir localmente o en la cloud.


Ventajas de un data lake

Un data lake funciona a partir de un principio llamado schema-on-read o esquema contra escritura. Esto significa que no existe un esquema predefinido en el que deban encajarse los datos antes de almacenarlos. Tan solo cuando los datos se leen durante el tratamiento se analizan y adaptan en un esquema según convenga. Esta característica ahorra mucho tiempo que normalmente se dedica a la definición del esquema. Esto también permite almacenar datos tal y como estén, en cualquier formato.

Accesibilidad de usuario compleja o simple: como los datos no están organizados en un formato simplificado antes del almacenamiento, un data lake suele requerir un experto que comprenda perfectamente los distintos tipos de datos y sus relaciones para poder leerlos.

Por contra, un almacén de datos es fácilmente accesible tanto para usuarios tecnológicos como inexpertos, debido a su esquema bien definido y documentado. Incluso un nuevo miembro de un equipo puede empezar a utilizar un almacén en muy poco tiempo


Flexibilidad o rigidez: con un almacén de datos no solo se tarda tiempo en definir el esquema en un primer momento, sino que también consume considerables recursos modificarlo cuando los requisitos cambian en un futuro. Sin embargo, los data lakes pueden adaptarse fácilmente a los cambios. Además, a medida que la necesidad de capacidad de almacenamiento aumenta, más fácil resulta escalar los servidores de un clúster de data lakes.


data lake tiene dos componentes: almacenamiento y computación. Tanto el almacenamiento como la computación pueden ubicarse localmente o en la cloud

https://www.talend.com/es/resources/what-is-data-lake/


Un lago de datos puede almacenar grandes volúmenes de datos sin procesar para su uso posterior junto con transformaciones de datos de almacenamiento provisional.

https://docs.microsoft.com/es-es/power-bi/guidance/center-of-excellence-business-intelligence-solution-architecture



ETL / ELT

La ingesta de datos se refiere a la extracción, transformación y carga de datos. O, quizás al revés: extracción, carga y transformación de datos. La diferencia estriba en el lugar donde se produce la transformación. Las transformaciones se aplican para limpiar, ajustar, integrar y estandarizar los datos
 
 