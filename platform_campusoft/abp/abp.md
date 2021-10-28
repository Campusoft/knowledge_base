# Abp


# Entity

- Entities with Composite Keys
- All these base classes also have ...WithUser pairs, like FullAuditedAggregateRootWithUser<TUser> and FullAuditedAggregateRootWithUser<TKey, TUser>. This makes possible to add a navigation property to your user entity
https://docs.abp.io/en/abp/latest/Entities


# API REST

Service Proxy Generation

ABP CLI provides generate-proxy command that generates client proxies for your HTTP APIs to make easy to consume your HTTP APIs from the client side

Service Proxies
Posee su propio mecaniso, por algunas desventajas al utilizar NSWAG para generar proxys
```
abp generate-proxy
```


Options:

-m|--module <module-name>          (default: 'app') The name of the backend module you wish to generate proxies for.
-a|--api-name <module-name>        (default: 'default') The name of the API endpoint defined in the /src/environments/environment.ts.
-s|--source <source-name>          (default: 'defaultProject') Angular project name to resolve the root namespace & API definition URL from.
-t|--target <target-name>          (default: 'defaultProject') Angular project name to place generated code in.
-p|--prompt                        Asks the options from the command line prompt (for the missing options)

https://docs.abp.io/en/abp/latest/UI/Angular/Service-Proxies


***Laboratorio***

Generar proxy para el modulo "AbpFeatureManagement", y el remoto servicio "featureManagement"

abp generate-proxy   --api-name AbpFeatureManagement --module featureManagement

El Api "/api/abp/api-definition", genera informacion de los diferentes modulos, y nombres remotos servicios

```
"modules": {
	"featureManagement": {
      "rootPath": "featureManagement",
      "remoteServiceName": "AbpFeatureManagement",
```

Informacion:

- El modulo, se genera a partir del area en el controller que esta marcado como remoto servicio.
- El servicio remoto se forma por el name en el atributo remoteServiceName

Ejemplo:

El modulo sera "ModuleFoo".

```
    [RemoteService(Name = "Foo")]
    [Area("ModuleFoo")]
    [Route("api/foo/sample")]
    public class SampleController : MyProjectNameController
```

- Si no se pone ell name en el atributo remoteservice, se utiliza el mismo nombre del modulo.

Otro ejemplo.

Generar proxy para un libreria que se encuentra en la carpeta "projects", en este caso la libreria se llama "@mre/administrative-unit"

abp generate-proxy  --module ModuleSample --api-name sample --target @mre/administrative-unit

Errores
------------

[Invalid Module] Backend module "app" does not exist in API definition.


angular.json file not found. You must run this command in the angular folder.


# Dynamic C# API Clients 

# UI

themes.md


Authorization in Angular UI
- OAuth is preconfigured in Angular application templates
https://docs.abp.io/en/abp/latest/UI/Angular/Authorization


# API Javascript

import { RestService } from '@abp/ng.core';

RestService
- Posee  handleError
- Configurarion skipHandleError
abp/npm/ng-packs/packages/core/src/lib/services/rest.service.ts


**Use @abp/ng.core**
Dependencia
ngxs

Instalar
- npm i @abp/ng.core

Tener archivo configuracion
- environment.ts

Configurar Module
```
import { CoreModule } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import { environment } from '../environments/environment';

imports: [
	...
	CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    ...
   ]
```




# Logs

Serilog


Permitir cambiar los nombres que se utilizan AbpSerilogMiddleware.  
- IOptions<AbpAspNetCoreSerilogOptions> options

- _options.EnricherPropertyNames.TenantId
- _options.EnricherPropertyNames.UserId
- _options.EnricherPropertyNames.ClientId
- _options.EnricherPropertyNames.CorrelationId
abp/framework/src/Volo.Abp.AspNetCore.Serilog/Volo/Abp/AspNetCore/Serilog/AbpSerilogMiddleware.cs


# Virtual File System

It's mainly used to embed (js, css, image..) files into assemblies and use them like physical files at runtime.
 
https://docs.abp.io/en/abp/latest/Virtual-File-System

Override Physical Files

Para permitir sobrescribir los files embebidos, con los fisicos, se debe configurar con "AbpVirtualFileSystemOptions" en las configuracion del modulo "ConfigureServices"

Codigo para agregar PhysicalFileProvider, en VirtualFileSystem

```
public override void ConfigureServices(ServiceConfigurationContext context)
{
	var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

   
	Configure<AbpVirtualFileSystemOptions>(options =>
	{
		options.FileSets.AddPhysical(hostEnvironment.ContentRootPath);
	});
	
	...
}
```			

# Globalization and localization

		

		

# Fundamentals


***Cache***

Permite establecer configuraciones:

- Tener una configuracion global. AbpDistributedCacheOptions.DistributedCacheEntryOptions. Utiliza configuracion nativo .net DistributedCacheEntryOptions
- Tener configuraciones por cada cache. AbpDistributedCacheOptions.CacheConfigurators. Utiliza configuracion nativo .net DistributedCacheEntryOptions 
- Aplicar un prefijo a todos las claves de cache que se utilicen. AbpDistributedCacheOptions.KeyPrefix

