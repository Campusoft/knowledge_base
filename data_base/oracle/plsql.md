# PL/SQL


# BULK COLLECT and FORALL


Bulk data processing with BULK COLLECT and FORALL in PL/SQL

https://blogs.oracle.com/connect/post/bulk-processing-with-bulk-collect-and-forall

## Referencias

**Revisiones**
Un cambio que puede mejorar el rendimiento aún mas, es que que en el FORALL, sustituir el m_rowid.count, por m_rowid.last.

En este caso, que las tablas cargan 100 elementos no se notara mucho, pero cuando se carga un número mucho mas grande si se nota el cambio, ya que no tarda lo mismo Oracle en decirte cuantos elementos hay que cual es el último elemento.


# Paquetes Importantes

## DBMS_SQL

The DBMS_SQL package provides an interface for using dynamic SQL to execute data manipulation language (DML) and data definition language (DDL) statements, execute PL/SQL anonymous blocks, and call PL/SQL stored procedures and functions.


## DBMS_PIPE


The DBMS_PIPE package lets two or more sessions in the same instance communicate. Oracle pipes are similar in concept to the pipes used in UNIX, but Oracle pipes are not implemented using the operating system pipe mechanisms.


## Pagination

Oracle implemented ANSI standards for data paging in 12c release.