# react-admin

- react-admin.md


# Next.js
 
[Next.js](Next.js.md)


# Gatsby.js

Gatsby data layer, which is a powerful feature of Gatsby that lets you easily build sites from Markdown, WordPress, headless CMSs, and other data sources of all flavors.

Gatsby is a React-based open-source framework for creating websites and apps. It's great whether you're building a portfolio site or blog, or a high-traffic e-commerce store or company homepage.


The Gatsby ecosystem includes built-in connectors to several database systems. These include:

-    PostgreSQL
-    MySQL
-    Amazon Redshift, SQLite3, Oracle and MSSQL
-    MongoDB
-    Firebase/Firestore


# Form Helper

[Forms](form.md)



# Validations

Yup is a JavaScript schema builder for value parsing and validation. Define a schema, transform a value to match, validate the shape of an existing value, or both. Yup schema are extremely expressive and allow modeling complex, interdependent validations, or value transformations.
https://github.com/jquense/yup


# Query / REST


## SWR

React Hooks library for data fetching

Therefore, for large-scale applications or projects that have to do with the distribution of data, SWR is preferred, while react-query is better for side projects or smaller applications.

Typescript
https://github.com/zeit/swr

https://swr.now.sh

## React Query

Hooks for fetching, caching and updating asynchronous data in React
Transport/protocol/backend agnostic data fetching (REST, GraphQL, promises, whatever!)

-  Backend agnostic

https://github.com/tannerlinsley/react-query
https://tanstack.com/query/v4


## Set custom port start
If you don't want to set the environment variable, another option is to modify the scripts part of package.json from:
"start": "react-scripts start"
to
Linux (tested on Ubuntu 14.04/16.04) and MacOS (tested by @aswin-s on MacOS Sierra 10.12.4):
"start": "PORT=3006 react-scripts start"
or (may be) more general solution by @IsaacPak
"start": "export PORT=3006 react-scripts start"
Windows @JacobEnsor solution
"start": "set PORT=3006 && react-scripts start"
https://stackoverflow.com/questions/40714583/how-to-specify-a-port-to-run-a-create-react-app-based-project




React-Toastify

React notification made easy 🚀 ! 
https://github.com/fkhadra/react-toastify


# Animaciones

## Framer Motion

A production-ready motion library for React. Utilize the power behind Framer, the best prototyping tool for teams. Proudly open source.

https://www.framer.com/motion/

react-transition-group
A set of components for managing component states (including mounting and unmounting) over time, specifically designed with animation in mind.
https://github.com/reactjs/react-transition-group

react-spring
A spring physics based React animation library
https://github.com/pmndrs/react-spring

