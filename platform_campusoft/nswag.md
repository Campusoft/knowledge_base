# nswag




## Templates


nswag, permite sobreescribir las plantillas, ests  son realizados con liquid


** aspnetboilerplate **

Sobrescribir los clientes (Proxy de APIS) para consumir el formato aspnetboilerplate.


Arhivos (Github):
NSwag/src/NSwag.CodeGeneration.CSharp/Templates/ 

https://github.com/RicoSuter/NSwag/tree/master/src/NSwag.CodeGeneration.CSharp/Templates



(Desearlizacion del objeto). 
Client.Class.ReadObjectResponse.liquid


Informacion como sobrescribir las plantillas
https://github.com/RicoSuter/NSwag/wiki/Templates

Plantillas TypeScript

**Clientes**

1 AngularClient.liquid 

- template Client.Method.Documentation

for operation in Operations
- Client.Method.Documentation
- Client.RequestUrl
- Client.RequestBody
- Client.ProcessRespons


Client.ProcessResponse.liquid

**TypeScript DTO templates (NJsonSchema)**

https://github.com/RicoSuter/NJsonSchema/tree/master/src/NJsonSchema.CodeGeneration.TypeScript/Templates


Modelo Property, para la generacion typescript
NJsonSchema/src/NJsonSchema.CodeGeneration.TypeScript/Models/PropertyModel.cs /


** NSwagStudio **

NSwagStudio, permite generar proxy para consumir servicios.

# Referencias

Notepad++ language for Liquid:
https://github.com/RSuter/NSwag/files/1410369/liquid_notepadpp.zip

Liquid Syntax Highlighting for Visual Studio
Support for the Liquid programming language in Visual Studio.
https://marketplace.visualstudio.com/items?itemName=igorfle.VSLiquidSyntax