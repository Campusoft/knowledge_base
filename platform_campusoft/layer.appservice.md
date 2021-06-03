
# Capa Servicios de Aplicación


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