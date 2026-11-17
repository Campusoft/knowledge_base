# LangSmith

> Documento revisado: 2026-11-13  
> Documentación oficial: [docs.langchain.com/langsmith](https://docs.langchain.com/langsmith/)

## Descripción

**LangSmith** es una plataforma para desarrollar, depurar, evaluar y operar aplicaciones basadas en modelos de lenguaje y agentes. Aunque forma parte del ecosistema de LangChain, puede observar y evaluar aplicaciones construidas con otros frameworks y SDK mediante sus integraciones y APIs.

Su propósito es hacer visible el comportamiento de una aplicación: qué entradas recibió, qué pasos ejecutó, qué herramientas utilizó, cuánto tardó y qué resultado produjo. Esa evidencia también puede convertirse en casos de evaluación y monitoreo continuo.

## Componentes principales

### Trazas y observabilidad

Una traza representa una ejecución y puede contener ejecuciones anidadas para llamadas a modelos, herramientas, retrievers y funciones propias. Los proyectos agrupan trazas relacionadas para inspeccionar errores, latencia, tokens, costos y comportamiento de versiones.

LangSmith admite instrumentación propia, integraciones de frameworks y compatibilidad con OpenTelemetry para propagar trazas entre servicios. La cobertura de los datos depende de cómo esté instrumentada la aplicación.

### Evaluación

Las evaluaciones permiten medir aplicaciones con un conjunto repetible de ejemplos:

1. **Dataset:** ejemplos de entrada y, cuando corresponda, respuestas o etiquetas de referencia.
2. **Target:** la función, cadena, agente o servicio que se desea evaluar.
3. **Evaluadores:** reglas deterministas, comparación con referencias, evaluadores basados en modelos u otras métricas.
4. **Experimento:** resultados de ejecutar un target sobre un dataset, útil para comparar modelos, prompts, retrievers o versiones.

Las evaluaciones offline sirven para probar cambios antes de publicar. Las evaluaciones online analizan trazas reales para vigilar calidad, detectar patrones problemáticos y descubrir ejemplos que conviene añadir a los datasets.

### Prompts y experimentación

La gestión de prompts permite guardar, versionar y compartir prompts, y asociarlos a experimentos. Esto facilita comparar cambios y reproducir qué instrucciones se usaron. Debe mantenerse control de versiones también en el repositorio de la aplicación y definir quién puede publicar cambios.

### Despliegue y operación

La documentación del producto incluye opciones de despliegue de agentes, entornos y flujos de evaluación/CI-CD. La disponibilidad y el modelo de operación dependen de la modalidad y el plan contratado; confirmar los requisitos de red, región, almacenamiento y control de acceso antes de adoptarlo.

## Arquitectura conceptual

```text
Aplicación LLM / agente
  ├── Framework o SDK (LangChain u otro)
  ├── Modelos, herramientas, APIs y fuentes de datos
  └── Instrumentación
         ├── SDK / integración de framework
         └── OpenTelemetry (cuando se requiere propagación entre servicios)
                 │
                 ▼
          LangSmith
            ├── Trazas y proyectos
            ├── Datasets y evaluaciones offline
            ├── Evaluaciones online y monitoreo
            ├── Gestión de prompts y experimentos
            └── Funciones de despliegue disponibles para el entorno
```

LangSmith observa y evalúa la aplicación; no reemplaza la lógica de negocio, las pruebas convencionales, las métricas de infraestructura ni los controles de seguridad de la aplicación.

## Casos de uso

- Depurar por qué un agente produjo una respuesta incorrecta o eligió una herramienta inadecuada.
- Comparar modelos, prompts, herramientas y estrategias RAG sobre los mismos ejemplos.
- Medir calidad, relevancia, corrección, latencia y costos antes de liberar una versión.
- Detectar cambios de calidad en tráfico real mediante evaluación online y muestreo.
- Crear un conjunto de regresión a partir de errores observados en producción.
- Seguir una solicitud que atraviesa varios servicios con propagación de contexto de trazas.
- Gestionar prompts compartidos y asociar sus versiones con experimentos.

## Recomendaciones

### Empezar con trazas útiles

1. Instrumentar una ruta representativa de extremo a extremo y revisar si las trazas muestran los pasos necesarios para diagnosticarla.
2. Registrar metadatos de versión de aplicación, modelo, prompt, entorno y herramientas, sin incluir secretos.
3. Usar nombres y proyectos consistentes para separar desarrollo, staging y producción.
4. Añadir muestreo cuando el volumen haga costoso o innecesario registrar cada ejecución.

### Evaluar antes y después de publicar

1. Crear datasets con casos frecuentes, límites y errores reales anonimizados.
2. Combinar métricas deterministas con evaluadores basados en modelos; revisar manualmente muestras de estos últimos.
3. Comparar cambios con el mismo dataset y conservar los resultados de referencia.
4. Vigilar online una muestra de producción y convertir hallazgos en nuevos casos offline.
5. No usar una única puntuación agregada: analizar por tipo de solicitud, segmento, fuente y trayectoria de herramientas.

### Privacidad, seguridad y gobierno

1. Clasificar los datos que pueden aparecer en entradas, salidas, prompts, herramientas y trazas.
2. Enmascarar o excluir datos personales, credenciales y contenido confidencial antes de exportar trazas.
3. Configurar acceso por roles y revisar la retención, ubicación y eliminación de datos según requisitos internos.
4. Evaluar el impacto de muestreo y redacción: una traza incompleta puede ocultar la causa de un fallo.
5. Revisar condiciones, opciones de alojamiento y políticas vigentes del servicio antes de usar datos regulados.

## Limitaciones y consideraciones

- La instrumentación no garantiza por sí sola que toda la ejecución quede registrada; las integraciones y el código propio deben configurarse correctamente.
- Las trazas pueden contener datos sensibles si no se aplican filtrado, redacción y controles de acceso.
- Los evaluadores basados en LLM pueden ser variables, sesgados o inconsistentes con el criterio del negocio; requieren calibración y revisión.
- Las evaluaciones dependen de la calidad y cobertura del dataset y no prueban todos los escenarios posibles.
- El muestreo reduce volumen y costo, pero puede omitir fallos poco frecuentes.
- El envío de telemetría a un servicio externo implica revisar residencia, retención, acceso, cumplimiento y costo.
- La plataforma no reemplaza pruebas unitarias, pruebas de integración, métricas de infraestructura ni controles de autorización y seguridad.
- La experiencia, límites, retención y capacidades disponibles pueden depender del plan y cambiar con el tiempo.

## Relación con LangChain y observabilidad abierta

- **LangChain** aporta componentes y abstracciones para construir aplicaciones LLM y agentes.
- **LangGraph** orquesta workflows con estado, ejecución durable e intervención humana.
- **LangSmith** ofrece trazabilidad, depuración, evaluación y herramientas operativas para aplicaciones de IA.
- **OpenTelemetry** proporciona estándares y APIs de instrumentación y propagación; puede complementar LangSmith en arquitecturas distribuidas.
- **Phoenix** es otra opción de observabilidad y evaluación del ecosistema de IA descrita en [Phoenix y OpenTelemetry](../observabilidad/phoenix-opentelemetry.md).

LangSmith y Phoenix/OpenTelemetry pueden cubrir necesidades parcialmente distintas o complementarias. La elección debe considerar integraciones requeridas, control de datos, despliegue, costos, retención, métricas y encaje con la plataforma de observabilidad existente.

## Fuentes

- [LangSmith: observabilidad](https://docs.langchain.com/langsmith/observability)
- [Conceptos de evaluación](https://docs.langchain.com/langsmith/evaluation-concepts)
- [Inicio rápido de evaluación](https://docs.langchain.com/langsmith/evaluation-quickstart)
- [Evaluaciones online](https://docs.langchain.com/langsmith/online-evaluations-llm-as-judge)
- [Trazas con OpenTelemetry](https://docs.langchain.com/langsmith/trace-with-opentelemetry)
- [Controlar y redactar datos de trazas](https://docs.langchain.com/langsmith/conditional-tracing)
- [Ejemplo de pipeline CI/CD](https://docs.langchain.com/langsmith/cicd-pipeline-example)
