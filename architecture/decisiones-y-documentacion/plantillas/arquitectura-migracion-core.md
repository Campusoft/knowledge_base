# Plantilla de arquitectura para migrar un core empresarial

> Plantilla especializada que complementa [arquitectura-software.md](arquitectura-software.md). Completarla por iniciativa y por ola cuando el alcance sea grande. Acordar con negocio, operación, seguridad y datos los criterios de continuidad antes de planificar el corte.

## 1. Identificación y gobierno

- **Core / iniciativa:** [nombre]
- **Documento / versión / estado:** [identificador, versión, estado]
- **Patrocinador y responsable de arquitectura:** [roles]
- **Responsables de negocio, datos, seguridad y operación:** [roles/equipos]
- **Fecha objetivo y ventana de cambio:** [fecha / ventana]
- **Aprobaciones requeridas:** [órganos o responsables]
- **Referencias:** [arquitectura general, requisitos, contratos, planes]

## 2. Motivo, objetivos y alcance

- **Motivación:** [riesgo, obsolescencia, costo, capacidad de negocio]
- **Resultados esperados:** [beneficios medibles]
- **Capacidades y procesos incluidos:** [alcance]
- **Exclusiones y límites:** [fuera de alcance]
- **Criterios de éxito:** [medidas y umbrales verificables]

## 3. Contexto de negocio y criticidad

| Capacidad / proceso | Criticidad | Horario / calendario | Impacto de interrupción | Dueño de negocio |
|---|---|---|---|---|
| [proceso] | [nivel] | [picos, cierres, temporadas] | [impacto] | [rol] |

Documentar cierres financieros, liquidaciones, nómina, facturación, conciliaciones y otros ciclos que condicionen la migración.

## 4. Arquitectura actual (AS-IS)

### Inventario

| Elemento | Función / dominio | Tecnología y versión | Ubicación | Responsable | Criticidad / fin de soporte |
|---|---|---|---|---|---|
| [módulo, servicio, job, base de datos] | [función] | [tecnología] | [ubicación] | [equipo] | [nivel / fecha] |

### Interfaces y dependencias

| Origen | Destino | Contrato / protocolo | Frecuencia / volumen | Acoplamiento y riesgo | Responsable |
|---|---|---|---|---|---|
| [sistema] | [sistema] | [API, archivo, evento, batch] | [patrón] | [riesgo] | [equipo] |

### Datos, procesos y operación actuales

- **Sistemas de registro y propietarios:** [dominio y datos]
- **Volúmenes, crecimiento y retención:** [datos observados]
- **Procesos batch y cierres:** [dependencias, secuencia y ventana]
- **Flujos críticos y reglas implícitas:** [reglas conocidas o por descubrir]
- **Incidentes, limitaciones y deuda técnica:** [evidencia]
- **Diagramas:** [contexto, contenedores, despliegue, flujos]

## 5. Requisitos de calidad y continuidad

| Requisito | Situación actual | Objetivo de destino | Método de medición / aceptación |
|---|---|---|---|
| Disponibilidad | [valor] | [valor] | [método] |
| Rendimiento / capacidad | [p95, TPS, lotes] | [umbral] | [prueba] |
| RPO / pérdida tolerable | [valor] | [valor] | [simulación/evidencia] |
| RTO / recuperación | [valor] | [valor] | [ejercicio] |
| Integridad / conciliación | [controles] | [criterio] | [reconciliación] |
| Seguridad / auditoría | [controles] | [criterio] | [validación] |

## 6. Estrategia de migración

| Dominio / componente | Estrategia | Justificación | Prerrequisitos | Dependencias |
|---|---|---|---|---|
| [elemento] | [retirar, conservar, mover, reemplazar, replatformar, refactorizar u otra] | [razón] | [condiciones] | [elementos] |

Explicar si la estrategia es por capacidad de negocio, módulo, interfaz, dato, región o etapa. No asumir que todos los componentes del core seguirán la misma ruta.

### Principios y decisiones

- [por ejemplo: preservar trazabilidad, migrar dominios con conciliación verificable]
- [decisiones y enlaces ADR]
- [alternativas consideradas y motivos de descarte]

## 7. Arquitectura objetivo (TO-BE)

- **Capacidades y límites de dominio:** [responsabilidades y propiedad]
- **Componentes y contratos:** [servicios, aplicaciones, integraciones]
- **Datos y sistemas de registro:** [propiedad y persistencia]
- **Seguridad y auditoría:** [controles]
- **Plataforma y despliegue:** [ambientes y topología]
- **Observabilidad y soporte:** [métricas, alertas, responsables]
- **Diagramas:** [C4 contexto/contenedores, secuencias, despliegue, datos]

## 8. Arquitectura de transición y coexistencia

Describir los estados intermedios entre AS-IS y TO-BE, incluidos los límites de quién procesa cada transacción y dónde reside la verdad de cada dato.

