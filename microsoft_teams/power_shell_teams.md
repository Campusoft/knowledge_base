Microsoft Teams PowerShell 
https://docs.microsoft.com/en-us/microsoftteams/teams-powershell-overview


Errores:

---------------------

Connect-MicrosoftTeams : One or more errors occurred.: AADSTS50076: Due to a configuration change made by your
administrator, or because you moved to a new location, you must use multi-factor authentication to access


This is happening because Security Defaults is enabled for your tenant. You can disable it by navigating to Azure Portal > Azure AD > Properties > Click on Manage Security Defaults link > Toggle Enable Security Defaults button to NO.

Refer to https://docs.microsoft.com/en-us/azure/active-directory/fundamentals/concept-fundamentals-security-defaults for more information about Security Defaults.



Link:
https://docs.microsoft.com/en-us/answers/questions/22998/login-message-says-i-must-use-mfa-but-signupsignin.html

----------------