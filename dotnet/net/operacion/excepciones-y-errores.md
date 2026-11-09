# Excepciones y errores en .NET

## Exceptions 

Exceptions should be uncommon. In comparison to other code flow patterns, the catch and throw of exceptions is slow. Exceptions are not used to control the program’s flow. Consider the program’s logic when identifying and resolving exception-prone scenarios.

## Exceptions


Best practices for exceptions
https://docs.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions

## Errores

### Unable to connect to web server 'IIS Express' 
 
- Posible problema con puertos, verificar y cambiar el puerto utilizado en: launchSettings.json utilizando el comando: netsh interface ipv4 show excludedportrange protocol=tcp para listar los puertos ocupados
- Fuente: https://www.variablenotfound.com/2020/06/como-solucionar-el-error-unable-to.html
