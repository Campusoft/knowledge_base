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

# Https 

Hosting ASP.NET Core images with Docker over HTTPS
https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0 

Hosting ASP.NET Core images with Docker Compose over HTTPS
https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0
