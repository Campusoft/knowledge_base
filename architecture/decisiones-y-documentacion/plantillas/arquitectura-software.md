# Plantilla de arquitectura de software

> Copiar este archivo al directorio de la iniciativa y completar las secciones pertinentes. Eliminar instrucciones entre corchetes y secciones que no aporten valor. Para decisiones complejas, enlazar ADR independientes.

## 1. Identificación

- **Sistema / iniciativa:** [nombre]
- **Versión del documento:** [versión]
- **Estado:** [borrador / revisión / aprobado / reemplazado]
- **Responsable:** [persona o equipo]
- **Fecha de creación / revisión:** [AAAA-MM-DD]
- **Partes interesadas:** [roles y equipos]
- **Referencias:** [requisitos, repositorios, documentación relacionada]

## 2. Resumen y objetivos

[Qué problema resuelve el sistema, para quién y qué resultado se espera.]

### Alcance

- Incluye: [capacidades o procesos]
- No incluye: [límites explícitos]

### Objetivos de calidad prioritarios

| Objetivo | Escenario / medida | Umbral o criterio | Prioridad |
|---|---|---|---|
| [disponibilidad, rendimiento, seguridad, etc.] | [condición observable] | [valor verificable] | [alta/media/baja] |

## 3. Contexto y alcance

### Actores y sistemas externos

| Actor o sistema | Responsabilidad | Interacción / datos |
|---|---|---|
| [nombre] | [responsabilidad] | [interacción] |

### Diagrama de contexto

[Insertar diagrama C4 de contexto o enlace a su fuente editable.]

## 4. Restricciones, supuestos y dependencias

- **Restricciones:** [tecnología, regulación, presupuesto, fechas, habilidades]
- **Supuestos:** [supuestos que deben confirmarse]
- **Dependencias:** [equipos, sistemas, proveedores, servicios]
- **Fuera de alcance:** [decisiones o áreas que esta arquitectura no cubre]

## 5. Estrategia de solución

[Estilo arquitectónico, principios, límites y razones principales.]

### Alternativas principales

| Alternativa | Ventajas | Costos / riesgos | Motivo de selección o descarte |
|---|---|---|---|
| [opción] | [ventajas] | [costos y riesgos] | [razón] |

## 6. Vista de bloques / contenedores

| Bloque o contenedor | Responsabilidad | Tecnología | Propietario de datos |
|---|---|---|---|
| [nombre] | [responsabilidad] | [tecnología] | [equipo o sistema] |

[Añadir diagrama C4 de contenedores y, si hace falta, diagramas de componentes.]

## 7. Flujos de ejecución

### Flujo: [nombre]

1. [Actor o sistema inicia la interacción.]
2. [Procesamiento y comunicación entre componentes.]
3. [Persistencia, respuesta y eventos relevantes.]
4. [Errores, reintentos, compensación o manejo alternativo.]

[Añadir diagrama de secuencia o actividad si facilita la revisión.]

## 8. Datos e integración

- **Datos principales y clasificación:** [categorías, sensibilidad y retención]
- **Sistemas de registro y propiedad:** [quién mantiene cada dato]
- **Persistencia y consistencia:** [almacenamiento, transacciones, consistencia]
- **Interfaces y contratos:** [API, eventos, archivos, protocolos; enlazar especificaciones]
- **Errores e idempotencia:** [timeouts, reintentos, deduplicación, reconciliación]

## 9. Seguridad y privacidad

- **Identidad y autenticación:** [usuarios y servicios]
- **Autorización y segregación:** [roles, permisos y límites de confianza]
- **Protección de datos:** [cifrado, clasificación, retención y privacidad]
- **Auditoría y trazabilidad:** [eventos que se registran y acceso a ellos]
- **Amenazas y mitigaciones:** [riesgos principales y controles]

## 10. Despliegue y operación

- **Ambientes:** [desarrollo, pruebas, producción y diferencias relevantes]
- **Topología y redes:** [nodos, zonas, conectividad y dependencias]
- **Entrega y configuración:** [proceso de despliegue, configuración y secretos]
- **Observabilidad:** [logs, métricas, trazas, alertas y responsables]
- **Resiliencia y recuperación:** [escalado, respaldos, RPO/RTO, continuidad]
- **Soporte:** [horarios, escalamiento y runbooks]

[Insertar diagrama de despliegue si es necesario.]

## 11. Decisiones arquitectónicas

| ID / enlace ADR | Decisión | Estado | Consecuencia principal |
|---|---|---|---|
| [ADR-NNN] | [decisión] | [propuesta/aceptada/reemplazada] | [impacto] |

## 12. Riesgos, deuda y preguntas abiertas

| Elemento | Impacto | Probabilidad / prioridad | Mitigación o responsable | Estado |
|---|---|---|---|---|
| [riesgo o pregunta] | [impacto] | [nivel] | [acción] | [estado] |

## 13. Validación y trazabilidad

| Requisito / escenario | Cómo se valida | Evidencia / responsable | Estado |
|---|---|---|---|
| [criterio] | [prueba, revisión o medición] | [enlace o rol] | [pendiente/hecho] |

## 14. Glosario

| Término | Definición |
|---|---|
| [término] | [definición] |

## Historial de cambios

| Fecha | Autor | Cambio |
|---|---|---|
| [AAAA-MM-DD] | [nombre/equipo] | [descripción] |
