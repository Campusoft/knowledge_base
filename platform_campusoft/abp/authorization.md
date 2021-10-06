# Authorization


# Claims


***Claims Principal Factory***

ABP uses the IAbpClaimsPrincipalFactory service to create claims on authentication. 
https://docs.abp.io/en/abp/latest/Authorization#claims-principal-factory


# Permission


Abp. Aplica permisos con politicas autorizacion de .net core.

Existe la implementaciones
- PermissionsRequirement. (El requerimiento para las politicas de permisos). Posee una lista permisos a evaluar, y si se aplican todos los permisos o unicamente uno
- PermissionsRequirementHandler. Procesar el requerimiento PermissionsRequirement. Segun el PermissionsRequirement, se verifica si se posee todos los permisos o unicamnete uno. Utiliza IPermissionChecker, para verificar si el usuario  posee la lista permisos



PermissionManagement
https://docs.abp.io/en/abp/latest/Modules/Permission-Management


Group
-	Permission 
-		Child permissions


Permission checking system is extensible. Any class derived from PermissionValueProvider (or implements IPermissionValueProvider) can contribute to the permission check. 
https://docs.abp.io/en/abp/latest/Authorization#permission-value-providers


- UserPermissionValueProvider checks if the current user is granted for the given permission. It gets user id from the current claims. User claim name is defined with the AbpClaimTypes.UserId static property. (Codigo: U)
- RolePermissionValueProvider checks if any of the roles of the current user is granted for the given permission. It gets role names from the current claims. Role claims name is defined with the AbpClaimTypes.Role static property. (Codigo: R)
- ClientPermissionValueProvider checks if the current client is granted for the given permission. This is especially useful on a machine to machine interaction where there is no current user. It gets the client id from the current claims. Client claim name is defined with the AbpClaimTypes.ClientId static property. (Codigo: C)


IPermissionStore

IsGrantedAsync(
	[NotNull] string name,
	[CanBeNull] string providerName,
	[CanBeNull] string providerKey
)

IPermissionStore is the only interface that needs to be implemented to read the value of permissions from a persistence source, generally a database system
https://docs.abp.io/en/abp/latest/Authorization#permission-store


Ejemplo
-  name. (Permiso)
-  providerName. PermissionValueProvider code.


***Varios***
AlwaysAllowAuthorizationService

AlwaysAllowAuthorizationService is a class that is used to bypass the authorization service. It is generally used in integration tests where you may want to disable the authorization system.




# Laboratorios


***Personalizar campos en usuarios (Backend)***

get-source (CLI option)
https://docs.abp.io/en/abp/latest/CLI#get-source


- Bajar codigo modulo Volo.Identity.  

```
abp get-source  Volo.Identity
```

Este commando bajo el codigo fuente del modulo, todos sus componentes "Blazor,MongoDB,AspNetCore,Web"

- Se debe enlazar los proyectos existentes a los proyectos del modulo "Volo.Identity", quitando las referencias a nuget del modulo "Volo.Identity"

- Agregar los campos que se requieren a la entidad IdentityUser "Volo.Abp.Identity.Domain"

- Generar las migraciones con los cambios realizados en la entidad


---------------------------

Error:


```
System.IO.FileLoadException: Could not load file or assembly 'Volo.Abp.Identity.Application.Contracts, Version=4.4.3.0, Culture=neutral, PublicKeyToken=null'. The located assembly's manifest definition does not match the assembly reference. (0x80131040)
File name: 'Volo.Abp.Identity.Application.Contracts, Version=4.4.3.0, Culture=neutral, PublicKeyToken=null'
```

Se debe agregar las propiedades de versionamiento a cada proyecto del modulo agregado. 
- Utilizar un archivo centralizado. Ejemplo: "common.abp.props", y luego importarlo en cada proyecto 

```
  <Import Project="..\..\common.abp.props" />
```

El contenido del common.abp.props, copiar de codigo fuente del abp. "abp/common.props"



----------------------------------


***Personalizar Flujo auntentificacion(Backend)***

- Bajar codigo modulo Volo.Account.  

```
abp get-source  Volo.Account
```


# Referencias

# Revisiones


Servicio para obtener informacion aplicacion para Abp.
abp/framework/src/Volo.Abp.AspNetCore.Mvc/Volo/Abp/AspNetCore/Mvc/ApplicationConfigurations/AbpApplicationConfigurationAppService.cs

https://localhost:44377/api/abp/application-configuration

IAbpAuthorizationService: IAuthorizationService


abp/framework/src/Volo.Abp.Authorization/Volo/Abp/Authorization/AbpAuthorizationService.cs


Reemplazar mecanismo autorizacion

abp/framework/src/Volo.Abp.Authorization/Microsoft/Extensions/DependencyInjection/AbpAuthorizationServiceCollectionExtensions.cs 

services.Replace(ServiceDescriptor.Singleton<IAuthorizationService, AlwaysAllowAuthorizationService>());
services.Replace(ServiceDescriptor.Singleton<IAbpAuthorizationService, AlwaysAllowAuthorizationService>());
services.Replace(ServiceDescriptor.Singleton<IMethodInvocationAuthorizationService, AlwaysAllowMethodInvocationAuthorizationService>());


Interfaces.
- Obtiene las politicas que tiene un usuario. 
- IPermissionDefinitionManager 