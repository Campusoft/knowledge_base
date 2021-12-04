# dotnet 

Ejecutar en ambiente desarrollo

```
dotnet Gradebook.Register.dll run --environment="Development"
```

Ejecutar en ambiente de produccion
 
```
dotnet Gradebook.Register.dll run --environment="Production"
```




Establecer el Puerto diferente de 5000 (default), ejecutar dotnet

dotnet run --urls=http://localhost:5001/


Establecer ambiente produccion



```
set ASPNETCORE_ENVIRONMENT="Production"
dotnet Gradebook.Register.dll run --environment="Production"
```