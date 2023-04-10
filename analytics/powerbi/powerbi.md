# Power BI


# Caracteristicas

- Tooltips
- Cross-highligth. Cross-filter
- Drill Down. Drill Through
- Sort visuals & tables
- Filter pages & visuals
  - a) Filter a report page. Filter a report page. Filters that you apply at a page level affect all the visuals on that page.
  - b) Filter a visual. Selecting a visual lets you apply filters just to that visual.


# Conceptos 

- Una visualización (u objeto visual) es un tipo de gráfico que han generado diseñadores de Power BI
- Un conjunto de datos es un contenedor de datos.
- Un panel es una pantalla única con iconos de objetos visuales, texto y gráficos interactivos. Un panel recopila las métricas más importantes, en una pantalla, para contar una historia o responder a una pregunta. El contenido del panel proviene de uno o varios informes y uno o varios conjuntos de datos.
- Un informe es una o varias páginas de gráficos, texto y objetos visuales interactivos que forman un único informe. Power BI basa un informe en un único conjunto de datos. A menudo, el diseñador organiza las páginas de un informe para abordar un área central de interés o para responder a una única pregunta.
- Una aplicación es un recurso de los diseñadores que sirve para agrupar y compartir paneles, informes y conjuntos de datos relacionados entre sí.

Conceptos básicos para usuarios profesionales del servicio Power BI
https://learn.microsoft.com/es-es/power-bi/consumer/end-user-basic-concepts

## Conjuntos de datos

Un conjunto de datos es una colección de datos que los diseñadores importan o a la cual se conectan y, a continuación, usan para crear informes y paneles.

## Informes

Un informe de Power BI se compone de una o más páginas de visualizaciones, gráficos y texto. Todas las visualizaciones de un informe proceden de un único conjunto de datos.

## Paneles

Un panel representa una vista gráfica personalizada de un subconjunto de los conjuntos de datos subyacentes. 

## Visualizaciones

Las visualizaciones (también conocidas como objetos visuales) muestran la información que Power BI descubre en los datos. Facilitan la interpretación de la información, ya que su cerebro puede entender una imagen más rápidamente que como entendería una hoja de cálculo de números.

## Aplicaciones

Estas colecciones de paneles e informes organizan contenido relacionado entre sí en un único paquete. Los diseñadores de Power BI las crean en las áreas de trabajo y comparten aplicaciones con personas, grupos, organizaciones completas o el público. Como usuario profesional, puede estar seguro de que usted y sus compañeros trabajan con la misma información; una única versión de confianza del contenido real.


# Modelamiento

What a Power BI Hierarchy Is, and How to Use it?
- Power BI hierarchies are a useful structure for drill-down and data exploration in reports
- Model hierarchy or Visual hierarchy?
https://radacad.com/what-a-power-bi-hierarchy-is-and-how-to-use-it



**DIM Calendarios**

PowerBI genera una DIM automaticamente por cada campo tipo Fecha. 

Apply auto date/time in Power BI Desktop
- The Auto date/time is a data load option in Power BI Desktop. The purpose of this option is to support convenient time intelligence reporting based on date columns loaded into a model. Specifically, it allows report authors using your data model to filter, group, and drill down by using calendar time periods (years, quarters, months, and days).
https://learn.microsoft.com/en-us/power-bi/transform-model/desktop-auto-date-time


**Model relationships**


Power Bi Relationship Functions
https://www.enjoysharepoint.com/power-bi-relationship-functions/

**what-if**

El uso de parámetros what-if ofrece la capacidad de transformar dinámicamente los datos, es decir, permite simular cómo los datos cambian en distintos escenarios. 


Creación y uso de parámetros what-if para visualizar variables en Power BI Desktop

https://learn.microsoft.com/es-es/power-bi/transform-model/desktop-what-if


# Columns 


Power BI Add Calculated Column [With Various Examples]
https://www.spguides.com/power-bi-add-calculated-column/



# Medidas(measures)

Measures vs Calculated Columns in DAX and Power BI
https://endjin.com/blog/2022/04/measures-vs-calculated-columns-in-dax



