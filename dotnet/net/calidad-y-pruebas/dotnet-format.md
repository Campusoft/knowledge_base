# `dotnet format`

> **Propósito:** aplicar y verificar en proyectos .NET las convenciones de formato, estilo y análisis definidas en `.editorconfig` y en los analizadores configurados.

## Qué es

`dotnet format` es una herramienta del **.NET SDK** que carga un proyecto o solución, analiza el código C# y puede aplicar correcciones automáticas. Sus preferencias se obtienen de `.editorconfig` cuando existe; sin ese archivo, se aplican los valores predeterminados de la herramienta.

Es útil para dos momentos distintos:

- **Corrección local:** modifica el código para que cumpla las reglas.
- **Verificación:** comprueba que no haya cambios de formato pendientes y falla si encontraría cambios. Este modo se usa en CI y validaciones automatizadas.

`dotnet format` no reemplaza el compilador, las pruebas, la revisión de código ni una evaluación de arquitectura. Su alcance depende de los proyectos que puede cargar y de las reglas habilitadas.

> La documentación oficial actual aplica el comando al .NET SDK 6 y versiones posteriores. Usa `dotnet format --help` con el SDK del repositorio para confirmar las opciones disponibles localmente.

## Relación con EditorConfig y los analizadores

La configuración del proyecto suele distribuirse así:

| Elemento | Responsabilidad |
|---|---|
| **`.editorconfig`** | Define propiedades de formato, preferencias de estilo, convenciones de nombres y severidades de diagnósticos. |
| **Analizadores .NET/Roslyn** | Interpretan las reglas C# y de calidad, producen diagnósticos y, para varias reglas, ofrecen correcciones. |
| **`dotnet format`** | Aplica las correcciones disponibles o comprueba que no haya cambios pendientes. |
| **Build y CI** | Ejecutan los controles acordados y determinan si el cambio puede avanzar. |

Para conocer la sintaxis, alcance de patrones y reglas de `.editorconfig`, consulta [EditorConfig para .NET](editorconfig.md). En proyectos nuevos, conviene definir primero una configuración común y después añadir las reglas que el equipo pueda mantener.

## Subcomandos y alcance

| Comando | Alcance principal | Uso típico |
|---|---|---|
| `dotnet format` | Formato, preferencias de estilo y sugerencias de analizadores disponibles para la solución o proyecto. | Revisión completa del formato y de reglas configuradas. |
| `dotnet format whitespace` | Espacios, sangría, saltos de línea y otras reglas de formato. | Normalizar formato sin aplicar reglas de estilo o calidad. |
| `dotnet format style` | Preferencias de estilo de código definidas en EditorConfig. | Corregir o verificar reglas como simplificaciones y estilo C#. |
| `dotnet format analyzers` | Reglas de analizadores de calidad, excluyendo reglas de estilo. | Aplicar correcciones de analizadores que tengan una corrección automática. |

El argumento puede ser una solución (`.sln` o `.slnx`) o un proyecto (`.csproj`). Si se omite, MSBuild busca una solución o proyecto en el directorio actual. En soluciones grandes, escoger el alcance explícitamente evita procesar proyectos ajenos al cambio.

## Opciones de uso frecuente

| Opción | Efecto |
|---|---|
| `--verify-no-changes` | No modifica archivos; devuelve código distinto de cero si detecta cambios que aplicar. |
| `--no-restore` | Evita una restauración implícita. Úsala si la solución ya fue restaurada correctamente en el mismo job. |
| `--include <rutas>` | Limita el análisis a archivos o directorios seleccionados. |
| `--exclude <rutas>` | Excluye archivos o directorios del análisis. |
| `--diagnostics <IDs>` | Limita el trabajo a identificadores de diagnóstico específicos. |
| `--severity <nivel>` | Define la severidad mínima de diagnósticos a corregir: `info`, `warn` o `error`. El valor predeterminado es `warn`. |
| `--include-generated` | Incluye código generado; por omisión normalmente se deja fuera. Úsalo solo si el proyecto tiene una razón para formatearlo. |
| `--report <ruta>` | Escribe un informe JSON con el resultado. |
| `--verbosity <nivel>` | Ajusta el detalle del registro para diagnóstico. |

Las opciones exactas pueden variar según la versión del SDK. Antes de estandarizar un comando en un repositorio, valida la sintaxis con `dotnet format --help` usando la versión fijada por el proyecto.

## Flujo local recomendado

1. **Restaurar la solución** si todavía no se han restaurado sus dependencias.
2. **Aplicar formato** sobre el proyecto o solución adecuada.
3. **Revisar el diff**, confirmar que los cambios sean mecánicos y revertir cualquier modificación inesperada.
4. **Compilar y ejecutar pruebas** para confirmar que el código sigue siendo válido.
5. **Incluir el cambio de formato por separado** cuando se trate de una corrección masiva en un repositorio existente.

