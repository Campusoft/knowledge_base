
# Security

Postgres Row Level Security

Policies Are Like WHERE Clauses#
Policies are easy to understand once you get the hang of them. You can just think of them as adding a WHERE clause to every query

5.8. Row Security Policies
When row security is enabled on a table (with ALTER TABLE ... ENABLE ROW LEVEL SECURITY), all normal access to the table for selecting rows or modifying rows must be allowed by a row security policy. (However, the table's owner is typically not subject to row security policies.) If no policy exists for the table, a default-deny policy is used, meaning that no rows are visible or can be modified. Operations that apply to the whole table, such as TRUNCATE and REFERENCES, are not subject to row security.
https://www.postgresql.org/docs/current/ddl-rowsecurity.html