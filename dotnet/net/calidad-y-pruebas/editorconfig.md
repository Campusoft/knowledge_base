# EditorConfig para .NET y C#

> **Propósito:** definir convenciones de formato y estilo cerca del código, compartirlas entre el equipo y aplicarlas desde los editores y la integración continua.

## Qué es

**EditorConfig** es un formato de configuración en texto plano para compartir preferencias de edición entre personas que usan distintos editores e IDE. Un archivo `.editorconfig` contiene secciones que seleccionan archivos por patrón y propiedades con valores para esos archivos.

EditorConfig no es por sí solo un formateador, compilador, linter ni analizador de calidad. La especificación define propiedades comunes —por ejemplo, indentación, finales de línea y espacios finales—; los editores, plugins y herramientas interpretan esas propiedades. Para C#, propiedades como `dotnet_style_*`, `csharp_style_*` y `dotnet_diagnostic.*.severity` son entendidas por los analizadores de .NET/Roslyn y herramientas compatibles.

Por ello, distinguir tres piezas ayuda a entender el flujo:

1. **`.editorconfig`:** declara el estilo deseado y, en .NET, opciones de analizadores y severidades.
2. **Editor, IDE o analizador:** muestra diagnósticos y, en muchos casos, ofrece correcciones.
3. **Compilación o CI:** comprueba las reglas que se han configurado para ejecutarse como advertencias o errores.

## Para qué se usa

- Mantener formato coherente aunque el equipo use Visual Studio, VS Code, Rider u otros editores compatibles.
- Establecer reglas de estilo y convenciones de nombres para C#.
- Configurar reglas de calidad del SDK de .NET y de analizadores adicionales.
- Definir convenciones distintas por lenguaje, extensión, carpeta o tipo de archivo.
- Reducir cambios de formato ajenos a la tarea durante una revisión de código.
- Aplicar validaciones repetibles en local y en CI.

## Estructura y selección de archivos

Un archivo `.editorconfig` tiene una propiedad especial y secciones:

```ini
# Propiedad especial: debe ir fuera de cualquier sección.
root = true

# Esta sección se aplica a todos los archivos dentro del alcance.
[*]
charset = utf-8
end_of_line = lf
insert_final_newline = true
trim_trailing_whitespace = true

# Esta sección se aplica a archivos C# y C# script.
[*.{cs,csx}]
indent_style = space
indent_size = 4
```

### `root`

`root = true`, colocado fuera de las secciones, indica que la búsqueda de configuraciones EditorConfig debe detenerse en ese archivo. Suele ubicarse en el `.editorconfig` de la raíz del repositorio para evitar que configuraciones de carpetas superiores del equipo alteren el resultado del proyecto.

### Secciones y patrones

Cada encabezado `[patrón]` selecciona rutas relativas al `.editorconfig` que lo contiene. Los patrones habituales incluyen:

| Patrón | Alcance aproximado |
|---|---|
| `[*]` | Todos los archivos dentro del alcance del archivo de configuración. |
| `[*.cs]` | Archivos C# que coinciden con el patrón en ese nivel y sus subdirectorios, según la regla de glob de EditorConfig. |
| `[*.{cs,csx}]` | Archivos `.cs` y `.csx`. |
| `[{*.yml,*.yaml}]` | Archivos YAML con cualquiera de esas extensiones. |
| `[src/**.cs]` | Archivos C# bajo `src/`. El separador `/` hace que el patrón sea relativo al directorio del `.editorconfig`. |
| `[**/Generated/*.cs]` | Archivos C# en carpetas `Generated` debajo de cualquier ruta. |

Los patrones disponibles incluyen `*`, `**`, `?`, rangos entre corchetes y grupos entre llaves. Confirme que el editor o plugin que use el equipo interpreta las formas avanzadas de glob que el proyecto necesita.

### Precedencia

