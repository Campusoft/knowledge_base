
# JsonPath

JSONPath es una forma estandarizada para consultar elementos de un objeto JSON. JSONPath utiliza expresiones de ruta para desplazarse por elementos, elementos anidados y matrices en un documento JSON.

## Notation

JsonPath uses special notation to represent nodes and their connections to adjacent nodes in a JsonPath path. There are two styles of notation, namely dot and bracket.

Both of the following paths refer to the same node from the above JSON document, which is the third element within the location field of creator node, that is a child of the jsonpath object belonging to tool under the root node.

With dot notation:

```
$.tool.jsonpath.creator.location[2]
```

With bracket notation:

```
$['tool']['jsonpath']['creator']['location'][2]
```

The dollar sign ($) represents root member object.

## filtros

Filters are logical expressions used to filter arrays. An example of a JSONPath expression with a filter is

```
$.store.book[?(@.price < 10)]
```

where @ represents the current array item or object being processed. Filters can also use $ to refer to the properties outside of the current object:

```
$.store.book[?(@.price < $.expensive)]
```

## Tips


----

Si se requiere recuperar algun campo, cuyo nombre tiene **caracteres especiales**, es mejor utilizar notacion corchetes, en ves notacion puntos

Notacion Puntos
$.[*].CampoNombre

Notacion Corchetes
$.[*]['CampoNombre']


Ejemplo:
```
[
       {
            "@odata.id": "http://services.odata.org/V4/(S(1pqyq3ukqopjwiukdz3asnc4))/TripPinServiceRW/People('sandyosborn')",
            "@odata.etag": "W/\"08D87ED313A9BECB\"",
            "@odata.editLink": "http://services.odata.org/V4/(S(1pqyq3ukqopjwiukdz3asnc4))/TripPinServiceRW/People('sandyosborn')",
            "UserName": "sandyosborn",
            "FirstName": "Sandy",
            "LastName": "Osborn"
        }
]
```

Para obtener la propiedad @odata.id, se puede utilizar

$..['@odata.id'] => obtener todos los elementos array
$[0]['@odata.id'] => obtener la propiedad elemento 1 en array
$[*]['@odata.id'] => obtener todos los elementos array



## Herramientas

Presenta un arbol JSON, y permite seleccionar un nodo especifico, y generar el JsonPath correspondiente
https://jsonpathfinder.com/


This website can be used to quickly do queries on JSON or XML data and extract subsets of JSON or XML using a variety of query tools.

You can add multiple steps where the output of one step becomes the input of another step. Just click the Add Step button add a step. You can change the input type, name and transform type for a step by clicking on the gear icon in the step header. You can delete a step using the trashcan icon in the step header. It is not possible to delete a step that is being used as input for another step.

https://www.jsonquerytool.com


JSONPath Online Evaluator
https://jsonpath.com/

## Referencias

JSONPath Syntax. (Tiene explicacion a detalle filtros)
https://support.smartbear.com/alertsite/docs/monitors/api/endpoint/jsonpath.html
