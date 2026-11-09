# Rust: arquitecturas, herramientas, frameworks y proyectos

## Arquitecturas y organización

Rust no impone una arquitectura de aplicación. Conviene elegirla según el dominio y mantener la organización comprensible para el equipo.

### Paquete Cargo convencional

```text
mi-proyecto/
├── Cargo.toml
├── Cargo.lock
├── src/
│   ├── main.rs       # punto de entrada de una aplicación
│   ├── lib.rs        # API de una biblioteca, si aplica
│   ├── bin/          # ejecutables adicionales
│   └── ...           # módulos del dominio y adaptadores
├── tests/            # pruebas de integración
├── examples/         # ejemplos ejecutables
└── benches/          # benchmarks, cuando se necesiten
```

Una aplicación pequeña puede comenzar con módulos por responsabilidad. En servicios que crecen, se pueden separar dominio, casos de uso y adaptadores (HTTP, persistencia, mensajería) para limitar dependencias y facilitar pruebas. No hace falta convertir esa separación en muchos crates desde el inicio.

### Workspace

Un workspace Cargo agrupa varios paquetes relacionados, comparte configuración y facilita desarrollar componentes juntos. Es útil cuando hay límites reales entre una biblioteca de dominio, un servicio, una CLI o paquetes reutilizables. Para proyectos pequeños, un solo paquete suele ser suficiente.

### Patrones de arquitectura aplicables

- **Monolito modular:** buen punto de partida para servicios de negocio; módulos claros dentro de un despliegue sencillo.
- **Hexagonal / puertos y adaptadores:** útil si se requiere cambiar infraestructura o probar la lógica sin base de datos ni red.
- **Event-driven y microservicios:** reservar para necesidades de escalado, despliegue o autonomía de equipos que justifiquen la complejidad operativa.
- **Concurrencia por tareas:** en servicios asíncronos, separar tareas y límites de concurrencia; definir manejo de errores, cancelación y backpressure desde el diseño.

## Herramientas del ecosistema

| Necesidad | Herramienta | Uso |
|---|---|---|
| Toolchain | [rustup](https://rust-lang.github.io/rustup/) | Instalar y administrar compiladores y componentes de Rust. |
| Compilar, dependencias y pruebas | [Cargo](https://doc.rust-lang.org/cargo/) | Administrar paquetes y ejecutar `cargo build`, `cargo test` y otros comandos. |
| Formato | [rustfmt](https://github.com/rust-lang/rustfmt) | Aplicar formato consistente con `cargo fmt`. |
| Lint | [Clippy](https://github.com/rust-lang/rust-clippy) | Detectar patrones sospechosos y sugerir mejoras con `cargo clippy`. |
| IDE / lenguaje | [rust-analyzer](https://rust-analyzer.github.io/) | Autocompletado, navegación, diagnósticos y refactorizaciones. |
| Registro de paquetes | [crates.io](https://crates.io/) | Buscar y publicar crates; revisar actividad, licencia y compatibilidad antes de elegir. |

## Frameworks y bibliotecas por área

Estas opciones son puntos de partida; comprobar versiones, mantenimiento, soporte de plataforma y requisitos de licencia al iniciar un proyecto.

| Área | Opciones | Orientación |
|---|---|---|
| Backend HTTP | [Axum](https://github.com/tokio-rs/axum), [Actix Web](https://github.com/actix/actix-web) | Frameworks para APIs y servicios web; revisar integración con el runtime y middleware requerido. |
| Asincronía y red | [Tokio](https://tokio.rs/) | Runtime asíncrono y herramientas de red ampliamente usadas en servicios Rust. |
| CLI | [clap](https://github.com/clap-rs/clap) | Argumentos, opciones y ayuda de línea de comandos. |
| GUI | [egui](https://github.com/emilk/egui), [Tauri](https://tauri.app/) | egui para interfaces nativas inmediatas; Tauri para aplicaciones de escritorio con frontend web y backend Rust. |
| Embebido | [Embassy](https://embassy.dev/) | Ecosistema asíncrono para microcontroladores; validar soporte del chip y HAL. |
| WebAssembly | [wasm-bindgen](https://github.com/rustwasm/wasm-bindgen), [wasm-pack](https://github.com/rustwasm/wasm-pack) | Integración de Rust con JavaScript y empaquetado de módulos WebAssembly. |

## Proyectos de código abierto para estudiar

Los siguientes proyectos permiten observar Rust en contextos diferentes. Algunos integran componentes escritos en otros lenguajes; revisar el repositorio para identificar el alcance actual del código Rust.

| Proyecto | Área | Qué explorar |
|---|---|---|
| [ripgrep](https://github.com/BurntSushi/ripgrep) | CLI / búsqueda | Interfaz de comandos, rendimiento, manejo de archivos y diseño en crates. |
| [Tokio](https://github.com/tokio-rs/tokio) | Runtime asíncrono | API de tareas, I/O, sincronización y documentación de runtime. |
| [Servo](https://github.com/servo/servo) | Motor de navegador | Organización de un sistema grande y trabajo paralelo en componentes. |
| [Deno](https://github.com/denoland/deno) | Runtime JavaScript/TypeScript | Integración de Rust con V8, módulos y herramientas de desarrollo. |
| [Zed](https://github.com/zed-industries/zed) | Editor | Aplicación de escritorio, colaboración y arquitectura de producto. |
| [Polars](https://github.com/pola-rs/polars) | Procesamiento de datos | API de DataFrame y optimización de consultas. |

## Referencias

- [Cargo Book: Package Layout](https://doc.rust-lang.org/cargo/guide/project-layout.html)
- [Cargo Book: Workspaces](https://doc.rust-lang.org/cargo/reference/workspaces.html)
- [Rust Reference](https://doc.rust-lang.org/reference/)
- [Rust project repositories](https://github.com/rust-lang)
