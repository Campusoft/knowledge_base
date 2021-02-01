

### BDD (Behauvior Driven Development)

- Dado: los pasos necesarios para poner al sistema en el estado que se desea probar
- Cuando: La interacción del usuario que acciona la funcionalidad que deseamos testear.
- Entonces: En este paso vamos a observar los cambios en el sistema y ver si son los deseados
- Given: Provee contexto para el escenario en que se va a ejecutar el test, tales como punto donde se ejecuta el test, o prerequisitos en los datos. Incluye los pasos necesarios para poner al sistema en el estado que se desea probar.
- When: Especifica el conjunto de acciones que lanzan el test. La interacción del usuario que acciona la funcionalidad que deseamos testear.
- Then: Especifica el resultado esperado en el test. Observamos los cambios en el sistema y vemos si son los deseados.


### Herramientas

#### Xunit

Getting Started with xUnit.net
Using .NET Framework with Visual Studio
https://xunit.net/docs/getting-started/netfx/visual-studio

Shared Context between Tests
https://xunit.net/docs/shared-context

Getting Started with xUnit.net
Using .NET Core with Visual Studio
https://xunit.net/docs/getting-started/netcore/visual-studio


Capturing Output
https://xunit.net/docs/capturing-output


Errores Comunes

Si no se ejecutan los test, en visual studio asegurarse que "Microsoft.NET.Test.Sdk" este en las dependencias del proyecto

The packages xunit.runner.visualstudio and Microsoft.NET.Test.Sdk are required for being able to run your test project inside Visual Studio as well as with dotnet test. 
https://xunit.net/docs/getting-started/netcore/visual-studio


#### Mock 

Bogus

Generar Datos faltos.

A simple and sane data generator for populating objects that supports different locales. A delightful port of the famed faker.js and inspired by FluentValidation. Use Bogus to create UIs with fake data or seed databases. Get started by using Faker class or a DataSet directly.

https://github.com/bchavez/Bogus


NSubstitute
A friendly substitute for .NET mocking frameworks
https://nsubstitute.github.io/


Shouldly 3.0.0
Shouldly - Assertion framework for .NET. The way asserting *Should* be

In unit test, generally, dependencies of testing class is mocked (by creating fake implementations using some mock frameworks like Moq and NSubstitute). This makes unit testing harder, especially when dependencies grows.
