## Validations

Las validaciones se establecen en validaciones de estructura, y negocio.

 
### Validationes Estructuras. 

Estas validaciones son aquellas, que no cambian en el tiempo o son rara vez cambiadas en el tiempo, estan relacionadas a restricciones a  nivel modelo Entidad/Relacion.

Algunas ejemplos estas validaciones, seria:
- La Longitud de un campo
- Si el campo es requerido. 

Estas validaciones, se utilizan dataAnnotations

### Validationes Negocio. 

Estas validaciones son aquellas, que pueden cambiar en tiempo. Estas validaciones deben realizarse en clases exclusivas que encapsulen la logica de la validacion.

Es recomendable utilizar libreria "FluentValidations"; o si el proyecto-requerimiento posee mecanismo parametrizacion se utilizarian este. 

Ejemplo de estas validaciones.
- Codigo Unico (Consistencia datos)
- Restricciones, no se puede eliminar un registro si cumple unas condiciones (Constraint)
- Gestion estados, segun la maquina estados, se determina las transacciones que pueden darse desde un valor estado a otro valor. 

 