### Aplicar a una solución

```bash
dotnet format ./Product.sln
```

Este comando modifica los archivos que puede corregir. Comprueba el resultado con `git diff` antes de confirmar los cambios.

### Aplicar a un proyecto

```bash
dotnet format ./src/Payments/Payments.csproj
```

Usa el proyecto cuando no quieras analizar toda la solución o cuando trabajes en un módulo aislado.

### Aplicar solo formato de espacios

```bash
dotnet format whitespace ./Product.sln
```

### Corregir un diagnóstico específico

```bash
dotnet format style ./Product.sln --diagnostics IDE0005 --severity info
```

El comando intenta aplicar correcciones para ese diagnóstico. No todos los diagnósticos tienen una corrección automática.

## Verificación en CI

En CI se recomienda **verificar, no modificar** los archivos. El pipeline debe restaurar una vez, ejecutar la verificación y fallar si el código no cumple el formato acordado.

```bash
dotnet restore ./Product.sln
dotnet format ./Product.sln --verify-no-changes --no-restore
dotnet build ./Product.sln --no-restore
dotnet test ./Product.sln --no-build --no-restore
```

Asegúrate de que el runner detenga el job cuando cualquiera de los comandos devuelva un código distinto de cero. El build y las pruebas cumplen funciones distintas: la verificación de formato no demuestra que el programa compile ni que su comportamiento sea correcto.

Para la verificación más limitada de espacios en blanco:

```bash
dotnet format whitespace ./Product.sln --verify-no-changes --no-restore
```

La opción `--no-restore` funciona si los proyectos fueron restaurados antes y los archivos de assets siguen presentes. Si no, permite que `dotnet format` restaure por sí mismo o ejecuta de nuevo `dotnet restore`.

### Ejemplo para Azure Pipelines

El comando es independiente del proveedor de CI; Azure Pipelines puede ejecutar los mismos comandos como pasos de script. Un ejemplo mínimo en YAML:

```yaml
steps:
- script: dotnet restore ./Product.sln
  displayName: Restore

- script: dotnet format ./Product.sln --verify-no-changes --no-restore
  displayName: Verify formatting

- script: dotnet build ./Product.sln --no-restore
  displayName: Build

- script: dotnet test ./Product.sln --no-build --no-restore
  displayName: Test
```

Usa una versión de SDK coherente con la configuración del repositorio, idealmente fijada en `global.json`. Si la solución contiene proyectos con SDKs o requisitos incompatibles, separa las comprobaciones por solución o proyecto.

## Ejemplos de escenarios productivos

### Repositorio con varios proyectos

Usa una solución común para comprobar los proyectos mantenidos por el equipo:

```bash
dotnet format ./Company.sln --verify-no-changes --no-restore
```

Si la solución también contiene herramientas externas, proyectos migrados o código que no forma parte del cambio, acota el conjunto:

```bash
dotnet format ./Company.sln --verify-no-changes --no-restore \
  --include ./src/Orders/ ./tests/Orders.Tests/
```

Las rutas de `--include` y `--exclude` deben ser relativas al directorio de trabajo. Confirma el resultado en el agente de CI, ya que estructura del checkout y ruta de ejecución afectan a esas rutas.

### Migración gradual de una base existente

Una base antigua puede tener miles de diferencias de formato. No conviertas todo el repositorio en un solo cambio funcional:

1. Genera una ejecución local para identificar el volumen y los diagnósticos.
2. Aplica el formato en una rama dedicada o por módulos.
3. Revisa el diff y confirma cambios de formato en lotes fáciles de auditar.
4. Añade `--verify-no-changes` a CI una vez que la rama principal esté limpia.
5. Adopta nuevas reglas de estilo progresivamente con `.editorconfig`.

Las excepciones deben ser específicas y temporales cuando sea posible. No uses `--exclude` como sustituto permanente de corregir la configuración o acordar el alcance.

### Monorepo con varios SDKs

En un monorepo, una única solución puede no ser la unidad correcta. Ejecuta la herramienta desde el directorio correspondiente y fija el SDK esperado para cada conjunto de proyectos. Evita que una actualización no coordinada del SDK cambie el conjunto de analizadores o las correcciones propuestas en todos los módulos a la vez.

### Código generado

Por defecto, `dotnet format` no incluye código generado. Mantén esa exclusión en general porque el formato puede regenerarse o no ser propiedad del equipo. Si un caso requiere procesarlo, revisa el impacto antes de habilitar `--include-generated`.

## Corrección frente a verificación

