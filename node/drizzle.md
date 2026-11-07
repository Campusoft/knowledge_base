# Drizzle ORM

## Propósito

Drizzle ORM es una biblioteca de acceso a datos para TypeScript que permite definir el esquema de una base de datos en código y consultarla con una API tipada de estilo SQL o mediante consultas relacionales. Se presenta como una herramienta ligera que se integra en la estructura existente de la aplicación, en lugar de imponer un framework de aplicación completo.

Esta guía resume su arquitectura, capacidades, escenarios de uso, limitaciones y criterios de adopción. La documentación oficial enlazada al final debe consultarse para confirmar detalles específicos de cada dialecto y versión.

## Conceptos y arquitectura

Drizzle se compone principalmente de:

- **Drizzle ORM:** biblioteca de ejecución para declarar esquemas, construir consultas y ejecutarlas mediante un controlador o cliente compatible.
- **Drizzle Kit:** CLI complementaria para inspeccionar esquemas y generar, aplicar o sincronizar cambios de base de datos.
- **Dialectos y drivers:** APIs específicas para PostgreSQL, MySQL y SQLite, además de integraciones con servicios y runtimes compatibles. La combinación disponible depende del motor, el controlador y el entorno de ejecución.
- **Esquema TypeScript:** definición tipada de tablas, columnas, índices, restricciones y otros objetos soportados. Puede servir como fuente de verdad en el enfoque code-first.
- **SQL y consultas relacionales:** dos estilos de consulta. La API SQL-like expresa selecciones, joins y operaciones cercanas al SQL; la API relacional permite obtener entidades relacionadas mediante una forma declarativa.

Flujo habitual en un proyecto code-first:

1. El equipo define y exporta los modelos de tablas en módulos TypeScript.
2. La aplicación crea una instancia de Drizzle con el driver elegido y registra el esquema si necesita consultas relacionales.
3. Los servicios de aplicación ejecutan consultas tipadas o SQL explícito a través de Drizzle.
4. Drizzle Kit compara el esquema del código con su historial de migraciones y genera archivos SQL; las migraciones revisadas se aplican a cada entorno mediante el proceso de despliegue.

Drizzle también admite un enfoque database-first: una base de datos existente puede seguir siendo la fuente de verdad y el esquema se incorpora al código. La decisión debe alinearse con la gobernanza de cambios del equipo.

## Características

- Inferencia de tipos TypeScript para modelos, parámetros y resultados.
- Consultas SQL-like y consultas relacionales.
- Soporte de operaciones CRUD, joins, subconsultas, CTE y SQL personalizado mediante sus APIs.
- Integración con transacciones y, según el dialecto, opciones como aislamiento y savepoints.
- Definición de esquemas, columnas, claves, índices y restricciones según las capacidades del motor.
- Drizzle Kit para generar migraciones SQL, aplicarlas, sincronizar esquemas en desarrollo e introspeccionar bases existentes.
- Diseño modular: la aplicación elige el motor, el driver y las herramientas que necesita.
- Adecuación a despliegues serverless y edge cuando existe un driver compatible con el entorno concreto.

## Casos de uso

Drizzle puede ser apropiado para:

- APIs y servicios TypeScript/Node.js que necesitan acceso tipado a bases de datos relacionales.
- Aplicaciones que prefieren mantener consultas y migraciones cercanas al SQL.
- Proyectos que quieren inferencia de tipos sin adoptar un framework de persistencia amplio.
- Aplicaciones serverless o edge, si el proveedor, el runtime y el driver elegido son compatibles.
- Sistemas con una base existente que necesitan introspección y acceso tipado progresivo.
- Equipos que desean revisar SQL de migraciones en control de versiones y someterlo a revisión antes de aplicarlo.

## Limitaciones y aspectos a evaluar

- **No elimina la necesidad de conocer SQL y el motor.** El equipo sigue siendo responsable de índices, planes de ejecución, bloqueos, aislamiento, restricciones y optimización.
- **La portabilidad no es automática.** Los tipos, funciones, sintaxis, comportamiento de migraciones y capacidades varían entre PostgreSQL, MySQL, SQLite y servicios administrados.
- **Tipos de TypeScript no son validación de datos de entrada.** Los datos externos requieren validación en el límite de la aplicación; las restricciones de base de datos siguen siendo necesarias para integridad.
- **El ORM no reemplaza el diseño de dominio ni la arquitectura de la aplicación.** La lógica de negocio, autorización, límites transaccionales y políticas de reintento deben diseñarse explícitamente.
- **Las consultas relacionales tienen reglas e inferencias propias.** Debe revisarse la documentación del API y del dialecto, especialmente para relaciones complejas, cíclicas o consultas con requisitos de carga específicos.
- **Las migraciones requieren control operativo.** La generación automática no garantiza que una modificación sea segura para producción, reversible, compatible con versiones desplegadas ni adecuada para grandes volúmenes de datos.
- **La compatibilidad depende del driver y del runtime.** Verificar pooling, límites de conexiones, transacciones, prepared statements, TLS y comportamiento ante fallos del proveedor elegido.
- **La capa de abstracción tiene un costo de aprendizaje y mantenimiento.** Si un equipo usa SQL directo o una plataforma distinta, añadir Drizzle debe justificarse por el valor de tipos, productividad e integración.