| Etapa | Sistemas activos | Datos maestros | Flujo de transacciones | Condición para avanzar |
|---|---|---|---|---|
| [etapa] | [sistemas] | [fuente autoritativa] | [flujo] | [criterio] |

- **Enrutamiento y partición del tráfico:** [reglas, llaves, segmentos]
- **Sincronización temporal:** [mecanismo, latencia, orden, reintentos]
- **Prevención de doble escritura o doble procesamiento:** [controles]
- **Reconciliación durante coexistencia:** [frecuencia, diferencias, dueño]
- **Límites y fecha de fin de coexistencia:** [criterio]

## 9. Migración y validación de datos

| Conjunto de datos | Fuente / destino | Volumen | Transformaciones | Estrategia de carga | Validación y dueño |
|---|---|---|---|---|---|
| [entidad] | [sistema] | [tamaño] | [reglas] | [inicial/incremental] | [conteo, suma, muestreo] |

- **Perfilado y calidad:** [duplicados, nulos, referencias, codificaciones]
- **Mapeo y transformación:** [reglas versionadas y aprobadas]
- **Carga histórica y delta:** [herramienta, orden, ventanas]
- **Integridad referencial y saldos:** [controles y tolerancias]
- **Conciliación funcional:** [reportes, totales, casos de negocio]
- **Retención, archivo y acceso al legado:** [plazos y responsables]
- **Evidencia y aprobación de datos:** [quién valida y cómo]

## 10. Plan por olas y secuencia

| Ola | Alcance / dominio | Fecha tentativa | Prerrequisitos | Riesgo | Criterios de entrada y salida |
|---|---|---|---|---|---|
| [piloto/ola] | [alcance] | [fecha] | [condiciones] | [nivel] | [criterios] |

Incluir piloto, grupos de usuarios o transacciones, periodos de observación y dependencias entre olas.

## 11. Pruebas y aceptación

- **Funcionales y regresión:** [procesos y reglas críticas]
- **Integración y contratos:** [sistemas y escenarios]
- **Migración y reconciliación:** [volúmenes, totales, tolerancias]
- **Rendimiento y capacidad:** [picos, concurrencia, batch]
- **Seguridad y auditoría:** [controles y evidencias]
- **Resiliencia y recuperación:** [fallos, RPO/RTO]
- **Paralelo / shadow mode:** [duración, comparación, límites]

| Prueba / escenario | Criterio de aceptación | Evidencia | Responsable | Estado |
|---|---|---|---|---|
| [caso] | [umbral] | [enlace] | [rol] | [pendiente/hecho] |

## 12. Corte, reversa y continuidad

### Runbook de corte

1. [Congelar cambios o establecer punto de consistencia.]
2. [Ejecutar delta final y reconciliación.]
3. [Validar salud, saldos, colas, trabajos y accesos.]
4. [Decisión go/no-go y responsables.]
5. [Habilitar tráfico gradualmente y vigilar indicadores.]

- **Duración prevista y ventana:** [tiempo]
- **Go/no-go:** [umbrales y autoridad de decisión]
- **Plan de reversa:** [pasos, límite temporal y roles]
- **Datos generados después del corte:** [cómo conservar consistencia al revertir]
- **Comunicaciones y soporte reforzado:** [audiencia, canal, guardias]
- **Condiciones para declarar estabilidad:** [periodo e indicadores]

## 13. Seguridad, cumplimiento y auditoría

- **Clasificación y residencia de datos:** [requisitos]
- **Identidad, acceso privilegiado y segregación:** [controles]
- **Cifrado, secretos y llaves:** [gestión]
- **Trazabilidad de cambios y transacciones:** [evidencia]
- **Requisitos regulatorios y aprobaciones:** [referencias y responsables]

## 14. Operación y retiro del legado

- **Monitoreo, alertas y tableros:** [indicadores y propietarios]
- **Soporte, escalamiento y runbooks:** [equipos]
- **Backups, recuperación y continuidad:** [procedimientos]
- **Costos y capacidad:** [seguimiento]
- **Criterios y fecha de retiro:** [dependencias, retención y aprobación]
- **Desmantelamiento seguro:** [accesos, datos, infraestructura, contratos]

## 15. Riesgos, decisiones y pendientes

| Riesgo / decisión / pendiente | Impacto | Mitigación o acción | Responsable | Fecha / estado |
|---|---|---|---|---|
| [elemento] | [impacto] | [acción] | [rol] | [fecha/estado] |

## 16. Aprobaciones e historial

| Área | Responsable | Aprobación / fecha | Observaciones |
|---|---|---|---|
| Negocio | [rol] | [estado] | [comentario] |
| Arquitectura | [rol] | [estado] | [comentario] |
| Datos / seguridad / operación | [rol] | [estado] | [comentario] |

| Fecha | Autor | Cambio |
|---|---|---|
| [AAAA-MM-DD] | [nombre/equipo] | [descripción] |
