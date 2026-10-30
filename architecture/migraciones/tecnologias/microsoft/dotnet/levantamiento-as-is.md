# Levantamiento AS-IS para migraciones .NET

El **levantamiento AS-IS** es la reconstrucción verificable del estado actual de un sistema antes de migrarlo: qué aplicaciones existen, cómo están construidas, de qué dependen, dónde se ejecutan, qué datos e integraciones usan y qué riesgos concentran. Es la línea base contra la que se planifica, estima y valida la migración de .NET Framework 4.x hacia .NET 6, 8 o 10.

No es un inventario de carpetas ni una lista de proyectos. Un AS-IS útil para migrar debe explicar la **arquitectura existente** y distinguir lo que se observa en el código de lo que realmente está desplegado en producción. El TO-BE —decisiones de arquitectura destino, contenedores, nube o rediseño funcional— se documenta aparte.

Este documento describe el método. La guía de migración, las fases, los riesgos y las validaciones están en [framework-a-dotnet.md](framework-a-dotnet.md); aquí solo se cubre cómo obtener y organizar la evidencia del estado actual.

## 1. Propósito y alcance

El levantamiento debe permitir responder, con evidencia:

- ¿Qué soluciones, proyectos, lenguajes y versiones de .NET Framework componen el sistema?
- ¿Qué dependencias externas, paquetes, ensamblados, componentes COM y APIs específicas de Windows se usan?
- ¿Qué tecnologías de aplicación están presentes: ASP.NET MVC, Web API, Web Forms, WCF, WPF, Windows Forms, WF, servicios Windows, procesos por lotes?
- ¿Cómo se compila, configura, despliega y opera cada componente?
- ¿Qué datos, integraciones, identidades, certificados y trabajos programados intervienen?
- ¿Qué elementos son compatibles, requieren ajustes, son reemplazables o bloquean la migración?
- ¿Qué información falta, qué supuestos se hicieron y con qué confianza?

**Incluye:** código fuente, soluciones y proyectos, construcción, dependencias, configuración, despliegue, runtime, datos, integraciones, seguridad y operación.

**Excluye:** decisiones de arquitectura destino, cambios funcionales, rediseño de experiencia, selección de nube y estimaciones definitivas. Esos temas pertenecen a las fases posteriores.

## 2. Principios

1. **Evidencia antes que supuestos.** Cada hallazgo debe indicar su fuente: archivo, configuración, servidor, entrevista o ejecución.
2. **Lectura primero.** El levantamiento no modifica código, configuraciones ni entornos. Cualquier cambio se hace en una rama y con autorización explícita.
3. **Reproducible.** Registrar fecha, commit, versión de scripts, parámetros y entorno para poder repetir el análisis.
4. **Trazable.** Vincular hallazgos, proyectos, riesgos y decisiones mediante identificadores estables.
5. **Sanitizado.** No incorporar secretos, cadenas de conexión reales, rutas personales ni datos de clientes en documentos compartidos.
6. **Con nivel de confianza.** Distinguir lo verificado en ejecución de lo inferido por análisis estático.
7. **Orientado a decisiones.** El inventario debe alimentar la clasificación de compatibilidad, el riesgo y la planificación por oleadas, no acumular datos sin uso.

## 3. Entradas y fuentes

| Fuente | Qué aporta | Prioridad |
|---|---|---|
| Repositorios y código fuente | Soluciones, proyectos, referencias, paquetes, configuraciones, recursos. | Alta |
| Historial y ramas | Versión desplegada, hotfixes, cambios recientes, deuda acumulada. | Alta |
| Pipelines y artefactos de compilación | Versión de MSBuild, SDK, pasos reales, artefactos publicados, pruebas. | Alta |
| Servidores e IIS | Sitios, aplicaciones, app pools, bindings, identidades, rutas, módulos. | Alta |
| Servicios Windows, tareas y jobs | Procesos en segundo plano, cuentas, frecuencias, dependencias. | Alta |
| Configuraciones desplegadas | `web.config`, `app.config`, transforms, variables, certificados. | Alta |
| Bases de datos y almacenamiento | Motores, esquemas, procedimientos, jobs, volumen y propietarios. | Media |
| Integraciones y contratos | Endpoints, colas, archivos, correo, servicios externos, versiones. | Media |
| Monitoreo y logs | Comportamiento real, errores, latencias, dependencias observadas. | Media |
| Documentación y ADR existentes | Decisiones previas, restricciones, arquitectura declarada. | Media |
| Entrevistas | Contexto de negocio, criticidad, excepciones, conocimiento tácito. | Alta |
| Inventario corporativo o CMDB | Hosts, propietarios, ambientes, contratos de soporte. | Media |

