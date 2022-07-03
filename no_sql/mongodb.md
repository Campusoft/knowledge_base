# mongodb


MongoDB on the other hand stores data in a JSON-like format in Documents that are stored in a Collection. This means that the structure of data to be stored can be changed for each record and does not require it to be in a pre-defined format. Storage in a JSON-like format also allows data to be nested to allow the record to be more data-rich and expressive

# Availability

MongoDB has a single Master Node controlling multiple Slave Nodes. If the Master Node goes down, an automatic election process starts at the end of which one of the Slave Nodes is elected to become the Master Node. This process can take up to a minute to complete and the database would not respond to any requests in the absence of a Master Node. Hence, even though MongoDB has high availability, it cannot guarantee 100% data availability.

 

# Connection

```
mongodb://<username>:<password>@<server_address>:<port>/<database_name>
```

# Herramientas / Tools

Compass. The GUI for MongoDB.
Compass is an interactive tool for querying, optimizing, and analyzing your MongoDB data. Get key insights, drag and drop to build pipelines, and more.
https://www.mongodb.com/products/compass