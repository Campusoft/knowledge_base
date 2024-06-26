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
- Docker images for production and development setups of the Frappe framework and ERPNext



```
git clone https://github.com/frappe/frappe_docker
cd frappe_docker
```

Ejecutar docker-compose con el archivo pwd.yml que se encuentra en repositorio
- Db: mariadb

```
docker-compose -f pwd.yml up
```
https://github.com/frappe/frappe_docker/tree/main



```
docker-compose -f compose.yaml -f overrides/compose.postgres.yaml config
```


----------------------

Al ejecutar docker-compose up, para que utilice el archivo compose.yaml que existe en el repositorio se obtiene el siguiente error:


```
invalid interpolation format for x-customizable-image.image.
You may need to escape any $ with another $.
required variable ERPNEXT_VERSION is missing a value: No ERPNext version set
```

Solucion:

We use environment variables to configure our setup. docker-compose uses variables from .env file. To get started, copy example.env to .env.


# Development


Development using containers
- Use VSCode Remote Containers extension
https://github.com/frappe/frappe_docker/blob/main/docs/development.md


# Usuarios

How to check if Role is for System User or Website User?

In the Role master, if field "Desk Access" is checked, that Role is for System User. If Desk Access field is unchecked, then that Role is for Website User.


## Roles / Permisos

Los permisos se establecen en los roles y en los tipos de documentos (llamados 'DocType') Se establecen permisos como: lectura, escritura, crear, borrar, validar, cancelar, corregir, reporte, importar, exportar, imprimir, Email y los permisos de usuario


# Movil


Frappe/ERPNext web views are mobile friendly, has full support for ERPNext features, need less maintenance and users can directly access sites from the mobile browser itself.

Due to this, we planned to deprecate mobile apps and we will be delisting the apps on both the stores.

This Project uses provider for State Management. hive, shared_preferences for storage. dio for making network requests.
https://github.com/frappe/mobile





# Versions 


What’s new in the Frappe and ERPNext V13 redesign?
- Bootstrap 4
https://erpnext.com/blog/ERPNext%20Features/what%E2%80%99s-new-in-redesign


# Frappe Books


# Referencias


How ERPNext superior than Odoo - An Odoo Consultant Perspective
- Odoo itself was also 100% Open Source when started, until Version 9, the license was changed from AGPL (100% Open) to LGPL (Open Core). Subsequently, serveral key modules have been removed from the community version.
https://ecosoft-odoo.blogspot.com/2023/05/how-erpnext-superior-than-odoo-odooer.html


# Revisiones

Integrating ERPNext With WooCommerce
https://erpnext.com/blog/ERPNext%20Features/integrating-erpnext-with-woocommerce