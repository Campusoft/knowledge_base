

To identify and select the optimal model(s), architects must understand advantages and disadvantages of each model 


Modular Monolith with DDD
https://github.com/kgrzybek/modular-monolith-with-ddd
 
 
 
ESBs disfrazados de API Gateways

Hace tiempo que advertimos en contra de los ESB centralizados y definimos que los “endpoints inteligentes y canales (pipes) tontos” eran una de las características base para las arquitecturas de microservicios. Desafortunadamente, estamos viendo como los ESBs tradicionales están cambiando su imagen y creando ESBs disfrazados de API Gateways , que naturalmente incentivan a crear API Gateways demasiado ambiciosos. Hay que evitar el engaño de la publicidad: independientemente de como se llamen, poner la lógica de negocio (incluyendo la orquestación y transformación) en una herramienta centralizada crea acoplamiento en la arquitectura, reduce la transparencia e incrementa la dependencia en un proveedor sin añadir ventajas. Los API gateways pueden servir como una abstracción útil para aplicar características horizontales (crosscutting concerns), pero estamos convencidos que la inteligencia debe estar en las propias APIs
 
Ref: thoughtworks.com/radar. Radar-vol-23-es.pdf 


GraphQL entre nuestros equipos por su habilidad de agregar información provista por diferentes recursos. Esta vez queremos advertir sobre el uso de Apollo Federation y su soporte sólido para un grafo de datos único y unificado para tu compañía. Si bien, a primera vista, la idea de tener conceptos ubicuos para toda la organización es tentadora, debemos tener en cuenta intentos similares ya propuestos en la industria, como MDM y modelos de datos canónicos, entre otros, que han expuesto las dificultades de este  enfoque. Los retos pueden ser significativos, especialmente cuando el dominio en el que nos encontramos es lo suficientemente
complejo como para crear un modelo único y unificado.

Ref: thoughtworks.com/radar. Radar-vol-23-es.pdf 


# Referencias Arquitecturas .Net (Codigo)

ASP.NET Boilerplate - Web Application Framework 
