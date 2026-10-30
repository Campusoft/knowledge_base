# .NET Standard 2.0 y multi-targeting

**.NET Standard** es una especificación de APIs, no un runtime ni un destino de aplicación. Una biblioteca que apunta a `netstandard2.0` puede ser consumida por .NET Framework y por .NET moderno, siempre que no use APIs fuera del contrato y que sus dependencias también lo soporten. Es la opción habitual para mantener una biblioteca compartida mientras las aplicaciones se migran por oleadas.

Esta guía detalla la decisión y la mecánica. La conversión del formato de proyecto está en [Conversión de proyectos a SDK-style](conversion-a-sdk-style.md); las fases y criterios generales, en la [guía de migración](../framework-a-dotnet.md).

## 1. Qué es y qué no es

- Es un **contrato de APIs** que cada implementación de .NET provee.
- No es un runtime: una aplicación no se ejecuta “sobre .NET Standard”.
- No es un destino para aplicaciones: una aplicación apunta a `net8.0`, `net10.0` o `net48`.
- No garantiza portabilidad en runtime: una API puede existir en el contrato y lanzar `PlatformNotSupportedException` en una plataforma concreta.
- No incluye APIs de interfaz de usuario, `System.Web`, COM ni componentes específicos de Windows.

## 2. Compatibilidad de versiones

| Destino | .NET Framework | .NET moderno | Notas |
|---|---|---|---|
| `netstandard2.0` | 4.6.1 o superior | .NET Core 2.0+, .NET 5+ | Última versión que soporta .NET Framework. Opción para bibliotecas compartidas. |
| `netstandard2.1` | No soportado | .NET Core 3.0+, .NET 5+ | No usar si hay consumidores .NET Framework. |
| `net8.0` / `net10.0` | No | Sí | Destino directo si todos los consumidores son modernos. |
| `net48` | Sí | No | Destino cuando la biblioteca depende de APIs de .NET Framework. |

Confirmar siempre la tabla oficial: las versiones menores de .NET Framework y las implementaciones de terceros pueden tener matices.

## 3. Decisión por biblioteca

| Situación | Opción recomendada |
|---|---|
| La biblioteca solo la consumen aplicaciones .NET modernas. | Apuntar directamente a `net8.0` o `net10.0`. |
| La consumen aplicaciones .NET Framework 4.6.1+ y aplicaciones .NET modernas, y usa APIs del contrato. | `netstandard2.0`. |
| La consumen ambos mundos, pero necesita APIs distintas por plataforma. | Multi-target: `net48;net8.0` (o `net48;net10.0`). |
| La biblioteca usa WPF, Windows Forms, `System.Web`, COM o APIs de Windows. | No puede ser `netstandard2.0`; conservar `net48` o multi-target de Windows (`net48;net8.0-windows`). |
| Los consumidores Framework son anteriores a 4.6.1. | Actualizar Framework o mantener un proyecto específico; `netstandard2.0` no es una solución directa. |
| Es una aplicación, servicio o proceso ejecutable. | Nunca `netstandard`; usar el runtime destino. |

La decisión se toma por biblioteca, no por solución completa, y se registra en el ADR de la Fase 2.

## 4. Cómo apuntar a `netstandard2.0`

1. Convertir el proyecto a SDK-style si aún es clásico (ver [anexo de conversión](conversion-a-sdk-style.md)).
2. Cambiar el destino:

```xml
<PropertyGroup>
  <TargetFramework>netstandard2.0</TargetFramework>
</PropertyGroup>
```

3. Eliminar referencias a ensamblados que no forman parte del contrato (`System.Web`, WPF, Windows Forms, COM, etc.).
4. Revisar los paquetes: cada dependencia debe ofrecer un activo compatible con `netstandard2.0`.
5. Resolver errores de compilación por APIs no disponibles; si son imprescindibles, evaluar multi-target o mantener Framework.
6. Compilar y empaquetar; probar el consumo desde una aplicación Framework y otra moderna.

En proyectos SDK modernos no es necesario agregar `NETStandard.Library` manualmente; el SDK provee las referencias del contrato.

## 5. Multi-targeting

Multi-target permite compilar la misma biblioteca para varios destinos:

```xml
<PropertyGroup>
  <TargetFrameworks>net48;netstandard2.0</TargetFrameworks>
</PropertyGroup>
```

o para escenarios con APIs específicas:

```xml
<PropertyGroup>
  <TargetFrameworks>net48;net8.0</TargetFrameworks>
</PropertyGroup>
```

Referencias condicionales por destino:

```xml
<ItemGroup Condition="'$(TargetFramework)' == 'net48'">
  <Reference Include="System.Web" />
</ItemGroup>

<ItemGroup Condition="'$(TargetFramework)' == 'net8.0'">
  <PackageReference Include="System.Text.Json" Version="..." />
</ItemGroup>
```

