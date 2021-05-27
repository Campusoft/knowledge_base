# Default namespace
 
## .net 
 
Si el documento tiene un namespace por defecto

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

Para aplicar xpath, se debe configurar con XmlNamespaceManager el namespace:

```
XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
nsManager.AddNamespace("x", "http://es.wikipedia.org/wiki/Espacio_de_nombres_XML/cliente");
```

El codigo anterior, crea un XmlNamespaceManager, y agrega namespace, con un prefijo "x"

Una vez configurado el XmlNamespaceManager, en los xPath se debe utilizar el prefijo "x"


```
XmlNode entlibDataNode = doc.SelectSingleNode( "/x:nombre", nsManager );
```

El prefijo pueder ser cualquiera, en este ejemplo se utilizo "x"
