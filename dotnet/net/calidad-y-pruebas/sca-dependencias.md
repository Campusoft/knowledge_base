# SCA de dependencias en .NET

> **Propósito:** detectar y gestionar vulnerabilidades conocidas en paquetes y componentes de una solución .NET con NuGet Audit y OWASP Dependency-Check, desde el trabajo local hasta CI/CD.

## 1. Qué es SCA

**Software Composition Analysis (SCA)** identifica componentes de terceros presentes en una aplicación y los evalúa frente a información de vulnerabilidades conocidas, avisos de seguridad, licencias y, según la herramienta, otros riesgos de la cadena de suministro.

En .NET, NuGet resuelve dependencias directas y transitivas. La evaluación de seguridad debe tener en cuenta el grafo resuelto, no solo los `PackageReference` escritos directamente en los `.csproj`. Una dependencia transitiva también puede introducir una versión vulnerable.

SCA se complementa con SAST —análisis del código propio—, escaneo de secretos, análisis de contenedores, revisión de licencias, SBOM y DAST. Un resultado sin vulnerabilidades conocidas no demuestra que el componente sea seguro ni que no existan vulnerabilidades aún no publicadas.

Para el contexto general de controles y su ubicación en el proceso de entrega, consulta [DevSecOps](../../../Operation/DevSecOps.md#sca-y-dependencias). Para conceptos generales de SAST, consulta [SAST](../../../architecture/seguridad/seguridad-aplicativa/sast.md).

## 2. Herramientas cubiertas

| Herramienta | Mecanismo | Papel recomendado en .NET |
|---|---|---|
| **NuGet Audit / `dotnet list package --vulnerable`** | NuGet consulta fuentes de auditoría de vulnerabilidades durante `restore` y permite listar paquetes vulnerables con la CLI. Informa advisories asociados a paquetes NuGet y produce warnings `NU1901`–`NU1904`. | Control integrado principal para proyectos que usan NuGet; activar en restore/build y revisar también dependencias transitivas. |
| **OWASP Dependency-Check** | Analizadores inspeccionan manifiestos, proyectos, paquetes o ensamblados, identifican componentes y correlacionan identificadores (en particular CPE) con vulnerabilidades publicadas, como CVE. | Complemento para inventario y análisis de componentes heterogéneos, directorios o artefactos; verificar cuidadosamente el alcance real y precisión para NuGet moderno. |

No son intercambiables. El control nativo de NuGet conoce el grafo de paquetes que NuGet resolvió y utiliza el protocolo de avisos de NuGet. Dependency-Check ofrece analizadores y reportes de distintos ecosistemas y puede inspeccionar más tipos de artefactos, pero la identificación por CPE puede producir falsos positivos o no resolver una identidad de paquete con precisión.

## 3. NuGet Audit y `dotnet list package --vulnerable`

### 3.1 Mecanismo

Desde .NET SDK 8, `dotnet restore` ejecuta NuGet Audit por defecto y puede emitir warnings de vulnerabilidad con identificador del paquete, severidad y enlace al advisory. NuGet obtiene datos desde las fuentes de auditoría configuradas; si no se define una fuente de auditoría por separado, se usan los orígenes de paquetes aplicables.

`dotnet list package --vulnerable` está disponible desde .NET SDK 5.0.200 y filtra los paquetes con vulnerabilidades conocidas. Para incluir transitivas usa `--include-transitive`. Desde .NET SDK 10 se prefiere el orden `dotnet package list`; la forma `dotnet list package` se conserva para SDK 9 y anteriores.

### 3.2 Uso local

Restaura la solución y revisa los avisos:

```bash
dotnet restore ./Product.sln
```

Lista paquetes vulnerables, incluyendo los transitivos:

```bash
# .NET SDK 9 y anteriores
dotnet list ./Product.sln package --vulnerable --include-transitive

# .NET SDK 10 y posteriores
dotnet package list --project ./Product.sln --vulnerable --include-transitive
```

Genera salida JSON para automatizar reportes o procesamiento posterior:

```bash
# Sintaxis compatible con versiones que soportan --format json
dotnet list ./Product.sln package --vulnerable --include-transitive --format json --output-version 1 > dependency-report.json
```

`--format json` está disponible desde SDK 7.0.200; el formato/versionado y el comportamiento de restore varían según SDK. SDK 10 restaura automáticamente cuando hace falta; en SDK anteriores asegúrate de que `project.assets.json` exista ejecutando restore. Consulta `dotnet package list --help` con el SDK fijado en el repositorio.

Para conocer por qué se incluyó un paquete transitivo, revisa el árbol de dependencias con `dotnet nuget why <proyecto> <paquete>` (disponible desde .NET SDK 8.0.4xx) y actualiza preferentemente el paquete directo más cercano que introduce esa dependencia. Si no existe una versión corregida, evalúa alternativas, mitigaciones y aceptación formal del riesgo.

### 3.3 Configuración de auditoría

NuGet Audit puede controlarse con propiedades MSBuild. Ejemplo de configuración explícita en `Directory.Build.props`:

```xml
<Project>
  <PropertyGroup>
    <NuGetAudit>true</NuGetAudit>
    <NuGetAuditMode>all</NuGetAuditMode>
    <NuGetAuditLevel>low</NuGetAuditLevel>
  </PropertyGroup>
</Project>
```

- `NuGetAudit`: habilita o deshabilita auditoría.
- `NuGetAuditMode`: `direct` o `all`. Para proyectos que targetean .NET 10+, `all` es el valor predeterminado; para proyectos con targets anteriores, el valor predeterminado puede ser `direct` según SDK/NuGet. Establécelo explícitamente para que la política no dependa accidentalmente del target.
- `NuGetAuditLevel`: nivel mínimo reportado (`low`, `moderate`, `high`, `critical`). El valor exacto debe alinearse con la versión de NuGet.
- `AuditSources` en `nuget.config`: permite establecer fuentes de avisos específicas, disponible en versiones actuales de NuGet/.NET SDK. Manténlas accesibles desde las redes de desarrolladores y CI.

No desactives `NuGetAudit` para evitar warnings. Si una advertencia concreta se considera no aplicable, documenta el análisis y usa la supresión específica que NuGet ofrece (`NuGetAuditSuppress`) donde esté soportada, con revisión y alcance controlados.

### 3.4 Gestión de warnings en CI

Los advisories pueden emitir NU1901 (baja), NU1902 (moderada), NU1903 (alta) y NU1904 (crítica). Si el repositorio usa `TreatWarningsAsErrors`, decide cómo tratar esos códigos: convertir todas las advertencias en errores puede ser útil en sistemas nuevos, pero puede romper restauraciones por deuda existente o advisories recién publicados.

Política gradual recomendada:

1. Empezar con auditoría habilitada y crear una línea base de hallazgos.
2. Corregir primero vulnerabilidades críticas/altas y priorizar las explotadas, expuestas o alcanzables.
3. Bloquear nuevos hallazgos según umbral acordado; evitar silenciar globalmente toda la familia NU19xx.
4. Mantener una lista de excepciones específica, con justificación, responsable y fecha de revisión.
5. Ejecutar auditoría en cada restore/build y también en cambios de dependencias.

## 4. OWASP Dependency-Check

### 4.1 Mecanismo y componentes

OWASP Dependency-Check es una suite SCA con CLI e integraciones para algunos entornos de build/CI. Sus analizadores inspeccionan archivos y artefactos para inferir la identidad de componentes. Después correlaciona las identidades reconocidas con fuentes de vulnerabilidades, con uso importante de CPE/CVE y datos de NVD. Puede generar reportes HTML, JSON, SARIF, JUnit y otros formatos.

Para .NET, la documentación lista analizadores para proyectos MSBuild (`.csproj`, `.vbproj`), ensamblados .NET (`.dll`, `.exe`), NuGet `.nuspec` y `packages.config`; además puede analizar archivos de paquetes como `.nupkg`. La forma en que cada analizador obtiene los metadatos es distinta. No asumas que revisar un `.csproj` o un ensamblado reconstruye con la misma exactitud el grafo resuelto de NuGet, incluidos todos los assets, versiones y condiciones. Usa NuGet Audit como fuente principal del grafo NuGet y valida el soporte de Dependency-Check con una muestra de los proyectos y paquetes reales.

### 4.2 Uso por CLI

Instala una versión aprobada desde los [releases oficiales](https://github.com/dependency-check/DependencyCheck/releases) y sigue la [guía de instalación](https://dependency-check.github.io/DependencyCheck/dependency-check-cli/). Para Windows el ejecutable suele ser `dependency-check.bat`; en Linux/macOS, `dependency-check.sh`.

Escaneo básico de una solución:

```powershell
dependency-check.bat `
  --project "Product" `
  --scan ".\src" `
  --out ".\artifacts\dependency-check" `
  --format "HTML" `
  --nvdApiKey "$env:NVD_API_KEY"
```

En Linux/macOS:

```bash
./dependency-check.sh \
  --project "Product" \
  --scan ./src \
  --out ./artifacts/dependency-check \
  --format SARIF \
  --nvdApiKey "$NVD_API_KEY"
```

La CLI permite múltiples rutas `--scan`, formatos múltiples y un umbral `--failOnCVSS <0-10>` que hace que el código de salida indique vulnerabilidades con puntuación igual o superior al umbral. Por ejemplo:

```bash
dependency-check.bat --project "Product" --scan ".\src" --out ".\artifacts\dependency-check" --format "HTML" --format "SARIF" --failOnCVSS 7
```

Verifica nombres y opciones con `dependency-check.bat --help` o `dependency-check.sh --help` para la versión concreta. El umbral CVSS no reemplaza evaluación de explotabilidad, exposición, alcance ni criticidad de negocio.

### 4.3 Datos y operación

Dependency-Check necesita actualizar datos de vulnerabilidades. La sincronización inicial puede ser lenta y consume llamadas a NVD; la documentación del proyecto recomienda obtener una API key NVD y configurarla como secreto del agente, nunca incluirla en el repositorio o en logs. Usa una caché/directorio de datos persistente entre ejecuciones si el agente y la política de seguridad lo permiten; define cómo refrescarlo para que no se vuelva obsoleto.

En redes corporativas restringidas, revisa los hosts externos que contactan los analizadores habilitados, la configuración de proxy y las opciones de espejo de datos. La salida puede contener nombres, rutas y versiones internas: limita acceso y retención de reportes.

## 5. Integración en CI/CD

### 5.1 NuGet Audit en pipeline

Integra la auditoría con restore y build. Ejemplo genérico que sirve como punto de partida para Azure Pipelines, GitHub Actions o un runner propio:

```yaml
steps:
  - script: dotnet restore ./Product.sln
    displayName: Restaurar y auditar dependencias NuGet

  - script: dotnet list ./Product.sln package --vulnerable --include-transitive --format json --output-version 1
    displayName: Generar reporte de paquetes vulnerables
```

Para SDK 10+, cambia el segundo paso a `dotnet package list --project ./Product.sln ...`. Restaura una sola vez y usa `--no-restore` al listar si la versión de CLI lo admite y los assets ya están disponibles.

La política de gate puede basarse en NU190x emitidos en restore, o en analizar el JSON con una política propia. No supongas que el comando de listado por sí solo falla con código distinto de cero en todas las versiones al encontrar vulnerabilidades; confirma el comportamiento del SDK fijado o usa la auditoría integrada de restore como gate y prueba el resultado de CI. Publica el reporte como artefacto si es útil, protegiendo su acceso.

### 5.2 Dependency-Check en pipeline

Ejecuta el CLI o imagen oficial fijando la versión, inyecta el token NVD desde el almacén de secretos de CI, monta una caché persistente si corresponde y publica los reportes HTML/SARIF/JUnit. Ejemplo de comando para un paso de pipeline:

```yaml
- script: >-
    dependency-check.sh
    --project "Product"
    --scan ./src
    --out ./artifacts/dependency-check
    --format SARIF
    --failOnCVSS 7
    --nvdApiKey "$NVD_API_KEY"
  displayName: OWASP Dependency-Check
```

La sintaxis de secretos y publicación de artefactos depende del orquestador. No pases la clave como literal ni la expongas en argumentos registrados si el runner puede imprimirlos; usa variables secretas y las opciones seguras que soporte el plugin/versión. En soluciones .NET, incluye las rutas con `.csproj`, paquetes/artefactos que se pretenda escanear y conserva NuGet Audit como validación específica del grafo resuelto.

### 5.3 Cadencia

- **En cambios de dependencias y pull requests:** auditoría NuGet en restore, visibilidad de hallazgos nuevos y política de bloqueo definida.
- **En rama principal:** escaneo completo con reportes persistentes y triage centralizado.
- **Periódicamente:** volver a escanear aunque el código no cambie, porque los avisos de vulnerabilidades se publican después de liberar una dependencia.
- **En releases:** guardar evidencia de dependencias y decisión de excepciones asociadas a la versión liberada.

## 6. Recomendación de uso conjunto

1. Habilita **NuGet Audit** como control predeterminado de todos los proyectos .NET, con `NuGetAuditMode` explícito y cobertura de transitivas.
2. Ejecuta `dotnet list package --vulnerable --include-transitive` para inspección local, triage y reportes de dependencias.
3. Usa **OWASP Dependency-Check** cuando exista una necesidad concreta de escanear componentes/artefactos heterogéneos, producir reportes consolidados o satisfacer una política de organización que ya lo utilice.
4. Para Dependency-Check, contrasta hallazgos .NET contra el advisory original y la información de NuGet; comprueba falsos positivos por identificación CPE y cobertura de dependencias.
5. Mantén lockfiles cuando el tipo de proyecto y NuGet lo permitan para hacer reproducibles las resoluciones. Revisa el archivo actualizado en cambios de dependencias.
6. Prioriza paquetes en función de severidad, disponibilidad de versión corregida, exposición, explotabilidad, reachability y criticidad del sistema.
7. Actualiza a una versión corregida; si es transitiva, intenta actualizar primero la dependencia directa más cercana. Ejecuta pruebas y revisa cambios de comportamiento.
8. Documenta excepciones con paquete, advisory, motivo, responsable, mitigación y expiración.
9. Mantén la CLI, los datos de vulnerabilidades, plugins y reglas actualizados de forma controlada.
10. Combina SCA con generación de SBOM y gestión de vulnerabilidades cuando el riesgo, regulación o tamaño de la organización lo requieran.

## 7. Casos de uso

### Aplicación interna o prototipo

NuGet Audit durante `dotnet restore` y la lista de paquetes vulnerables pueden dar cobertura inicial con bajo coste operativo. Mantén el audit encendido y corrige de inmediato los riesgos altos y críticos.

### Producto SaaS con varios servicios .NET

Estandariza `Directory.Build.props`, versiones del SDK (`global.json`) y política de excepciones. Ejecuta auditoría para cada solución y publica resultados por repositorio o servicio. Usa Dependency-Check si también se escanean imágenes, binarios u otros ecosistemas desde un proceso común.

### Sistema financiero

Además del restore audit, exige escaneo en CI, revisión de dependencias directas y transitivas, seguimiento con responsable y plazos por severidad, reporte de componentes y evidencia por release. Para advisories críticos/explotados, define un proceso de respuesta acelerado. Dependency-Check puede aportar inventario y reportes transversales, pero no debe sustituir el grafo de NuGet ni una evaluación de aplicabilidad/explotación.

### Repositorio con conectividad restringida

Configura fuentes de auditoría de NuGet accesibles desde el entorno, coordina cachés y espejos autorizados, y valida su frescura. Para Dependency-Check, utiliza proxy o espejo de datos conforme a su documentación y política de red; no ejecutes el análisis con datos desactualizados sin mostrar esa condición en el resultado.

## 8. Riesgos y limitaciones

- Una vulnerabilidad conocida no significa automáticamente que el camino vulnerable sea alcanzable; pero una dependencia no usada directamente puede seguir exponiendo código a través de otro componente.
- Una ausencia de findings no descarta vulnerabilidades sin advisory público, errores de configuración, componentes propios inseguros o cobertura incompleta.
- NuGet Audit depende de fuentes de auditoría y del SDK/NuGet disponibles en restore.
- Dependency-Check identifica componentes mediante analizadores y correlación de identidad; en .NET una coincidencia CPE aproximada puede generar ruido. Revisa el advisory y el paquete exacto.
- La lista CLI, el restore audit y Dependency-Check tienen formatos, cobertura y políticas de salida distintos. Mantén explícito cuál es gate y cuál genera evidencia adicional.
- Un umbral de CVSS aislado no captura exposición ni explotación activa. Considera KEV, reachability y criticidad.
- La supresión reduce visibilidad; no suprimas paquetes globalmente sin alternativa, responsable y revisión futura.

## 9. Ejercicios

### Ejercicio 1: inventario del grafo NuGet

Ejecuta restore y la lista vulnerable con `--include-transitive`. Escoge una dependencia transitiva y localiza qué paquete directo la incorpora. Documenta la versión corregida disponible y la ruta de actualización.

### Ejercicio 2: gate de restore

Configura NuGet Audit explícitamente para dependencias transitivas. Añade o usa un paquete de prueba vulnerable en una rama controlada, observa el warning NU190x y confirma si el pipeline lo trata como warning o error según la política.

### Ejercicio 3: reportes JSON

Genera JSON desde el SDK fijado, guarda el reporte como artefacto y define una política automatizada para marcar nuevos hallazgos. Asegura que el gate funcione con el código de salida comprobado para esa versión.

### Ejercicio 4: comparar Dependency-Check

Ejecuta Dependency-Check contra los mismos proyectos/artefactos. Compara componentes identificados con el grafo NuGet y separa coincidencias exactas, falsos positivos y paquetes no detectados por cada enfoque.

### Ejercicio 5: respuesta de vulnerabilidad

Simula un advisory crítico en una dependencia directa y otro en una transitiva. Define actualización, pruebas regresivas, plazo, aprobación excepcional y evidencia que se conservaría para release.

## 10. Documentación relacionada

- [DevSecOps: SCA y dependencias](../../../Operation/DevSecOps.md#sca-y-dependencias): lugar de SCA dentro del proceso DevSecOps y herramientas relacionadas.
- [SAST](../../../architecture/seguridad/seguridad-aplicativa/sast.md): análisis del código propio y relación con SCA.
- [Semgrep para .NET y C#](semgrep.md): análisis estático de seguridad del código fuente.
- [Análisis estático en .NET](analisis-estatico.md): controles estáticos de calidad y código.
- [Pruebas .NET](test.md): pruebas automatizadas complementarias a la actualización de dependencias.

## 11. Referencias oficiales

- [Microsoft: auditoría de paquetes NuGet](https://learn.microsoft.com/nuget/concepts/auditing-packages)
- [Microsoft: `dotnet package list`](https://learn.microsoft.com/dotnet/core/tools/dotnet-package-list)
- [Microsoft: `dotnet nuget why`](https://learn.microsoft.com/dotnet/core/tools/dotnet-nuget-why)
- [Microsoft: auditoría de paquetes transitivos en .NET 10](https://learn.microsoft.com/dotnet/core/compatibility/sdk/10.0/nugetaudit-transitive-packages)
- [OWASP Dependency-Check: guía CLI](https://dependency-check.github.io/DependencyCheck/dependency-check-cli/)
- [OWASP Dependency-Check: analizadores .NET y NuGet](https://dependency-check.github.io/DependencyCheck/analyzers/index.html)
- [OWASP Dependency-Check: argumentos CLI](https://dependency-check.github.io/DependencyCheck/dependency-check-cli/arguments.html)
- [OWASP Dependency-Check: orígenes de datos y caché](https://dependency-check.github.io/DependencyCheck/data/index.html)
- [OWASP Dependency-Check: repositorio y releases](https://github.com/dependency-check/DependencyCheck)
