
# Generacion Codigo

Parametros:

- --file-name-input <file-name-input> (REQUIRED)  Nombre archivo (schema.json)
- --file-name-output <file-name-output>           Nombre archivo salida
- --template-code <template-code> (REQUIRED)      Codigo plantilla a utilizar
  
Generar el codigo del builForm para angular, a partir Dto que se encuentra en un schema json


```
$ Campusoft.Generate.Cli --file-name-input Foo.Schema.json --template-code angular.form-group.scriban
```

Generar el codigo html para los controle del formulario para angular, a partir Dto que se encuentra en un schema json

```
$ Campusoft.Generate.Cli --file-name-input Foo.Schema.json --template-code angular.form.bootstrap.scriban
```





# Importacion namespace


MyCompanyName.MyProjectName.AdministrativeUnitType.AdministrativeUnitTypeController
MyCompanyName.MyProjectName.Application.AdministrativeUnitTypeDto

Estos namespace, genera las siguientes carpetas


- proxy\application
- proxy\administrative-unit-type

Esto causa que los Dto, esten por un lado, y los servicios esten en otro, lo cual causa que las importaciones para utilizar estos objetos se complica.



# Generar proxy.



abp generate-proxy  --module MyProjectName --api-name MyProjectName --target @mre/administrative-unit


/src/environments/environment.ts.
-- 

```
 apis: {
    default: {
      url: 'https://localhost:44323',
      rootNamespace: 'MyCompanyName.MyProjectName',
    },
	MyProjectName: {
      url: 'https://localhost:44362',
      rootNamespace: 'MyCompanyName.MyProjectName',
    },
  },
```

/api/abp/api-definition:

```
"MyProjectName": {
      "rootPath": "MyProjectName",
      "remoteServiceName": "MyProjectName",
	  
```



Crear un paths en tsconfig.json, para el el proxy del modulo. Apuntar a un archivo ts, en el cual expone todos los elementos del proxy.


```
"paths": {
      "@proxy": [
        "src/app/proxy/index.ts"
      ],
      "@proxy/*": [
        "src/app/proxy/*"
      ],
	  
      ...
	  
      "@mre/administrative-unit":[
		  "projects/mre/administrative-unit/src/public-api.ts"
	  ],
	  "@mre/administrative-unit/proxy":[
		  "projects/mre/administrative-unit/src/lib/proxy/public-api.ts"
	  ]	  
    }
```

# Trabajar con commandos angular cli

yarn ng generate module person --module app --routing --route person

# Crear Componentes

Crear commandos con angular cli

- Crear componente serviceType, en la libreria "@mre/administrative-unit", en la carpeta component
- Si el nombre del componente son varias palabras utilice "-" para separa las palabras (Ref: https://angular.io/guide/styleguide#style-02-02)

Ejemplo:
```
ng generate component components/service-type --skip-tests --project=@mre/administrative-unit
```



# Errores


Rutas en los modulos

