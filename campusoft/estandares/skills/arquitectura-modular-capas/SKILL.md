---
name: arquitectura-modular-capas
description: Diseña, revisa o amplía un backend .NET modular por capas (Domain, Application, Infrastructure, HttpApi) con un único host. Úsala al crear un módulo, agregar un caso de uso, definir dependencias entre proyectos o integrar módulos entre sí. Stack de referencia: .NET 8, ASP.NET Core, EF Core, PostgreSQL.
---

# Arquitectura modular por capas (backend .NET)

Guía independiente del proyecto y de la herramienta de IA. Las políticas propias del proyecto (`AGENTS.md`, documentos de arquitectura) prevalecen sobre esta guía.

## Cuándo usar

- Crear un módulo nuevo o un caso de uso.
- Decidir en qué proyecto va una clase.
- Revisar dependencias entre proyectos o entre módulos.
- Registrar un módulo en el host.

## Antes de empezar

1. Leer la arquitectura vigente del proyecto y sus convenciones de desarrollo. No usar documentos en borrador o históricos como fuente.
2. Revisar un módulo existente similar y seguir su estructura antes de inventar una.
3. No asumir que algo planificado ya está implementado.

## Estructura de un módulo

Cada módulo es un conjunto de proyectos con un prefijo común (`<Empresa>.<Modulo>.*`):

| Proyecto | Responsabilidad | No debe contener |
| --- | --- | --- |
| `Domain` | Entidades, objetos de valor, invariantes y reglas del módulo. | Referencias a EF Core, ASP.NET Core ni a otros módulos. |
| `Application` | Casos de uso, DTO, validaciones, puertos (interfaces) y contratos públicos del módulo. | Acceso directo a `DbContext` ni detalles HTTP. |
| `Infrastructure` | `DbContext`, configuraciones EF, migraciones, repositorios y adaptadores que implementan los puertos. | Reglas de negocio. |
| `HttpApi` | Endpoints, mapeo HTTP y registro de rutas del módulo. Es una biblioteca, no un ejecutable. | Reglas de negocio ni acceso a datos. |
| `Test` | Pruebas del módulo (dominio, aplicación, infraestructura, permisos). | |

Opcional: un proyecto `Contracts` cuando otros módulos necesitan consumir tipos públicos sin referenciar `Application`.

## Dirección de dependencias

```text
HttpApi -> Application -> Domain
HttpApi -> Infrastructure -> Application -> Domain
```

- `Domain` no depende de nadie.
- `Application` depende solo de `Domain` y de abstracciones compartidas.
- `Infrastructure` implementa los puertos de `Application`.
- Una dependencia en sentido contrario es un defecto de diseño.

## Host único y composición

- Un solo proyecto ejecutable (`<Empresa>.<Sistema>.HttpApi`) compone los módulos: configuración, autenticación, autorización, middleware, documentación de API y health checks.
- Cada módulo expone un método de registro (por ejemplo, `AddXxxModule` y `MapXxxEndpoints`) que el host invoca. El host no contiene reglas de negocio.
- El arranque normal no aplica migraciones. Se ejecutan con un comando explícito.

## Comunicación entre módulos

- Entre módulos solo se usan contratos públicos de `Application` o `Contracts`. Nunca se usa el `DbContext`, las entidades ni las clases de `Infrastructure` de otro módulo.
- Las referencias a datos de otro módulo se guardan como identificadores opacos, sin clave foránea en base de datos. La integridad se valida en la capa de aplicación.
- Cada dato tiene un único módulo propietario. Los demás lo consultan por contrato, no lo copian.
- Evitar dependencias circulares entre módulos. Si aparecen, extraer un contrato o usar un evento.

## Persistencia

- Un `DbContext` por módulo, con su propia tabla de historial de migraciones (por ejemplo `__EFMigrationsHistory_<Modulo>`).
- Migraciones generadas y aplicadas por contexto, revisadas antes de aplicarse y sin cambios destructivos sin respaldo.
- Configuración de entidades en clases `IEntityTypeConfiguration`, no con atributos en el dominio.
- Tenant, auditoría y reloj se resuelven mediante abstracciones compartidas. Nunca se recibe el identificador de tenant desde el cliente para operaciones de negocio.

## API HTTP

- Rutas versionadas (`/api/v1/...`).
- Validar la entrada en el borde y devolver errores consistentes (ProblemDetails).
- La autorización se basa en permisos y políticas declarados por el módulo. No se hace dentro del endpoint con condiciones ad hoc.
- La hora de negocio se obtiene de una abstracción de reloj, no de `DateTime.Now`.

## Pasos para agregar un módulo

1. Confirmar con el análisis o requisito vigente que el módulo y su alcance están aprobados.
2. Crear los proyectos `Domain`, `Application`, `Infrastructure`, `HttpApi` y `Test` con las referencias de la tabla.
3. Definir las entidades y sus invariantes en `Domain`.
4. Definir los puertos y casos de uso en `Application`, y los contratos públicos que otros módulos consumirán.
5. Crear el `DbContext`, las configuraciones y la migración inicial en `Infrastructure`.
6. Exponer los endpoints y registrar permisos en `HttpApi`.
7. Registrar el módulo en el host y añadir una prueba de composición.
8. Documentar los permisos, contratos y decisiones pendientes.

## Lista de verificación

- [ ] `Domain` no referencia EF Core ni ASP.NET Core.
- [ ] No hay referencias a `Infrastructure` ni al `DbContext` de otro módulo.
- [ ] No hay claves foráneas entre módulos.
- [ ] Hay un solo host ejecutable.
- [ ] Cada contexto tiene su historial de migraciones.
- [ ] Los endpoints no contienen reglas de negocio.
- [ ] Hay pruebas del dominio, de los casos de uso y de los permisos.
- [ ] No hay secretos ni cadenas de conexión en el código.

## Errores frecuentes

- Poner lógica en el endpoint o en el repositorio.
- Devolver entidades de dominio directamente en la API.
- Compartir un `DbContext` único entre módulos "por comodidad".
- Crear un módulo `Common` o `Shared` que acumula reglas de negocio.
- Aplicar migraciones automáticamente al iniciar la aplicación en producción.
