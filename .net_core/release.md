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