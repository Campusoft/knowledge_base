# Pool Connection Oracle

Informacion para establecer configuracion pool oracle.

Connection Lifetime = how long a connection lives before it is killed and recreated. A lifetime of 0 means never kill and recreate. Normally not a bad thing, because killing and recreating a connection is slow. Through various bugs your connections may get stuck in an unstable state (like when dealing with weird 3 way transactions).. but 99% of the time it is good to keep connection lifetime as infinite.


## Referencias Parametros / Conneciones


Oracle Data Provider for .NET / ODP.NET connection strings
[Enlace](https://www.connectionstrings.com/oracle-data-provider-for-net-odp-net/)

Connection Pooling

ODP.NET connection pooling is enabled and disabled using the Pooling connection string attribute. By default, connection pooling is enabled. The following are ConnectionString attributes that control the behavior of the connection pooling service: 

[Enlace](https://docs.oracle.com/en/database/oracle/oracle-data-access-components/18.3/odpnt/featConnecting.html#GUID-AAC5352A-83F2-483B-A681-93A5200CA83A
)



Informacion parametros en la coneccion
[Enlace](https://docs.oracle.com/en/database/oracle/oracle-data-access-components/18.3/odpnt/featConnecting.html#GUID-0CFEB161-68EF-4BC2-8943-3BDFFB878602)


Establecer parametros en la cadena conexion. (Valores Referenciales, no deben considerarse valores optimos o estandares)
[Enlace](https://www.connectionstrings.com/oracle-data-provider-for-net-odp-net/specifying-pooling-parameters/)



