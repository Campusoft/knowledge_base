# Servicios REST

## Estandares, Convenciones


## Seguridad

- Token JWT
- Tipos autentificacion HTTP. Basic Authentication, NTLM/Digest


## Documentacion

Agregar swagger/openApi a los proyectos.

Una principales razones, para generar metadatos/documentacion con swagger/openAPI, es la habilidad para que otros herramientas automaticamente consuman e integren las APIs.


Ref.

[ASP.NET Core web API help pages with Swagger / OpenAPI](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.1)

## Versionamiento

## Proxy / OpenAPI


Generacion automatica de rest api desde servicios aplicacion, al utilizar ASP.NET Boilerplate, facilita exponer los servicios aplicacion como rest api. (Generacion dinamica)

Para tener un mejor control de que servicios aplicacion se exponen como REST, existe la interfas "IGenerateDynamicApi", que marca un servicio aplicacion para consideralo en la generacion.

```
FooAppService : IFooAppService, IGenerateDynamicApi
```

Con esta interfas, se puede configurar que se genere todos los servicios aplicacion de un modulo "FooApplicationModule", que tengan esta interfas "IGenerateDynamicApi" 

```
 //Api Dynamic
Configuration.Modules.AbpAspNetCore()
	.CreateControllersForAppServices(
		typeof(FooApplicationModule).GetAssembly(),
		 moduleName: "app"
	)
	.Where(type => typeof(IGenerateDynamicApi).IsAssignableFrom(type));

```


ASP.NET Boilerplate provides the infrastructure to create application services. If you want to expose your application services to remote clients as controllers (as previously done using dynamic web api), you can easily do that with a simple configuration in the PreInitialize method of your module. 

https://aspnetboilerplate.com/Pages/Documents/AspNet-Core#application-services-as-controllers

### Referencias

[Microsoft REST API Guidelines](https://github.com/Microsoft/api-guidelines/blob/vNext/Guidelines.md)


[Abp - Building Dynamic Web API Controllers](https://aspnetboilerplate.com/Pages/Documents/Dynamic-Web-API)


