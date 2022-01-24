# Abp MongoDb



The startup template disables transactions in the .MongoDB project by default. If your MongoDB server supports transactions, you can enable it in the YourProjectMongoDbModule class's ConfigureServices method
https://docs.abp.io/en/abp/latest/Getting-Started-Create-Solution?UI=NG&DB=Mongo&Tiered=No#mongodb-transactions


# Errores


-------------------

```
Unable to authenticate using sasl protocol mechanism SCRAM-SHA-1.
```
The reason for this was MongoDB authentication was not happening against the admin DB somehow This can be fixed by adding auth source to connection string like

```
"ConnectionStrings": {
	"MongoConnection": "mongodb://user:password@ipaddress:port/DbName?authSource=admin"
}
```

https://stackoverflow.com/questions/67784616/hangfire-mongodb-net-core-unable-to-authenticate-using-sasl-protocol-mechanism

----------------