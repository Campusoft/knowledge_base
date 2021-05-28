# NMS API
 
API Overview

The NMS API (.Net Message Service API) provides a standard .NET interface to Messaging Systems. There are multiple implementations for different providers (including MSMQ). The NMS API allows you to build .NET applications in C#, VB, or any other .NET language, using a single API to connect to multiple different providers using a JMS style API.

NMS API currently supports all of the features of JMS in a simple pure C# API and implementation apart from XA

## Librerias

Apache.NMS
Apache NMS (.Net Standard Messaging Library): An abstract interface to Message Oriented Middleware (MOM) providers
https://www.nuget.org/packages/Apache.NMS/

Apache.NMS.ActiveMQ
Apache NMS (.Net Standard Messaging Library): Openwire implementation of Apache NMS API

The ActiveMQ NMS client is a .NET client that communicates with the ActiveMQ broker using its native Openwire protocol. This client supports advanced features such as Failover, Discovery, SSL, and Message Compression

https://www.nuget.org/packages/Apache.NMS.ActiveMQ/

