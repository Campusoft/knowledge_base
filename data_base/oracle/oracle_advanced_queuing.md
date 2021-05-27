# Oracle Advanced Queuing 
 
Advanced Queuing provides database-integrated message queuing functionality. Advanced Queue messages can be stored persistently, propagated between queues on different machines and databases, and transmitted using Oracle Net Services, HTTP(S), and SMTP. Since Oracle Advanced Queuing is implemented in database tables, all the benefits of high availability, scalability, and reliability are applicable to queue data. This technology is presented by DBMS_AQ and DBMS_AQADM packages. DBMS_AQ manages queue messages, allowing enqueuing and dequeuing. DBMS_AQADM controls a queue lifecycle and its properties.  

Interfaces to AQ include PL/SQL, JMS 1.1, JDBC, ODP.NET and OCI. Oracle WebLogic Server applications interoperate with AQ through the JMS API.


Queues and Queue Tables

Messages enqueued in a queue are stored in a queue table. A queue table must be created before creating a queue based on it. Use the DBMS_AQADM PL/SQL package or Oracle Developer Tools for Visual Studio to create and administer queue tables and queues. 


Queue Models There are two models of AQ functionality: Point-to-Point Model and Publish-Subscribe Model. 

AQ supports two queue models, namely point-to-point and publish/subscribe queues. A point-to-point or single-consumer queue is aimed at a specific target.

Applications can set up rules for delivery to consumers, and these rules can be defined on message payload, attributes or both. Subscriber applications can receive messages that match the subscription rules automatically at dequeue time.


AQ Components

- Message 
- Message Queue –Messages are stored in queues and these queues act as “postal boxes” where different applications can look for “mail” in the form of messages. Thus, when one application wants to contact certain applications for certain tasks, it can leave messages in these queues, and the receiving applications will be able to find these messages for processin
- Message Interfaces –AQ supports enqueue, dequeue, and propagation operations that integrate seamlessly with existing applications by supporting popular standards
- Message Handling –AQ supports rule-based routing of messages according to data in the message payload or attributes. Additionally, message transformations can be applied to messages to re-format data before the messages are automatically delivered to target applications or subscribers.


Rules-based Message 

RoutingAQ can intelligently route messages to the right subscribers in a multi-consumer queue or propagate messages to the right queues based on rules specified by each application. The rules can be defined on message properties, message data content, or both. Similar in syntax to the WHEREclause of a SQL query, rules can be expressed in terms of attributes that represent message properties or message content. The rules engine supports faster evaluation of many SQL-92 expressions such as BITAND , CEIL, FLOOR , LENGTH, POWER, CONCAT, LOWER, UPPER, LENGTH, INSTR, SYS_CONTEXT, and UI

