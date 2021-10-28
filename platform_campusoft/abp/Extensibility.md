# Extensibility



# Personalizacion

Obtener el codigo fuente de los modulos. (Opcion 1)

get-source (CLI option)
https://docs.abp.io/en/abp/latest/CLI#get-source


Customizing the Existing Modules
https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Guide

# Extending Entities

- All extra properties of an entity are stored as a single JSON object in the database table
-  ABP Framework entity extension system for the Entity Framework Core that allows you to use the same extra properties, but store a desired property as a separate field in the database table.


# Module Entity Extensions

Module entity extension system is a high level extension system that allows you to define new properties for existing entities of the depended modules. It automatically adds properties to the entity, database, HTTP API and the user interface in a single point.

- Configurar nuevos campos.
- Al agregar nuevos campos se visualiza por defecto en UI - Lista, UI - Form Create, UI = Form Edit. Se puede configurar en cuales se visualiza. 
- Se puede establecer a nivel API, si el campo esta disponible. 
- Module extension system naturally supports the enum types.
https://docs.abp.io/en/abp/latest/Module-Entity-Extensions

UI Visibility
https://docs.abp.io/en/abp/latest/Module-Entity-Extensions#ui-visibility

# UI Extensibility


## Table UI / Angular

- Data Table Column Extensions

Data table column extension system allows you to add a new column in the data table on the user interface
https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Guide


Entity prop extension system allows you to add a new column to the data table for an entity or change/remove an already existing one. 
https://docs.abp.io/en/abp/latest/UI/Angular/Data-Table-Column-Extensions


You can use the valueResolver to render an HTML string in the table.
(Ejemplo, personlizar la salida de una columna, colocar en mayusculas, agregar un icono segun el valor)
https://docs.abp.io/en/abp/latest/UI/Angular/Data-Table-Column-Extensions#how-to-render-custom-html-in-cells


- Entity Actions
Entity action extension system allows you to add a new action to the action menu for an entity on the user interface
https://docs.abp.io/en/abp/latest/UI/Angular/Entity-Action-Extensions

How Replaceable Components Work with Extensions
https://docs.abp.io/en/abp/latest/UI/Angular/How-Replaceable-Components-Work-with-Extensions


## Laboratorios


***Personalizar administracion usuarios (Frontend) / Angular *** 

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


Opcion 3:

Reemplazar componentes:

- Basado en 
abp/npm/ng-packs/packages/identity/src/lib/components/users/

- Commando:

```
ng generate component components/users-adapt
```

- Reutilizar los proxys de las librerias, ejemplo usuarios. O crear  nuevos proxys, con los cambios en los modelos.


Errores




```
ERROR NullInjectorError: R3InjectorError(IdentityModule)[ValidationGroupDirective -> ValidationGroupDirective -> ValidationGroupDirective -> ValidationGroupDirective -> ValidationGroupDirective]: 
  NullInjectorError: No provider for ValidationGroupDirective!
```


***TODO.***
- Generacion proxy, en una libreria. (Como volver a generar el proxy para una libreria con abp)


# Modificar Codigo Abp.



# Laboratorios


***Personalizar campos en usuarios (Backend)***

get-source (CLI option)
https://docs.abp.io/en/abp/latest/CLI#get-source


- Bajar codigo modulo Volo.Identity.  (La otra opcion es copiar codigo directamente, en este se debe utilizar el branch especifico con la version que se va modificar) 

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
