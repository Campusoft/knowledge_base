# APEX


3.7 Understanding URL Syntax 
Oracle Application Express applications support two types of URL syntax: Friendly URL Syntax and f?p Syntax. 


f?p URL Structure and Syntax
https://hostname:port/ords/f?p=<app_id>:<page_number>:<session_id>

Ejemplo:
https://hostname:port/ords/f?p=392:3:13766599855150

https://docs.oracle.com/en/database/oracle/application-express/20.2/htmdb/understanding-url-syntax.html#GUID-F9B81EAF-D33F-401D-8349-3952DEDA5460

6.3 Understanding Page Types, Features, and Settings 
https://docs.oracle.com/en/database/oracle/application-express/20.2/htmdb/page-types-features-settings.html#GUID-668FAB1D-729E-49A9-AC44-0A729674E0FB

# Version

 
Oracle APEX 20.2 introduced data synchronization from a REST Data Source (formerly known as Web Source Module) to a local table. Synchronization can run either on Schedule or on Demand, by calling the APEX_REST_SOURCE_SYNC package. Developers don't need to build custom PL/SQL code in order to copy data from REST services to local tables; APEX provides this as a declarative option.
