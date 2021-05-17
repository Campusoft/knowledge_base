# react-admin


A frontend Framework for building admin applications running in the browser on top of REST/GraphQL APIs, using ES6, React and Material Design http://marmelab.com/react-admin
https://github.com/marmelab/react-admin

# Conceptos

- DataProviders. Permiten establecer el dialecto "estructura,restricciones, etc" de los api que se consuman para las acciones CRUD.
- AuthProvider. Permite establecer el mecanismo autentificacion y  autorizacion.

Data Providers.
Los data provider permiten mapear el request y el response. 
The dataProvider is also the ideal place to add custom HTTP headers, authentication, etc.

https://camo.githubusercontent.com/cbd81d5761596346d61937020e8e2b00935bd585ebf6d40a638fa1def27a354e/68747470733a2f2f6d61726d656c61622e636f6d2f72656163742d61646d696e2f696d672f646174612d70726f76696465722e706e67
 
https://marmelab.com/react-admin/DataProviders.html


# UI

Implementa Material-UI en sus componenetes

## Filtering The List

Las listas pueden llevar las siguientes propiedades

title => sobre escribe el titulo en la barra de navegacion. Puede ser string o elementp JSX

bulkActionButtons => permite personalizar la seleccion de uno o varios elementos y realizar alguna operacion 

https://marmelab.com/react-admin/List.html#filters-filter-inputs

# Referencias

Referencias aplicaciones

React-admin CRM

This is a demo of the react-admin library for React.js. It's a CRM for a fake Web agency with a few sales. You can test it online at https://marmelab.com/react-admin-crm.

https://github.com/marmelab/react-admin/tree/master/examples/crm


RA Language Spanish License: MIT
Spanish translations for React Admin

https://github.com/BlackBoxVision/react-admin-extensions/tree/main/packages/ra-language-spanish