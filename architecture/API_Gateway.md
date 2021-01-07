# API Gateway
 
 
ESBs disfrazados de API Gateways

Hace tiempo que advertimos en contra de los ESB centralizados y definimos que los “endpoints inteligentes y canales (pipes) tontos” eran una de las características base para las arquitecturas de microservicios. Desafortunadamente, estamos viendo como los ESBs tradicionales están cambiando su imagen y creando ESBs disfrazados de API Gateways , que naturalmente incentivan a crear API Gateways demasiado ambiciosos. Hay que evitar el engaño de la publicidad: independientemente de como se llamen, poner la lógica de negocio (incluyendo la orquestación y transformación) en una herramienta centralizada crea acoplamiento en la arquitectura, reduce la transparencia e incrementa la dependencia en un proveedor sin añadir ventajas. Los API gateways pueden servir como una abstracción útil para aplicar características horizontales (crosscutting concerns), pero estamos convencidos que la inteligencia debe estar en las propias APIs
 
Ref: thoughtworks.com/radar. Radar-vol-23-es.pdf 
 
