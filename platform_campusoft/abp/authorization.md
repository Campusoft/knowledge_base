# Authorization


# Claims


***Claims Principal Factory***

Agregar claims personalizados.


ABP uses the IAbpClaimsPrincipalFactory service to create claims on authentication. 
- If you need to add your custom claims to the authentication ticket, you can implement the IAbpClaimsPrincipalContributor in your application.
https://docs.abp.io/en/abp/latest/Authorization#claims-principal-factory


# Permission


Abp. Aplica permisos con politicas autorizacion de .net core.

Existe la implementaciones
- PermissionsRequirement. "IAuthorizationRequirement" (El requerimiento para las politicas de permisos). Posee una lista permisos a evaluar, y si se aplican todos los permisos o unicamente uno
- PermissionsRequirementHandler. "AuthorizationHandler" Procesar el requerimiento PermissionsRequirement. Segun el PermissionsRequirement, se verifica si se posee todos los permisos o unicamnete uno. Utiliza IPermissionChecker, para verificar si el usuario  posee la lista permisos
- AbpAuthorizationPolicyProvider.  "DefaultAuthorizationPolicyProvider". Implementa IAuthorizationPolicyProvider, para crear las politicas en tiempo ejecucion, con el requerimiento PermissionsRequirement 


IAbpAuthorizationPolicyProvider


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


# IdentityServer

Proyecto => AbpAccountWebIdentityServerModule => AbpIdentityServerDomainModule.

El modulo AbpIdentityServerDomainModule, realiza la configuracion  IdentityServer.


```
var identityServerBuilder = services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        });
```		

Si no existe la implementacion IClientStore, se utiliza configuracion memoria, y lo que se encuentre configurado en "IdentityServer:Clients"

```
if (!services.IsAdded<IClientStore>())
{
	identityServerBuilder.AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"));
}
```

CreateStandardResourcesAsync
- Crea los IdentityResources estandar. Estos recursos de identidad tiene los claims correspondientes
  - OpenId Profile  Email Address Phone
  - IdentityResources para los roles "role". Claims: "role"








**Agregar claims IdentityServer**

Abp posee AbpClaimsService que agregar claims especificos a IdentityServer. Hereda de la clase DefaultClaimsService de  IdentityServer 
abp/modules/identityserver/src/Volo.Abp.IdentityServer.Domain/Volo/Abp/IdentityServer/AbpClaimsService.cs

Se agrega los siguientes claims
AbpClaimTypes.TenantId,
AbpClaimTypes.ImpersonatorTenantId,
AbpClaimTypes.ImpersonatorUserId,
AbpClaimTypes.Name,
AbpClaimTypes.SurName,
JwtRegisteredClaimNames.UniqueName,
JwtClaimTypes.PreferredUserName,
JwtClaimTypes.GivenName,
JwtClaimTypes.FamilyName,

Si se agregan Claims a la identidad en abp con el metodo de IAbpClaimsPrincipalFactory. Se debe configurar los nuevos claims  con  AbpClaimsServiceOptions para que estos claims se incluyan en los tokens que se generan con IdentityServer

ABP uses the IAbpClaimsPrincipalFactory service to create claims on authentication
https://docs.abp.io/en/abp/latest/Authorization#claims-principal-factory


```
//Agregar claim personalizados
            Configure<AbpClaimsServiceOptions>(options =>
            {
                options.RequestedClaims.AddRange(new[]{
                     BaseConsts.Claims.SelloSeguridad
                });
            });
```


# External provider authentication 


Abp, posee una clase abstract, para implementar fuentes externas de autentificacion:

- Volo.Abp.Identity.ExternalLoginProviderBase


En codigo Abp, en test, existe una clase "FakeExternalLoginProvider", que implementa la class abstract ExternalLoginProviderBase

- https://github.com/abpframework/abp/blob/dev/modules/identity/test/Volo.Abp.Identity.AspNetCore.Tests/Volo/Abp/Identity/AspNetCore/FakeExternalLoginProvider.cs

La configuracion para agregar proveedor externo autentificacion.

```
Configure<AbpIdentityOptions>(options =>
{
	options.ExternalLoginProviders.Add<FakeExternalLoginProvider>(FakeExternalLoginProvider.Name);
});
```


# Current User


ICurrentUser
ICurrentUser is the main service to get info about the current active user.

ICurrentUser works independently of how the user is authenticated or authorized. It seamlessly works with any authentication system that works with the current principal.

Para obtener el Id del usuario actual con ICurrentUser

```
Guid? userId = CurrentUser.Id;
```

Para obtener el userName del usuario actual  ICurrentUser
```
string userName = CurrentUser.UserName;
```

Verificar si el usuario se encuentra autentificado. 

```
CurrentUser.IsAuthenticated 
```

Si el usuario no se encuentra autentificado los valores seran nulos. 

Para ver todas las Propiedades del ICurrentUser
https://docs.abp.io/en/abp/4.4/CurrentUser#properties

Informacion adicional:
https://docs.abp.io/en/abp/4.4/CurrentUser

## Obtener usuario Angular

Se utiliza el servicio "ConfigStateService", para obtener informacion del usuario

Con este servicio, con una constante especifica "currentUser", se obtiene informacion usuario

```
const currentUser = this.configStateService.getOne("currentUser");
```

El objeto que se retorna es tipo "CurrentUserDto". Ver las propiedades "https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Mvc.Contracts/Volo/Abp/AspNetCore/Mvc/ApplicationConfigurations/CurrentUserDto.cs"

Mas detalles sobre ConfigStateService
https://docs.abp.io/en/abp/latest/UI/Angular/Config-State-Service


# Dynamic Permissions and Features


With ABP 7.0, we've introduced the dynamic permissions and dynamic features systems
- Basically, in the solution with ABP 7.0, all microservices serialize their own permission definitions and write them into a shared database on their application startup (with a highly optimized algorithm).
https://blog.abp.io/abp/ABP.IO-Platform-7.0-RC-Has-Been-Published#Dynamic%20Permissions%20and%20Features

Dynamic permissions
- Notas del diseno, implementado en abp. Para permisos distribuidos.
https://github.com/abpframework/abp/pull/13644

Authorization in a Distributed / Microservice System | .NET Conf 2022 
https://www.youtube.com/watch?v=DVqvRZ0w-7g

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