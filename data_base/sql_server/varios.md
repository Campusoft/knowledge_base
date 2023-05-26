# Niveles de Aislamiento

READ COMMITTED es el nivel de aislamiento predeterminado de SQL Server. Impide las lecturas de datos sucios especificando que las instrucciones no pueden leer valores de datos modificados, pero aún no confirmados por otras transacciones. Otras transacciones pueden seguir modificando, insertando o eliminando datos entre ejecuciones de instrucciones específicas dentro de la transacción actual, lo que da lugar a lecturas no repetibles o datos "fantasma".

Transacciones en SQL SERVER

En SQL Server el tipo de concurrencia es pesimista. El bloqueo se activa al modificar los datos; no al leerlos. Si queremos activarlo en su lectura con debemos usar la clausula WITH UPDLOCK.
1
	
SELECT * FROM TABLE WITH(UDPLOCK) WHERE ID = 1

El nivel del Aislamiento en SQL SERVER se indica con la instrucción SET TRANSACTION ISOLATION LEVEL. Por defecto es READ COMMITTED.
En SQL SERVER cuando indicamos READ COMMITTED, puede ser READ COMMITTED o READ COMMITED SNAPSHOT. Esto se determina en función de la configuración de la base de datos:

    READ COMMITED SNAPSHOT: La base de datos se encuentra con la configuración SET READ_COMMITTED_SNAPSHOT ON.
    READ COMMITTED: La base de datos se encuentra con la configuración SET READ_COMMITTED_SNAPSHOT OFF

En SQL AZURE solo existe READ COMMITTED SNAPSHOT. No se puede desactivar con la configuración SET READ_COMMITTED_SNAPSHOT OFF.




# Tools

bryteflow
SQL Server Change Tracking for real-time SQL Server Replication
https://bryteflow.com/sql-server-change-tracking-for-real-time-sql-server-replication/




# SQL Server Management Studio 


**Errores**
-------------------

Se realizo una instalacion de SQL Server Management Studio en el drive D, luego por errores no se puede acceder a este drive
- Al intentar volver instalar el SQL Server Management Studio en otro drive no se puede porque se obtiene un error "no existe el drive" 

Ir a "Registry Editor":
- Deleted "HKLM\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Management Studio"


