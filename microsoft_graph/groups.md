# Groups



Type Groups
- Microsoft 365 groups 	Facilitating user collaboration with shared Microsoft online resources.
- Security groups 	Controlling user access to in-app resources.

https://docs.microsoft.com/en-us/graph/api/resources/groups-overview?view=graph-rest-1.0



Dynamic membership rules for groups in Azure Active Directory
In Azure Active Directory (Azure AD), you can create complex attribute-based rules to enable dynamic memberships for groups. Dynamic group membership reduces the administrative overhead of adding and removing users. This article details the properties and syntax to create dynamic membership rules for users or devices. You can set up a rule for dynamic membership on security groups or Microsoft 365 groups.
https://docs.microsoft.com/en-us/azure/active-directory/enterprise-users/groups-dynamic-membership


# Microsoft 365 Groups

Microsoft 365 Groups is the foundational membership service that drives all teamwork across Microsoft 365. With Microsoft 365 Groups, you can give a group of people access to a collection of shared resources. These resources include:

    A shared Outlook inbox
    A shared calendar
    A SharePoint document library
    A Planner
    A OneNote notebook
    Power BI
    Yammer (if the group was created from Yammer)
    A Team (if the group was created from Teams)
    Roadmap (if you have Project for the web)
    Stream

---

# Gestión a través de SDK
#### Generalidades
- El token dura 60 minutos
- For performance reasons, the create, get, and list operations return only a subset of more commonly used properties by default. These default properties are noted in the Properties section. To get any of the properties that are not returned by default, specify them in a $select OData query option.

#### Obtener autorización a partir de clientId, tenant y clientSecret
- Permite obtener un proveedor de servicios a partir de las credenciales del cliente
- Si las credenciales son incorrectas el builder devuelve un objeto de todas formas y el error se presentará cuando se intente hacer una petición.

```
                IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(config.ClientId)
                    .WithTenantId(config.Tenant)
                    .WithClientSecret(config.ClientSecret)
                    .Build();
					
                authProvider = new ClientCredentialProvider(confidentialClientApplication);
				
ERROR
  Message: 
    Microsoft.Graph.ServiceException : Code: generalException
    Message: An error occurred sending the request.
    
    ---- Microsoft.Graph.Auth.AuthenticationException : Code: generalException
    Message: Unexpected exception returned from MSAL.
    
    -------- Microsoft.Identity.Client.MsalServiceException : A configuration issue is preventing authentication - check the error message from the server for details. You can modify the configuration in the application registration portal. See https://aka.ms/msal-net-invalid-client for details.  Original exception: AADSTS7000215: Invalid client secret is provided.
    Trace ID: 9c7aeb55-242d-458b-a0f3-b1e9728fff01
    Correlation ID: 074eb12d-4b7d-452f-bdf8-69939e28b0a5
    Timestamp: 2021-08-05 16:48:12Z				
```

#### Listado de grupos
- Permite listar los grupos configurados.
- En caso de no existir grupos devuelve un total de 0 grupos.
- **GET** https://graph.microsoft.com/v1.0/groups
```
var groups = await graphClient.Groups
	.Request()
	.GetAsync();
```        

#### Listado de grupos utilizando select
- Permite listar los grupos creados con propiedades específicas.
- **GET** https://graph.microsoft.com/v1.0/groups?$select=id,createdDateTime,visibility

```
var groups = await graphClient.Groups
	.Request()
	.Select("id, createdDateTime, visibility")
	.GetAsync();
```    

#### Listado de grupos con filtros
- Permite listar grupos a partir de filtros definidos
- **GET** https://graph.microsoft.com/v1.0/groups?$filter=displayName eq 'Grupo de prueba 2'

```
var groups = await graphClient.Groups
	.Request()
	.Filter($"displayName eq 'Prueba mínima desde Postman'")
	.GetAsync();
```
     
