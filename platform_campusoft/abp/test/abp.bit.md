


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