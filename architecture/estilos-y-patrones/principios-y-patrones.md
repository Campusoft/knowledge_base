# Principios y patrones de diseño

Referencias de diseño que ayudan a organizar responsabilidades y dependencias dentro de una aplicación. Un principio orienta decisiones; un patrón describe una solución recurrente. No son reglas que deban aplicarse mecánicamente.

## SOLID

- **Responsabilidad única (SRP):** reunir en un módulo responsabilidades que cambian por la misma razón.
- **Abierto/cerrado (OCP):** permitir extender comportamiento sin modificar repetidamente código estable.
- **Sustitución de Liskov (LSP):** las implementaciones sustitutas deben respetar los contratos esperados.
- **Segregación de interfaces (ISP):** preferir contratos pequeños y relevantes para sus consumidores.
- **Inversión de dependencias (DIP):** hacer que la lógica de alto nivel dependa de abstracciones, no de detalles concretos.

## Mediator

El patrón Mediator centraliza la coordinación entre componentes para reducir referencias directas y dependencias cruzadas. Puede ser útil cuando la coordinación está dispersa, pero un mediador demasiado grande se convierte en otro punto de acoplamiento. CQRS puede implementarse con un mediador, pero no lo requiere.

## Unit of Work

Unit of Work coordina cambios relacionados para confirmarlos como una unidad transaccional. Debe respetar el límite de consistencia de la aplicación; no hace que una transacción distribuida entre varias bases sea automáticamente atómica.

## Referencias

- [Refactoring.Guru: patrones de diseño y principios](https://refactoring.guru/es): conceptos e ilustraciones en español.
- [GoF Design Patterns in C# with working examples inspired by food](https://github.com/wesdoyle/design-patterns-explained-with-food): ejemplos didácticos de patrones GoF.
- [CQRS](cqrs.md): patrón para separar responsabilidades de lectura y escritura.
