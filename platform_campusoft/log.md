# Logs


Castle.LoggingFacility.MsLogging

# Laboratorios

## Utilizar nlog con LoggingFacility Castle

- Instalar  Castle.Core-NLog 
- Configurar correctamente el AddFacility

  container.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>());
  
Informacion para utilizar nlog con AddFacility correctamente:  
https://github.com/castleproject/Core/issues/292#issuecomment-330053520
  


#Referencias

Logging Facility
https://github.com/castleproject/Windsor/blob/master/docs/logging-facility.md

ASP.NET Boilerplate uses Castle Windsor's logging facility.
https://aspnetboilerplate.com/Pages/Documents/Logging
 

