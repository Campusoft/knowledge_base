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
Establecer explicitamente la Region
```
 var sqsClient = new AmazonSQSClient(RegionEndpoint.USEast2);
```

https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-region-selection.html


Amazon.Runtime.AmazonServiceException : Unable to get IAM security credentials from EC2 Instance Metadata Service.


## Tools

AWS Toolkit for Visual Studio

