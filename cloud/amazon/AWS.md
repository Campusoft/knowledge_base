# Amazon


Nombres de recursos de Amazon (ARN)

Los nombres de recursos de Amazon (ARN) identifican de forma exclusiva los recursos de AWS
arn:partition:service:region:account-id:resource-id

https://docs.aws.amazon.com/es_es/general/latest/gr/aws-arns-and-namespaces.html


Get your access key ID and secret access key

# IAM Role


Un IAM Role (Identity and Access Management Role) es, en esencia, una identidad virtual con permisos definidos que no está asociada a una persona específica, sino que puede ser adoptada por cualquier entidad (un usuario, una aplicación o un servicio) que tenga autorización para hacerlo.

A diferencia de un IAM User (que tiene un nombre, una contraseña y llaves de acceso permanentes), un IAM Role no tiene credenciales propias a largo plazo.

Definición Técnica
Es un recurso de AWS (o de cualquier nube moderna) que define un conjunto de permisos para realizar solicitudes a los servicios de la plataforma. Se compone de dos documentos fundamentales:

- Trust Policy (Política de Confianza): Es un JSON que responde a la pregunta "¿Quién puede usar este rol?". Define las entidades confiables (servicios como EC2, cuentas de AWS externas o usuarios específicos).
- Permissions Policy (Política de Permisos): Es un JSON que responde a la pregunta "¿Qué puede hacer el que use este rol?". Enumera las acciones permitidas (ej. s3:ListBucket, dynamodb:PutItem).



# IAM users

# Observabilidad

The first and most important rule of microservice logging is those logs should go to a single place.

AWS Cloudwatch 




#  Varios.


Amazon Simple Notification Service (Amazon SNS) es un servicio de mensajería completamente administrado para la comunicación aplicación a aplicación (A2A) y aplicación a persona (A2P). 



Amazon SQS (from AWS) - The Ultimate Guide.
(Tiene buena informacion)
https://www.serverless.com/amazon-sqs

Basic Amazon SQS architecture
https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-basic-architecture.html


## Cloudtrail

AWS CloudTrail es un servicio que le permite realizar auditorías de gobernanza, de conformidad, operativas y de riesgo en su cuenta de AWS.


# Buenas practicas


Prowler is a command line tool that helps you with AWS security assessment, auditing, hardening and incident response.

It follows guidelines of the CIS Amazon Web Services Foundations Benchmark (49 checks) and has more than 100 additional checks including related to GDPR, HIPAA, PCI-DSS, ISO-27001, FFIEC, SOC2 and others.

https://github.com/toniblyx/prowler

# CloudFront

AWS CloudFront es un servicio de red de entrega de contenido (CDN) de Amazon Web Services que acelera la distribución global de contenido estático y dinámico, como archivos web, imágenes, videos y APIs, a través de una red mundial de ubicaciones de borde (Edge Locations).

CloudFront recibe solicitudes de usuarios y las dirige automáticamente a la Edge Location más cercana para minimizar la latencia, utilizando la red troncal de AWS para una entrega rápida. Si el contenido no está en caché, lo obtiene del origen (como S3, EC2 o un servidor personalizado), lo almacena temporalmente en la caché de borde y lo entrega al usuario, reduciendo la carga en el servidor original en solicitudes futuras