#### Creación de grupos
- Permite crear un grupo.
- **POST** https://graph.microsoft.com/v1.0/groups
```
var response = await graphClient.Groups
	.Request()
	.AddAsync(group);

BODY	
{
  "description": "Descripción del grupo de prueba 1",
  "displayName": "Grupo de prueba 1",
  "groupTypes": [
	"Unified"
  ],
  "mailEnabled": true,
  "mailNickname": "prueba1",
  "securityEnabled": false
}	
```
#### Creación masiva de grupos
- Permite crear varios grupos a través de batch.
	- https://docs.microsoft.com/en-us/graph/json-batching?context=graph%2Fapi%2F1.0&view=graph-rest-1.0
	- https://docs.microsoft.com/en-us/samples/azure-samples/graphapi-batching/batching-requests-to-msgraph/
	- https://docs.microsoft.com/en-us/graph/sdks/batch-requests?tabs=csharp
- **POST** https://graph.microsoft.com/v1.0/groups
- El uso de id es para rastrear las respuestas por el id de la petición
```
//Requests
var userRequest = graphClient.Me.Request();
var eventsRequest = graphClient.Me.CalendarView.Request(queryOptions);

// Build the batch
var batchRequestContent = new BatchRequestContent();

// Using AddBatchRequestStep adds each request as a step
// with no specified order of execution
var userRequestId = batchRequestContent.AddBatchRequestStep(userRequest);
var eventsRequestId = batchRequestContent.AddBatchRequestStep(eventsRequest);
var returnedResponse = await graphClient.Batch.Request().PostAsync(batchRequestContent);

BODY	
{
  "requests": [
    {
      "id": "1",
      "method": "GET",
      "url": "/me/drive/root:/{file}:/content"
    },
    {
      "id": "2",
      "method": "GET",
      "url": "/me/planner/tasks"
    },
    {
      "id": "3",
      "method": "GET",
      "url": "/groups/{id}/events"
    },
    {
      "id": "4",
      "url": "/me",
      "method": "PATCH",
      "body": {
        "city" : "Redmond"
      },
      "headers": {
        "Content-Type": "application/json"
      }
    }
  ]
}	

#### Crear grupos repetidos
- No permite crear ya que valida a nivel de mailNickname y esta propiedad es obligatoria. No valida las demás propiedades.
- **POST** https://graph.microsoft.com/v1.0/groups
```
var response = await graphClient.Groups
	.Request()
	.AddAsync(group);

  Message: 
    Microsoft.Graph.ServiceException : Code: Request_BadRequest
    Message: Another object with the same value for property mailNickname already exists.
    Inner error:
    	AdditionalData:
    	date: 2021-08-05T16:18:11
    	request-id: 0f08554d-e024-4fee-b79d-ba6a766281be
    	client-request-id: 0f08554d-e024-4fee-b79d-ba6a766281be
    ClientRequestId: 0f08554d-e024-4fee-b79d-ba6a766281be
```

#### Parámetros mínimos de creación
- Crear un nuevo grupo con los parámetros mínimos.
- **POST** https://graph.microsoft.com/v1.0/groups
```
var response = await graphClient.Groups
	.Request()
	.AddAsync(group);

BODY	
{
  "displayName": "Prueba mínima desde Postman",
  "mailEnabled": true,
  "mailNickname": "pruebaMinimaPostman",
  "securityEnabled": false,
  "groupTypes": [
    "Unified"
  ]
}
```

**Errores**
- Sin displayName, mailEnabled, mailNickname, securityEnabled, 
```
    "error": {
        "code": "Request_BadRequest",
        "message": "A value is required for property '<Nombre de la propiedad>' of resource 'Group'.",
        "innerError": {
            "date": "2021-08-05T03:21:03",
            "request-id": "134eb242-28df-4f6f-ae35-c47fca06c4be",
            "client-request-id": "134eb242-28df-4f6f-ae35-c47fca06c4be"
        }
    }
