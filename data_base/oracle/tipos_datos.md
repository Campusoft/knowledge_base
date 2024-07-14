# Tipos datos

## record:

   Cannot be stored in the database.
   Cannot be recursively referenced.
   Cannot have logic defined as part of their definition.



## Object

object:

   Can be stored as a database table column or as an entire row.
   Can be recursively referenced using the SELF parameter.
   Can have logic defined as part of their definition using member methods.


object type is a database object that must be declared using the CREATE statement.


## CLOB


.net work clob. (Examples)
https://github.com/oracle/dotnet-db-samples/tree/master/samples/lob





## Arrays 

Working with Collections (Array,  Associative array - Variable-size array )
Part 8 in a series of articles on understanding and using PL/SQL
Collections are used in some of the most important performance optimization features of PL/SQL
https://blogs.oracle.com/oraclemagazine/working-with-collections

You can't use a locally declared collection in an SQL clause:

```
PLS-00642: local collection types not allowed in SQL statements
```

https://stackoverflow.com/questions/16185277/oracle-collection-in-where-clause


## Collecciones


-    Associative arrays, also known as index-by tables, let you look up elements using arbitrary numbers and strings for subscript values. These are similar to hash tables in other programming languages.

-    Nested tables hold an arbitrary number of elements. They use sequential numbers as subscripts. You can define equivalent SQL types, allowing nested tables to be stored in database tables and manipulated through SQL.

-    Varrays (short for variable-size arrays) hold a fixed number of elements (although you can change the number of elements at runtime). They use sequential numbers as subscripts. You can define equivalent SQL types, allowing varrays to be stored in database tables. They can be stored and retrieved through SQL, but with less flexibility than nested tables.

Trabajar con una colleccion, igual como se fuera una table. 

Several versions back of Oracle Database, Oracle added the table operator, which transforms a collection's contents into a relational dataset that can be consumed by a SELECT. The syntax looks like:

```
SELECT column_list FROM TABLE (my_collection)
```


If the TYPE statement contains an INDEX BY clause, the collection type is an associative array.
If the TYPE statement contains the VARRAY keyword, the collection type is a varray.
If the TYPE statement does not contain an INDEX BY clause or a VARRAY keyword, the collection type is a nested table.

NESTED TABLE
Al recuperar los elementos, estos estan ordenados si el indice es texto, esta ordenado alfabeticamente. 


# generic datatypes

 ANYTYPE, ANYDATA, and ANYDATASET


