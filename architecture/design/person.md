# Person

El concepto persona, es comun en varios tipos dominios, ejemplo dominio ventas serian clientes, proveedores, etc, en un dominio academico existe los estudiantes, docentes. 



# Revisiones


Tratar como entidad idependiente las direcciones, es decir varias entidades pueden enlazarse a una direccion.  O considerar la entidad dependiente de la entidad Persona. Si la entidad persona o la abstraccion de esta puede ser personas, y no personas, asociarla las direcciones a esta entidad seria una buena decision, sin embargo si esta entidad no es abstraccion y es exclusiva para personas, considerar las direcciones como entidad idenpendientes es mejor. Desde el punto servicios (rest, u otro tipo), se puede considerar como recursos separados la persona y las direcciones; esto no necesariamente puede repressentar el modelo interno de la base de datos esta puede ser persona + direccionas, o personas y direcciones independientes