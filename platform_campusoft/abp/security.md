# Abp - Security 
Antiforgery

# Audit Logging

AuditingOptions is the main options object to configure the audit log system. You can configure it in the ConfigureServices method of your module
https://docs.abp.io/en/abp/latest/Audit-Logging#abpauditingoptions

**Entity History Selectors**

Saving all changes of all your entities would require a lot of database space. For this reason, audit log system doesn't save any change for the entities unless you explicitly configure it.


- EntityHistorySelectors
- AbpAuditingOptions. (Configuraciones)
- AuditingInterceptor
- AbpAuditActionFilter
https://docs.abp.io/en/abp/latest/Audit-Logging


**Ejemplo de utilizacion de  EntityHistorySelectors

Configurar tres entidades (tipos), que se auditen. con NamedTypeSelector. Las entidades son Unit, UnitType, Client

```
Configure<AbpAuditingOptions>(options =>
{
	options.EntityHistorySelectors.Add(
		new NamedTypeSelector(
			"Unit",
			type => type == typeof(Unit))
	);

	options.EntityHistorySelectors.Add(
		new NamedTypeSelector(
			"UnitType",
			type => type == typeof(UnitType))
	);
	
	options.EntityHistorySelectors.Add(
		new NamedTypeSelector(
			"Client",
			type => type == typeof(Client))
	);
});
```

Se puede configurar con un listado de tipos (entidades), en un solo selector

```
Configure<AbpAuditingOptions>(options =>
{
	options.EntityHistorySelectors.Add("Agregar-tres-Entidades",typeof(Unit),typeof(UnitType),typeof(Client));
});
```

## Audit Log Contributors
https://docs.abp.io/en/abp/latest/Audit-Logging#audit-log-contributors