```
- Sin groupTypes
```
    "error": {
        "code": "Request_BadRequest",
        "message": "The service does not currently support writes of mail-enabled groups. Please ensure that the mail-enablement property is unset and the security-enablement property is set.",
        "innerError": {
            "date": "2021-08-05T03:19:49",
            "request-id": "6b9885a8-0a81-4fff-b327-5411077c5531",
            "client-request-id": "6b9885a8-0a81-4fff-b327-5411077c5531"
        }
    }
```

#### Listar grupos
- Permite obtener un grupo a partir de su ID.
- Cuando no existe devuelve la excepción:
```
  Message: 
    Microsoft.Graph.ServiceException : Code: Request_ResourceNotFound
    Message: Resource '082f2d69-70ba-4289-bbb9-85c2ac2ccf5c' does not exist or one of its queried reference-property objects are not present.
    Inner error:
    	AdditionalData:
    	date: 2021-08-05T00:57:53
    	request-id: 6d2d31ad-7d8f-41a2-91e2-87587d064a78
    	client-request-id: 6d2d31ad-7d8f-41a2-91e2-87587d064a78
    ClientRequestId: 6d2d31ad-7d8f-41a2-91e2-87587d064a78
```
- **GET** https://graph.microsoft.com/v1.0/groups/{id}
```
var group = await graphClient.Groups[groupId2]
   .Request()
   .GetAsync();
```

#### Actualizar grupos
- Permite actualizar un grupo 
- Presenta error al devolver el grupo, en la documentación indica que no devuelve nada pero el método indica lo contrario
```
  Message: 
    Microsoft.Graph.ServiceException : Code: Request_ResourceNotFound
    Message: Resource '082f2d69-70ba-4289-bbb9-85c2ac2ccf5c' does not exist or one of its queried reference-property objects are not present.
    Inner error:
    	AdditionalData:
    	date: 2021-08-05T00:57:53
    	request-id: 6d2d31ad-7d8f-41a2-91e2-87587d064a78
    	client-request-id: 6d2d31ad-7d8f-41a2-91e2-87587d064a78
    ClientRequestId: 6d2d31ad-7d8f-41a2-91e2-87587d064a78
```
- **POST** https://graph.microsoft.com/v1.0/groups
```
await graphClient.Groups[groupId3]
	.Request()
	.UpdateAsync(group);

BODY	
{
  "description": "Descripción del grupo de prueba 1",
  "displayName": "Grupo de prueba 1",
  "groupTypes": [
	"Unified"
  ],
  "mailEnabled": true,
  "mailNickname": "prueba1",
  "securityEnabled": false
}	
```

#### Eliminar grupos
- Permite eliminar un grupo 
- **DELETE** https://graph.microsoft.com/v1.0/groups/{id}
```
await graphClient.Groups[groupId3]
	.Request()
	.DeleteAsync();
```
- Si no existe el id del grupo presentar un error
```
  Standard Output: 
    Error: Code: Request_ResourceNotFound
    Message: Resource '2e904035-1e11-4e40-8e6d-5a7e30105e5b' does not exist or one of its queried reference-property objects are not present.
    Inner error:
    	AdditionalData:
    	date: 2021-08-05T20:09:44
    	request-id: 26947b90-0018-4503-ba8d-98b281eefa24
    	client-request-id: 26947b90-0018-4503-ba8d-98b281eefa24
    ClientRequestId: 26947b90-0018-4503-ba8d-98b281eefa24
```

#### Listar miembro de un grupo
- Listar miembros del grupo con sus propiedades
- **GET** https://graph.microsoft.com/v1.0/groups/{id}/members
- **GET** https://graph.microsoft.com/v1.0/users/{id | userPrincipalName}

```
//Obtener miembros de un grupo
var members = await graphClient.Groups[allCompanyGroup].Members
	.Request()
	.GetAsync();

//Obtener propiedades del miembro (user)	
var user = await graphClient.Users[member.Id]
	.Request()
	.GetAsync();	
