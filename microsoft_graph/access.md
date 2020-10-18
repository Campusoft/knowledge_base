


## Types of permissions


Accessing Microsoft Graph endpoints requires that the application and / or user making the request has the appropriate permissions assigned.  These permissions can be one of two types: delegated permissions or application permissions.

https://developer.microsoft.com/en-us/graph/blogs/30daysmsgraph-day-11-azure-ad-application-permissions/



Choose a Microsoft Graph authentication provider based on scenario
https://docs.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=CS#UsernamePasswordProvider



## Labs:



###  Consumir graph con Postman.


Use Postman with the Microsoft Graph API (Tiene collections / environment) de Postman
https://docs.microsoft.com/en-us/graph/use-postman


1. Crear Aplicacion en Azure


- Create a new Azure AD application.
- Assign the delegated permission for Mail.Read.
- Assign the delegated permission for Calendars.ReadWrite.
- Assign the application permission for User.Invite.All.
- Grant admin consent to the application.

https://developer.microsoft.com/en-us/graph/blogs/30daysmsgraph-day-9-azure-ad-applications-on-v2-endpoint/


2. Postman to make Microsoft Graph calls


- Registering the Azure AD App
- Get admin consent for the app
- Get access token using the app
- Make Microsoft Graph API call using the access token as bearer token


https://developer.microsoft.com/en-us/graph/blogs/30daysmsgraph-day-13-postman-to-make-microsoft-graph-calls/


**Errores**

----------------


Consumir Graph con .net core


Errores:

Original exception: AADSTS7000218: The request body must contain the following parameter: 'client_assertion' or 'client_secret'.


Solution:

https://github.com/azuread/microsoft-authentication-library-for-dotnet/wiki/Client-Applications#invalid-client


-------------------

Resource '<GUID>' does not exist or one of its queried reference-property objects are not present.

Solution:

You are obtaining a token via the confidential client flow, meaning you not running in the context of a user. Thus you should not be using the /me endpoint, but /users/objectID one.

https://docs.microsoft.com/en-us/answers/questions/3432/microsoft-graph-api-unable-to-get-logged-in-user-d.html


----------------