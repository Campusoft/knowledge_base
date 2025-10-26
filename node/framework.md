# Node Framework

# Nest

Nest is a framework for building efficient, scalable Node.js server-side applications. It uses modern JavaScript, is built with TypeScript (preserves compatibility with pure JavaScript) and combines elements of OOP (Object Oriented Programming), FP (Functional Programming), and FRP (Functional Reactive Programming).

Under the hood, Nest makes use of Express, but also, provides compatibility with a wide range of other libraries, like e.g. Fastify, allowing for easy use of the myriad third-party plugins which are available.

A progressive Node.js framework for building efficient, scalable, and enterprise-grade server-side applications on top of TypeScript & JavaScript (ES6, ES7, ES8) 


Services


Dependency injection

Nest is built around the strong design pattern commonly known as Dependency injection. We recommend reading a great article about this concept in the official Angular documentation.


 
Característica | Descripción
-- | --
🧱 Arquitectura modular | Todo se organiza en módulos, lo que permite dividir la aplicación en piezas reutilizables y mantenibles.
🧭 Inyección de dependencias (DI) | Basado en un contenedor IoC (Inversion of Control), facilita el manejo de dependencias, muy similar a frameworks de backend enterprise (como Spring en Java o Angular en frontend).
🧰 Soporte nativo de TypeScript | Aunque también puedes usar JavaScript, Nest aprovecha todo el poder de TypeScript: tipos, decoradores, interfaces, etc.
⚙️ Encima de Express o Fastify | Puedes elegir qué motor HTTP usar. Fastify te da mayor rendimiento; Express tiene más compatibilidad con middleware existente.
🧩 Decoradores | Define controladores, servicios y módulos con decoradores (@Controller, @Injectable, @Module, @Get, @Post, etc.).
🧠 Pipes, Guards, Interceptors y Filters | Permiten manejar validación, seguridad, logging, errores y transformación de datos de forma estructurada y centralizada.
🧬 Soporte de GraphQL, WebSockets, Microservicios y gRPC | Nest integra fácilmente distintos transportes y protocolos.
🧪 Testing integrado | Compatible con Jest y herramientas modernas de testing.
📦 CLI oficial | nest new, nest generate, etc. para scaffolding rápido.

 
 
 
## Modules


## microservice 


https://docs.nestjs.com/microservices/basics


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

 

# Actionhero

Actionhero is a node.js API framework for both tcp sockets, web sockets, and http clients. The goal of Actionhero is to create an easy-to-use toolkit for making reusable & scalable APIs. Clients connected to an Actionhero server can consume the API, consume static content, and communicate with each other.

Unlike Express and Koa, but similar to Loopback, Actionhero is an API server framework. Instead of focusing on HTTP request handling Actionhero separates the transport mechanism (HTTP, Websocket, etc) from the API logic with the concept of Actions. Actions are discrete and synchronous units of logic that can be invoked using any transport (i.e. Servers). This separation helps us to accomplish many things.


https://www.actionherojs.com