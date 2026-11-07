# Planner

- Notificaciones


Microsoft 365 groups
Plans
Tasks


# Planner resource versioning

Planner versions all resources using etags. These etags are returned with @odata.etag property on each resource. PATCH and DELETE requests require the last etag known by the client to be specified with a If-Match header. 

ETag. El encabezado de respuesta de HTTP ETag es un identificador para una versión específica de un recurso.

Con la ayuda del ETag y los encabezados If-Match (en-US) se puede ser capaz de detectar las colisiones

https://docs.microsoft.com/en-us/graph/api/resources/planner-overview?view=graph-rest-1.0#planner-resource-versioning


# Permissions


Group permissions are also used to control access to Microsoft Planner resources and APIs. Only delegated permissions are supported for Microsoft Planner APIs; application permissions are not supported. Personal Microsoft accounts are not supported.



# Implementation

- Permission type: Delegated 


# Revisiones / TODO

 Small tool that allows ex- and importing MS Planner plans, buckets, tasks, details etc. 
https://github.com/tfenster/plannerexandimport