El orden recomendado es automatizar primero lo que está en repositorios y artefactos, y usar entrevistas y verificación en runtime para cubrir lo que el código no revela.

## 4. Modelo de inventario

### 4.1 Niveles

```
Organización
└── Sistema o aplicación de negocio
    ├── Solución (.sln)
    │   ├── Proyecto (.csproj, .vbproj, .fsproj)
    │   │   ├── Dependencias (NuGet, ensamblados, COM, nativas)
    │   │   ├── Uso de APIs y tecnologías
    │   │   └── Configuración y recursos
    │   └── Proyectos compartidos y bibliotecas
    ├── Despliegue (IIS, servicios, jobs, hosts)
    ├── Datos e integraciones
    └── Operación y seguridad
```

### 4.2 Entidades y campos mínimos

| Entidad | Campos mínimos |
|---|---|
| **Sistema** | Nombre, propósito, propietario técnico, propietario de negocio, criticidad, usuarios, ventanas de mantenimiento, ambientes. |
| **Solución** | Ruta, nombre, formato, proyectos incluidos, configuraciones de compilación, artefactos que produce. |
| **Proyecto** | Ruta, nombre, tipo, lenguaje, `TargetFramework`, estilo SDK o legado, tipo de salida, plataforma, firma, referencias, paquetes, recursos web. |
| **Dependencia** | Nombre, versión, origen (NuGet, ensamblado, COM, nativa), ámbito, licencia, uso, criticidad, alternativa conocida. |
| **Tecnología** | Framework de aplicación, APIs de Windows, ORM, acceso a datos, componentes de UI, reportes, mensajería, scripting. |
| **Configuración** | Archivos, transforms, claves relevantes, orígenes de valores, secretos referenciados (sin valor), dependencia del entorno. |
| **Despliegue** | Host, IIS o servicio, app pool, identidad, bindings, rutas, certificados, dependencias del sistema operativo, instalador. |
| **Datos** | Motor, base, esquema, ORM o proveedor, procedimientos, jobs, volumen aproximado, propietario. |
| **Integración** | Sistema externo, protocolo, contrato, dirección, autenticación, frecuencia, criticidad, versión. |
| **Hallazgo** | Identificador, categoría, evidencia, severidad, confianza, clasificación de compatibilidad, acción propuesta. |

### 4.3 Clasificación de compatibilidad

Cada proyecto, dependencia o tecnología debe clasificarse con los mismos criterios de la guía de migración:

- **Compatible:** funciona en el destino con cambios nulos o menores.
- **Compatible con ajustes:** requiere cambios identificados y estimables.
- **Reemplazable:** existe una alternativa viable con costo y riesgo conocidos.
- **Bloqueante:** no hay ruta aceptable sin rediseño, sustitución o permanencia temporal.

La clasificación debe incluir evidencia y confianza; no se acepta una categoría sin justificación.

### 4.4 Formato de proyecto y bibliotecas compartidas

El formato del proyecto y sus consumidores determinan si una biblioteca puede convertirse a SDK-style, apuntar a `netstandard2.0` o requerir multi-target.

| Campo | Valores | Detección |
|---|---|---|
| `projectFormat` | `sdk`, `legacy`, `unknown` | Atributo `Sdk` en `<Project>`, `TargetFramework(s)`, imports clásicos, `ToolsVersion`. |
| `targetFrameworks` | `net48`, `netstandard2.0`, `net8.0`, etc. | `TargetFramework`/`TargetFrameworks` o `TargetFrameworkVersion`. |
| `isSharedLibrary` | sí/no | Proyecto referenciado por más de un consumidor. |
| `consumers` | `framework`, `modern`, `both` | Grafo de dependencias e inventario de aplicaciones. |
| `canTargetNetStandard` | sí/no/desconocido | APIs usadas, tipo de biblioteca y soporte de paquetes. |
| `blockingApis` | lista | `System.Web`, WPF/WinForms, COM, P/Invoke y APIs específicas de Windows. |
| `packageSupport` | lista | Paquetes sin destino `netstandard2.0`. |

Reglas:

