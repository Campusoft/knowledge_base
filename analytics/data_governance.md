# Data Governance

# Niveles de madurez/alcance (mínimo, deseable, avanzado) 

Nivel | Conceptos principales
-- | --
Mínimo (básico) | Propiedad de datos, definiciones de negocio, calidad de datos básica, seguridad y privacidad, catálogo de datos
Deseable (intermedio) | Linaje, clasificación de datos, stewardship, retención, MDM básico
Avanzado (estratégico) | MDM completo, metadata management, lineage automatizado, ética de datos, data literacy, gobierno organizacional, monitoreo de calidad


## Nivel mínimo (lo básico e imprescindible)

Estos son los pilares que casi cualquier organización necesita:
- Propiedad de datos (Data Ownership) → quién es responsable de cada conjunto de datos.
- Definiciones de negocio → glosario común de KPIs, métricas, dimensiones.
- Calidad de datos (Data Quality) → controles básicos de duplicados, formatos, campos obligatorios.
- Seguridad y privacidad → accesos, roles, encriptación, cumplimiento legal (ej. LOPDP en Ecuador).
- Catálogo de datos (Data Catalog) → inventario de datos disponibles y su significado.

## Nivel deseable (lo recomendable en un DW/BI bien gestionado)

A medida que el gobierno madura, se agregan prácticas más organizadas:
- Linaje de datos (Data Lineage) → trazabilidad desde el origen hasta el dashboard.
- Clasificación y sensibilidad de datos → marcar qué es público, confidencial, sensible (ej. datos financieros, salud, PEP).
- Data Stewardship → personas que velan por la calidad y consistencia de los datos.
- Política de retención de datos → cuánto tiempo se conservan y cómo se eliminan/anonimizan.
- Gestión de cambios en datos maestros (MDM básico) → evitar múltiples versiones de "la misma persona/cliente".

## Nivel avanzado (madurez alta de gobierno de datos)

Ya se convierte en una práctica estratégica de toda la organización:
- Master Data Management (MDM) completo → repositorios únicos para personas, productos, cuentas.
- Metadata Management → gestión avanzada de metadatos técnicos + de negocio.
- Data Lineage automatizado → visualización automática de flujos ETL/ELT + reportes impactados.
- Data Ethics → asegurar uso responsable, ético y no discriminatorio de los datos.
- Data Literacy → programas de capacitación para que toda la organización entienda y use bien los datos.
- Data Governance Council → comité formal que define políticas, estándares y prioridades.
- Monitoreo continuo de calidad → dashboards de calidad, alertas por anomalías.


# Catálogo de datos (Data Catalog)

Es como una biblioteca organizada de todos los datos disponibles en la organización.

Permite a analistas, científicos de datos y usuarios de BI saber qué datos existen, qué significan, de dónde provienen y cómo usarlos.

Contiene típicamente:

- Inventario de datasets/tablas/campos (ej. FactVentas, DimCliente, TotalIngresos).
- Metadatos:
  - Nombre del campo
  - Tipo de dato (numérico, fecha, texto)
  - Definición de negocio (ej. “TotalIngresos = suma de todas las facturas cobradas”)
  - Calidad (completitud, frecuencia de actualización, confiabilidad).
- Clasificación: etiquetas como datos sensibles, financieros, demográficos.
- Usuarios responsables (data owners, stewards).
- Herramientas comunes: Azure Purview, Alation, Collibra, Google Data Catalog.

Ejemplo práctico: si alguien quiere usar "Ingresos Netos", puede buscarlo en el catálogo y ver qué tabla y campo debe usar, cómo está calculado y quién lo administra.


# Linaje de datos (Data Lineage)

Es la trazabilidad de los datos: muestra de dónde vienen, cómo se transforman y en qué reportes terminan.
- Responde a la pregunta: “¿Cuál es el recorrido de este dato desde su origen hasta el dashboard?”

Incluye:
- Origen → de qué sistema vienen (ej. ERP, CRM, call center).
- Transformaciones → qué cálculos o procesos ETL/ELT se aplicaron (ej. conversión de divisas, limpieza de duplicados, reglas de negocio).
- Destino → en qué tablas del Data Warehouse se almacenan.
- Consumo → qué reportes o KPIs los usan.

Ejemplo:
IngresosTotales en un dashboard de ventas puede provenir de:
Sistema Facturación → ETL de limpieza → FactVentas.TotalVenta → Power BI KPI "Ingresos Mensuales".

