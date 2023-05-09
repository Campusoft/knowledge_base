# Erpnext

Free and Open Source Enterprise Resource Planning (ERP) 
https://github.com/frappe/erpnext


# Concepts

## Company

This represents the Company records for which ERPNext is setup. With this same setup, you can create multiple Company records, each representing a different legal entity. The accounting for each Company will be different, but they will share the Customer, Supplier and Item records.

## Customer

Represents a customer. A Customer can be an individual or an organization. You can create multiple Contacts and Addresses for each Customer.

## Supplier

Represents a supplier of goods or services. Your telephone company is a Supplier, so is your raw materials Supplier. Again, a Supplier can be an individual or an organization and has multiple Contacts and Addresses.

## Item

A Product, sub-product or Service that is either bought, sold or manufactured and is uniquely identified.

## Price List

A Price List is a place where different rate plans can be stored. It’s a name you give to a set of Item Prices stored under a particular List.

## Fiscal Year

Represents a Financial Year or Accounting Year. You can operate multiple Fiscal Years at the same time. Each Fiscal Year has a start date and an end date and transactions can only be recorded in this period. When you “close” a fiscal year, it's balances are transferred as “opening” balances for the next fiscal year.

## Sales Invoice

A bill sent to Customers for delivery of Items (goods or services).

## Purchase Invoice

A bill sent by a Supplier for delivery of Items (goods or services).



# Install


Trabajar con el repositorio frappe_docker

```
git clone https://github.com/frappe/frappe_docker
cd frappe_docker
```

Ejecutar docker-compose con el archivo pwd.yml que se encuentra en repositorio

```
docker-compose -f pwd.yml up
```
https://github.com/frappe/frappe_docker/tree/main




----------------------

Al ejecutar docker-compose up, para que utilice el archivo compose.yaml que existe en el repositorio se obtiene el siguiente error:


```
invalid interpolation format for x-customizable-image.image.
You may need to escape any $ with another $.
required variable ERPNEXT_VERSION is missing a value: No ERPNext version set
```

# Usuarios

How to check if Role is for System User or Website User?

In the Role master, if field "Desk Access" is checked, that Role is for System User. If Desk Access field is unchecked, then that Role is for Website User.


## Roles / Permisos

Los permisos se establecen en los roles y en los tipos de documentos (llamados 'DocType') Se establecen permisos como: lectura, escritura, crear, borrar, validar, cancelar, corregir, reporte, importar, exportar, imprimir, Email y los permisos de usuario


#  Frappe Framework.

## DocTypes

A DocType is the core building block of any application based on the Frappe Framework.

It describes the Model and the View of your data. It contains what fields are stored for your data, and how they behave with each other. It contains information about how your data is named




Field Types
https://docs.erpnext.com/docs/v13/user/manual/en/customize-erpnext/articles/field-types


Section Break
- Section Break is used to divide the form into multiple sections.
- Ej. Referencia. En bootstrap, seria un row una section.

## REST API 

Frappe framework generates REST API for all of your DocTypes out of the box. You can also run arbitrary python methods using their dotted module path.
https://frappeframework.com/docs/v14/user/en/api/rest


Frappe ships with an HTTP API that can be classified into Remote Procedure Calls (RPC), to call whitelisted methods and Representational State Transfer (REST), to manipulate resources.
- All documents in Frappe are available via a RESTful API with prefix /api/resource/. You can perform all CRUD operations on them:
https://frappeframework.com/docs/v14/user/en/guides/integration/rest_api


Token Based Authentication 
- There are two types of authorization: token and Basic
- Access Token
https://frappeframework.com/docs/v14/user/en/guides/integration/rest_api/token_based_authentication

## Bench

Bench is a CLI tool to manage Frappe Deployments. It provides an easy interface to help you setup and manage multiple sites and apps based on Frappe Framework.


# Referencias


How ERPNext superior than Odoo - An Odoo Consultant Perspective
- Odoo itself was also 100% Open Source when started, until Version 9, the license was changed from AGPL (100% Open) to LGPL (Open Core). Subsequently, serveral key modules have been removed from the community version.
https://ecosoft-odoo.blogspot.com/2023/05/how-erpnext-superior-than-odoo-odooer.html