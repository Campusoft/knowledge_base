# Escenarios de gestión de ramas en Git

Este documento reúne situaciones prácticas, alternativas y criterios para decidir cómo organizar las ramas. Complementa la [introducción a GitFlow](branch_tag.md); las estrategias deben adaptarse al producto y a su forma de publicar versiones.

## Escenario 1: desbloquear despliegues sin frenar funcionalidades grandes

### Situación y objetivo

El equipo trabaja con `main` para producción y `develop` como rama de integración. Una funcionalidad grande necesita varios PRs/MRs. Si sus partes incompletas se integran y afectan el comportamiento existente, `develop` deja de estar lista para publicar y bloquea fixes o cambios menores.

El objetivo es poder publicar cambios independientes mientras continúa el desarrollo grande. Tener código incompleto integrado no es por sí mismo el problema: lo es que ese código impida desplegar con seguridad.

### Propuesta aportada

- `main` representa producción y sus cambios aprobados disparan el despliegue a ese ambiente.
- `develop` recibe fixes, cambios menores y funcionalidades grandes ya terminadas.
- Cada funcionalidad grande tiene una rama aislada, por ejemplo `feature/grande`.
- Los PRs parciales apuntan a esa rama padre; al terminar, se abre un PR hacia `develop`.

**Aclaración de ambientes:** en este escenario se asume que `develop` despliega automáticamente a integración o pruebas y que los cambios validados se promueven a `main`. Si ambas ramas despliegan a producción, hay que definir una única ruta de publicación y la relación entre sus historiales.

El material aportado indica que su autor adoptó la rama padre. No se proporcionó el enlace original: esa atribución y la frecuencia de las recomendaciones no están verificadas.

### Alternativa A: integración frecuente con feature flags

Integrar incrementos pequeños en la rama compartida, manteniendo desactivado el comportamiento nuevo hasta que esté listo. Las flags permiten separar despliegue de activación, aunque añaden complejidad de pruebas y mantenimiento. Véase [Feature Toggles, de Pete Hodgson](https://martinfowler.com/articles/feature-toggles.html).

**Cuándo elegirla:** cuando el cambio puede convivir con el comportamiento actual y cada incremento es seguro para desplegar.

Pasos propuestos para este escenario:

1. Identificar qué comportamiento controla la flag y dejarlo desactivado por defecto en producción.
2. Dividir el trabajo en PRs pequeños que mantengan la compilación y las pruebas del comportamiento vigente.
3. Validar los estados activado y desactivado, incluyendo procesos en segundo plano y permisos.
4. Activar la funcionalidad tras validarla y definir cómo volver al comportamiento previo.
5. Retirar la flag y el código anterior cuando termine la transición.

**Riesgos:** ocultar una pantalla no aísla cambios en datos, contratos o procesos. Las migraciones deben ser compatibles con las versiones que conviven. En rediseños amplios, evaluar si hace falta introducir una capa de abstracción o una transición por módulos; el tamaño del cambio no demuestra por sí solo que las flags sean insuficientes.

### Alternativa B: rama padre de funcionalidad con PRs pequeños

Mantener temporalmente la funcionalidad fuera de `develop`, revisando sus partes mediante PRs hacia `feature/grande`.

**Cuándo elegirla:** cuando todavía no se puede integrar el cambio de forma segura y el equipo acepta el coste de una integración diferida.

Flujo propuesto:

1. Crear `feature/grande` desde una versión validada de `develop`.
2. Crear ramas de subtareas desde ella y dirigir sus PRs a la rama padre.
3. Ejecutar CI en cada PR y en la rama padre; disponer de un ambiente para probar la funcionalidad completa.
4. Incorporar regularmente los cambios de `develop` a la rama padre y resolver incompatibilidades durante el desarrollo.
5. Seguir publicando cambios independientes por `develop` → `main`.
6. Cuando la funcionalidad esté lista, abrir el PR final a `develop`, validar el conjunto y promoverlo a `main` por el flujo habitual.

**Riesgos:** los PRs parciales facilitan la revisión, pero el PR final sigue acumulando el cambio completo. La divergencia, los conflictos y la validación conjunta crecen con la duración de la rama. Definir responsable y revisiones periódicas de su necesidad; dividir y adelantar las partes que puedan publicarse de forma independiente.

### Alternativa C: GitFlow con ramas de release y hotfix

GitFlow organiza funcionalidades, preparación de versiones y correcciones urgentes con ramas distintas. Encaja mejor en publicaciones planificadas; sus ramas duraderas pueden dificultar CI/CD. La [guía de Atlassian](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) describe sus roles y limitaciones.

**Cuándo elegirla:** cuando el producto necesita una fase de estabilización y versiones programadas.

- Las funcionalidades se integran en `develop` cuando cumplen los criterios acordados.
- Una rama `release/*` se corta desde un estado publicable de `develop` para estabilizar esa versión.
- Una corrección urgente puede partir de `main` mediante `hotfix/*`; después debe regresar a las líneas activas que la necesiten.

**Límite clave para este escenario:** crear una release desde un `develop` que ya contiene cambios incompletos arrastra esos cambios. GitFlow por sí solo no elimina el bloqueo; hace falta aislarlos, desactivarlos de forma segura o partir de un estado publicable conocido.

Integrar funcionalidades nuevas en una rama de release cambia su papel: pasa a ser una rama de integración y necesita validación adicional. Para estabilizar una versión, conviene acordar qué correcciones admite y evitar añadir alcance nuevo.

### Complemento: Git worktrees

Las worktrees permiten trabajar con varias ramas en directorios separados del mismo repositorio. Ayudan a atender un fix sin desmontar el trabajo local de una funcionalidad, pero no deciden qué cambios se publican ni resuelven la integración. Véase la [documentación oficial de Git worktree](https://git-scm.com/docs/git-worktree).

### Criterios de decisión para este escenario

| Condición | Estrategia que conviene evaluar | Coste principal |
|---|---|---|
| El código nuevo puede convivir desactivado con el actual | PRs pequeños con feature flags | Pruebas de estados y retirada de flags |
| El cambio todavía no puede convivir con la versión publicable | Rama padre temporal con PRs pequeños | Divergencia e integración final |
| Hay versiones programadas con estabilización | Ramas de release dentro del flujo acordado | Mantener varias líneas sincronizadas |
| Hay que atender un incidente mientras continúa otra tarea | Worktrees como complemento | Gestión de directorios y entornos locales |

### Conclusión propuesta

Mantener una línea publicable y acordar qué significa que un cambio está listo para entrar en ella. Como criterio inicial, favorecer incrementos seguros e integración frecuente; usar una rama padre temporal cuando esa convivencia aún no sea viable. Añadir ramas de release si existe una necesidad de estabilización, y worktrees para facilitar el trabajo local.

Antes de adoptar el flujo, concretar: qué rama despliega a cada ambiente, qué pruebas y revisiones exige la promoción, cómo se propagan los hotfixes y quién decide que una funcionalidad puede activarse o integrarse.

## Documentación relacionada

- [Ramas y etiquetas: introducción a GitFlow](branch_tag.md).
- [Taller Git Essentials](git.md).
- [Catálogo de Git y GitHub](catalogo-git-y-github.md).
