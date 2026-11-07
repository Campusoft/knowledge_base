# Phoenix y OpenTelemetry

> Alcance: conceptos, arquitectura, casos de uso, limitaciones y recomendaciones para observar y evaluar aplicaciones de inteligencia artificial con Phoenix y OpenTelemetry.

## Resumen

**Phoenix**, de Arize AI, es una plataforma de observabilidad y evaluación de aplicaciones de machine learning e IA generativa. Permite inspeccionar trazas, analizar ejecuciones de LLM y flujos de recuperación, comparar experimentos y evaluar resultados.

**OpenTelemetry (OTel)** es un conjunto abierto de estándares y herramientas para generar, recopilar y exportar telemetría. Define señales como trazas, métricas y logs, además de convenciones semánticas y protocolos de exportación. No es en sí mismo una base de datos ni una interfaz de visualización.

En conjunto, OpenTelemetry puede instrumentar la aplicación y transportar telemetría, mientras Phoenix recibe y presenta trazas de IA para su análisis. La integración concreta depende de las versiones, instrumentaciones y despliegue elegidos.

## Qué aporta cada componente

| Componente | Responsabilidad | No debe confundirse con |
|---|---|---|
| **OpenTelemetry** | Instrumentación, contexto de trazas, convenciones y exportación de telemetría | Un backend único de almacenamiento o análisis |
| **Phoenix** | Observabilidad y evaluación de flujos de IA, con exploración de trazas y herramientas para análisis | Un sustituto de la instrumentación de la aplicación o del monitoreo de toda la plataforma |
| **Aplicación instrumentada** | Generar spans y atributos que describan solicitudes, modelos, herramientas y recuperación | Una fuente automática de datos completos; hay que elegir qué instrumentar |

## Arquitectura de referencia

```text
Aplicación / agente / pipeline RAG
    └─ SDKs e instrumentación OpenTelemetry
        └─ Spans, contexto y atributos
            └─ Exportador OTLP
                └─ Phoenix
                    ├─ Exploración de trazas
                    ├─ Evaluación y anotaciones
                    └─ Análisis de latencia, errores y calidad
```

La aplicación crea spans para operaciones relevantes, por ejemplo una solicitud al modelo, recuperación de documentos o invocación de una herramienta. La instrumentación propaga el contexto para relacionar operaciones dependientes. El exportador envía la telemetría, normalmente mediante OTLP, a un endpoint configurado para Phoenix.

Phoenix puede ejecutarse localmente para desarrollo o desplegarse en una infraestructura administrada por el equipo, según la edición y modalidad disponibles. En producción se deben definir explícitamente la autenticación, persistencia, retención, escalamiento y acceso a la interfaz.

## Capacidades principales

- **Trazas de aplicaciones LLM:** visualizar pasos de una ejecución y sus relaciones.
- **Instrumentación de frameworks:** integrar instrumentaciones disponibles para frameworks y SDKs de IA compatibles.
- **Análisis de RAG:** inspeccionar recuperación, documentos, fragmentos y generación para localizar fallos.
- **Inspección de llamadas a herramientas:** entender el orden, entradas y salidas de pasos de agentes, sujeto a lo que se registre.
- **Evaluación:** aplicar evaluadores y anotaciones para revisar calidad, relevancia o comportamiento.
- **Experimentos y conjuntos de datos:** organizar ejecuciones comparables para iterar sobre prompts, modelos o componentes.
- **Interoperabilidad:** usar OpenTelemetry/OTLP y convenciones semánticas compatibles para reducir acoplamiento con una instrumentación propietaria.

Las capacidades concretas pueden variar por versión y modalidad de Phoenix. Antes de diseñar una solución, verificar la documentación de la versión elegida.

## Casos de uso

1. **Diagnóstico de respuestas incorrectas:** recorrer la traza para identificar si el problema ocurrió en recuperación, construcción del prompt, herramienta o modelo.
2. **Optimización de latencia:** comparar la duración de spans de recuperación, inferencia y herramientas.
3. **Evaluación de cambios:** comparar modelos, prompts o estrategias de recuperación con un conjunto de casos representativos.
4. **Observación de agentes:** reconstruir la secuencia de decisiones y llamadas a herramientas registrada por la instrumentación.
5. **Control de regresiones:** ejecutar evaluaciones periódicas y detectar degradación de calidad tras cambios.
6. **Análisis de errores:** agrupar fallos y relacionarlos con versiones, modelos o componentes cuando esos atributos se registran.
7. **Desarrollo local:** inspeccionar trazas durante la construcción de una PoC antes de desplegar una plataforma de telemetría completa.

## Qué no resuelve por sí solo

- No garantiza que la respuesta de un modelo sea correcta; ofrece datos y herramientas para observarla y evaluarla.
- No instrumenta automáticamente todos los componentes ni conoce el contexto de negocio de cada aplicación.
- No reemplaza métricas de infraestructura, alertas, monitoreo de disponibilidad, auditoría ni gestión de incidentes.
- No establece por sí solo controles de privacidad, anonimización, autorización o retención de datos.
- No elimina la necesidad de definir criterios de evaluación y validar los evaluadores.
- No convierte telemetría incompleta o mal correlacionada en una explicación confiable.

## Limitaciones y riesgos

