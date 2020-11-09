# SharePoint



SharePoint tiene muchos años, Microsoft lanzó su primera versión en el año 2001 bajo el nombre de Windows SharePoint Services (WSS), que posteriormente adquirió el nombre de SharePoint Foundation. 

Posteriormente Microsoft lanzó SharePoint Server, una propuesta muy parecida a SharePoint Foundation pero con algunas características y funcionalidades adicionales, como por ejemplo la inteligencia empresarial, la administración de contenido (tener un control de qué puede hacer cada usuario), búsqueda avanzada de documentos, creación de sitios personales, suministro de noticias, etc.


Hoy en día conocemos cómo SharePoint Online, la cara visible de Microsoft 365 y el cerebro dónde se almacena toda la documentación de una empresa.


## Componentes del sitio SharePoint

**Web Parts**.  Una parte web es un bloque de creación básico para la mayoría de las páginas en su sitio SharePoint. Si tiene permiso para editar páginas en su sitio, puede usar Partes web para personalizar su sitio y mostrar fotos y gráficas, porciones de otras páginas web, listas de documentos, vistas personalizadas de datos de negocios y más.

**Lists**.  Permite a sus usuarios almacenar, compartir y administrar información en SharePoint. Puede usar listas para llevar un control de sus proyectos de trabajo o eventos deportivos en un calendario. Las listas también le permiten realizar encuestas u ofrecer debates en un panel de discusión.


**Libraries**.  Una biblioteca es un tipo específico de lista dónde puede almacenar archivos. En una biblioteca, usted puede controlar cómo se ven los documentos, cómo se lleva un control de ellos, cómo se administran y crean.


**Views**.  Al crear y administrar las vistas, usted puede asegurar que los miembros de diversos equipos vean en una lista o biblioteca los elementos que les son más importantes. Usando las vistas, usted puede crear una lista de elementos que son importantes para cierto departamento, o destacar determinados documentos en una biblioteca.

## Sitios, Collecciones Sitios

Cada sitio de SharePoint Server pertenece a una única colección de sitios, mientras que una colección de sitios está formada por un sitio de primer nivel y todos los sitios por debajo de este.


