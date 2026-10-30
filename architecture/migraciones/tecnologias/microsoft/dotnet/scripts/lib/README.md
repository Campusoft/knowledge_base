# Librería común de los scripts

Funciones reutilizables por los scripts de levantamiento AS-IS. Aquí vive lo que no pertenece a un script concreto: lectura de proyectos, normalización de rutas, redacción de datos sensibles, registro de hallazgos y utilidades de serialización.

## Contenido previsto

```
lib/
├── README.md
└── DotNetAsIs.psm1    # módulo con las funciones exportadas (previsto)
```

| Elemento | Responsabilidad |
|---|---|
| `DotNetAsIs.psm1` | Módulo que agrupa y exporta las funciones comunes. |
| Funciones de rutas | Normalizar rutas absolutas y relativas, comparar y ordenar de forma estable. |
| Funciones de proyectos | Leer XML de `.csproj`, `.vbproj` y `.fsproj`, detectar `projectFormat` (`sdk`/`legacy`), `TargetFrameworks` y consumidores. |
| Funciones de paquetes | Extraer `PackageReference`, `packages.config`, referencias locales y metadatos disponibles. |
| Funciones de configuración | Leer y comparar `app.config`, `web.config` y transforms. |
| Funciones de redacción | Enmascarar cadenas de conexión, tokens y valores sensibles. |
| Funciones de salida | Serializar JSON con `schemaVersion`, generar CSV y ordenar resultados. |
| Funciones de registro | Escribir mensajes, advertencias y hallazgos con evidencia. |

> **Estado:** el módulo aún no se ha implementado. Esta estructura define las responsabilidades antes de escribir código.

## Reglas

- Cada función debe ser **pura o de solo lectura**; no modificar archivos ni el entorno.
- Exportar solo la API pública; el resto permanece interno.
- Documentar parámetros, salida y errores con comentarios de ayuda.
- No depender de módulos externos salvo que sea imprescindible y esté declarado.
- Mantener compatibilidad con PowerShell 7; si se soporta 5.1, probarlo explícitamente.
- Añadir pruebas Pester en [`../tests/`](../tests/README.md) para cada función exportada.

## Cómo agregar una función

1. Verificar que la lógica se reutiliza en más de un script o que aísla una responsabilidad compleja.
2. Implementarla con nombres claros y parámetros tipados.
3. Exportarla en `DotNetAsIs.psm1` y documentarla en este README.
4. Añadir su prueba y un caso de error.
5. Revisar que no introduce acceso a red ni escritura de archivos.

## Referencias

- [Scripts de levantamiento](../README.md)
- [Pruebas](../tests/README.md)
- [Ejemplos y fixtures](../../ejemplos/README.md)
