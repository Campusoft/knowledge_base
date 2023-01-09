# Auditoria

Explicaciones de Auditorias. 
https://docs.microsoft.com/es-es/sql/relational-databases/tables/temporal-table-usage-scenarios?view=sql-server-2017

Creación de una tabla temporal con control de versiones del sistema
https://docs.microsoft.com/es-es/sql/relational-databases/tables/creating-a-system-versioned-temporal-table?view=sql-server-2017


# Backup


Sacar respaldos con azure data studio
https://docs.microsoft.com/en-us/sql/azure-data-studio/tutorial-backup-restore-sql-server?view=sql-server-ver15


Activar opciones para poder sacar respaldos. 
https://stackoverflow.com/questions/53513963/how-to-restore-a-database-from-bak-file-from-azure-data-studio-on-mac


Azure Backup: SQL Databases and How To Back Them Up
https://cloud.netapp.com/blog/hosted-database-backup-azure


Generar un respaldo de base de datos LocalDB 
- Conectar Sql Server Management Studio a localDB.
Connecting Localdb using Sql Server Management Studio
https://dotnetthoughts.net/connecting-localdb-using-sqlserver-management-studio/

- Utilizar Sql Server Management Studio, para generar los respaldos

## DACPAC

 DACPAC is an acronym of Data-Tier Application Package. It is a logical database entity defining all database objects such as tables, views, users and logins. It enables developers and database administrators to create a single package file consisting of database objects. We call this package a DACPAC package.

Usually, developers use SQL Server Data Tools (SSDT) for writing t-SQL scripts and making changes as per the requirement. They can generate a DACPAC package and send it to DBAs for deploying in multiple environments. Similarly, DBA can give the DACPAC copy of a production database to developers for their development purpose.

We can also use DACPAC packages for schema comparison as well between different copies of it. You can use the Azure Data Studio SQL Server Schema Compare extension for this. 


BACPAC

A BACPAC is a Windows file with a .bacpac extension that encapsulates a database's schema and data. The primary use case for a BACPAC is to move a database from one server to another - or to migrate a database from a local server to the cloud - and archiving an existing database in an open format.
