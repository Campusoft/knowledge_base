# Moodle

- Moodle significa Entorno de Aprendizaje Dinámico Orientado a Objetos Modular (del inglés, Modular Object-Oriented Dynamic Learning Environment o MOODLE).
- Open source: licencia en abierto bajo GNU Pública General y Creative Commons.
- https://es.wikipedia.org/wiki/Moodle


## Requisitos
Moodle es una aplicación web que se ejecuta sin modificaciones en UNIX, GNU/Linux, OpenSolaris, FreeBSD, Windows, Mac OS X, NetWare y otros sistemas que soportan PHP incluyendo la mayoría de proveedores de hosting Web. Además, para poder utilizar esta plataforma no hace falta saber programar. Además, todos los archivos están en continuas copias de seguridad y cifrados.

Los requisitos mínimos en el servidor para la instalación de Moodle son:
- **PHP** 5.6. o superior (7.1 a partir de la versión 3.7).
- **MySQL** 5.5. o superior.
- **Apache** 2.X o superior.

En cuanto a los requisitos mínimos de hardware, son los siguientes:
- **Espacio en el disco:** 5 GB recomendado.
- **Procesador:** 1 GHz (mínimo), 2 GHz de doble núcleo o más recomendado.
- **Memoria:** 512 Mb (mínimo), 1 Gb o más recomendado, en los servidores de gran producción se recomiendan 8 Gb.
- Se recomienda, servidores separados para la web “front-end” y la base de datos.


## Instalación


