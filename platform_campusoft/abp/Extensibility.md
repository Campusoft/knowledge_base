# Extensibility


# Personalizacion

Obtener el codigo fuente de los modulos. (Opcion 1)

get-source (CLI option)
https://docs.abp.io/en/abp/latest/CLI#get-source


Customizing the Existing Modules
https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Guide

# Extending Entities

- All extra properties of an entity are stored as a single JSON object in the database table
-  ABP Framework entity extension system for the Entity Framework Core that allows you to use the same extra properties, but store a desired property as a separate field in the database table.


# Module Entity Extensions

Module entity extension system is a high level extension system that allows you to define new properties for existing entities of the depended modules. It automatically adds properties to the entity, database, HTTP API and the user interface in a single point.

- Configurar nuevos campos.
- Al agregar nuevos campos se visualiza por defecto en UI - Lista, UI - Form Create, UI = Form Edit. Se puede configurar en cuales se visualiza. 
- Se puede establecer a nivel API, si el campo esta disponible. 
- Module extension system naturally supports the enum types.
https://docs.abp.io/en/abp/latest/Module-Entity-Extensions

UI Visibility
https://docs.abp.io/en/abp/latest/Module-Entity-Extensions#ui-visibility

# UI Extensibility


## Table UI / Angular

- Data Table Column Extensions

Data table column extension system allows you to add a new column in the data table on the user interface
https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Guide


Entity prop extension system allows you to add a new column to the data table for an entity or change/remove an already existing one. 
https://docs.abp.io/en/abp/latest/UI/Angular/Data-Table-Column-Extensions


You can use the valueResolver to render an HTML string in the table.
(Ejemplo, personlizar la salida de una columna, colocar en mayusculas, agregar un icono segun el valor)
https://docs.abp.io/en/abp/latest/UI/Angular/Data-Table-Column-Extensions#how-to-render-custom-html-in-cells


- Entity Actions

Entity action extension system allows you to add a new action to the action menu for an entity on the user interface
https://docs.abp.io/en/abp/latest/UI/Angular/Entity-Action-Extensions

How Replaceable Components Work with Extensions
https://docs.abp.io/en/abp/latest/UI/Angular/How-Replaceable-Components-Work-with-Extensions
