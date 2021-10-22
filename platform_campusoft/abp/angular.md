# Angular


Depended Packages

-    NG Bootstrap is used as UI component library.
-    NGXS is used as state management library.
-    angular-oauth2-oidc is used to support for OAuth 2 and OpenId Connect (OIDC).
-    Chart.js is used to create widgets.
-    ngx-validate is used for dynamic validation of reactive forms.
-    ngx-datatable. https://github.com/swimlane/ngx-datatable


# Utilizacion Proxy

```
abp generate-proxy
```
https://docs.abp.io/en/abp/latest/UI/Angular/Service-Proxies

El archivo ./proxy/index.ts, enlaza todas las subcarpetas que se han creado, estas carpetas se crean segun el namespace del api en el backend. Se exponen con nombres "as Person"

```
import * as Address from './address';
import * as Person from './person';
export { Address, Person };
```

Para ser utilizados se hace referencia a "@proxy", el cual esta configurado "tsconfig.json"

```
"paths": {
      "@proxy": ["src/app/proxy/index.ts"],
      "@proxy/*": ["src/app/proxy/*"]
    }
```
Para importar las clases generadas, por el proxy de la carpeta "/proxy/address", utilizar el objeto expuesto Address "export { Address, Person };"

import { <Entity>Dto,<Service> } from "@proxy/Address";


# Componentes UI

***abp-modal***

- Posee un comportamiento de dirty, sobre las modificaciones relacionadas en la forma, para solicitar confirmacion en salir sin guardar. 


# Localization 

The Localization key format consists of 2 sections which are Resource Name and Key. ResourceName::Key


https://docs.abp.io/en/abp/latest/UI/Angular/Localization

# Laboratorios


***Personalizar administracion usuarios (Frontend)***

Opcion 1:
No funciona la opcion agregar package con codigo. (Presenta error que no existe modulo). 
```
abp add-package @abp/ng.identity --with-source-code
```

Opcion 2:

Copiar el codigo del modulo, como una libreria, para utilizar en reemplazo a la dependencia del modulo configurado en package.json "dependencies"  

- Bajar el codigo del package desde https://github.com/abpframework/abp/tree/dev/npm/ng-packs/packages
- Package "@abp/ng.identity". Ruta: abp/npm/ng-packs/packages/identity/
- Copiar el modulo en la carpeta "projects", para trabajarlo como una libreria
- Ajustar angular.json y tsconfig.json
- Angular.Json
  - Agregar una nuevo proyecto "projects". Se puede copiar desde abp-github "abp/npm/ng-packs/angular.json", el bloque correspondiente al modulo que se esta copiando.
  - En abp, la carpeta de las librerias es "packages", cambiar a "projects". Ex. "sourceRoot": "packages/identity/src" a "sourceRoot": "projects/identity/src"
- tsconfig.json
  - Agregar en "paths", el nombre del modulo para utilizar el codigo fuente que existe en la carpeta "projects". 
```
	"@abp/ng.identity": ["projects/abp-ng.identity/src/public-api.ts"],
    "@abp/ng.identity/config": ["projects/abp-ng.identity/config/src/public-api.ts"],
    "@abp/ng.identity/proxy": ["projects/abp-ng.identity/proxy/src/public-api.ts"],
```


TODO.
- Generacion proxy, en una libreria. (Como volver a generar el proxy para una libreria con abp)


# Referencias

Web Application Development Tutorial
(Abp: Entity-Framework  + Angular)
https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=NG&DB=EF
