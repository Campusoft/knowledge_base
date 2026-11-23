# Knowledge Factory para agentes de IA

| Campo | Valor |
|---|---|
| Estado | Captura |
| Fecha de inicio | 2026-11 |
| Responsable | Por designar |
| Última actualización | 2026-11 |
| Destino (si se promueve) | Pendiente |

## 1. Fuente y resumen

- Fuente: *How to build a knowledge factory for AI agents*, Harrison Chase (LangChain), 21 de noviembre de 2026.
- URL: https://www.langchain.com/blog/knowledge-factory
- Este resumen es propio. Para el texto íntegro, consultar la fuente.

### 1.1 Problema

El factor limitante para que la IA aporte valor en una empresa no es la potencia del modelo ni el tamaño del contexto. Es el **conocimiento** de cómo funciona la organización: estándares internos, formas de hacer las cosas, dónde están los datos y por qué se decidió cada cosa. Ese conocimiento es en gran parte tácito, implícito y disperso (personas, Slack, Notion, wikis incompletas, correos).

Para que los agentes trabajen de forma confiable, hay que **recolectar, curar y mantener** una representación estructurada de ese conocimiento. El sistema que lo hace de manera continua se llama **Knowledge Factory**. LangChain lo aplica en sus agentes GTM, Content Studio y LangSmith Engine.

### 1.2 Contexto frente a conocimiento

- **Contexto:** todo lo que el agente tiene disponible al ejecutar una tarea concreta.
- **Conocimiento:** entendimiento reutilizable y mantenido en el tiempo (definiciones, políticas, criterios de confianza en fuentes, manejo de casos). Puede estar documentado o no.

La fábrica convierte documentos dispersos y experiencia acumulada en conocimiento estructurado, que luego pasa a ser contexto en futuras ejecuciones. Los datos que ya se gestionan de forma fiable, sobre todo los que cambian rápido, se entregan directamente al agente.

### 1.3 Analogía con la Software Factory

En una Software Factory, los mejores ingenieros construyen el sistema que permite al resto de la organización y a los agentes construir software. El trabajo de conocimiento sigue el mismo camino: los agentes ya generan presentaciones, reportes o outreach, pero necesitan el conocimiento tácito de la empresa. La palanca está en construir la fábrica que lo recolecte, cure y actualice.

### 1.4 Tres tipos de conocimiento

| Tipo | Contenido |
|---|---|
| Skills | Cómo hacer el trabajo: procedimientos, instrucciones y scripts según los estándares de la empresa. Muchas veces nunca se escribieron. |
| Domain knowledge | Cómo funciona el negocio: definiciones, políticas y hechos estables sobre productos, clientes, estructura y procesos. |
| Data access guidance | Dónde obtener información actualizada: qué fuente usar y cómo consultarla. No se duplican los datos si ya hay una buena fuente (Snowflake, Databricks, BigQuery). |

### 1.5 Componentes de la fábrica

1. **Reporte de conocimiento faltante o incorrecto:** una herramienta para que el agente señale lagunas o errores mientras trabaja y, opcionalmente, proponga la corrección.
2. **Curator agent:** agente en segundo plano, periódico, que actualiza y mantiene el conocimiento. Se separa de los agentes operativos para evitar contradicciones. Accede a las fuentes originales, a los traces de ejecución y a los reportes de problemas.
3. **Almacenamiento:** es el componente menos crítico hoy. Opciones: skills en GitHub, archivos en Notion, formato OKF en LangSmith Context Hub, grafos (Neo4j). No existe estándar consolidado para memoria semántica. Se recomienda no sobrecomplicar el formato.

### 1.6 Ejemplo de la fuente

Un agente prepara un reporte de ingresos con datos en BigQuery y Salesforce. Durante la tarea detecta que una columna fue renombrada, lo resuelve y lo reporta. Después, un líder de finanzas corrige el formato e introduce un nuevo encabezado obligatorio. Esa noche, el curator agent revisa el reporte y los traces, y actualiza los skills. En la siguiente ejecución el agente ya usa la información corregida.

### 1.7 Recomendación de la fuente

No documentar todo antes de empezar. Construir un sistema de aprendizaje continuo: trabajar revela lo que falta, y cerrar esas lagunas mejora al agente. Empezar con **un flujo recurrente**, observar dónde falla el agente y cerrar el ciclo reporte → curación → actualización.

## 2. Investigación complementaria

Pendiente. Puntos a investigar:

- Otras implementaciones de memoria y curación de conocimiento para agentes (skills, bases de conocimiento versionadas).
- Riesgos de que un agente modifique conocimiento de forma automática (calidad, seguridad, trazabilidad).
- Formatos de skills y su compatibilidad con herramientas que Campusoft ya usa.
- Evidencia de resultados (métricas, casos) más allá del artículo, que es de un proveedor con interés comercial.

| Fuente | Aporte | A favor / En contra |
|---|---|---|
| | | |

## 3. Evaluación

Resultado: **Pendiente**

Punto de partida para la evaluación: Campusoft ya tiene piezas relacionadas, que conviene contrastar con la propuesta.

| Criterio | Valoración |
|---|---|
| Problema concreto que resuelve en Campusoft | Pendiente. ¿Qué conocimiento tácito o disperso afecta hoy al desarrollo? |
| Encaje con políticas y estándares vigentes | Pendiente. Ya existen `estandares/skills/` (los estándares son la fuente de verdad y los skills son derivados) y una política de ciclo de vida con aprobación. Un curator agent que edite skills directamente chocaría con esa regla, salvo que proponga cambios que luego se aprueben. |
| Costo y esfuerzo de adopción | Pendiente |
| Riesgos | Pendiente. Cambios automáticos sin revisión humana, exposición de información sensible a agentes, deriva entre estándares y skills. |
| Equipos o proyectos afectados | Pendiente |

Justificación:

## 4. Propuesta personalizada

Pendiente. Ideas iniciales para evaluar:

- Tomar solo parte: el mecanismo de reporte de conocimiento faltante y un flujo de curación con aprobación humana.
- Elegir un único flujo recurrente para el piloto, como recomienda la fuente.
- Mantener la jerarquía actual: el curator propone cambios a estándares y guías, y no modifica contenido aprobado por sí mismo.

## 5. Piloto

| Campo | Valor |
|---|---|
| Equipo / proyecto | Pendiente |
| Duración | Pendiente |
| Métricas de éxito | Pendiente |
| Resultados | |
| Lecciones | |

## 6. Decisión

Pendiente.

- Fecha:
- Decidido por:
- Motivo:
- Destino (estándar o guía) y enlace:
