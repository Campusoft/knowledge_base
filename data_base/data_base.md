
***Why Naming Conventions Are Important***

Names Are Contracts

Database objects are referenced by their names, thus object names are part of the contract for an object. In a way you can consider your database table and column names to be the API to your data model.

Once they are set, changing them may break dependent applications. This is all the more reason to name things properly before the first use.


Developer Context Switching

Having consistent naming conventions across your data model means that developers will need to spend less time looking up the names of tables, views, and columns. Writing and debugging 
# IDs

## Snowflake IDs

Snowflake is a service used to generate unique IDs for objects within Twitter (Tweets, Direct Messages, Users, Collections, Lists etc.). These IDs are unique 64-bit unsigned integers, which are based on time, instead of being sequential. The full ID is composed of a timestamp, a worker number, and a sequence number.

# Tools

DBeaver

Free multi-platform database tool for developers, SQL programmers, database administrators and analysts.
Supports any database which has JDBC driver (which basically means - ANY database). Commercial versions also support non-JDBC datasources such as MongoDB, Cassandra, Couchbase, Redis, BigTable, DynamoDB, etc


https://github.com/dbeaver/dbeaver