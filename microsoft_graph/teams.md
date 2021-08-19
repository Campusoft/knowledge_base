# Microsoft Graph 
## Teams con rest api
- https://docs.microsoft.com/en-us/graph/api/resources/teams-api-overview?view=graph-rest-1.0
### Generalidades
- El token dura 60 minutos
- For performance reasons, the create, get, and list operations return only a subset of more commonly used properties by default. These default properties are noted in the Properties section. To get any of the properties that are not returned by default, specify them in a $select OData query option.

### Listar grupos de teams con version beta
- Permite listar todos los grupos de teams usando la version beta del api puntualmente expresion lambda. No se recomienda.

```
GET https://graph.microsoft.com/beta/{group_id}?$filter=resourceProvisioningOptions/Any(x:x eq 'Team')
```

### Listar teams con programación
- Permite listar todos los grupos de teams usando un filtrado por programacion de los grupos por resourceProvisioningOptions = Team

```
GET https://graph.microsoft.com/v1.0/v1.0/groups/

var groups = result["value"].ToList();

var teams = groups.Where(x => x["resourceProvisioningOptions"].ToArray().Contains("Team")).ToList();
```

### Crear un team con parámetros mínimos
- Crea un grupo con los parametros minimos.
- No funciona con el ejemplo en la documentacion ya que exige incluir un propietario.
- La generación no es inmediata, puede demorar hasta 15 minutos. https://docs.microsoft.com/en-us/graph/api/team-post?view=graph-rest-1.0&tabs=http#example-4-create-a-team-from-group
- Roles vacio es considerado un miembro normal.
- https://docs.microsoft.com/en-us/graph/api/team-post?view=graph-rest-1.0&tabs=http
- A pesar de ser un arreglo de miembros no se puede agregar mas de un miembro
#### **Errores**
```
POST https://graph.microsoft.com/v1.0/v1.0/teams

Correcto
{
	"template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
	"displayName": "Teams Group Minimal",
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

Incorrecto
{
  "template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
  "displayName": "My Sample Team",
  "description": "My Sample Team’s Description"
}

Error producido
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


Request con varios miembros
{
    "template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
    "displayName": "Teams Group Minimal 1",
    "members":[
        {
            "@odata.type":"#microsoft.graph.aadUserConversationMember",
            "roles":["owner"],
            "user@odata.bind":"https://graph.microsoft.com/v1.0/users('f614b729-9236-47fe-a15e-dc0189a6cfdd')"
        },
        {
            "@odata.type":"#microsoft.graph.aadUserConversationMember",
            "roles":[],
            "user@odata.bind":"https://graph.microsoft.com/v1.0/users('3d31920a-5c30-4590-b8ca-2b768824d40d')"
        },
        {
            "@odata.type":"#microsoft.graph.aadUserConversationMember",
            "roles":["owner"],
            "user@odata.bind":"https://graph.microsoft.com/v1.0/users('7b8dc73a-baa4-4cf1-9044-356e1dfecaf6')"
        }        
    ]
}

{
    "error": {
        "code": "BadRequest",
        "message": "Adding more than one member is not supported.",
        "innerError": {
            "message": "Adding more than one member is not supported.",
            "code": "InvalidRequest",
            "innerError": {},
            "date": "2021-08-12T22:37:34",
            "request-id": "5f53dd24-6171-4ffd-ad87-db66f13a8aa2",
            "client-request-id": "5f53dd24-6171-4ffd-ad87-db66f13a8aa2"
        }
    }
}
```

### Crear team a partir de un grupo
- If the group was created less than 15 minutes ago, it's possible for the Create team call to fail with a 404 error code due to replication delays. The recommended pattern is to retry the Create team call three times, with a 10 second delay between calls.
- Los usuarios creados no tienen asignadas las licencias y no puede utilizar el teams. [Fuente](https://answers.microsoft.com/en-us/msoffice/forum/all/i-am-getting-a-youre-missing-out-ask-your-admin-to/3a36ff27-6608-4272-92cb-feccc6c1ec0d)
- [Agregar licencias a usuarios](https://docs.microsoft.com/en-us/graph/api/user-assignlicense?view=graph-rest-1.0&tabs=http)
- In order to create a team, the group you're creating it from must have a least one owner.
- The team that's created will always inherit from the group's display name, visibility, specialization, and members. Therefore, when making this call with the group@odata.bind property, the inclusion of team displayName, visibility, specialization, or members@odata.bind properties will return an error.
- Cuando se asigna el propietario al grupo puede tardar unos minutos hasta que permita crear el team.
- https://docs.microsoft.com/en-us/graph/api/team-post?view=graph-rest-1.0&tabs=http

#### **Forma 1**
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

#### **Forma 2**
```
POST https://graph.microsoft.com/v1.0/teams

{
  "template@odata.bind": "https://graph.microsoft.com/v1.0/teamsTemplates('standard')",
  "group@odata.bind": "https://graph.microsoft.com/v1.0/groups('{group_id}')"
}
```

#### **Error response**
- If the request is unsuccessful, this method returns a 400 Bad Request response code.

```
HTTP 400 Bad Request
```

The following are common reasons for this response:
- createdDateTime is set in the future.
- createdDateTime is correctly specified but the teamCreationMode instance attribute is missing or set to an invalid value.

### Crear team con canales, tabs, apps y plantilla
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

### Obtener un team
- Permite obtener los detalles de un grupo de teams.
```
GET https://graph.microsoft.com/v1.0/teams/{team_id}
```

### Listar miembros de un team
- Permite listar los miembros de un grupo de teams

```
GET https://graph.microsoft.com/v1.0/teams/{team_id}/members
```

### Agregar un miembro al team
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

### Agregar miembros a un team
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

### Listar los sku suscritos
- Permite listar las licencias existentes, SKU(Stock Keeping Unit), en el plan de servicio. Esto es necesario para obtener el skus id para asignarlo al usuario y que pueda utilizar las apps que requiera.
- https://docs.microsoft.com/en-us/partner-center/develop/product-resources#sku
- https://docs.microsoft.com/es-es/azure/container-registry/container-registry-skus#:~:text=Azure%20Container%20Registry%20est%C3%A1%20disponible,de%20Docker%20privado%20en%20Azure.

```
GET https://graph.microsoft.com/v1.0/subscribedSkus
```

### Asignar sku a miembro
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
