

# Release

Multiple Ways To Set Hosting Environment In .NET Applications
- Local Debugging – launchsettings.json
- IIS App Pools – applicationHost.config
- Web.Config for IIS based deployments
- Azure Web Apps Environment
https://thecodeblogger.com/2021/04/12/multiple-ways-to-set-hosting-environment-in-net-applications/

# IIS



# hsts

// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

# Environment Identifier in .NET (core)

The .NET 5 is now the latest version of .NET Core (hereafter referred to as .NET). And it uses JSON configuration file, appsettings.json. The ASPNETCORE_ENVIRONMENT is the environment variable which .NET uses to determine the name of environment.

Just to add up to the complications, there is one more environment variable DOTNET_ENVIRONMENT. This also serves the same purpose. If both environment variables are defined, then ASPNETCORE_ENVIRONMENT takes priority and it overrides the DOTNET_ENVIRONMENT value.

By default, 3 values and methods are provided by framework for ASPNETCORE_ENVIRONMENT:

- Development – you can use IWebHostEnvironment.IsDevelopment() method to see if the code is running in development environment.
- Staging – you can use IWebHostEnvironment.IsStaging() method to see if the code is running in staging environment.
- Production – The default value, if nothing is set on DOTNET_ENVIRONMENT and ASPNETCORE_ENVIRONMENT. You can use IWebHostEnvironment.IsProduction() method to see if the code is running in production environment.
	
But if you have more than 3 environments or the environment names are different, you can set the environment variable value to any string value(e.g. Test, Dev, Performance, Security, Load, etc.)

## When running application locally

When running application locally, you can use launchsettings.json file (in Properties folder), to set the environment variables. You can define multiple profiles in the JSON file as shown below

```
{
  "profiles": {
    "IGG.DbMigrator": {
      "commandName": "Project"
    },
    "IGG.DbMigrator.Local": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Local"
      }
    }
  }
}
```

Este archivo posee dos profiles "IGG.DbMigrator", "IGG.DbMigrator.Local".
- En el profiles "IGG.DbMigrator.Local", se establece el ambiente "Local", en la variable de entorno "ASPNETCORE_ENVIRONMENT"
  - Este profile puede combinarse con appsettings. El archivo "appsettings.Local.json", sera el appsettings para el ambiente "Local", establecido en el profile
  
  

