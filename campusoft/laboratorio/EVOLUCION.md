# Evolución del laboratorio

Registro interno de las estructuras consideradas para `laboratorio/`. Sirve para revisar la decisión cuando el volumen o las necesidades cambien.

## Decisión vigente

**Opción A: un archivo por tema** en una sola carpeta. Elegida por ser la más simple para iniciar. Fecha: 2026-11.

```
laboratorio/
├── README.md
├── EVOLUCION.md
├── _plantilla.md
├── AAAA-MM-tema.md
└── archivo/
```

## Alternativas consideradas

### B. Una carpeta por tema, con pocos archivos

```
laboratorio/AAAA-MM-tema/
├── ESTADO.md       # etapa, responsable, fechas, decisión
├── resumen.md      # fuente, ideas centrales e investigación
├── evaluacion.md   # ¿aplica? y propuesta personalizada
└── piloto.md       # práctica, resultados y decisión final
```

- Ventaja: separa las fases y permite anexos. Sirve para pilotos largos.
- Límite: 4 archivos por tema, aunque se descarte pronto.

### C. Carpetas por etapa

```
laboratorio/
├── 1-por-evaluar/
├── 2-en-piloto/
└── 3-cerrado/
```

- Ventaja: el estado se ve en el explorador.
- Límite: al mover archivos se rompen enlaces y el historial de Git es menos claro.

## Cuándo pasar de A a B

Migrar un tema (no todos) a la opción B cuando ocurra alguna de estas condiciones:

- El archivo supera unas 400 líneas o cuesta navegarlo.
- El piloto genera anexos (datos, capturas, scripts, encuestas).
- Varias personas editan fases distintas del mismo tema a la vez.

Al migrar, el archivo original se reemplaza por la carpeta del mismo nombre y el índice del `README.md` se actualiza.

## Registro de cambios

| Fecha | Cambio |
|---|---|
| 2026-11 | Se adopta la opción A. |
