# Theme


# MVC

**Theme**

themes.md


Personalizar la plantilla
https://docs.abp.io/en/abp/latest/UI/AspNetCore/Theming#implementing-a-theme

Abp posee Layout Hook Points, para inyectar contenido en los diferentes puntos de los layout
https://docs.abp.io/en/abp/latest/UI/AspNetCore/Layout-Hooks#layout-hook-points


# Angular

-    Ng Bootstrap will be used as the UI component library.
-    Ngx-Datatable will be used as the datatable library.


**Theme**

Implementing a Theme
https://docs.abp.io/en/abp/latest/UI/Angular/Theming#implementing-a-theme



Referencia plantilla base de abp. (El layout)
- Gestiona la base principal (Logo, menus)
- Gestion el contenido (section main).
- En esta plantilla, se utiliza abpReplaceableTemplate para establecer los diferentes hook para reemplazar estos componentes con implementaciones personalizadas. (Ver: Component Replacement) 
abp/npm/ng-packs/packages/theme-basic/src/lib/components/application-layout/application-layout.component.html 
  


**Menus**


Modifying the Menu
- How to Add a Navigation Element (Via RoutesService). Los menus principales asociados a los routers
- Patch or Remove a Navigation Element
- Add an Element to Right Part of the Menu
- Patch or Remove an Right Part Element
https://docs.abp.io/en/abp/latest/UI/Angular/Modifying-the-Menu



NavItems = 'Theme.NavItemsComponent'

Este componente presenta los menus, se se visualiza en la parte derecha de la plantilla base de abp. En este componente tambien trabaja con los menus para el usuario autentificado.
(Si el usuario no esta autentificado, presenta menu para iniciar sesion)
 
Menus / Items 
Este template, muestra como utilizar el servicio NavItemsService, el cual contiene
la lista menues configurados.
abp/npm/ng-packs/packages/theme-basic/src/lib/components/nav-items/nav-items.component.html 
 
Templates para opciones usuario autentificado.
abp/npm/ng-packs/packages/theme-basic/src/lib/components/nav-items/current-user.component.html 


Routes = 'Theme.RoutesComponent'
Este componente presenta el menu principal del sistema, 
que visualiza los menus asociados a los routes configurados.

Referencia componente de la plantilla basica abp
abp/npm/ng-packs/packages/theme-basic/src/lib/components/routes/

Laboratorio
- Al reemplazar el componente, con un codigo copiando el original. No se presenta los submenus de gestion usuarios y tenants. (*)


**error handling**
HTTP Error Handling
https://docs.abp.io/en/abp/latest/UI/Angular/HTTP-Requests#http-error-handling


## Referencias

Component Replacement
You can replace some ABP components with your custom components.
https://docs.abp.io/en/abp/latest/UI/Angular/Component-Replacement 

Using Angular Material Components With the ABP Framework
https://community.abp.io/articles/using-angular-material-components-with-the-abp-framework-af8ft6t9