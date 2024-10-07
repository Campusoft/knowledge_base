# Oracle XE
 


## Instalacion



Tip:  La instalcion oracle XE, ejecutarla sin la VPN Activa del Cliente. 
https://www.oracle.com/technetwork/es/articles/apex/instalar-bd-oraclexe18c-5487194-esa.html


## Tips Instalacion

- Tener desactivada VPNs (Clientes, u otras). (Al tener estas activas la instalacion puede causar configuraciones erroneas en los Listener Oracle)


## Errores


Status : Failure -Test failed: Listener refused the connection with the following error:
ORA-12514, TNS:listener does not currently know of service requested in connect descriptor


Buscar inforamcion 
http://logic.edchen.org/how-to-resolve-ora-12514-tns-listener-does-not-currently-know-of-service-requested-in-connect-descriptor/
 
 

Oracle client ORA-12541: TNS:no listener [closed] 
 
Step 1 - Edit listener.ora

This file is located in:

    Windows: %ORACLE_HOME%\network\admin\listener.ora.
    Linux: $ORACLE_HOME/network/admin/listener.ora

Replace localhost with 0.0.0.0 or COMPUTER_NAME

https://stackoverflow.com/questions/13358656/oracle-client-ora-12541-tnsno-listener 


LISTENER =
  (DESCRIPTION_LIST =
    (DESCRIPTION =
      (ADDRESS = (PROTOCOL = IPC)(KEY = EXTPROC1521))
      (ADDRESS = (PROTOCOL = TCP)(HOST = 0.0.0.0)(PORT = 1521))
    )
  )

Step 2. Restart oracle services

- OracleServiceXE
- OracleOraDB18Home1TNSListener	

Step 3. Execute lsnrctl status to check the status of listener

Step 4. Test Connection on SqlDeveloper


# Docker





Oracle Database XE Release 21c (21.3.0.0) Docker Image Documentation
https://container-registry.oracle.com/ords/f?p=113:4:108829676329776:::4:P4_REPOSITORY,AI_REPOSITORY,AI_REPOSITORY_NAME,P4_REPOSITORY_NAME,P4_EULA_ID,P4_BUSINESS_AREA_ID:803,803,Oracle%20Database%20Express%20Edition,Oracle%20Database%20Express%20Edition,1,0&cs=3npWP3oWiSsAMSR_0LmvanI3exZOwFYWHUAedx9Uw4AzQ0yKw0YgcUa--dpdBiXLkeQvWxSvOVYBF4PJ6hTk92w

```
docker run -d --name <oracle-db>
  container-registry.oracle.com/database/express:21.3.0-xe
```

Ejemplo
- Con la clave demo123

```
docker run --name oracle-db  -p 1521:1521 -p 5500:5500  -e ORACLE_PWD=demo123 container-registry.oracle.com/database/express:21.3.0-xe
```


