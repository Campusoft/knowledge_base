# Syncfusion 



# QueryBuilder
## Tipo de presentación
A través de la opción displayMode se puede presentar horizontal o verticalmente el formulario.

```
	displayMode="Horizontal" // por defecto
```

## Generación automática de campos
- The Columns are automatically generated when the Columns declaration is empty or undefined while initializing the query builder. All the columns in the DataSource are bound as the query builder columns.
- Sin ```<e-querybuilder-columns>``` o vacía identifica automaticamente los tipos de datos. 

###	Problema con objetos complejos
Al generar automáticamente los campos syncfusion descompone el objeto y genera incorrectamente la condición porque se pierde la referencia.

```	
	Data.field.value => field = value
```

## Métodos importantes
- **getValidRules():** obtiene las reglas desde el componente en formato object.
- **getSqlFromRules():** convierte en formato sql.
- **JSON.stringify():** convierte en json.

## Atributos de ejs-querybuilder 
- **id:** Identificador del elemento.
- **dataSource:** Conjunto de datos de origen.
- **ruleChange:** Definir la función js que manejará los cambios en las condiciones.
- **displayMode:** Dirección del formulario.

## Atributos de e-querybuilder-column
- **field:** Campo asociado a la condición.
- **label:** Texto que se presenta en la condición. Si no se define presenta el campo pero como vacío (undefined).
- **type:** Tipo de dato del campo. Si se omite lo toma como string. Es importante definir porque automáticamente determina el tipo de input que debe utilizar.
- **template:** Define el conjunto de funciones js asociadas a diferentes acciones.
	- **create:** Creates the custom component.
	- **write:** Wire events for the custom component.
	- **destroy:** Destroy the custom component.
- **values:** Contiene los valores del componente en caso de necesitar utilizar una lista. Puede ser una lista de strings si no tienen un id asociado o un objeto {id, name} cuando existe un id asociado (tipo catálogo).
	```
	public class CatalogType
	{
		public string Id { get; set; }
		public string Type { get; set; }
		public string Desc { get; set; }
	}
	```
## Consideraciones
- Exiten 3 tipos de reglas:
	- Comparativa directa: A == B
	- Comparativa por función: A is not null
	- Comparativa por rango: A between X and Y
- No se puede mapear directamente el json a objeto QueryBuilderRule
- El operador between y notbetween no tienen equivalentes en c# por lo que se deben descomponer en 2 reglas
- Los valores tipo date deben ser convertidos a DateTime. Llegan en formato MM/dd/yy después del año 2000 y MM/dd/yyyy antes del 2000

## Fuentes	
- [Ejemplos](https://ej2.syncfusion.com/aspnetcore/QueryBuilder/DefaultFunctionalities#/material)
- [Proyecto Ejemplo Oficial](https://github.com/syncfusion/ej2-aspnetcore-samples)
- [Documentación](https://ej2.syncfusion.com/aspnetcore/documentation/query-builder/getting-started/)
- [DropDownList](https://ej2.syncfusion.com/javascript/documentation/drop-down-list/how-to/add-item/)
- [MultiSelect](https://ej2.syncfusion.com/aspnetcore/documentation/multi-select/getting-started/)
