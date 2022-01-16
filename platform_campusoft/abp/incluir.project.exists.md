# Incluir Abp, en soluciones existentes

Se requiere la integracion Autofac, para utilizar abp. Sea un proyecto asp.net o console.

https://docs.abp.io/en/abp/latest/Autofac-Integration#asp-net-core-application



***Localizaciones.***

Utilizar  app.UseAbpRequestLocalization();, para establecer el lenguaje.

- AbpRequestLocalizationMiddleware

Error
------------------
AbpException: Undefined setting: Abp.Localization.DefaultLanguage

Este error se debe porque no existe las dependencias a Volo.Abp.Autofac, la cual es requerida para utilizar abp.


***Librerias***

- Volo.Abp.Core
- Volo.Abp.Ddd.Domain
- Volo.Abp.Ddd.Application
- Volo.Abp.AspNetCore.Mvc. (Api, MVC, )
  - Posee app.InitializeApplication(), para inicializar una aplicacion en el Startup
  - En Startup.ConfigureServices, llamar a "services.AddApplication<NOMBREModule>()". NOMBREModule, es el modulo que se encuentra en el proyecto principal el host. 
  - En Startup.Configure, llamar a " app.InitializeApplication();"
  

***AbpAspNetCoreMvcModule***

Revision:

AbpAspNetCoreModule

  

# Auditoria

AbpAuditingMiddleware 
- Verifica si la auditoria esta activa
- Verifica si la URL, esta incluida para auditar
- En un bloque de IAuditingManager 
https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore/Volo/Abp/AspNetCore/Auditing/AbpAuditingMiddleware.cs

IAuditingManager 
- https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Auditing/Volo/Abp/Auditing/IAuditingManager.cs
- Returna IAuditLogSaveHandle

IAuditLogSaveHandle
- DisposableSaveHandle

IAuditingStore 
  
# Laboratorio

***Incluir en una solucion MVC***

Minimo las librerias que deben existir
- Volo.Abp.AspNetCore.Mvc
- Volo.Abp.Autofac

Elementos
- Crear un modulo abp "NombreModule". Agregar dependencias Volo.Abp.AspNetCore.Mvc,Volo.Abp.Autofac
- En el proyecto host "pricipal":
  - En Startup.ConfigureServices, llamar a "services.AddApplication<NombreModule>()". NombreModule, es el modulo que se encuentra en el proyecto principal el host. 
  - En Startup.Configure, llamar a " app.InitializeApplication();"
- Agregar .UseAutofac(); en CreateHostBuilder //Integrate Autofac!


***En una solucion. Clean.Architecture.Solution.Template***

dotnet new --uninstall Clean.Architecture.Solution.Template

dotnet new --install Clean.Architecture.Solution.Template::1.1.5


Dependencias
- NSwag.AspNetCore. (Configurado en "HostAplicacion"/api)
- Posee IPipelineBehavior, para colocar tuberias (pipe), en los flujos de MediatR. 
  - AuthorizationBehaviour. (Para autorizacion, con mediatR)
https://github.com/jasontaylordev/CleanArchitecture

# Errores


Volo.Abp.AspNetCore.Mvc.AbpDataAnnotationAutoLocalizationMetadataDetailsProvider.CreateDisplayMetadata(DisplayMetadataProviderContext context)

MvcDataAnnotationsLocalizationOptions. Provides programmatic configuration for DataAnnotations localization in the MVC framework.

Al poner la configuracion de localizaciones, no se soluciona el inconveniente
----------------------

System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation.
---> Volo.Abp.AbpException: Can not find a resource with given type: Incluir.abp.Domain.Localization.IncluirAbpResource, Incluir.abp.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
   at Volo.Abp.Localization.LocalizationResourceDictionary.Get[TResource]()


***Plantilla MVC, Visual studio***


InvalidOperationException: No service for type 'Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal.IndexModel`1[Microsoft.AspNetCore.Identity.IdentityUser]' has been registered.
