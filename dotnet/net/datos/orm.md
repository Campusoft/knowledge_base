# EntityFramework

EF o Entity Framework es el ORM oficial de Microsoft




Como crea los objetos Entity Framework

- No utiliza el constructor público predeterminado ni cualquier otro constructor explícito por defecto.
- Usa reflexión para establecer las propiedades incluso si son privadas o no tienen setters públicos.
- Por tanto, no necesita que todas las propiedades estén inicializadas en el constructor.


# DbContext 

El AddDbContext, agrega un Db con el ciclo vida por defecto. ServiceLifetime.Scoped

DbContext Configuration
- There are various ways the tools try to create the DbContext:
  - From application services
  - Using a constructor with no parameters
  - From a design-time factory
  - Configuring DbContextOptions
  - Using DbContext with dependency injection
https://riptutorial.com/ef-core-advanced-topics/learn/100000/dbcontext-configuration



Codigo
- EntityFrameworkServiceCollectionExtensions.AddDbContext
https://github.com/dotnet/efcore/blob/main/src/EFCore/Extensions/EntityFrameworkServiceCollectionExtensions.cs

# Conventions

Code First Conventions
- Conventions are sets of rules that are used to automatically configure a conceptual model based on class definitions when working with Code First.
- Type Discovery
- Primary Key Convention
- Relationship Convention
https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/conventions/built-in

Conventions in Entity Framework Core. (Reglas aplicadas)
- Primary Key 
- Foreign Key 
- Table 
- Columns 
- Data Types 
https://www.learnentityframeworkcore.com/conventions

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


Configurar un padre, que tiene una lista hija, y esta clase hija, posee explicitamente una propiedad de la clave principal del padre. 
- Clase Company, tiene una lista de clase "Stores"
- Clase Stores, no posee una propiedad de navegacion al padre "Company", solo una propiedad "CompanyId" de la clave principal del padre (Clave Foranea)
- Se debe realizar la configuracion en el padre, ya que si se realiza alguna configuracion en la clase hija "Stores" se va obtener errores porque EF va crear "Shadow foreign keys"


Error "Shadow foreign keys"

```
warn: Microsoft.EntityFrameworkCore.Model.Validation[10625]
      The foreign key property 'PointSale.StoreId1' was created in shadow state because a conflicting property with the simple name 'StoreId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.  
```

Configuracion en el hijo. (Error Shadow foreign keys)

```
public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> b)
    {
        b.HasKey(ci => ci.Id);

        b.HasOne<Company>().WithMany(e => e.Stores)
          .HasForeignKey(x => x.CompanyId)
          .OnDelete(DeleteBehavior.Restrict);
         
    }
}
```

Configuracion en el padre. (ok)

```

 public class CompanyConfiguration : IEntityTypeConfiguration<Company>
 {
     public void Configure(EntityTypeBuilder<Company> builder)
     {
         builder.HasKey(ci => ci.Id);

        
         builder
           .HasMany(e => e.Stores)
             .WithOne()
             .HasForeignKey(x => x.CompanyId)
             .OnDelete(DeleteBehavior.Restrict);

     }
 }
 
```

Establecer que una propiedad "String", que no requiere Unicode. 

```
entity.Property(x => x.ColumnName).IsUnicode(false);  
```

## Value converters

Json
https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=fluent-api#composite-value-objects

# Ejecutar Sql Directamente



- Use the DbSet.FromSql method for queries that return entity types. The returned objects must be of the type expected by the DbSet object, and they're automatically tracked by the database context unless you turn tracking off.

- Use the Database.ExecuteSqlCommand for non-query commands.



# Migration


- Install the tools
  - Si desea trabajar en visual studio.  Entity Framework Core tools reference - Package Manager Console in Visual Studio
  - We generally recommend using the .NET Core CLI tools, which work on all platforms. Entity Framework Core tools reference - .NET Core CLI  
- Ejecutar commandos



**.NET Core CLI tools**

Escenario
- Existe un proyecto DbContext. 
- Otro proyecto host, proyecto startup, donde se configura el proveedor de base de datos a utilizar.

Crear migracion
- En el proyecto donde se encuentra DbContext, ejecutar. 
- El parametro "--startup-project" sirve para establecer donde se encuentra el proyecto startup. 
- Nombre de la migracion "InitialCreate"

```
dotnet ef --startup-project ../MyCompanyName.MyProjectName.HttpApi/ migrations add InitialCreate  
```

Aplicar migracion a la base de datos.


```
dotnet ef database update --startup-project ../MyCompanyName.MyProjectName.HttpApi/
```

--------------------


Entity Framework Core tools reference - .NET Core CLI

