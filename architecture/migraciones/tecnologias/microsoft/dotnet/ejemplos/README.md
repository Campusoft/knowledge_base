# Ejemplos y fixtures

Ejemplos **sintéticos** que muestran la salida esperada del levantamiento y sirven como datos de prueba para los scripts. No contienen información real de clientes, servidores ni credenciales.

## Contenido previsto

```
ejemplos/
├── README.md
├── fixture/                       # solución .NET de ejemplo para pruebas (previsto)
├── inventario-ejemplo.json        # inventario canónico de muestra (previsto)
├── inventario-ejemplo.csv         # versión plana para revisión (previsto)
├── grafo-dependencias-ejemplo.mmd # grafo Mermaid (previsto)
└── informe-as-is-ejemplo.md       # informe resumido (previsto)
```

| Elemento | Propósito |
|---|---|
| `fixture/` | Solución pequeña con proyectos SDK y legado, referencias, paquetes y configuraciones representativas. |
| `inventario-ejemplo.json` | Mostrar el esquema completo y los valores esperados. |
| `inventario-ejemplo.csv` | Mostrar la proyección plana para revisión. |
| `grafo-dependencias-ejemplo.mmd` | Mostrar el formato del grafo y un caso con dependencia compartida. |
| `informe-as-is-ejemplo.md` | Mostrar la estructura y el nivel de detalle del entregable. |

> **Estado:** los ejemplos aún no se han creado. Esta estructura define qué se incluirá y con qué reglas.

## Reglas

- **Datos sintéticos:** nombres como `SistemaEjemplo`, `Proyecto.Web` o `EmpresaDemo`; nunca datos de un cliente.
- **Sin secretos:** cadenas de conexión falsas, tokens inexistentes y rutas genéricas.
- **Tamaño mínimo:** el fixture debe ser pequeño, comprensible y rápido de analizar.
- **Cobertura:** incluir al menos un proyecto SDK, un proyecto legado, una referencia entre proyectos, un paquete NuGet, `packages.config`, `app.config` o `web.config` y un uso de API a migrar.
- **Consistencia:** los ejemplos deben validar contra [`plantillas/inventario-esquema.json`](../plantillas/README.md) cuando exista.
- **Mantenimiento:** actualizar los ejemplos cuando cambie el esquema, el informe o el formato del grafo.

## Uso

- Los scripts de [`scripts/`](../scripts/README.md) pueden ejecutarse contra `fixture/` para generar una salida de referencia.
- Las pruebas de [`scripts/tests/`](../scripts/tests/README.md) usan el fixture para validar resultados.
- El informe de ejemplo sirve como referencia de tono, secciones y nivel de detalle para un AS-IS real.

## Cómo agregar un ejemplo

1. Definir qué comportamiento o formato se quiere mostrar.
2. Crear los datos sintéticos y verificar que no contienen información sensible.
3. Generar la salida con los scripts cuando estén disponibles.
4. Revisar que sea legible y que no incluya ruido innecesario.
5. Enlazarlo desde el [levantamiento AS-IS](../levantamiento-as-is.md) o desde la plantilla correspondiente.

## Referencias

- [Levantamiento AS-IS](../levantamiento-as-is.md)
- [Plantillas](../plantillas/README.md)
- [Scripts](../scripts/README.md)
- [Pruebas](../scripts/tests/README.md)
