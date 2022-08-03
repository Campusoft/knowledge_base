
# Next.js

Next.js es un pequeño framework construido sobre React.js que viene a ayudar a reducir esta fatiga. ¿Cómo? Next nos permite, instalando una sola dependencia, tener configurado todo lo que necesitamos para crear una aplicación de React usando Babel, Webpack, server render y muchas otras técnicas como HMR o separación de código y… ¡hasta hace más fácil hacer deploy de nuestras aplicaciones!

Server Side Rendering

- NextJS makes the development of large React application easier, since it provides many additional features
- pre-rendering, both static generation (SSG) and server-side rendering (SSR) are supported
- built-in CSS and Sass support, and support for any CSS-in-JS library
- development environment with Fast Refresh support

https://nextjs.org/learn/

# Pages in Next.js

In Next.js, a page is a React Component exported from a file in the pages directory.

# Link Component

When linking between pages on websites, you use the <a> HTML tag.

In Next.js, you use the Link Component from next/link to wrap the <a> tag. <Link> allows you to do client-side navigation to a different page in the application.

```
  <Link href="/posts/first-post">
    <a>this page!</a>
  </Link>
```

As you can see, the Link component is similar to using <a> tags, but instead of <a href="…">, you use <Link href="…"> and put an <a> tag inside.

# Pre-rendering

By default, Next.js pre-renders every page. This means that Next.js generates HTML for each page in advance, instead of having it all done by client-side JavaScript. Pre-rendering can result in better performance and SEO.

Next.js has two forms of pre-rendering
- Static Generation (Recommended): The HTML is generated at build time and will be reused on each request.
- Server-side Rendering: The HTML is generated on each request.


https://nextjs.org/docs/basic-features/pages#pre-rendering

# Pre-rendering and Data Fetching

https://nextjs.org/learn/basics/data-fetching/pre-rendering