- EditorConfig busca configuraciones desde la carpeta del archivo hacia sus directorios superiores, hasta llegar a la raíz o encontrar `root = true`.
- Se aplican las propiedades de las secciones que coinciden con el archivo. Si hay valores repetidos, las reglas posteriores tienen precedencia; los `.editorconfig` más cercanos suelen permitir especializar reglas generales.
- `unset` elimina el efecto de una propiedad heredada para el archivo coincidente y permite que actúe el valor predeterminado del editor.
- No añada archivos `.editorconfig` anidados sin una necesidad real: pueden hacer más difícil entender qué regla se aplica.

## Propiedades comunes

| Propiedad | Uso |
|---|---|
| `indent_style` | `space` para espacios o `tab` para tabuladores. |
| `indent_size` | Número de columnas por nivel de indentación; también puede ser `tab`. |
| `tab_width` | Ancho visual de un tabulador. Normalmente coincide con `indent_size`. |
| `end_of_line` | Convención de saltos de línea: `lf`, `crlf` o `cr`. |
| `charset` | Codificación del archivo, por ejemplo `utf-8`. |
| `insert_final_newline` | Exige o evita un salto de línea al final del archivo. |
| `trim_trailing_whitespace` | Quita o conserva espacios al final de las líneas al guardar. |
| `max_line_length` | Longitud preferida para editores que reconocen esta propiedad; no todos los formatos o herramientas la aplican automáticamente. |

Las propiedades comunes solo tienen efecto cuando el editor o la herramienta que procesa el archivo las soporta. No presuponga que todos los editores imponen una longitud máxima de línea o guardan automáticamente con el formato declarado.

## Opciones de estilo y calidad para .NET

Las propiedades comunes de EditorConfig controlan principalmente la edición y el formato. .NET añade opciones que los analizadores de C# y Roslyn pueden interpretar:

- **Convenciones .NET:** `dotnet_style_*`.
- **Preferencias específicas de C#:** `csharp_style_*`.
- **Formato C#:** `csharp_*` para llaves, espacios, saltos de línea y organización del código.
- **Severidad de diagnósticos:** `dotnet_diagnostic.<ID>.severity`.
- **Severidad por categoría o proveedor de analizadores:** `dotnet_analyzer_diagnostic.*`, cuando corresponda.
- **Reglas de nombres:** familias `dotnet_naming_symbols.*`, `dotnet_naming_style.*` y `dotnet_naming_rule.*`.

La [referencia de reglas de estilo de .NET](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/style-rules/) documenta las opciones disponibles y sus identificadores. No todas las propiedades son comprendidas por todos los editores; compilar o analizar con el SDK permite verificar las que deben formar parte de los controles del proyecto.

### Severidades

Una regla puede ser una sugerencia para el editor, una advertencia de compilación o un error que detiene la compilación. Para definir severidad por identificador:

```ini
[*.cs]
# IDE0040: exigir modificadores de acceso explícitos.
dotnet_diagnostic.IDE0040.severity = warning

# CA1822: revisar miembros que pueden ser estáticos.
dotnet_diagnostic.CA1822.severity = warning
```

Valores de severidad comunes en analizadores de .NET:

| Valor | Efecto habitual |
|---|---|
| `none` | Deshabilita el diagnóstico. |
| `silent` | No muestra una notificación visible, pero puede mantener una preferencia para correcciones del IDE. |
| `suggestion` | Muestra una sugerencia en el editor. |
| `warning` | Produce una advertencia durante la compilación si la regla se ejecuta en build. |
| `error` | Produce un error de compilación y bloquea el build. |

El nivel `warning` no bloquea por sí solo un build, salvo que la configuración trate las advertencias como errores. Conviene comenzar con sugerencias o advertencias y elevar reglas seleccionadas a error cuando el equipo haya validado que son claras, útiles y corregibles.

### Reglas de nombres

Las reglas de nombres relacionan tres definiciones: el grupo de símbolos afectado, el estilo requerido y la regla que los conecta.

