# Rust: características, casos de uso y recomendaciones

## ¿Qué es Rust?

Rust es un lenguaje compilado de propósito general, especialmente adecuado para software de sistemas y servicios donde importan el rendimiento, el control de recursos y la seguridad de memoria. El compilador verifica muchas condiciones antes de ejecutar el programa; Rust no depende de un recolector de basura.

## Características principales

- **Propiedad y préstamos (ownership y borrowing):** el compilador controla quién puede usar un valor y por cuánto tiempo. Las referencias y reglas de préstamo ayudan a evitar accesos inválidos y carreras de datos en código seguro.
- **Seguridad de memoria:** el sistema de tipos y el análisis estático detectan errores comunes antes de ejecutar. `unsafe` permite operaciones de bajo nivel, pero debe quedar acotado y justificado.
- **Rendimiento y control:** el código se compila a código nativo y permite trabajar con recursos y memoria con poco costo de ejecución.
- **Concurrencia:** los tipos y restricciones del lenguaje ayudan a detectar ciertos errores de concurrencia durante la compilación. No eliminan errores de lógica, bloqueos ni fallos de diseño.
- **Tipos explícitos y manejo de errores:** `Option` y `Result` hacen visibles los valores ausentes y los errores que deben tratarse.
- **Ecosistema integrado:** Cargo administra paquetes, dependencias, compilación y pruebas; rustfmt y Clippy ayudan a mantener formato y calidad.

## Casos de uso adecuados

- Herramientas de línea de comandos y utilidades de alto rendimiento.
- Servicios de red, APIs y componentes backend con requisitos de latencia o consumo de recursos.
- Sistemas operativos, motores, bases de datos, runtimes y componentes de infraestructura.
- Sistemas embebidos y firmware, cuando la plataforma y el ecosistema de dependencias son compatibles.
- WebAssembly y componentes de alto rendimiento que se integran con aplicaciones existentes.
- Bibliotecas nativas que se exponen a otros lenguajes mediante FFI.

## Cuándo evaluar otra opción

Rust puede requerir más aprendizaje inicial, en especial para entender ownership, lifetimes, traits y asincronía. La compilación y los diagnósticos de tipos complejos pueden aumentar el ciclo de desarrollo. Si el problema es principalmente CRUD convencional y el equipo ya tiene una plataforma madura, conviene comparar el costo de adopción con el beneficio esperado. La disponibilidad de crates, soporte de la plataforma y experiencia del equipo deben validarse para cada proyecto.

## Recomendaciones de adopción

1. Definir el problema que Rust resolverá: rendimiento medible, uso de memoria, seguridad, portabilidad o integración con código nativo.
2. Empezar con una herramienta o servicio acotado y medir compilación, operación, observabilidad y mantenimiento.
3. Usar Cargo y mantener el `Cargo.lock` en aplicaciones ejecutables para reproducir las versiones resueltas.
4. Mantener pequeño y documentado el código `unsafe`; encapsularlo detrás de interfaces seguras cuando sea posible.
5. Adoptar `cargo fmt` y `cargo clippy` en el flujo de trabajo, y ejecutar pruebas automatizadas.
6. Revisar licencias, mantenimiento, avisos de seguridad y compatibilidad de las dependencias antes de incorporarlas.
7. Para equipos nuevos, seguir [The Rust Programming Language](https://doc.rust-lang.org/book/) y consultar la [documentación oficial](https://www.rust-lang.org/learn).

## Fuentes

- [Rust: Why Rust?](https://github.com/rust-lang/rust#why-rust)
- [The Rust Programming Language: Ownership](https://doc.rust-lang.org/book/ch04-00-understanding-ownership.html)
- [The Rust Programming Language: Error Handling](https://doc.rust-lang.org/book/ch09-00-error-handling.html)
- [The Rust Reference](https://doc.rust-lang.org/reference/)
- [Cargo Book](https://doc.rust-lang.org/cargo/)
