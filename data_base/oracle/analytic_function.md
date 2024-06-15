# Oracle Analytic Functions

Oracle analytic functions calculate an aggregate value based on a group of rows and return multiple rows for each group.


# LAG

Oracle LAG() is an analytic function that allows you to access the row at a given offset prior to the current row without using a self-join.


```
SELECT
    cliente_id,
    fecha,
    monto,
    LAG(monto, 1, 0) OVER (PARTITION BY cliente_id ORDER BY fecha) AS monto_anterior
FROM
    ventas
ORDER BY
    cliente_id,
    fecha;
```

Explicación:
- LAG(monto, 1, 0) OVER (PARTITION BY cliente_id ORDER BY fecha): LAG(monto, 1, 0): LAG obtiene el valor de monto de la fila anterior. El segundo argumento 1 indica que queremos la fila inmediatamente anterior. El tercer argumento 0 es el valor predeterminado que se devuelve si no hay una fila anterior (por ejemplo, para la primera fila de un cliente).

- OVER (PARTITION BY cliente_id ORDER BY fecha): Esta cláusula especifica que la función LAG debe reiniciarse para cada cliente_id y ordenar las filas por fecha.

# LEAD



# OVER


Cálculo de la suma de cada grupo - SUM() con OVER(PARTITION BY ...)


Totalizar el salary por cada deparmento (dept_id)

```
SELECT
  emp_id,
  name,
  job,
  dept_id,
  salary,
  SUM(salary) OVER(PARTITION BY dept_id) AS dept_total_salary
FROM employees;
```