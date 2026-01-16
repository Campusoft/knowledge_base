# CRM Twenty

# Twenty CRM - Tabla Resumen de Tecnologías Clave

| Capa | Tecnología | Propósito | Características |
| :--- | :--- | :--- | :--- |
| **Frontend** | React + Next.js | UI interactiva y componentizada | SSR/SSG, enrutamiento mejorado, alta performance |
| | Apollo Client | Gestión de estado y datos | Cache inteligente, queries GraphQL, estado local |
| | Twenty Design System | Librería de componentes | UI consistente y experiencia de usuario unificada |
| **Backend** | NestJS | Framework de aplicación servidor | Arquitectura modular, TypeScript, escalable |
| | GraphQL + Apollo Server | API layer | Endpoint único, flexibilidad en queries, strongly typed |
| | TypeORM | Mapeo objeto-relacional (ORM) | Consultas type-safe, múltiples motores de BD |
| | Motor de Metadatos | Núcleo de personalización | Define objetos, campos y relaciones dinámicamente |
| **Base de Datos** | PostgreSQL (default) | Almacenamiento persistente | ACID, consultas complejas, estructura relacional |
| **Arquitectura** | Pila de 3 capas | Separación de responsabilidades | Frontend-Backend-BD con GraphQL como conector |
| | Basada en metadatos | Personalización sin código | Configuración dinámica de esquemas y UI |


Twenty uses Recoil for centralized state management with atomic state updates and component-scoped state.

GraphQL API Integration
The frontend integrates with the backend through Apollo Client with automatic code generation and type safety, using a dual-schema approach for metadata and core operations.


Database Systems
- PostgreSQL as the primary relational database
- ClickHouse for analytics and time-series data
- Redis for caching, sessions, and job queues


# Características Arquitectónicas Destacadas

- **API GraphQL**: Un solo endpoint, evita over/under fetching
- **Sistema de metadatos**: Personalización en tiempo real
- **Full TypeScript**: Type-safe en frontend y backend
- **Código abierto**: Transparencia y capacidad de extensión
- **UI dinámica**: Se renderiza basado en metadatos
- **Base de datos relacional**: Estructura sólida con PostgreSQL