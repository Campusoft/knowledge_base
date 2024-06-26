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


## Referencias

Understanding Doctype and its types in Frappe ERPnext
- 1. Single DocTypes
- 2. Child Table Doctypes
- 3. Submittable Doctypes
- 4. Tree Doctypes
- 
https://medium.com/@ogoigbe12/understanding-doctype-and-its-types-in-frappe-erpnext-cfe02bacd307

# App

The default app frappe is a frappe app which acts as the framework for all apps

A Frappe app is a python package that uses the Frappe framework. Frappe apps live in a directory called apps in the frappe-bench directory.

To use an app, it must be installed on a site. 

# Sites

Frappe is a multitenant platform and each tenant is called a site. A site has its own database. Sites exist in a directory called sites, assumed as the current working directory when running a bench command.


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


# Background Jobs

Queue 
There are 3 default queues that are configured with the framework: short, default, and long. Each queue has a default timeout as follows:

- short: 300 seconds
- default: 300 seconds
- long: 1500 seconds


# Labs




bench new-site library.test

bench use library.test



User: Administrator
Password: 123


# Manuales

Building An eBook Store on Frappe: Part 1, Building The Storefront
- Building An eBook Store on Frappe: Part 2, Payment & Delivery
- Building An eBook Store on Frappe: Part 3, Interactivity & Deploy
https://frappe.io/blog/engineering/building-an-online-ebook-store-with-frappe-bulma-razorpay-part-I


# install