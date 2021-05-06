# Oracle Advanced Queuing 
 

Interfaces to AQ include PL/SQL, JMS 1.1, JDBC, ODP.NET and OCI. Oracle WebLogic Server applications interoperate with AQ through the JMS API.


Queues and Queue Tables

Messages enqueued in a queue are stored in a queue table. A queue table must be created before creating a queue based on it. Use the DBMS_AQADM PL/SQL package or Oracle Developer Tools for Visual Studio to create and administer queue tables and queues. 


Queue Models

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



# .NET



Note:

ODP.NET, Managed Driver does not support the AQ .NET classes. 



# Node (Javascript)


https://github.com/oracle/node-oracledb

Node-oracledb APIs for AQ were introduced in node-oracledb 4.0. With earlier versions, use AQ’s PL/SQL interface.
https://oracle.github.io/node-oracledb/doc/api.html#aq