![imagen](https://user-images.githubusercontent.com/222181/96375655-e656eb80-113f-11eb-8bbb-ad983c09e737.png)




## Versiones

 - SharePoint Server 2013 
 - SharePoint Server 2016 
 - SharePoint Server 2019

## SharePoint Framework

https://docs.microsoft.com/en-us/sharepoint/dev/spfx/sharepoint-framework-overview



## API

Always use Microsoft Graph APIs when possible, but if you need to use capabilities which are not yet exposed within the Microsoft Graph, you might need to fall back on the SharePoint Online CSOM APIs. Today we are thrilled to announce availability of a new version of the SharePoint Online CSOM NuGet package, which also includes .NET Standard versions of the CSOM APIs.


![imagen](https://user-images.githubusercontent.com/222181/96357250-288c1880-10bf-11eb-9161-372f30826d5e.png)




### REST API in SharePoint


Using REST API remotely we can interact with SharePoint 2013/2016/2019/Online sites and can perform create, read, update and delete(CRUD) operation.

The REST client access API was first introduced in SharePoint 2010, but was greatly expanded in SharePoint 2013.

The REST API in SharePoint 2010 is accessed through the ListData web service at the /_vti_bin/ListData.svc url. SharePoint 2013 introduced the /_api/lists/ and /_api/web endpoint URLs, which behave slightly differently.

El método recomendado para obtener los tokens de acceso para SharePoint Online es configurar una aplicación de Azure AD.



### .NET Standard

Using CSOM for .NET Standard instead of CSOM for .NET Framework

Octubre-2020. 

On-Premises SharePoint support.  No. (Revisar)

https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/using-csom-for-dotnet-standard

### Conexión a través de credenciales office 365 o de red

Se utiliza la librería Microsoft.SharePointOnline.CSOM (16.1.20616.12000)

##### SharePointAuthentication.SharePointOnline:
 - credentials = new SharePointOnlineCredentials(Username, SecurePassword);

##### SharePointAuthentication.SharePointActiveDirectory:
 - credentials = new NetworkCredential(Username, Password);

### Definición de campos (columnas) con XML
```
<Field
    Type="Text"
    DisplayName="Document Title English"
    Description="Document Title in English"
    Required="TRUE"
    EnforceUniqueValues="FALSE"
    Indexed="FALSE"
    MaxLength="255"
    Group="Demo"
    ID="{161ef8f6-e73c-4c56-8a5f-c6a8900f2fc8}"
    StaticName="TitleEN"
    Name="TitleEN">
</Field>
```

https://olafd.wordpress.com/2017/05/09/create-fields-from-xml-for-sharepoint-online

### Autenticación a través de PnP (OfficeDevPnP)
https://www.c-sharpcorner.com/article/authenticate-sharepoint-using-pnp-authentication-manager

### Límites en archivos
100 GB. The maximum size for files attached to list items is 250 MB.
En caso de de utilizar FileCreationInformation() el límite son 2mb. 

### Agregar metadatos durante la carga

```https://docs.microsoft.com/en-us/sharepoint/dev/spfx/sharepoint-framework-overview
//Cargar archivos a través de Stream
private static void UploadLargeFile(ClientContext Context, string FilePath, string Library, string FileName)
{
	string relativeFolder = String.Empty;
	string relativePath = String.Empty;
	FilePath = Path.Combine(Environment.CurrentDirectory, @"Extra\", FilePath);
	if (FileName.Equals(String.Empty))
	{
		if (System.IO.File.Exists(FilePath))
		{
			FileName = Path.GetFileName(FilePath);
		}
		else
		{
			Console.WriteLine($"No se encontro el archivo: {FilePath}");
			return;
		}
	}

	var myLibrary = Context.Web.Lists.GetByTitle(Library);
	Folder folder = myLibrary.RootFolder;
	Context.Load(folder, f => f.ServerRelativeUrl);
	Context.Load(myLibrary.ContentTypes);
	Context.ExecuteQuery();
	relativeFolder = myLibrary.RootFolder.ServerRelativeUrl;
	relativePath = relativeFolder + "/" + FileName;

	FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
	Microsoft.SharePoint.Client.File.SaveBinaryDirect(Context, relativePath, fs, true);

	File uploadedFile = folder.Files.GetByUrl(relativePath);
	Context.Load(uploadedFile);
	Context.ExecuteQuery();

	ContentType myContentType = myLibrary.ContentTypes.Where(ct => ct.Name == "Documento Beca").First();
	uploadedFile.ListItemAllFields["ContentTypeId"] = myContentType.Id;
	uploadedFile.ListItemAllFields["Created"] = DateTime.Today.AddDays(-1);
	uploadedFile.ListItemAllFields["Identificacion"] = FileName;
	uploadedFile.ListItemAllFields["StudentName"] = $"Juan Pablo Correa Herrera - {FileName}";
	uploadedFile.ListItemAllFields["Pidm"] = $"123456 - {FileName}";
	uploadedFile.ListItemAllFields.Update();
	Context.ExecuteQuery();
}
```

https://stackoverflow.com/questions/12474940/adding-metadata-while-uploading-documents-to-sharepoint

### Tutorial básico de sharepoint client
https://www.youtube.com/playlist?list=PLaIJswamN5lSSYPatSGSG5IC_gZXpBNdv

### LABS
https://github.com/Campusoft/LABS/tree/main/sharepoint/SharepointLabs

# Reference


Understanding the REST API of SharePoint 2013
https://www.slideshare.net/SPSSTHLM/understanding-the-rest-api-of-sharepoint-2013?from_action=save


