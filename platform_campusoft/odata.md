

# ¿Qué es OData?

**OData** (**Open Data Protocol**) es un estándar que define un conjunto de mejores prácticas para la construcción y consumo de RESTful APIs, permitiendo a los desarrolladores la capacidad de desarrollar **Queryable APIs**. Este protocolo utiliza otras tecnologías ya reconocidas como **HTTP**, **AtomPub** o **JSON** y permite a cualquier cliente obtenga información de cualquier fuente basándose en:

-   La estandarización de una forma uniforme de representación de datos estructurados a través de **Atom** o **JSON**.
-   La utilización de convenciones **URL** uniformes, tanto para la navegación, filtrado, orden y paginación de datos (entre otros).
-   La creación de operaciones uniformes mediante métodos **HTTP** (**GET, POST, PUT y DELETE**).


La potencia de **OData** nos da la posibilidad, entre otras, de:

-   Aplicar filtros en los datos a obtener.
-   Habilitar paginación.
-   Ordenar los datos.
-   Reestructurar los datos de respuesta.
-   Envío de solicitudes de forma asíncrona o en modo **_batch_**.


OData ayuda a concentrarse en la lógica de su negocio mientras crea API RESTful sin tener que preocuparse por los diversos enfoques para definir encabezados de solicitud y respuesta, códigos de estado, métodos HTTP, convenciones de URL, tipos de medios, formatos de carga útil, opciones de consulta, etc.


## Laboratorio

### Utilizar odata con Abp


1. Instalar Microsoft.AspNetCore.OData

2. Instalar y configurar

https://aspnetboilerplate.com/Pages/Documents/OData-AspNetCore-Integration


Errores:

------

Al generar los metadata

http://localhost:5000/odata/$metadata

```
Newtonsoft.Json.JsonSerializationException: 'Self referencing loop detected for property 'declaringType' with type 'Microsoft.OData.Edm.EdmEntityType'. Path 'result.schemaElements[0].declaredKey[0]'.'
```

You can disable wrapping by default in the PreInitialize method of your module:
```
Configuration.Modules.AbpAspNetCore().DefaultWrapResultAttribute.WrapOnSuccess = false;
```

https://stackoverflow.com/questions/49535921/abp-aspnetcore-odata-self-referencing-loop-when-accessing-odata-metadata


----------------------

ObjectDisposedException: Cannot access a disposed object.

Los repositorios, y los objetos dbContext, ya se encuentra eliminados al momento ejecutar la accion IQueryable

Opcion Alternativa. 
No exponer la informacion directamente, con atributos sobre el metodo, ejecutar el repositorio y obtener listado objetos, y estos retornarlos como simples listas. (No permite utilizar los parametros que se enviaron)

Posibles:

ODataController and UnitOfWork
Los metodos acciones en los controller, tiene que ser virtuales. 

https://support.aspnetzero.com/QA/Questions/115/ODataController-and-UnitOfWork

--------------------

## Referencias

OData for ASP.NET Core 3.1
https://devblogs.microsoft.com/odata/enabling-endpoint-routing-in-odata/


Seguridad [https://docs.microsoft.com/en-us/odata/webapi/odata-security](https://docs.microsoft.com/en-us/odata/webapi/odata-security)

Para más información podéis visitar [https://www.odata.org/](https://www.odata.org/)

Protect your Queryable API with the validation feature in ASP.NET Web API OData [enlace](https://devblogs.microsoft.com/aspnet/protect-your-queryable-api-with-the-validation-feature-in-asp-net-web-api-odata/)


