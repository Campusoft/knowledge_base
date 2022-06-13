# nswag

NSwag is a Swagger/OpenAPI 2.0 and 3.0 toolchain for .NET, .NET Core, Web API, ASP.NET Core, TypeScript (jQuery, AngularJS, Angular 2+, Aurelia, KnockoutJS and more) and other platforms, written in C#. The OpenAPI/Swagger specification uses JSON and JSON Schema to describe a RESTful web API. The NSwag project provides tools to generate OpenAPI specifications from existing ASP.NET Web API controllers and client code from these OpenAPI specifications.

- Generate C# or TypeScript clients/proxies from these specs
- Generate Swagger 2.0 and OpenAPI 3.0 specifications from C# ASP.NET (Core) controllers
- Everything can be automated via CLI (distributed via NuGet tool or build target; or NPM)
- CLI configured via JSON file or NSwagStudio Windows UI

# Templates


Nswag, permite sobreescribir las plantillas, ests  son realizados con liquid

Informacion como sobrescribir las plantillas
- To override templates, create a template directory, put the overridden templates into it and point to it with the TemplateDirectory setting
https://github.com/RicoSuter/NSwag/wiki/Templates




Los templates para desealizar objetos  
- Client.Class.ReadObjectResponse.liquid
- aspnetboilerplate: Sobrescribir los clientes (Proxy de APIS) para consumir el formato aspnetboilerplate.




**Clientes**

Plantillas TypeScript

1 AngularClient.liquid 

- template Client.Method.Documentation

for operation in Operations
- Client.Method.Documentation
- Client.RequestUrl
- Client.RequestBody
- Client.ProcessRespons


Client.ProcessResponse.liquid





** NSwagStudio **

NSwagStudio, permite generar proxy para consumir servicios.

** command line tool** 


## Template Csharp


Los templates para generar codigo c#
- github: NSwag/src/NSwag.CodeGeneration.CSharp/Templates/ 
- https://github.com/RicoSuter/NSwag/tree/master/src/NSwag.CodeGeneration.CSharp/Templates


Las templates para la generacion de DTO para Csharp se encuentra:
- Class.liquid. (Template principal)
- https://github.com/RicoSuter/NJsonSchema/tree/master/src/NJsonSchema.CodeGeneration.CSharp/Templates 

** Models que se envian a los templates **

- Models Class
 - https://github.com/RicoSuter/NJsonSchema/blob/master/src/NJsonSchema.CodeGeneration.CSharp/Models/ClassTemplateModel.cs
- Models Property
 - https://github.com/RicoSuter/NJsonSchema/blob/master/src/NJsonSchema.CodeGeneration.CSharp/Models/PropertyModel.cs



## Template TypeScript

TypeScript templates 
- https://github.com/RicoSuter/NJsonSchema/tree/master/src/NJsonSchema.CodeGeneration.TypeScript/Templates

** Models que se envian a los templates **

Ruta:
- https://github.com/RicoSuter/NJsonSchema/tree/master/src/NJsonSchema.CodeGeneration.TypeScript/Models

Modelo Property, para la generacion typescript
- NJsonSchema/src/NJsonSchema.CodeGeneration.TypeScript/Models/PropertyModel.cs 

## Liquid

Liquid is a template language created by Shopify and written in Ruby. It is now available as an open source project on GitHub, and used by many different software projects and companies. Liquid is the backbone of all Shopify themes, and is used to load dynamic content in the pages of online stores.
https://shopify.dev/api/liquid


# Versiones

NJsonSchema v10.6.0
- Breaking change: Migrate from DotLiquid to Fluid (reasons: much faster code generation & more strict liquid implementation) 
- Fluid is faster and allocates less memory than all other well-known .NET Liquid parsers. For parsing, Fluid is 30% faster than Scriban
https://github.com/RicoSuter/NJsonSchema/releases/tag/v10.6.0


# Referencias

Notepad++ language for Liquid:
https://github.com/RSuter/NSwag/files/1410369/liquid_notepadpp.zip
https://github.com/RicoSuter/NSwag/issues/263#issuecomment-338214521


Liquid Syntax Highlighting for Visual Studio
Support for the Liquid programming language in Visual Studio.
https://marketplace.visualstudio.com/items?itemName=igorfle.VSLiquidSyntax

# Revisiones

El proyecto nswag permite generar codigo de diferentes fuentes (openAPI, JsonSchema, Dll), para csharp , typescript. 


