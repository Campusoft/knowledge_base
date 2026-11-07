# Access


# Access Postman / Sin Azure Registration Application

Los siguientes tres articulos, son similares para generar ClienteID, y ClientSecret, sin hacerlo con azure registration application. 

1) Getting an Access Token for SharePoint Online. (Una referencia)
https://anexinet.com/blog/getting-an-access-token-for-sharepoint-online/


2) Access SharePoint Online using Postman
http://www.ktskumar.com/2017/01/access-sharepoint-online-using-postman/

3)  Sharepoint Online Authentication for API Access using POSTMAN 
http://pratapreddypilaka.blogspot.com/2018/05/sharepoint-online-authentication-for.html
 
 
 
Notas:

El valor "resource", para generar el token. Tiene la siguiente estructura:

[SharePoint Online application principal ID]/[Tenant-Name].sharepoint.com@[Tenant-ID]

[SharePoint Online application principal ID] is always 00000003-0000-0ff1-ce00-000000000000

Mayor informacion sharepoint Online Application principal ID. Su valor: 
https://docs.microsoft.com/en-us/openspecs/sharepoint_protocols/ms-sps2sauth/e76bf208-f2b7-4136-9592-a657adf51581#Appendix_A_1

Section 2.2:  00000003-0000-0ff1-ce00-000000000000 identifies SharePoint Server 2013.


Otros enlaces:
Using Postman on SharePoint REST service
https://genuineprogrammer.com/blog/2018/09/10/using-postman-on-sharepoint-rest-service/
