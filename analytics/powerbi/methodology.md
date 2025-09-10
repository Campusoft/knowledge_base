# Generar indicadores. 


# Metodogias trabajo (Framework)


# Fases Power BI

- Get Data. Obtener datos, excel, pdf, base de datos, ect
- Data Preparation. Preparacion de Datos. De las tablas de datos obtenidas - limpia y organiza en Query Editor.
- Data Modeling. Modelado de Datos. De las tablas de datos, crea estructuras (modelos) que permitan relacionar datos.
- Data Visualization. Visualizacion de Datos. Representaciones de datos en forma de graficos, matrices, y mas visualizaciones
- Data Reporting. Reporteo de Datos. Estructura y Formato de visualizaaciones y elementos que daran lugar a un Reporte. 
 
https://user-images.githubusercontent.com/11231959/118572546-536a4800-b746-11eb-9b4c-c97ffacf2824.png


**Metodología estructurada para diseñar y construir tableros de Power BI**


1. Definición de Objetivos

- Entender la necesidad del negocio: ¿Qué preguntas se quieren responder?
- Definir indicadores clave (KPIs): ventas, rentabilidad, mora, rotación, etc.
- Identificar usuarios finales: gerencia, analistas, área operativa.
- Alcance del tablero: estratégico, táctico u operativo.

Herramienta recomendada: entrevistas, workshops, o un canvas de KPIs.

2. Recolección y Análisis de Datos
- Identificar fuentes de datos: ERP, CRM, archivos Excel, bases de datos (SQL, Oracle, etc.).
- Validar calidad de datos: duplicados, valores nulos, inconsistencias.
- Definir periodicidad de actualización: tiempo real, diario, semanal.

Es clave documentar las fuentes y responsables de cada dato (gobernanza de datos).

3. Modelado de Datos (Power Query & Power BI Desktop)

- ETL (Extracción, Transformación y Carga):
  - Limpieza y normalización en Power Query.
  -  Aplicar reglas de negocio (ejemplo: categorización de clientes).
  - Modelo Estrella (Star Schema):
- Tablas de hechos (ventas, transacciones).
- Tablas de dimensiones (clientes, productos, tiempo).
- Buenas prácticas DAX: medidas en tablas separadas, evitar cálculos en columnas cuando sea posible.

Objetivo: modelo optimizado, entendible y flexible.

4. Diseño del Tablero

- Principios de visualización:
  - Jerarquía visual clara (qué KPI es más importante).
  - Usar colores corporativos y consistentes.
  - Evitar sobrecarga (no más de 6–8 visualizaciones por página).
- Tipos de tableros:
  - Estratégico → KPIs de alto nivel (ej. rentabilidad).
  - Táctico → comparaciones y tendencias (ej. ventas por región).
  - Operativo → detalle granular (ej. facturas pendientes).

- Interactividad:
  - Segmentadores (slicers).
  - Navegación entre páginas.
  - Drill-through / tooltips personalizados.

Usar prototipos (wireframes en Figma, PowerPoint o incluso papel).

5. Validación con el Usuario

- Validar cálculos de KPIs con el negocio.
- Hacer pruebas de navegación e interactividad.
- Ajustar diseño según feedback.
- Importante: que el tablero responda a preguntas reales, no solo mostrar datos.

6. Publicación y Gobernanza

- Publicar en Power BI Service.
- Configurar workspaces (dev, test, prod).
- Asignar permisos (lectura, edición, administración).
- Definir políticas de actualización (gateway de datos).
- Si es corporativo, aplicar seguridad a nivel de fila (RLS).

7. Mantenimiento y Evolución

- Monitorear uso con el Usage Metrics Report de Power BI Service.
- Revisar si los KPIs siguen alineados con los objetivos del negocio.
- Mantener documentación (fuentes, KPIs, reglas de negocio).
- Plan de mejora continua (nuevos requerimientos, nuevas fuentes de datos).



## Referencias


**Power BI Adoption Framework**
 

Welcome to the Power BI Adoption Framework repository. This repository includes all the presentations that you require to run through the Power BI Adoption workshops as described in the YouTube series.

1. Power BI Adoption
2. Power BI Governance
3. Power BI Service Management
4. Power BI Security
5. Power BI Rollout and Support
-  Strategy Documents
https://github.com/pbiaf/powerbiadoption


Introduction to the series: Part 1 | Power BI Adoption Framework
Tiene documentos presentacion, y plantillas documentos framework
https://www.youtube.com/watch?v=e7Nb-XmrOfY


# Labs

Determinar el tipo de dashboard. operational dashboards, analytical dashboards

As a first step, you should  determine the purpose of the dashboard – operational dashboards provide time-critical data  to consumers. I like to think about operational dashboards as of cockpit in the car or plane… On  the other hand, analytical dashboards focus more on identifying trends and patterns from historical data and enable better mid to long-term decision-making.


6 pasos fundamentales para diseñar un dashboard
- KPI’s. Planificar las métricas fundamentales. Vamos a medir en función de los objetivos, de qué fuentes de datos extraerlos y la herramienta de visualización que utilicemos. 
- Audiencia. Conocer al usuario. Para esto tenemos que tener en claro: ¿Quién usará el dashboard? ¿Cuáles son sus expectativas? ¿Qué tipo de decisiones tomará con él?
- Contenido. ¿Qué data debería mostrar el dashboard? ¿Qué secciones debería tener y qué contenido no puede faltar?
- Mockup. Crear un borrador antes de comenzar el diseño total en una herramienta de Business Intelligence. Esta es la parte en la que pensamos cómo ubicar los componentes que definimos en el paso anterior. ¿Cómo lo imaginamos? ¿Cómo nos gustaría que se vea? ¿Qué lineamientos tenemos que seguir? 
- Revisar. Conversar sobre el mockup con los principales stakeholders y confirmar que el contenido sea el correcto. ¿Es la información que esperan obtener? ¿Lo entienden a nivel conceptual? ¿Qué le quitarían o agregarían? 
- Diseñar (tool). Ahora sí, es el momento de diseñar, teniendo en cuenta el feedback recibido y dentro de una plataforma de BI. El lienzo comienza a poblarse de todos los componentes que ya fueron validados por el equipo. 
https://miamiadschool.com.ar/blog/data-analytics-6-pasos-y-8-consejos-para-disenar-un-dashboard/


# Revisiones


¿Cómo utilizar la "Metodología Agile" en proyectos de Power BI aplicados a la auditoría? Parte 1 
- 1. ANÁLISIS GENERAL DEL REQUERIMIENTO (INPUT – OUTPUT)
- 2. DISEÑO GENERAL DEL TABLERO (VISTAS AGRUPAMIENTO)
- 3. CONEXIÓN Y TRANSFORMACIÓN DE LA INFORMACIÓN
- 4. CÁLCULO DE MEDIDAS Y COLUMNAS CALCULADAS (RESULTADOS)
- 5. DISEÑO DE ASPECTOS VISUALES
- 6. TESTEO DE LOS RESULTADOS CON INFORMACIÓN REAL
https://www.auditool.org/blog/auditoria-de-ti/8113-como-utilizar-la-metodologia-agile-en-proyectos-de-power-bi-parte-1
https://www.auditool.org/blog/auditoria-de-ti/8134-como-utilizar-la-metodologia-agile-en-proyectos-de-power-bi-aplicados-a-la-auditoria-parte-2
https://www.auditool.org/blog/auditoria-de-ti/8149-como-utilizar-la-metodologia-agile-en-proyectos-de-power-bi-aplicados-a-la-auditoria-parte-3