| Intención | Comando | ¿Modifica archivos? |
|---|---|---|
| Formatear una solución durante desarrollo | `dotnet format ./Product.sln` | Sí |
| Verificar antes de integrar | `dotnet format ./Product.sln --verify-no-changes` | No |
| Aplicar solo espacios | `dotnet format whitespace ./Product.sln` | Sí |
| Aplicar estilo | `dotnet format style ./Product.sln` | Sí |
| Aplicar correcciones de analizadores | `dotnet format analyzers ./Product.sln` | Sí |
| Probar una regla concreta | `dotnet format style ./Product.sln --diagnostics IDE0005` | Puede modificar archivos |

Si una ejecución cambia código inesperado, usa `--include` para aislar un archivo, `--diagnostics` para acotar una regla o `--verbosity diagnostic` para recopilar información del proceso. Revisa siempre el diff.

## Seguridad y límites operativos

`dotnet format` carga el proyecto o solución y puede restaurar, compilar y ejecutar analizadores. Ejecútalo solo sobre código confiable, como harías con otros comandos de build. En agentes de CI, usa permisos mínimos y evita exponer secretos a pipelines que procesan cambios no revisados.

El comando solo puede corregir diagnósticos para los que haya una corrección disponible. Una ejecución exitosa no significa que todos los diagnósticos estén resueltos ni que se hayan comprobado seguridad, comportamiento funcional o reglas de arquitectura.

## Buenas prácticas

- Mantén reglas y severidades en `.editorconfig` versionado; evita depender de preferencias personales del IDE.
- Usa la misma versión del SDK y el mismo alcance en local y CI.
- Usa el modo normal para aplicar correcciones y `--verify-no-changes` para proteger la integración.
- Ejecuta primero `restore`; usa `--no-restore` después para reducir trabajo duplicado en CI.
- Comienza con reglas concretas, con diagnósticos identificables y correcciones que el equipo entienda.
- Mantén separados los cambios mecánicos de formato y los cambios funcionales.
- Revisa diferencias grandes antes de aceptarlas; las correcciones automáticas siguen necesitando revisión.
- Usa `--include` y `--exclude` para gestionar transiciones acotadas y documenta el motivo.
- Mantén pruebas, compilación y análisis de seguridad como controles separados.

## Ejercicios

### Ejercicio 1: aplicar y revisar formato

Ejecuta `dotnet format ./Product.sln` en una rama de práctica. Revisa el diff y clasifica los cambios entre espacios, estilo y correcciones de analizadores.

**Resultado esperado:** identificar las reglas que originaron cambios y confirmar que no se alteró comportamiento funcional.

### Ejercicio 2: comprobar la rama limpia

Ejecuta `dotnet format ./Product.sln --verify-no-changes` con la solución formateada y luego introduce una diferencia de espacios en un archivo de prueba.

**Resultado esperado:** la primera ejecución pasa y la segunda devuelve código distinto de cero sin modificar el archivo.

### Ejercicio 3: limitar por proyecto y diagnóstico

Ejecuta `dotnet format style ./Product.sln --diagnostics IDE0005 --include ./src/Orders/`.

**Resultado esperado:** las correcciones quedan limitadas al diagnóstico y ruta seleccionados; comprueba la sintaxis de opciones con la versión del SDK elegida.

### Ejercicio 4: integrar en CI

Añade restore, verificación, build y test a una pipeline de práctica. Introduce una infracción de formato y observa qué etapa falla.

**Resultado esperado:** la verificación bloquea la integración con un mensaje accionable; la herramienta no cambia los archivos en CI.

## Problemas frecuentes

- **El comando no encuentra solución o proyecto:** ejecútalo desde la carpeta correcta o pasa explícitamente el `.sln`, `.slnx` o `.csproj`.
- **No respeta una regla:** revisa que la solución esté cargando el archivo `.editorconfig` esperado y que la sección coincida con el archivo.
- **Diferencias entre local y CI:** compara `dotnet --info`, el SDK fijado, la ruta de ejecución, las variables del entorno y la restauración.
- **La verificación falla con una diferencia grande:** ejecuta el modo de corrección localmente, revisa el diff y vuelve a validar; no quites el gate sin entender el origen.
- **Una regla produce diagnóstico pero no cambia el archivo:** puede no existir una corrección automática, o la corrección puede depender de información de compilación que no pudo cargarse.
- **El comando tarda demasiado:** acota a proyecto, archivos o diagnóstico; evita ejecuciones paralelas que reparen simultáneamente la misma solución.
- **Los proyectos no cargan en CI:** restaura dependencias, comprueba workloads, variables y SDK, y conserva logs detallados para diagnóstico.

## Documentación relacionada

- [EditorConfig para .NET](editorconfig.md): reglas, patrones, severidades y configuración.
- [Convenciones de C#](convenciones-csharp.md): estilo de código en esta base.
- [Análisis estático en .NET](analisis-estatico.md): herramientas y enfoques de análisis.
- [Calidad y pruebas .NET](README.md): índice del área.
- [Comando `dotnet format`](https://learn.microsoft.com/dotnet/core/tools/dotnet-format)
- [Análisis de código .NET](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/overview)