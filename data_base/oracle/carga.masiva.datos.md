# Carga Masiva de Datos a Oracle




En Oracle, la función/procedimiento GATHER_TABLE_STATS es utilizada para recolectar estadísticas sobre una tabla específica y sus índices asociados. Estas estadísticas son cruciales para que el Optimizador de Consultas (Oracle Query Optimizer) tome decisiones eficientes al ejecutar consultas SQL.



Cuándo usar GATHER_TABLE_STATS

- Después de grandes operaciones DML: Inserciones, actualizaciones o eliminaciones masivas que cambien significativamente los datos.
- Al crear o modificar índices.
- Cuando la tabla crece o cambia regularmente: Por ejemplo, tablas de transacciones en sistemas bancarios o de comercio.


Consulta para verificar estadísticas

```
SELECT table_name, num_rows, last_analyzed
FROM user_tables
WHERE table_name = 'NOMBRE_TABLA';
```

Consulta para generar estadisticas GATHER_TABLE_STATS

```

BEGIN
   DBMS_STATS.GATHER_TABLE_STATS(
      ownname => 'NOMBRE_PROPIETARIO',
      tabname => 'NOMBRE_TABLA',
      estimate_percent => DBMS_STATS.AUTO_SAMPLE_SIZE, -- Usa una muestra automática para eficiencia
      cascade => TRUE                                -- Actualiza también las estadísticas de los índices asociados
   );
END;
 

```