
# System.Text.Json


Serialize to UTF-8
Deserialize from UTF-8

Add JsonPath support to JsonDocument/JsonElement #31068 
https://github.com/dotnet/runtime/issues/31068

## JsonDocument

The JsonDocument class within the namespace is responsible for examining the structural content of a JSON value, similar to JToken within Json.NET.


## Deserialize dynamic

System.Text.Json provides two ways to build a JSON DOM:
- JsonNode 
- JsonDocument 

Consider the following factors when choosing between JsonDocument and JsonNode:

- The JsonNode DOM can be changed after it's created. The JsonDocument DOM is immutable.
- The JsonDocument DOM provides faster access to its data
	
**Use JsonNode**
https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-6-0#use-jsonnode

**Use JsonDocument**
https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-6-0#use-jsondocument

## Reference

Manipulate JSON with System.Text.Json.Nodes
https://kevsoft.net/2021/12/29/manipulate-json-with-system-text-json-nodes.html


C# JSON
- The JsonElement.EnumerateArray enumerates the values in the JSON array represented by a JsonElement. 
- The Utf8JsonWriter provides a high-performance API for forward-only, non-cached writing of UTF-8 encoded JSON text. 
https://zetcode.com/csharp/json/


# JsonSchema


NJsonSchema is a .NET library to read, generate and validate JSON Schema draft v4+ schemas. The library can read a schema from a file or string and validate JSON data against it. A schema can also be generated from an existing .NET class. With the code generation APIs you can generate C# and TypeScript classes or interfaces from a schema.

https://github.com/RicoSuter/NJsonSchema

System.Text.Json-based support for all of your JSON needs. 
- Generation of schemas from .Net types supported in an additional library
- Random instance data generation (powered by Bogus)
- A vocabulary for accessing instance and external data
- A vocabulary for validating item uniqueness based on specific item values
https://github.com/gregsdennis/json-everything

