---
name: frontend-estructura-features
description: Organiza una aplicación web React por features (páginas, componentes, hooks, servicios y esquemas por funcionalidad) con rutas, datos remotos y formularios. Úsala al crear una feature, ubicar un archivo, definir rutas o revisar la estructura del frontend. Stack de referencia: React, Vite, Tailwind CSS, shadcn/ui, React Router, TanStack Query, React Hook Form y Zod.
---

# Estructura del frontend por features

Guía independiente del proyecto y de la herramienta de IA. Las convenciones del proyecto (`AGENTS.md`, guías de frontend) prevalecen sobre esta guía.

## Cuándo usar

- Crear una feature o agregar una pantalla.
- Decidir dónde colocar un componente, hook, servicio o esquema.
- Definir rutas y permisos de navegación.
- Revisar acoplamiento entre features.

## Antes de empezar

1. Leer la arquitectura y las guías de desarrollo, formularios y UI del proyecto.
2. Revisar una feature existente similar y replicar su estructura.
3. Respetar el lenguaje y las extensiones del proyecto (por ejemplo `.jsx` para componentes y `.js` para utilidades, servicios y esquemas).

## Estructura base

```text
src/
  app/              # composición: providers, rutas, componente raíz
  components/       # componentes compartidos y base visual (ui/)
  features/
    <feature>/
      components/   # componentes propios de la feature
      hooks/        # hooks de datos y de UI de la feature
      pages/        # componentes de página, uno por ruta
      schemas/      # esquemas Zod de formularios y contratos
      services/     # llamadas a la API de la feature
      utils/        # utilidades propias de la feature
      index.js      # API pública de la feature
  hooks/            # hooks compartidos
  lib/              # cliente HTTP, entorno, utilidades genéricas
  <núcleo>/         # sesión, tenant y permisos (si aplica)
  styles/
  main.jsx
```

## Reglas de ubicación

| Elemento | Dónde va |
| --- | --- |
| Pantalla ligada a una ruta | `features/<f>/pages` |
| Componente usado solo por una feature | `features/<f>/components` |
| Componente usado por dos o más features | `components/` |
| Primitivas de UI (shadcn/ui) | `components/ui` |
| Llamadas HTTP de una feature | `features/<f>/services` |
| Configuración del cliente HTTP | `lib/api-client` (único punto) |
| Esquema de formulario o contrato | `features/<f>/schemas` |
| Sesión, tenant y permisos globales | Núcleo compartido, no en una feature |

## Fronteras entre features

- Una feature expone lo que otras pueden usar mediante su `index.js`. No se importan archivos internos de otra feature.
- Las features no se importan en ciclo. Lo compartido se sube a `components/`, `hooks/` o `lib/`.
- La composición entre features, por ejemplo una pantalla que combina secciones de dos módulos, ocurre en la página o en `app/`, no dentro de una feature ajena.
- Cada feature corresponde a un módulo funcional del backend y presenta solo los datos que ese módulo posee. Los datos de otro módulo se consultan por su API.

## Rutas y permisos

- Las rutas se declaran en `app/routes`. Cada feature aporta sus páginas y el archivo de rutas las importa.
- Las rutas autenticadas se envuelven en un componente de ruta protegida.
- La visibilidad de menús y acciones usa los permisos efectivos del usuario. La autorización definitiva siempre reside en el backend.
- Los códigos de permiso se definen por el backend. El frontend no inventa permisos.

## Datos remotos (TanStack Query)

- Las páginas y componentes no llaman a `fetch` directamente. Usan hooks de la feature que envuelven los servicios.
- Claves de consulta jerárquicas y centralizadas por feature (por ejemplo `['persons', 'list', filtros]`).
- Tras una mutación, invalidar las claves afectadas.
- Manejar siempre los estados de carga, error y vacío.
- No guardar datos remotos en estado local ni en Context. Context se reserva para sesión, tenant y acceso.

## Formularios (React Hook Form + Zod)

- Un esquema Zod por formulario en `schemas/`, usado con el resolver de React Hook Form.
- Los mensajes de error son claros y están en el idioma de la interfaz.
- Los errores devueltos por la API se mapean a los campos del formulario.
- El esquema valida el formato y la obligatoriedad. Las reglas de negocio se validan en el backend.

## UI (Tailwind y shadcn/ui)

- Reutilizar los componentes de `components/ui` antes de crear otros.
- Estilos con clases de Tailwind y utilidades de composición de clases. Evitar CSS ad hoc.
- Accesibilidad mínima: etiquetas en campos, foco visible, navegación con teclado y contraste suficiente.
- Textos de la interfaz en un solo idioma y con terminología coherente.

## Seguridad

- No guardar secretos en el código ni en variables `VITE_*`. Todo lo que lleva ese prefijo es público.
- Los tokens se gestionan en un único módulo de sesión.
- No enviar identificadores de tenant arbitrarios en operaciones de negocio.
- No renderizar HTML sin sanitizar.

## Pasos para agregar una feature

1. Confirmar que el requisito o el análisis vigente respalda la pantalla.
2. Crear `features/<f>/` con las carpetas que realmente se necesiten, sin carpetas vacías.
3. Definir los esquemas y los servicios según el contrato de la API.
4. Crear los hooks de datos con TanStack Query.
5. Construir las páginas y componentes con los componentes de UI existentes.
6. Exponer lo público en `index.js` y registrar las rutas y la navegación con sus permisos.
7. Agregar pruebas de la lógica pura (esquemas, utilidades, permisos) y verificar lint, pruebas y build.

## Lista de verificación

- [ ] La feature no importa archivos internos de otra feature.
- [ ] No hay llamadas HTTP fuera de `services/` y `lib/api-client`.
- [ ] Los datos remotos se gestionan con TanStack Query.
- [ ] Cada formulario tiene su esquema Zod.
- [ ] Las rutas están protegidas y los permisos provienen del backend.
- [ ] No hay secretos ni URLs fijas en el código.
- [ ] Lint, pruebas y build pasan.

## Errores frecuentes

- Crear una carpeta `common` que acumula código de varias features.
- Duplicar un componente en lugar de subirlo a `components/`.
- Calcular permisos en el cliente sin respaldo del backend.
- Mezclar lógica de negocio dentro de componentes de presentación.
- Copiar datos de otro módulo al estado local en lugar de consultarlos.
