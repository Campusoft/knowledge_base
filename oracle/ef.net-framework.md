

## Errores



-------------

```
Type is not resolved for member 'Oracle.ManagedDataAccess.Client.OracleException,Oracle.ManagedDataAccess, Version=4.122.19.1, Culture=neutral, PublicKeyToken=89b483f429c47342'.
```

Solucion:

Installing Oracle.ManagedDataAccess.dll to GAC resolved my issue. 


- Ir a la carpeta en donde se encuentra los packages, solucion .net que se esta ejecutando. En esta carpeta se encuentra Oracle.ManagedDataAccess.
- Agregar dll al GAC con gacutil

Commando

```
..\packages\Oracle.ManagedDataAccess.12.1.021\lib\net40>"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\gacutil.exe" /i Oracle.ManagedDataAccess.dll

```

Enlaces:








-------------
