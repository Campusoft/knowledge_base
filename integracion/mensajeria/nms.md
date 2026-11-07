# NMS API
 
API Overview

The NMS API (.Net Message Service API) provides a standard .NET interface to Messaging Systems. There are multiple implementations for different providers (including MSMQ). The NMS API allows you to build .NET applications in C#, VB, or any other .NET language, using a single API to connect to multiple different providers using a JMS style API.

NMS API currently supports all of the features of JMS in a simple pure C# API and implementation apart from XA

# Librerias

Apache.NMS
Apache NMS (.Net Standard Messaging Library): An abstract interface to Message Oriented Middleware (MOM) providers
https://www.nuget.org/packages/Apache.NMS/

Apache.NMS.ActiveMQ
Apache NMS (.Net Standard Messaging Library): Openwire implementation of Apache NMS API

The ActiveMQ NMS client is a .NET client that communicates with the ActiveMQ broker using its native Openwire protocol. This client supports advanced features such as Failover, Discovery, SSL, and Message Compression

https://www.nuget.org/packages/Apache.NMS.ActiveMQ/

# Acknowledgement

Mode
- AutoAcknowledge:  With this acknowledgment mode, the session will not acknowledge receipt of a message since the broker assumes successful receipt of a message after the onMessage handler has returned without error. 
- DupsOkAcknowledge:  With this acknowledgment mode, the session automatically acknowledges a client's receipt of a message either when the session has successfully returned from a call to receive or when the message listener the session has called to process the message successfully returns. Acknowlegements may be delayed in this mode to increase performance at the cost of the message being redelivered this client fails. 
- ClientAcknowledge:  With this acknowledgment mode, the client acknowledges a consumed message by calling the message's acknowledge method. This acknowledgement acknowledges the given message and all unacknowedged messages that have preceeded it for the session in which the message was delivered. 
- Transactional:  Messages will be consumed when the transaction commits. 
- IndividualAcknowledge:  With this acknowledgment mode, the client acknowledges a consumed message by calling the message's acknowledge method. This acknowledgement mode allows the client to acknowledge a single message. This mode is not required to be supported by all NMS providers, however the provider should throw an appropriate exception to indicate that the mode is unsupported. 

Se debe iniciar la session especificando el modo ACK en client

```
ISession session = connection.CreateSession(AcknowledgementMode.ClientAcknowledge)
```

Para confirmar, en el mensaje recibido llamar a "Acknowledge()"

```
message.Acknowledge(); 
```


Mode: AcknowledgementMode.Transactional




# Revisiones. 

Propiedad NMSXDeliveryCount = 6
- Esta propiedad lee JMSXDeliveryCount

