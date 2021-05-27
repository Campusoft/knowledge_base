
# namespace
Un namespace se declara usando el atributo XML reservado xmlns

```
xmlns="http://www.w3.org/1999/xhtml"
```

# Default namespace


Cuando se define en la etiqueta de inicio de un elemento XML, se aplica a todos elementos sin prefijo del ámbito del elemento, pero no a los atributos. 

```
<?xml version="1.0"?>
<cliente xmlns='http://es.wikipedia.org/wiki/Espacio_de_nombres_XML/cliente'
         xmlns:ped='http://es.wikipedia.org/wiki/Espacio_de_nombres_XML/pedido'>
    <numero_ID>1232654</numero_ID>
    <nombre>Nombre</nombre>
    <telefono>99999999</telefono>
    <ped:pedido>
      <ped:numero_ID>6523213</ped:numero_ID>
      <ped:articulo>Caja de herramientas</ped:articulo>
      <ped:precio>187,90</ped:precio>
    </ped:pedido>
</cliente>
```