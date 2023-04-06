# Data Analysis Expressions - DAX 
 
Data Analysis Expressions (DAX) is a formula language used to create custom calculations in Analysis Services, Power BI, and Power Pivot in Excel. DAX formulas include functions, operators, and values to perform advanced calculations on data in tables and columns


Whether you're using Power BI Desktop, Power Pivot in Excel, or Analysis Services, learning Data Analysis Expressions (DAX) is essential to creating effective data models.

# Context

Understanding the difference between row context and filter context is the first and most important concept to learn to use DAX correctly.


DAX 101: Filter context in DAX
https://www.sqlbi.com/articles/filter-context-in-dax/



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



**KEEPFILTERS**



#  DAX Studio


## Referencias Dax Studio

Getting Started with DAX Studio
https://exceleratorbi.com.au/getting-started-dax-studio/



# Variables


DAX 101: Variables in DAX
https://www.sqlbi.com/articles/variables-in-dax/


# Funciones

## Condicionantes



**IF**

Checks whether a condition is met, and returns one value if TRUE, and another value if FALSE.

```
IF(<logical_test>, <value_if_true>[, <value_if_false>])
```



## RELATED

Returns a related value from another table. A single value that is related to the current row.

The RELATED function requires that a regular relationship exists between the current table and the table with related information.

https://www.sqlbi.com/articles/using-related-and-relatedtable-in-dax/

## CALCULATE

Evaluates an expression in a context modified by filters.

CALCULATE, with its companion function CALCULATETABLE, is the only function in DAX that can change the filter context. 

it is impossible to properly understand the details of CALCULATE without a proper understanding of the row context, the filter context and the context transition. 

You invoke CALCULATE with an expression as its first argument, and a set of filters starting from the second parameter onwards. 



```
CALCULATE(<expression>[, <filter1> [, <filter2> [, …]]])
```

Para cada expresión de filtro hay dos posibles resultados estándar:

- Si las columnas (o tablas) no están en el contexto de filtro, para evaluar la expresión se agregarán filtros nuevos al contexto de filtro.
- Si las columnas (o tablas) ya están en el contexto de filtro, para evaluar la expresión CALCULATE los filtros nuevos sobrescribirán los existentes.

https://learn.microsoft.com/es-es/dax/calculate-function-dax#remarks



Función CALCULATE
https://cartasdax.com/calculate/


Introducing CALCULATE in DAX
- The evaluation contexts and CALCULATE are the foundation of the entire DAX language 
https://www.sqlbi.com/articles/introducing-calculate-in-dax/






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



Power Bi Filter Functions with Examples
- Using Power Bi All Function
- Using Power Bi ALLEXCEPT Function
https://www.enjoysharepoint.com/power-bi-filter-functions/


Power BI —All the important filters you need to know
- KEEPFILTER
- REMOVEFILTERS
- ALLEXCEPT
- ALL
- ALLSELECTED
https://towardsdatascience.com/power-bi-all-the-important-filters-you-need-to-know-583fec7c0f8c

## Funciones de Contar

- COUNT(<<ColumnName>>)
- COUNTROWS(<<Table>>)
- COUNTA(<<ColumnName>>). Cuenta el número de filas de la columna especificada que contienen valores que no están en blanco.
- DISTINCTCOUNT(<<ColumnName>>)

## Funciones de Suma

**SUMX**

Returns the sum of an expression evaluated for each row in a table.

- SUM is the short version of SUMX, when used with one column only
- SUMX is required to evaluate formulas, instead of columns

```
SUMX(<table>, <expression>)  
```

## Calendarios 

**CALENDAR**

Returns a table with one column of all dates between StartDate and EndDate.

https://dax.guide/calendar/

Ejemplo DAX. Generar una DIM con DAX, utilizando la funcion CALENDAR, las fechas inicio y fin se basan en el origen de datos, de una columna que posee fechas.

```
Dim_Calendar DAX =
ADDCOLUMNS (
	CALENDAR ( DATE( YEAR ( MIN ( fuenteDatos[Date] )), 01, 01), DATE( YEAR( MAX( fuenteDatos[Date] ) ), 12, 31 ) )
	, "Month Number", MONTH ( [Date] )
    , "YEAR", YEAR(  [Date] )
)
```


## Time Intelligence


The date dimension that is acceptable by time intelligence functions of DAX should;

- have one record per day
- start from the minimum date in the date field or before that, and ends at the maximum date in the date field or later than that.
- have no date missing (if there are no sales in the 1st of January, still that date should be in this table. This is one of the reasons why you do need to have a separate date table)



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
- Building a Date table
- Naming convention
https://www.daxpatterns.com/standard-time-related-calculations/



Comparing different time periods 
- the model must contain two date tables
- use USERELATIONSHIP 
https://www.daxpatterns.com/comparing-different-time-periods/
 

**SAMEPERIODLASTYEAR**

