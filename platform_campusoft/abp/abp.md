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

# Plantillas

Posee plantillas para sql-server, postgres, mysql, oracle

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



# Revisiones


Personalizar los modulos existentes.
Backend.
- Module: Usuarios

```
abp get-source Volo.Users
```

get-source (CLI option)
https://docs.abp.io/en/abp/latest/CLI#get-source

(Como utilizar varias base de datos)
Using Multiple Databases
https://docs.abp.io/en/abp/latest/Entity-Framework-Core-Migrations#using-multiple-databases


***Revision AbpHelper.CLI***

Elementos considerar
- Agregar DTO, Servicios Aplicacion, Crea un repositorio para la entidad, Crear permisos, Agrega clase en DbContexto.
- Las entidades debe tener su propio archivo. 
- Las plantillas actuales (Originales), generan subCarpetas para los archivos dentro de las carpetas. Si se requiere cambiar este comportamiento se necesita cambiar los archivos templates originales y quitar referencias a {EntityInfo.RelativeDirectory}
- Las configuraciones que soportan los comandos. Se puede revisar codigo fuente el archivo de opciones del commando a ejecutarse. Ejemplo commando "Crud", el archivo opciones es CrudCommandOption.
   -  Tambien con help. Ej. Commando generate crud "abphelper generate  crud --help"
- No existe plantillas para la UI - Angular. (Ref: https://github.com/EasyAbp/AbpHelper.CLI/issues/13)

Para sobrescribir los templates que existen en el proyecto se debe modificar la configuracion AbpVirtualFileSystemOptions para considerar AddPhysical