# Directus

Directus dynamically generates custom API endpoints based on your SQL database's schema in real-time — something we call "Database Mirroring". Whether you install fresh or on top of an existing database, you always maintain end-to-end control over your actual database, including: tables, columns, datatypes, default values, indexes, relationships, etc.

El visionamiento de directus, es permitir tener total control sobre la base de datos, lo que llaman "Database Mirroring".

Node or PHP
- PHP-Laravel API for Directus 9. (Working..)

UI
- Vue


# Features 

- Authentication
- Querying
- Custom Logic
- Rate Limiting
- Caching


Any SQL Database

Database abstraction is now handled by Knex, and we’ve expanded our official support to include: MySQL, PostgreSQL, SQLite, Microsoft SQL Server, Oracle DB, and variants such as MariaDB, AWS RedShift, AWS Aurora, and more.

- REST API
- GraphQL API
- Event Hooks
- Webhooks
- Custom API Filters
 
## Database
 
Current database support includes: PostgreSQL, MySQL, SQLite, MS-SQL Server, OracleDB, MariaDB, and varients such as AWS Aurora/Redshift or Google Cloud Platform SQL. 


## API


<url-base>/items/<collection-name>


## Custom API Endpoints

Custom API Endpoints register new API routes which can be used to infinitely extend the core functionality of the platform.

https://docs.directus.io/guides/api-endpoints/


# Architecture

Directus only requires Node.js and supports most operating systems and SQL database vendors.

- Node.js 12.20+
- npm 6.x+
	
https://docs.directus.io/getting-started/architecture.html


# Instalar


Docker
- Docker Compose
https://docs.directus.io/self-hosted/quickstart.html	

This compose file is meant to spin up a copy of all supported database vendors + Redis and S3 (Minio).
https://github.com/directus/directus/blob/main/docker-compose.yml

# Laboratorios

Iniciar

```
npx directus start
``

# Referencias

Getting Started with Directus 9 — Platform Overview & Tutorial. (Video)
https://www.youtube.com/watch?v=AicEmIeuuLw
