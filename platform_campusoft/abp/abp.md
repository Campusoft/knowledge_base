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

# Versiones

**4.4.x**

- angular: 12.x



# Referencias

https://abp.io