Calculated Columns and Measures in DAX
One of the first concepts to learn in DAX is the difference between calculated columns and measures. This article shortly recaps the differences and describes when to use each one. 
- The DAX expression defined for a calculated column operates in the context of the current row across that table. Any reference to a column returns the value of that column for the current row. You cannot directly access the values of other rows.
- One important concept that you need to remember about calculated columns is that they are computed during the database processing and then stored in the model
https://www.sqlbi.com/articles/calculated-columns-and-measures-in-dax/



Creating a Measures Table in Power BI
- A measures table is essentially just a placeholder table that acts as a folder for your measures.
https://www.phdata.io/blog/creating-a-measures-table-in-power-bi/

# Visualizaciones / Visualizations 


**Tipos Visualizaciones**

8 tipos de gráficos para la visualización de datos en Power BI
- Gráfico lineal
- Gráfico de columnas
- Gráfico combinado
- Mapas de calor y mapas de puntos
- Gráficos de dispersión y gráficos de burbujas
- Gráficos de cascadas y embudos
- Gráficos circulares y gráficos de anillos
- Gráfico con filtros
https://blog.aitana.es/2018/09/12/tipos-graficos-power-bi/

**Slicer**


How to get selected value from Slicer in Power BI
- Power BI get selected value from slicer
- Power BI get selected value from date slicer
- Power BI get multiple selected value from slicer
- Power BI get first selected value from slicer
- Power BI get max selected value from slicer
- Power BI get selected value from between slicer
- Power BI get number of selected values from slicer
https://www.enjoysharepoint.com/power-bi-get-selected-value-from-slicer/


**Matrix** 


**Radial gauge**


Radial gauge charts in Power BI
- When to use a radial gauge
- Create a basic radial gauge
- Use manual format options to set Minimum, Maximum, and Target values
https://learn.microsoft.com/en-us/power-bi/visuals/power-bi-visualization-radial-gauge-charts?tabs=powerbi-desktop



Gauge Visual Colours in Power BI
- DAX Solution
https://www.dearwatson.net.au/blog/gauge-visual-colours-in-power-bi/


Power bi gauge chart – How to use with examples
- When to use Gauge chart in power bi?
- How to create a power bi gauge chart?
- Power bi gauge chart conditional formatting
- Power bi gauge chart percentage
- Power bi gauge chart multiple values
- Power bi gauge chart background-color
- Power bi gauge chart color (dynamically)
- Power bi gauge chart with multiple targets
https://www.enjoysharepoint.com/power-bi-gauge-chart/

**Key Influencers**

Influenciadores Clave


Internamente esta visualización utiliza algoritmos de Machine Learning (ML) y en la documentación nos dicen que utiliza la biblioteca de código abierto ML.NET.


Creación de visualizaciones de influenciadores clave
- Posee datos, en archivo powerBI.
- Presenta ejemplos de Principal factor único que influye en la probabilidad de una calificación baja
https://learn.microsoft.com/es-es/power-bi/visuals/power-bi-visualization-influencers?tabs=powerbi-desktop



Articulo, de utilizacion del control visual, con datos de Juego de Tronos.
- Analizando con Influyentes Clave las muertes de Juego de Tronos
https://vandalytic.com/tutorial-power-bi-influenciadores-clave-aplicado-a-las-muertes-de-juego-de-tronos/




**Waterfall charts**


Gráfico de cascada

Waterfall charts are a great choice:

- When you have changes for the measure across time, a series, or different categories.
- To audit the major changes contributing to the total value.
- To plot your company's annual profit by showing various sources of revenue and arrive at the total profit (or loss).
- To illustrate the beginning and the ending headcount for your company in a year.
- To visualize how much money you make and spend each month, and the running balance for your account.


Waterfall charts in Power BI
- When to use a waterfall chart
- Create a waterfall chart
- Sort the waterfall chart
- Breakdown bucket
https://learn.microsoft.com/en-us/power-bi/visuals/power-bi-visualization-waterfall-charts?tabs=powerbi-desktop


Qué es y como se crea un Waterfall Chart o Gráfico Cascada
- Ejemplo en excel detallado
https://www.excelfreeblog.com/que-es-y-como-se-crea-un-waterfall-chart-o-grafico-cascada/