- Un proyecto que apunta a `netstandard2.0` normalmente requiere formato SDK-style; la conversión es un habilitador, no el objetivo.
- `netstandard2.0` aplica a bibliotecas, no a aplicaciones.
- Si hay consumidores Framework, `netstandard2.1` no es una opción.
- La decisión final se registra en la Fase 2 y se detalla en [.NET Standard 2.0 y multi-targeting](anexos/netstandard2.0-y-multitargeting.md).

## 5. Recolección automatizada

Los scripts de PowerShell previstos en [`scripts/`](scripts/README.md) automatizan el análisis estático y generan la evidencia primaria:

| Ámbito | Qué se obtiene |
|---|---|
| Soluciones y proyectos | Inventario de `.sln`, `.csproj`, `.vbproj`, `.fsproj`, `TargetFramework(s)`, `projectFormat`, salidas y referencias. |
| Bibliotecas compartidas | Consumidores Framework/.NET, APIs bloqueantes y paquetes sin soporte `netstandard2.0`. |
| Paquetes y ensamblados | `PackageReference`, `packages.config`, `Directory.Packages.props`, referencias directas, `project.assets.json` y binarios locales. |
| Dependencias | Grafo proyecto a proyecto, bibliotecas compartidas, ciclos y componentes externos. |
| Uso de APIs | Búsqueda de `System.Web`, WCF, Web Forms, WinForms, WPF, WF, Remoting, COM, P/Invoke, Registro, DTC y otras APIs específicas. |
| Formatos y configuración | `app.config`, `web.config`, transforms, `.resx`, `.settings`, `.edmx`, `.pubxml`, `NuGet.config`, `global.json`, `Directory.Build.*`. |
| Métricas | Cantidad de proyectos, líneas de código, dependencias, paquetes y distribución por tecnología. |

### 5.1 Secuencia de ejecución

1. **Congelar la línea base:** identificar commit, rama y artefacto desplegado; registrar fecha y responsable.
2. **Inventariar soluciones y proyectos:** obtener el mapa completo y detectar proyectos huérfanos o no compilables.
3. **Inventariar dependencias:** paquetes, ensamblados, COM y componentes nativos, con versiones y origen.
4. **Analizar uso de APIs y formatos:** localizar incompatibilidades y configuraciones relevantes.
5. **Construir el grafo:** visualizar dependencias y detectar ciclos, acoplamientos y componentes compartidos.
6. **Consolidar:** generar el JSON canónico, el CSV de revisión y el informe Markdown.
7. **Validar en runtime:** contrastar con IIS, servicios, tareas, certificados y configuraciones desplegadas.
8. **Revisar con los propietarios:** corregir vacíos, confirmar criticidad y priorizar hallazgos.
9. **Publicar el resumen sanitizado:** dejar la evidencia cruda en el espacio del proyecto y el resumen en la base de conocimiento.

### 5.2 Reglas de ejecución

- Los scripts son de **solo lectura** por defecto y no ejecutan `restore`, `build` ni publicaciones.
- Los resultados se escriben en un directorio `output/` fuera del control de versiones.
- Las rutas se registran de forma relativa cuando sea posible.
- El orden de los elementos debe ser determinista para permitir comparaciones entre ejecuciones.
- Los errores de acceso se registran como hallazgos, no se ignoran en silencio.
- No se leen credenciales ni se resuelven secretos; solo se anota su existencia y ubicación.

## 6. Verificación manual y en runtime

El análisis estático no ve todo. La validación manual cubre:

- **IIS:** sitios, aplicaciones virtuales, app pools, identidades, bindings, autenticación, módulos, handlers, rutas físicas y permisos.
- **Servicios y tareas:** servicios Windows, cuentas de ejecución, dependencias, tareas programadas, jobs de base de datos y procesos batch.
- **Configuración desplegada:** transforms aplicados, variables de entorno, secretos, certificados y diferencias entre ambientes.
- **Dependencias del sistema:** GAC, registro, COM registrado, componentes de 32/64 bits, fuentes, impresoras y rutas compartidas.
- **Integraciones:** conectividad real, contratos vigentes, colas, archivos, correo y servicios externos.
- **Datos:** motores, versiones, esquemas, procedimientos, volúmenes, retención y respaldo.
- **Operación:** logs, monitoreo, alertas, respaldos, recuperación y ventanas de cambio.
- **Negocio:** criticidad, usuarios, procesos estacionales, obligaciones regulatorias y tolerancia a interrupciones.

