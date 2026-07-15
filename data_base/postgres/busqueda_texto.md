# PostgreSQL - Busqueda de texto

Guia practica para elegir y aplicar mecanismos de busqueda de texto en PostgreSQL.

Fuentes principales:

- PostgreSQL docs: Full Text Search, capitulo 12.
- PostgreSQL docs: `pg_trgm`.
- PostgreSQL docs: `unaccent`.
- PostgreSQL docs: tipos de indices.

## Opciones principales

### 1. `LIKE` e `ILIKE`

Busqueda simple por patron.

```sql
SELECT *
FROM productos
WHERE nombre ILIKE '%laptop%';
```

Uso recomendado:

- Filtros pequenos o administrativos.
- Prototipos.
- Patrones simples.
- Busquedas sobre tablas pequenas.

Limitaciones:

- `ILIKE '%texto%'` normalmente no usa bien un indice B-tree.
- En tablas grandes puede terminar en escaneo completo.
- No entiende relevancia, stemming, stop words ni idioma.
- No maneja errores tipograficos.

Recomendacion:

- Usar solo cuando el volumen sea bajo o cuando se combine con `pg_trgm`.

### 2. Indice B-tree para prefijos

Util cuando la busqueda esta anclada al inicio del texto.

```sql
CREATE INDEX idx_productos_nombre_lower
ON productos (lower(nombre));

SELECT *
FROM productos
WHERE lower(nombre) LIKE 'lap%';
```

Uso recomendado:

- Autocomplete por prefijo.
- Codigos, identificadores, usernames, slugs.
- Campos con busqueda exacta o "empieza con".

Limitaciones:

- No sirve bien para `'%texto%'`.
- No resuelve busqueda flexible ni ranking.

### 3. Full Text Search (`tsvector` / `tsquery`)

Busqueda semantica por lexemas. PostgreSQL tokeniza el documento, normaliza palabras segun configuracion de idioma y permite ranking.

Conceptos clave:

- `to_tsvector(config, texto)`: convierte texto en documento indexable.
- `to_tsquery(config, consulta)`: consulta estructurada con operadores.
- `plainto_tsquery(config, texto)`: convierte texto plano en consulta segura.
- `websearch_to_tsquery(config, texto)`: sintaxis mas parecida a buscadores web.
- `ts_rank` / `ts_rank_cd`: ranking de resultados.
- `ts_headline`: resaltado de fragmentos.

Ejemplo basico:

```sql
SELECT id, titulo
FROM articulos
WHERE to_tsvector('spanish', titulo || ' ' || contenido)
      @@ plainto_tsquery('spanish', 'base de datos relacional');
```

Ejemplo recomendado con columna generada:

```sql
ALTER TABLE articulos
ADD COLUMN search_vector tsvector
GENERATED ALWAYS AS (
  setweight(to_tsvector('spanish', coalesce(titulo, '')), 'A') ||
  setweight(to_tsvector('spanish', coalesce(resumen, '')), 'B') ||
  setweight(to_tsvector('spanish', coalesce(contenido, '')), 'C')
) STORED;

CREATE INDEX idx_articulos_search_vector
ON articulos
USING GIN (search_vector);

SELECT id,
       titulo,
       ts_rank(search_vector, websearch_to_tsquery('spanish', 'postgres busqueda texto')) AS rank
FROM articulos
WHERE search_vector @@ websearch_to_tsquery('spanish', 'postgres busqueda texto')
ORDER BY rank DESC
LIMIT 20;
```

Uso recomendado:

- Busqueda en articulos, documentos, tickets, comentarios o descripciones.
- Busqueda con ranking.
- Busqueda por idioma.
- Consultas con operadores logicos.
- Resultados explicables y ordenados por relevancia.

Ventajas:

- Integrado en PostgreSQL.
- Puede usar indices `GIN` o `GiST`.
- Soporta ranking y resaltado.
- Permite ponderar campos.
- Evita muchos falsos positivos de `LIKE`.

Limitaciones:

- No es busqueda fuzzy por defecto: errores como `postgrs` no necesariamente encuentran `postgres`.
- Depende de la configuracion de idioma.
- Requiere pensar en normalizacion, stop words y diccionarios.
- En busquedas multi-idioma se debe modelar la configuracion por idioma o usar una configuracion mas simple.
- No reemplaza motores especializados cuando se necesita analisis linguistico avanzado, agregaciones de busqueda complejas o escalado independiente.

