#  Frappe Framework.

Frappe, pronounced fra-pay, is a full stack, batteries-included, web framework written in Python and Javascript with MariaDB as the database. It is the framework which powers ERPNext, is pretty generic and can be used to build database driven apps.


- Python
- MariaDB / Postgres 
- JavaScript / jQuery
- Jinja Templating

# DocTypes

A DocType is the core building block of any application based on the Frappe Framework.

It describes the Model and the View of your data. It contains what fields are stored for your data, and how they behave with each other. It contains information about how your data is named

Field Types
https://docs.erpnext.com/docs/v13/user/manual/en/customize-erpnext/articles/field-types

Section Break
- Section Break is used to divide the form into multiple sections.
- Ej. Referencia. En bootstrap, seria un row una section.

# REST API 

Frappe framework generates REST API for all of your DocTypes out of the box. You can also run arbitrary python methods using their dotted module path.
https://frappeframework.com/docs/v14/user/en/api/rest


Frappe ships with an HTTP API that can be classified into Remote Procedure Calls (RPC), to call whitelisted methods and Representational State Transfer (REST), to manipulate resources.
- All documents in Frappe are available via a RESTful API with prefix /api/resource/. You can perform all CRUD operations on them:
https://frappeframework.com/docs/v14/user/en/guides/integration/rest_api


Token Based Authentication 
- There are two types of authorization: token and Basic
- Access Token
https://frappeframework.com/docs/v14/user/en/guides/integration/rest_api/token_based_authentication

#  Bench

Bench is a CLI tool to manage Frappe Deployments. It provides an easy interface to help you setup and manage multiple sites and apps based on Frappe Framework.


# Apps

A Frappe app is a python package that uses the Frappe framework. Frappe apps live in a directory called apps in the frappe-bench directory.


Installing an app into a site

To use an app, it must be installed on a site. Installing an app on a site means creating the models that are bundled with the app into the site, which means creating database tables in the site database.


To install an app onto a site, run the following command:

```
$ bench --site site_name install-app custom_app

Installing custom_app...
```

# Database


Postgres Database Setup

Use following command to use Postgres as database for your new site.

bench new-site <site-name> --db-type postgres

Note: Make sure you have Postgres version 9 or greater installed in your system.




# Labs




bench new-site library.test

bench use library.test



User: Administrator
Password: 123