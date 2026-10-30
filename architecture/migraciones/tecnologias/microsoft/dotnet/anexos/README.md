# Anexos de migración .NET

Guías detalladas para ejecutar las actividades que la [guía de migración](../framework-a-dotnet.md) describe a nivel de fase. Los anexos no reemplazan la metodología ni crean fases nuevas: explican **cómo** hacer una tarea concreta, con procedimientos, ejemplos, riesgos y validaciones.

## Documentos

| Anexo | Cuándo consultarlo |
|---|---|
| [Conversión de proyectos a SDK-style](conversion-a-sdk-style.md) | Antes de modernizar el formato de un proyecto, migrar `packages.config` o preparar una biblioteca para `netstandard2.0` o multi-target. |
| [.NET Standard 2.0 y multi-targeting](netstandard2.0-y-multitargeting.md) | Al decidir si una biblioteca compartida debe apuntar a `netstandard2.0`, usar multi-target o mantener un destino específico. |
| [Herramientas y validación](herramientas-y-validacion.md) | Para elegir herramientas, ejecutar compilaciones por destino, automatizar la matriz y registrar evidencias. |

## Relación con el resto de la sección

- La [guía de migración](../framework-a-dotnet.md) define estrategias, fases, riesgos y criterios de aceptación. Remite a estos anexos desde las secciones 5, 6 y 15; las secciones 7, 9 y 10 contienen las actividades, riesgos y pruebas que los anexos detallan.
- El [levantamiento AS-IS](../levantamiento-as-is.md) captura el formato de proyecto, los consumidores y las APIs bloqueantes; con esos datos se elige el anexo aplicable.
- Las [plantillas](../plantillas/README.md) y los [scripts](../scripts/README.md) usan los mismos nombres de campos (`projectFormat`, `targetFrameworks`, `consumers`).

## Reglas

- **Un anexo, una tarea.** No mezclar conversión de formato, decisión de destino y automatización en el mismo documento.
- **Autocontenido.** Cada anexo debe poder seguirse sin leer los demás, aunque enlace a ellos.
- **Ejemplos sintéticos.** Sin nombres de clientes, rutas personales ni secretos.
- **Versionado.** Indicar la versión de .NET, del SDK y de las herramientas cuando cambie el procedimiento.
- **Validación explícita.** Todo procedimiento termina con una checklist verificable.
- **Enlaces de vuelta.** Cada anexo enlaza a la guía principal, al AS-IS y a los anexos relacionados.

## Cómo agregar un anexo

1. Confirmar que la tarea no cabe en la guía principal sin perder legibilidad.
2. Definir alcance, requisitos, procedimiento, errores frecuentes, validación y reversión.
3. Crear el archivo en esta carpeta y añadirlo a la tabla anterior.
4. Enlazarlo desde la sección correspondiente de la guía y desde el [índice de la sección](../README.md).
5. Verificar que los enlaces relativos resuelvan y que los ejemplos sean sintéticos.

## Estado

La estructura está definida. Los anexos se incorporan de forma incremental; si un procedimiento aún no está validado en el repositorio, debe indicarse en el documento.
