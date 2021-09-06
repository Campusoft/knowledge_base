# EntityFramework



## Conventions


Entity Framework Core: Naming Convention
https://www.meziantou.net/entity-framework-core-naming-convention.htm

### Laboratorios

Generar prefijos tablas a todas las columnas.
Buscar el prefijo que se establece para una tabla, con el nombre clase de la entidad: "entity.ClrType.Name".


**Reference**
Prefixing ID columns with the table name in Entity Framework
https://imar.spaanjaars.com/588/prefixing-id-columns-with-the-table-name-in-entity-framework


## Migration

.NET Core CLI
- dotnet ef migrations add InitialCreate

Visual Studio
- Add-Migration InitialCreate

Visual Studio (Actualizar base de datos)
- Update-Database

Migrations Overview
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli


## Reference

This site provides documentation and tutorials for people looking for help with using Entity Framework Core
https://www.learnentityframeworkcore.com/

# Oracle


Mappging GUID

using .HasConversion<string>(), para especificar que se gestione como un string el GUID.
```
builder.Property(a => a.Id)
     .HasConversion<string>();
```
https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=fluent-api


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

