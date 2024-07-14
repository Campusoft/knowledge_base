


#Model-Based Conventions




# Entity Framework .net Framework - Oracle


## Referencias


Sitio, en el cual existe varias formas establece la conexion a oracle, con diferentes drives
https://www.connectionstrings.com/oracle/


Existe varios ítems para realizar la configuración de Oracle / Entity Framework. (Indica algunos errores comunes) 
http://izzydev.net/2017/02/01/Porting-to-Oracle-with-Entity-Framework.html



## Errores

-------------

Al trabajar con .net framework, se presenta un error:

```
Type is not resolved for member 'Oracle.ManagedDataAccess.Client.OracleException,Oracle.ManagedDataAccess, Version=4.122.19.1, Culture=neutral, PublicKeyToken=89b483f429c47342'.
```

Solucion:

Se tiene que agregar dll al GAC. 

Installing Oracle.ManagedDataAccess.dll to GAC resolved my issue. 

- Ir a la carpeta en donde se encuentra los packages, en la solucion .net que se esta ejecutando. En esta carpeta se encuentra Oracle.ManagedDataAccess.
- Agregar dll al GAC con utilidad gacutil

Commando Referencia:

```
..\packages\Oracle.ManagedDataAccess.12.1.021\lib\net40>"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\gacutil.exe" /i Oracle.ManagedDataAccess.dll

```

Enlaces Mencionan la solucion:

https://entityframework.net/knowledge-base/32006884/entity-framework-seed-method-exception
https://itqna.net/questions/17838/entity-framework-oracle
https://support.aspnetzero.com/QA/Questions/4567/Oracle-Database





-------------


# Referencias

Tiene manuales, contenido varios temas Entity Framework .Net
https://www.entityframeworktutorial.net


### Evitar crear Annotation("SqlServer:Identity", "1, 1")
https://stackoverflow.com/questions/59673398/ef-core-always-create-annotationsqlserveridentity-1-1-on-add-migration