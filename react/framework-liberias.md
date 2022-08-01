# react-admin

- react-admin.md


# Next.js

Next.js es un pequeño framework construido sobre React.js que viene a ayudar a reducir esta fatiga. ¿Cómo? Next nos permite, instalando una sola dependencia, tener configurado todo lo que necesitamos para crear una aplicación de React usando Babel, Webpack, server render y muchas otras técnicas como HMR o separación de código y… ¡hasta hace más fácil hacer deploy de nuestras aplicaciones!

Server Side Rendering

- NextJS makes the development of large React application easier, since it provides many additional features
- pre-rendering, both static generation (SSG) and server-side rendering (SSR) are supported
- built-in CSS and Sass support, and support for any CSS-in-JS library
- development environment with Fast Refresh support

https://nextjs.org/learn/

## Pages in Next.js

In Next.js, a page is a React Component exported from a file in the pages directory.

## Link Component

When linking between pages on websites, you use the <a> HTML tag.

In Next.js, you use the Link Component from next/link to wrap the <a> tag. <Link> allows you to do client-side navigation to a different page in the application.

```
  <Link href="/posts/first-post">
    <a>this page!</a>
  </Link>
```

As you can see, the Link component is similar to using <a> tags, but instead of <a href="…">, you use <Link href="…"> and put an <a> tag inside.

## Pre-rendering

By default, Next.js pre-renders every page. This means that Next.js generates HTML for each page in advance, instead of having it all done by client-side JavaScript. Pre-rendering can result in better performance and SEO.

Next.js has two forms of pre-rendering
- Static Generation (Recommended): The HTML is generated at build time and will be reused on each request.
- Server-side Rendering: The HTML is generated on each request.


https://nextjs.org/docs/basic-features/pages#pre-rendering

## Pre-rendering and Data Fetching

https://nextjs.org/learn/basics/data-fetching/pre-rendering


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


# Create new project react typescript based
npx create-react-app project-name --template typescript
https://medium.com/codex/typescript-and-create-react-app-11bdebcbf763

Nota: El nombre no debe llevar puntos u otros caracteres especiales, en caso de requerir un nombre con esas condiciones renombrar después de crear

# Animaciones

## Framer Motion

A production-ready motion library for React. Utilize the power behind Framer, the best prototyping tool for teams. Proudly open source.

https://www.framer.com/motion/

react-transition-group
A set of components for managing component states (including mounting and unmounting) over time, specifically designed with animation in mind.
https://github.com/reactjs/react-transition-group
