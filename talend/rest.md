# REST


## Componentes

### tWriteJSONField 


Campos nullos, el tWriteJSONField los convierte en []

For any field with the null value, this component writes a pair of square brackets ([]). If needed, you can remove or replace them using a regular expression in a tJavaRow component next to the tWriteJSONField component. For exmaple:

output_row.rootNode = input_row.rootNode.replaceAll("\\[\\]", "");

https://help.talend.com/r/KxVIhxtXBBFymmkkWJ~O4Q/50PMKM8mz4A5fAL~FQcLHw


## Tips

Los componentes, que se encuentre antes tRESTRequest, no se ejecutan el flujo cuando se llama al servicio REST, cualquier logica debe estar en algun flujo "Metodo / Endpoint" implementado en el servicio REST  

## Referencias

How to built a JSON string with arrays in Talend
http://talendhowto.com/2017/10/22/how-to-built-a-json-string-with-arrays/
