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

MVC

**Theme**
Personalizar la plantilla
https://docs.abp.io/en/abp/latest/UI/AspNetCore/Theming#implementing-a-theme

Abp posee Layout Hook Points, para inyectar contenido en los diferentes puntos de los layout
https://docs.abp.io/en/abp/latest/UI/AspNetCore/Layout-Hooks#layout-hook-points


Angular

-    Ng Bootstrap will be used as the UI component library.
-    Ngx-Datatable will be used as the datatable library.


**Theme**

Implementing a Theme
https://docs.abp.io/en/abp/latest/UI/Angular/Theming#implementing-a-theme



Using Angular Material Components With the ABP Framework
https://community.abp.io/articles/using-angular-material-components-with-the-abp-framework-af8ft6t9


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
