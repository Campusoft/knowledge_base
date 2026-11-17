# Plan de Implementacion: [Nombre de la feature o modulo]

* **Identificador:** PLAN-[Codigo o secuencia, ej. `platform-01`, `catalog-03`]
* **Fecha de creacion:** [DD/MM/AAAA]
* **Version:** 1.0
* **Estado:** (Pendiente / En Desarrollo / Completado / Obsoleto)
* **Prioridad:** (Alta / Media / Baja)
* **Tipo de plan:** (Backend / Frontend / Full-stack / Herramienta-Script)
* **Modulo(s) afectado(s):** [ej. `Campusoft.Platform`, `app/frontend/src/features/customers`, `app/Catalog`]
* **Responsable:** [Nombre del Tech Lead / desarrollador asignado]
* **Requerimiento(s) relacionado(s):** [RF-XXX / RNF-XXX, o "N/A"]
* **ADR relacionado(s):** [Numero de ADR, o "N/A"]

> Esta plantilla es unica para planes de implementacion Backend y Frontend. Las secciones marcadas **(Obligatoria)** se completan siempre. Las secciones marcadas **(Opcional)** se incluyen solo cuando aplican al caso; si no aplican, se eliminan del documento en vez de dejarlas vacias.

---

## 1. Objetivo (Obligatoria)

Descripcion breve (1-3 parrafos) de que se va a construir y por que. Responder: que problema resuelve, a quien beneficia, y en que feature/dominio se ubica.

## 2. Alcance (Obligatoria)

Lista concreta de lo que este plan SI cubre.

- [Elemento de alcance 1]
- [Elemento de alcance 2]
- [Elemento de alcance 3]

## 3. Fuera de alcance (Obligatoria)

Lista concreta de lo que este plan NO cubre, incluyendo referencias a otros planes que si lo cubren cuando existan.

- [Elemento fuera de alcance 1, ej. "Edicion de X, cubierta por `plan-xx-nombre.md`"]
- [Elemento fuera de alcance 2]

## 4. Dependencias (Obligatoria)

Elementos que deben existir o estar resueltos antes de ejecutar este plan.

- [Feature, servicio o libreria previa requerida]
- [Plan de implementacion previo, ej. "Auth implementado"]
- [Contrato API u OpenAPI vigente del que depende]
- [Version minima de una libreria/paquete]

## 5. Contratos API relacionados (Opcional - Frontend que consume API, Backend que expone API)

Usar cuando el plan consume o expone endpoints HTTP.

- Endpoints consumidos o expuestos: `MÉTODO /ruta`
- Parametros relevantes (query, path, body).
- Campos principales de request/response relevantes para la UI o el consumidor.
- Fuente del contrato: [ruta al OpenAPI/swagger vigente, nunca inferido del codigo].

## 6. Modelo de datos y migraciones (Opcional - cuando el plan crea o modifica entidades/esquema)

Usar cuando el plan implica cambios de entidades, tablas o migraciones de base de datos.

- Entidades nuevas o modificadas: [nombre, campos clave, relaciones].
- Migraciones requeridas: [descripcion, y si son reversibles].
- Impacto en datos existentes: [backfill, defaults, riesgo de perdida de datos].

## 7. Ubicacion y estructura de archivos esperada (Opcional - proyectos/features nuevos)

Usar cuando el plan crea una feature, modulo o proyecto nuevo, o reorganiza uno existente. Incluir el arbol de carpetas esperado.

```text
[ej. src/features/nombre-feature/
       pages/
       components/
       hooks/
       services/
       schemas/
     -- o --
     app/Modulo/Proyecto/
       Domain/
       Application/
       Infrastructure/
       HttpApi/]
```

## 8. Tecnologia y decisiones tecnicas (Opcional - cuando el plan introduce tecnologia, patron o dependencia nueva)

Usar solo si se introduce algo que no esta ya cubierto por la arquitectura vigente (`doc/arquitectura/`, `doc/arquitectura-componentes/`, `app/frontend/doc/arquitectura-frontend.md`). Si la decision es relevante y duradera, debe registrarse tambien como ADR (`ADR_plantilla.decisiones.arquitectura.v.1.0.md`).

| Elemento | Decision |
| --- | --- |
| [ej. Lenguaje/Runtime] | [ej. .NET 8 / React 19] |
| [ej. Libreria] | [nombre y version] |

## 9. Interfaz de ejecucion (Opcional - CLI, jobs, scripts, herramientas de consola)

Usar cuando el plan produce una herramienta ejecutable en vez de (o ademas de) una pantalla o endpoint.

```bash
[comando esperado --input ... --output ...]
```

| Parametro | Obligatorio | Descripcion |
| --- | --- | --- |
| [--parametro] | Si/No | [descripcion] |

## 10. Formato de entrada/salida (Opcional - integraciones, importadores, ETL, procesos batch)

