# Validations

Las validaciones se establecen en validaciones de estructura, y negocio.

 
# Validationes Estructuras. 

Estas validaciones son aquellas, que no cambian en el tiempo o son rara vez cambiadas en el tiempo, estan relacionadas a restricciones a  nivel modelo Entidad. Se les puede considerar estaticas. 

Algunas ejemplos estas validaciones, seria:
- Si la propiedad es requerida. 
- La longitud de una propiedad (Cadena).
- Controlar los caracteres que se permiten en una propiedad. Se utiliza expresiones regulares. Ej. Solo se permiten letras o numeros etc
- Rango valores.

Estas validaciones, se utilizan dataAnnotations:
- Si las entidades posee persitencia. Al utilizar ORM Entity Framework los dataAnnotations se utilizar para aplicar restricciones en la base de datos. Campos requeridos longitud cadenas etc.
- Si se trabaja con Asp .Net MVC estas validaciones se aplican en la UI de forma trasparente

# Validationes Negocios

Estas validaciones son aquellas, que pueden cambiar en tiempo. Estas validaciones deben realizarse en clases exclusivas que encapsulen la logica de la validacion.

- Al ser logica negocio deberian estar en la capa dominio. En las entidades o en servicios dominio. (DDD)

Es recomendable utilizar libreria "FluentValidations"; o si el proyecto-requerimiento posee mecanismo de parametrizacion se utilizarian este. 

Ejemplo de estas validaciones.
- Codigo Unico (Consistencia datos)
- Restricciones, no se puede eliminar un registro si cumple unas condiciones (Constraint)

## Controlar estados

- Si una entidad posee estados y los posibles valores poseen logica negocio maquina estados  se determina las transacciones que pueden darse desde un valor estado a otro valor.  (Ej: Si es cerrado no se puede regresar a pagado)
- Estas validaciones deben ser parte de la entidad negocio o en casos especificos se puede colocar en servicios de dominio.


# Especificaciones

- Valores / parametros que se utilicen en validaciones debe estar un clase de constantes. (Si aplica se puede utilizar enumerables). Ejemplos valores. El maximo de longitud de una cadena.
- Crear clase de constantes especificas por entidades para agrupar Valores / parametros de validaciones que se apliquen a una entidad.
- Crear un clase global para establecen constantes genericas o globales. Ejemplo todos las propiedades que almacen nombres debe ser 60 caracteres.