```
Configure<AbpDistributedCacheOptions>(options =>
{
    options.KeyPrefix = ...
});

```

Set the cache key prefix for the application.
https://docs.abp.io/en/abp/latest/Caching#abpdistributedcacheoptions

Laboratorio
- Establecer una clave de cache, explicitamente, sin tener los prefijos y otros elementos que se agraban por cada componente que utiliza abp-cache.


"c:TemplateCacheItem.Name.Explicito,k:MyProjectName:tp:STRING"
"c:<Nombre-Cache-Item>,k:<Prefjijo-Configurado>:<Key>"

```
 var normalizedKey = $"c:{args.CacheName},k:{DistributedCacheOptions.KeyPrefix}{args.Key}";
```



El Nombre-Cache-Item, se puede establecer con el atributo "CacheName" en la clase del CacheItem, si no se establece se considera el namaspace.type de la clase



***Connection Strings***

Using Multiple Databases
https://docs.abp.io/en/abp/latest/Entity-Framework-Core-Migrations#using-multiple-databases


# Modules

## Account

## Setting Management

Setting Management UI. (Indica como construir UI en diferentes tecnologias)
- MVC UI
- Blazor UI
- Angular UI
https://docs.abp.io/en/abp/4.4/Modules/Setting-Management


Referencias UI
- Configuracion Email. (Abp-Code)
https://github.com/abpframework/abp/tree/rel-4.4/npm/ng-packs/packages/setting-management/config/src/components/email-setting-group


***Laboratorio***

Agregar un tab

```
ng generate component config/foo-settings
```

## Volo.Abp.Ldap



# Timing

TimeZone Setting

ABP Framework defines a setting, named Abp.Timing.Timezone, that can be used to set and get the time zone for a user, tenant or globally for the application. The default value is UTC




# Plantillas

Posee plantillas para sql-server, postgres, mysql, oracle


***Errores / Angular***

```
[2/4] Fetching packages...
error @angular/animations@12.0.5: The engine "node" is incompatible with this module. Expected version "^12.14.1 || >=14.0.0". Got "12.14.0"
error Found incompatible module.
info Visit https://yarnpkg.com/en/docs/cli/install for documentation about this command.
```

***Plantillas personalizadas***

Abp-Cli, permite trabajar con plantillas personalizadas. El proceso busca las siguientes palabras claves:


- MyCompanyName. (Nombre inicial del namespace)
- MyProjectName. (Nombre del proyecto)

YourCompany.YourProduct.YourModule 


Las palabras claves que seran reemplazadas.
MyCompanyName.MyProjectName


public MyProjectNameClass {


Permite generar proyecto desde plantillas personalizadas:

Ejemplo: Generar proyecto de tipo consola, con la plantilla personalizada que se encuentra en la carpeta "C:\proyectos\abp-templates-custom". El Abp-Cli, buscara en la carpeta con el nombre <TIPO>-<VERSION>.zip. 

Para el tipo consola, con la version 4.4.3, se buscara el archivo  "console-4.4.3.zip"

El commando completo para el ejemplo sera:

```
abp new Acme.MyConsoleApp -t console --template-source C:\proyectos\abp-templates-custom 
```

Options Ab Cli:
https://docs.abp.io/en/abp/latest/CLI#options

El proyecto abp - cli, se encuentra en abp\framework\src\Volo.Abp.Cli



# Versiones

**4.4.x**

- angular: 12.x



# Referencias

https://abp.io


**Generation Code**

AbpHelper.GUI

Es una GUI para usar AbpHelper.CLI.

Providing code generation and more features to help you develop applications and modules with the ABP framework. 
https://github.com/EasyAbp/AbpHelper.GUI

Providing code generation and more features to help you develop applications and modules with the ABP framework. 
Utiliza:
- Scriban
- Elsa (Workflow) 

Aplica templates tanto al contenido, como para los nombres de los archivos. 
https://github.com/EasyAbp/AbpHelper.CLI




Elementos considerar
- Agregar DTO, Servicios Aplicacion, Crea un repositorio para la entidad, Crear permisos, Agrega clase en DbContexto.
- Las entidades debe tener su propio archivo. 
- Las plantillas actuales (Originales), generan subCarpetas para los archivos dentro de las carpetas. Si se requiere cambiar este comportamiento se necesita cambiar los archivos templates originales y quitar referencias a {EntityInfo.RelativeDirectory}
- Las configuraciones que soportan los comandos. Se puede revisar codigo fuente el archivo de opciones del commando a ejecutarse. Ejemplo commando "Crud", el archivo opciones es CrudCommandOption.
   -  Tambien con help. Ej. Commando generate crud "abphelper generate  crud --help"
- No existe plantillas para la UI - Angular. (Ref: https://github.com/EasyAbp/AbpHelper.CLI/issues/13)

Para sobrescribir los templates que existen en el proyecto se debe modificar la configuracion AbpVirtualFileSystemOptions para considerar AddPhysical

# Revisiones


 


(Como utilizar varias base de datos)
Using Multiple Databases
https://docs.abp.io/en/abp/latest/Entity-Framework-Core-Migrations#using-multiple-databases



