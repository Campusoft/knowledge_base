# JMeter



# k6

Diferencia clave

| Tipo de prueba        | Para qué sirve       |
| --------------------- | -------------------- |
| shared-iterations     | Validar flujo        |
| constant-arrival-rate | Medir capacidad      |
| ramping-arrival-rate  | Encontrar límite     |
| stress test           | Ver punto de ruptura |



SharedArray + archivo CSV/JSON (Recomendado)

¿Cómo funciona?

- Guardas los datos en un archivo (CSV o JSON)
- k6 los carga en memoria compartida
- Cada VU toma un registro diferente usando:
- __VU (número de usuario virtual)
- __ITER (iteración actual)

