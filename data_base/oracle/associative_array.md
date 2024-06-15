# PL/SQL Associative Array Binding 
 
ODP.NET supports PL/SQL Associative Arrays (formerly known as PL/SQL Index-By Tables) binding.

- Oracle does not support passing null or empty arrays to the PL/SQL array types. I get around this by sending a one-element array with a predefined value. Later in the procedure I check for this condition and act accordingly.



In Oracle, there are two scopes where statements can be evaluated:

The SQL scope where the Oracle engine will parse SQL statements; and
The PL/SQL scope where the Oracle engine will parse procedural language statements (PL/SQL).
An associative array is a PL/SQL data type and can only be used in the PL/SQL scope. It CANNOT be used in SQL statements so it is impossible to use an associative array directly as you are attempting (and, for some unknown reason, C# does not support passing non-associative arrays).




samples/assoc-array

https://github.com/oracle/dotnet-db-samples/blob/master/samples/assoc-array/AssocArray.cs


Referencias



[Pass PL/SQL associative array to Oracle stored procedure from C# ](http://www.vickram.me/passing-arrays-to-oracle-stored-procedure-from-c)

[ODP.NET - PL/SQL Associative Array Binding](https://docs.oracle.com/en/database/oracle/oracle-data-access-components/19.3/odpnt/featOraCommand.html#GUID-05A6D391-E77F-41AF-83A2-FE86A3D98872)

 
 
