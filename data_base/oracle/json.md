# Json

**JSON_VALUE**
 
json_value(<JSON column name>, ‘$.<JSON path>’)
 
 
**JSON_TABLE**
 
Al utlizar json_table, siempre utilizer ERROR ON ERROR

FROM   TCAN_GRADE_MAPPINGS conf
, json_table
(conf.configuration,'$[*]'  ERROR ON ERROR
columns (

JSON_TABLE Options: Error Handling, Nested Path
https://www.oratable.com/json_table-error-handling-nested-path/
 

# Referencias


Querying JSON Data in Oracle: SQL/JSON Query Functions, Dot Notation

https://www.oratable.com/querying-json-data-in-oracle/


Json and .net 
https://github.com/oracle/dotnet-db-samples/tree/master/samples/json


 
 
