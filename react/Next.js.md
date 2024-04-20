
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

Pages are associated with a route based on their file name. For example, in development:

- pages/index.js is associated with the / route.
- pages/posts/first-post.js is associated with the /posts/first-post route.
	

# Link Component

When linking between pages on websites, you use the <a> HTML tag.

The Link component enables client-side navigation between two pages in the same Next.js app.




In Next.js, you use the Link Component from next/link to wrap the <a> tag. <Link> allows you to do client-side navigation to a different page in the application.

```
  <Link href="/posts/first-post">
    <a>this page!</a>
  </Link>
```

As you can see, the Link component is similar to using <a> tags, but instead of <a href="…">, you use <Link href="…"> and put an <a> tag inside.

Client-side navigation means that the page transition happens using JavaScript, which is faster than the default navigation done by the browser.

# Code splitting and prefetching

Next.js does code splitting automatically, so each page only loads what’s necessary for that page. That means when the homepage is rendered, the code for other pages is not served initially.

Furthermore, in a production build of Next.js, whenever Link components appear in the browser’s viewport, Next.js automatically prefetches the code for the linked page in the background.

Next.js automatically optimizes your application for the best performance by code splitting, client-side navigation, and prefetching (in production).

# Pre-rendering

By default, Next.js pre-renders every page. This means that Next.js generates HTML for each page in advance, instead of having it all done by client-side JavaScript. Pre-rendering can result in better performance and SEO.

Next.js has two forms of pre-rendering
- Static Generation (Recommended): The HTML is generated at build time and will be reused on each request.
- Server-side Rendering: The HTML is generated on each request.


https://nextjs.org/docs/basic-features/pages#pre-rendering

# Pre-rendering and Data Fetching

https://nextjs.org/learn/basics/data-fetching/pre-rendering

# Authentication

NextAuth.js. Authentication for Next.js
https://next-auth.js.org/

# Deploy

Vercel is the fastest way to deploy your Next.js application with zero configuration.




# Training

Next.js 13 for Beginners
https://github.com/gitdagray/next-js-course

Curso de Next.js 13 desde cero
https://www.youtube.com/watch?v=tA-_vAz9y78


Start building with Next.js
- For this course, we'll be building a simplified version of the financial dashboard that has:
- A public home page.
- A login page.
- Dashboard pages that are protected by authentication.
- The ability for users to add, edit, and delete invoices.
https://nextjs.org/learn


# Referencias

When you need HTTPS on LOCAL environment, local-ssl-proxy is the best solution
- use local-ssl-proxy. using local-ssl-proxy like a reverse proxy server
- Installing scoop. On Windows, it is very easy that installing mkcert using scoop
- Installing mkcert. Mkcert is the best solution for creating certificates on Windows. It is easier than installing OpenSSL and others
- Ejecutar el proxy ssl puerto origen, a un puerto destiono con certificado ssl
```
npx local-ssl-proxy --key localhost-key.pem --cert localhost.pem --source 3001 --target 3000
```

In this example, an application is running on port 3000. Run local-ssl-proxy on port 3001 targeting for port 3000.


```
npx local-ssl-proxy --source 3001 --target 3000
```

https://dev.to/cress/when-you-need-https-on-local-environment-local-ssl-proxy-is-the-best-solution-24n6


## Starter Kit. (Plantillas base)

The Open Source Next.js SaaS boilerplate for Enterprise SaaS app development.

- Next.js
- Tailwind CSS
- Postgres
- React
- Prisma
- TypeScript
- SAML Jackson (Provides SAML SSO, Directory Sync)
- Svix (Provides Webhook Orchestration)
- Retraced (Provides Audit Logs Service)
https://github.com/boxyhq/saas-starter-kit

https://dev.to/cress/when-you-need-https-on-local-environment-local-ssl-proxy-is-the-best-solution-24n6

## Applications


An open-source link management tool for modern marketing teams to create, share, and track short links.

- Next.js – framework
- TypeScript – language
- Tailwind – CSS
- Upstash – redis
- Tinybird – analytics
- PlanetScale – database
- NextAuth.js – auth
- BoxyHQ – SSO/SAML
- Turborepo – monorepo
- Stripe – payments
- Resend – emails
- Vercel – deployments
https://github.com/dubinc/dub