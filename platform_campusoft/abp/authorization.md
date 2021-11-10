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

# Convensiones


"Module"."Entity/Process"."Action"

Ejemplo: El modulo de administracion de personas. Permisos para crear direcciones

- "PersonManagement.PersonAddress.Create"
- "PersonManagement.PersonAddress.Edit"
- "PersonManagement.PersonAddress.Delete"


AbpIdentity.Roles.Create

# Current User

ICurrentUser
ICurrentUser is the main service to get info about the current active user.

ICurrentUser works independently of how the user is authenticated or authorized. It seamlessly works with any authentication system that works with the current principal 

https://docs.abp.io/en/abp/4.4/CurrentUser



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