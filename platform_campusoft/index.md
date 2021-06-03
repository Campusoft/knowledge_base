
# Plataforma .Net

La plataforma que se utiliza para construir sistemas, con tecnologia .net, en proyectos de Campusoft se basa en las siguientes elementos:

**Framework:**

- .Net Core 2.x, 3.x
-  Plataforma ASP.NET Boilerplate (ABP) 
-- .Net 4.x and .net core [https://aspnetboilerplate.com/](https://aspnetboilerplate.com/)
-- Nueva Version (.net core): [https://abp.io/](https://abp.io/)
- Platform Campusoft

**Conceptos:**
- DDD
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


## Plataforma Abp / Aspnetboilerplate

Esta plataforma facilita la creacion inicial de proyectos, posee un sinumero elementos base para construir software seguiendo buenas practicas.

Adicional la documentacion es bastante buena.

Para sus entendimiento, inicial con los manuales: 
-   [https://aspnetboilerplate.com/Pages/Documents/Articles-Tutorials](https://aspnetboilerplate.com/Pages/Documents/Articles-Tutorials)
 
 Luego ejecutar dichos manuales, es importante leer algunos elementos claves plataforma. 
- Arquitectura [Enlace](https://aspnetboilerplate.com/Pages/Documents/NLayer-Architecture)
- Entidades dominio. [enlace](https://aspnetboilerplate.com/Pages/Documents/Entities)
- Servicios Aplicacion.   [enlace](https://aspnetboilerplate.com/Pages/Documents/Application-Services)
   
Seguir el tutorial

- [Tutorial Aspnetboilerplate] tutorial.aspnetboilerplate.md    
   
## Plataforma Campusoft.

La plataforma Campusoft, esta implementa sobre .net, y abp. Posee un sinumero elementos comunes que se utilizan en cualquier sistema que se implemente, tambien elementos particulares para ciertos tipos sistemas.

Para su entendimiento  se estructura segun las capas:

**Capa dominio**

[Informacion Capa dominio](layer.domain.md)

**Capa Servicios de Aplicación.**

[Informacion Capa de Servicios Aplicacion](layer.appservice.md)


**Capa Servicios distribuidos**

Son servicios que son llamados remotamente a su aplicacion. 

Se utilizan los siguientes protocolos servicios:

- [Soap](soap.md)
- [Rest](rest.md)
- [Odata](odata.md)
- [gRPC](grpc.md)
- [GraphQL](graphql.md)

**Capa Acceso datos (ORM)**

TODO
   
**Capa Infraestructura**

TODO
   
**Capa de Presentación.**

[Informacion Capa de Servicios Aplicacion](layer.presentation.md)

**Tutoriales**

- [Tutorial Inicial Plataform Campusoft] tutorial.platafom.campusoft.md   
