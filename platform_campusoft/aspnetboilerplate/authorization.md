# aspnetboilerplate Authorization


Agregar fuentes autentificacion externas. 

Ejemplo agregar la autentificacion externa "FooAuthenticationSource"

Configuration.Modules.Zero().UserManagement.ExternalAuthenticationSources.Add<FooAuthenticationSource>()

Se puede implementar autentificacion externas heredando de DefaultExternalAuthenticationSource

```
DefaultExternalAuthenticationSource<TTenant, TUser>
```