# Sql Server - JSON


If you have JSON text that's stored in database tables, you can read or modify values in the JSON text by using the following built-in functions:

-    ISJSON (Transact-SQL) tests whether a string contains valid JSON.
-    JSON_VALUE (Transact-SQL) extracts a scalar value from a JSON string.
-    JSON_QUERY (Transact-SQL) extracts an object or an array from a JSON string.
-    JSON_MODIFY (Transact-SQL) changes a value in a JSON string.

Convert JSON collections to a rowset

OPENJSON transforms the array of JSON objects into a table in which each object is represented as one row, and key/value pairs are returned as cells



JSON data in SQL Server
- Extract values from JSON text and use them in queries
- Convert JSON collections to a rowset
https://docs.microsoft.com/en-us/sql/relational-databases/json/json-data-sql-server?view=sql-server-ver15