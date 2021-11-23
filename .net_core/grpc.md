# grpc


gRPC services on ASP.NET Core
gRPC services can be hosted on ASP.NET Core. Services have full integration with ASP.NET Core features such as logging, dependency injection (DI), authentication, and authorization.


gRPC uses Protobuf as its Interface Definition Language (IDL). Protobuf IDL is a language neutral format for specifying the messages sent and received by gRPC services. Protobuf messages are defined in .proto files


# protocol 


This guide describes how to use the protocol buffer language to structure your protocol buffer data, including .proto file syntax and how to generate data access classes from your .proto files. It covers the proto3 version of the protocol buffers language
https://developers.google.com/protocol-buffers/docs/proto3


Packages
You can add an optional package specifier to a .proto file to prevent name clashes between protocol message types
https://developers.google.com/protocol-buffers/docs/proto3#packages

repeated: this field can be repeated any number of times (including zero) in a well-formed message. The order of the repeated values will be preserved.
https://developers.google.com/protocol-buffers/docs/proto3#specifying_field_rules


Package name should be in lowercase
https://developers.google.com/protocol-buffers/docs/style#packages

Files should be named lower_snake_case.proto
https://developers.google.com/protocol-buffers/docs/style#file_structure


Use CamelCase (with an initial capital) for message names – for example, SongServerRequest. Use underscore_separated_names for field names (including oneof field and extension names) – for example, song_name.
https://developers.google.com/protocol-buffers/docs/style#message_and_field_names

Use pluralized names for repeated fields.
https://developers.google.com/protocol-buffers/docs/style

If your .proto defines an RPC service, you should use CamelCase (with an initial capital) for both the service name and any RPC method names:
https://developers.google.com/protocol-buffers/docs/style#services


# Client

C# Tooling support for .proto files
The tooling package Grpc.Tools is required to generate the C# assets from *.proto files.

Client projects should directly reference Grpc.Tools alongside the other packages required to use the gRPC client.

Errores
----------------

```
The "AdditionalProtocArguments" parameter is not supported by the "ProtoCompile"
```
can be solved by restarting VS
https://github.com/grpc/grpc/issues/26116#issuecomment-866770710

# Test

Set up gRPC reflection
https://docs.microsoft.com/en-us/aspnet/core/grpc/test-tools?view=aspnetcore-5.0#set-up-grpc-reflection

# Tools


***gRPCurl***

gRPCurl is a command-line tool created by the gRPC community. Its features include:

-    Calling gRPC services, including streaming services.
-    Service discovery using gRPC reflection.
-    Listing and describing gRPC services.
-    Works with secure (TLS) and insecure (plain-text) servers.

Para utilizar gRPCurl, el servidor debe soportar "Service discovery using gRPC reflection."

Ejemplo. Hacer test sobre los servicio grpc que estan en localhost:5000. 

```
grpcurl -plaintext localhost:5000 list
```


***gRPCui***

gRPCui builds on top of gRPCurl and adds an interactive web UI for gRPC, similar to tools such as Postman and Swagger UI.

https://github.com/fullstorydev/grpcui


Ejemplo. Hacer test sobre los servicio grpc que estan en localhost:5000. 

```
grpcui.exe -plaintext localhost:5000
```



***gRPC-Gateway***

The gRPC-Gateway is a plugin of the Google protocol buffers compiler protoc. It reads protobuf service definitions and generates a reverse-proxy server which translates a RESTful HTTP API into gRPC. This server is generated according to the google.api.http annotations in your service definitions.

gRPC to JSON proxy generator following the gRPC HTTP spec 
https://github.com/grpc-ecosystem/grpc-gateway 


# Laboratorios


No existe las clases c# generadas de los archivos proto. Verificar que los archivos a nivel proyecto tenga:
- None Remove
- Protobuf Include

![imagen](https://user-images.githubusercontent.com/222181/137601993-9da1005d-4e73-4ef9-9b58-5a46906699c6.png)


Errores

```
System.Net.Http.HttpRequestException: The SSL connection could not be established, see inner exception.
 ---> System.Security.Authentication.AuthenticationException: Authentication failed, see inner exception.
```

------------

Revision

```
HTTP/2 over TLS is not supported on Windows versions older than W
indows 10 and Windows Server 2016 due to incompatible ciphers or missing ALPN su
pport. Falling back to HTTP/1.1 instead.
```


# Referencias

Getting Started with ASP.NET Core and gRPC
https://blog.jetbrains.com/dotnet/2021/07/19/getting-started-with-asp-net-core-and-grpc/