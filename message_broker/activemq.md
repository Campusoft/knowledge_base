# Activemq



There are currently two "flavors" of ActiveMQ available - the "classic" 5.x broker and the "next generation" Artemis broker. Once Artemis reaches a sufficient level of feature parity with the 5.x code-base it will become ActiveMQ 6.


# Administracion

Activemq, utiliza topic “advisory” para guardar información, listado de conexiones, listado de queue, listado de topic. Etc. 
https://activemq.apache.org/advisory-message.html



# Protocolos

## AMQP

https://activemq.apache.org/amqp
Working with Destinations with AMQP
You should prefix destination address with queue:// to use queue based destinations or topic:// to use topic based destinations. The destination type defaults to queue when the destination prefix is omitted.

https://activemq.apache.org/amqp

## NMS API

API Overview

The NMS API (.Net Message Service API) provides a standard .NET interface to Messaging Systems. There are multiple implementations for different providers (including MSMQ). The NMS API allows you to build .NET applications in C#, VB, or any other .NET language, using a single API to connect to multiple different providers using a JMS style API.

NMS API currently supports all of the features of JMS in a simple pure C# API and implementation apart from XA

## REST

Se permite consumir mensajes con REST
Mapping of REST to JMS
To publish a message use a HTTP POST. To consume a message use HTTP DELETE or GET.

http://activemq.apache.org/rest.html


https://activemq.apache.org/rest


## MQTT 

v3.1 support allowing for connections in an IoT environment.


# Librerias

Apache.NMS.AMQP

The goal of this project is to combine the .NET Message Service API (NMS) with the Advanced Message Queuing Protocol (AMQP) 1.0 standard wireline protocol. Historically, the Apache community created the NMS API which provided a vendor agnostic .NET interface to a variety of messaging systems.

https://activemq.apache.org/components/nms/providers/amqp/


# Laboratorios


## Configuracion del Almacen de Mensajes

En la carpeta de instalacion existe algunos ejemplos.
examples\conf\activemq-demo.xml

Contiene algunas ejemplos para mysql, Derby,  postgres y Oracle





# Referencias

Artemis Differences From ActiveMQ 5
- message store
- Addressing differences
- JMS 1.1 & 2.0 with full client implementation including JNDI
http://activemq.apache.org/components/artemis/migration

