# Active Directory Federation Services - ADFS

Configuring Single Sign-on with ADFS can be done in two ways, depending on your ADFS version.

For ADFS 4.0, on Windows Server 2016 and up, use OpenID

For ADFS 2.0 and 3.0, on Windows Server 2012 R2 and below, use SAML


# OpenID Connect / OAuth


If you use WS-Fed or SAML, the client is a relying party (RP).

If you use OpenID Connect / OAuth, the client is inside an application group. But functionally, it's the same as a RP.



# Referencias

Active Directory Federation Services
https://docs.microsoft.com/en-us/windows-server/identity/active-directory-federation-services


AD FS OpenID Connect/OAuth Concepts
https://docs.microsoft.com/en-us/windows-server/identity/ad-fs/development/ad-fs-openid-connect-oauth-concepts


AD FS OpenID Connect/OAuth flows and Application Scenarios
https://docs.microsoft.com/en-us/windows-server/identity/ad-fs/overview/ad-fs-openid-connect-oauth-flows-scenarios


---------------
Check if the endpoints are enabled
AD FS provides various endpoints for different functionalities and scenarios. Not all endpoints are enabled by default. 
https://support.microsoft.com/en-us/topic/check-if-the-endpoints-are-enabled-401d8d08-3808-56d7-bf88-3248f5960ade

------------------
Algunas enlaces con pantllas para configuraciones OpenId en ADFS

Windows 2016 - ADFS 4.0
https://django-auth-adfs.readthedocs.io/en/latest/adfs_4.0_config_guide.html

OAuth/OpenID Single Sign On (SSO) into Bamboo using ADFS
https://plugins.miniorange.com/oauth-openid-single-sign-on-sso-bamboo-using-adfs#step_1

--------------------------------

OpenID Configuration URL 

In order to configure ADFS federation in your vCenter Server, you will need to know your ADFS server's OpenID Configuration URL. This is the standard OpenID Connect (OIDC) Discovery Endpoint that advertises OIDC metadata information about an OAuth identity provider. It is a well-known address that is typically the issuer endpoint concatenated with the path “/.well-known/openid-configuration”. For example: https://adfsserver01.corp.local/adfs/.well-known/openid-configuration

To obtain the OpenID Address for your ADFS server:

    Open a PowerShell terminal on your ADFS server as an Administrator
    Execute the following command:

Get-AdfsEndpoint | Select FullUrl | Select-String openid-configuration

    Copy the URL that is returned (select only the URL itself, not the closing bracket or the initial "@{FullUrl=" part)
    Use this URL whenever vCenter asks for the OpenID Address

Ref Img:
![imagen](https://user-images.githubusercontent.com/222181/104085338-935f7280-521c-11eb-95ae-e493cb79ff32.png)

	
https://kb.vmware.com/s/article/78029

