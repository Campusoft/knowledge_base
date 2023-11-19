# React Archicture

4 estructuras para organizar tu proyecto de React y React Native
- Estructura por tipo de fichero
  - components. (Componentes compartidos)
  - contexts
  - hooks
  - pages. (Las diferentes paginas, con sus componentes exclusivos que no se comparten con otras paginas)
  - routes
  - utils
  - services
- Estructura por funcionalidad o modular
  - La regla fundamental que se pretenden seguir es no importar código entre módulos
- Estructura basada en Atomic design
  - La idea principal es dividir nuestros componentes en cinco tipos de elementos:
  - Átomos, Moléculas, Organismos, Templates, Páginas
- Estructura basada en la arquitectura hexagonal
https://reboot.studio/blog/es/estructuras-organizar-proyecto-react/


Organizacion de Carpetas "Project Structure"

Most of the code lives in the `src` folder and looks like this:

```sh
src
|
+-- assets            # assets folder can contain all the static files such as images, fonts, etc.
|
+-- components        # shared components used across the entire application
|
+-- config            # all the global configuration, env variables etc. get exported from here and used in the app
|
+-- features          # feature based modules
|
+-- hooks             # shared hooks used across the entire application
|
+-- lib               # re-exporting different libraries preconfigured for the application
|
+-- providers         # all of the application providers
|
+-- routes            # routes configuration
|
+-- stores            # global state stores
|
+-- test              # test utilities and mock server
|
+-- types             # base types used across the application
|
+-- utils             # shared utility functions
```

In order to scale the application in the easiest and most maintainable way, keep most of the code inside the features folder, which should contain different feature-based things. Every feature folder should contain domain specific code for a given feature. This will allow you to keep functionalities scoped to a feature and not mix its declarations with shared things. This is much easier to maintain than a flat folder structure with many files.

```sh
src/features/awesome-feature
|
+-- api         # exported API request declarations and api hooks related to a specific feature
|
+-- assets      # assets folder can contain all the static files for a specific feature
|
+-- components  # components scoped to a specific feature
|
+-- hooks       # hooks scoped to a specific feature
|
+-- routes      # route components for a specific feature pages
|
+-- stores      # state stores for a specific feature
|
+-- types       # typescript types for TS specific feature domain
|
+-- utils       # utility functions for a specific feature
|
+-- index.ts    # entry point for the feature, it should serve as the public API of the given feature and exports everything that should be used outside the feature
```

Everything from a feature should be exported from the `index.ts` file which behaves as the public API of the feature.

You should import stuff from other features only by using:

`import {AwesomeComponent} from "@/features/awesome-feature"`

and not

`import {AwesomeComponent} from "@/features/awesome-feature/components/AwesomeComponent`

This was inspired by how NX handles libraries that are isolated but available to be used by the other modules. Think of a feature as a library or a module that is self-contained but can expose different parts to other features via its entry point.

# Autentificacion


Keycloak - A gentle introduction to Keycloak using Vite+React, NodeJS 
https://www.youtube.com/watch?v=5z6gy4WGnUs


# Meta-Frameworks


- Nextjs
- Gatsby
- Remix
- Astro
- Redwoodjs
- Blitzjs



# Application References


## Metasfresh


metasfresh is a responsive, Free and Open Source ERP System. Our aim is to create fast and easy-to-use enterprise software with an outstanding user experience.

- It has a 3-tier architecture with Rest-API and a Web User Frontend developed in HTML5/ ReactJS/ Redux.

https://github.com/metasfresh/metasfresh


## Kutt

Kutt is a modern URL shortener Open Source react project with support for custom domains. Shorten URLs, manage your links and view the click rate statistics.
Database:
- PostgreSQL
Back-end technologies:
- Node.js
- Next
- knex
https://github.com/thedevs-network/kutt

## Mattermost

Mattermost is an open source platform for secure collaboration across the entire software development lifecycle. This repo is the primary source for core development on the Mattermost platform; it's written in Golang and React, and runs as a single Linux binary with MySQL or PostgreSQL. 
- Architecture overview
  - Mattermost is a single-compiled Go binary that is exposed as a Restful JSON web server with Javascript and Go clients.
  - React, redux
  - https://docs.mattermost.com/getting-started/architecture-overview.html
  
https://github.com/mattermost/mattermost-webapp


Next generation iOS and Android apps for Mattermost in React Native 
https://github.com/mattermost/mattermost-mobile


## Bulletproof React 

A simple, scalable, and powerful architecture for building production ready React applications.
https://github.com/alan2207/bulletproof-react
 

## Cal.com (formerly Calendso)

The open-source Calendly alternative. 

- Next.js
- React
- Tailwind
- Prisma
https://github.com/calcom/cal.com

## AdminJS

State management
- redux
- react-redux

Styling
- styled-components
- styled-system

Other
- axios
- recharts
- flat
- @carbon/icons-react

An AdminJS application consists of:
- a core package
- a plugin (for a framework of your choice)
  - Express server
  - Nest server
  - Hapi server
  - Koa server
  - Fastify server
- an adapter for (for a ORM/ODM of your choice)
  - TypeORM
  - Sequelize
  - Mongoose
  - MikroORM
  - Objection

AdminJS is an admin panel for apps written in node.js 
https://github.com/SoftwareBrothers/adminjs
