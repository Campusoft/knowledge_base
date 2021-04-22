# Pool Connection Oracle

Informacion para establecer configuracion pool oracle.

Connection Lifetime = how long a connection lives before it is killed and recreated. A lifetime of 0 means never kill and recreate. Normally not a bad thing, because killing and recreating a connection is slow. Through various bugs your connections may get stuck in an unstable state (like when dealing with weird 3 way transactions).. but 99% of the time it is good to keep connection lifetime as infinite.

Connection Timeout: The time to wait (in seconds) for a new connection or an idle connection from the connection pool before a connection time out error can occur. Default: 15



Using Connection Pooling

When connection pooling is enabled (the default), the Open and Close methods of the OracleConnection object implicitly use the connection pooling service, which is responsible for pooling and returning connections to the application.

The connection pooling service creates connection pools by using the ConnectionString property as a signature, to uniquely identify a pool.

If there is no existing pool with the exact attribute values as the ConnectionString property, the connection pooling service creates a new connection pool. If a pool already exists with the requested signature, a connection is returned to the application from that pool.

When a connection pool is created, the connection pooling service initially creates the number of connections defined by the Min Pool Size attribute of the ConnectionString property. This number of connections is always maintained by the connection pooling service for the connection pool.

At any given time, these connections are in use by the application or are available in the pool.

The Incr Pool Size attribute of the ConnectionString property defines the number of new connections to be created by the connection pooling service when more connections are needed in the connection pool.

When the application closes a connection, the connection pooling service determines whether or not the connection lifetime has exceeded the value of the Connection Lifetime attribute. If so, the connection pooling service closes the connection; otherwise, the connection goes back to the connection pool. The connection pooling service enforces the Connection Lifetime only when a connection is going back to the connection pool.

The Max Pool Size attribute of the ConnectionString property sets the maximum number of connections for a connection pool. If a new connection is requested, but no connections are available and the limit for Max Pool Size has been reached, then the connection pooling service waits for the time defined by the Connection Timeout attribute. If the Connection Timeout time has been reached, and there are still no connections available in the pool, the connection pooling service raises an exception indicating that the connection pool request has timed-out. Upon a connection timeout, ODP.NET distinguishes whether the timeout occurred due to the database server failing to deliver a connection in the allotted time or no connection being available in the pool due to the maximum pool size having been reached. The exception text returned will either be "Connection request timed out" in the case of the former or "Pooled connection request timed out" in the case of the latter.

The Validate Connection attribute validates connections coming out of the pool. This attribute should be used only when absolutely necessary, because it causes a round-trip to the database to validate each connection immediately before it is provided to the application. If invalid connections are uncommon, developers can create their own event handler to retrieve and validate a new connection, rather than using the Validate Connection attribute. This generally provides better performance.

The connection pooling service closes connections when they are not used; connections are closed every 3 minutes. The Decr Pool Size attribute of the ConnectionString property provides connection pooling service for the maximum number of connections that can be closed every 3 minutes.

Beginning with Oracle Data Provider for .NET release 11.1.0.6.20, enabling connection pooling by setting "pooling=true" in the connection string (which is the case by default) will also pool operating system authenticated connections.
 
https://docs.oracle.com/en/database/oracle/oracle-data-access-components/18.3/odpnt/featConnecting.html#GUID-DEE5B959-0BC8-42A7-962D-697A38BA5DA8 

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