Buenas prácticas:

- Usar multi-target solo cuando aporte un beneficio real; duplica compilación, pruebas y mantenimiento.
- Evitar `#if` disperso: aislar diferencias en pocas clases o adaptadores.
- Declarar paquetes con condiciones por destino cuando la versión o el activo cambien.
- Revisar que cada configuración compile y pase pruebas, no solo la principal.
- Documentar qué APIs justifican cada destino.

## 6. Código condicional

Las constantes habituales permiten separar implementaciones:

```csharp
#if NETFRAMEWORK
    // Implementación específica de .NET Framework
#else
    // Implementación para .NET moderno
#endif
```

Constantes útiles: `NETFRAMEWORK`, `NET48`, `NETSTANDARD2_0`, `NET8_0_OR_GREATER`, `NET10_0_OR_GREATER`. Verificar las definidas por el SDK y no asumir nombres.

Reglas:

- Preferir abstracciones y adaptadores antes que condicionales repartidos por todo el código.
- Cubrir ambas ramas con pruebas.
- No usar `#if` para ocultar deuda: si una API no existe, documentar la alternativa y su costo.
- Revisar el lenguaje C# disponible: `netstandard2.0` tiene un valor predeterminado conservador; si se eleva `LangVersion`, validar que las características no dependan de APIs del runtime.

## 7. Empaquetado y consumo

- `dotnet pack` genera un paquete con carpetas `lib/<tfm>` para cada destino.
- Un consumidor .NET Framework elige el activo compatible más cercano; un consumidor .NET moderno hace lo mismo.
- Revisar el `.nupkg` para confirmar que incluye todos los destinos y las dependencias correctas.
- En consumidores .NET Framework pueden necesitarse *binding redirects*; en .NET moderno no.
- Versionar el paquete y documentar qué destinos soporta cada versión.
- Probar el paquete desde un proyecto de consumo real, no solo desde el proyecto de la biblioteca.

## 8. Limitaciones y anti-patrones

- **`netstandard2.1` con Framework:** no compila ni se consume; .NET Framework no lo implementa.
- **Bibliotecas de UI:** WPF, Windows Forms y controles de terceros no forman parte de .NET Standard.
- **`System.Web` y WCF de servidor:** no están en el contrato; requieren Framework o reemplazo.
- **Dependencias incompatibles:** un paquete sin activo `netstandard2.0` bloquea la biblioteca.
- **“Compila, luego es portable”:** el contrato no evita errores en runtime por diferencias de plataforma.
- **Multi-target por defecto:** duplica el costo sin necesidad si todos los consumidores son modernos.
- **Código condicional excesivo:** vuelve la biblioteca difícil de probar y mantener.
- **Paquetes con contenido o scripts:** pueden comportarse distinto al cambiar de formato y destino.

## 9. Validación

- [ ] La biblioteca compila para cada destino declarado.
- [ ] Las pruebas pasan en los destinos ejecutables; `netstandard2.0` se prueba a través de un consumidor.
- [ ] Una aplicación Framework consume la biblioteca y funciona.
- [ ] Una aplicación .NET moderna consume la biblioteca y funciona.
- [ ] El paquete contiene `lib/net48`, `lib/netstandard2.0` o los destinos esperados.
- [ ] Las dependencias tienen activos compatibles con cada destino.
- [ ] Las APIs condicionales están cubiertas por pruebas.
- [ ] La documentación indica qué destinos soporta cada versión.

## 10. Ejemplo de decisión

Una biblioteca de reglas de negocio es usada por cuatro aplicaciones en .NET Framework 4.8 y dos servicios nuevos en .NET 10. No usa `System.Web`, WPF, COM ni APIs de Windows, y todos sus paquetes soportan `netstandard2.0`.

Decisión: convertir a SDK-style y apuntar a `netstandard2.0`. Se valida compilando y probando desde una aplicación 4.8 y otra .NET 10, y se publica un paquete con ese único destino.

Si la biblioteca usara `System.Web`, la decisión sería distinta: conservar `net48`, multi-target `net48;net8.0` con adaptadores, o reemplazar la dependencia antes de compartirla. Si usara WPF, el destino sería `net48;net8.0-windows`, nunca `netstandard2.0`.

## 11. Referencias

- [.NET Standard](https://learn.microsoft.com/dotnet/standard/net-standard)
- [Implementaciones de .NET Standard](https://learn.microsoft.com/dotnet/standard/net-standard#net-implementation-support)
- [Multi-targeting en proyectos .NET](https://learn.microsoft.com/dotnet/standard/library-guidance/cross-platform-targeting)
- [Crear paquetes NuGet multi-target](https://learn.microsoft.com/nuget/create-packages/supporting-multiple-target-frameworks)
- [Conversión a SDK-style](conversion-a-sdk-style.md)
- [Herramientas y validación](herramientas-y-validacion.md)
