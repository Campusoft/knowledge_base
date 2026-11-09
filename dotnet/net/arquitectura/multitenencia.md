# Multitenencia en .NET

## Multi Tenant


**Opción Simple: Shared Database, Shared Schema (Base de Datos y Esquema Compartidos)**


Concepto: Se añade una columna de TenantId a casi todas las tablas que contienen datos de tenant.

.NET C#	Fácil de implementar con Entity Framework Core y filtros de consulta globales (Query Filters).

{Request} ->{Middleware (Identifica TenantId)} ->{Servicio Scoped} ->{DbContext/EF Core (Aplica Aislamiento)}$$


**Opción Media: Shared Database, Separate Schemas (Base de Datos Compartida, Esquemas Separados)**

Concepto: El aislamiento se logra a nivel de esquema de base de datos.


.NET C#	Requiere una lógica de conexión/mapeo dinámico en EF Core para apuntar al esquema correcto.


**Opción Compleja: Separate Databases (Bases de Datos Separadas)**

Concepto: La aplicación sabe a qué base de datos conectarse basándose en el TenantId (generalmente extraído del hostname o token de autenticación).


.NET C#	Requiere un patrón de fábrica de DbContext o inyección dinámica de dependencias para el connection string correcto.


 
Tipo | Tamaño | Seguridad | Fácil de recordar | Recomendado para
-- | -- | -- | -- | --
int | 4 bytes | baja | sí | multiempresa pequeña
long | 8 bytes | media | sí | más de 2B tenants
Guid | 16 bytes | alta | no | SaaS multi-tenant moderno
string | variable | alta | depende | tenants por dominio/subdominio

 
 
Aumenta tamaño de índices. Tipo de dato para el tenantId

- Guid = 16 bytes
- int = 4 bytes
- string 36 char = 36 bytes

Si tu volumen es gigante (millones de registros → por tenant), podría ser relevante.