```

---

# Gestión a través de APIREST
- https://docs.microsoft.com/en-us/graph/api/resources/users?view=graph-rest-1.0

#### Generalidades
- El token dura 60 minutos
- Las operaciones son realizadas a través de llamadas http al API.

```
https://graph.microsoft.com/v1.0/v1.0/groups
```

#### Listar grupos
- Permite listar los grupos existentes
**Response**
- If successful, this method returns a 200 OK response code and collection of group objects in the response body. The response includes only the default properties of each group.
```
GET https://graph.microsoft.com/v1.0/v1.0/groups/
```

#### Obtener un grupo
- Permite obtener un grupo a partir de su id
**Response**
- If successful, this method returns a 200 OK response code and group object in the response body. It returns the default properties unless you use $select to specify specific properties.

```
GET https://graph.microsoft.com/v1.0/v1.0/groups/{group_id}
```

#### Crear un grupo
- Permite crear un grupo
**Response**
- If successful, this method returns a 201 Created response code and a group object in the response body. The response includes only the default properties of the group.

```
POST https://graph.microsoft.com/v1.0/v1.0/groups

{
  "displayName": "Grupo para crear",
  "description": "Descripción de grupo para crear",
  "groupTypes": [
    "Unified"
  ],
  "mailEnabled": true,
  "mailNickname": "grupocrear",
  "securityEnabled": false
}
```

#### Actualizar grupo
- Permite actualizar un grupo
**Response**
- If successful, this method returns a 204 No Content response code—except a 200 OK response code when updating the following properties: allowExternalSenders, autoSubscribeNewMembers, hideFromAddressLists, hideFromOutlookClients, isSubscribedByMail, unseenCount.

```
PATCH https://graph.microsoft.com/v1.0/v1.0/groups/{grupo_actualizar_id}

{
  "description": "Prueba 1 desde Postman actualizado",
  "displayName": "Descripción prueba 1 desde Postman actualizado",
  "groupTypes": [
    "Unified"
  ],
  "mailEnabled": true,
  "mailNickname": "prueba1PostmanActualizado",
  "securityEnabled": false
}
```

#### Listar usuarios
- Permite listar los usuarios
**Response**
- If successful, this method returns a 200 OK response code and collection of user objects in the response body. If a large user collection is returned, you can use paging in your app.

```
GET https://graph.microsoft.com/v1.0/v1.0/users
```

#### Crear usuarios
- Permite crear un nuevo usuario
- Es necesario enviar el parámetro usageLocation ya que es requerido para la asignación de las licencias

**Response**
- If successful, this method returns 201 Created response code and user object in the response body.

```
POST https://graph.microsoft.com/v1.0/v1.0/users

{
	"accountEnabled": true,
	"displayName": "Usuario Prueba 1",
	"mailNickname": "usuarioprueba1",
	"userPrincipalName": "usuarioprueba1@jcprojectmicrosoft.onmicrosoft.com",
	"passwordProfile": [
		"forceChangePasswordNextSignIn", true,
		"password", "passwordNewUser1"
	],
    "usageLocation":"US"
}

```

**Errores**
- License assignment cannot be done for user with invalid usage location.” error when applying a license via Graph API
	- Es necesario especificar el usageLocation. [Leer más](https://docs.microsoft.com/es-es/archive/blogs/dsadsi/did-you-get-a-license-assignment-cannot-be-done-for-user-with-invalid-usage-location-error-when-applying-a-license-via-graph-api)


#### UpdateUser
- Permite actualizar los datos de un usuario a partir de su id. No se puede cambiar la clave por restricciones de permisos. [Detalles](https://docs.microsoft.com/en-us/azure/active-directory/fundamentals/users-default-permissions#compare-member-and-guest-default-permissions)
**Response**
- If successful, this method returns a 204 No Content response code.


```
PATCH https://graph.microsoft.com/v1.0/v1.0/users/{user_id}

