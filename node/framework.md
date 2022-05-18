# Node Framework

# Nest

Nest is a framework for building efficient, scalable Node.js server-side applications. It uses modern JavaScript, is built with TypeScript (preserves compatibility with pure JavaScript) and combines elements of OOP (Object Oriented Programming), FP (Functional Programming), and FRP (Functional Reactive Programming).

Under the hood, Nest makes use of Express, but also, provides compatibility with a wide range of other libraries, like e.g. Fastify, allowing for easy use of the myriad third-party plugins which are available.


A progressive Node.js framework for building efficient, scalable, and enterprise-grade server-side applications on top of TypeScript & JavaScript (ES6, ES7, ES8) 

# Fastify

Fast and low overhead web framework, for Node.js

Core features

These are the main features and principles on which fastify has been built:

-   Highly performant: as far as we know, Fastify is one of the fastest web frameworks in town, depending on the code complexity we can serve up to 30 thousand requests per second.
-   Extensible: Fastify is fully extensible via its hooks, plugins and decorators.
-   Schema based: even if it is not mandatory we recommend to use JSON Schema to validate your routes and serialize your outputs, internally Fastify compiles the schema in a highly performant function.
-   Logging: logs are extremely important but are costly; we chose the best logger to almost remove this cost, Pino!
-   Developer friendly: the framework is built to be very expressive and to help developers in their daily use, without sacrificing performance and security.
-   TypeScript ready: we work hard to maintain a TypeScript type declaration file so we can support the growing TypeScript community.
	
https://www.fastify.io/

# typeorm

TypeORM is an ORM that can run in NodeJS, Browser, Cordova, PhoneGap, Ionic, React Native, NativeScript, Expo, and Electron platforms and can be used with TypeScript and JavaScript (ES5, ES6, ES7, ES8). Its goal is to always support the latest JavaScript features and provide additional features that help you to develop any kind of application that uses databases - from small applications with a few tables to large scale enterprise applications with multiple databases.

TypeORM supports both Active Record and Data Mapper patterns, unlike all other JavaScript ORMs currently in existence, which means you can write high quality, loosely coupled, scalable, maintainable applications the most productive way.

TypeORM is highly influenced by other ORMs, such as Hibernate, Doctrine and Entity Framework.

https://typeorm.io/

# Actionhero

Actionhero is a node.js API framework for both tcp sockets, web sockets, and http clients. The goal of Actionhero is to create an easy-to-use toolkit for making reusable & scalable APIs. Clients connected to an Actionhero server can consume the API, consume static content, and communicate with each other.

Unlike Express and Koa, but similar to Loopback, Actionhero is an API server framework. Instead of focusing on HTTP request handling Actionhero separates the transport mechanism (HTTP, Websocket, etc) from the API logic with the concept of Actions. Actions are discrete and synchronous units of logic that can be invoked using any transport (i.e. Servers). This separation helps us to accomplish many things.


https://www.actionherojs.com