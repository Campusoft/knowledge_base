# Frappe Books


Frappe Books is built on Vue.js and Electron. It is offline by default and uses a local SQLite file as the database.

https://frappebooks.com/


Expected version ">=16.13.1 <17". Got "18.14.2"


```
# start the electron app
yarn electron:serve
```

# Items

Item is term used by Frappe Books for anything that is purchased or sold using a Sales or Purchase Invoice.

All of the goods and services that are subject to the GST in India are categorized using an HSN code (Harmonized System of Nomenclature) and a SAC (Service Accounting Code), which helps in removing any obstacles to international trade. These HSN and SAC codes have also been applied to invoices and record-keeping, which makes it simple to identify the products and services provided. In this article, we will examine the difference between the HSN and SAC codes in GST.

# Architecture

## Fyo

This is the underlying framework that runs Books, at some point it may be removed into a separate repo, but as of now it's in gestation.

The reason for maintaining a framework is to allow for varied backends. Currently Books runs on the electron renderer process and all db stuff happens on the electron main process which has access to nodelibs. As the development of Fyo progresses it will allow for a browser frontend and a node server backend.

This platform variablity will be handled by code in the fyo/demux subdirectory.


## Folder

Main purpose of this is to describe the shape of the models' table in the database.

https://github.com/frappe/books/tree/master/schemas


# FrappeJS


He started by building a new framework called FrappeJS, which was the Javascript version of our Frappe Framework. 
