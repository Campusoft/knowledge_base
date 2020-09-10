# Host ASP.NET Core in a web farm


Si requiere que la aplicacion se encuentre en varios servidores/host, y esta aplicacion tiene autentificacion con cookies, se requiere compartir entre los servidores/host como se encripta las cookies. Para esto se utiliza "Data Protection"

Para compartir la key, para data protection existe varios formas. 
- Carpetas compartidas. "PersistKeysToFileSystem"
- Redis "Persisting keys with Redis"
- Otros

Configuracion estandar en Campusoft, para configurar "PersistKeysToFileSystem". Se lo realiza a traves de la clave "Security:DataProtection"


Reference:

https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/web-farm?view=aspnetcore-3.1
 
 



