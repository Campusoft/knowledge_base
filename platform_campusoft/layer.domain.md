# Capa de Dominio

# Especificaciones


**Preferencialmente utilizar clase separadas, no catalogos**

Al utilizar clases/modelos separados, permite extender las clases separadas, segun las necesidades Negocio. Ejemplo si se requiere agregar una nueva propiedad, algun comportamiento, etc.
 
**Tipos de datos para las claves primarias/identicaciones**
 
En conceptos negocios "catalogos", es preferible utilizar codigos (strings), como claves primarias. Esto facilita el uso directamente en modelos dependientes. Ejemplo el catalogo ciudades, paises: se utiliza codigos "Ec: Ecuador.  Loj: Loja, Qto: Quito", al utilizarse en otros modelos ejemplo persona para indicar el pais nacimiento la clave foranea seria "EC", lo cual facilita los filtros, entendimiento de los datos sin necesidad de cruzar con el catalogo.

Para lo identificadores utilizar:

- Uso codigos (Stings), en modelos tipo catalogos, conceptos negocio que describen algun elemento, configuraciones, solicitudes de un proceso
- Uso int, en modelos que registran informacion de transacciones/eventos/procesos
- Uso Guid. (TODO: Documentar en que escenarios es preferible este tipo identicaciones

**Centralizar especificaciones de los campos**
 
Centralizar las configuraciones tamaños de los campos, los caracteres que se permite en un campo en clases separadas "static" de clase del modelo. Ejemplo todo lo que se refiere a nombres propios (Ciudades, personas, etc), tiene que tener 80 caracteres, esta especificacion centralizar en una classe "static" de constantes y utilizarlas en donde  se requiere.

 


