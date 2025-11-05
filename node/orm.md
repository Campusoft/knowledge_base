# Sequelize

Sequelize es un ORM para Node.js que facilita el desarrollo de aplicaciones cuando se utiliza base de datos relacionales. 

Permite trabajar con diferentes bases de datos como: Postgres, MySql, MariaDB, MSSQL.

Soporta características sólidas de transacciones, relaciones entre tablas, mecanismos de migraciones y carga de datos

https://sequelize.org/

## revisiones


--------------------------
Soport Oracle:

Hello Everyone,

I am closing this PR. No one in current Sequelize team has any experience with Oracle. Nor we have enough resources to maintain this new dialect.

You can use TypeORM if you want to use Oracle in your projects.

Thanks for all the work you guys put into this.
https://github.com/sequelize/sequelize/pull/7123#issuecomment-620375692



# typeorm

TypeORM is an ORM that can run in NodeJS, Browser, Cordova, PhoneGap, Ionic, React Native, NativeScript, Expo, and Electron platforms and can be used with TypeScript and JavaScript (ES5, ES6, ES7, ES8). Its goal is to always support the latest JavaScript features and provide additional features that help you to develop any kind of application that uses databases - from small applications with a few tables to large scale enterprise applications with multiple databases.

TypeORM supports both Active Record and Data Mapper patterns, unlike all other JavaScript ORMs currently in existence, which means you can write high quality, loosely coupled, scalable, maintainable applications the most productive way.

TypeORM is highly influenced by other ORMs, such as Hibernate, Doctrine and Entity Framework.

https://typeorm.io/


# Prisma

Prisma es un ORM moderno (Object–Relational Mapping) para Node.js y TypeScript, diseñado para simplificar el acceso y la gestión de bases de datos relacionales (como PostgreSQL, MySQL, SQLite, SQL Server, CockroachDB, MongoDB, etc.).
Se destaca por su tipado fuerte, rendimiento, y productividad para los desarrolladores modernos de JavaScript/TypeScript.


 
Característica | Prisma | Sequelize | TypeORM | Knex
-- | -- | -- | -- | --
Tipado TypeScript | ✅ Excelente | ⚠️ Limitado | ✅ Bueno | ❌ Manual
Migraciones | ✅ Automáticas | ⚠️ Manuales | ✅ | ❌
Performance | ✅ Alto | ⚠️ Medio | ⚠️ Medio | ✅ Alto
Facilidad de uso | ✅ Muy alta | ⚠️ Media | ⚠️ Media | ⚠️ Media
Soporte MongoDB | ✅ (limitado) | ❌ | ❌ | ❌

 
 

**Integración con TypeScript y Node.js**

Prisma está escrito en TypeScript, y su cliente aprovecha el tipado estático al máximo.

Se integra fácilmente con frameworks backend como:

- Express
- Fastify
- NestJS
- Next.js (API Routes)
- Remix, SvelteKit, etc.


**Migrations automáticas y controladas**

Prisma incluye una herramienta de migraciones (prisma migrate) que:

- Genera archivos SQL a partir del esquema.
- Permite mantener sincronizado el modelo de datos con la base.
- Facilita ambientes dev, staging y prod.

```
npx prisma migrate dev --name init
```

**Transacciones y atomicidad**

Prisma maneja transacciones con prisma.$transaction():

```
await prisma.$transaction([
  prisma.user.create({ data: { name: 'Juan' } }),
  prisma.post.create({ data: { title: 'Nuevo post' } }),
]);
```


# Knex.js

Knex.js (se pronuncia "knex") es un constructor de consultas SQL (Query Builder) para Node.js que soporta múltiples sistemas de bases de datos como PostgreSQL, MySQL, SQLite, entre otros.



En términos simples:
Es una biblioteca que te permite escribir consultas de base de datos usando JavaScript en lugar de SQL puro, aunque también te permite usar SQL raw cuando lo necesites.


```
// En lugar de escribir SQL así:
// "SELECT * FROM users WHERE age > 18 AND country = 'Mexico'"

// Con Knex lo escribes en JavaScript:
knex('users')
  .select('*')
  .where('age', '>', 18)
  .andWhere('country', 'Mexico')
  
```
Ventajas de Usar Knex.js


1. Código más legible y mantenible
2. Protección contra SQL Injection
3. Multiplataforma
4. SQL Raw cuando lo necesites

