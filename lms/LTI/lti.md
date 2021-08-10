# LTI - Learning Tools Interoperability




# LTI 1.3


Actualizacion del LTI 1.x a LTI 1.3
https://www.imsglobal.org/why-platforms-and-tools-should-adopt-lti-13


LTI Advantage is Based on LTI 1.3

- Assignment and Grade Services seamlessly syncs grades, progress, and comments from multiple sources into an LMS platform’s gradebook, greatly reducing faculty effort and the chance of errors.
	

# Librerias

LTI 1.1.1 library for .NET Applications.  
https://github.com/LtiLibrary/LtiLibrary

# Laboratorio

Conectar proyecto .NET LTI a Canvas

Se debe tener en consideración que la versión LTI que actualmente está usando canvas es LTI V.1.3 primero se deberá generar un xml que luego será usado para subir a canvas los pasos detallados se encuentran en este tutorial desde el en titulo de lanzamiento de una LTI: 

https://community.canvaslms.com/groups/canvas-developers/blog/2016/09/13/net-lti-project-part-1



Se debe tener en consideración que al momento de usar el XML se debe tener uno adecuado para su funcionalidad puesto que permite elegir la ubicación de la LTI ya sea desde un usuario o curso como lo muestra en el ejemplo.
 
Ver variables que nos otorga canvas

Dentro de las variables que canvas devuelve al haber enlazado con LTI se puede observar que permite el acceso a algunas variables estándar de LTI para su manejo, estas variables se definen en la documentación global de IMS en el siguiente enlace, precisamente en el punto 3:
https://www.imsglobal.org/specs/ltiv1p0/implementation-guide

Dentro de estas variables se nota que de gran importancia con las oauth_* ya que son indispensables para la autenticación.
También dentro de las variables ya mencionadas existen algunas variables que son personalizadas. Para mejor comprensión se detalla dentro del API de Canvas LMS
Se debe recordar que el Api de Canvas usa la notación de “punto” en lugar de guion bajo.
https://canvas.instructure.com/doc/api/file.tools_variable_substitutions.html

Claves OAuth

Tomando en consideración dentro del punto inicial se usó dos claves al configurar en XML, la clave del consumidor y el secreto del  consumidor, dentro de este punto podemos tomar en consideración a estas claves ya que son fundamentales a la hora de autenticar.


The Shared Secret is the password.  Canvas will never send you the Shared Secret, it is meant to be private, and should only be known by you and Canvas.  Canvas will use the Shared Secret to create an encrypted hash of the form parameters that are sent to you in the launch request as the oauth_signature.


Para que el consumidor de  herramientas  (LMS  normalmente)  envíe al proveedor de herramientas los datos de Lanzamiento básicos (Basic Launch Data)



# Referencias


LTI 1.3 - Canvas
How do I configure an LTI key for an account?

https://community.canvaslms.com/docs/DOC-16729-how-do-i-configure-an-lti-key-for-an-account



Crear el LTI - Canvas
- Admin link
- Settings link
- Apps tab
https://community.canvaslms.com/docs/DOC-16730-42141110273

 

**LTI 1.3 Canvas**


Informacion del flujo
https://canvas.instructure.com/doc/api/file.lti_dev_key_config.html


Implementar LTI 1.3 .net core
https://andyfmiller.com/2018/12/21/using-asp-net-core-for-lti-v1-3-and-lti-advantage/


(Buen ariticulo)
https://andyfmiller.com/2018/12/28/launching-an-lti-1-3-resource-link-using-openid-connect-third-party-login/


Claves OAuth / Consumir API


Generar Clave
https://community.canvaslms.com/docs/DOC-12657-4214441833



Implementing OAuth in an ASP.NET Core 2.2 MVC web app
https://community.canvaslms.com/groups/canvas-developers/blog/2019/08/20/implementing-oauth-in-an-aspnet-core-22-mvc-web-app



Generar claves JWK
https://mkjwk.org/


.NET LTI Project - Part 1 - Connect to Canvas
https://community.canvaslms.com/t5/Canvas-Developers-Group/NET-LTI-Project-Part-1-Connect-to-Canvas/ba-p/271647

