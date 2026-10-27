# Guía central para construir una arquitectura

Esta guía permite identificar rápidamente qué decisiones y elementos considerar al diseñar una arquitectura de software. Sirve como punto de partida para arquitecturas nuevas o para evolucionar una solución existente. No es una receta: los requisitos, riesgos y restricciones del negocio determinan qué aplicar.

## Ruta rápida

1. **Entender el negocio y el alcance:** quién usa la solución, qué capacidades ofrece, qué sistemas externos intervienen y qué queda fuera.
2. **Definir requisitos de calidad:** disponibilidad, rendimiento, seguridad, escalabilidad, resiliencia, mantenibilidad, privacidad y costo. Para cada uno, anotar una medida o umbral verificable.
3. **Registrar restricciones y riesgos:** regulación, residencia de datos, tecnologías existentes, habilidades del equipo, presupuesto, plazos y dependencias.
4. **Elegir el estilo de arquitectura:** por ejemplo, monolito modular, arquitectura por capas, orientada a eventos, microservicios o serverless. Explicar por qué responde al contexto.
5. **Identificar componentes y límites:** aplicaciones, módulos o servicios; responsabilidades; propiedad de datos; interfaces y dependencias.
6. **Diseñar los flujos principales:** entradas, autenticación, procesamiento, comunicación, persistencia, errores y respuestas.
7. **Definir datos e integración:** sistemas de registro, consistencia, APIs, eventos, colas, formatos y contratos.
8. **Definir seguridad y operación:** controles por capa, observabilidad, despliegue, escalado, respaldo, recuperación y soporte.
9. **Comparar alternativas y registrar decisiones:** opciones consideradas, consecuencias, supuestos y preguntas abiertas mediante ADR.
10. **Validar con escenarios:** flujo normal, carga alta, caída de una dependencia, incidente de seguridad y recuperación.

## Lista básica para revisar una arquitectura

| Área | Pregunta que debe quedar respondida | Referencia |
|---|---|---|
| Contexto y alcance | ¿Qué capacidades y actores cubre la solución? | [Dominios](dominios/README.md) |
| Atributos de calidad | ¿Qué significa que la solución sea segura, disponible y rápida? | [Fundamentos](fundamentos/README.md) |
| Estilo y límites | ¿Cómo se divide el sistema y por qué? | [Estilos y patrones](estilos-y-patrones/README.md) |
| Componentes | ¿Qué responsabilidad cumple cada componente y cuándo se necesita? | [Componentes](componentes/README.md) |
| Comunicación | ¿Cómo intercambian datos las partes y los sistemas externos? | [Comunicación e integración](comunicacion-e-integracion/README.md) |
| Datos | ¿Quién es dueño de cada dato, dónde vive y cómo se protege? | [Datos](datos/README.md) |
| Plataforma | ¿Dónde se ejecuta y cómo se conectan los ambientes? | [Infraestructura](infraestructura/README.md) |
| Seguridad | ¿Cómo se identifican usuarios y servicios, y cómo se reduce el riesgo? | [Seguridad](seguridad/README.md) |
| Operación | ¿Cómo se detectan fallos, se despliega y se recupera el servicio? | [Operación](operacion/README.md) |
| Decisiones | ¿Qué opciones se evaluaron y qué consecuencias se aceptan? | [Decisiones y documentación](decisiones-y-documentacion/README.md) |

## Crear una arquitectura nueva

Para iniciar un nuevo diseño, crear una carpeta para la solución o iniciativa y documentar, como mínimo:

1. Propósito, alcance, actores y capacidades de negocio.
2. Requisitos funcionales y atributos de calidad medibles.
3. Contexto: sistemas relacionados, límites de confianza y flujos críticos.
4. Vista lógica: componentes, responsabilidades e interfaces.
5. Vista de datos: entidades relevantes, propiedad, clasificación y persistencia.
6. Vista de despliegue: ambientes, redes, servicios de plataforma y dependencias.
7. Seguridad, disponibilidad, escalado, observabilidad y recuperación.
8. Decisiones arquitectónicas (ADR), alternativas descartadas y riesgos pendientes.
9. Diagramas necesarios para explicar contexto, contenedores/componentes y despliegue.

Conviene empezar con las vistas necesarias para responder preguntas concretas; no es necesario diagramar cada detalle antes de validar el diseño.

## Consideraciones por línea de negocio

Las líneas de negocio reutilizan las mismas áreas de arquitectura, pero cambian prioridades, restricciones y escenarios. Estas listas son puntos de análisis, no diseños prescriptivos.

### Financiera

- Analizar regulación aplicable, privacidad, clasificación y residencia de datos.
- Definir controles de identidad, autorización, segregación de funciones, auditoría y gestión de secretos.
- Establecer integridad transaccional, idempotencia, conciliación y trazabilidad de operaciones.
- Evaluar disponibilidad, recuperación, continuidad, protección contra fraude y límites de terceros.
- Acordar objetivos medibles para latencia, consistencia, recuperación y retención.

### Universidad

- Identificar estudiantes, docentes, personal administrativo, aspirantes e integraciones institucionales.
- Revisar privacidad de datos académicos y personales, permisos por rol y ciclo de vida de matrícula.
- Considerar periodos de alta demanda: matrículas, evaluaciones, publicación de notas y pagos.
- Definir interoperabilidad entre LMS, sistema académico, identidad, biblioteca, pagos y reportes.
- Considerar accesibilidad, experiencia móvil, conectividad variable y procesos académicos auditables.

### Cómo agregar otra línea de negocio

Crear una sección con: actores y capacidades; datos sensibles y regulación; flujos críticos; integraciones; picos de carga; requisitos de disponibilidad; controles particulares; y decisiones o patrones frecuentes. Mantener los principios comunes en las categorías generales y enlazarlos desde la sección del negocio.

## Plantilla breve de decisión arquitectónica

- **Contexto y problema:**
- **Requisitos y restricciones:**
- **Decisión:**
- **Alternativas consideradas:**
- **Consecuencias y riesgos:**
- **Cómo se validará:**

Ver [documentación y ADR](decisiones-y-documentacion/README.md) para ampliar el registro de decisiones.
