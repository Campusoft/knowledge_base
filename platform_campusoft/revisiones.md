

Septiembre-2021

Basado en CQRS, tener objetos para read (query), y otros objetos para write (commandos), se puede considerar siempre crear objetos DTO en abp para:
- GetList. (Dto lectura)
- Create / Update (Dto escritura).

Las unicos servicios que puede mantener un solo DTO, para todo  seria los servicios para entidades Catalogos.
