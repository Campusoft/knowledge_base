# Autorizacion 


## Configuraciones de Permisos

La plataforma Campusoft, se basa en aspnetboilerplate. 

1. Revisar el funcionamiento Permisos como se trabajan en aspnetboilerplate

https://aspnetboilerplate.com/Pages/Documents/Authorization

2. Especificaciones

- Los permisos se tiene que establecer a nivel Servicio Aplicacion

- Tambien se tiene que establecer a nivel Controller en estilos (MVC)

- Los nombres permisos tiene que formarse por las siguientes convenciones

<Nombre-Sistema>.<Nombre-Funcionalidad>.<Nombre-Accion-Requiere-Autorizacion>

Si se trata de un CRUD para la funcionalidad Usuarios del Sistema Facturacion, los nombres serian

Billing.User.Create
Billing.User.Update
Billing.User.Delete
Billing.User.Get

- Los nombres permisos, centralizarlos en una clase "PermissionNames", con constantes

El nombre constantes, estara formado:

.<Nombre-Funcionalidad>_<Nombre-Accion-Requiere-Autorizacion>

Ejemplo: Permisos para la funcionalidad Escala de calificaciones, del Sistema Gradebook:

```
public const string GradeScale_Create = "Gradebook.GradeScale.Create";
public const string GradeScale_Update = "Gradebook.GradeScale.Update";
public const string GradeScale_Delete = "Gradebook.GradeScale.Delete";
public const string GradeScale_Get = "Gradebook.GradeScale_Get";		
```

3. Configurar los permisos en clases Base

3.1 Servicios Aplicacion Base CRUD. #IAsyncBaseCrudAppService

En el constructor del servicio aplicacion que se requiere establecer los permisos, se utiliza las siguientes  propiedades:

Nombre permiso para la Accion CREATE:
CreatePermissionName = "Nombre Permiso";

Nombre permiso para la Accion UPDATE:
UpdatePermissionName = "Nombre Permiso";

Nombre permiso para la Accion DELETE:
DeletePermissionName = "Nombre Permiso";

Nombre permiso para la Accion GET-ALL. "Obtener todos los registros de una entidad":

GetAllPermissionName = "Nombre Permiso";

Nombre permiso para la Accion GET. "Obtener un elemento/registro de una entidad por su ID":

GetPermissionName = "Nombre Permiso";

Example

```
CreatePermissionName = PermissionNames.GradeScale_Create;
UpdatePermissionName = PermissionNames.GradeScale_Update;
DeletePermissionName = PermissionNames.GradeScale_Delete;
GetAllPermissionName = PermissionNames.GradeScale_Get;
GetPermissionName = PermissionNames.GradeScale_Get;
		
```
			
3.2 Controller Base - CRUD. #BaseCrudDtoController

En el constructor del controllador se requiere establecer los permisos,  se utiliza las siguientes  propiedades:

Nombre permiso para la Accion CREATE:
CreatePermissionName = "Nombre Permiso";

Nombre permiso para la Accion UPDATE:
UpdatePermissionName = "Nombre Permiso";

Nombre permiso para la Accion DELETE:
DeletePermissionName = "Nombre Permiso";

Nombre permiso para la Accion GET-ALL. "Obtener todos los registros de una entidad":

GetAllPermissionName = "Nombre Permiso";

Nombre permiso para la Accion GET. "Obtener un elemento/registro de una entidad por su ID":

GetPermissionName = "Nombre Permiso";

3.3 Recomendaciones:

Un servicio aplicacion (CRUD), si esta relacionado a un controller base (CRUD), se tiene que utilizar los mismos  permisos, en ambos elementos.

Si el servicio aplicacion, es consumido por otro servicio Aplicacion o otro elemento no relacionado, se 
puede dejar sin establecer el permiso GetAllPermissionName, GetPermissionName en el servicio; unicamente 
que requiere autentificacion. Esto para disminuir las dependencias que se tiene que hacer al momento de dar los permisos: Ejemplo Si un servicio aplicacion B, dependende servicio aplicacion A, y el metodo servicio B "Process", utiliza el metodo GetAll del Servicio A, entonces el usuario debe tener permisos para la accion

- Ejecutar accion.  Process. Servicio B). 
- Obtener los registros. GetAll. Servicio A).


Si unicamente se requiere tener un permiso para cualquier accion CRUD, se puede establecer el mismo permiso universal, en las 5 propiedades para establecer los permisos en las clases Base. 

4. Configurar elementos Adicionales

4.1 Menus

Un menu de una funcionalidad, por lo general presentara el listado registros de una entidad, ejemplo: "Menu: Clientes",
en este caso el permiso que se asocie al menu, seria elGetAllPermissionName o el GetPermissionName

Ejemplo:

```
new MenuItemDefinition(
  name: "Client",
  displayName: L("ClientMenu"),
  url: "clients",
  icon: "icon-layers",
  requiredPermissionName: PermissionNames.Client_Get

```


								  

