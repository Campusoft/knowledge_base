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

Medusa v2 se puede instalar localmente con `create-medusa-app`, pero para desarrollo es mas comodo levantar PostgreSQL y Redis con Docker. Si se quiere evitar instalar Node/PostgreSQL/Redis en la maquina, tambien se puede levantar todo el stack con Docker Compose.

## Opcion A: stack completo con Docker

Prerequisitos:

- Docker
- Docker Compose
- Git

Clonar el starter oficial:

```bash
git clone https://github.com/medusajs/dtc-starter.git --depth=1 my-medusa-store
cd my-medusa-store
```

Crear `docker-compose.yml` en la raiz del proyecto:

```yaml
services:
  postgres:
    image: postgres:15-alpine
    container_name: medusa_postgres
    restart: unless-stopped
    environment:
      POSTGRES_DB: medusa-store
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    networks:
      - medusa_network

  redis:
    image: redis:7-alpine
    container_name: medusa_redis
    restart: unless-stopped
    ports:
      - "6379:6379"
    networks:
      - medusa_network

  medusa:
    build: .
    container_name: medusa_backend
    restart: unless-stopped
    depends_on:
      - postgres
      - redis
    ports:
      - "9000:9000"
      - "5173:5173"
    environment:
      NODE_ENV: development
      DATABASE_URL: postgres://postgres:postgres@postgres:5432/medusa-store
      REDIS_URL: redis://redis:6379
    env_file:
      - apps/backend/.env
    volumes:
      - .:/server
      - /server/node_modules
      - /server/apps/backend/node_modules
    networks:
      - medusa_network

  storefront:
    build: .
    container_name: medusa_storefront
    restart: unless-stopped
    depends_on:
      - medusa
    ports:
      - "8000:8000"
    environment:
      NEXT_PUBLIC_MEDUSA_BACKEND_URL: http://medusa:9000
    env_file:
      - apps/storefront/.env
    volumes:
      - .:/server
      - /server/node_modules
      - /server/apps/backend/node_modules
      - /server/apps/storefront/node_modules
      - /server/apps/storefront/.next
    entrypoint: ["./start-storefront.sh"]
    networks:
      - medusa_network

volumes:
  postgres_data:

networks:
  medusa_network:
    driver: bridge
```

Crear `.env` desde las plantillas:

```bash
cp apps/backend/.env.template apps/backend/.env
cp apps/storefront/.env.template apps/storefront/.env
```

Crear `start.sh` en la raiz del proyecto:

```sh
#!/bin/sh
set -e

cd apps/backend
npx medusa db:migrate
npm run dev
```

Crear `start-storefront.sh` en la raiz del proyecto:

```sh
#!/bin/sh
set -e

cd apps/storefront
npm run dev -- --hostname 0.0.0.0 --port 8000
```

Si el proyecto usa pnpm, cambiar `npm run dev` por `pnpm dev` en ambos scripts.

Crear `Dockerfile` en la raiz del proyecto:

```dockerfile
FROM node:20-alpine

WORKDIR /server

RUN corepack enable

COPY package.json ./
COPY pnpm-lock.yaml* ./
COPY pnpm-workspace.yaml* ./
COPY apps/backend/package.json apps/backend/package.json
COPY apps/storefront/package.json apps/storefront/package.json

RUN if [ -f pnpm-lock.yaml ]; then pnpm install --frozen-lockfile; else npm install; fi

COPY . .

EXPOSE 9000 5173 8000

ENTRYPOINT ["./start.sh"]
```

Crear `.dockerignore`:

```gitignore
node_modules
apps/backend/node_modules
apps/storefront/node_modules
apps/backend/.medusa
apps/storefront/.next
.git
coverage
dist
build
*.log
```

En macOS/Linux, dar permisos de ejecucion a los scripts:

```bash
chmod +x start.sh start-storefront.sh
```

En Windows, asegurar que `start.sh` y `start-storefront.sh` usen saltos de linea LF.

En `apps/backend/medusa-config.ts`, desactivar SSL para PostgreSQL local en Docker y configurar Redis:

```ts
module.exports = defineConfig({
  projectConfig: {
    // ...
    databaseDriverOptions: {
      ssl: false,
      sslmode: "disable",
    },
    redisUrl: process.env.REDIS_URL,
  },
})
```

Levantar los servicios:

```bash
docker compose up --build -d
docker compose logs -f medusa
```

URLs principales:

- Backend: `http://localhost:9000`
- Admin: `http://localhost:9000/app`
- Storefront: `http://localhost:8000`
- PostgreSQL: `localhost:5432`
- Redis: `localhost:6379`

Crear usuario admin:

```bash
docker compose exec medusa npx medusa user -e admin@example.com -p supersecret
```

Detener servicios:

```bash
docker compose down
```

Eliminar tambien la base de datos local:

```bash
docker compose down -v
```

## Opcion B: solo PostgreSQL y Redis con Docker

Usar esta opcion si quieres correr Medusa directamente con Node.js en tu maquina, pero dejar la infraestructura en Docker.

Prerequisitos:

- Node.js v20+ LTS
- Git
- Docker
- Docker Compose

Crear `docker-compose.yml`:

```yaml
services:
  postgres:
    image: postgres:15-alpine
    container_name: medusa_postgres
    restart: unless-stopped
    environment:
      POSTGRES_DB: medusa-store
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  redis:
    image: redis:7-alpine
    container_name: medusa_redis
    restart: unless-stopped
    ports:
      - "6379:6379"

volumes:
  postgres_data:
```

Levantar infraestructura:

```bash
docker compose up -d
```

Crear el proyecto:

```bash
npx create-medusa-app@latest my-medusa-store
cd my-medusa-store/apps/backend
```

Variables utiles para `apps/backend/.env`:

```env
DATABASE_URL=postgres://postgres:postgres@localhost:5432/medusa-store
REDIS_URL=redis://localhost:6379
```

Iniciar Medusa en desarrollo:

```bash
npm run dev
```

o, si el proyecto usa pnpm:

```bash
pnpm dev
```

## Problemas comunes

- Si Docker ya tiene servicios usando `5432`, `6379`, `9000`, `5173` o `8000`, cambiar el puerto del lado izquierdo. Ejemplo: `"9001:9000"`.
- Si hay varios proyectos Medusa en la misma maquina, cambiar `container_name`, nombre del volumen y nombre de la red para evitar conflictos.
- En Windows, los scripts `.sh` usados dentro del contenedor deben tener saltos de linea LF, no CRLF.
- Si `http://localhost:9000` muestra `Cannot GET /`, no necesariamente es error. Probar `http://localhost:9000/app` o `http://localhost:9000/health`.
- Para revisar logs: `docker compose logs -f medusa`.

Docs:

- https://docs.medusajs.com/learn/installation
- https://docs.medusajs.com/learn/installation/docker


# Referencias

Create an Ecommerce Storefront with Medusa, Strapi, and Remix
- With Medusa, you can perform ecommerce functionalities while using Strapi to control the content displayed on your store.
- To top it off, with Remix you can create awesome and fast UI. Remix is an open source react framework focused on web standards and modern web app UX.
https://medusajs.com/blog/ecommerce-storefront-medusa-strapi-remix/?utm_content=229218640&utm_medium=social&utm_source=twitter&hss_channel=tw-3832252517
