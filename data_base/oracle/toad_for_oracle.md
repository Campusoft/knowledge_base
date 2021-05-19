# Toad for Oracle

## Toad marca errores en las consultas SQL, cuando existen líneas en blanco

A partir de la versión 12.9 de Toad, se agregó una configuración que por defecto se encuentra activada "Treat blank line as statement terminator" (Tratar línea en blanco como terminador de declaración) esto provoca que Toad marque esto como error.
Para evitarlo se puede deshabilitar el check en la configuración de Toad View => Toad Options => Execute/Compile.
![image](https://user-images.githubusercontent.com/11231959/118861230-e2d83e00-b8a1-11eb-860b-283b17a01646.png)
Rererencia: https://forums.toadworld.com/t/toad-doesnt-like-blank-lines-in-sql-statements/37229
