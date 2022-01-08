# Abp - Audit


ABP Framework provides an extensible audit logging system that automates the audit logging by convention and provides configuration points to control the level of the audit logs.
https://docs.abp.io/en/abp/latest/Audit-Logging

La auditoria, se encuentra activa por defecto.


Tables
-   AbpAuditLogs
	- AbpAuditLogActions
    - AbpEntityChanges
    - AbpEntityPropertyChanges


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
