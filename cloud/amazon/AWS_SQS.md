# Amazon Simple Queue Service (SQS)

Due to the distributed nature of queues, Amazon SQS can't guarantee that you'll receive messages in the precise order they're sent. If you need to preserve message order, use an Amazon SQS FIFO queue. 

Tipos
- Colas estándar

Capacidad de procesamiento ilimitada: las colas estándar admiten un número casi ilimitado de transacciones por segundo (TPS) para cada acción de la API.
Al menos una entrega: los mensajes se entregan al menos una vez, aunque en ocasiones se entregan varias copias el mensaje.

Orden para optimizar el esfuerzo: en ocasiones, los mensajes se entregan en un orden distinto al que se enviaron.

- Colas FIFO

Procesamiento único: los mensajes se envían una vez y permanecen disponibles hasta que el cliente los procesa y elimina. Los duplicados no se introducen en la cola.
Entrega primero en entrar, primero en salir: se conserva estrictamente el orden en que se envían y reciben los mensajes.

Caracteristicas:
https://aws.amazon.com/es/sqs/features/

# Funcioanmiento de Amazon SQS

Cuando un consumidor recibe y procesa un mensaje de una cola, el mensaje permanece en la cola. Amazon SQS no elimina automáticamente el mensaje. Dado que Amazon SQS es un sistema distribuido, 
no garantiza que el consumidor reciba realmente el mensaje (por ejemplo, debido a un problema de conectividad o a un problema en la aplicación del consumidor). Por tanto, el consumidor debe eliminar el mensaje de la cola después de recibirlo y procesarlo. 

- Arquitectura de Amazon SQS: https://docs.aws.amazon.com/es_es/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-basic-architecture.html

- Amazon SQS utiliza las colas estandar como tipo predeterminado: https://docs.aws.amazon.com/es_es/AWSSimpleQueueService/latest/SQSDeveloperGuide/standard-queues.html

- Identificadores de mensajes: https://docs.aws.amazon.com/es_es/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-queue-message-identifiers.html

Cada vez que se recibe un mensaje, este permanece en la cola. Para evitar que otros consumidores procesen el mensaje de nuevo, Amazon SQS establece untiempo de espera de visibilidad, 
un periodo de tiempo durante el cual Amazon SQS impide que otros consumidores reciban y procesen el mensaje. El tiempo de espera de visibilidad predeterminado de un mensaje es de 30 segundos.
https://docs.aws.amazon.com/es_es/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-visibility-timeout.html

# .net core 

SDK de AWS para .NET
https://aws.amazon.com/es/sdk-for-net/


AWSSDK.SQS
https://www.nuget.org/packages/AWSSDK.SQS/


Configuring AWS Credentials
You must manage your AWS credentials securely and avoid practices that can unintentionally expose your credentials to the public. In this topic, we describe how you configure your application's AWS credentials so that they remain secure
https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-creds.html


----------------
Errores:

Amazon.Runtime.AmazonClientException : No RegionEndpoint or ServiceURL configured

Solucion:
Establecer explicitamente las credenciales y la Region
```
 var awsCredentials = new BasicAWSCredentials(accessKey, secretKey);
 var sqsClient = new AmazonSQSClient(awsCredentials, RegionEndpoint.USEast2);
```

https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-region-selection.html


Amazon.Runtime.AmazonServiceException : Unable to get IAM security credentials from EC2 Instance Metadata Service.


## Tools

AWS Toolkit for Visual Studio

