## Types of permissions


Accessing Microsoft Graph endpoints requires that the application and / or user making the request has the appropriate permissions assigned.  These permissions can be one of two types: delegated permissions or application permissions.

- Delegated permissions, sometimes called “on behalf of” permissions, require a user context to also be supplied when making the request.  In effect an application is making Microsoft Graph requests on behalf of the user.  As such, the required permissions will be a combination of 1) what the user has permissions to do and 2) what the application has permissions to do.

- Application permissions, sometimes called app-only or “without a user”, run without a user context.  Common examples of this would be a background service or a daemon application.  Only the permissions granted to the application will be evaluated when Microsoft Graph request is made.


https://developer.microsoft.com/en-us/graph/blogs/30daysmsgraph-day-11-azure-ad-application-permissions/



Choose a Microsoft Graph authentication provider based on scenario
https://docs.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=CS#UsernamePasswordProvider



Choose a Microsoft Graph authentication provider based on scenario. Work Microsoft Graph SDKs
https://docs.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=CS


## Labs:


### Consumir graph con .net Core


In this quickstart, you download and run a code sample that demonstrates how a .NET Core console application can get an access token to call the Microsoft Graph API and display a list of users in the directory. The code sample also demonstrates how a job or a windows service can run with an application identity, instead of a user's identity.
https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-netcore-daemon


**errores**


If you try to run the application at this point, you'll receive HTTP 403 - Forbidden error: Insufficient privileges to complete the operation. This happens because any app-only permission requires Admin consent, which means that a global administrator of your directory must give consent to your application. Select one of the options below depending on your role:



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




Original exception: AADSTS7000218: The request body must contain the following parameter: 'client_assertion' or 'client_secret'.


Solution:

https://github.com/azuread/microsoft-authentication-library-for-dotnet/wiki/Client-Applications#invalid-client


-------------------

Resource '<GUID>' does not exist or one of its queried reference-property objects are not present.

Solution:

You are obtaining a token via the confidential client flow, meaning you not running in the context of a user. Thus you should not be using the /me endpoint, but /users/objectID one.

https://docs.microsoft.com/en-us/answers/questions/3432/microsoft-graph-api-unable-to-get-logged-in-user-d.html


----------------