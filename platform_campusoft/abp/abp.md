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
https://docs.abp.io/en/abp/latest/UI/Angular/Service-Proxies



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

# Event-Bus

ABP Framework provides two type of event buses;

- Local Event Bus is suitable for in-process messaging.
- Distributed Event Bus is suitable for inter-process messaging, like microservices publishing and subscribing to distributed events.
https://docs.abp.io/en/abp/latest/Event-Bus

***Personalizar***

IDistributedEventBus 


# Logs

Serilog


Permitir cambiar los nombres que se utilizan AbpSerilogMiddleware.  
- IOptions<AbpAspNetCoreSerilogOptions> options

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
# Fundamentals


***Cache***

Set the cache key prefix for the application.
https://docs.abp.io/en/abp/latest/Caching#abpdistributedcacheoptions



***Connection Strings***

Using Multiple Databases
https://docs.abp.io/en/abp/latest/Entity-Framework-Core-Migrations#using-multiple-databases


# Modules

## Account

## Setting Management

# Timing

TimeZone Setting

ABP Framework defines a setting, named Abp.Timing.Timezone, that can be used to set and get the time zone for a user, tenant or globally for the application. The default value is UTC


# Personalizacion

Obtener el codigo fuente de los modulos. (Opcion 1)

get-source (CLI option)
https://docs.abp.io/en/abp/latest/CLI#get-source


Customizing the Existing Modules
https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Guide




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



