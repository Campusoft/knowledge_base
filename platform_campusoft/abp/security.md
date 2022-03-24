# Abp - Security 
Antiforgery

# Audit Logging

AuditingOptions is the main options object to configure the audit log system. You can configure it in the ConfigureServices method of your module
https://docs.abp.io/en/abp/latest/Audit-Logging#abpauditingoptions

***Entity History Selectors***

Saving all changes of all your entities would require a lot of database space. For this reason, audit log system doesn't save any change for the entities unless you explicitly configure it.


- EntityHistorySelectors
- AbpAuditingOptions. (Configuraciones)
- AuditingInterceptor
- AbpAuditActionFilter
https://docs.abp.io/en/abp/latest/Audit-Logging


## Audit Log Contributors
https://docs.abp.io/en/abp/latest/Audit-Logging#audit-log-contributors