Estas limitaciones son criterios de evaluación, no necesariamente defectos del producto; algunas dependen del motor, la versión, el driver y las decisiones de diseño del proyecto.

## Recomendaciones de arquitectura

1. **Mantener la persistencia detrás de límites claros.** Evitar que controladores HTTP, componentes UI o procesos de dominio dependan de detalles del driver. Ubicar acceso a datos en módulos de infraestructura o adaptadores apropiados para la arquitectura elegida.
2. **No filtrar el ORM por todo el dominio sin necesidad.** Decidir si los servicios trabajan con tipos del esquema, DTOs o modelos de aplicación. Mapear cuando ayude a proteger el dominio de cambios de persistencia.
3. **Tratar el esquema y las migraciones como código crítico.** Revisar los SQL generados, versionarlos y aplicar migraciones mediante una etapa controlada de CI/CD, con respaldo y estrategia de recuperación.
4. **Separar cambios de esquema de transformaciones de datos complejas.** Usar migraciones SQL personalizadas y procesos explícitos cuando una transformación no se represente de forma segura con un diff automático.
5. **Evitar `push` como mecanismo de despliegue productivo.** Reservarlo normalmente para iteración local controlada; en entornos compartidos o productivos, usar migraciones revisables y un proceso de aplicación reproducible.
6. **Definir transacciones alrededor de invariantes de negocio.** Mantener dentro de una misma transacción todas las escrituras que deban confirmarse o revertirse juntas, y configurar aislamiento de acuerdo con el motor y el riesgo de concurrencia.
7. **Usar consultas SQL explícitas cuando mejoren claridad o control.** Mantenerlas parametrizadas y revisar sus planes de ejecución; no forzar una abstracción si dificulta expresar una operación específica del motor.
8. **Gestionar conexiones y secretos fuera del código.** Usar configuración segura por entorno, pooling apropiado al runtime y credenciales con privilegios mínimos.
9. **Medir con el motor real.** Validar consultas críticas, concurrencia, latencia y volumen con el mismo motor, versión, driver y condiciones de despliegue previstos.
10. **Verificar compatibilidad antes de estandarizar.** Hacer una prueba técnica con el proveedor de base de datos, ORM, Drizzle Kit y runtime que realmente se utilizarán.

## Flujo recomendado de migraciones

### Desarrollo

1. Modificar el esquema TypeScript.
2. Generar una migración SQL con Drizzle Kit.
3. Revisar el SQL, los cambios destructivos, índices, valores por defecto y dependencias.
4. Aplicar la migración a una base local o efímera.
5. Incorporar la migración al control de versiones junto al cambio de aplicación.

### Integración y producción

1. Aplicar migraciones en una base representativa y ejecutar verificaciones del proyecto.
2. Establecer orden y exclusión mutua para evitar que varios despliegues apliquen cambios simultáneamente.
3. Usar cambios compatibles con despliegues graduales cuando convivan versiones de la aplicación: expandir, migrar datos y contraer en etapas separadas.
4. Respaldar y definir recuperación para cambios de alto impacto.
5. Aplicar migraciones con una identidad de despliegue separada de la cuenta de ejecución normal de la aplicación.
6. Registrar versión aplicada, resultado y duración; detener el despliegue ante fallos sin asumir que una migración parcialmente ejecutada se puede revertir automáticamente.

`drizzle-kit generate` crea archivos de migración para revisión; no equivale por sí solo a aplicarlos. `drizzle-kit migrate` aplica migraciones generadas. `drizzle-kit push` sincroniza esquema y base sin el flujo normal de archivos de migración, por lo que la documentación oficial lo recomienda principalmente para desarrollo local.

## Criterios para elegirlo

Elegir Drizzle cuando el equipo trabaja principalmente con TypeScript, valora SQL legible y tipos estáticos, y acepta administrar explícitamente drivers, migraciones y particularidades del motor. Compararlo mediante una prueba acotada con SQL directo u otros clientes de datos si el proyecto requiere capacidades avanzadas de unidad de trabajo, mapeo de dominio, carga de grafos o abstracciones que Drizzle no prioriza.

Antes de adoptarlo, comprobar:

- Motor, versión, hosting y driver exactos.
- Operaciones SQL especiales que usa el sistema.
- Requisitos de transacciones, aislamiento y concurrencia.
- Estrategia de migraciones y despliegue gradual.
- Observabilidad, pooling, límites de conexiones y seguridad.
- Experiencia del equipo y costo de mantener SQL específico del motor.

## Fuentes oficiales

- [Drizzle ORM: visión general](https://orm.drizzle.team/docs/overview)
- [Declaración de esquemas SQL](https://orm.drizzle.team/docs/sql-schema-declaration)
- [Consultas y operaciones CRUD](https://orm.drizzle.team/docs/data-querying)
- [Relaciones](https://orm.drizzle.team/docs/relations)
- [Transacciones](https://orm.drizzle.team/docs/transactions)
- [Fundamentos de migraciones](https://orm.drizzle.team/docs/migrations)
- [FAQ: `generate` y `push`](https://orm.drizzle.team/docs/faq)
- [Motores y opciones de inicio](https://orm.drizzle.team/docs/get-started)