### 4. `pg_trgm` para similitud y busqueda parcial

Extension basada en trigramas. Es muy util para busqueda por similitud, tolerancia a errores y aceleracion de `LIKE`, `ILIKE` y regex.

Activacion:

```sql
CREATE EXTENSION IF NOT EXISTS pg_trgm;
```

Indice recomendado para busqueda parcial:

```sql
CREATE INDEX idx_productos_nombre_trgm
ON productos
USING GIN (nombre gin_trgm_ops);
```

Ejemplos:

```sql
-- Contiene texto, usando indice trigram si hay trigramas suficientes
SELECT *
FROM productos
WHERE nombre ILIKE '%teclado mecanico%';

-- Similitud
SELECT id, nombre, similarity(nombre, 'teclado mecaniko') AS score
FROM productos
WHERE nombre % 'teclado mecaniko'
ORDER BY score DESC
LIMIT 10;

-- Distancia; menor es mejor
SELECT id, nombre
FROM productos
ORDER BY nombre <-> 'teclado mecaniko'
LIMIT 10;
```

Uso recomendado:

- Autocomplete flexible.
- Busqueda con errores tipograficos.
- Nombres de personas, productos, ciudades o entidades.
- Campos cortos y medianos.
- Acelerar `ILIKE '%texto%'` sobre columnas grandes.

Limitaciones:

- No entiende idioma ni semantica.
- Puede devolver coincidencias irrelevantes si el umbral es bajo.
- Patrones muy cortos tienen pocos trigramas y pueden ser poco selectivos.
- Patrones sin trigramas extraibles pueden degradar a escaneo amplio.
- Para igualdad exacta, un B-tree puede ser mas eficiente.

Parametros utiles:

```sql
SHOW pg_trgm.similarity_threshold;
SET pg_trgm.similarity_threshold = 0.35;
```

Valores mas altos aumentan precision y reducen resultados. Valores mas bajos aumentan recall, pero pueden traer ruido.

### 5. `unaccent` para ignorar tildes

Extension que remueve diacriticos. Es util en busquedas donde `cancion` debe encontrar `cancion` y `cancion` con tilde.

Activacion:

```sql
CREATE EXTENSION IF NOT EXISTS unaccent;
```

Uso simple:

```sql
SELECT unaccent('cancion');
```

Con `ILIKE`:

```sql
CREATE INDEX idx_productos_nombre_unaccent_trgm
ON productos
USING GIN (unaccent(lower(nombre)) gin_trgm_ops);

SELECT *
FROM productos
WHERE unaccent(lower(nombre)) ILIKE unaccent(lower('%cafe%'));
```

Nota: para indexar expresiones con `unaccent`, validar en la version concreta y en el entorno si la funcion puede usarse directamente en indices. En algunos proyectos se prefiere una columna normalizada persistida para evitar problemas de inmutabilidad, rendimiento y consistencia.

## Indices

### GIN

Opcion mas comun para Full Text Search y `pg_trgm`.

Recomendado para:

- Consultas de lectura frecuentes.
- Documentos relativamente estables.
- Busqueda full-text con `@@`.
- Trigramas con `gin_trgm_ops`.

Consideraciones:

- Puede ocupar mas espacio.
- Las escrituras tienen costo adicional.
- En cargas masivas conviene crear el indice despues de importar.

### GiST

Tambien soporta Full Text Search y `pg_trgm`.

Recomendado para:

- Casos donde se requiere ordenar por distancia con trigramas usando `<->`.
- Indices mas compactos en ciertos escenarios.
- Busquedas aproximadas donde se acepta recheck.

Consideraciones:

- Para Full Text Search, GIN suele ser la eleccion inicial.
- En `pg_trgm`, GiST puede ser mejor para top-N por distancia.

## Casos de uso y decision rapida

| Necesidad | Opcion recomendada |
| --- | --- |
| Buscar por ID, codigo, slug o valor exacto | B-tree |
| Buscar por prefijo, ejemplo `abc%` | B-tree sobre `lower(campo)` |
| Buscar por contiene, ejemplo `%abc%` | `pg_trgm` + GIN |
| Buscar documentos con relevancia | Full Text Search + GIN |
| Buscar con errores tipograficos | `pg_trgm` |
| Buscar sin tildes | `unaccent` o columna normalizada |
| Buscar documentos y sugerir correcciones | Full Text Search + `pg_trgm` |
| Busqueda tipo Google en app pequena o mediana | `websearch_to_tsquery` + FTS |
| Busqueda avanzada, analitica o distribuida | Evaluar Elasticsearch, OpenSearch, Meilisearch o Typesense |