{
    "userType": "Member",
    "surname": "surname usuario",
    "streetAddress": "dirección del usuario 1",
    "state": "new york",
    "postalCode": "123456",
	"accountEnabled": true,
	"displayName": "Usuario Prueba 1",
	"mailNickname": "usuarioprueba1",
	"userPrincipalName": "usuarioprueba1@jcprojectmicrosoft.onmicrosoft.com"
}

```
**Error**
```
Message: 
    System.Exception : Failed to call the web API: Forbidden. Content: {"error":{"code":"Authorization_RequestDenied","message":"Insufficient privileges to complete the operation.","innerError":{"date":"2021-08-05T23:31:52","request-id":"dedd179d-d940-4fc5-a03b-e7e4ab6ec938","client-request-id":"dedd179d-d940-4fc5-a03b-e7e4ab6ec938"}}}

```

#### Agregar propietario a un grupo
- Permite agregar propietarios a un grupo a partir de un usuario
**Response**
- If successful, this method returns a 204 No Content response code. It does not return anything in the response body. This method returns a 400 Bad Request response code when the object is already a member of the group. This method returns a 404 Not Found response code when the object being added doesn't exist.

```
POST https://graph.microsoft.com/v1.0/v1.0/groups/{grupo_id}/owners/$ref

{
	"@odata.id": $"https://graph.microsoft.com/v1.0/users/{user_id}"
}
```

#### Agregar un miembro a un grupo
- Permite agregar miembros a un grupo a partir de usuarios
**Response**
- If successful, this method returns a 204 No Content response code. It does not return anything in the response body. This method returns a 400 Bad Request response code when the object is already a member of the group. This method returns a 404 Not Found response code when the object being added doesn't exist.
- Límite de miembros por request. https://docs.microsoft.com/en-us/graph/api/group-post-members?view=graph-rest-1.0&tabs=http#example-2-add-multiple-members-to-a-group-in-a-single-request

```
POST https://graph.microsoft.com/v1.0/v1.0/groups/{grupo_id}/members/$ref

{
	"@odata.id": $"https://graph.microsoft.com/v1.0/directoryObjects/{user_id}"
}
```

#### Crear un grupo con un propietario
- Permite crear un grupo con propietarios
**Response**
- If successful, this method returns a 201 Created response code and a group object in the response body. The response includes only the default properties of the group.

```
POST https://graph.microsoft.com/v1.0/v1.0/groups

{
	"displayName": "Grupo 2 desde REST con owner",
	"description": "Descripción de grupo 2 desde REST con owner",
	"mailEnabled": true,
	"mailNickname": "grupo2restowner",
	"securityEnabled": false,
	"groupTypes": [
		"Unified"
	],
	"owners@odata.bind": [
		"https://graph.microsoft.com/v1.0/users/{user_id}"
	]
}
```

#### Crear un grupo con propietario y miembros
- Permite crear un grupo con propietarios y miembros
**Response**
- If successful, this method returns a 201 Created response code and a group object in the response body. The response includes only the default properties of the group.

```
POST https://graph.microsoft.com/v1.0/v1.0/groups

{
	"displayName": "Grupo 2 desde REST con owner",
	"description": "Descripción de grupo 2 desde REST con owner",
	"mailEnabled": true,
	"mailNickname": "grupo2restowner",
	"securityEnabled": false,
	"groupTypes": [
		"Unified"
	],
	"owners@odata.bind": [
		"https://graph.microsoft.com/v1.0/users/{user_id}"
	],
	"members@odata.bind": [
		"https://graph.microsoft.com/v1.0/users/{user_id_1}",
		"https://graph.microsoft.com/v1.0/users/{user_id_2}",
		"https://graph.microsoft.com/v1.0/users/{user_id_3}",
		"https://graph.microsoft.com/v1.0/users/{user_id_4}"
	]
}
```

---

### Teams con rest api
- https://docs.microsoft.com/en-us/graph/api/resources/teams-api-overview?view=graph-rest-1.0
#### Generalidades
- El token dura 60 minutos
- For performance reasons, the create, get, and list operations return only a subset of more commonly used properties by default. These default properties are noted in the Properties section. To get any of the properties that are not returned by default, specify them in a $select OData query option.

#### Listar grupos de teams con version beta
- Permite listar todos los grupos de teams usando la version beta del api puntualmente expresion lambda. No se recomienda.

```
GET https://graph.microsoft.com/beta/{group_id}?$filter=resourceProvisioningOptions/Any(x:x eq 'Team')
```

#### Listar teams con programación
- Permite listar todos los grupos de teams usando un filtrado por programacion de los grupos por resourceProvisioningOptions = Team

```
GET https://graph.microsoft.com/v1.0/v1.0/groups/

