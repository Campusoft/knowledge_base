# Activemq



There are currently two "flavors" of ActiveMQ available - the "classic" 5.x broker and the "next generation" Artemis broker. Once Artemis reaches a sufficient level of feature parity with the 5.x code-base it will become ActiveMQ 6.


# Administracion

Activemq, utiliza topic “advisory” para guardar información, listado de conexiones, listado de queue, listado de topic. Etc. 
https://activemq.apache.org/advisory-message.html


$activemq_home\bin\activemq start # Windows
$activemq_home/bin/activemq console #macOS/Linux


The Broker XBean URI allows you to run a configured broker by referencing an Xml Configuration on the classpath. 
https://activemq.apache.org/broker-xbean-uri

ActiveMQ supports advisory messages which allows you to watch the system using regular JMS messages
https://activemq.apache.org/advisory-message


# Conceptos

## prefetch

Informacion: Si se tiene varios clientes, consumiendo una cola X, se puede utiliar el prefetch para limitar cuantos mensajes se reserva para cada cliente.  

https://activemq.apache.org/what-is-the-prefetch-limit-for

## Wildcards

Wildcards and destination hierarchies are useful for adding flexibility to your
applications, allowing for a message consumer to subscribe to more than one destination at a time.

But wildcards only work for consumers. If you publish a message to a topic named
rugby.>, the message will only be sent to the topic named rugby.>, and not all topics
that start with the name “rugby.” There is a way for a message producer to send a message to multiple destinations: by using composite destinations


## Composite Destinations

Composite Destinations

We do this using a simple separator of “,” allowing a number of destinations to be specified when creating a destintation, or registering destinations in JNDI. e.g. the destination

```
FOO.A,FOO.B,FOO.C
```

Mix:

```
FOO.A,topic://NOTIFY.FOO.A
```

Composite destinations can also be configured on the broker side, such that messages sent to a single destination by the client will be transparently copied to multiple physical destinations.

https://activemq.apache.org/composite-destinations

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


ActiveMQ has a Servlet that takes care of the integration between HTTP and the ActiveMQ dispatcher.


Consumir Mensajes

Confguraciones Relacionadas
- prefetch 
  - persistent queues (default value: 1000)
  - Configurar: destinationOptions. Option Name: consumer.prefetchSize  (Info: https://activemq.apache.org/destination-options)
  
  
  
Configuraciones  Servlet
- activemq\webapps\api\WEB-INF


Consuming without sessions

Since 5.2.0 you can use clientId parameter to avoid storing actual JMS consumer in the request session. When using this approach, you don’t need to keep sessions alive between requests, you just need to use the same clientId every time.


Parametros:
- For reading messages you can specify a readTimeout parameter to determine how long the servlet should block for.
  - Example:  api/message/FOO?type=queue&readTimeout=2000
  - Si no se establece, se considera el valor  maximumReadTimeout = 20000 (Milesegundos)
  - El valor maximumReadTimeout, tambien se puede establecer  
- JMSDeliveryMode with value persistent, para mensajes persistentes, con otro valor mensajes no persistentes.

http://activemq.apache.org/rest.html


Codigo

org/apache/activemq/web/MessageServlet.java 


## MQTT 

v3.1 support allowing for connections in an IoT environment.


# Securidad

ActiveMQ 4.x and greater provides pluggable security through various different providers.

The most common providers are

- Simple authentication plug-in— Handles credentials directly in the XML configuration file or in a properties file
- JAAS authentication plug-in— Implements the JAAS API and provides a more powerful and customizable authentication solution


http://activemq.apache.org/security

Indicaciones como realizar encriptacion password

Linux:

```
$ export ACTIVEMQ_ENCRYPTION_PASSWORD=activemq
```

Windows:
```
$ setx ACTIVEMQ_ENCRYPTION_PASSWORD activemq
```

http://activemq.apache.org/encrypted-passwords.html



The advisory topics are part of ActiveMQ, and all users need access to them.

```
  <authorizationEntries>
    ...  
	<authorizationEntry topic="ActiveMQ.Advisory.>" read="everyone" write="everyone" admin="everyone"/>
  </authorizationEntries>
				
```


## Simple Authentication Plugin

With this plugin you can define users and groups directly in the broker’s XML configuration.

From version 5.4.0 onwards, you can configure simple authentication plugin to allow anonymous access to the broker.

Now, when the client connects without username and password provided, a default username (anonymous) and group (anonymous) will be assigned to its security context. 

Ejemplo configuracion con Simple Authentication Plugin

https://github.com/apache/activemq/blob/master/assembly/src/release/examples/conf/activemq-security.xml


## Referencias

to use the same JAAS Authentication Plugin for the web console, you can
execute the following additional steps: 
http://activemq.2283324.n4.nabble.com/jaasAuthenticationPlugin-td4757466.html#a4757490



# Librerias

Apache.NMS.AMQP

The goal of this project is to combine the .NET Message Service API (NMS) with the Advanced Message Queuing Protocol (AMQP) 1.0 standard wireline protocol. Historically, the Apache community created the NMS API which provided a vendor agnostic .NET interface to a variety of messaging systems.

https://activemq.apache.org/components/nms/providers/amqp/


# Laboratorios


## Workers / Process

Si se tiene varios clientes, consumiendo una cola X, se puede utiliar el prefetch para limitar cuantos mensajes se reserva para cada cliente (workers), con esto se establece la carga trabajo para cada cliente.
 

## Configuracion del Almacen de Mensajes

En la carpeta de instalacion existe algunos ejemplos.
examples\conf\activemq-demo.xml

Contiene algunas ejemplos para mysql, Derby,  postgres y Oracle

## Seguridad

En la carpeta de instalacion existe algunos ejemplos.
\examples\conf\activemq-security.xml



# Referencias

Artemis Differences From ActiveMQ 5
- message store
- Addressing differences
- JMS 1.1 & 2.0 with full client implementation including JNDI
http://activemq.apache.org/components/artemis/migration

