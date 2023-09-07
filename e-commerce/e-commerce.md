# e-commerce process



The e-commerce process in 5 steps - and how to automate it
https://www.luhhu.com/blog/ecommerce-process-how-to-automate


# Medusa


Medusa is an open-source headless commerce engine that enables developers to create amazing digital commerce experiences.  The Open Source Shopify Alternative

https://medusajs.com/


## Architecture overview

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
https://docs.medusajs.com/development/fundamentals/architecture-overview

## Products

Products are items that a business sells to customers. Each product can have options and variants. Options are the different available attributes of a product, and variants are the salable combinations of these options.

For example, a product can have a “Color” option with values blue and green. You can then create two product variants from these options: one using the option value blue, and the other using the value green. This is just a simple example, as you can have multiple options and have variants combine values from each of these options.

Products can be associated with categories, collections, types, and more. This allows merchants to better organize products either internally or for their customers.

**Product variants**

Product variants are the actual salable item in your store. Each variant is a combination of the different option values available on the product

**Customizing**

Customizing the Product Entity
- For those cases, you can use the metadata attribute. This is an object stored in the database as a JSONB type in the database.

## install




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


## Referencias

Create an Ecommerce Storefront with Medusa, Strapi, and Remix
- With Medusa, you can perform ecommerce functionalities while using Strapi to control the content displayed on your store.
- To top it off, with Remix you can create awesome and fast UI. Remix is an open source react framework focused on web standards and modern web app UX.
https://medusajs.com/blog/ecommerce-storefront-medusa-strapi-remix/?utm_content=229218640&utm_medium=social&utm_source=twitter&hss_channel=tw-3832252517