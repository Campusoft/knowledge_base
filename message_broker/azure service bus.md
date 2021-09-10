# Azure Service Bus
-	Servicio de colas (push)
-	Agente de mensajes de integración empresarial
-	Desacopla aplicaciones y servicios
-	Transferencia asíncrona de datos y estado
-	Formato binario
-	JSON, XML o texto
-	Clientes .net y java
-	Temas
	-	Varios suscriptores de un tema
	-	Eventos de dominio
## Ventajas
-	Sesiones de mensajes para garantizar FIFO (cola)
-	Reenvío automático
-	Cola de fallidos
-	Entrega programada
-	Procesamiento por lotes
-	Detección de duplicados
## Crear
	-	Ingresar en el portal azure
	-	New Service Bus
	-	New Queue
	-	Obtener connection string en Shared access policies
		-	Crear nueva policy o utilizar la que crea por defecto
-	Consumir
	-	.net
		-	https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-portal
		-	Error: NamespaceConnectionString should not contain EntityPath
			-	Eliminar la parte de EntityPath del connection string
## Ejemplo
-	https://github.com/Anexsoft/azure-service-bus-example