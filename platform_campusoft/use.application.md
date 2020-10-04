
## Utilizacion Capa Servicios Aplicacion

 
### Utilizar Views (*.cshtml) (MVC), existentes en proyectos base

1. En el proyecto principal (Web,MVC, etc), se debe crear una la carpeta “EmbeddedResource”, en la raiz.

![imagen](https://user-images.githubusercontent.com/222181/95022321-3318cd80-063c-11eb-89eb-63a6708cdbd5.png)


3.  En el Startup.Configure, despues de

     app.UseStaticFiles();

Agregar configuracion para permitir utilizar los view (*.cshtml)

      app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "EmbeddedResource")),
                    RequestPath = "/EmbeddedResource"
                });
                
      app.UseEmbeddedFiles(); //Allows to expose embedded files to the web!