Power BI Waterfall Charts: A Detailed Guide
- What is a waterfall chart?
- What's the difference between a waterfall chart and a bar chart?
- When should you use a waterfall chart?
- 7 tips for creating a waterfall chart in Power B
  - 1. Use colors to tell a story
  - 2. Add connection lines
  - 3. Create and customize labels
  - 4. Set target with thresholds
  - 5. Define the sequence
  - 6. Choose Sub-Total Mode
https://zoomcharts.com/en/microsoft-power-bi-custom-visuals/blog/power-bi-waterfall-charts-a-detailed-guide


**KPI**

Power BI KPI
https://www.wallstreetmojo.com/power-bi-kpi/


Use a KPI in a table within Power BI 
- In this video, Adam shows you how you can import KPIs from Excel into Power BI. Once in Power BI, you can use the KPI in a table or matrix.
https://www.youtube.com/watch?v=3_bLEIAhaDw



![image](https://user-images.githubusercontent.com/222181/215827928-3cb818ec-bef4-44b3-99ca-5d9b1b577b13.png)

The measure symbol indicates that it is a KPI which had been created in either Power pivot or SSAS and then imported to Power bi which will show up as it looks in the picture (though not always)



**Show Values**

Power bi show value as percentage + 13 Examples
- Power bi show value as percentage
- Power bi show value as percentage format
- Power bi show value as percentage of row total
- Power bi show value as percentage of column total
- Power bi show value as percentage in matrix
- Power bi show value as percentage of grand total
- Power bi show value as percentage of total
- Power bi show value as percentage of subtotal
- Power bi show card value as percentage
- Power bi show value as percentage no decimal
- Power bi measure percentage column total
- Power bi measure percentage of total with filter
- Power bi measure percentage of group
https://www.enjoysharepoint.com/power-bi-show-value-as-percentage/




# Themes


With Power BI Desktop report themes, you can apply design changes to your entire report, such as using corporate colors, changing icon sets, or applying new default visual formatting. When you apply a report theme, all visuals in your report use the colors and formatting from your selected theme as their defaults. A few exceptions are described later in this article.


5 Great Places To Download Templates

Numerro a Microsoft partner makes it possible for you to implement design best practices and store your building reports. With an archive of numerous dashboards, including those for sales, marketing, social media, human resources, and logistics.
https://www.numerro.io/


# Query Editor



Create Customized Age Bins (or Groups) in Power BI
- Power Query Conditional Column
https://radacad.com/create-customized-age-bins-or-groups-in-power-bi

## Power Query M



**Valor de tabla**
- #table para construir una tabla a partir de una lista de listas de filas y una lista de nombres de encabezado

Crear una tabla con dos columnas y tres filas
```
#table({"columna 1", "columna 2"}, {{1,1}, {2,4}, {3,9}})
```

https://learn.microsoft.com/es-es/powerquery-m/m-spec-values#table


**Revisiones**

- La opcion de transformacion, posee opciones para ver distribuccion y calidad en las columnas. (Esta informacion se basa en 1000 registros)


# Licencias

Power BI Pro and Power BI Premium


Comparacion de caracteristicas por licencia.
https://powerbi.microsoft.com/en-us/pricing/#features-compare-charts


Power BI Premium
- Incremental refresh is supported for Power BI Premium, Premium per user, Power BI Pro, and Power BI Embedded datasets.
https://learn.microsoft.com/en-us/power-bi/connect-data/incremental-refresh-overview


Power BI Premium
- Hybrid tables. Hybrid tables combine the performance of VertiPaq in-memory caches with the capabilities of DirectQuery, allowing users to unlock massive datasets for real-time, interactive analysis.
https://learn.microsoft.com/en-us/power-platform-release-plan/2021wave2/power-bi/hybrid-tables


Power BI Premium features
https://learn.microsoft.com/en-us/power-bi/enterprise/service-premium-features


Power BI Licensing Ultimate Guide
- Power BI Free
- Power BI Pro
- Power BI Premium Per User
- Power BI Embedded
- Power BI Premium
- Power BI Report Server Only licensing
https://radacad.com/power-bi-licensing-walk-through-guide


# Performance


Introducing the Power BI Performance Analyzer
https://www.sqlbi.com/articles/introducing-the-power-bi-performance-analyzer/


# Workspaces 

How to organize workspaces in a Power BI environment?
https://radacad.com/how-to-organize-workspaces-in-a-power-bi-environment

Power BI Workspace; Collaborative DEV Environment
https://radacad.com/groups-in-power-bi

Best Practice for Power BI Workspace Roles Setup
https://radacad.com/best-practice-for-power-bi-workspace-roles-setup



# Power BI gateway

Where are the Power BI Gateway logs? I thought I knew... 
https://www.youtube.com/watch?v=H6sLCVanIYE
 
# Publish


Power BI Sharing Methods Comparison – All in One Review
- Basic Sharing
- Workspace
- Power BI App
- Publish to Web
- Embed in SharePoint online
- Power BI Embedded
- Secure Embed
https://radacad.com/power-bi-sharing-methods-comparison-all-in-one-review
 

# Power Bi embedded
 

# Power BI Report Builder

Power BI Report Builder is a tool for authoring paginated reports that you can publish to the Power BI service. Paginated reports are designed to be printed or shared. 
 
# Training



# Referencias

Power BI Governance, Good Practices, Part 2: Version Control with OneDrive, Teams and SharePoint Online
https://www.biinsight.com/power-bi-governance-good-practices-part-2-version-control-with-onedrive-teams-and-sharepoint-online/

Whitepapers for Power BI
https://docs.microsoft.com/en-us/power-bi/guidance/whitepapers



Blog comunitario de la suite de analítica de Microsoft de la mano de Power Platform y Azure Data Platform.  (Tiene algunos articulos interesantes)
https://blog.ladataweb.com.ar/


Diferentes reportes de Power BI. Referencias
- Análisis población países.pbix 
- Análisis empleados
- Proyecto finanzas
https://github.com/DaniMonsalve/PowerBI.pbix-


## Recursos de aprendizaje Oficiales 

- Documentación Power BI - https://docs.microsoft.com/es-es/power-bi/
- Introducción a Power BI Desktop - https://docs.microsoft.com/es-es/power-bi/fundamentals/desktop-getting-started
- Aprendizaje Autodirigiado - https://docs.microsoft.com/es-es/power-bi/guided-learning/
- Ejemplos Power BI - https://docs.microsoft.com/es-es/power-bi/create-reports/sample-datasets

## Community Tools

Following are some of the tools contributed by the vibrant Power BI user community. These are independent tools not supported by Microsoft, some of which are free and others are paid products.

DAX Studio
DAX Studio is a client tool for executing and troubleshooting DAX queries. 
Contributed and supported by: SQLBI
http://daxstudio.org/ 

DAX Formatter
The DAX Formatter tool improves the readability of DAX.
Contributed and supported by: SQLBI
http://www.daxformatter.com/ 




Bravo 
- Analyze Model
- Format DAX
- Manage Dates
- Export Data. Use Bravo to easily export your data model to Excel spreadsheets or CSV files.
- Documentación: https://docs.sqlbi.com/bravo/
https://www.sqlbi.eu/

Lingo
Lingo is a web-based code editor for editing the linguistic schema in Power BI, for the purpose of improving how the natural language Q&A functionality.
Contributed and supported by: Power BI Tips
https://powerbi.tips/2018/04/introducing-lingo/ 

Report Theme Generator
The Report Theme Generator is a web-based tool which allows a user to specify properties and download into a JSON file to be used in a Power BI theme.
Contributed and supported by: Power BI Tips
https://powerbi.tips/tools/report-theme-generator-v3/ 

Power BI Helper
The Power BI Helper aids in removing unused data elements from a model, finding dependencies, and in documenting a model.  
Contributed and supported by: RADACAD
http://radacad.com/power-bi-helper 

Power BI Documenter
Power BI Documenter is a tool which auto-generates documentation from Power BI Desktop, for the purpose of identifying and documenting data usage, visuals, and development practices.
Contributed and supported by: Data Vizioner
https://www.datavizioner.com/resources/how-to-use-power-bi-documenter


Power Update
Power Update is a tool which moves scheduled refresh operations for datasets in Power BI Desktop or Excel to a local machine.
Contributed and supported by: Power On BI
http://poweronbi.com/power-update/  

Power BI Visual Planning
The Visual Planning tool allows for users to edit data directly in a report or dashboard. 
Contributed and supported by: Power On BI
http://poweronbi.com/powerbi-visual-planning/ 

Power Pivot Utilities
Power Pivot Utilities is an add-in for Excel. It aids with documenting models, relationships, calculated columns, unused columns, and memory usage via a set of VBA macros.
Contributed and supported by: Bertrand d’Arbonneau
https://www.sqlbi.com/tools/power-pivot-utilities/ 

Turbo.net Power BI Desktop Application
The Turbo.net tools allow applications to run on any desktop. Their version allows Power BI Desktop to run on an Apple Mac.
Contributed and supported by: Turbo.net
https://turbo.net/run/powerbi/powerbi

 

## Indicadores referencias


https://github.com/DaniMonsalve/PowerBI.pbix- 


Dashboard Financiero con Power BI
- Posee los archivos PowerBI.
- Utiliza un fondo (imagen), para mejorar el diseno.
https://www.youtube.com/watch?v=aeUA-otL220&t=92s



## Examples

15 Power BI Projects Examples and Ideas for Practice
- This blog lists 15 Microsoft Power BI projects for you. We have categorized these Power BI examples into beginner, intermediate, and advanced levels. You can choose any of these power bi projects for practice to upskill yourself in the Data Science domain. 
https://www.projectpro.io/article/power-bi-microsoft-projects-examples-and-ideas-for-practice/533



Retail Analysis Sample PBIX
The Retail Analysis built-in sample contains a dashboard, report, and dataset that analyzes retail sales data of items sold across multiple stores and districts. The metrics compare this year's performance to last year's for sales, units, gross margin, and variance, as well as new-store analysis.
https://learn.microsoft.com/en-us/power-bi/create-reports/sample-retail-analysis


## Conjuntos de Datos para prácticas

- Datos COVID Microsoft - https://github.com/microsoft/Bing-COVID-19-Data/tree/master/data
- Datos COVID OMS - https://covid19.who.int/WHO-COVID-19-global-data.csv
 

## Cuando usar tipos de gráficos


How and When to Use 7 of the Most Popular Chart Types for Your Survey
- limesurvey
https://www.limesurvey.org/en/blog/20-blog/107-how-and-when-to-use-7-of-the-most-popular-chart-types


https://www.dummies.com/software/microsoft-office/excel/10-excel-chart-types-and-when-to-use-them/

Graphs and Charts
- Bar graphs to show numbers that are independent of each other. Example data might include things like the number of people who preferred each of Chinese takeaways, Indian takeaways and fish and chips.
- Pie charts to show you how a whole is divided into different parts. You might, for example, want to show how a budget had been spent on different items in a particular year.
- Line graphs show you how numbers have changed over time. They are used when you have data that are connected, and to show trends, for example, average night-time temperature in each month of the year.
- Cartesian graphs have numbers on both axes, which therefore allow you to show how changes in one thing affect another. These are widely used in mathematics, and particularly in algebra.
https://www.skillsyouneed.com/num/graphs-charts.html

14 Best Types of Charts and Graphs for Data Visualization [+ Guide]
- 5 Questions to Ask When Deciding Which Type of Chart to Use
  - 1. Do you want to compare values?
  - 2. Do you want to show the composition of something?
  - 3. Do you want to understand the distribution of your data?
  - 4. Are you interested in analyzing trends in your data set?
  - 5. Do you want to better understand the relationship between value sets?
https://blog.hubspot.com/marketing/types-of-graphs-for-data-visualization


Data Visualization 101: How to Choose a Chart Type
https://towardsdatascience.com/data-visualization-101-how-to-choose-a-chart-type-9b8830e558d6
 

## layouts

Free Power BI Templates Download
- Fondo gris, barra superior oscura (negra)
https://www.collaboris.com/downloadable-backgrounds-for-power-bi/


Layouts – Purple Haze
https://powerbi.tips/product/layouts-purple-haze/



Simply Modern Light
- This free to download package include .JSON file (Power BI theme).
https://metricalist.com/powerbi-solutions/simply-modern-light/

# Revisiones

Integrar Power BI con Synapse Analytics



Power BI — How to Fit 200 Million Rows in Less than 1GB
https://towardsdatascience.com/power-bi-how-to-fit-200-million-rows-in-less-than-1gb-63ab5a4eb1c0


aggregation table