![imagen](https://user-images.githubusercontent.com/222181/116788816-6526cc80-aa71-11eb-90cb-48732294b299.png)


In Oracle Streams, logical change records (LCRs) are message payloads that contain information about changes to a database. These changes can include changes to the data, which are data manipulation language (DML) changes, and changes to database objects, which are data definition language (DDL) changes.

When you use Oracle Streams, the capture process captures changes in the form of LCRs and enqueues them into a queue. These LCRs can be propagated from a queue in one database to a queue in another database. Finally, the apply process can apply LCRs at a destination database. You also have the option of creating, enqueuing, and dequeuing LCRs manually

# Information Queue


state

-  DBMS_AQ.READY: The message is ready to be processed.

-  DBMS_AQ.WAITING: The message delay has not yet been reached.

-  DBMS_AQ.PROCESSED: The message has been processed and is retained.

-  DBMSAQ.EXPIRED: The message has been moved to the exception queue.

MESSAGE_PROPERTIES_T Type
https://docs.oracle.com/database/121/ARPLS/t_aq.htm#i997396


# dequeue  operation

DBMS_AQ.DEQUEUE 

Este proceso permite extraer un registro de la cola, se pasa varios parametros

- Parameters
  - queue_name
  - dequeue_options DEQUEUE_OPTIONS_T Type.
  - message_properties OUT. MESSAGE_PROPERTIES_T Type.
  - payload OUT. (La variable para recibir el valor registro que se encuentra en la cola). The payload must be specified according to the specification in the associated queue table.
  - msgid. OUT. 

El tipo DEQUEUE_OPTIONS_T Type,  permite configurar el proceso dequeue.
- La opcion visibility, permite establecer la transaccion para leer el extraer el registro de la cola 
  - ON_COMMIT. The dequeue will be part of the current transaction. This setting is the default. Es decir si no se realiza un commit el mensaje continura en la queue y se registrara un intento (Columna RETRY_COUNT, de la tabla de la cola). ; incluso llamando a DBMS_AQ.DEQUEUE  



# Dequeuing an Array of Messages

Oracle 10g enables us to enqueue and dequeue in bulk.

- DBMS_AQ.ENQUEUE_ARRAY
- DBMS_AQ.DEQUEUE_ARRAY



# JMS


SYS.AQ$_JMS_TEXT_MESSAGE Type
This type is the ADT used to store a TextMessage in an Oracle Database Advanced Queuing queue.
Payload Methods

set_text (payload IN VARCHAR2)

    Sets the payload, a VARCHAR2 value, to an internal representation.
set_text (payload IN CLOB)

    Sets the payload, a CLOB value, to an internal representation.
get_text (payload OUT VARCHAR2)

    Puts the internal representation of the payload into a VARCHAR2 variable payload.
get_text (payload OUT CLOB)

    Puts the internal representation of the payload into a CLOB variable payload.

https://docs.oracle.com/database/121/ARPLS/t_jms.htm#ARPLS71811



# Exception queue



# Programmatic Interfaces for Accessing Oracle Database Advanced Queuing

Using PL/SQL to Access Oracle Database Advanced Queuing

The PL/SQL packages DBMS_AQADM and DBMS_AQ support access to Oracle Database Advanced Queuing administrative and operational functions using the native Oracle Database Advanced Queuing interface. 


Oracle Java Message Service (Oracle JMS) 
aqapi.jar
Oracle's implementation of JMS specification in compliance with JMS 1.1  
https://mvnrepository.com/artifact/com.oracle.database.messaging/aqapi


Registering for Notification

This procedure registers an e-mail address, user-defined PL/SQL procedure, or HTTP URL for message notification. 

DBMS_AQ.REGISTER

https://docs.oracle.com/en/database/oracle/oracle-database/19/adque/aq-operations-using-pl-sql.html#GUID-DBBE096A-EEA0-4E1E-AC3F-6E1B492D1FB5

# Notification

Oracle will notify an agent to execute a registered PL/SQL "callback" procedure (alternatively, the agent can notify an email address or http:// address rather than execute a callback procedure).

# Security 

To enqueue or dequeue, users need EXECUTE rights on DBMS_AQ and either ENQUEUE or DEQUEUE privileges on target queues

# .NET



Note:

ODP.NET, Managed Driver does not support the AQ .NET classes. 



# Node (Javascript)


https://github.com/oracle/node-oracledb

Node-oracledb APIs for AQ were introduced in node-oracledb 4.0. With earlier versions, use AQ’s PL/SQL interface.
https://oracle.github.io/node-oracledb/doc/api.html#aq


# Referencias


Se puede utilizar The DBMS_AQ package in Oracle XE. Si se puede utilizar. Se debe dar permisos permisos de ejecucion al PK DBMS_AQ al usuario que se desee trabajar.


API DBMS_AQ Documentacion
https://docs.oracle.com/database/121/ARPLS/d_aq.htm#ARPLS096

API DBMS_AQ.ENQUEUE Procedure Documentacion
https://docs.oracle.com/database/121/ARPLS/d_aq.htm#ARPLS097


introduction to advanced queuing
http://www.oracle-developer.net/display.php?id=411

array-based advanced queuing in 10g
http://www.oracle-developer.net/display.php?id=319

Configuring JMS nodes to communicate with Oracle AQ
https://www.ibm.com/docs/en/integration-bus/10.0?topic=jms-configuring-nodes-communicate-oracle-aq


# Errores


  Message=ORA-01031: insufficient privileges
ORA-06512: at "SYS.DBMS_AQ", line 750
ORA-06512: at "BANINST1.PKG_UTPL_BEP", line 58
ORA-06512: at line 1
  Source=Oracle Data Provider for .NET, Managed Driver
  
  