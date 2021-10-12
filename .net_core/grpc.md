# grpc


gRPC services on ASP.NET Core
gRPC services can be hosted on ASP.NET Core. Services have full integration with ASP.NET Core features such as logging, dependency injection (DI), authentication, and authorization.


gRPC uses Protobuf as its Interface Definition Language (IDL). Protobuf IDL is a language neutral format for specifying the messages sent and received by gRPC services. Protobuf messages are defined in .proto files


# Test


Set up gRPC reflection
https://docs.microsoft.com/en-us/aspnet/core/grpc/test-tools?view=aspnetcore-5.0#set-up-grpc-reflection

# Tools


The gRPC-Gateway is a plugin of the Google protocol buffers compiler protoc. It reads protobuf service definitions and generates a reverse-proxy server which translates a RESTful HTTP API into gRPC. This server is generated according to the google.api.http annotations in your service definitions.

gRPC to JSON proxy generator following the gRPC HTTP spec 
 
https://github.com/grpc-ecosystem/grpc-gateway 