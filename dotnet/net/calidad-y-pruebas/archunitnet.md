# ArchUnitNET: pruebas de arquitectura en .NET

> **Propósito:** convertir decisiones arquitectónicas en reglas ejecutables para detectar dependencias y convenciones no permitidas mientras el sistema evoluciona.

## 1. Qué es

ArchUnitNET es una biblioteca de pruebas para .NET que inspecciona los ensamblados compilados y permite expresar reglas arquitectónicas con una API fluida de C#. Las reglas se ejecutan como pruebas automatizadas, por lo general con xUnit, NUnit o MSTest.

Puede comprobar, entre otras cosas, dependencias prohibidas, límites entre capas, convenciones de nombres, ubicación de tipos, interfaces implementadas y llamadas a métodos. Una violación hace fallar la prueba y muestra los tipos que incumplen la regla.

ArchUnitNET no decide cuál arquitectura es correcta ni reemplaza el diseño, la revisión técnica, los analizadores estáticos o las pruebas funcionales. El equipo define las reglas a partir de sus decisiones arquitectónicas.

## 2. Conceptos principales

| Concepto | Significado |
|---|---|
| **Architecture** | Modelo construido a partir de uno o más ensamblados que la biblioteca analiza. |
| **ArchRule** | Regla que describe qué tipos se seleccionan (`That`) y qué condición deben cumplir (`Should`). |
| **Selector** | Consulta sobre clases, interfaces, métodos o tipos, por ejemplo por namespace, nombre o ensamblado. |
| **Dependencia** | Relación detectada entre tipos, como referencias en firmas, atributos, herencia o llamadas. |
| **Check** | Evaluación de una regla contra el modelo de arquitectura. |
| **Razón** | Explicación opcional con `Because(...)` para que el fallo documente la intención de la regla. |

Una regla se lee como una frase: “los tipos que cumplen esta selección (`That`) deben satisfacer esta condición (`Should`)”.

## 3. Cuándo usarlo

- Mantener una arquitectura por capas y evitar dependencias en dirección incorrecta.
- Proteger una arquitectura hexagonal o Clean Architecture de accesos desde el dominio hacia infraestructura o presentación.
- Detectar referencias entre módulos que deberían permanecer independientes.
- Aplicar convenciones de nombres y ubicación de clases, por ejemplo sufijos `Controller`, `Repository` o `Handler`.
- Evitar que determinados tipos usen componentes, métodos o paquetes internos no permitidos.
- Hacer visibles reglas de arquitectura en pull requests y pipelines de CI.
- Migrar un sistema legado de forma gradual, fijando primero las restricciones que ya se cumplen y cerrando excepciones de manera incremental.

No es la herramienta apropiada para probar resultados funcionales, rendimiento, configuración del despliegue ni políticas que dependan de información externa al ensamblado analizado.

## 4. Instalación y organización

Instala el paquete principal y la extensión correspondiente al framework de pruebas del proyecto. Los paquetes de integración tienen nombres como `TngTech.ArchUnitNET.xUnit`, `TngTech.ArchUnitNET.NUnit` y `TngTech.ArchUnitNET.MSTestV2`; para xUnit v3 existe `TngTech.ArchUnitNET.xUnitV3`. Confirma las versiones compatibles con el target framework y el runner del repositorio antes de fijar versiones.

Mantén las pruebas de arquitectura en un proyecto de pruebas separado, por ejemplo `Product.Architecture.Tests`, o en una carpeta identificable dentro del proyecto de pruebas existente. Debe referenciar los proyectos que contienen los ensamblados bajo análisis. Estas pruebas no necesitan levantar la aplicación ni una base de datos.

Carga la arquitectura una vez por clase o fixture de pruebas cuando varias reglas analicen los mismos ensamblados. En soluciones grandes, limita los ensamblados cargados para reducir tiempo y evitar que reglas locales inspeccionen código que no les corresponde.

## 5. Ejemplos

Los siguientes ejemplos son patrones que deben adaptarse a los namespaces, ensamblados, versión de ArchUnitNET y framework de pruebas reales del repositorio.