### Cobertura y calidad de la instrumentación

La visibilidad depende de qué spans y atributos emite cada SDK. Las convenciones semánticas y la cobertura de frameworks pueden cambiar; una integración puede requerir instrumentación manual y adaptación al actualizar dependencias.

### Coste y volumen

Registrar cada ejecución, especialmente con entradas y salidas extensas, puede elevar almacenamiento, transferencia y coste operativo. El muestreo reduce volumen, pero puede dejar fuera casos poco frecuentes o errores.

### Privacidad y secretos

Prompts, respuestas, documentos recuperados, parámetros y salidas de herramientas pueden contener datos personales, información financiera o secretos. Las trazas deben tratarse como datos sensibles y configurarse con minimización, redacción, controles de acceso y retención definida.

### Evaluaciones imperfectas

Los evaluadores automáticos pueden producir falsos positivos o negativos, y sus resultados dependen de los datos, criterios y modelos usados. Las decisiones importantes requieren validación humana y un conjunto de referencia representativo.

### Operación

Una instancia autoalojada requiere actualización, persistencia, copias de seguridad, capacidad, observabilidad de la propia plataforma y protección de red. El despliegue local de desarrollo no demuestra que la arquitectura esté lista para producción.

### Compatibilidad

La compatibilidad de instrumentaciones, convenciones y exportadores depende de versiones concretas. Cambios de esquema o de atributos pueden afectar consultas, paneles y evaluaciones.

## Recomendaciones de arquitectura y operación

1. **Definir el objetivo de observabilidad:** diagnóstico, calidad, rendimiento o experimentación; elegir las señales y atributos necesarios para cada objetivo.
2. **Instrumentar límites útiles:** registrar spans para solicitudes, recuperación, llamadas al modelo y herramientas, con contexto de correlación.
3. **Evitar dependencias innecesarias:** usar APIs e instrumentación estándar de OpenTelemetry donde sean adecuadas y aislar la configuración del exportador.
4. **Minimizar datos sensibles:** evitar registrar contenido completo por defecto; redactar o excluir secretos y datos personales, y documentar excepciones.
5. **Establecer muestreo y retención:** definir reglas por entorno, criticidad y volumen, y conservar muestras de errores con una política explícita.
6. **Separar entornos y accesos:** aislar desarrollo, pruebas y producción; proteger UI, endpoints de recepción y credenciales.
7. **Controlar cardinalidad:** no incluir identificadores únicos o texto libre como etiquetas métricas de alta cardinalidad.
8. **Versionar la instrumentación:** registrar versiones de aplicación, modelo, prompt y configuración relevantes para poder interpretar comparaciones.
9. **Validar evaluaciones:** medir acuerdo con revisiones humanas y mantener casos de prueba representativos del dominio.
10. **Monitorear Phoenix y el pipeline:** observar errores de exportación, retrasos, disponibilidad y consumo de recursos.
11. **Probar fallos de telemetría:** asegurar que una caída del backend de observabilidad no interrumpa indebidamente el flujo funcional de la aplicación.
12. **Revisar compatibilidad antes de actualizar:** probar SDKs, instrumentaciones, OTLP y consultas/paneles en un entorno de preproducción.

## Guía de adopción

### PoC o desarrollo local

- Instrumentar un flujo representativo, no toda la aplicación.
- Capturar trazas útiles para depurar; usar datos sintéticos o anonimizados.
- Validar que la propagación de contexto y la exportación funcionan.
- Evaluar si Phoenix responde las preguntas de diagnóstico planteadas.

### Producción

- Definir topología, alta disponibilidad, persistencia y recuperación.
- Acordar clasificación de datos, redacción, retención y permisos con los responsables de seguridad y datos.
- Configurar muestreo y límites de volumen según criticidad y carga esperada.
- Establecer monitoreo del exportador y del backend sin hacer depender la respuesta de negocio de la disponibilidad de Phoenix.
- Documentar versiones soportadas y procedimiento de actualización.

## Terminología

| Término | Descripción conceptual | Descripción técnica |
|---|---|---|
| **Telemetría** | Datos que describen el comportamiento de un sistema | Trazas, métricas y logs emitidos o recopilados por componentes |
| **Traza** | Recorrido de una operación a través de varios pasos | Conjunto de spans vinculados por un identificador de traza |
| **Span** | Una operación dentro de ese recorrido | Unidad con nombre, tiempos, atributos, estado y relaciones |
| **OTLP** | Protocolo para transportar datos de OpenTelemetry | Protocolo de exportación compatible con receptores que implementan OTLP |
| **Instrumentación** | Mecanismo que observa y describe operaciones | Código manual o librerías que crean spans, métricas y atributos |
| **Muestreo** | Selección de una parte de las operaciones para registrar | Política de decisión que limita qué trazas se exportan |

## Referencias

- [Phoenix: documentación oficial](https://arize.com/docs/phoenix)
- [Phoenix: repositorio oficial](https://github.com/Arize-ai/phoenix)
- [OpenTelemetry: documentación](https://opentelemetry.io/docs/)
- [OpenTelemetry Protocol (OTLP)](https://opentelemetry.io/docs/specs/otlp/)
- [OpenTelemetry Semantic Conventions](https://opentelemetry.io/docs/specs/semconv/)