```ini
[*.cs]
# Qué símbolos se revisan.
dotnet_naming_symbols.private_fields.applicable_kinds = field
dotnet_naming_symbols.private_fields.applicable_accessibilities = private

# Qué forma deben tener.
dotnet_naming_style.private_field_style.required_prefix = _
dotnet_naming_style.private_field_style.capitalization = camel_case

# Cómo se vinculan los símbolos y el estilo.
dotnet_naming_rule.private_fields_follow_style.symbols = private_fields
dotnet_naming_rule.private_fields_follow_style.style = private_field_style
dotnet_naming_rule.private_fields_follow_style.severity = suggestion

# Para exigir el diagnóstico de nombres durante el build:
dotnet_diagnostic.IDE1006.severity = warning
```

La severidad puesta en `dotnet_naming_rule.*.severity` se aplica en el IDE, pero no basta para exigir la regla durante la compilación. Para build, configura la severidad del diagnóstico `IDE1006`. Las convenciones deben ser coherentes con la guía de [convenciones de C#](convenciones-csharp.md) del repositorio.

## Activar análisis de estilo en compilación

En proyectos SDK-style, agrega a un archivo de proyecto o a `Directory.Build.props` la propiedad `EnforceCodeStyleInBuild`:

```xml
<PropertyGroup>
  <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
</PropertyGroup>
```

Esta propiedad permite que las reglas de estilo de código de .NET configuradas como advertencias o errores se ejecuten en la compilación de línea de comandos. Algunas reglas que solo son útiles dentro del IDE pueden no ejecutarse en build por rendimiento. El análisis de calidad integrado en el SDK y el análisis de estilo no son exactamente lo mismo; confirma que cada diagnóstico importante se ejecuta donde se necesita.

En repositorios con varios proyectos, `Directory.Build.props` permite compartir la propiedad. Para aplicar excepciones por proyecto se pueden usar las condiciones de MSBuild, pero es preferible mantener una configuración fácil de comprender y reducir excepciones.

## Integrar `dotnet format`

`dotnet format` utiliza las opciones de `.editorconfig` para aplicar formato y correcciones de estilo y análisis. Sin `--verify-no-changes`, puede modificar archivos; revisa siempre el diff que genera.

```powershell
# Aplicar correcciones sobre una solución antes de revisar los cambios.
dotnet format .\MiSolucion.sln

# Comprobar formato sin modificar archivos; apropiado para validación y CI.
dotnet format .\MiSolucion.sln --verify-no-changes

# Limitar la comprobación a espacio en blanco.
dotnet format whitespace .\MiSolucion.sln --verify-no-changes
```

`dotnet format` puede restaurar, cargar y analizar los proyectos de la solución. En CI, ejecutar la restauración en una etapa previa y usar `--no-restore` si se quiere evitar otra restauración implícita. Se puede acotar el alcance con `--include` y `--exclude`; documenta cualquier exclusión.

Un pipeline habitual combina:

1. Restaurar dependencias.
2. Ejecutar `dotnet format <solución> --verify-no-changes`.
3. Compilar con analizadores habilitados.
4. Ejecutar pruebas.

No uses el formateador como sustituto de pruebas, revisión de código o análisis de arquitectura.

## Ejemplo base para un repositorio C#

Este ejemplo es una base para discutir con el equipo, no una política universal. Ajusta finales de línea, reglas y severidades a los proyectos y a sus herramientas.

```ini
root = true

[*]
charset = utf-8
end_of_line = lf
insert_final_newline = true
trim_trailing_whitespace = true

[*.{cs,csx}]
indent_style = space
indent_size = 4
tab_width = 4

dotnet_style_require_accessibility_modifiers = always
dotnet_diagnostic.IDE0040.severity = warning
dotnet_diagnostic.CA1822.severity = warning

# Convención de nombres para campos privados.
dotnet_naming_symbols.private_fields.applicable_kinds = field
dotnet_naming_symbols.private_fields.applicable_accessibilities = private
dotnet_naming_style.private_field_style.required_prefix = _
dotnet_naming_style.private_field_style.capitalization = camel_case
dotnet_naming_rule.private_fields_follow_style.symbols = private_fields
dotnet_naming_rule.private_fields_follow_style.style = private_field_style
dotnet_naming_rule.private_fields_follow_style.severity = suggestion
dotnet_diagnostic.IDE1006.severity = warning

# Una excepción localizada puede tener severidad menor, si se justifica.
[**/Legacy/**/*.cs]
dotnet_diagnostic.IDE1006.severity = suggestion
```

### Consideraciones sobre versiones

- El análisis de código integrado en el SDK de .NET está habilitado por defecto para proyectos que apuntan a .NET 5 o posterior. Proyectos que apuntan a frameworks anteriores pueden necesitar configuración adicional.
- La sintaxis de severidad `opción = valor:severidad` depende de la versión del SDK. Para compatibilidad explícita, se puede usar `dotnet_diagnostic.<ID>.severity`.
- Alinear el SDK usado localmente y en CI, por ejemplo con `global.json`, hace más reproducibles las reglas y sus correcciones.
- Una regla nueva o una actualización del SDK puede introducir diagnósticos adicionales. Revisa las notas de la versión y valida los cambios antes de elevarlos a errores.

## Buenas prácticas

1. **Versionar la configuración en el repositorio.** Así el editor, el build y CI parten de las mismas reglas.
2. **Colocar el archivo raíz en la raíz del repositorio.** Usa `root = true` salvo que el proyecto necesite heredar reglas superiores intencionalmente.
3. **Preferir una política común y excepciones estrechas.** No debilites una regla para todo el repositorio por un directorio legado.
4. **Separar formato, estilo y calidad.** Un cambio de espacios no debería ocultar una advertencia de calidad o seguridad.
5. **Elegir reglas accionables.** Evita activar reglas que el equipo no entiende, no puede corregir o que producen mucho ruido.
6. **Adoptar reglas gradualmente en bases existentes.** Primero recopila diagnósticos; después corrige por lotes pequeños o limita reglas a código nuevo; finalmente sube la severidad.
7. **No formatear todo el repositorio en el mismo cambio que una modificación funcional.** El formato masivo genera diffs grandes y complica las revisiones.
8. **No suprimir reglas sin motivo.** Si una excepción es necesaria, limita su alcance y deja una explicación junto a ella.
9. **Asegurar concordancia entre IDE, SDK y CI.** El editor mejora el feedback, pero la CI es el control compartido que valida los cambios del equipo.
10. **Revisar los archivos generados y terceros por separado.** Excluirlos o aplicarles reglas específicas cuando el equipo no sea propietario de su estilo.
11. **Probar cambios de configuración.** Modifica un archivo de ejemplo, comprueba qué regla se activa y valida el resultado con la misma versión de SDK que usa CI.
12. **Mantener ejemplos válidos.** Los valores de opciones y patrones deben seguir la documentación del analizador y de EditorConfig.

## Casos de uso

### Equipo que usa varios editores

Define formato y estilo en `.editorconfig`, súbelo junto al código y pide a cada persona que use un editor con soporte EditorConfig. El IDE presenta las reglas mientras se escribe; el pipeline confirma que los diagnósticos configurados se cumplan.

### Convenciones distintas por tipo de archivo

Usa secciones independientes para C#, archivos de proyecto XML y Markdown. Las propiedades comunes se pueden compartir en `[*]`; las preferencias de lenguaje se limitan a sus extensiones correspondientes.

### Modernización de una solución heredada

Empieza ejecutando análisis para obtener el estado actual. Aplica el formateo en lotes separados, acuerda reglas nuevas con el equipo y usa excepciones acotadas para carpetas que todavía no se han migrado. Evita bloquear la entrega completa con cientos de diagnósticos heredados en el primer cambio.

### Validación en CI

Ejecuta `dotnet format --verify-no-changes` y la compilación con estilo habilitado. Configura como error solo reglas que el equipo considere requisitos de integración; conserva las demás como advertencias o sugerencias.

## Ejercicios

### Ejercicio 1: formato común

Crea un `.editorconfig` con `root = true` y configura UTF-8, finales de línea, salto de línea final, eliminación de espacios al final e indentación de cuatro espacios para C#.

**Comprueba:** crea un archivo con tabuladores o espacios finales y valida cómo lo presenta tu editor y qué detecta `dotnet format`.

### Ejercicio 2: convención de nombres

Define una regla para que los campos privados comiencen con `_` y tengan capitalización camelCase.

**Comprueba:** crea un campo que incumpla la convención. Verifica el diagnóstico en el IDE y luego configura `IDE1006` como advertencia para comprobar que aparezca durante la compilación.

### Ejercicio 3: severidades y CI

Activa `EnforceCodeStyleInBuild`, configura `IDE0040` como advertencia y `CA1822` como error. Ejecuta la compilación y observa qué diagnósticos afectan el resultado.

**Comprueba:** baja temporalmente una severidad y confirma la diferencia entre mensaje, advertencia y error. Revertir esta modificación de práctica al terminar.

### Ejercicio 4: regla por carpeta

Añade una sección para `Legacy` que reduzca a sugerencia una regla que es advertencia para el código nuevo.

**Comprueba:** compara un archivo dentro y otro fuera de esa carpeta. Registra por qué existe la excepción y qué condición permitirá retirarla.

### Ejercicio 5: validación sin cambios

Ejecuta `dotnet format <solución> --verify-no-changes` y revisa el código de salida con el repositorio limpio. Cambia el formato de un archivo y repite la validación.

**Comprueba:** la segunda ejecución debe informar que habría cambios y devolver un código distinto de cero; después ejecuta el formateador y revisa el diff.

## Problemas frecuentes

- **El editor ignora una regla:** confirma el patrón que selecciona el archivo, si el editor soporta esa propiedad y si existe otro `.editorconfig` más cercano.
- **La regla aparece en el IDE pero no en CI:** revisa que la severidad permita ejecución en build y activa `EnforceCodeStyleInBuild` para reglas de estilo.
- **Cambiar `.editorconfig` produce muchos cambios:** separa el ajuste de configuración del cambio funcional y aplica el formateo en un commit dedicado.
- **La opción existe pero no se aplica:** comprueba que el nombre y el valor pertenecen a las opciones reconocidas por el analizador instalado y que el patrón alcanza el archivo.
- **Una advertencia no bloquea la integración:** `warning` no siempre falla el build. Si la política requiere bloqueo, configura una regla como `error` o la política del pipeline apropiada.
- **Diferencias entre equipos:** alinea versión de SDK, configuración del repositorio y extensiones; no dependas solo de ajustes personales del editor.

## Documentación relacionada

- [Convenciones de C#](convenciones-csharp.md): notas de estilo existentes en esta base.
- [Análisis estático en .NET](analisis-estatico.md): herramientas y enfoques de análisis.
- [Calidad y pruebas .NET](README.md): índice de esta área.
- [EditorConfig: especificación](https://spec.editorconfig.org/)
- [EditorConfig: propiedades y patrones](https://editorconfig.org/)
- [Opciones de estilo de código .NET](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/code-style-rule-options)
- [Configurar reglas de análisis de código .NET](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/configuration-options)
- [Reglas de nombres en .NET](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/style-rules/naming-rules)
- [Análisis de código .NET y estilo durante el build](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/overview)
- [Comando `dotnet format`](https://learn.microsoft.com/dotnet/core/tools/dotnet-format)