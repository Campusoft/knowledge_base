# Abp - Audit


ABP Framework provides an extensible audit logging system that automates the audit logging by convention and provides configuration points to control the level of the audit logs.
https://docs.abp.io/en/abp/latest/Audit-Logging

La auditoria, se encuentra activa por defecto.

Tables
-   AbpAuditLogs
	- AbpAuditLogActions
    - AbpEntityChanges
    - AbpEntityPropertyChanges

Informacion registra en las tablas:

- AuditLogInfo: The root object. El que contiene la informacion general (Cabecera), usuario, fecha, ip, etc.
- AuditLogActionInfo: An audit log action is typically a controller action or an application service method call during the web request.
- EntityChangeInfo: Represents a change of an entity in this web request. An audit log may contain zero or more entity changes.
- EntityPropertyChangeInfo: Represents a change of a property of an entity.

Explicacion.

Se ejecuta un proceso 'Ej: crear una orden de compra' 
- La auditoria, registrara un registro principal (Cabecera), de esta accion.
- Se registraran todos los servicios de aplicacion que intervienen en esta accion.
- Se registraran todas los cambios en las entidades que intervengan en esta accion, y si se encuentra configuradas que se auditen. 

# Configuration

AuditingOptions is the main options object to configure the audit log system. You can configure it in the ConfigureServices method of your module
https://docs.abp.io/en/abp/latest/Audit-Logging#abpauditingoptions

**Entity History Selectors**

Saving all changes of all your entities would require a lot of database space. For this reason, audit log system doesn't save any change for the entities unless you explicitly configure it.


- EntityHistorySelectors
- AbpAuditingOptions. (Configuraciones)
- AuditingInterceptor
- AbpAuditActionFilter
https://docs.abp.io/en/abp/latest/Audit-Logging


**Ejemplo de utilizacion de  EntityHistorySelectors**

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


Configurar todas las entidades 

```
Configure<AbpAuditingOptions>(options =>
{
    options.EntityHistorySelectors.AddAllEntities();
});

```


# Audit Log Contributors

https://docs.abp.io/en/abp/latest/Audit-Logging#audit-log-contributors


# Implementaciones


AbpAuditingMiddleware
- abp/framework/src/Volo.Abp.AspNetCore/Volo/Abp/AspNetCore/Auditing/AbpAuditingMiddleware.cs 
- https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore/Volo/Abp/AspNetCore/Auditing/AbpAuditingMiddleware.cs


AuditingManager
abp/framework/src/Volo.Abp.Auditing/Volo/Abp/Auditing/AuditingManager.cs 

AddAllEntities
- https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Ddd.Domain/Volo/Abp/Auditing/EntityHistorySelectorListExtensions.cs


# Revisiones


UseCorrelationId
abp/samples/MicroserviceDemo/applications/AuthServer.Host/AuthServerHostModule.cs 

Implementacion ICorrelationIdProvider
abp/framework/src/Volo.Abp.AspNetCore/Volo/Abp/AspNetCore/Tracing/AspNetCoreCorrelationIdProvider.cs

Configuraciones Correlation. AbpCorrelationIdOptions
- Nombre cabecera por defecto. "X-Correlation-Id"

Claim
- CurrentUser.FindClaim: Gets a claim with the given name. Returns null if not found.
https://docs.abp.io/en/abp/latest/CurrentUser
