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

