# gRPC
 

gRPC uses Protobuf as its Interface Definition Language (IDL). Protobuf IDL is a language neutral format for specifying the messages sent and received by gRPC services. Protobuf messages are defined in .proto files.


gRPC recommended scenarios

gRPC is well suited to the following scenarios:

    Microservices: gRPC is designed for low latency and high throughput communication. gRPC is great for lightweight microservices where efficiency is critical.
    Point-to-point real-time communication: gRPC has excellent support for bi-directional streaming. gRPC services can push messages in real-time without polling.
    Polyglot environments: gRPC tooling supports all popular development languages, making gRPC a good choice for multi-language environments.
    Network constrained environments: gRPC messages are serialized with Protobuf, a lightweight message format. A gRPC message is always smaller than an equivalent JSON message.
    Inter-process communication (IPC): IPC transports such as Unix domain sockets and named pipes can be used with gRPC to communicate between apps on the same machine. For more information, see Inter-process communication with gRPC.


Limited browser support
https://docs.microsoft.com/en-us/aspnet/core/grpc/comparison?view=aspnetcore-3.1#limited-browser-support

 
# Entrenamiento

## Basicos
 
https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-3.1&tabs=visual-studio



##Intermedios


##  Avanzados


# Tools

 An interactive web UI for gRPC, along the lines of postman 
https://github.com/fullstorydev/grpcui

# Referencias