### 5.1 Cargar ensamblados y prohibir una dependencia

```csharp
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

public sealed class ArchitectureRules
{
    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            typeof(Campusoft.Billing.Domain.Billing.Invoice).Assembly,
            typeof(Campusoft.Billing.Infrastructure.Persistence.InvoiceRepository).Assembly)
        .Build();

    [Fact]
    public void Domain_must_not_depend_on_infrastructure()
    {
        var domain = Types().That()
            .ResideInNamespaceMatching("Campusoft\\.Billing\\.Domain(\\..*)?")
            .As("Billing Domain");

        var infrastructure = Types().That()
            .ResideInNamespaceMatching("Campusoft\\.Billing\\.Infrastructure(\\..*)?")
            .As("Billing Infrastructure");

        Types().That().Are(domain)
            .Should().NotDependOnAny(infrastructure)
            .Because("the domain must remain independent from persistence and external adapters")
            .Check(Architecture);
    }
}
```

Los tipos usados en `typeof(...)` son marcadores para obtener ensamblados; reemplázalos por tipos reales. Verifica la sobrecarga de selección de namespace disponible en la versión instalada; también puedes seleccionar por nombre completo o ensamblado cuando la estructura del repositorio lo haga más estable.

### 5.2 Convención de nombres

```csharp
[Fact]
public void Application_handlers_should_end_with_Handler()
{
    Classes().That()
        .ResideInNamespace("Campusoft.Billing.Application", "")
        .And().ImplementInterface("MediatR.IRequestHandler`2")
        .Should().HaveNameEndingWith("Handler")
        .Because("los handlers deben ser fáciles de identificar en revisiones")
        .Check(Architecture);
}
```

Las expresiones de interfaces genéricas y las consultas de namespace pueden requerir ajustes según la versión y cómo compila el proyecto. Si una convención se expresa de forma frágil, divide la selección en consultas pequeñas y comprueba primero qué tipos quedan incluidos.

### 5.3 Ejecutar desde CLI y CI

```bash
dotnet test ./tests/Product.Architecture.Tests/Product.Architecture.Tests.csproj --configuration Release
```

En Azure Pipelines, GitHub Actions u otro servidor, ejecuta este comando después de restaurar y compilar según el flujo del repositorio. Trátalo como una prueba requerida para las ramas protegidas si la regla representa una restricción arquitectónica acordada.

## 6. Casos productivos

### Solución por capas

Reglas típicas: Domain no depende de Application, Infrastructure ni API; Application no depende de Infrastructure ni API; API puede depender de Application para componer la entrada. Ajusta la dirección a la arquitectura adoptada y permite explícitamente las abstracciones compartidas cuando estén justificadas.

### Arquitectura hexagonal

Comprueba que el núcleo no referencie adaptadores concretos. Las interfaces de puertos pueden estar en el núcleo o en Application y las implementaciones en Infrastructure. La regla debe reflejar el diseño real, no solo los nombres de carpetas.

### Monolito modular

Define qué módulos pueden referenciarse y cuáles deben comunicarse mediante contratos públicos. Para excepciones temporales, documenta responsable, motivo y condición de eliminación; evita listas de excepciones permanentes sin revisión.

### Sistema legado

Empieza con reglas de alto valor y bajo ruido: una dependencia prohibida crítica o una convención de nombres acotada. Establece una línea base y añade reglas gradualmente para que la adopción no bloquee cambios legítimos por incumplimientos históricos ajenos al cambio.

## 7. Recomendaciones

1. Formula reglas a partir de decisiones acordadas y escríbelas con nombres y razones comprensibles.
2. Prefiere límites observables y estables (ensamblados, namespaces bien definidos, contratos) a detalles accidentales de implementación.
3. Mantén cada regla enfocada; cuando falla, el equipo debe poder identificar qué dependencia corregir.
4. Empieza con pocas reglas importantes, ejecútalas en CI y amplía la cobertura con la experiencia.
5. Evita duplicar validaciones que ya sean más simples y claras en el compilador, referencias de proyectos o analizadores.
6. No ocultes fallos deshabilitando una regla globalmente; revisa la selección, la excepción y la intención arquitectónica.
7. Revisa las reglas cuando cambien los módulos, namespaces, ensamblados o límites del sistema.
8. Mantén el paquete y el test runner compatibles con el SDK/target framework del repositorio; fija versiones y actualízalas mediante el proceso normal de dependencias.
9. Asegura que los ensamblados cargados correspondan al código que el pipeline acaba de compilar.
10. Añade una razón (`Because`) a las restricciones cuyo propósito no resulte obvio por el nombre de la prueba.

## 8. Límites y precauciones

- La regla solo puede evaluar los ensamblados y tipos que se cargaron; una carga incompleta puede dar una falsa sensación de cobertura.
- Los nombres de namespaces son convenciones, no límites de seguridad. Si una frontera importa, combínala con estructura de proyectos, control de dependencias y revisiones.
- Reflexión, generación de código, tipos dinámicos y patrones de dependencia indirecta pueden requerir validaciones complementarias.
- Una regla demasiado amplia puede hacer ruidosa la suite o dificultar refactorizaciones legítimas.
- ArchUnitNET no reemplaza pruebas unitarias, de integración, analizadores Roslyn ni escáneres de vulnerabilidades.
- No infieras que una regla pasó si ningún tipo fue seleccionado. Valida que la consulta identifique los tipos esperados, especialmente al crear una regla nueva.

## 9. Ejercicios

### Ejercicio 1: detectar la dirección incorrecta

En una solución con `Domain`, `Application`, `Infrastructure` y `Api`, escribe una regla que falle si `Domain` referencia `Infrastructure`. Añade `Because(...)` y provoca una infracción temporal para observar el diagnóstico.

### Ejercicio 2: establecer límites de módulos

Elige dos módulos de una aplicación, por ejemplo `Billing` y `Catalog`. Permite únicamente las dependencias aprobadas y comprueba que el resto de tipos del módulo no depende del otro módulo. Describe cómo expresarías un contrato compartido sin abrir todos los namespaces.

### Ejercicio 3: codificar una convención

Selecciona handlers de comandos y consultas y exige una convención de nombres o ubicación. Verifica que la consulta selecciona todos los tipos esperados y ninguno de otra capa.

### Ejercicio 4: adoptar reglas en un legado

Identifica una dependencia no deseada existente. Define un plan por etapas: limitar la regla a código nuevo o a un módulo, registrar una excepción temporal con dueño y fecha objetivo, y retirar la excepción después de la migración.

### Ejercicio 5: integrar en CI

Añade el proyecto de pruebas de arquitectura al comando de CI. Comprueba que un fallo devuelve un código distinto de cero y bloquea el job. Asegúrate de que el pipeline restaura las dependencias y compila los ensamblados requeridos.

## 10. Diagnóstico de problemas

| Síntoma | Revisión inicial |
|---|---|
| La regla pasa aunque parece haber una violación | Comprueba qué ensamblados se cargaron, si los tipos cumplen el selector y si se compiló la versión actual. |
| La regla falla para muchos tipos inesperados | Revisa el alcance del namespace, los tipos generados y si la regla expresa realmente el límite deseado. |
| El test runner no descubre la prueba | Verifica el paquete de integración correcto, su versión y la configuración del framework de pruebas. |
| La suite tarda demasiado | Carga solo los ensamblados necesarios y reutiliza la instancia de `Architecture`. |
| Cambió el namespace o el nombre del ensamblado | Actualiza los selectores y trata el cambio como una modificación de la regla arquitectónica. |

## 11. Referencias

- [Repositorio y guía oficial de ArchUnitNET](https://github.com/TNG/ArchUnitNET)
- [Documentación de ArchUnitNET](https://archunitnet.readthedocs.io/)
- [Análisis estático en .NET](analisis-estatico.md)
- [EditorConfig](editorconfig.md)
- [`dotnet format`](dotnet-format.md)
- [Pruebas en .NET](test.md)
