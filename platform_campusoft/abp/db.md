DBMS 

# Cambiar prefijos tablas ABP


abp/framework/src/Volo.Abp.Data/Volo/Abp/Data/AbpCommonDbProperties.cs

AbpCommonDbProperties.DbTablePrefix
AbpCommonDbProperties.DbSchema
 

Revisiones:

abp/modules/feature-management/src/Volo.Abp.FeatureManagement.EntityFrameworkCore/Volo/Abp/FeatureManagement/EntityFrameworkCore/FeatureManagementDbContextModelCreatingExtensions.cs


Aplica las configuraciones columnas conceptos genericos.
abp/framework/src/Volo.Abp.EntityFrameworkCore/Volo/Abp/EntityFrameworkCore/Modeling/AbpEntityTypeBuilderExtensions.cs




# Postgres

Las tablas de ABP, no se ajustan si se aplica "EFCore.NamingConventions", para las convenciones de postgres.

--------
Al aplicar convenciones, en postgres para que todos las tablas sean en minusculas, se obtiene error

```
PostgresException: 42P01: relation "AbpSettings" does not exist
```

Existe dbContexto exclusivo para settings. SettingManagementDbContext


Errores similares

```
Message=42P01: relation "AbpPermissionGrants" does not exist
```

```
Npgsql.PostgresException (0x80004005): 42P01: relation "AbpBackgroundJobs" does not exist
```

```
Npgsql.PostgresException (0x80004005): 42P01: relation "AbpAuditLogs" does not exist
```

```
Npgsql.PostgresException (0x80004005): 42P01: relation "AbpFeatureValues" does not exist
```

***Posible solucion:***

En el DbContext, hacer que implemente las diferentes interfaces de los dbContext de los modulos. Ejemplo ISettingManagementDbContext. 

Revisar el codigo fuente de las implementaciones dbContext de los modulos, para conocer que codigo se debe replicar

Poner el atributo de la interfaces dbContext que se va reemplazar

```
[ReplaceDbContext(typeof(ISettingManagementDbContext))]
```

El dbContext que se este utilizando, hacer que implemente la interfaces

```
public class FooDbContext : 
        AbpDbContext<FooDbContext>,
        ISettingManagementDbContext
```
Llamar a la configuraciones de mapeo del modulo.

```
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		.
		.
		.
		modelBuilder.ConfigureSettingManagement();
		
		.
		.
		.
	}

```
--------



```
StackExchange.Redis.RedisConnectionException: It was not possible to connect to the redis server(s). 
```