### 6.1 Nivel de confianza

| Nivel | Significado | Uso |
|---|---|---|
| **Alta** | Verificado en código, configuración desplegada o runtime. | Base para decisiones y estimaciones. |
| **Media** | Inferido por análisis estático o documentación, sin verificación en ejecución. | Requiere confirmación antes de comprometer fechas. |
| **Baja** | Supuesto, entrevista no contrastada o información incompleta. | Se registra como pregunta abierta. |

## 7. Vistas de arquitectura AS-IS

El inventario se convierte en arquitectura cuando se organiza en vistas. Para una migración se recomienda un conjunto mínimo:

| Vista | Contenido | Fuentes principales |
|---|---|---|
| **Contexto** | Sistemas, actores, integraciones externas y límites. | Entrevistas, contratos, diagramas existentes. |
| **Contenedores** | Aplicaciones web, servicios, jobs, bases, colas y almacenamiento. | IIS, servicios, repositorios, runtime. |
| **Componentes** | Proyectos, ensamblados, capas, bibliotecas compartidas y dependencias. | Soluciones, grafo, referencias. |
| **Datos** | Almacenes, propietarios, motores, esquemas y flujos. | Bases, ORM, procedimientos, integraciones. |
| **Despliegue** | Hosts, IIS, servicios, redes, certificados y ambientes. | Servidores, configuración, CMDB. |
| **Operación y seguridad** | Identidades, autenticación, logs, respaldo, monitoreo y controles. | Configuración, operación, seguridad. |

Las vistas deben seguir los criterios de la [guía central de arquitectura](../../../../architecture.md) para que el AS-IS y el TO-BE sean comparables. No hace falta diagramar cada detalle: basta con lo necesario para decidir y estimar.

## 8. Clasificación y priorización

Con la evidencia consolidada:

1. **Clasificar** cada proyecto y dependencia como compatible, con ajustes, reemplazable o bloqueante.
2. **Decidir por biblioteca compartida:** mantener el proyecto clásico, convertir a SDK-style, apuntar a `netstandard2.0` o usar multi-target, según consumidores, APIs y dependencias.
3. **Medir complejidad:** cantidad de proyectos, líneas de código, dependencias, APIs incompatibles, componentes COM/nativos y deuda técnica.
4. **Medir criticidad:** impacto de negocio, usuarios, ingresos, regulación y tolerancia a interrupciones.
5. **Identificar acoplamientos:** bibliotecas compartidas, bases compartidas, integraciones punto a punto y ciclos.
6. **Construir la matriz de riesgo:** probabilidad, impacto, evidencia y mitigación.
7. **Proponer oleadas:** primero componentes hoja y de bajo riesgo; después capas con dependencias; finalmente aplicaciones críticas o bloqueantes.
8. **Registrar preguntas abiertas:** todo lo que impide clasificar con confianza suficiente.

