# Data Analysis Expressions - DAX 
 
Data Analysis Expressions (DAX) is a formula language used to create custom calculations in Analysis Services, Power BI, and Power Pivot in Excel. DAX formulas include functions, operators, and values to perform advanced calculations on data in tables and columns


Whether you're using Power BI Desktop, Power Pivot in Excel, or Analysis Services, learning Data Analysis Expressions (DAX) is essential to creating effective data models.

# Context

Understanding the difference between row context and filter context is the first and most important concept to learn to use DAX correctly.


## Row context

## Filter context

## Context transition


# Queries



From SQL to DAX: Projection
This article describes projection functions and techniques in DAX, showing the differences between SELECTCOLUMNS, ADDCOLUMNS, and SUMMARIZE.
https://www.sqlbi.com/articles/from-sql-to-dax-projection/


## Filtros

El Contexto de Filtro en el Lenguaje DAX
https://www.excelfreeblog.com/el-contexto-de-filtro-en-el-lenguaje-dax/


From SQL to DAX: Filtering Data

SQL: 
```
SELECT * 
FROM DimProduct
WHERE Color = 'Red'
```

DAX:
```
EVALUATE
FILTER ( Product, Product[Color] = "Red" )
```
https://www.sqlbi.com/articles/from-sql-to-dax-filtering-data/



#  DAX Studio


## Referencias Dax Studio

Getting Started with DAX Studio
https://exceleratorbi.com.au/getting-started-dax-studio/


# Funciones

## RELATED

Returns a related value from another table. A single value that is related to the current row.

The RELATED function requires that a regular relationship exists between the current table and the table with related information.

https://www.sqlbi.com/articles/using-related-and-relatedtable-in-dax/

## CALCULATE

Evaluates an expression in a context modified by filters.

CALCULATE, with its companion function CALCULATETABLE, is the only function in DAX that can change the filter context. 

it is impossible to properly understand the details of CALCULATE without a proper understanding of the row context, the filter context and the context transition. 

You invoke CALCULATE with an expression as its first argument, and a set of filters starting from the second parameter onwards. 

Función CALCULATE
https://cartasdax.com/calculate/

**CALCULATETABLE**

CALCULATETABLE is identical to CALCULATE, except for the result: it returns a table instead of a scalar value.


## SUMMARIZECOLUMNS

https://dax.guide/summarizecolumns/



From SQL to DAX: Grouping Data
- The GROUP BY condition of a SQL statement is natively implemented by SUMMARIZE in DAX
https://www.sqlbi.com/articles/from-sql-to-dax-grouping-data/

## Filtros

** ALL**
Returns all the rows in a table, or all the values in a column, ignoring any filters that might have been applied.

**FILTER**
Returns a table that has been filtered.
 
https://dax.guide/filter/


Función FILTER
- Informacion spanish
- Posee ilustraccion de los parametros
- Posee ejemplos 
https://cartasdax.com/filter/ 

## Funciones de Contar

- COUNT(<<ColumnName>>)
- COUNTROWS(<<Table>>)
- COUNTA(<<ColumnName>>)
- DISTINCTCOUNT(<<ColumnName>>)

## Funciones de Suma

**SUMX**

Returns the sum of an expression evaluated for each row in a table.

- SUM is the short version of SUMX, when used with one column only
- SUMX is required to evaluate formulas, instead of columns


## Calendarios 

**CALENDAR**

Returns a table with one column of all dates between StartDate and EndDate.

https://dax.guide/calendar/

Ejemplo DAX. Generar una DIM con DAX, utilizando la funcion CALENDAR, las fechas inicio y fin se basan en el origen de datos, de una columna que posee fechas.

```
Dim_Calendar DAX =
ADDCOLUMNS (
	CALENDAR ( DATE( YEAR ( MIN ( fuenteDatos[Date] )), 01, 01), DATE( YEAR( MAX( fuenteDatos[Date] ) ), 12, 31 ) )
)
```


## Time Intelligence

In order to use any time intelligence calculation, you need a well-formed date table. 
https://learn.microsoft.com/es-es/dax/sameperiodlastyear-function-dax#remarks


Time intelligence functions
https://learn.microsoft.com/en-us/dax/time-intelligence-functions-dax


Power BI DAX: SAMEPERIODLASTYEAR, PARALELLPERIOD and DATEADD
https://databear.com/power-bi-dax-sameperiodlastyear-paralellperiod-and-dateadd/

Time Intelligence in Power BI Desktop
- These functions can be divided in two categories:
  - Functions that returns a scalar value without requiring CALCULATE
  - Functions that returns a table, which has to be used as a filter in a CALCULATE statement
https://www.sqlbi.com/articles/time-intelligence-in-power-bi-desktop/


Standard time-related calculations. (Guia completa para diferentes escenarios, acumulados, comparativas con periodos anteriores, etc.)
- Disabling the Auto Date/Time
- Limitations of standard time intelligence functions
https://www.daxpatterns.com/standard-time-related-calculations/


**DATESYTD**
Returns a set of dates in the year up to the last date visible in the filter context.

Ejemplos.
- Ventas acumuladas (mes x mes) de un year.


**TOTALYTD**


**SAMEPERIODLASTYEAR**

Returns a set of dates in the current selection from the previous year.

Devuelve una tabla que contiene una columna de fechas desplazadas un año atrás en el tiempo desde las fechas de la columna dates especificada, en el contexto actual.


**PARALLELPERIOD**

Returns a parallel period of dates by the given set of dates and a specified interval.

## RANKX

## Forecasts 

## Varias

**SELECTCOLUMNS**
Returns a table with selected columns from the table and new columns specified by the DAX expressions.

https://dax.guide/selectcolumns/

# Power Pivot para Excel


Iniciar el complemento Power Pivot para Excel

https://support.microsoft.com/es-es/office/iniciar-el-complemento-power-pivot-para-excel-a891a66d-36e3-43fc-81e8-fc4798f39ea8




# Referencias


The curated content of DAX Guide makes it a go-to reference on the DAX language.
https://dax.guide/

videos dax y otros powerBi
https://www.youtube.com/c/datdata/videos


**dax.do**
Code, run, and share DAX
Write your DAX queries, try them and share them easily with DAX.do
https://dax.do/


## Escenarios

New and returning customers
- The New and returning customers pattern helps in understanding how many customers in a period are new, returning, lost, or recovered. There are several variations to this pattern, each with different performance and results depending on the requirements. 
https://www.daxpatterns.com/new-and-returning-customers/