## Recomendaciones practicas

1. Separar busqueda exacta, parcial y full-text. No usar una sola tecnica para todo.
2. Para documentos, crear una columna `tsvector` persistida y un indice GIN.
3. Usar `websearch_to_tsquery` para entradas libres del usuario; es mas tolerante que `to_tsquery`.
4. Ponderar campos: titulo con peso `A`, resumen con `B`, contenido con `C`.
5. Normalizar texto cuando el negocio lo requiera: minusculas, tildes, espacios, caracteres especiales.
6. Para nombres y productos, combinar `pg_trgm` con ranking propio del negocio.
7. Medir con `EXPLAIN (ANALYZE, BUFFERS)` antes y despues de crear indices.
8. Evitar funciones directas sobre columnas si no existe un indice compatible con esa expresion.
9. No bajar demasiado `pg_trgm.similarity_threshold`; genera ruido y mas trabajo.
10. Para multi-tenant, incluir `tenant_id` en filtros y evaluar indices parciales o compuestos segun cardinalidad.

## Patron recomendado para una busqueda combinada

Ejemplo: catalogo de productos donde se quiere buscar por texto, tolerar errores y priorizar resultados activos.

```sql
CREATE EXTENSION IF NOT EXISTS pg_trgm;

ALTER TABLE productos
ADD COLUMN search_vector tsvector
GENERATED ALWAYS AS (
  setweight(to_tsvector('spanish', coalesce(nombre, '')), 'A') ||
  setweight(to_tsvector('spanish', coalesce(descripcion, '')), 'B')
) STORED;

CREATE INDEX idx_productos_search_vector
ON productos USING GIN (search_vector);

CREATE INDEX idx_productos_nombre_trgm
ON productos USING GIN (nombre gin_trgm_ops);

WITH q AS (
  SELECT websearch_to_tsquery('spanish', 'teclado mecanico') AS tsq,
         'teclado mecanico'::text AS raw
)
SELECT p.id,
       p.nombre,
       ts_rank(p.search_vector, q.tsq) AS text_rank,
       similarity(p.nombre, q.raw) AS name_similarity
FROM productos p
CROSS JOIN q
WHERE p.activo = true
  AND (
    p.search_vector @@ q.tsq
    OR p.nombre % q.raw
  )
ORDER BY
  (ts_rank(p.search_vector, q.tsq) * 0.7 + similarity(p.nombre, q.raw) * 0.3) DESC,
  p.nombre
LIMIT 20;
```

## Limitaciones generales de PostgreSQL para busqueda

- No esta pensado como motor de busqueda distribuido independiente.
- El ranking nativo es correcto, pero puede quedarse corto frente a necesidades complejas de relevancia.
- FTS no corrige errores automaticamente.
- Las configuraciones linguisticas requieren pruebas con datos reales.
- Indices de texto grandes aumentan almacenamiento, tiempo de escritura y mantenimiento.
- Busquedas con filtros muy amplios pueden requerir indices compuestos, parciales o redisenar la consulta.
- Para sinonimos, diccionarios y thesaurus, hay soporte, pero aumenta la complejidad operativa.
- Para busqueda semantica/vectorial se debe evaluar `pgvector` u otro motor especializado.

## Checklist de implementacion

- Definir tipo de busqueda: exacta, prefijo, contiene, full-text, fuzzy o semantica.
- Definir idioma y reglas de normalizacion.
- Crear indices segun patron real de consulta.
- Usar datos representativos para pruebas.
- Revisar planes con `EXPLAIN (ANALYZE, BUFFERS)`.
- Medir precision y recall con consultas reales.
- Ajustar ranking, pesos y umbrales.
- Documentar limites conocidos para producto y soporte.

## Referencias

- https://www.postgresql.org/docs/current/textsearch.html
- https://www.postgresql.org/docs/current/pgtrgm.html
- https://www.postgresql.org/docs/current/unaccent.html
- https://www.postgresql.org/docs/current/indexes-types.html
