# Orca

> Documento revisado: 2026-09-26  
> Producto documentado: Orca de Stably, [onorca.dev](https://www.onorca.dev/)

## Descripción

Orca es un entorno de desarrollo para ejecutar y supervisar varios agentes de programación en paralelo. Cada tarea puede tener su propio *git worktree*, terminal, pestaña de trabajo y navegador, de manera que varios agentes pueden intentar resolver el mismo problema sin modificar directamente el mismo checkout.

Orca no es un modelo de IA ni un proveedor de modelos. Ejecuta agentes CLI que el usuario ya utiliza, como Claude Code, Codex, Cursor CLI, OpenCode y otros. Está pensado para desarrolladores que revisan diferencias, commits y pruebas, no como una herramienta no-code.

## Cómo funciona

El flujo principal es:

1. Se agrega un repositorio local.
2. Se crea un worktree para una tarea a partir de una rama, commit o referencia base.
3. Se inicia un agente CLI dentro de ese worktree.
4. Se pueden crear otros worktrees y ejecutar agentes con el mismo objetivo o con enfoques diferentes.
5. El usuario observa los agentes en paneles divididos.
6. Se comparan los diffs, se anotan cambios, se elige una solución y se hace commit o push.

La unidad de aislamiento es el worktree real de Git. Por lo tanto, Orca aprovecha ramas, commits, diffs y operaciones normales de Git; no reemplaza Git.

## Características

- Ejecución paralela de múltiples agentes de código.
- Worktrees aislados por tarea.
- Terminales, pestañas y paneles divididos para supervisar agentes.
- Soporte para múltiples agentes CLI mediante un selector integrado.
- Visor de diferencias y anotaciones sobre código generado por IA.
- Integraciones de revisión y envío de cambios desde la aplicación.
- Navegador por worktree y modo de diseño para tareas que requieren revisar interfaces.
- Restauración de sesiones e historial de agentes.
- Seguimiento de uso y límites de consumo cuando el agente lo permite.
- Ejecución local, sobre SSH, en servidores Orca autogestionados o en máquinas remotas bajo control del usuario.
- CLI, automatizaciones programadas, skills, MCP y tareas de computer use.
- Disponible para macOS, Windows y Linux.
- Proyecto gratuito y open source según la documentación pública del producto.

## Casos de uso

### Comparar soluciones para un mismo problema

Crear tres worktrees y entregar el mismo bug o requerimiento a Claude Code, Codex y Cursor CLI. Después se comparan los diffs, pruebas, complejidad y riesgos antes de seleccionar una implementación.

### Paralelizar tareas independientes

Ejecutar en paralelo la implementación de una funcionalidad, la generación de pruebas, la actualización de documentación o una investigación técnica, siempre que las tareas no dependan de los mismos cambios sin integrar.

### Supervisar agentes de larga duración

Mantener varios agentes trabajando en tareas separadas y revisar sus estados, terminales, logs y resultados desde una sola interfaz.

### Revisar interfaces web

Usar el navegador asociado a un worktree para validar una aplicación, investigar un defecto visual o proporcionar al agente contexto de una página.

### Trabajar con más capacidad de cómputo

Enviar worktrees y agentes a máquinas remotas mediante SSH o servidores autogestionados cuando el equipo local no tenga suficiente CPU, memoria o acceso a un entorno de pruebas.

### Flujo de revisión humana

Usar Orca como una bandeja de trabajo para producir alternativas, inspeccionar diffs y conservar la decisión final en manos del desarrollador.

## Limitaciones y riesgos

- Orca no incluye por sí mismo el modelo ni necesariamente el acceso al agente; se necesitan las instalaciones, cuentas o suscripciones correspondientes.
- Ejecutar tres agentes sobre una misma tarea puede multiplicar el consumo de tokens, tiempo y costo.
- Los worktrees aíslan los cambios, pero no garantizan que el código generado sea correcto, seguro o fácil de integrar.
- La integración de resultados sigue requiriendo revisión, pruebas y decisiones de Git; descartar o fusionar worktrees incorrectos también es responsabilidad del equipo.
- La documentación indica que algunos lanzamientos pueden iniciar agentes con modos de permisos muy amplios, incluidos equivalentes de “yolo” o bypass de aprobaciones. Esto reduce fricción, pero aumenta el riesgo de comandos destructivos o acceso indebido.
- Los agentes pueden duplicar esfuerzos o producir soluciones incompatibles si el problema no se divide con claridad.
- El trabajo remoto depende de SSH, servidores, credenciales, red y máquinas administradas por el usuario; Orca no convierte automáticamente esos recursos en un servicio gestionado.
- La restauración de una sesión no sustituye el control de versiones ni un registro de decisiones.
- Integraciones, nombres de agentes, opciones de permisos y funciones de automatización pueden cambiar entre versiones.

## Recomendaciones

1. Usar un worktree por tarea y una rama descriptiva; no permitir que varios agentes escriban sobre el mismo checkout.
2. Empezar con modo de permisos manuales. Activar permisos amplios solo en repositorios desechables y entornos aislados.
3. No proporcionar credenciales de producción, llaves privadas ni secretos innecesarios a los agentes.
4. Definir primero criterios de aceptación, comandos de prueba y restricciones técnicas; luego distribuir el mismo encargo entre agentes.
5. Comparar no solo el diff: revisar pruebas, dependencias nuevas, migraciones, rendimiento, seguridad y facilidad de mantenimiento.
6. Medir el costo real de la paralelización. Para tareas pequeñas, un solo agente puede ser más eficiente.
7. Integrar una solución ganadora mediante un commit revisado; conservar o eliminar los worktrees secundarios de forma explícita.
8. Para trabajo remoto, aplicar el mismo control de acceso, aislamiento y respaldo que a cualquier servidor de desarrollo.
9. Fijar versiones durante una evaluación y registrar qué agente, modelo, instrucciones y configuración produjo cada resultado.
10. Tratar las automatizaciones programadas y el computer use como operaciones supervisadas hasta demostrar que son seguras.

## Cuándo elegir Orca

Orca es una buena opción cuando el objetivo principal es comparar o coordinar varios agentes de programación con aislamiento Git, revisión visual de diffs y una interfaz unificada. Es especialmente útil para equipos que ya utilizan agentes CLI y quieren aumentar el paralelismo sin perder el control del repositorio.

No es la primera opción si solo se necesita un terminal persistente, reconectar sesiones o administrar procesos remotos sin una capa de worktrees y revisión de código.

## Fuentes

- [Documentación oficial: qué es Orca](https://www.onorca.dev/docs)
- [Primera sesión con tres agentes](https://www.onorca.dev/docs/first-session)
- [Agentes compatibles y permisos predeterminados](https://www.onorca.dev/docs/agents/supported)
