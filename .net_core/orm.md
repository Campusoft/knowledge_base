# EntityFramework



# Conventions


Entity Framework Core: Naming Convention
https://www.meziantou.net/entity-framework-core-naming-convention.htm

***Revisiones***

GetColumnName extension method is obsolete
https://stackoverflow.com/questions/65837188/how-to-get-list-of-database-table-primary-key-columns-in-ef-core

Informacion obsolete GetColumnName
https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/breaking-changes#getcolumnname-obsolete

## Laboratorios

Generar prefijos tablas a todas las columnas.
Buscar el prefijo que se establece para una tabla, con el nombre clase de la entidad: "entity.ClrType.Name".


**Reference**
Prefixing ID columns with the table name in Entity Framework
https://imar.spaanjaars.com/588/prefixing-id-columns-with-the-table-name-in-entity-framework

# Global Query Filters

Global query filters are LINQ query predicates applied to Entity Types in the metadata model (usually in OnModelCreating). 
https://docs.microsoft.com/en-us/ef/core/querying/filters

# Connection

Connection Resiliency
Connection resiliency automatically retries failed database commands. 
https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency


For Azure SQL DB, Entity Framework (EF) Core already provides internal database connection resiliency
and retry logic.

# Mapeos

Configurar el mapeo de una propiedad que permite asociar dos entidades, que no posee propiedad de navegacion.
- Propiedad para asociar a otra entidad. ServiceId
- Entidad "Domain.Service", que no posee una propiedad de navegacion en la entidad que se esta configurado.

```
 b.HasOne<Domain.Service>().WithMany().HasForeignKey(ur => ur.ServiceId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
```

Configurar el mapeo de una propiedad que permite asociar dos entidades, que  posee propiedad de navegacion.
- Propiedad asociacion "PaymentTypeId"
- Propiedad de navegacion a la otra entidad. "PaymentType"
```
 b.HasOne(u => u.PaymentType).WithMany().HasForeignKey(a => a.PaymentTypeId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);
```

Establecer que una propiedad "String", que no requiere Unicode. 

```
entity.Property(x => x.ColumnName).IsUnicode(false);  
```

## Value converters

Json
https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=fluent-api#composite-value-objects



# Migration

.NET Core CLI
- dotnet ef migrations add InitialCreate



Visual Studio
- Add-Migration InitialCreate

.NET Core CLI
- dotnet ef database update

--startup-project

```
dotnet ef database update --startup-project ../../host/Mre.Sb.AdministrativeUnit.HttpApi.Host --context AdministrativeUnitDbContext
```

Visual Studio (Actualizar base de datos)
- Update-Database

Migrations Overview
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli



The dotnet ef tool is no longer part of the .NET Core SDK

install dotnet ef as a global tool typing the following command:
```
dotnet tool install --global dotnet-ef
```



Entity Framework Core tools reference - .NET Core CLI
https://docs.microsoft.com/en-us/ef/core/cli/dotnet

Errores .NET Core CLI
----------------------

```
More than one DbContext was found. Specify which one to use. Use the '-Context'
parameter for PowerShell commands and the '--context' parameter for dotnet comma
nds.
```

```
dotnet ef database update --context AdministrativeUnitDbContext
```

# Interceptors
Entity Framework Core (EF Core) interceptors enable interception, modification, and/or suppression of EF Core operations. This includes low-level database operations such as executing a command, as well as higher-level operations, such as calls to SaveChanges.

Interceptors are different from logging and diagnostics in that they allow modification or suppression of the operation being intercepted.  
https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors

The auditing sample contains a simple console application that makes changes to the blogging database and then shows the auditing that was created.
https://github.com/dotnet/EntityFramework.Docs/tree/main/samples/core/Miscellaneous/SaveChangesInterception

# Best Practiq


- Consider whether to use Unicode data type  (e.g. NCHAR,NVARCHAR,NTEXT) if you need to support internationalization, but Unicode will take twice as much space. (*****)

```
entity.Property(x => x.ColumnName).IsUnicode(false);  
```


# Versiones


Save Changes Interceptors were introduced in EF Core 5.0.


# Reference

This site provides documentation and tutorials for people looking for help with using Entity Framework Core
https://www.learnentityframeworkcore.com/

***Oracle Mappging GUID***

using .HasConversion<string>(), para especificar que se gestione como un string el GUID.
```
builder.Property(a => a.Id)
     .HasConversion<string>();
```
https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=fluent-api

# Revisiones

- TODO: Interceptores EntityFramework ORM

# Dapper

Dapper is a micro-ORM for .Net that extends your IDbConnection, simplifying query setup, execution, and result-reading.


Configuraciones

SqlMapper.Settings.CommandTimeout = 120; // 2 min

Parameter Syntax Reference 
https://riptutorial.com/dapper/topic/10/parameter-syntax-reference


Manuales
https://riptutorial.com/dapper

## Transactions

Dapper - Transaction
Una referencia como generar transacciones con dos formas diferentes:
- Connecton.BeginTransaction
- TransactionScope
https://dapper-tutorial.net/transaction

## Dapper.Oracle



## Referencias

[Oracle - Dapper]
(https://github.com/DIPSAS/Dapper.Oracle)

