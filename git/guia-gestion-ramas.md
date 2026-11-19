# Guía práctica de gestión de ramas

## 1. Propósito y flujo de referencia

Esta guía describe cómo llevar una tarea desde su inicio hasta producción. Es una referencia adaptable: **no establece una política ya aprobada por el equipo**. Los comandos son ejemplos para ejecutar en un repositorio de trabajo; sustitúyelos por los nombres y rutas de tu proyecto.

Para conocer los conceptos, consulta [Ramas y etiquetas](branch_tag.md). Para comparar estrategias ante situaciones particulares, consulta [Escenarios de gestión de ramas](escenarios-gestion-ramas.md).

### Supuestos del ejemplo

| Rama | Función | Ambiente / destino |
|---|---|---|
| `main` | Línea publicable y registro de versiones aprobadas | Producción mediante el proceso de despliegue acordado |
| `develop` | Integración de cambios que cumplen los criterios de aceptación | Integración o pruebas |
| `feature/123-exportar-reporte` | Una funcionalidad pequeña y revisable | PR/MR a `develop` |
| `fix/124-validar-fecha` | Una corrección ordinaria | PR/MR a `develop` |
| `hotfix/125-error-acceso` | Corrección urgente que parte de producción | PR/MR a `main` y propagación a las líneas activas |

Flujo habitual: rama de tarea → PR/MR a `develop` → validación → PR/MR a `main` → despliegue y verificación.

Este flujo mantiene dos ramas permanentes, pero no incorpora automáticamente todo GitFlow. Si el equipo usa únicamente `main`, las tareas parten de ella y sus PRs apuntan a ella; desaparece la promoción desde `develop`. Si necesita estabilizar versiones por separado, debe definir además el ciclo de `release/*`.

### Decisiones que el equipo debe completar

- Rama que dispara cada despliegue y si producción requiere aprobación o se despliega automáticamente.
- Responsable de aprobar publicaciones, revisores requeridos y checks de CI obligatorios.
- Método de integración permitido, nomenclatura y criterio de versionado.
- Criterios de aceptación, pruebas del producto y mecanismo de recuperación del despliegue.
- Necesidad de mantener `develop`, ramas de release o versiones anteriores.

Antes de adoptar la guía, registrar estas decisiones en el repositorio del producto. Proteger las ramas permanentes con PR/MR, revisión y checks requeridos; evitar cambios directos que omitan ese proceso.

## 2. Empezar una tarea

Requisitos: Git instalado, repositorio clonado, remoto llamado `origin` y acceso al proyecto. Los ejemplos suponen que las ramas permanentes ya existen. Trabajar con el directorio limpio antes de cambiar de rama o integrar cambios; guardar los avances pendientes en la rama correspondiente.

```shell
git status
git fetch origin
git switch develop
git pull --ff-only origin develop
git switch -c feature/123-exportar-reporte
```

Resultado esperado: una rama local nueva desde el último `develop` remoto. `--ff-only` rechaza la actualización si la rama local ha divergido; en ese caso revisar los commits locales antes de continuar, sin descartar trabajo para forzar la actualización.

Usar un nombre que identifique tipo, tarea y propósito. Para una corrección ordinaria, usar por ejemplo `fix/124-validar-fecha`.

## 3. Desarrollar, guardar y sincronizar

Hacer cambios pequeños con un objetivo claro. Revisar qué se va a guardar y añadir explícitamente los archivos de la tarea:

```shell
git diff
git add ruta/al/archivo
git diff --staged
git commit -m "Agrega exportación de reportes"
git push -u origin feature/123-exportar-reporte
```

`ruta/al/archivo` es un marcador: reemplazarlo por una ruta real. Ejecutar las pruebas pertinentes del proyecto antes de solicitar revisión y comprobar que no se incluyan credenciales, archivos generados ni cambios ajenos.

Para incorporar avances de la base, desde la rama de tarea y con el directorio limpio:

```shell
git fetch origin
git merge origin/develop
```

Si otras personas también trabajan sobre la misma rama de tarea, incorporar primero sus commits remotos según el acuerdo del equipo. Si hay conflictos, editar los archivos afectados, revisar el resultado, añadir las rutas resueltas y completar el merge con `git commit`. Después ejecutar las pruebas y publicar con `git push`.

Para cancelar un merge en curso:

```shell
git merge --abort
```

Resultado esperado: la rama de tarea contiene los avances de la base y conserva el cambio previsto. La guía usa merge para sincronizar sin reescribir commits compartidos. Reservar rebase para ramas personales cuando el equipo lo permita; no reescribir ramas compartidas ni forzar pushes como parte del procedimiento habitual.

## 4. Abrir un PR/MR

Desde GitHub o GitLab, crear una solicitud desde la rama de tarea hacia `develop`. Confirmar la rama destino y revisar el diff completo.

Incluir en la descripción:

- Problema que resuelve y referencia a la tarea.
- Comportamiento resultante y alcance del cambio.
- Pruebas realizadas y evidencia pertinente.
- Migraciones, configuración, dependencias o pasos de despliegue necesarios.
- Flag de activación y forma de recuperar el comportamiento anterior, si aplica.

Resultado esperado: una solicitud revisable con CI en ejecución y revisores asignados. Si el trabajo aún no está listo, mantener el PR/MR como borrador. Abrirlo no habilita su integración.

## 5. Revisar e integrar

Integrar cuando el cambio cumpla los criterios de aceptación, tenga las aprobaciones acordadas, los checks obligatorios estén aprobados y las conversaciones estén resueltas. Tras modificarlo, repetir los checks afectados y solicitar nueva revisión cuando corresponda.

