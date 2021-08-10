# supabase

Backend as a Service (BaaS)

Supabase is an open source Firebase alternative. This is a bold title, because Firebase is intended as a complete solution, with various features like authentication, file storage, serverless functions, SDK, and much more.

Even though Firebase has tons of features, Supabase may be more useful because it uses open source technology. Supabase gives you the flexibility to host on your local machine, in a cloud service provider, or even as a Docker container. This means it’s restriction free, so there’s no vendor locking.

Supabase uses PostgreSQL under the hood for the database and listens to real-time changes through several tools that they build.


Supabase uses several tools with PostgreSQL to give real-time updates. They are as follows:

-    Realtime allows you to listen to events in PostgreSQL like inserts, updates, and deletes, and converts data to JSON format using WebSockets
-    Postgres-meta allows you to query PostgreSQL through a REST API
-    PostgREST turns the PostgreSQL database into a RESTful API
-    GoTrue manages users through an SWT API which generates SWT tokens
-    Kong is a cloud-native API gateway




# Storage

Storage is now available in Supabase.
Architecture Storage

- The storage server is built with Fastify and Typescript
- 
https://supabase.io/blog/2021/03/30/supabase-storage