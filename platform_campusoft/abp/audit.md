# Abp - Audit


ABP Framework provides an extensible audit logging system that automates the audit logging by convention and provides configuration points to control the level of the audit logs.
https://docs.abp.io/en/abp/latest/Audit-Logging

La auditoria, se encuentra activa por defecto.


Tables
-   AbpAuditLogs
	- AbpAuditLogActions
    - AbpEntityChanges
    - AbpEntityPropertyChanges


AuditLogInfo: The root object. El que contiene la informacion general (Cabecera), usuario, fecha, ip, etc.
AuditLogActionInfo: An audit log action is typically a controller action or an application service method call during the web request.
EntityChangeInfo: Represents a change of an entity in this web request. An audit log may contain zero or more entity changes.
EntityPropertyChangeInfo: Represents a change of a property of an entity.

Explicacion.

Se ejecuta un proceso 'Ej: crear una orden de compra' 
- La auditoria, registrara un registro principal (Cabecera), de esta accion.
- Se registraran todos los servicios de aplicacion que intervienen en esta accion.
- Se registraran todas los cambios en las entidades que intervengan en esta accion, y si se encuentra configuradas que se auditen. 


Entity History

# Implementaciones


AbpAuditingMiddleware
abp/framework/src/Volo.Abp.AspNetCore/Volo/Abp/AspNetCore/Auditing/AbpAuditingMiddleware.cs 

AuditingManager
abp/framework/src/Volo.Abp.Auditing/Volo/Abp/Auditing/AuditingManager.cs 

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
