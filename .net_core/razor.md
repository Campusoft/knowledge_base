

# Razor 

## Reusable UI using the Razor class library 

El proyecto que tiene los objetos UI que seran compartidos, la estructura del proyecto  debe ser similar a:
```
 <Project Sdk="Microsoft.NET.Sdk.Razor">
 
   <PropertyGroup>
     <TargetFramework>netstandard2.0</TargetFramework>   </PropertyGroup>
 
   <ItemGroup>
     <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.2.0" />   
	</ItemGroup> 
  </Project>
```


Referencias
Create reusable UI using the Razor class library project in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-2.2&tabs=visual-studio