var groups = result["value"].ToList();

var teams = groups.Where(x => x["resourceProvisioningOptions"].ToArray().Contains("Team")).ToList();
```

#### Crear un team con parámetros mínimos
- Crea un grupo con los parametros minimos.
- No funciona con el ejemplo en la documentacion ya que exige incluir un propietarios.
- Roles vacio es considerado un miembro normal.
- https://docs.microsoft.com/en-us/graph/api/team-post?view=graph-rest-1.0&tabs=http
**Errores**
```
POST https://graph.microsoft.com/v1.0/v1.0/teams

{
	"template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
	"displayName": "Teams Group Minimal",
	"description": "Grupo de teams con parametros minimos",
  
  
	"members":[
	  {
		 "@odata.type":"#microsoft.graph.aadUserConversationMember",
		 "roles":[
			"owner"
		 ],
		 "user@odata.bind":"https://graph.microsoft.com/v1.0/users('{user_id}')"
	  }
	],  
}


{
    "error": {
        "code": "BadRequest",
        "message": "Failed to execute Templates backend request CreateTeamFromTemplateRequest. Request Url: https://teams.microsoft.com/fabric/amer/templates/api/team, Request Method: POST, Response Status Code: BadRequest, Response Headers: Strict-Transport-Security: max-age=2592000\r\nx-operationid: 6d116b0aace8034c99283962ed7448a7\r\nx-telemetryid: 00-6d116b0aace8034c99283962ed7448a7-d926573c0cafa449-00\r\nX-MSEdge-Ref: Ref A: BA9937F11BB743C19C7AE7A7DF3A5213 Ref B: BY3EDGE0311 Ref C: 2021-08-06T23:21:12Z\r\nDate: Fri, 06 Aug 2021 23:21:11 GMT\r\n, ErrorMessage : {\"errors\":[{\"message\":\"A team owner must be provided when creating a team in application context.\",\"errorCode\":\"Unknown\"}],\"operationId\":\"6d116b0aace8034c99283962ed7448a7\"}",
        "innerError": {
            "date": "2021-08-06T23:21:12",
            "request-id": "10181b98-4541-45d9-828d-31d7b9c7141f",
            "client-request-id": "10181b98-4541-45d9-828d-31d7b9c7141f"
        }
    }
}
```

#### Crear team a partir de un grupo
- If the group was created less than 15 minutes ago, it's possible for the Create team call to fail with a 404 error code due to replication delays. The recommended pattern is to retry the Create team call three times, with a 10 second delay between calls.
- Los usuarios creados no tienen asignadas las licencias y no puede utilizar el teams.
- In order to create a team, the group you're creating it from must have a least one owner.
- The team that's created will always inherit from the group's display name, visibility, specialization, and members. Therefore, when making this call with the group@odata.bind property, the inclusion of team displayName, visibility, specialization, or members@odata.bind properties will return an error.
- Cuando se asigna el propietario al grupo puede tardar unos minutos hasta que permita crear el team.
- https://docs.microsoft.com/en-us/graph/api/team-post?view=graph-rest-1.0&tabs=http

**Forma 1**
```
PUT https://graph.microsoft.com/v1.0/v1.0/groups/{group_id}/team

