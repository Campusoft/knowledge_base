# Angular


Depended Packages

-    NG Bootstrap is used as UI component library. https://ng-bootstrap.github.io
-    NGXS is used as state management library.
-    angular-oauth2-oidc is used to support for OAuth 2 and OpenId Connect (OIDC).
-    Chart.js is used to create widgets.
-    ngx-validate is used for dynamic validation of reactive forms.
-    ngx-datatable. https://github.com/swimlane/ngx-datatable


# Utilizacion Proxy


```
abp generate-proxy
```


Options:

-m|--module <module-name>          (default: 'app') The name of the backend module you wish to generate proxies for.
-a|--api-name <module-name>        (default: 'default') The name of the API endpoint defined in the /src/environments/environment.ts.
-s|--source <source-name>          (default: 'defaultProject') Angular project name to resolve the root namespace & API definition URL from.
-t|--target <target-name>          (default: 'defaultProject') Angular project name to place generated code in.
-p|--prompt                        Asks the options from the command line prompt (for the missing options)

https://docs.abp.io/en/abp/latest/UI/Angular/Service-Proxies


Ejemplo:

Generar el proxy del modulo Person:

abp generate-proxy  --module Person --api-name Person 

--api-name. Se obtiene de /api/abp/api-definition, del valor remoteServiceName. Si el api-name posee otra direccion, esta va estar configurada /src/environments/environment.ts
--module. Se obtiene de /api/abp/api-definition, 

Datos en /src/environments/environment.ts.
-- 

```
 apis: {
    default: {
      url: 'https://localhost:44323',
      rootNamespace: 'MyCompanyName.MyProjectName',
    },
	Person: {
      url: 'https://localhost:44362',
      rootNamespace: 'Foo.Bar.Person',
    },
  },
```

Datos en /api/abp/api-definition:

```
"Person": {
      "rootPath": "Foo.Bar.Person",
      "remoteServiceName": "Person",
	  
```
---------------------------------

Generar el proxy de un modulo Abp. 

abp generate-proxy  --module identity


Datos en /api/abp/api-definition:

```
	"modules": {
		"identity": {
			"rootPath": "identity",
			"remoteServiceName": "AbpIdentity",
```			

----------------------------------

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


form-utils

***abp-modal***

- Posee un comportamiento de dirty, sobre las modificaciones relacionadas en la forma, para solicitar confirmacion en salir sin guardar. 

***abp-extensible-form***


***mapEnumToOptions***



# Localization 

The Localization key format consists of 2 sections which are Resource Name and Key. ResourceName::Key


https://docs.abp.io/en/abp/latest/UI/Angular/Localization



# Referencias

Web Application Development Tutorial
(Abp: Entity-Framework  + Angular)
https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=NG&DB=EF
