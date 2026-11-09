# dotnet 

Ejecutar en ambiente desarrollo

```
dotnet Gradebook.Register.dll run --environment="Development"
```

Ejecutar en ambiente de produccion
 
```
dotnet Gradebook.Register.dll run --environment="Production"
```




Establecer el Puerto diferente de 5000 (default), ejecutar dotnet

dotnet run --urls=http://localhost:5001/


Establecer ambiente produccion



```
set ASPNETCORE_ENVIRONMENT="Production"
dotnet Gradebook.Register.dll run --environment="Production"
```


## dotnet tool

- dotnet tool install - Installs the specified .NET tool on your machine.


Global tools can be installed in the default directory or in a specific location. The default directories are:

OS 	Path
Linux/macOS 	$HOME/.dotnet/tools
Windows 	%USERPROFILE%\.dotnet\tools


## dotnet build 



## Custom templates for dotnet new

The template engine offers features that allow you to replace values, include and exclude files, and execute custom processing operations when your template is used.

A template is composed of the following parts:

- Source files and folders.
- A configuration file (template.json).

It’s very easy to create a new template and they are surprisingly easy to maintain. Traditionally, templates that can perform text substitution use a special syntax, like $VARIABLE$ markers that will be replaced when the template is evaluated. Unfortunately, this is usually invalid syntax for the file type, which makes it impossible to run the project to test that the template is correct. This leads to bugs and slow iteration times, and basically, a bit of a maintenance headache.

Fortunately, the designers of the template engine have thought about this, and come up with a much nicer way of working: running templates.

The idea is simple — the template is just plain text files. No special formats, no special markers. So, a C# file is always a valid C# file. If a template wishes to substitute some text, such as replacing a C# namespace for one that's based on the project name, this is handled with simple search and replace
https://www.infoq.com/articles/dotnet-core-template-engine/


.NET template, the template engine offers features like:

- Replace values.
- Include and exclude files.
- Preprocessor conditional directives to include or exclude entire blocks of code.
- Execute custom operations when your template is used.



Custom templates for dotnet new
https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates

**template.json**

The name in the source tree to replace with the name the user specifies. The template engine will look for any occurrence of the sourceName mentioned in the config file and replace it in file names and file contents. The value to be replaced with can be given using the -n or --name options while running a template. If no name is specified, the current directory is used.
- sourceName: 	string 	The name in the source tree to replace with the name the user specifies. The template engine will look for any occurrence of the sourceName mentioned in the config file and replace it in file names and file contents. The value to be replaced with can be given using the -n or --name options while running a template. If no name is specified, the current directory is used.
https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates#templatejson

Reference for template.json
- This page describes the available properties of template.json configuration file.
https://github.com/dotnet/templating/wiki/Reference-for-template.json

Creating .NET Project Templates
- The SourceName Parameter
- The PreferNameDirectory Property
- Creating Custom Parameters for .NET Project Templates
- Adding Optional Contents to a .NET Project Template 
https://code-maze.com/dotnet-project-templates-creation/



**Ejemplo**

Especificaciones:
- Utilizar nombres proyectos: MyCompanyName.MyProjectName
- Utilizar namaspace: MyCompanyName.MyProjectName

Pasos:
- Crear carpeta: .template.config, en el proyecto que se requiere colocarlo como template
- Crear archivo, dentro de la carpeta creada: template.json
- Configurar archivo template.json

```
{
  "$schema": "http://json.schemastore.org/template",
  "author": "Campusoft",
  "classifications": [ "C#" ],
  "identity": "Campusoft.Report",
  "name": "Campusoft. Utilizar modulo de Reportes.",
  "shortName": "campusoft-report",
  "sourceName": "MyCompanyName.MyProjectName",
  "preferNameDirectory":true,
  "tags": {
    "language": "C#",
    "type": "project"
  },
  "sources": [
    {
      "exclude": [
        "**/[Bb]in/**",
        "**/[Oo]bj/**",
        "**/.vs/**",
        "**/.git/**",
        "**/.idea/**",
        "**/*.user",
        "**/*.suo"
      ]
    }
  ]
}
```

Instala la plantilla.

- La plantilla esta en la carpeta c:\plantillas\app\MyCompanyName.MyProjectName
- Utilizar dotnet new para instalar la plantilla
- --debug:custom-hive ./hive, para evitar que se instale globalmente la plantilla, sino temporalmente.

```
dotnet new --install c:\plantillas\app\MyCompanyName.MyProjectName --debug:custom-hive ./hive
```

Si se requiere volver instalar la misma plantilla.

```
dotnet new --install D:\proyectos\Campusoft.Report\app\MyCompanyName.MyProjectName --debug:custom-hive ./hive --force
```

- Utilizar la plantilla, en este caso el nombre "shortName" es campusoft-report
- -n Nuevo.Proyecto, reemplazara todas las ocurrencias en nombres de archivos, o textos dentro de los archivos de "sourceName": "MyCompanyName.MyProjectName" especificado en template.json
- --output ./TestReport, indicar la carpeta de salida de la creacion de un nuevo proyecto utilizando la plantilla

```
dotnet new campusoft-report -n Nuevo.Proyecto --output ./TestReport --debug:custom-hive ./hive  
```


# Https 

Hosting ASP.NET Core images with Docker over HTTPS
https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0 

Hosting ASP.NET Core images with Docker Compose over HTTPS
https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0



# Certificados SSL



Use dotnet dev-certs to create self-signed certificates for development and testing.

Generar certificados. Primero limpiar, luego generar
```
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```
Si un certificado se caduco, y en el navegador continuea utilizando el certificado caducado a pesar de haber eliminado. En el siguiente enlace se menciona algunos elementos
- dotnet dev-certs https --clean Fails

https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-3.1&tabs=visual-studio#all-platforms---certificate-not-trusted-1

Errores relacionados

```
[13:54:44 ERR] Exception occurred while processing message.
System.InvalidOperationException: IDX20803: Unable to obtain configuration from:
 'System.String'.
 ---> System.IO.IOException: IDX20804: Unable to retrieve document from: 'System
.String'.
 ---> System.Net.Http.HttpRequestException: The SSL connection could not be esta
blished, see inner exception.
 ---> System.Security.Authentication.AuthenticationException: The remote certifi
cate is invalid because of errors in the certificate chain: UntrustedRoot

```
