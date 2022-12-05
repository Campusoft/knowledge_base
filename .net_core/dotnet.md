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

**Ejemplo**


# Https 

Hosting ASP.NET Core images with Docker over HTTPS
https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0 

Hosting ASP.NET Core images with Docker Compose over HTTPS
https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0