Puede ser a través de un servidor apache (herramienta softaculus en cpanel), instalación local desde [bitnami](https://bitnami.com/stack/moodle). EJ. [Bitnami Docker Image for Moodle LMS](https://hub.docker.com/r/bitnami/moodle/)

Docker



## Documentación oficial
https://docs.moodle.org/311/en/Main_page
## Administración
- La administración de los cursos en Moodle es piramidal y está sujeta a distintos niveles o roles, a los que se asocian "permisos" de diversa amplitud. De mayor a menor, estos serían:
	- **Administrador o mánager**: puede crear cursos y categorías, modificar y asignar roles dentro de los cursos, crear cuentas de acceso y asignar roles, instalar bloques, modificar el tema gráfico, etc. En general, este rol puede realizar cualquier modificación y puede existir más de uno dentro de la plataforma.
	- **Creador de cursos**: puede crear nuevos cursos y categorías.
	- **Profesor**: puede crear, modificar y borrar actividades o recursos dentro del curso al que este asignado, además de inscribir, calificar, dar retroalimentación y establecer y regular la comunicación con los participantes al curso.
	- **Profesor sin permisos de edición**: solo puede calificar, dar retroalimentación y establecer comunicación con los participantes del curso.
	- **Estudiante**: puede visualizar y realizar las actividades, revisar los recursos y establecer comunicación con los otros participantes al curso y con el profesor.
	- **Invitado**: solo está habilitado para visualizar el curso o la plataforma, pero no puede participar dentro de ella.
	- **Usuario autenticado**: está habilitado para visualizar el curso y realizar otro tipo de funciones dentro de la plataforma educativa.
## Utilizar servicios web
- https://docs.moodle.org/311/en/Using_web_services
- Se puede utilizar el usuario administrador que tiene permisos completos o crear uno para el servicio a utilizar
### 1. Habilitar servicios web
- Menú/Administración del sitio/Opciones avanzadas
	- Habilitar servicios web
	- Guardar cambios
### 2. Habilitar protocolos
- Menú/Administración del sitio/Servidor/Servicios Web/Administrar protocolos
	- Habilitar el/los protocolos requeridos
	- Habilitar Documentación de servicios web
	- Guardar cambios
### 3. Habilitar uso de servicios
- Menú/Administración del sitio/Servidor/Servicios Web/Servicios externos/Servicio externo
	- Editar
	- Habilitar
	- Guardar cambios
### 4. Agregar nuevo servicio	
- Menú/Administración del sitio/Servidor/Servicios Web/Servicios externos/Servicio externo
### 5. Agregar funciones
- Menú/Site administration/Server/Web services/External services/Functions	
	- Add functions
	- Buscar y agregar de acuerdo a la necesidad (https://docs.moodle.org/dev/Web_service_API_functions)
### 6. Autorizar usuario para utilizar servicio creado
- Site administration/Server/Web services/External services/Authorised users
### 7. Crear rol y agregar capacidades de rest, SOAP, etc
- Site administration/Users/Permissions/Define roles	
### 8. Crear token
- Menú/Administración del sitio/Servidor/Servicios Web/Administrar fichas (tokens)
	- Agregar usuario/s
	- Seleccionar servicio
	- Ingresar restricciones de IP (no es requerido)
	- Seleccionar fecha de validez de token (no es requerido)
	- Guardar cambios	
## Protocolos
#### Implemented
 - REST (returning XML)
 - REST (returning JSON)
 - XMLRPC
#### Must be implemented
 - SOAP
#### Suggested
- REST (returning JSONP)
- JSON (https://tracker.moodle.org/browse/MDL-21341)
## Métodos Http
- Unicamente soporta POST.
# Integración con Moodle
## Versionamiento
- [Releases](https://docs.moodle.org/dev/Releases). Se realiza un release menor aproximadamente cada 2 meses y uno mayor cada 6.
	- **Últimas versiones**
		- **Moodle 3.11:** 17 May 2021
		- **Moodle 3.10:** 9 November 2020
		- **Moodle 3.9:** 15 June 2020
		- **Moodle 3.8:** 18 November 2019
		- **Moodle 3.7:** 20 May 2019
		- **Moodle 3.6:** 3 December 2018
		- **Moodle 3.5:** 17 May 2018
		- **Moodle 3.4:** 13 November 2017
		- **Moodle 3.3:** 15 May 2017
		- **Moodle 3.2:** 5 December 2016
		- **Moodle 3.1:** 23 May 2016
		- **Moodle 3.0:** 16 November 2015
		- **Moodle 2.0:** 24 November 2010
		- **Moodle 1.0:** 20 August 2002
- **Obtener versión sin ser admin.** No existe una forma oficial de obtener sin ser admin pero se puede buscar la version en formato YYYYMMDDRELEASE (2021051701) dentro de código html y cotejar con los [releases](https://docs.moodle.org/dev/Releases)
- No existen versiones completas para el api, cada función tiene su propia versión mínima. Ej.
	- core_course_create_categories, version 2.3
	- core_form_dynamic_form, version 3.11
	- core_message_decline_contact_request, version 3.6
	- core_role_assign_roles, version 2.0
	- mod_assign_lock_submissions, version 2.6	
	
	
	
	
###  Versiones Principales de Moodle y sus Características

| Versión         | Año de lanzamiento | Características principales                                                                 |
|-----------------|--------------------|---------------------------------------------------------------------------------------------|
| Moodle 3.0      | 2015               | Mejoras en compatibilidad móvil, nuevos tipos de preguntas, soporte para plugins modernos. |
| Moodle 3.5 LTS  | 2018               | Primera LTS con soporte prolongado, GDPR compliance, mejoras en privacidad y reportes.     |
| Moodle 3.9      | 2020               | Integración con H5P nativa, mejoras en actividades y accesibilidad.                        |
| Moodle 3.11     | 2021               | Mejora en reportes del estudiante, más accesibilidad, soporte adicional para LTI 1.3.      |
| Moodle 4.0      | 2022               | Rediseño de interfaz, navegación moderna, nuevo "Course Index", mejoras UX.                |
| Moodle 4.1 LTS  | 2022 (dic)         | LTS estable con soporte hasta 2027, mejoras en usabilidad, accesibilidad, y rendimiento.   |
| Moodle 4.2      | 2023               | Mejoras en gestión de cursos, nuevo banco de contenido, filtros avanzados.                |
| Moodle 4.3      | 2023 (nov)         | Mejora de editores, informes personalizados, rendimiento y compatibilidad LTI Advantage.   |
| Moodle 4.4      | 2024               | Mejoras a nivel de IA, analíticas, y simplificación en edición de curso.                   |
	
	
	
	
	
## [Seguridad](https://docs.moodle.org/dev/Security#Security_of_web_applications)
	- Manual accounts (no desabilitable)
	- No login (no desabilitable)
	- Email-based self-registration (habilitada por defecto)
	- CAS server (SSO) (desabilitada por defecto)
	- External database (desabilitada por defecto)
	- LDAP server (desabilitada por defecto)
	- LTI (desabilitada por defecto)
	- MNet authentication (desabilitada por defecto)
	- No authentication (desabilitada por defecto)
	- OAuth 2 (desabilitada por defecto)
	- Shibboleth (desabilitada por defecto)
	- Web services authentication (no desabilitable)
## Consumo servicios
	- Método: solo POST
	- Servicios: Conjunto de funciones que se puede utilizar.
	- Token: a través de usuario, clave y servicio (Email-based self-registration)
	- Funciones: como parámetro o como endpoint
	- Protocolos: SOAP, REST, XMLRPC, personalizados a través de plugins
## Informacion entidades
- [Wikipedia](https://es.wikipedia.org/wiki/Moodle#Est%C3%A1ndares_internacionales_de_Moodle)
- Moodle se basa en cursos como unidad básica. El administrador debe crear el curso, configurarlo, enrolar al alumnado y generar las actividades.
- La organización está basada en jerarquía por categorías. Todo curso debe pertenecer a una sola categoría pero las categorías pueden pertenecer a otras categorías con lo que se arma el árbol de categorización de cursos. Ej. UNIVERSIDAD/MODALIDAD/NIVEL/AREA/DEPARTAMENTO/SECCIÓN/Curso
- Los cursos tienen asociados:
	- Grades
	- Enrolment
	- Activities
- El enrolamiento consiste en agregar usuarios (los usuarios no tiene rol por si mismos). Una vez agregados los usuarios se puede asignar a cada uno un rol (estudiante, profesor). Esta asignación es a nivel de contexto, es decir, los que son estudiantes o profesores solo lo serán en este curso en particular, estos mismos pueden tener diferente rol en otro cursos (profesores siendo estudiantes y viceversa).
## Informacion base de datos
- [Esquema](https://www.examulator.com/er/)
## Connection
- Token generado en moodle por cada usuario y obtenido a través de usuario, clave y servicio (nombre del servicio que se desea consumir).
```
REQUEST
[URL_DOODLE]/login/token.php?username={username}&password={password}&service={service}

RESPONSE
{
    "token": "d49bda52a7ff829218ed9e1e645d499d",
    "privatetoken": null
}
```
## Obtener informacion
- Unicamente peticiones POST a través de varios protocolos: SOAP, REST, XMLRPC (por defecto pero se pueden agregar a través de plugins. Ej. RESTful)
- Maneja concepto de funciones que serían los endpoints tradicionales y pueden invocarse de diversas formas dependiendo del protocolo. Cada función recibe sus propios parámetros en función del protocolo. 
- **Web service functions**
	- Cada función tiene una versión moodle de introducción. Ej. core_user_search_identity (3.11), core_course_create_courses (2.0) [Leer más](https://docs.moodle.org/dev/Web_service_API_functions)
	- Naming convention: La funciones deben nombrarse bajo la convención: {fullcomponent}_{methodname}_{verb}_{noun}. Ej: core_user_get_user_preferences
	- Donde
		- fullcomponent: is the full frankenstyle name or the component such as core_user here 
		- methodname: is the name of the method in the form of {verb}_{noun} such as get_user_preferences here
		- verb: preferably one of get, create, delete, update or eventually other that well describes the action
		- noun: moodle objects, usually plural such as user_preferences here, or e.g. posts, discussions, users, courses etc.
				This naming convention has been in place since Moodle 2.2. See MDL-29106 for more details.
	- **Documentación**
		- [Descripción de funciones](https://docs.moodle.org/dev/Web_service_API_functions#API_Roadmap): Lista y presenta información relevante de las funciones (Componente, Nombre, Versión de introducción, Descripción, Disponibilidad con AJAX, Login requerido, Servicios que la incluyen)
		- Documentación para desarrolladores: Viene incluida con la instalación de moodle (Web Services/API Documentacion) y debe ser habilitada (Administration/Server/Web services/Manage protocols). Contiene requisitos para request y responses.
- **Protocolos**
- Cada protocolo puede habilitarse individualmente para hacer peticiones en diferentes formatos:
	- REST: 
		- Las funciones a utilizar se invocan  como un parámetro. Ej. 
			```
			{URL_MOODLE}/webservice/rest/server.php?wsfunction=core_course_create_courses
			```
		- Recibe: application/x-www-form-urlencoded
		- Devuelve: XML, JSON
- **Plugins para agregar "Protocolos"**
- Existen plugins para agregar protocolos por parte de terceros. Ej. [RESTful](https://moodle.org/plugins/webservice_restful)
## Paginación
- https://github.com/moodle/moodle
- https://docs.moodle.org/dev/Web_service_API_functions
- Algunas funciones (endpoints) de consultas (get) soportan paginación. Ej. 
	- core_course_get_courses, mod_forum_get_forums_by_courses, core_course_get_categories no soportan
	- mod_forum_get_forum_discussions si soport.
- Algunas funciones (endpoints) de búsqueda (search) soportan paginación. Ej. 
	- core_cohort_search_cohorts, core_course_search_courses, core_enrol_search_users soportan
	- core_competency_search_competencies no soporta
- No todas funciones tienen el mismo nombre de parámetros.
	- Página: page, limitfrom; Número de registros: perpage, limitnum.
- No todas las implementaciones tienen el mismo valor por defecto. 
	- core_cohort_search_cohorts: limitnum (Default to "25")
	- core_course_search_courses: perpage (Default to "0").
- Ejemplo de request y response de paginación de cursos
```
REQUEST
wstoken: "10787a782d5cea26d69e103729d594f7",
wsfunction: "core_course_search_courses",
moodlewsrestformat: "json",
criterianame: "search", //criteria name (search, modulelist (only admins), blocklist (only admins), tagid)
criteriavalue: "a",
page: 0, //Paginación desde la posición 0, primera página
perpage: 10

RESPONSE
{
    "total": 22,
    "courses": [
        {
            "id": 1,
            "fullname": "Lawrence Haag",
            "displayname": "Lawrence Haag",
			...        
		},
        {
            "id": 2,
            "fullname": "Lawrence Haag",
            "displayname": "Lawrence Haag",
			...        
		},
	...
    ],
    "warnings": []
}
```
## Crear información
- Funciona igual que para obtener, método post, función y parámetros de acuerdo a la documentación para desarrolladores. Dependiendo del protocolo es la implementación.
- Ejemplo de creación de cursos
```
REQUEST
wstoken: "10787a782d5cea26d69e103729d594f7",
wsfunction: "core_course_search_courses",
moodlewsrestformat: "json",
courses[0][fullname]: "Nombre del curso"
courses[0][categoryid]: 1
courses[0][shortname]: "nombre_corto", //debe ser única
courses[0][summary]: "Descripción"

RESPONSE
[
    {
        "id": 60,
        "shortname": "nombre_corto"
    }
]
```
	