Usar cuando el plan procesa archivos o payloads con un formato especifico de entrada y/o salida.

```json
[ejemplo representativo de entrada o salida]
```

## 11. Configuracion y variables de entorno (Opcional - cuando el plan agrega o modifica configuracion)

- [Variable de entorno, proposito, valor de ejemplo]
- Seguir convenciones existentes: `.env.example` en frontend, `appsettings`/variables de entorno en backend.

## 12. Seguridad y permisos (Opcional - cuando el plan expone datos sensibles o requiere control de acceso)

- Permisos o roles requeridos para acceder a la funcionalidad.
- Datos sensibles involucrados y como se protegen.
- Validaciones de autorizacion esperadas (frontend: control visual; backend: politica/handler).

## 13. Observabilidad (Opcional - cuando el plan requiere logs, metricas o trazabilidad especifica)

- Eventos o logs que deben registrarse.
- Metricas relevantes para monitorear la feature una vez desplegada.

## 14. Fases o rollout (Opcional - planes progresivos o de migracion en varias etapas)

Usar cuando la implementacion no es de una sola vez (migraciones, separacion de modulos, cambios progresivos). Documentar el estado de cada fase.

- **Fase A - [nombre]:** Estado: [Ejecutada / Pendiente]. [Detalle]
- **Fase B - [nombre]:** Estado: [Ejecutada / Pendiente]. [Detalle]

## 15. Pruebas unitarias (Obligatoria para Backend; Opcional para Frontend)

En todo plan de tipo Backend o Full-stack, la implementacion debe incluir pruebas unitarias para la logica de negocio, servicios y casos de uso creados o modificados. Este plan debe sugerir al menos los casos de prueba minimos esperados; el desarrollador puede agregar mas durante la construccion.

- Proyecto/carpeta de pruebas: [ej. `Campusoft.Platform.Tests`, `app/<Modulo>.Tests`].
- Componentes a cubrir: [servicio/caso de uso/validador que requiere prueba unitaria].
- Casos sugeridos (camino feliz, bordes y errores):
  - [ej. "Crear entidad X con datos validos retorna X creado con estado inicial correcto"]
  - [ej. "Crear entidad X con campo obligatorio faltante lanza excepcion de validacion"]
  - [ej. "Actualizar X en estado no permitido es rechazado"]
  - [ej. "Regla de negocio Y con condicion limite (cero, negativo, nulo, duplicado)"]
- Dependencias externas a simular (mocks/stubs): [repositorios, servicios externos, reloj/fecha, IDs].
- Cobertura minima esperada: [si el modulo define un umbral; de lo contrario, "cubrir toda regla de negocio nueva o modificada"].

En Frontend, esta seccion se activa solo cuando el plan introduce logica no trivial en hooks, servicios o validaciones (Zod) que amerite prueba unitaria; para UI pura de presentacion no es obligatoria.

## 16. Tareas (Obligatoria)

Lista verificable de tareas tecnicas en orden de ejecucion.

- [ ] [Tarea 1]
- [ ] [Tarea 2]
- [ ] [Tarea 3]
- [ ] Escribir pruebas unitarias segun la seccion 15 (Backend/Full-stack).

## 17. Criterios de aceptacion (Obligatoria)

Condiciones verificables que determinan que el plan esta completo. Redactar siempre desde la perspectiva de la prueba.

- [Criterio de aceptacion 1]
- [Criterio de aceptacion 2]
- Las pruebas unitarias sugeridas en la seccion 15 pasan (Backend/Full-stack).

## 18. Riesgos (Obligatoria)

- [Riesgo identificado y su posible impacto]
- [Riesgo identificado y su posible impacto]

## 19. Decisiones abiertas (Obligatoria)

Preguntas o definiciones pendientes que no bloquean el inicio del plan pero deben resolverse antes de darlo por completado.

- [Decision abierta 1]
- [Decision abierta 2]

---

## Guia rapida de uso

- Un archivo por capacidad funcional o unidad de trabajo (mismo criterio ya usado en `app/frontend/doc/implementation-plans/`).
- Nombrar el archivo con prefijo por feature/dominio y secuencia, ej. `platform-01-auth.md`, `catalog-03-prices.md`; la secuencia puede reiniciarse por dominio.
- Los endpoints citados deben provenir del OpenAPI/swagger vigente del modulo, nunca supuestos.
- Si el plan requiere una decision de arquitectura relevante y duradera, registrarla como ADR y enlazarla en el encabezado, no dentro de las tareas.
- Eliminar del documento final las secciones opcionales que no apliquen; no dejarlas como placeholder vacio.
- En planes Backend/Full-stack, la seccion 15 (Pruebas unitarias) es obligatoria: no se elimina, aunque los casos sugeridos se ajusten o amplien durante la construccion.
