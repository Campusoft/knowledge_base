# Oracle Sql Developer
 

## Connections

### Postgres

No se permite establecer el nombre de la base de datos, realizar un export conexion, cambiar el archivo, luego 
hacer un import estableciendo la db en URL "customUrl"
https://www.enterprisedb.com/postgres-tutorials/how-connect-postgresql-using-sql-developer-visual-studio-and-dbeaver 
 
### Mysql


------------------------------

Al ejecutar script con DELIMITER, posiblemente no es soportado oracl sql developer. 

```
--
-- Disparadores `orderlines`
--
DELIMITER $$
CREATE TRIGGER `restar_stock` AFTER INSERT ON `orderlines` FOR EACH ROW BEGIN
UPDATE products
SET products.stock = products.stock - new.quantity
where products.id = new.product_id;
END
$$
DELIMITER ;
```
Error:
You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near '$$
DELIMITER' at line 6

The DELIMITER is not a MySQL command, this command has to be supported by client. The DELIMITER helps client to parse statements in a script.

https://stackoverflow.com/questions/11353567/mysql-procedure-in-oracle-sqldeveloper 
 
----------------------------

 
## Reference

MySQL : Connections in SQL Developer
https://oracle-base.com/articles/mysql/mysql-connections-in-sql-developer

How to create SQL query or plsql coding template in Oracle SQL developer.
Crear bloques codigo repetidos
https://uthamaselvan.wordpress.com/2017/10/10/how-to-create-sql-query-or-plsql-coding-template-in-oracle-sql-developer/ 
 
