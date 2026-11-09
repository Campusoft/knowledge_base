# Semgrep para SAST en .NET y C#

> **Propósito:** describir cómo incorporar Semgrep al análisis estático de seguridad de aplicaciones .NET, desde el uso local y la configuración de reglas hasta su ejecución en CI.

## 1. Resumen

[Semgrep](https://semgrep.dev/docs/) es una herramienta de análisis estático basada en reglas que analiza el código fuente sin ejecutar la aplicación. Reconoce estructuras sintácticas del lenguaje y busca patrones de código, y según el motor, el lenguaje y el plan puede analizar flujos de datos (*taint analysis*) entre fuentes no confiables (*sources*) y operaciones sensibles (*sinks*).

Para C# puede identificar patrones relacionados con inyección SQL, LDAP, uso inseguro de criptografía, validaciones y otras prácticas riesgosas. Semgrep es un componente SAST: no reemplaza el análisis de dependencias (SCA), el escaneo de secretos, DAST, revisión de código ni análisis de riesgos de diseño.

Para el concepto, alcance y ubicación de SAST en el ciclo de desarrollo, consulta [SAST: análisis estático de seguridad](../../../architecture/seguridad/seguridad-aplicativa/sast.md).

## 2. Características

- **Reglas legibles:** describen patrones de código en YAML mediante sintaxis cercana al lenguaje objetivo.
- **Análisis sintáctico:** los patrones reconocen estructuras del programa y metavariables, no dependen únicamente de buscar texto literal.
- **Reglas listas para usar:** hay reglas y conjuntos mantenidos por Semgrep y la comunidad en el Registry.
- **Reglas propias:** se pueden escribir reglas para requisitos de seguridad o convenciones internas.
- **Uso local y automatizado:** permite ejecutar escaneos desde CLI y en pipelines; la plataforma añade gestión centralizada de políticas y hallazgos.
- **Hallazgos accionables:** informa regla, archivo, ubicación, severidad y mensaje; el detalle del flujo de datos varía por motor y lenguaje.
- **Cobertura dependiente de edición y versión:** soporte de sintaxis C#, profundidad de análisis y reglas disponibles pueden diferir entre Semgrep Community Edition (CE) y Semgrep Code/AppSec Platform. Confirma la matriz vigente para la versión y licencia adoptadas.

La documentación actual de Semgrep distingue para C# soporte comunitario de CE y soporte de Semgrep Code con análisis interarchivo y versiones de lenguaje más recientes. Por ello, valida explícitamente la versión de C# del proyecto y las capacidades que se esperan antes de basar una política de bloqueo en el análisis.

## 3. Arquitectura del análisis

```text
Repositorio C#
    │
    ├── Código fuente y configuración (.semgrep.yml, .semgrepignore)
    │
    ▼
CLI / motor Semgrep
    ├── Parser del lenguaje → estructura sintáctica
    ├── Reglas → patrones, condiciones y metadatos
    ├── Motor de análisis → coincidencias y, según edición, flujo de datos
    └── Exclusiones y supresiones configuradas
    │
    ▼
Hallazgos → consola / JSON / SARIF / plataforma o sistema CI
    │
    ▼
Triage → corrección, excepción justificada o seguimiento
```

### Componentes

| Componente | Función |
|---|---|
| **CLI** | Ejecuta los escaneos localmente o desde un agente de CI. |
| **Parser** | Interpreta el código de acuerdo con el lenguaje y la versión soportada. |
| **Reglas** | Definen qué patrones o flujos se consideran hallazgos, con identificador, mensaje, severidad y metadatos. |
| **Configuración de escaneo** | Elige reglas o rulesets, alcance, exclusiones y salida. Puede mantenerse en el repositorio. |
| **Motor de análisis** | Evalúa patrones y, si está disponible, análisis semántico y de flujo de datos con mayor alcance. |
| **Integración CI/plataforma** | Ejecuta las reglas acordadas, conserva o publica resultados y aplica políticas de aceptación. |
| **Triage** | Revisa validez y contexto, asigna prioridad y controla excepciones. |

En una configuración local, el repositorio puede contener el archivo de configuración y reglas personalizadas. En un uso conectado a la plataforma, la selección de reglas y políticas puede administrarse centralmente. Conviene decidir cuál es la fuente de verdad para que el escaneo local y el de CI sean comparables.

## 4. Instalación y primer escaneo

Instala Semgrep siguiendo la [guía oficial de instalación](https://semgrep.dev/docs/getting-started/). Selecciona una versión aprobada y reproducible para los equipos y agentes de CI; no presupongas que el comando está preinstalado.

Escaneo explícito con el conjunto para C# del Registry:

```bash
semgrep scan --config p/csharp
```

Escaneo con un conjunto general:

```bash
semgrep scan --config p/default
```

`p/csharp` y `p/default` son configuraciones mantenidas en el Registry; revisa reglas, licencias y cobertura antes de adoptarlas. Fija las configuraciones y revisa cambios relevantes de reglas como parte del gobierno de seguridad.

Para explorar una regla en un directorio específico, usa la ruta del repositorio como objetivo (por ejemplo, `src/`). Consulta `semgrep scan --help` para las opciones vigentes de alcance, exclusiones y formatos de salida.

## 5. Reglas personalizadas

Un archivo de reglas Semgrep suele ser YAML. Cada regla define un `id` estable, `languages`, `message`, `severity`, metadatos y uno o más patrones. Un ejemplo didáctico que marca concatenación de SQL directamente en una llamada a Dapper:

```yaml
rules:
  - id: dotnet-sql-interpolation-dapper
    languages:
      - csharp
    message: >-
      Revisa esta consulta SQL interpolada. Usa parámetros de consulta para
      separar datos y sintaxis SQL, y verifica que el valor no controle la
      estructura de la consulta.
    severity: WARNING
    metadata:
      category: security
      cwe: "CWE-89: Improper Neutralization of Special Elements used in an SQL Command"
      technology:
        - .net
    pattern: connection.Query($"SELECT * FROM $TABLE WHERE Id = { $ID }")
```

El patrón es intencionalmente ilustrativo: debe probarse contra la versión concreta del parser C# y adaptarse a las APIs y convenciones del proyecto. Una regla de patrón por sí sola no prueba explotabilidad; puede marcar usos seguros o no detectar formas equivalentes. Para una regla de producción, cubre variantes relevantes, añade ejemplos positivos y negativos, y prefiere reglas oficiales auditadas para vulnerabilidades comunes.

Ejecutar una regla o archivo local:

```bash
semgrep scan --config .semgrep.yml --error src/
```

`--error` hace que los hallazgos provoquen un código de salida no exitoso en el uso compatible. Confirma el comportamiento con la versión fijada y decide qué severidades deben bloquear el pipeline.

### Pruebas de reglas

Mantén fixtures mínimos con código que **debe** coincidir y código que **no debe** coincidir. Semgrep permite probar reglas mediante comentarios de prueba y `semgrep --test`; consulta la documentación vigente de [testing rules](https://semgrep.dev/docs/writing-rules/testing-rules/). La revisión de reglas debe comprobar precisión, alcance, severidad, mensaje y comportamiento con código idiomático del repositorio.

## 6. Integración en CI/CD

Una canalización típica restaura y compila el proyecto según corresponda, ejecuta SAST contra el cambio y publica los resultados. El escaneo de pull request ofrece feedback temprano; el análisis periódico o de la rama principal permite revisar la base completa.

Ejemplo conceptual para una integración propia con el CLI:

```yaml
# Pseudoconfiguración: adapta a Azure Pipelines, GitHub Actions,
# GitLab CI u otro orquestador y a la instalación aprobada de Semgrep.
- script: semgrep scan --config p/csharp --error --json --output semgrep.json src/
  displayName: Ejecutar SAST de C#
```

Adapta la publicación de `semgrep.json` al formato consumido por el proveedor y protege los artefactos si contienen fragmentos de código. Para Semgrep AppSec Platform, `semgrep ci` puede utilizar las políticas configuradas en la organización; un `semgrep scan --config ...` local no necesariamente ejecuta la misma selección de reglas. Alinea ambas configuraciones para que el desarrollador pueda reproducir los hallazgos de CI.

### Política incremental

1. Ejecuta un análisis de línea base en toda la solución y confirma qué proyectos y versiones de C# quedaron cubiertos.
2. Clasifica hallazgos existentes y registra excepciones con motivo, responsable y fecha de revisión.
3. En pull requests, bloquea nuevos hallazgos acordados de severidad alta/crítica o exige una decisión de seguridad.
4. En la rama principal, mantén un escaneo completo y seguimiento de hallazgos heredados.
5. Revisa cambios de versión, reglas y configuración para evitar aumentos silenciosos de ruido o pérdida de cobertura.

## 7. Casos de uso .NET

### API ASP.NET Core

Buscar consultas dinámicas vulnerables, redirecciones inseguras, validación deficiente, uso riesgoso de criptografía y patrones de autorización incorrectos. Las reglas de autorización requieren contexto: una coincidencia de sintaxis no sustituye una revisión del modelo de identidad y permisos.

### Acceso a datos

Detectar concatenación o interpolación de entrada en SQL, LDAP o comandos. Revisar las rutas de datos de extremo a extremo y priorizar consultas parametrizadas y APIs seguras.

### Servicios financieros

Definir controles internos para APIs sensibles, algoritmos criptográficos permitidos, almacenamiento de credenciales, registro de datos sensibles y llamadas a servicios de pago. Las reglas propias deben validarse con responsables de seguridad y cumplimiento; no deben codificar políticas ambiguas ni suponer que un patrón sintáctico representa todo el riesgo.

### Bibliotecas y monorepos

Ejecutar sobre cada proyecto o conjunto de rutas con configuración común, y añadir reglas específicas cuando módulos o productos tengan riesgo distinto. Registrar los alcances para evitar que carpetas excluidas queden fuera de todo escaneo.

## 8. Buenas prácticas

- Mantén configuración y reglas personalizadas bajo control de versiones junto al código que gobiernan.
- Usa reglas oficiales como punto de partida; evalúa licencia, mantenimiento, severidad y aplicabilidad antes de habilitarlas.
- Fija la versión de Semgrep y controla actualizaciones del CLI y rulesets.
- Comienza con modo informativo o con una línea base; luego aplica bloqueo a un conjunto pequeño y confiable de reglas.
- Prioriza vulnerabilidades nuevas en pull requests y gestiona la deuda heredada por separado.
- Da a cada regla propia un ID estable, mensaje accionable, severidad justificada, CWE cuando aplique y fixtures.
- Documenta cada excepción, limita su alcance y establece revisión/expiración; no ignores directorios enteros por comodidad.
- Controla resultados duplicados y ruido sin desactivar reglas globalmente.
- Comprueba cobertura de todas las aplicaciones y proyectos C# del repositorio y la versión de lenguaje soportada por el motor usado.
- Protege tokens y reportes; usa secretos del sistema CI y permisos mínimos.
- Usa escaneo local para feedback rápido y CI central para garantizar cumplimiento consistente.
- Combina SAST con SCA, análisis de secretos, pruebas dinámicas, revisión de diseño, code review y pruebas de seguridad.

## 9. Limitaciones y lectura de resultados

- **No es una prueba de seguridad completa:** ausencia de hallazgos no demuestra ausencia de vulnerabilidades.
- **Cobertura variable en C#:** los límites de sintaxis y profundidad cambian según motor, edición y versión. CE tiene capacidades y soporte de versiones C# distintos de Semgrep Code.
- **Falsos positivos y negativos:** revisa si el flujo es alcanzable, si la entrada es controlable por un atacante y qué mitigaciones existen.
- **Reglas sintácticas vs. flujo de datos:** patrones simples son transparentes, pero una vulnerabilidad puede requerir seguir el dato a través de métodos, archivos o frameworks.
- **Sin contexto de ejecución o negocio:** Semgrep no puede, por sí solo, determinar permisos efectivos, configuración desplegada o impacto en una operación financiera.
- **Licencia y reglas:** verifica licencias y condiciones del producto para rulesets y funciones seleccionadas.

Interpreta cada hallazgo según severidad, confianza, exposición, posibilidad de explotación e impacto. Registra la decisión y la corrección para que hallazgos similares se traten de manera consistente.

## 10. Ejercicios

### Ejercicio 1: ejecutar reglas oficiales

Ejecuta `p/csharp` en una API de ejemplo, guarda la salida y separa hallazgos válidos, falsos positivos y posibles brechas de cobertura. Anota la versión de Semgrep y del target framework.

### Ejercicio 2: regla de SQL

Escribe una regla que identifique un patrón inseguro de consulta dinámica. Crea un fixture positivo con concatenación y uno negativo con consulta parametrizada. Ejecuta `semgrep --test` y ajusta el patrón para reducir falsos positivos.

### Ejercicio 3: política de pull request

Integra el escaneo en CI. Define qué hallazgos hacen fallar la revisión, publica un reporte legible y prueba deliberadamente un caso que deba bloquear.

### Ejercicio 4: deuda heredada

Genera una línea base, elige un hallazgo de alto riesgo, documenta una excepción acotada si no puede corregirse inmediatamente y establece cómo se evitarán nuevas instancias.

### Ejercicio 5: cobertura y versiones

Comprueba si el escaneo examina todos los proyectos C# y la sintaxis usada. Compara los resultados de la edición y motor disponibles con una muestra revisada manualmente, y documenta limitaciones antes de usarlo como gate obligatorio.

## 11. Documentación relacionada

- [SAST: análisis estático de seguridad](../../../architecture/seguridad/seguridad-aplicativa/sast.md): conceptos, flujo de adopción y comparación con otras herramientas.
- [Análisis estático en .NET](analisis-estatico.md): análisis estático general y herramientas .NET.
- [Convenciones de C#](convenciones-csharp.md): reglas y convenciones de código.
- [EditorConfig](editorconfig.md): convenciones de formato y estilo.
- [ArchUnitNET](archunitnet.md): pruebas de reglas de arquitectura.
- [`dotnet format`](dotnet-format.md): formato y reglas de estilo/análisis.
- [DevSecOps](../../../Operation/DevSecOps.md): integración de controles de seguridad en el ciclo de entrega.

## 12. Referencias oficiales

- [Semgrep: soporte para C#](https://semgrep.dev/docs/languages/csharp)
- [Semgrep: CLI](https://semgrep.dev/docs/cli-reference)
- [Semgrep: sintaxis de reglas](https://semgrep.dev/docs/writing-rules/rule-syntax)
- [Semgrep: sintaxis de patrones](https://semgrep.dev/docs/writing-rules/pattern-syntax)
- [Semgrep: pruebas de reglas](https://semgrep.dev/docs/writing-rules/testing-rules/)
- [Semgrep CE frente a Semgrep Code](https://semgrep.dev/docs/semgrep-ce-vs-semgrep-code)
