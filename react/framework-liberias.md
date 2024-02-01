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

# Remix 

Remix es un framework de React muy completo con enrutamiento anidado. Te permite dividir tu aplicación en partes anidadas que pueden cargar datos en paralelo y actualizarse en respuesta a las acciones del usuario. Para crear un nuevo proyecto Remix, ejecuta:

Remix es mantenido por Shopify. Cuando creas un proyecto Remix, debes elegir su destino de implementación. Puedes implementar una aplicación Remix en cualquier ambiente Node.js, alojamiento sin servidor usando o escribiendo un adaptador.


https://remix.run/

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


Beginner's Guide to React Query

- Performing basic data fetching
- Mutating Data
- used Jsonplaceholder as our API endpoint and Axios 
https://refine.dev/blog/react-query-guide/



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

# refine 

refine is a React-based framework for the rapid development of web applications. It eliminates the repetitive tasks demanded by CRUD operations and provides industry standard solutions for critical parts like authentication, access control, routing, networking, state management, and i18n.
https://refine.dev


Architecture	
- Hooks Based 
- State Management	React Query

refine is licensed under the MIT Licence. It only requires the preservation of copyright and license notices. Licensed works, modifications, and larger works may be distributed under different terms and without source code.

Instead of being limited to a set of pre-styled components, refine provides collections of helper hooks, components and providers and more. Since business logic and UI are completely decoupled, you can customize UI without constraints.

It means, refine just works seamlessly with any custom designs or UI frameworks. Thanks to it's headless architecture, you can use popular CSS frameworks like TailwindCSS or even create your own styles from scratch.

refine also provides integrations with Ant Design, Material UI, Mantine, and Chakra UIto get you started quickly. These libraries are set of components which are nicely integrated with headless @refinedev/core package.

refine also provides integrations with Ant Design, Material UI, Mantine, and Chakra UIto get you started quickly.

## Concepts

refine core is fully independent of UI, meaning that you can use core components and hooks without any UI dependency. All of the data-related hooks, such as useTable, useForm, useList, of refine can also be given some common properties like resource, meta, queryOptions etc. that are independent of UI.

**resource**

resource is a prop that gets passed to dataProvider as a paremeter by refine. It is usually used as an API endpoint path but it all depends on how you handle it in your dataProvider

**meta**

meta is a special property that can be used to pass additional information to data provider methods for the following purposes:

## Examples


This is a complete eCommerce storefront app built with refine, Medusa.js, Next.js, and Stripe.

https://github.com/refinedev/refine/tree/master/examples/store

# AdminJS 

Open-Source Admin Panel for your Node.js Application

The out-of-the-box version of AdminJS is pretty powerful, but its in-depth customizability is where it really shines. With a basic knowledge of React and Node.js, you can change nearly every behaviour of your admin panel.

https://adminjs.co/index.html