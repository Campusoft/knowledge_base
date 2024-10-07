# Sqlplus


Installar

Oracle Instant Client Downloads
https://www.oracle.com/database/technologies/instant-client/downloads.html


Connection

sqlplus "username/password@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=hostname)(PORT=port))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=servicename)))"


How to connect sqlplus without tnsnames.ora 
- 1) EZCONNECT

```
sqlplus username/password@[//]host[:port][/service_name]
```

2) TNS Connect String

```
sqlplus "username/password@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=hostname)(PORT=port))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=servicename)))"
```
 
http://nimishgarg.blogspot.com/2014/10/how-to-connect-sqlplus-without.html