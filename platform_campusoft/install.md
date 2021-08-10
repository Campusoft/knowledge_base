# Install

Instalar la plataforma Campusoft

## Generales

**Implementar IApplication**

Crear una implementacion de IApplication


**Instalar Binder**

Instalar "FilterEntityBinderProvider"  

En .Net 3.x, codigo referencia:

```
services.AddControllersWithViews(
                    options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                        options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());

                        // add custom binder to beginning of collection
                        options.ModelBinderProviders.Insert(0, new FilterEntityBinderProvider());
                    }
                )
				
```

## Entity Framework. (.net Core)


### Oracle

1.Inslar Oracle.EntityFrameworkCore

- Instalar en los Proyectos *.EntityFramework
  - Opcional. , *.test, *.web,  *.Migrator

2. Referencial al proyecto "Campusoft.Core.EntityFrameworkCore"

Se tiene que hacer una referencia a "Campusoft.Core.EntityFrameworkCore", en el proyecto *.EntityFramework, 
de la solucion que estamos trabajando

Agregar la dependencia al modulo "CampusoftEntityFrameworkModule", en el modulo que se encuentre en el proyecto *.EntityFramework 


3.Connexiones.

Realizar las configuraciones en appsettings.json:

	- Configurar la cadena de conexión. 
	- El framework establece una cadena de conexión “defualt”, en el proyecto *.EntityFramework. DbContext. (Connection: Default). (Cambiar si se requiere)
	- Configurar el esquema. (Por defecto Entitity Framework tiene dbo). (Utilizar appsettings)
      - Key: Core:Database:Schema:Default

4. Configuraciones Proyectos

Modificar clase <Proyecto>DbContextConfigurer. En el proyecto "<Proyecto>.EntityFrameworkCore"

Quitar SqlServer: 

```
   builder.UseSqlServer(connectionStr
ing);
```

Cambiar por:

```   
   builder. UseOracle (connectionString);
```

Configurar esquema por defecto
https://docs.microsoft.com/en-us/ef/core/modeling/relational/default-schema

En el proyecto *.EntityFramework, en la clase DbContext,  sobreescribir el método OnModelCreating. 

- Recuperar el esquema por defecto desde AppSetting
- Establecer el esquema.

```

string defaultSchema = Configuration.GetValue<string>("Core:Database:Schema:Default");
modelBuilder.HasDefaultSchema(defaultSchema);

```

Ajustes varias para Oracle. 

-	El size de las propiedades, nombres de tablas, Oracle tiene restricción de 32.
-	Todo el objeto base de datos a mayúsculas. (Tablas, propiedades)
-	Aplicar políticas para convertir todos los nombres de tablas en mayúsculas. 

Para estos ajustes varios utilizar el método, del proyecto “Campusoft.Core.EntityFrameworkCore” / OracleModelBuilderExtensions

```
modelBuilder.Fix(modelBuilder.Model.GetEntityTypes(), prefix);
```


Solucionar inconveniente con nombres filtros. Aplicar sobreescribir "CreateFilterExpression". TODO: Revisar si las nuevas versiones driver oracle, requiere estos Fix


Eliminar todas las Migrations, de la plantilla. 

Asilamiento de Transacciones

- Aplicar “<Proyecto>DataModule” el aislamiento de las transacciones de la base de datos de Oracle.

```
Configuration.UnitOfWork.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
```
Se debe configurar para evitar el error:

```
Message=IsolationLevel must be ReadCommitted or Serializable 
Source=Oracle.ManagedDataAccess
```

5. Generar la migracion desde el proyecto   "<SchemaProyecto>.EntityFrameworkCore"

## MVC


Agregar dependencias en _ViewImports.cshtml

```
@using Campusoft.Core.Application
@using Campusoft.Core
```

**Layout**

En el layout se deben definir secciones que se utilizan en las vistas de formularios dinamicos.
- page_toolbar
- page_title
- view_form

```
InvalidOperationException: The following sections have been defined but have not been rendered by the page at '/Views/Shared/_Layout.cshtml': 'page_title, page_toolbar'. To ignore an unrendered section call IgnoreSection("sectionName").
```

Crer un view para visualizar avisos acciones CRUD.

/Views/Shared/_ShowAlert.cshtml


