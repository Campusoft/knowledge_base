# Azure Functions


Use your preferred language: Write functions in C#, Java, JavaScript, PowerShell, or Python, or use a custom handler to use virtually any other language.


# Azure Functions triggers and bindings concepts

Triggers are what cause a function to run. A trigger defines how a function is invoked and a function must have exactly one trigger. Triggers have associated data, which is often provided as the payload of the function.

Binding to a function is a way of declaratively connecting another resource to the function; bindings may be connected as input bindings, output bindings, or both. Data from bindings is provided to the function as parameters.

# Authentication / authorization

Azure Functions supports trigger, input, and output bindings for Azure Cosmos DB.

# Configuration

Working with options and settings
https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#working-with-options-and-settings

***local development***

Local settings file
- The local.settings.json file stores app settings and settings used by local development tools
https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-local#local-settings-file

# Dependency injection



# Cosmos

This set of articles explains how to work with Azure Cosmos DB bindings in Azure Functions 2.x and higher. Azure Functions supports trigger, input, and output bindings for Azure Cosmos DB.
- Supported APIs. Azure Cosmos DB bindings are only supported for use with the SQL API. For all other Azure Cosmos DB APIs, you should access the database from your function by using the static client for your API, including Azure Cosmos DB's API for MongoDB, Cassandra API, Gremlin API, and Table API.

https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2

This set of articles explains how to work with Azure SQL bindings in Azure Functions. Azure Functions supports input and output bindings for the Azure SQL and SQL Server products.
https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-azure-sql


Azure Cosmos DB + Azure Functions - serverless database processing
https://www.youtube.com/watch?v=Dudcl8le2cE

Integrating Azure Cosmos DB with Azure Functions
https://www.youtube.com/watch?v=L88quzuyjDY

# Referencias


Best practices for reliable Azure Functions
https://docs.microsoft.com/en-us/azure/azure-functions/functions-best-practices?tabs=csharp


Azure Function - An Serverless Architecture

- Create Azure Function using Azure Portal
- Create Azure Function using Visual Studio
https://www.c-sharpcorner.com/article/azure-function-an-serverless-architecture/

# Revisar

TNDStudios.Patterns.CQRS
Example of a robust search brokering system using Azure Function Apps & Web UI built with CQRS & MVVM Patterns using .Net Core in C#. 
https://tndstudios.github.io/TNDStudios.Patterns.CQRS/

Caching in Azure Function – how you can use Redis
https://www.jankowskimichal.pl/en/2017/10/caching-in-azure-function-how-you-can-use-redis/

Azure Functions and Caching
You can use distributed cache, e.g. Redis, by using its SDK from Function code. Might be a bit nicer, since you keep the stateless nature of Functions, but might cost a bit more.
https://stackoverflow.com/questions/47722722/azure-functions-and-caching