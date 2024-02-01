# Medusa


Medusa is an open-source headless commerce engine that enables developers to create amazing digital commerce experiences.  The Open Source Shopify Alternative

https://medusajs.com/


# Architecture overview

Headless Backend

This is the main component that holds all the logic and data of the store. Your admin dashboard and storefront interact with the backend to retrieve, create, and modify data through REST APIs.

Storefront
Your customers use the Storefront to view products and make orders. Medusa provides 2 storefronts, one built with Next.js and one with Gatsby. You are also free to create your own storefront using the Storefront REST APIs.
 
Medusa uses PostgreSQL as its database and you will need to install it on your computer to get going.

Redis
Redis is an open-source in memory data structure store which is used in Medusa to emit messages in the system and cache data. 
 
Medusa in Microservices Architectures

Medusa’s commerce modules can be used in isolation from the core package and within a larger ecosystem. For example, you can use Medusa’s Cart module within a blog to allow readers to buy merch.

Medusa's core package @medusajs/medusa is a Node.js backend built on top of Express. It combines all the Commerce Modules that Medusa provides. Commerce Modules are ecommerce features that can be used as building blocks in an ecommerce ecosystem. Product is an example of a Commerce Module.

Medusa Architecture Overview
In this document, you'll get an overview of Medusa's architecture to better understand how all resources and tools work together.
- The backend connects to a database, such as PostgreSQL, to store the ecommerce store’s data. The tables in that database are represented by Entities, built on top of Typeorm.
The retrieval, manipulation, and other utility methods related to that entity are created inside a Service. Services are TypeScript or JavaScript classes that, along with other resources, can be accessed throughout the Medusa backend through dependency injection.
- The backend does not have any tightly-coupled frontend. Instead, it exposes Endpoints which are REST APIs that frontends such as an admin or a storefront can use to communicate with the backend.
- Medusa also uses an Events Architecture to trigger and handle events. Events are triggered when a specific action occurs, such as when an order is placed. To manage this events system, Medusa connects to a service that implements a pub/sub model, such as Redis.
- You can create any of the resources in the backend’s architecture, such as entities, endpoints, services, and more, as part of your custom development without directly modifying the backend itself. The Medusa backend uses loaders to load the backend’s resources, as well as your custom resources and resources in Plugins.
https://docs.medusajs.com/development/fundamentals/architecture-overview


Base Entitiess
- metadata Attribute. Most entities in Medusa have a metadata attribute. This attribute is an object that can be used to store custom data related to that entity. In the database, this attribute is stored as a JSON Binary (JSONB) column. On retrieval, the attribute is parsed into an object.
https://docs.medusajs.com/development/entities/overview

# Products

Products are items that a business sells to customers. Each product can have options and variants. Options are the different available attributes of a product, and variants are the salable combinations of these options.

For example, a product can have a “Color” option with values blue and green. You can then create two product variants from these options: one using the option value blue, and the other using the value green. This is just a simple example, as you can have multiple options and have variants combine values from each of these options.

Products can be associated with categories, collections, types, and more. This allows merchants to better organize products either internally or for their customers.

**Product variants**

Product variants are the actual salable item in your store. Each variant is a combination of the different option values available on the product

**Customizing**

Customizing the Product Entity
- For those cases, you can use the metadata attribute. This is an object stored in the database as a JSONB  (Binary JSON) type in the database.

# Install

Prerequisites
Before you can install and use Medusa, you need the following tools installed on your machine:

- Node.js v16+
- Git
- PostgreSQL


Base de datos Postgres. (docker-compose.yml)

```
version: '3.8'
services:
  db:
    image: postgres:14.1-alpine
    restart: always
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
    ports:
      - '5432:5432'
    volumes: 
      - db:/var/lib/postgresql/data
volumes:
  db:
   # driver: local
```


Iniciar utilizando medusa CLI

```
medusa develop
```


**Utilizar supabase, como store***

```
npx local-ssl-proxy --key localhost-key.pem --cert localhost.pem --source 7002 --target 7001
```

https://docs.medusajs.com/create-medusa-app#example-connect-to-a-supabase-database




----------------------------------

ERR_SSL_PROTOCOL_ERROR not able to see https localhost pages in chrome browser

Go to chrome://net-internals in the Chrome and switch to the Domain Security Policy tab.

In the "Delete domain security policies" section at the bottom, write "localhost" in Domain field and press the "Delete" button.

Note, this is a temporary fix.

----------------------------------


# Referencias

Create an Ecommerce Storefront with Medusa, Strapi, and Remix
- With Medusa, you can perform ecommerce functionalities while using Strapi to control the content displayed on your store.
- To top it off, with Remix you can create awesome and fast UI. Remix is an open source react framework focused on web standards and modern web app UX.
https://medusajs.com/blog/ecommerce-storefront-medusa-strapi-remix/?utm_content=229218640&utm_medium=social&utm_source=twitter&hss_channel=tw-3832252517