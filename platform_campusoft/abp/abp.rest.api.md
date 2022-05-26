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

<https://docs.abp.io/en/abp/latest/UI/Angular/Service-Proxies>

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

- Si no se pone el name en el atributo remoteservice, se utiliza el mismo nombre del modulo.

Otro ejemplo.

Generar proxy para un libreria que se encuentra en la carpeta "projects", en este caso la libreria se llama "@mre/administrative-unit"

abp generate-proxy  --module ModuleSample --api-name sample --target @mre/administrative-unit

Errores
------------

[Invalid Module] Backend module "app" does not exist in API definition.

angular.json file not found. You must run this command in the angular folder.

# API versioning

API Versioning System

ABP Framework integrates the ASPNET-API-Versioning feature and adapts to C# and JavaScript Static Client Proxies and Auto API Controller.
https://docs.abp.io/en/abp/5.2/API/API-Versioning


# Rest Client

Revision

- RemoteService
- RemoteServiceConfiguration
- volo.abp.http.client

La clase AbpHttpClientOptions, es una clase de configuracion para las url de los remoteservice

***IdentityModel***

Dll

- Volo.Abp.Http.Client.IdentityModel

Revisiones

- Existe una clase IdentityModelAuthenticationService, Volo.Abp.IdentityModel que realiza el flujo obtener token si no existe, o esta caducado.

La dll Volo.Abp.IdentityModel,

- Posee la clase IdentityClientConfiguration, que es la que posee los campos para configurar para generacion de token para los proxys, para consumir un remoteServiceName
- Posee CacheAbsoluteExpiration, Absolute expiration duration (as seconds) for the access token cache. Este tiempo es para el accessToken, y para obtener la informacion de configuracion OpenId Connect Discovery Document

# Dynamic C# API Clients

ABP can dynamically create C# API client proxies to call your remote HTTP services (REST APIs). In this way, you don't need to deal with HttpClient and other low level details to call remote services and get results.

<https://docs.abp.io/en/abp/latest/API/Dynamic-CSharp-API-Clients>

Para generar los proxy dinamicos, se utiliza el servicio "api-definition", del RemoteServices que se configure.

```
<host:Port>/api/abp/api-definition
```

Your interface should implement the IRemoteService interface to be automatically discovered. Since the IApplicationService inherits the IRemoteService interface, the IXService above satisfies this condition.

La configuracion de la generacion clientes dinamicos "proxys" de los servicios aplicacion, que estan expuestos como servicios remotos

Generar los proxy de los contratos "BookStoreApplicationContractsModule"

```
//Create dynamic client proxies
        context.Services.AddHttpClientProxies(
            typeof(BookStoreApplicationContractsModule).Assembly
        );

```

***Laboratorio***

Crear los contratos, en donde se consumen. Sin hacer referencial a la dll del servicio remoto que se va crear los proxys.

Estos contratos deben tener los mismos namaspace, que las definidos en el servicio remoto.

Generar el proxy de un contrato directamente, sin dependencias a los contratos (dll) del servicio remoto.

```
context.Services.AddHttpClientProxy(
  typeof(IServiceTrakingAppService),
  "ServiceTraking"
);
```


# referencias

How to Hide ABP Related Endpoints on Swagger UI
https://community.abp.io/posts/how-to-hide-abp-related-endpoints-on-swagger-ui-mb2w01fe