El resultado alimenta la Fase 1 de la [metodología de migración](framework-a-dotnet.md#7-metodología-por-fases) y la decisión de arquitectura de la Fase 2.

## 9. Formatos y artefactos

| Artefacto | Formato | Uso |
|---|---|---|
| Inventario canónico | JSON con `schemaVersion` | Fuente de verdad para informes y automatización. |
| Inventario de revisión | CSV | Filtros, tablas dinámicas y revisión con negocio. |
| Grafo de dependencias | Mermaid o DGML | Detectar ciclos, acoplamientos y orden de migración. |
| Informe AS-IS | Markdown | Documento de arquitectura y hallazgos. |
| Registro de riesgos | Markdown | Riesgos, evidencia, mitigación y responsable. |
| Métricas de línea base | JSON o CSV | Comparación antes y después de la migración. |

Convenciones:

- Nombrar los archivos como `<sistema>-<yyyyMMdd>-<artefacto>.<ext>`.
- Versionar el esquema y documentar los cambios en [`plantillas/`](plantillas/README.md).
- Mantener los datos crudos en `output/` (ignorado por Git) y publicar solo el resumen sanitizado.
- No incluir secretos, cadenas de conexión, rutas personales ni nombres de clientes en artefactos compartidos.

## 10. Criterios de calidad

- [ ] Todas las soluciones y proyectos del alcance están inventariados.
- [ ] Cada referencia y paquete tiene origen, versión y estado.
- [ ] Los proyectos no compilables o inaccesibles están registrados como hallazgos.
- [ ] Cada tecnología o API relevante tiene evidencia y clasificación.
- [ ] El despliegue declarado se contrastó con el observado.
- [ ] Los datos e integraciones críticos están identificados.
- [ ] Los riesgos y bloqueantes tienen evidencia, impacto y mitigación.
- [ ] Las preguntas abiertas tienen responsable y fecha objetivo.
- [ ] El informe distingue hechos, inferencias y supuestos.
- [ ] El resumen publicado está sanitizado.

## 11. Riesgos y limitaciones del levantamiento

- **Análisis estático incompleto:** reflexión, carga dinámica, generación de código, SQL dinámico y dependencias resueltas en runtime pueden no detectarse.
- **Código distinto de producción:** hotfixes manuales, ramas no mergeadas y configuraciones cambiadas en servidores pueden invalidar conclusiones.
- **Accesos restringidos:** repositorios, servidores, bases o documentación inaccesibles dejan vacíos que deben declararse.
- **Falsos positivos y negativos:** una referencia encontrada no siempre se usa; una dependencia no encontrada puede existir.
- **Datos de licencias:** sin acceso a la fuente de paquetes, la licencia y el estado de soporte pueden quedar sin verificar.
- **Volumen y tiempo:** repositorios grandes requieren muestreo, priorización y ejecución por oleadas.
- **Exposición de información:** inventarios y configuraciones pueden contener datos sensibles si no se sanitizan.
- **Deriva:** el inventario caduca; debe repetirse antes de cada decisión importante y al cerrar la migración.

Mitigaciones: combinar análisis estático con runtime, revisar con propietarios, registrar confianza, automatizar la repetición y separar evidencia cruda de resúmenes.

## 12. Entregables

1. Inventario de soluciones, proyectos y dependencias en JSON y CSV.
2. Grafo de dependencias y lista de ciclos o acoplamientos relevantes.
3. Informe AS-IS con las vistas de contexto, contenedores, componentes, datos y despliegue.
4. Registro de riesgos, bloqueantes y preguntas abiertas.
5. Métricas de línea base: cantidad de proyectos, dependencias, APIs incompatibles y cobertura.
6. Clasificación de compatibilidad y propuesta inicial de oleadas.
7. Resumen sanitizado para la base de conocimiento y evidencia cruda en el espacio del proyecto.

## 13. Integración con la migración

| Fase de la guía | Uso del AS-IS |
|---|---|
| Fase 0: mandato y alcance | Delimitar sistemas, equipos y responsabilidades. |
| Fase 1: descubrimiento y evaluación | Inventario, línea base, clasificación y riesgos. |
| Fase 2: decisión y plan | Priorización, oleadas, estimación y ADR. |
| Fase 3: preparación | Identificar dependencias críticas y pruebas necesarias. |
| Fases 4 a 7 | Verificar que el alcance migrado coincide con lo inventariado y actualizar el AS-IS. |

El AS-IS no se congela para siempre: se actualiza cuando cambia el alcance, se descubre un componente oculto o se modifica el sistema durante la migración.

## 14. Referencias internas

- [Migraciones .NET](README.md)
- [Guía de migración de .NET Framework a .NET moderno](framework-a-dotnet.md)
- [Scripts de levantamiento](scripts/README.md)
- [Plantillas y esquemas](plantillas/README.md)
- [Ejemplos y fixtures](ejemplos/README.md)
- [Anexos de migración](anexos/README.md)
- [Conversión de proyectos a SDK-style](anexos/conversion-a-sdk-style.md)
- [.NET Standard 2.0 y multi-targeting](anexos/netstandard2.0-y-multitargeting.md)
- [Herramientas y validación](anexos/herramientas-y-validacion.md)
- [Casos de migración](../../../casos/README.md)
- [Guía central de arquitectura](../../../../architecture.md)
- [Documentación de decisiones](../../../../decisiones-y-documentacion/README.md)

## 15. Referencias externas

- [.NET Portability Analyzer](https://learn.microsoft.com/dotnet/standard/analyzers/portability-analyzer)
- [Documentación de MSBuild](https://learn.microsoft.com/visualstudio/msbuild/msbuild)
- [Referencia de NuGet](https://learn.microsoft.com/nuget/)
- [Pester: framework de pruebas para PowerShell](https://pester.dev/)
- [PowerShell: documentación](https://learn.microsoft.com/powershell/)
- [.NET: guía de portabilidad](https://learn.microsoft.com/dotnet/core/porting/)