- The command-line interface (CLI) tools for Entity Framework Core perform design-time development tasks. For example, they create migrations, apply migrations, and generate code for a model based on an existing database. The commands are an extension to the cross-platform dotnet command, which is part of the .NET Core SDK. These tools work with .NET Core projects.


.NET Core CLI

Crear nueva migracion

```
dotnet ef migrations add InitialCreate
```

Actualizar la base de datos. 
```
dotnet ef database update
```

Opcion, para especificar el proyecto inicial. 

--startup-project

```
dotnet ef database update --startup-project ../../host/Mre.Sb.AdministrativeUnit.HttpApi.Host --context AdministrativeUnitDbContext
```
 
------------------------------------------

Visual Studio
- Add-Migration InitialCreate


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

--------------------------------------

```
Unable to create an object of type 'ApplicationDb'. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
```

Design-time DbContext Creation
- If your startup project uses the ASP.NET Core Web Host or .NET Core Generic Host, the tools try to obtain the DbContext object from the application's service provider.
- From a design-time factory

You can also tell the tools how to create your DbContext by implementing the Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<TContext> interface: If a class implementing this interface is found in either the same project as the derived DbContext or in the application's startup project, the tools bypass the other ways of creating the DbContext and use the design-time factory instead.

```
public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
{
    public BloggingContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
        optionsBuilder.UseSqlite("Data Source=blog.db");

        return new BloggingContext(optionsBuilder.Options);
    }
}
```

-------------------------------------------

How to fix in EF Core: Your startup project '[StartupProjectName]' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.


Make sure you have installed or referenced the Microsoft.EntityFrameworkCore.Design package in your Startup Project.





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

# Provider


## sql server

## sqlite



# Reverse Engineering

How it works

Reverse engineering starts by reading the database schema. It reads information about tables, columns, constraints, and indexes.

Next, it uses the schema information to create an EF Core model. Tables are used to create entity types; columns are used to create properties; and foreign keys are used to create relationships.



Reverse Engineering
- With Visual Studio o With .NET Core CLI
https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=vs


Creating a Model for an Existing Database in Entity Framework Core
- Tener instalado: Microsoft.EntityFrameworkCore.Tools
- Si tiene problemas con las conexion con las claves. Change double quotes " for single quotes ' It is not necessary to add a escape character 
https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx


If you use Visual Studio, the EF Core Power Tools community extension, a graphical tool which builds on top of the EF Core command line tools, offers additional workflow and customization options.

https://github.com/ErikEJ/EFCorePowerTools

# Versiones


Save Changes Interceptors were introduced in EF Core 5.0.

Entity Framework 6.x
- Configuración del modelo anterior a la convención
```
configurationBuilder
    .Properties<string>()
    .AreUnicode(false)
    .HaveMaxLength(1024);
```

---------------------------------

Entity Framework 7.x
- JSON columns
- Bulk updates and deletes
- Faster SaveChanges
- Table-per-concrete-type (TPC) inheritance mapping
- Stored procedure mapping for insert/update/delete

Examples entity framework 7.x
https://github.com/dotnet/EntityFramework.Docs/tree/main/samples/core/Miscellaneous/NewInEFCore7


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


## Manuales / Tutoriales

For Beginners "Principiante"

**Entity Framework Core for Beginners video series**

- Posee videos.
- Posee codigo.
- Posee documentacion de los tutoriales
Temas
- Persist and retrieve relational data with Entity Framework Core (Microsoft Learn)
- Reverse Engineering
- Tutorial: Create a Razor Pages web app with ASP.NET Core
- Azure Cosmos DB local emulator
- No-tracking queries. Loading related data. Split queries. DbContext pooling. Raw SQL queries
https://github.com/MicrosoftDocs/ef-core-for-beginners

- Los videos del curso
Getting Started with Entity Framework Core [1 of 5] | Entity Framework Core for Beginners
https://www.youtube.com/watch?v=SryQxUeChMc

Working with an Existing Database [2 of 5] | Entity Framework Core for Beginners
https://www.youtube.com/watch?v=DCYVfLT5_QI

ASP.NET Core Web Apps with EF Core [3 of 5] | Entity Framework Core for Beginners
https://www.youtube.com/watch?v=c-wN-fc594c

Database Providers [4 of 5] | Entity Framework Core for Beginners
https://www.youtube.com/watch?v=moRmKo3nrN4

Performance Tips [5 of 5] | Entity Framework Core for Beginners
https://www.youtube.com/watch?v=jgESld7U5Bw&t=68s


## Revisiones

- TODO: Interceptores EntityFramework ORM



- AddDbContextPool:
https://docs.microsoft.com/es-es/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cwith-constant#dbcontext-pooling


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

