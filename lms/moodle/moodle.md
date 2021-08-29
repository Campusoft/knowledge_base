# Moodle

 
# Instalar

Bitnami Docker Image for Moodle LMS
https://hub.docker.com/r/bitnami/moodle/

Documentación oficial
https://docs.moodle.org/311/en/Main_page

Utilizar servicios web
- https://docs.moodle.org/311/en/Using_web_services
- Se puede utilizar el usuario administrador que tiene permisos completos o crear uno para el servicio a utilizar
1. Habilitar servicios web
	- Menú/Administración del sitio/Opciones avanzadas
		- Habilitar servicios web
		- Guardar cambios
2. Habilitar protocolos
	- Menú/Administración del sitio/Servidor/Servicios Web/Administrar protocolos
		- Habilitar el/los protocolos requeridos
		- Habilitar Documentación de servicios web
		- Guardar cambios
3. Habilitar uso de servicios
	- Menú/Administración del sitio/Servidor/Servicios Web/Servicios externos/Servicio externo
		- Editar
		- Habilitar
		- Guardar cambios
4. Agregar nuevo servicio	
	- Menú/Administración del sitio/Servidor/Servicios Web/Servicios externos/Servicio externo
5. Agregar funciones
	- Menú/Site administration/Server/Web services/External services/Functions	
		- Add functions
		- Buscar y agregar de acuerdo a la necesidad (https://docs.moodle.org/dev/Web_service_API_functions)
4. Crear token
	- Menú/Administración del sitio/Servidor/Servicios Web/Administrar fichas (tokens)
		- Agregar usuario/s
		- Seleccionar servicio
		- Ingresar restricciones de IP (no es requerido)
		- Seleccionar fecha de validez de token (no es requerido)
		- Guardar cambios	
 
 ### Protocolos
#### Implemented
 - REST (returning XML)
 - REST (returning JSON)
 - XMLRPC
#### Must be implemented
 - SOAP
#### Suggested
- REST (returning JSONP)
- JSON (https://tracker.moodle.org/browse/MDL-21341)

### Métodos Http
- Unicamente soporta POST.
