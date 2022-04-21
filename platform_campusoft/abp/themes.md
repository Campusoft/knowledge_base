# Theme


# MVC

**Theme**

themes.md


Personalizar la plantilla
https://docs.abp.io/en/abp/latest/UI/AspNetCore/Theming#implementing-a-theme

Abp posee Layout Hook Points, para inyectar contenido en los diferentes puntos de los layout
https://docs.abp.io/en/abp/latest/UI/AspNetCore/Layout-Hooks#layout-hook-points


Ejemplo de una proyecto para personalizar la plantilla abp con plantilla bootstrap "Stisla"
Stisla Is a bootstrap-based admin template
https://github.com/realLiangshiwei/Lsw.Abp.AspNetCore.Mvc.UI.Theme.Stisla
https://github.com/stisla/stisla

# Angular

-    Ng Bootstrap will be used as the UI component library.
-    Ngx-Datatable will be used as the datatable library.


**Theme**

abp posee tres layout
- eLayoutType.account. Component abp AccountLayoutComponent
- eLayoutType.application. Component abp ApplicationLayoutComponent
- eLayoutType.empty. Component abp EmptyLayoutComponent

Con RoutesService se agrega los routes. Cada route posee propiedad layout defines in which layout the route will be loaded. (default: eLayoutType.empty)
https://docs.abp.io/en/abp/latest/UI/Angular/Modifying-the-Menu#via-routesservice


Implementing a Theme. (Personalizar la plantilla)

The easiest way to create a new theme is to add Basic Theme Source Code to your project via ABP CLI command and customize it.
You can run the following command in Angular project directory to copy the source code to your solution:

```
abp add-package @abp/ng.theme.basic --with-source-code
```
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


# Referencias

Customizing the ABP Basic Theme with Bootswatch
https://community.abp.io/posts/customizing-the-abp-basic-theme-with-bootswatch-4luoqzr0