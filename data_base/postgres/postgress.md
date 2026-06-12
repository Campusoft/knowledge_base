# Postgres


Identifiers and Key Words
Key words and unquoted identifiers are case insensitive

equals table names:

```
UPDATE MY_TABLE SET A = 5;
uPDaTE my_TabLE SeT a = 5;
```



There is a second kind of identifier: the delimited identifier or quoted identifier. It is formed by enclosing an arbitrary sequence of characters in double-quotes ("). Area case sensitive

different table names:

```
UPDATE "MY_TABLE" SET A = 5;
uPDaTE "my_TabLE" SeT a = 5;
```


# Install

**Docker**

https://hub.docker.com/_/postgres


Postgres with Docker and Docker compose a step-by-step guide for beginners
```
version: '3.8'
services:
  db:
    image: postgres:14.1-alpine
    restart: always
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
    ports:
      - '5432:5432'
    volumes: 
      - db:/var/lib/postgresql/data
volumes:
  db: 
```

https://geshan.com.np/blog/2021/12/docker-postgres/


**Referencias**

Migrating Database from SQL Server(MSSQL) to PostgreSQL 
- Migrating Database Schema manually 
- EnterpriseDB Migration Toolkit
https://dev.to/abhinavgupta1997/migrating-database-from-sql-server-mssql-to-postgresql-1mje


#  Schemas

https://www.postgresql.org/docs/9.1/ddl-schemas.html

# Elementos Generales


# Drives


**.Net**

Npgsql - .NET Access to PostgreSQL
https://www.npgsql.org/

Naming Conventions for Entity Framework Core Tables and Columns.
Entity Framework Core plugin to apply naming conventions to table and column names (e.g. snake_case) 
https://github.com/efcore/EFCore.NamingConventions

# Commandos

CREATE DATABASE databasename;

Crear usuario
CREATE USER <name> WITH CREATEROLE CREATEDB PASSWORD '<password>';
 
## Obtener version de PostgreSQL

Para consultar la version de una base de datos PostgreSQL se puede usar la funcion `version()`.

```
SELECT version();
```

Ejemplo de resultado:

```
PostgreSQL 16.3 on x86_64-pc-linux-gnu, compiled by gcc, 64-bit
```

Si solo se necesita el numero de version del servidor:

```
SHOW server_version;
```

Tambien se puede consultar como parametro del sistema:

```
SELECT current_setting('server_version');
```

Para obtener informacion mas detallada desde vistas del sistema:

```
SELECT
  version() AS postgres_version,
  current_database() AS database_name,
  current_user AS connected_user;
```



# Naming conventions


Use schemas for major functional areas


Date Fields

- Include the timezone, specially if you are working on a global project.
- As a good practice all  tables should have a createdAt and updatedAt column, it will be very useful for debugging. 
- Field should finish with At
  - createdAt
  - updatedAt
  - deletedAt
  - lastUpdatedAt
  
Foreign Keys

They should be a combination of the name of the foreign table and the + _id, examples:

- user_id
- post_id


Status Fields

use booleans for single status.
- isActive
- isPublished

Use enums if you need few columns that can be true or false at the same time.
- post.status (draft, inactive, published)
- user.status (inactive, active, banned)
- product.status (draft, in_review, approved, disapproved)
	
	
	
Database Naming Convention
https://github.com/RootSoft/Database-Naming-Convention

Database, Table and Column naming conventions
https://xpromx.me/blog/database-table-and-column-naming-converntions


How I Write SQL, Part 1: Naming Conventions
Prefixes and Suffixes (are bad). (Interesante ...)
https://launchbylunch.com/posts/2014/Feb/16/sql-naming-conventions/

***Revisiones***

- Stored procedures. 
  - prefix p_<name>
  - If the store procedure is using only one table, I’ll name it p_<table_name>_<action_name>
  - If the procedure uses more than 1 table, I would use a descriptive name for the procedure. 

- Functions
  - prefix f_<name>
 


# Security

Postgres Row Level Security

Policies Are Like WHERE Clauses#
Policies are easy to understand once you get the hang of them. You can just think of them as adding a WHERE clause to every query

5.8. Row Security Policies
When row security is enabled on a table (with ALTER TABLE ... ENABLE ROW LEVEL SECURITY), all normal access to the table for selecting rows or modifying rows must be allowed by a row security policy. (However, the table's owner is typically not subject to row security policies.) If no policy exists for the table, a default-deny policy is used, meaning that no rows are visible or can be modified. Operations that apply to the whole table, such as TRUNCATE and REFERENCES, are not subject to row security.
https://www.postgresql.org/docs/current/ddl-rowsecurity.html


# Tools

# Extensiones

## PostGIS


PostGIS extends its capabilities for geographic objects

## Timescale

## citusdata

## Pipelinedb

High-performance time-series aggregation for PostgreSQL 
https://github.com/pipelinedb/pipelinedb
 
# Backup

## Generar respaldos y restauraciones con pg_dump / pg_restore

### Concepto

`pg_dump` y `pg_restore` son herramientas oficiales de PostgreSQL para generar respaldos logicos y restaurarlos. Un respaldo logico exporta la estructura y/o los datos de una base de datos en un formato que puede ser restaurado posteriormente en otra base de datos PostgreSQL.

### Descripcion

`pg_dump` se usa para crear respaldos de una base de datos PostgreSQL. Puede generar archivos SQL planos o archivos en formatos especiales como `custom`, `directory` o `tar`.

`pg_restore` se usa para restaurar respaldos generados con `pg_dump` en formatos no planos, principalmente el formato `custom` (`-F c`) y el formato `directory` (`-F d`). Si el respaldo fue generado como SQL plano, se restaura normalmente con `psql`.

Formatos comunes:

- `plain`: genera un archivo `.sql`. Se restaura con `psql`.
- `custom`: genera un archivo binario comprimido `.dump` o `.backup`. Se restaura con `pg_restore`.
- `directory`: genera una carpeta con archivos internos del respaldo. Se restaura con `pg_restore`.
- `tar`: genera un archivo `.tar`. Se restaura con `pg_restore`.

### Datos de conexion de ejemplo

```
Host: db-prod-demo.campus.local
Puerto: 5432
Base de datos: ventas_demo
Usuario: backup_user
Password: DemoPass_2026!
```

### Generar respaldo SQL plano

Este respaldo genera un archivo `.sql` con sentencias SQL. Es facil de revisar manualmente, pero puede ser menos flexible para restauraciones parciales.

```
PGPASSWORD="DemoPass_2026!" pg_dump \
  -h db-prod-demo.campus.local \
  -p 5432 \
  -U backup_user \
  -d ventas_demo \
  -F p \
  -f ventas_demo_backup.sql
```

Restaurar un respaldo SQL plano:

```
PGPASSWORD="DemoPass_2026!" psql \
  -h db-restore-demo.campus.local \
  -p 5432 \
  -U postgres_admin \
  -d ventas_demo_restaurada \
  -f ventas_demo_backup.sql
```

### Generar respaldo en formato custom

Este formato es recomendado para muchos escenarios porque permite compresion, restauracion selectiva y restauracion paralela.

```
PGPASSWORD="DemoPass_2026!" pg_dump \
  -h db-prod-demo.campus.local \
  -p 5432 \
  -U backup_user \
  -d ventas_demo \
  -F c \
  -b \
  -v \
  -f ventas_demo_backup.dump
```

Restaurar respaldo en formato custom:

```
PGPASSWORD="RestorePass_2026!" pg_restore \
  -h db-restore-demo.campus.local \
  -p 5432 \
  -U postgres_admin \
  -d ventas_demo_restaurada \
  -v \
  ventas_demo_backup.dump
```

Restaurar limpiando objetos existentes antes de recrearlos:

```
PGPASSWORD="RestorePass_2026!" pg_restore \
  -h db-restore-demo.campus.local \
  -p 5432 \
  -U postgres_admin \
  -d ventas_demo_restaurada \
  --clean \
  --if-exists \
  -v \
  ventas_demo_backup.dump
```

### Generar respaldo solo de estructura

Util para migrar o versionar el esquema sin incluir datos.

```
PGPASSWORD="DemoPass_2026!" pg_dump \
  -h db-prod-demo.campus.local \
  -p 5432 \
  -U backup_user \
  -d ventas_demo \
  --schema-only \
  -F p \
  -f ventas_demo_schema.sql
```

### Generar respaldo solo de datos

Util cuando la estructura ya existe y solo se requiere copiar informacion.

```
PGPASSWORD="DemoPass_2026!" pg_dump \
  -h db-prod-demo.campus.local \
  -p 5432 \
  -U backup_user \
  -d ventas_demo \
  --data-only \
  -F c \
  -f ventas_demo_data.dump
```

### Respaldar una tabla especifica

```
PGPASSWORD="DemoPass_2026!" pg_dump \
  -h db-prod-demo.campus.local \
  -p 5432 \
  -U backup_user \
  -d ventas_demo \
  -t public.clientes \
  -F c \
  -f clientes_backup.dump
```

Restaurar una tabla especifica desde un respaldo custom:

```
PGPASSWORD="RestorePass_2026!" pg_restore \
  -h db-restore-demo.campus.local \
  -p 5432 \
  -U postgres_admin \
  -d ventas_demo_restaurada \
  -t public.clientes \
  -v \
  clientes_backup.dump
```

### Crear la base de datos antes de restaurar

`pg_restore` no siempre crea la base de datos destino automaticamente. En muchos casos conviene crearla antes:

```
PGPASSWORD="RestorePass_2026!" createdb \
  -h db-restore-demo.campus.local \
  -p 5432 \
  -U postgres_admin \
  ventas_demo_restaurada
```

Luego se restaura:

```
PGPASSWORD="RestorePass_2026!" pg_restore \
  -h db-restore-demo.campus.local \
  -p 5432 \
  -U postgres_admin \
  -d ventas_demo_restaurada \
  -v \
  ventas_demo_backup.dump
```

### Restauracion paralela

Para respaldos en formato `custom` o `directory`, se puede acelerar la restauracion usando varios procesos con `-j`.

```
PGPASSWORD="RestorePass_2026!" pg_restore \
  -h db-restore-demo.campus.local \
  -p 5432 \
  -U postgres_admin \
  -d ventas_demo_restaurada \
  -j 4 \
  -v \
  ventas_demo_backup.dump
```

### Buenas practicas

- Usar usuarios con permisos limitados para generar respaldos.
- Guardar los respaldos en una ubicacion segura y con control de acceso.
- Probar periodicamente la restauracion; un respaldo no validado puede no servir en una emergencia.
- Automatizar respaldos con tareas programadas como `cron`, `systemd timers`, GitHub Actions, Jenkins o herramientas del sistema operativo.
- Incluir fecha y hora en el nombre del archivo, por ejemplo `ventas_demo_2026_05_21_2300.dump`.
- Evitar escribir passwords reales directamente en scripts compartidos; preferir variables de entorno, archivos `.pgpass` o gestores de secretos.

# Referencias


A curated list of awesome PostgreSQL software, libraries, tools and resources
https://github.com/dhamaniasad/awesome-postgres


Building a Scalable Event-Driven Search Architecture With Postgres’ Full-Text Search
- Terminologies
  - Stemming
  - NGram
  - Fuzziness
  - Similarity
  - Ranking
- pg_trgm Postgres extension and added a words column to the table to store the searchable text.
https://betterprogramming.pub/building-a-scalable-event-driven-search-architecture-with-postgres-full-text-search-4780b87a34ef

