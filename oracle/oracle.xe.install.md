# Oracle XE
 


## Instalacion



Tip:  La instalcion oracle XE, ejecutarla sin la VPN Activa del Cliente. 
https://www.oracle.com/technetwork/es/articles/apex/instalar-bd-oraclexe18c-5487194-esa.html



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

Replace localhost with 0.0.0.0 

https://stackoverflow.com/questions/13358656/oracle-client-ora-12541-tnsno-listener 