{
	"displayName": "TestTeam",
	"description": "TestTeam",
	"visibility": 0,
	"memberSettings": {
		"allowCreateUpdateChannels": false,
		"allowDeleteChannels": false,
		"allowAddRemoveApps": false,
		"allowCreateUpdateRemoveTabs": false,
		"allowCreateUpdateRemoveConnectors": false
	},
	"messagingSettings": {
		"allowUserEditMessages": true,
		"allowUserDeleteMessages": true,
		"allowOwnerDeleteMessages": true,
		"allowTeamMentions": true,
		"allowChannelMentions": true
	}
}
```

**Forma 2**
```
POST https://graph.microsoft.com/v1.0/teams

{
  "template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
  "group@odata.bind": "https://graph.microsoft.com/v1.0/groups('{group_id}')"
}
```

**Error response**
- If the request is unsuccessful, this method returns a 400 Bad Request response code.

```
HTTP 400 Bad Request
```

The following are common reasons for this response:
- createdDateTime is set in the future.
- createdDateTime is correctly specified but the teamCreationMode instance attribute is missing or set to an invalid value.

#### Crear team con canales, tabs, apps y plantilla
- Permite crear un grupo de team con canales, tabs, acceso a apps y plantilla.
- https://docs.microsoft.com/en-us/graph/api/team-post?view=graph-rest-1.0&tabs=http

```
POST https://graph.microsoft.com/v1.0/teams

{
    "template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
    "visibility": "Private",
    "displayName": "Sample Engineering Team",
    "description": "This is a sample engineering team, used to showcase the range of properties supported by this API",
    "members":[
      {
         "@odata.type":"#microsoft.graph.aadUserConversationMember",
         "roles":[
            "owner"
         ],
         "user@odata.bind":"https://graph.microsoft.com/v1.0/users('f614b729-9236-47fe-a15e-dc0189a6cfdd')"
      }
    ],
    "channels": [
        {
            "displayName": "Announcements 📢",
            "isFavoriteByDefault": true,
            "description": "This is a sample announcements channel that is favorited by default. Use this channel to make important team, product, and service announcements."
        },
        {
            "displayName": "Training 🏋️",
            "isFavoriteByDefault": true,
            "description": "This is a sample training channel, that is favorited by default, and contains an example of pinned website and YouTube tabs.",
            "tabs": [
                {
                    "teamsApp@odata.bind": "https://graph.microsoft.com/v1.0/appCatalogs/teamsApps('com.microsoft.teamspace.tab.web')",
                    "displayName": "A Pinned Website",
                    "configuration": {
                        "contentUrl": "https://docs.microsoft.com/microsoftteams/microsoft-teams"
                    }
                },
                {
                    "teamsApp@odata.bind": "https://graph.microsoft.com/v1.0/appCatalogs/teamsApps('com.microsoft.teamspace.tab.youtube')",
                    "displayName": "A Pinned YouTube Video",
                    "configuration": {
                        "contentUrl": "https://tabs.teams.microsoft.com/Youtube/Home/YoutubeTab?videoId=X8krAMdGvCQ",
                        "websiteUrl": "https://www.youtube.com/watch?v=X8krAMdGvCQ"
                    }
                }
            ]
        },
        {
            "displayName": "Planning 📅 ",
            "description": "This is a sample of a channel that is not favorited by default, these channels will appear in the more channels overflow menu.",
            "isFavoriteByDefault": false
        },
        {
            "displayName": "Issues and Feedback 🐞",
            "description": "This is a sample of a channel that is not favorited by default, these channels will appear in the more channels overflow menu."
        }
    ],
    "memberSettings": {
        "allowCreateUpdateChannels": true,
        "allowDeleteChannels": true,
        "allowAddRemoveApps": true,
        "allowCreateUpdateRemoveTabs": true,
        "allowCreateUpdateRemoveConnectors": true
    },
    "guestSettings": {
        "allowCreateUpdateChannels": false,
        "allowDeleteChannels": false
    },
    "funSettings": {
        "allowGiphy": true,
        "giphyContentRating": "Moderate",
        "allowStickersAndMemes": true,
        "allowCustomMemes": true
    },
    "messagingSettings": {
        "allowUserEditMessages": true,
        "allowUserDeleteMessages": true,
        "allowOwnerDeleteMessages": true,
        "allowTeamMentions": true,
        "allowChannelMentions": true
    },
    "discoverySettings": {
        "showInTeamsSearchAndSuggestions": true
    },
    "installedApps": [
        {
            "teamsApp@odata.bind": "https://graph.microsoft.com/v1.0/appCatalogs/teamsApps('com.microsoft.teamspace.tab.vsts')"
        },
        {
            "teamsApp@odata.bind": "https://graph.microsoft.com/v1.0/appCatalogs/teamsApps('1542629c-01b3-4a6d-8f76-1938b779e48d')"
        }
    ]
}
```

#### Obtener un team
- Permite obtener los detalles de un grupo de teams.
```
GET https://graph.microsoft.com/v1.0/teams/{team_id}
```

#### Listar miembros de un team
- Permite listar los miembros de un grupo de teams

```
GET https://graph.microsoft.com/v1.0/teams/{team_id}/members
```

#### Agregar un miembro al team
- Permite agregar usuarios a un grupo de teams. Esta operación aún no permite utilizar teams, es necesarios asignar licencias de uso.
- Roles con un valor vacío significa que es un miembro normal.
- Si se envía varias veces el mismo usuario actúa como si lo agregara pero no indica ni error ni que está repetido.

```
POST https://graph.microsoft.com/v1.0/v1.0/teams/{team_id}/members

