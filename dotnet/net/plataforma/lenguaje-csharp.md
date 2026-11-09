# Notas del lenguaje C#

## C# Records

Understanding C# Records
C# 9 introduced a new feature called records, designed to simplify the creation of classes that are primarily used to hold data. Records come with several built-in features that make them particularly well-suited for DTOs:

- Value Equality: Records automatically generate value-based equality comparisons, making it easier to compare instances based on their content rather than their reference.
- Immutability: Records are immutable by default, meaning their properties cannot be changed after creation. This ensures that the data remains consistent and avoids unexpected modifications.
- Conciseness: Records use a concise syntax for defining properties and automatically generating useful methods like constructors, `Equals`, `GetHashCode`, and `ToString`.
