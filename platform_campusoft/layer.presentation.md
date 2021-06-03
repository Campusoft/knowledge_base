# Capa de Presentación

 
 

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