{
    "@odata.type": "#microsoft.graph.aadUserConversationMember",
    "roles": ["owner"],
    "user@odata.bind": 
        "https://graph.microsoft.com/v1.0/users('3568fafc-d437-4d21-90aa-96f413f131ec')"
}
```

#### Agregar miembros a un team
- Permite agregar varios usuarios a un grupo de teams en una sola petición. Esta operación aún no permite utilizar teams, es necesarios asignar licencias de uso.
- Roles con un valor vacío significa que es un miembro normal.
- Si se envía varias veces el mismo usuario actúa como si lo agregara pero no indica ni error ni que está repetido.

```
PATCH https://graph.microsoft.com/v1.0/groups/{group-id}

{
  "members@odata.bind": [
    "https://graph.microsoft.com/v1.0/directoryObjects/{user_id_1}",
    "https://graph.microsoft.com/v1.0/directoryObjects/{user_id_2}",
    "https://graph.microsoft.com/v1.0/directoryObjects/{user_id_3}"
    ]
}
```

#### Listar los sku suscritos
- Permite listar las licencias existentes, SKU(Stock Keeping Unit), en el plan de servicio. Esto es necesario para obtener el skus id para asignarlo al usuario y que pueda utilizar las apps que requiera.
- https://docs.microsoft.com/en-us/partner-center/develop/product-resources#sku
- https://docs.microsoft.com/es-es/azure/container-registry/container-registry-skus#:~:text=Azure%20Container%20Registry%20est%C3%A1%20disponible,de%20Docker%20privado%20en%20Azure.

```
GET https://graph.microsoft.com/v1.0/subscribedSkus
```

#### Asignar sku a miembro
- Permite asignar licencias a usuarios para utilizar diversos servicios. En el portal de azure se realiza desde Inicio > Grupo Team > Usuario > Licencias +, en este caso es Microsoft 365 E5 Developer (without Windows and Audio Conferencing).
- Es necesario enviar el parámetro usageLocation durante la creación o actualizarlo ya que es requerido para la asignación de las licencias.

```
POST https://graph.microsoft.com/v1.0/v1.0/users/{user_id}/assignLicense

{
  "addLicenses": [
    {
      "skuId": "c42b9cae-ea4f-4ab7-9717-81576235ccac"
    }
  ],
  "removeLicenses": []
}
```
