# Elasticsearch


# Versiones

 
 
# Instalar 

Install Elasticsearch with Docker
https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html

 Elasticsearch as a container for beginners 
https://dev.to/jinnabalu/elasticsearch-as-a-container-for-beginners-d1e


Optional but important when we are running in dev machine ES_JAVA_OPTS: "-Xms512m -Xmx1024m". En ambientes de desarrollo para controlar asignacion recursos no sean altos.

Errores

```
elasticsearch    | Error: Could not find or load main class "-Xms512m
elasticsearch    | Caused by: java.lang.ClassNotFoundException: "-Xms512m
```

Colocar correctamente: ES_JAVA_OPTS:

```
- "ES_JAVA_OPTS=-Xms512m -Xmx512m"
```

Al colocar de esta forma da error

```
- ES_JAVA_OPTS="-Xms512m -Xmx1024m"
```
------------

# Commandos

Ver el mapping de index apache
```
http://localhost:9200/apache/_mapping/?pretty
```

Consultar la cantidad de espacio ocupado pos los índices
```
http://localhost:9200/_cat/indices?v
```

Eliminar un indice
```
DELETE “http://localhost:9200/NombreIndiceEliminar”
```
# Licencias

***Revisar***
https://www.elastic.co/what-is/open-x-pack 

