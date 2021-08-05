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

# Microsoft Graph 
## GraphNetCore
### GroupsSdkTest
#### Generalidades
- El token dura 60 minutos
- For performance reasons, the create, get, and list operations return only a subset of more commonly used properties by default. These default properties are noted in the Properties section. To get any of the properties that are not returned by default, specify them in a $select OData query option.
#### GetAuthProvider
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

#### ListGroups
- Permite listar los grupos configurados.
- En caso de no existir grupos devuelve un total de 0 grupos.
- **GET** https://graph.microsoft.com/v1.0/groups
```
var groups = await graphClient.Groups
	.Request()
	.GetAsync();
```        

#### ListGroupsWithSelectedProperties
- Permite listar los grupos creados con propiedades específicas.
- **GET** https://graph.microsoft.com/v1.0/groups?$select=id,createdDateTime,visibility

```
var groups = await graphClient.Groups
	.Request()
	.Select("id, createdDateTime, visibility")
	.GetAsync();
```    

#### ListFilteredGroups
- Permite listar grupos a partir de filtros definidos
- **GET** https://graph.microsoft.com/v1.0/groups?$filter=displayName eq 'Grupo de prueba 2'

```
var groups = await graphClient.Groups
	.Request()
	.Filter($"displayName eq 'Prueba mínima desde Postman'")
	.GetAsync();
```
     
#### CreateGroups
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

#### CreateRepeatGroups
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

#### CreateMinumunGroup
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

#### GetGroup
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

#### UpdateGroup
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

#### DeleteGroup
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

#### ListMembers
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
