# Business Intelligence (BI) - Inteligencia Negocios

Business Intelligence (BI). Business Analytics. The Cloud. On-Premises. Artificial intelligence (AI). Machine learning (ML). Self-service analytics. Augmented Analytics. All of these concepts are important to understand


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

Preset Cloud is a fully hosted, hassle free cloud service for Apache Superset™. Get started for free today!
https://www.preset.io/product/


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

## Install

 Apache Superset Setup Methods 
- Method 1. Docker
- Method 2: Superset GitHub Configuration Through the Installation of Python Packages 
https://tech-mags.com/apache-superset-setup-methods/

# Redash

Redash is designed to enable anyone, regardless of the level of technical sophistication, to harness the power of data big and small. SQL users leverage Redash to explore, query, visualize, and share data from any data sources. Their work in turn enables anybody in their organization to use the data. Every day, millions of users at thousands of organizations around the world use Redash to develop insights and make data-driven decisions.

https://github.com/getredash/redash

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


