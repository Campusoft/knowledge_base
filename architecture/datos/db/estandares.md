# Estándares y criterios para bases de datos relacionales

Estas son heurísticas para iniciar el diseño; deben validarse frente al dominio, el motor de base de datos, las cargas y las necesidades de evolución.

## Modelado

- Comprender el dominio y las reglas antes de definir tablas y relaciones.
- Representar conceptos distintos en tablas distintas cuando tengan atributos, restricciones o ciclos de vida propios; evitar tablas genéricas que mezclen entidades sin una razón clara.
- Definir claves primarias y restricciones que protejan la integridad de datos.
- Elegir entre claves naturales y sustitutas según estabilidad, unicidad, significado y requisitos de integración; no hay una elección universal.
- Usar Unicode cuando sea necesario para los idiomas y caracteres que admite el sistema. El costo de almacenamiento depende del motor y del tipo de dato.
- Documentar tablas, relaciones, restricciones, índices, disparadores y procedimientos almacenados.

## Convenciones

- Elegir nombres de tabla singulares o plurales y aplicar una convención consistente en todo el esquema. La recomendación de singular no es universal.
- Evitar prefijos repetitivos si el esquema o el nombre de la tabla ya identifica claramente el dominio. Si una convención heredada requiere prefijos, documentarla y aplicarla de forma consistente.

## Referencias

- [Database Guidelines (RDBMS/SQL)](http://soa-java.blogspot.com/2013/01/database-guidelines-rdbmssql.html) — referencia general; algunas recomendaciones pueden ser específicas de su época o motor.
- [Tips for Better Database Design](https://vertabelo.com/blog/9-tips-for-better-database-design/)
- [Diseño de modelos de base de datos](design.model.db.md)
