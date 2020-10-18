# SharePoint



SharePoint tiene muchos años, Microsoft lanzó su primera versión en el año 2001 bajo el nombre de Windows SharePoint Services (WSS), que posteriormente adquirió el nombre de SharePoint Foundation. 

Posteriormente Microsoft lanzó SharePoint Server, una propuesta muy parecida a SharePoint Foundation pero con algunas características y funcionalidades adicionales, como por ejemplo la inteligencia empresarial, la administración de contenido (tener un control de qué puede hacer cada usuario), búsqueda avanzada de documentos, creación de sitios personales, suministro de noticias, etc.


Hoy en día conocemos cómo SharePoint Online, la cara visible de Microsoft 365 y el cerebro dónde se almacena toda la documentación de una empresa.



Componentes del sitio SharePoint 

Web Parts.  Una parte web es un bloque de creación básico para la mayoría de las páginas en su sitio SharePoint. Si tiene permiso para editar páginas en su sitio, puede usar Partes web para personalizar su sitio y mostrar fotos y gráficas, porciones de otras páginas web, listas de documentos, vistas personalizadas de datos de negocios y más.

Lists.  Permite a sus usuarios almacenar, compartir y administrar información en SharePoint. Puede usar listas para llevar un control de sus proyectos de trabajo o eventos deportivos en un calendario. Las listas también le permiten realizar encuestas u ofrecer debates en un panel de discusión.


Libraries.  Una biblioteca es un tipo específico de lista dónde puede almacenar archivos. En una biblioteca, usted puede controlar cómo se ven los documentos, cómo se lleva un control de ellos, cómo se administran y crean.


Views.  Al crear y administrar las vistas, usted puede asegurar que los miembros de diversos equipos vean en una lista o biblioteca los elementos que les son más importantes. Usando las vistas, usted puede crear una lista de elementos que son importantes para cierto departamento, o destacar determinados documentos en una biblioteca.




Cada sitio de SharePoint Server pertenece a una única colección de sitios, mientras que una colección de sitios está formada por un sitio de primer nivel y todos los sitios por debajo de este.


![imagen](https://user-images.githubusercontent.com/222181/96375655-e656eb80-113f-11eb-8bbb-ad983c09e737.png)




## Versiones

SharePoint Server 2013
SharePoint Server 2016
SharePoint Server 2019



# SharePoint Framework





# API

Always use Microsoft Graph APIs when possible, but if you need to use capabilities which are not yet exposed within the Microsoft Graph, you might need to fall back on the SharePoint Online CSOM APIs. Today we are thrilled to announce availability of a new version of the SharePoint Online CSOM NuGet package, which also includes .NET Standard versions of the CSOM APIs.


![imagen](https://user-images.githubusercontent.com/222181/96357250-288c1880-10bf-11eb-9161-372f30826d5e.png)




## REST API in SharePoint


Using REST API remotely we can interact with SharePoint 2013/2016/2019/Online sites and can perform create, read, update and delete(CRUD) operation.

The REST client access API was first introduced in SharePoint 2010, but was greatly expanded in SharePoint 2013. The REST API in SharePoint 2010 is accessed through the ListData web service at the /_vti_bin/ListData.svc url. SharePoint 2013 introduced the /_api/lists/ and /_api/web endpoint URLs, which behave slightly differently.



El método recomendado para obtener los tokens de acceso para SharePoint Online es configurar una aplicación de Azure AD.



## .NET Standard

Using CSOM for .NET Standard instead of CSOM for .NET Framework

Octubre-2020. 

On-Premises SharePoint support.  No. (Revisar)

https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/using-csom-for-dotnet-standard


## Reference


Understanding the REST API of SharePoint 2013
https://www.slideshare.net/SPSSTHLM/understanding-the-rest-api-of-sharepoint-2013?from_action=save
