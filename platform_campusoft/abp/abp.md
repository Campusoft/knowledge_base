# Abp

# Entity

- Entities with Composite Keys
- All these base classes also have ...WithUser pairs, like FullAuditedAggregateRootWithUser<TUser> and FullAuditedAggregateRootWithUser<TKey, TUser>. This makes possible to add a navigation property to your user entity

<https://docs.abp.io/en/abp/latest/Entities>

# API REST

[API REST](abp.rest.api.md)

# UI

themes.md

Authorization in Angular UI

- OAuth is preconfigured in Angular application templates
<https://docs.abp.io/en/abp/latest/UI/Angular/Authorization>

# API Javascript

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

<https://docs.abp.io/en/abp/latest/Virtual-File-System>

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

No es necesario (Revision). El proyecto, que posee los archivos de localizaciones, debe tener una referencia a "Microsoft.Extensions.FileProviders.Embedded", para que funcione ?

El archivo del proyecto debe tener "GenerateEmbeddedFilesManifest" a true, en alguna etiqueta "PropertyGroup"

```  
 <GenerateEmbeddedFilesManifest>true</GenerateEmbeddedFilesManifest>
```
  
TODO:

- Como establecer los nombres en los campos, en validaciones dataannotation

```
Tu Solicitud no es valida.
Los siguientes errores fueron detectados durante la validación. - El campo AddressTypeId es requerido.
```
  
DataAnnotation

- Utilizar atributo Display, para establecer la clave de localizacion
  - Ej: [Display(Name = "NameFieldKeyLocalizacion")]
  - Con el NameFieldKeyLocalizacion, se puede establecer sus valores  en los archivos de localizacion que existen.

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
<https://docs.abp.io/en/abp/latest/Caching#abpdistributedcacheoptions>

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
<https://docs.abp.io/en/abp/latest/Entity-Framework-Core-Migrations#using-multiple-databases>

***Dependency Injection***

ABP's Dependency Injection system is developed based on Microsoft's dependency injection extension library (Microsoft.Extensions.DependencyInjection nuget package)

Autofac Integration

Autofac is one of the most used dependency injection frameworks for .Net. It provides some advanced features compared to .Net Core standard DI library, like dynamic proxying and property injection.

<https://docs.abp.io/en/abp/latest/Autofac-Integration>

# Exception-Handling

# BLOB Storing

The ABP Framework has already the following storage provider implementations:

- File System: Stores BLOBs in a folder of the local file system, as standard files.
- Database: Stores BLOBs in a database.
- Azure: Stores BLOBs on the Azure BLOB storage.
- Aliyun: Stores BLOBs on the Aliyun Storage Service.
- Minio: Stores BLOBs on the MinIO Object storage.
- Aws: Stores BLOBs on the Amazon Simple Storage Service.
<https://docs.abp.io/en/abp/latest/Blob-Storing>

# Modules

## Account

## Setting Management

Setting Management UI. (Indica como construir UI en diferentes tecnologias)

- MVC UI
- Blazor UI
- Angular UI
<https://docs.abp.io/en/abp/4.4/Modules/Setting-Management>

Referencias UI

- Configuracion Email. (Abp-Code)
<https://github.com/abpframework/abp/tree/rel-4.4/npm/ng-packs/packages/setting-management/config/src/components/email-setting-group>

***Laboratorio***

Agregar un tab

```
ng generate component config/foo-settings
```

## Volo.Abp.Ldap

Utiliza ldap4net

- Cross platform LdapForNet library is used for Windows LDAP authentication.
<https://github.com/flamencist/ldap4net>

<https://docs.abp.io/en/commercial/latest/modules/account/ldap>

# Timing

TimeZone Setting

ABP Framework defines a setting, named Abp.Timing.Timezone, that can be used to set and get the time zone for a user, tenant or globally for the application. The default value is UTC

# Background Jobs

# Background Workers

Background workers are simple independent threads in the application running in the background. Generally, they run periodically to perform some tasks

- AbpBackgroundWorkersModule

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
<https://docs.abp.io/en/abp/latest/CLI#options>

El proyecto abp - cli, se encuentra en abp\framework\src\Volo.Abp.Cli

# Versiones

**5.3.x**
- Batch Publish Events from Outbox to the Event Bus
- OpenIddict Module & Keycloack Integration
https://blog.abp.io/abp/ABP.IO-Platform-5.3-RC-Has-Been-Published

**5.2.x**

- API Versioning
- libs Folder Has been Removed from Source Control
https://blog.abp.io/abp/ABP.IO-Platform-5-2-RC-Has-Been-Published

**5.0.x**
- Transactional Outbox & Inbox for the Distributed Event Bus
https://blog.abp.io/abp/ABP-IO-Platform-5.0-RC-1-Has-Been-Released

**4.4.x**

- angular: 12.x



# Referencias

<https://abp.io>

**Generation Code**

AbpHelper.GUI

Es una GUI para usar AbpHelper.CLI.

Providing code generation and more features to help you develop applications and modules with the ABP framework.
<https://github.com/EasyAbp/AbpHelper.GUI>

Providing code generation and more features to help you develop applications and modules with the ABP framework.
Utiliza:

- Scriban
- Elsa (Workflow)

Aplica templates tanto al contenido, como para los nombres de los archivos.
<https://github.com/EasyAbp/AbpHelper.CLI>

Elementos considerar

- Agregar DTO, Servicios Aplicacion, Crea un repositorio para la entidad, Crear permisos, Agrega clase en DbContexto.
- Las entidades debe tener su propio archivo.
- Las plantillas actuales (Originales), generan subCarpetas para los archivos dentro de las carpetas. Si se requiere cambiar este comportamiento se necesita cambiar los archivos templates originales y quitar referencias a {EntityInfo.RelativeDirectory}
- Las configuraciones que soportan los comandos. Se puede revisar codigo fuente el archivo de opciones del commando a ejecutarse. Ejemplo commando "Crud", el archivo opciones es CrudCommandOption.
  - Tambien con help. Ej. Commando generate crud "abphelper generate  crud --help"
- No existe plantillas para la UI - Angular. (Ref: <https://github.com/EasyAbp/AbpHelper.CLI/issues/13>)

Para sobrescribir los templates que existen en el proyecto se debe modificar la configuracion AbpVirtualFileSystemOptions para considerar AddPhysical

# Revisiones

(Como utilizar varias base de datos)
Using Multiple Databases
<https://docs.abp.io/en/abp/latest/Entity-Framework-Core-Migrations#using-multiple-databases>

Las clases AuditedAggregateRoot. Posee

- ConcurrencyStamp
- ExtraProperties
