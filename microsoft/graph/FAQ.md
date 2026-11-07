- Agregar miembros, mezclados a un grupos. Es decir grupos y usuarios agregarlo como miembros
	- El grupo "padre" e "hijo" debe ser de tipo ["DynamicMembership"], para lo cual se envia a crear sin groupTypes, mailEnabled en false y securityEnabled en true.
	- Se agregan tal y como cualquier otro miembro
- Listar los miembros de grupo, en el cual estos miembros son diferente tipo.
	- Se puede identificar los grupos a través de @odata.type = #microsoft.graph.group y los usuarios por @odata.type = #microsoft.graph.user.
	- Al listar los miembros del grupo padre solo presentará lo que está en su nivel, es decir, listará los grupos y solamente los usuarios que pertenecen directamente a el.
- Consultar obtener lo grupos que tiene cambios o se han creados desde una fecha X. hasta este momento
	- No se puede validar que grupos tienen cambios ya que no hay una propiedad para tal cosa. Existe la fecha de renovación que sirve para renovar un grupo que tiene una fecha de caducidad.
	- No se puede filtrar directamente por un fallo en graph. Se deben obtener todos y filtrar a través de programación.
		- https://stackoverflow.com/questions/58416592/filtering-azure-groups-by-createddatetime-using-graph-api
		- https://www.titanwolf.org/Network/q/38b18479-b089-4a98-bf89-9ed5bbf7c755/y
- Agregar miembros en un solo request
	```
		PATCH https://graph.microsoft.com/v1.0/groups/{group-id}
		
		{
			"members@odata.bind": [
				"https://graph.microsoft.com/v1.0/directoryObjects/{id}",
				"https://graph.microsoft.com/v1.0/directoryObjects/{id}",
				"https://graph.microsoft.com/v1.0/directoryObjects/{id}"
			]
		}
	```
- Crear pruebas de usuarios con menos datos para que de error
	- Presenta una Exception: A value is required for property 'displayName' of resource 'User'.
- Quitar miembros de grupos
	```
	DELETE https://graph.microsoft.com/v1.0/groups/{id}/members/{id}/$ref
	```
	- Para quitar un propietario
	```
	DELETE https://graph.microsoft.com/v1.0/groups/{id}/owners/{id}/$ref
	```
- Analizar la respuesta del batch
	- Asignar manualmente un id a cada request de batch para luego obtener la respuesta en función de ese id ya que no se ejecutan en el mismo orden.
	```
	Requests
		var request1 = GenerateObjectJsonForGroupsWithId("request1");
		var request2 = GenerateObjectJsonForGroupsWithId("request2");
		var request3 = GenerateObjectJsonForGroupsRepeatedWithId("request3");	
			
	Responses
		Id request: request3 - Status Request: 400 - Error: Another object with the same value for property mailNickname already exists.
		Id request: request1 - Status Request: 201 - Id: fc03a05c-e3cf-4ae5-8807-f48e2fd62559
		Id request: request2 - Status Request: 201 - Id: 1a3277cf-00d4-4252-8c04-10e3374acd40
	
	```
- Revisar memberOf y los demas
	- List members sirve para listar los miembros de un grupo (usuarios, grupos, equipos, etc).
	```
		GET https://graph.microsoft.com/v1.0/groups/{id}/members
	```
	- List memberOf sirve para listar los grupos a los que pertenece un grupo.
	```
		GET https://graph.microsoft.com/v1.0/groups/groups/{id}/memberOf
	```
	- List transitive members sirve para listar los miembros de un grupo expandiendo los elementos de los grupos miembros.
	```
		GET https://graph.microsoft.com/v1.0/groups/groups/{id}/transitiveMembers
	```
	- List transitive memberOf sirve para listar los grupos a los que pertenece un grupo en forma anidada.
	```
		GET https://graph.microsoft.com/v1.0/groups/groups/{id}/transitiveMemberOf
	```
- Puede saber en la respuesta el tipo miembro ?? 
	- Si, a través de @odata.type: #microsoft.graph.group | user
	```
		if (group.OdataType.Equals("#microsoft.graph.group"))
		{
		} else {
		}	
	```
