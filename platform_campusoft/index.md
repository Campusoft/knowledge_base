
# Plataforma .Net
La plataforma que se utiliza para construir sistemas, con tecnologia .net, en proyectos de Campusoft se basa en las siguientes elementos:

**Framework:**

- .Net Core 2.x, 3.x
-  Plataforma ASP.NET Boilerplate (ABP) 
-- .Net 4.x and .net core [https://aspnetboilerplate.com/](https://aspnetboilerplate.com/)
-- Nueva Version (.net core): [https://abp.io/](https://abp.io/)
- Platform Campusoft

**Conceptos:**
- ORM. ORM, Entity Framework. / ORM ligero, Drapper. 
- Servicios. (REST)

**Framework o Librerias Frontend**
-- Jquery
-- Angular
-- React

**Librerias Soporte**
- AutoMapper. Facilitar mapeo de clases.
- Syncfusion. Componentes UI, multi-tecnologias (angular, react, jquery,  etc), integradas con .net 
- MassTransit. Para la gestion mensajes, en broker message.


## Plataforma Abp.

Esta plataforma facilita la creacion inicial de proyectos, posee un sinumero elementos base para construir software seguiendo buenas practicas.

Adicional la documentacion es bastante buena.

Para sus entendimiento, inicial con los manuales: 
-   [https://aspnetboilerplate.com/Pages/Documents/Articles-Tutorials](https://aspnetboilerplate.com/Pages/Documents/Articles-Tutorials)
 
 Luego ejecutar dichos manuales, es importante leer algunos elementos claves plataforma. 
- Arquitectura [Enlace](https://aspnetboilerplate.com/Pages/Documents/NLayer-Architecture)
- Entidades dominio. [enlace](https://aspnetboilerplate.com/Pages/Documents/Entities)
- Servicios Aplicacion.   [enlace](https://aspnetboilerplate.com/Pages/Documents/Application-Services)
   
 ## Plataforma Campusoft.

La plataforma Campusoft, esta implementa sobre .net, y abp. Posee un sinumero elementos comunes que se utilizan en cualquier sistema que se implemente, tambien elementos particulares para ciertos tipos sistemas.

Para su entendimiento  se estructura segun las capas:

**Capa dominio**

[Informacion Capa dominio](domain)

**Capa Servicios de Aplicación.**

Abp, permite utilizar una arquitectura DDD, y existe un sinnúmero de elementos ya implementados.

Un servicio de aplicación, encapsula o utiliza servicios de dominio, repositorios, entidades de negocio, servicios de infraestructura.

Los servicios de aplicación, son los que exponen características de un sistema, para ser 
consumidas por diferentes clientes “Frontend”.  Ejemplo Web utilizando MVC, o SPA utilizando servicios REST, o una interfaz de línea de comandos “CLI”

![](https://lh6.googleusercontent.com/5E1sJE3mnPo6Tn34fj09B80ih35HnZYWA5q3kNqxzA04_S9hAyTfVt_n2ROcANRHv043ItE7-pSsW4oK2buptC8mYXYCKq0hqF0_Bp7LFYZTZttZShqNb4JNhoUlt9AVhy_O3G7Y)
**Especificaciones:**

Los servicios aplicacion siempre debe tener una interfaz.

La interfaz de los servicios aplicacion debe estar documentada a detalle.

Los servicios de aplicación siempre deben exponen DTO, no deben exponer las entidades de dominio.

Los metodos, debe ser uso verificacion autorizacion para consumir dicho metodo.

Utilizar convenciones en los nombres Get, Create, Update, Delete, para logica relacionada la Obtener informacion, a Crear informacion, actualizar informacion, o borrar informacion. Ejemplo un servicio aplicacion clientes puede tener varios metodos para obtener informacion clientes. GetAll "Obtener todos clientes", GetActive "Obtener clientes activos", GetLookup "Obtener un listado clientes en formato clave,valor", etc.

Los métodos públicos de un servicio de aplicación, siempre deben recibir sólo un parámetro, para permitir generar servicios rest dinámicos de estos servicios de aplicación.  

![](https://lh4.googleusercontent.com/tYriR6h5UPN5OvZyeetZPYVtjghfIcBCrsaDLRqFUlZvqAjrv-zUHWlpo6tx1RyKkgEetFmIwjAalMGwuWaOriVBUl4HfL0ST4OggyYO2-3eAGgrcYcXk05-4Rlra1yEZH5yAVxg)
 
La plataforma Campusoft, tiene clases base que extendiende las características ofrecidas por ABP, las cuales pueden ser debe utilizadas en conjunto con controladores base en un modelo MVC, para generar formularios básicos dinámicos.

TODO
- Autorizacion
- Validaciones
- Mapeo
- Process / Jobs
- Dynamic Api

  
Existe dos interfaces base.


-   IAsyncReadOnlyAppService. Que se utiliza para entidades, de solo lectura. La implementación es AsyncReadOnlyAppService
    
-   IAsyncBaseCrudAppService. Que se utiliza para entidades que se permite crear, modificar, eliminar. La implementación es AsyncBaseCrudAppService

**Capa Servicios distribuidos**

Son servicios que son llamados remotamente a su aplicacion. 

Se utilizan los siguientes protocolos servicios:

- [Soap](soap.md)
- [Rest](rest.md)
- [Odata](odata.md)
- [gRPC](grpc.md)
- [GraphQL](graphql.md)

**Capa Acceso datos (ORM)**

   
**Capa Infraestructura**
   
**Capa de Presentación.**
Existe las siguientes clases base
-   BaseReadOnlySearchDtoController, se utiliza para crear funcionalidades de visualizacion de informacion.
    
-   BaseCrudDtoController, se utiliza para crear funcionalidades que permite acciones (CRUD); crear, leer, actualizar, borrar.

**Formularios Dinámicos.**

La plataforma de Campusoft, se basa en el concepto de View, para construir los elementos de una funcionalidad en la UI, existe las siguientes tipos de View.

-   Tree. Table show data. Visualizar un listado de registros, en una organización de tabla.
    
-   Search. Allow filter data. Permite generar bloque de búsqueda, con varios elementos de busqueda, para aplicarlos en la recuperacion de informacion.
    
-   Form. Allow Crud Data. Permite generar formularios para crear, actualizar, visualizar, o eliminar un registro.
   
La plataforma, tiene implementaciones genéricas para view, utilizando mvc “*.cshtml”, por lo cual cualquiera de estas implementaciones puede sobrescribirse para personalizar alguna UI, utilizando el concepto de template de mvc.

![](https://lh5.googleusercontent.com/UTt2cW_dmGzAJtEJsseLMuj8j8ymZyBQ-e4_qNRaY0ynCbBxifxhV4j9yAGduaaz5OzBmSlHNfc4wUuMldRfbIO6SAnUiQAPC-X3UsAwz9qIlnZ9DIsi6w5FoOSr4mALhxkamKq-)
Las view de mvc, que corresponden a cada view de la plataforma.


#### View Tree.

La plataforma genera automáticamente, la vista tipo “Tree”, basado en las propiedades del objeto que se utiliza para generar esta vista “El DTO”, en el caso que se requiera visualizar propiedades específicas de un objeto, se debe establecer explícitamente los campos que se desean, realizando una sobreescritura del método “GetViewTreeTyped(string name)” en el controlador que utiliza los controller base.
 
#### View Search.

Para construir las opciones de búsqueda que puede tener una funcionalidad, se debe establecer explícitamente los campos que se requieren, realizando una sobrescritura del método “GetViewSearchTyped(string name)” en el controlador que utiliza los controller base.

#### View Form.

La plataforma genera automáticamente, la vista tipo “Form”, de todas las propiedades de un objeto para generar la vista “El DTO”, en el caso que se requiera visualizar propiedades específicas de un objeto, se puede establecer manualmente los campos que se utilizan para la acción “Crear”, acción “Editar”, acción “Detalles”.

-   Establecer el view genérico para todas las acciones “GetViewFormTyped(string name)”
   
-   En el constructor del controlador, se debe establecer la propiedad a true, para indicarle al framework que se utiliza un view personalizado.
    
-   ViewFormTyped = true;

-   Establecer el view para la acción de crear “GetViewFormCreateTyped(string name)”, si no se especifica esta vista, se utilizara la vista generica.

-   En el constructor del controlador, se debe establecer la propiedad a true, para indicarle al framework que se utiliza un view personalizado.
 
-   ViewFormCreateTyped = true;
    

-   Establecer el view para la acción de editar “GetViewFormEditTyped(string name)”, si no se especifica esta vista, se utilizara la vista genérica
    

-   En el constructor del controlador, se debe establecer la propiedad a true, para indicarle al framework que se utiliza un view personalizado.
    

-   ViewFormEditTyped = true;
    

-   Establecer el view para la acción de detalles “GetViewFormDetailTyped(string name)”, si no se especifica esta vista, se utilizara la vista genérica.
    

-   En el constructor del controlador, se debe establecer la propiedad a true, para indicarle al framework que se utiliza un view personalizado.
    

-   ViewFormDetailTyped = true;
    

-   Para la acción de delete, se utiliza el view establecido en “GetViewFormDetailTyped(string name)”, o la vista genérica.
    
Correlación entre clases base de Servicio de Aplicación y clases base de Controller para generar formularios dinámicos base.


![](https://lh5.googleusercontent.com/k7ACtPDbzG0mUxc8vzSXHBcPCAs0KcqxuBW-WK18nY5oi8luLMXVHRVtPwgxFdRtxUq54g8ZLqoiMsRzMZHRmnkfPX4e5YuN3B2O04U9k6SxQYJINbGnsxjm-_IOnCZwOIzla9Xh)