Returns a set of dates in the current selection from the previous year.

Devuelve una tabla que contiene una columna de fechas desplazadas un año atrás en el tiempo desde las fechas de la columna dates especificada, en el contexto actual.


Errores
-------------------

Calculation error in measure 'Medidas'[Total Creditos Anio Anterior]: A date column containing duplicate dates was specified in the call to function 'SAMEPERIODLASTYEAR'. This is not supported.

-------------------


**DATEADD**

Moves the given set of dates by a specified interval.

- DateAdd y SamePeriodLastYear son funciones que navegan en un periodo dinámico a través del contexto del filtro (Filter Context); con la diferencia que DateAdd puede retornar más de un año atrás.


**DATESYTD**

Returns a set of dates in the year up to the last date visible in the filter context.

Ejemplos.
- Ventas acumuladas (mes x mes) de un year.


**TOTALYTD**




**PARALLELPERIOD**

Returns a parallel period of dates by the given set of dates and a specified interval.

**Otras**

PREVIOUSMONTH


## Matematicas

**FIXED**

Rounds a number to the specified number of decimals and returns the result as text with optional commas.



**ROUND**

Rounds a number to a specified number of digits.

## RANKX


## Parameters

Generating a series of numbers in DAX
This article describes how to create a table with a series of numbers in DAX by using the new GENERATESERIES function or through a workaround using CALENDAR. 
https://www.sqlbi.com/articles/generating-a-series-of-numbers-in-dax/


Parameter table
- Changing the scale of a measure
- Multiple independent parameters
- Multiple dependent parameters
- Selecting top N products dynamically
https://www.daxpatterns.com/parameter-table/



Considerations and limitations

There are a couple considerations and limitations for parameters to keep in mind:

- Parameters can only be used with value ranges between 0 and 1,000. For ranges greater than 1,000, the parameter value will be sampled.
- Parameters are designed for measures within visuals, and might not calculate properly when used in a dimension calculation.
https://learn.microsoft.com/en-us/power-bi/transform-model/desktop-what-if#considerations-and-limitations


Restricciones de la cantidad de valores en parametros.
https://whatthefact.bi/power-bi/data-model/why-is-it-sometimes-not-possible-to-select-the-exact-number-in-the-what-if-parameter-and-how-can-this-be-solved/


**Field Parameters**

The same functionality was achievable using the SWITCH() DAX function, but that took both additional time and expertise. The new Field Parameters feature does not require much time or expertise to implement. 



What are Field Parameters in Power BI?
- What are Field Parameters? 
- Why are Field Parameters Important? 
- How to Use Field Parameters 
- Field Parameter Example Use Case 
https://www.phdata.io/blog/what-are-field-parameters-in-power-bi/



Fields parameters in Power BI
- 
https://www.sqlbi.com/articles/fields-parameters-in-power-bi/



**GENERATESERIES**
Returns a table with one column, populated with sequential values from start to end.


## Forecasts 

## Relationship

DAX 101: Using USERELATIONSHIP in DAX
- USERELATIONSHIP function in DAX to change the active relationship in a CALCULATE function. 
- If two tables are linked by more than one relationship, you can decide which relationship to activate by using USERELATIONSHIP. 
https://www.sqlbi.com/articles/using-userelationship-in-dax/

## Varias

**SELECTCOLUMNS**
Returns a table with selected columns from the table and new columns specified by the DAX expressions.

https://dax.guide/selectcolumns/


**WINDOW**

Así se hace para generar acumulados con la función WINDOW 
https://www.youtube.com/watch?v=wsI4491-qPo


**SELECTEDVALUE**

The SELECTEDVALUE and VALUES functions read the current filter context, not the row context;

Using the SELECTEDVALUE function in DAX
https://www.sqlbi.com/articles/using-the-selectedvalue-function-in-dax/


**EOMONTH**

Returns the date in datetime format of the last day of the month before or after a specified number of months.
https://dax.guide/eomonth/


**CROSSJOIN**


# Power Pivot para Excel


Iniciar el complemento Power Pivot para Excel

https://support.microsoft.com/es-es/office/iniciar-el-complemento-power-pivot-para-excel-a891a66d-36e3-43fc-81e8-fc4798f39ea8


# Best Practices


DAX Best Practice Guide
- Last Updated: December 26, 2022 
https://maqsoftware.com/insights/dax-best-practices

Analyzing a slow report query in DAX Studio  
https://www.youtube.com/watch?v=C5HBhlLUFsE


# Performance

Optimizing IF conditions by using variables
- This article describes a very common optimization pattern that relies on variables to optimize conditional expressions in DAX. 
https://www.sqlbi.com/articles/optimizing-if-conditions-using-variables/


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




Create Customized Age Bins (or Groups) in Power BI
- Power Query Conditional Column
https://radacad.com/create-customized-age-bins-or-groups-in-power-bi