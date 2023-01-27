# Business Intelligence (BI) - Inteligencia Negocios

Business Intelligence (BI). Business Analytics. The Cloud. On-Premises. Artificial intelligence (AI). Machine learning (ML). Self-service analytics. Augmented Analytics. All of these concepts are important to understand


# Dashboard

El dashboard o también conocido como panel de información o panel de gestión, es una interfaz gráfica que ayuda a los usuarios a visualizar indicadores clave de desempeño o KPI y métricas para la toma de decisiones que llevarán a la empresa a lograr sus objetivos. Por esta razón, muchos profesionales lo ven como un informe de progreso.

Básicamente, reúne una gran cantidad de datos que se encuentran disponibles en la organización, transformándolos en KPI, con tablas y gráficos.

Existe tres tipos comunes de dashboards
- Operacional
- Estrategico / Ejecutivo
- Analitico

**Dashboard Operativos**

Enfatizan el monitoreo mas que el analisis y la administracion. Este tipo muestra datos que facilitan la parte operativa de un negocio. Permite realizar el seguimiento de la situacion de procesos y/o sectores de la organizacional, al menos de forma diaria. 

Operational dashboards provide time-critical data to consumers. I like to think about operational dashboards as of cockpit in the car or plan.


**Dashboard Tacticos**

On  the other hand, analytical dashboards focus more on identifying trends and patterns from  historical data and enable better mid to long-term decision-making


## KPI

**KPI de eCommerce**

Los 8 KPIs de un eCommerce 
- 1. Tráfico de la tienda online
- 2. Visitas a las vistas de producto
- 3. Tasa de conversión
- 4. Páginas de salida
- 5. Valor medio de pedido
- 6. Recurrencia de compra
- 7. Estacionalidad
- 8. Tasa abandono carrito 
http://datablog.zeus.vision/2017/06/28/los-8-kpis-de-un-ecommerce/


**Social Media Dashboard**


## Referencias

7-Step Process to Build a Dashboard 
- 1. Recopilación de requisitos. 
  - Understand the type of request
  - Who is your intended user base/audience?
  - Which platform(s) should the dashboard be accessible through?
  - What would your users like to see?.  Are there any particular KPIs or metrics they are interested in? Do they want to see any trends or growth/decline in certain metrics? What are the questions or problems your users are trying to solve through the dashboard?
  - What views would your users like to see
  - How frequently should the dashboard be refreshed?
  - Discuss what the data sources can be used
  - Talk about the deadlines
- 2. Ideation
  - Wireframing
- 3. Extract, Transform & Load Your Data
https://www.linkedin.com/pulse/7-step-process-build-dashboard-krishnadev-pillai/


## Revisiones

MAD Framework
The MAD framework is a 3-level approach defined as MONITOR - ANALYSE - DETAIL that allows the end user to flow through data using a top down approach to view data at the 3 levels of detail as required.


The Data Warehousing Institute (TWDI)"https://tdwi.org" came out with the MAD Framework as the optimal way to design business intelligence delivery.



Get MAD with your data
- Monitor. A dashboard is a type of graphical user interface which often provides at-a-glance views of key performance indicators (KPIs) relevant to a particular objective or business process. In other usage, "dashboard" is another name for "progress report" or "report."
- Analyse. This next level gives us the ability to dig a little deeper to understand the issue, when we look at activities we can compare Country, Campaigns, Products etc. to see how each of those are doing in comparison to each other or to a different period and we can start identifying specific areas where there are issues.
- Detail. Once you have drilled in and pinpointed a possible issue that needs to be dealt with - Your Actionable Insight - you may require some further detailed pieces of data. For example, a specific category may be performing much lower on average than others and you have identified something that may be causing it. Once you have drilled down to this key area, you might then want to be able to view all the details of what is happening.
https://www.flockconsulting.co.nz/articles/get-mad-with-your-data


# Metabase

The simplest, fastest way to get business intelligence and analytics to everyone in your company 😋
metabase.com 

https://github.com/metabase/metabase

Metabase is an analytics open-source project written in Clojure. Metabase architecture is relatively simple, deployed in a jar file, and an application database (local H2, MySQL or Postgres)

## Install

Running Metabase on Docker
https://www.metabase.com/docs/latest/operations-guide/running-metabase-on-docker.html 

# Holistics

Holistics is a nice BI alternative to Metabase. It works similar to Metabase in a way that it allows you to map your database tables into models and relationships, and expose this to the end business users to "self-service explore". For more information on how Holistics work, check out the mechanisms.

# Tableau


# Looker

# Apache Superset

Superset is a modern data exploration and data visualization platform. Superset can replace or augment proprietary business intelligence tools for many teams. Superset integrates well with a variety of data sources.


Apache Superset is a modern, enterprise-ready business intelligence web application. It is fast, lightweight, intuitive, and loaded with options that make it easy for users of all skill sets to explore and visualize their data, from simple pie charts to highly detailed deck.gl geospatial charts.

https://superset.apache.org/




## Superset Architecture

In distributed mode superset spread the queries(only it do it for sql_lab, not for dashboards and explore_json) between its celery workers. Superset can use RabbitMQ or Redis for distributing the tasks and it uses Redis for caching queries.

Apache superset is built entirely on top of python; it uses flask app builder internally.
It supports python version > 3.6

Superset is also cloud-native in the sense that it is flexible and lets you choose the:

- web server (Gunicorn, Nginx, Apache), (can run multiple instances). The web server is a flask python app, using sqlalchemy ORM to connect to any database.
- metadata database engine (MySQL, Postgres, MariaDB, etc),
- message queue (Redis, RabbitMQ, SQS, etc),
- results backend (S3, Redis, Memcached, etc),
- caching layer (Memcached, Redis, etc)

## Dashboard & Slices

Dashboard is nothing but a user interface that allows you to examine various graphs and data. So, each section inside the Dashboard is called Slice. Slices can be in the form of data, text, graph, or anything that shares insights–for example, the total number of users who bought a product in a specific city.

## SQL Lab

SQL Lab is a React-based SQL IDE with a wide range of features.

## Alerts and Reports

Users can configure automated alerts and reports to send dashboards or charts to an email recipient or Slack channel.

https://superset.apache.org/docs/installation/alerts-reports/


## Cloud - Hosting

Preset Cloud is a fully hosted, hassle free cloud service for Apache Superset™. Get started for free today!
https://www.preset.io/product/

## Install

 Apache Superset Setup Methods 
- Method 1. Docker
- Method 2: Superset GitHub Configuration Through the Installation of Python Packages 
https://tech-mags.com/apache-superset-setup-methods/

# Redash

Redash is designed to enable anyone, regardless of the level of technical sophistication, to harness the power of data big and small. SQL users leverage Redash to explore, query, visualize, and share data from any data sources. Their work in turn enables anybody in their organization to use the data. Every day, millions of users at thousands of organizations around the world use Redash to develop insights and make data-driven decisions.

https://github.com/getredash/redash

Redash supports more than 35 SQL and NoSQL data sources. 
- MongoDB
- Prometheus

# Google Data Studio 



# CartoDB

CARTO is the leading Location Intelligence platform. It enables organizations to use spatial data and analysis for more efficient delivery routes, better behavioral marketing, strategic store placements, and much more.


Mapas

CartoDB
Gestion de Mapas opensource, 
Create amazing maps with your data
A cloud-based solution for all your mapping needs
Location Intelligence & Data Visualization tool
http://cartodb.com/

## Architecture

Behind the scenes, CARTO leverages PostgreSQL + PostGIS as a web service. That means you the user can have complete control of a fully managed database.