| Método | Cuándo considerarlo | Implicación |
|---|---|---|
| Squash merge | Una tarea cuyos commits intermedios no necesitan conservarse | Genera un commit nuevo para el conjunto |
| Merge commit | Se quiere conservar la estructura y ascendencia de las ramas | Conserva los commits y registra la unión |
| Rebase y merge | Se requiere historia lineal y se aceptan commits reescritos | Puede cambiar los identificadores de los commits integrados |

**Propuesta para este ejemplo:** squash para tareas independientes y merge commit para promover `develop` a `main`, preservando la ascendencia entre ramas permanentes. Confirmar que el proyecto permite estos métodos. Evitar promociones repetidas mediante squash entre ramas permanentes, porque no registran la misma relación de integración.

Resultado esperado: cambio integrado en `develop` y despliegue de pruebas validado. Un cambio integrado no equivale todavía a un cambio publicado en producción.

## 6. Publicar una versión

1. Confirmar que el contenido completo que se promoverá está listo. Si hay funcionalidad incompleta no aislada, consultar el [escenario de funcionalidades grandes](escenarios-gestion-ramas.md#escenario-1-desbloquear-despliegues-sin-frenar-funcionalidades-grandes).
2. Abrir un PR/MR de `develop` a `main` y revisar todos los cambios desde la publicación anterior.
3. Validar CI, aceptación, compatibilidad de migraciones y plan de recuperación. Obtener las aprobaciones de publicación.
4. Integrar y ejecutar el despliegue previsto por el proyecto. Si el merge dispara producción automáticamente, completar las aprobaciones antes de integrarlo.
5. Verificar salud del servicio y comportamiento esperado. Registrar el commit y el artefacto realmente desplegados.
6. Etiquetar ese commit conforme al versionado del proyecto y registrar las notas de versión.

Ejemplo de etiqueta anotada, cuando el proceso no la crea automáticamente:

```shell
git fetch origin --tags
git tag -a v1.2.0 COMMIT_VALIDADO -m "Versión 1.2.0"
git push origin v1.2.0
```

Sustituir `COMMIT_VALIDADO` por el SHA verificado y `v1.2.0` por una versión disponible. No asumir que la punta actual de `main` es el commit desplegado. Si los tags disparan el despliegue, crearlos antes de desplegar siguiendo ese pipeline y verificar después; evitar disparar una segunda publicación manual. No mover una etiqueta ya publicada para corregir una versión.

Finalmente, propagar a `develop` cualquier corrección realizada solo en `main`, mediante un PR/MR de sincronización revisado. Conocer qué commit está en producción requiere el registro del despliegue; el nombre de una rama no basta.

## 7. Atender un error urgente

El ejemplo supone que `main` coincide con la versión actualmente en producción. Si contiene cambios aún no desplegados, partir del tag o SHA realmente desplegado y acordar una ruta de publicación que no arrastre cambios ajenos.

Con esa precondición y el directorio limpio:

```shell
git fetch origin
git switch -c hotfix/125-error-acceso origin/main
```

1. Preparar la corrección mínima y verificar el fallo y su solución.
2. Publicar la rama con `git push -u origin hotfix/125-error-acceso`.
3. Abrir el PR/MR hacia `main`, ejecutar los checks y obtener la revisión requerida para incidentes.
4. Publicar, verificar y registrar la nueva versión siguiendo la sección anterior.
5. Abrir un PR/MR de `main` hacia `develop` para conservar la corrección en el trabajo futuro. Revisar también las ramas de release o versiones mantenidas que la necesiten.

Si una línea requiere solo esa corrección y no todo el contenido de `main`, preparar una rama específica y revisar el traslado del cambio por separado. No integrar indiscriminadamente una rama que contiene cambios incompatibles con la versión destino.

Resultado esperado: producción corregida y propagación verificada en las líneas afectadas. Si la publicación falla, ejecutar el mecanismo de recuperación previsto para el producto y evaluar el estado de datos y migraciones antes de revertir código.

## 8. Cerrar el trabajo

Comprobar que el PR/MR se integró, que los checks posteriores pasaron y que no quedan cambios exclusivos pendientes. Actualizar la tarea y eliminar la rama remota cuando ya no tenga consumidores ni PRs dependientes.

Para limpiar la rama local, después de guardar cualquier trabajo pendiente:

```shell
git switch develop
git pull --ff-only origin develop
git branch -d feature/123-exportar-reporte
```

`-d` puede rechazar la eliminación tras squash o rebase porque los commits originales no aparecen como ancestros de la rama destino. En ese caso, comprobar el PR/MR integrado y que no haya trabajo adicional antes de decidir una eliminación forzada; esta guía no la automatiza.

Si la rama remota sigue existiendo y se verificó que ya puede eliminarse:

```shell
git push origin --delete feature/123-exportar-reporte
git fetch origin --prune
```

Resultado esperado: ramas de tarea retiradas sin perder trabajo pendiente. Conservar las ramas permanentes, etiquetas de versiones y ramas que aún tengan una función activa.

## Referencias

Comandos contrastados con la documentación oficial de Git mediante Context7:

- [Crear y cambiar ramas: git switch](https://git-scm.com/docs/git-switch).
- [Actualizar desde remotos: git pull](https://git-scm.com/docs/git-pull).
- [Integrar cambios y cancelar conflictos: git merge](https://git-scm.com/docs/git-merge).
- [Crear etiquetas: git tag](https://git-scm.com/docs/git-tag).
- [Gestionar y eliminar ramas: git branch](https://git-scm.com/docs/git-branch).
- [Publicar y eliminar referencias remotas: git push](https://git-scm.com/docs/git-push).

Las reglas de revisión, ambientes y publicación de esta guía son una propuesta organizativa y deben acordarse por producto.
