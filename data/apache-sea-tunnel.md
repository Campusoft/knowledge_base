# Apache SeaTunnel: guía operativa

> Guía introductoria basada en Apache SeaTunnel 2.3.13, la versión estable indicada por la documentación oficial al 18 de julio de 2026. Antes de usarla en otro momento, conviene revisar la [página de descargas](https://seatunnel.apache.org/download/) y sustituir la versión en los comandos.

## 1. ¿Qué es Apache SeaTunnel?

Apache SeaTunnel es una plataforma distribuida de integración y movimiento de datos. Permite definir canalizaciones por configuración para leer datos desde una fuente, aplicar transformaciones opcionales y escribirlos en uno o varios destinos.

Su objetivo principal no es orquestar procesos de negocio como un planificador de flujos, sino transportar y sincronizar datos entre sistemas heterogéneos. Admite cargas por lotes, flujos continuos, captura de cambios (CDC) mediante conectores compatibles y migraciones de tablas completas.

La estructura conceptual de un trabajo es:

```text
                 configuración HOCON
                         |
                         v
Source(s) ---> Transform(s) opcionales ---> Sink(s)
     \                 |                       /
      +---------- motor de ejecución ----------+
```

El mismo modelo de conectores puede ejecutarse con distintos motores. Para una evaluación inicial se recomienda **SeaTunnel Engine**, también conocido como **Zeta**, porque viene integrado y requiere menos infraestructura. Flink y Spark son alternativas útiles cuando la organización ya opera esos motores.

## 2. Arquitectura

### 2.1 Componentes principales

- **Configuración del trabajo:** archivo HOCON que declara el entorno, fuentes, transformaciones y destinos.
- **SeaTunnel Core:** interpreta la configuración, carga los plugins, construye el plan de ejecución y lo entrega al motor elegido.
- **Source:** conector que lee desde una API, base de datos, archivo, cola u otro sistema.
- **Transform:** operación opcional sobre el flujo, por ejemplo renombrar, seleccionar o convertir campos.
- **Sink:** conector que escribe el resultado en una base de datos, archivo, consola, almacén analítico u otro destino.
- **Motor de ejecución:** ejecuta el plan, administra paralelismo, tareas, estado y tolerancia a fallos.

Los nombres `plugin_output` y `plugin_input` enlazan etapas cuando una canalización tiene transformaciones, bifurcaciones o varias fuentes. En una canalización lineal muy simple SeaTunnel puede inferir parte del enlace, pero declararlo facilita la lectura y evita ambigüedad.

### 2.2 Motores disponibles

| Motor | Cuándo usarlo |
|---|---|
| SeaTunnel Engine (Zeta) | Primera evaluación, proyectos nuevos, sincronización, CDC y menor carga operativa. |
| Apache Flink | Cuando ya existe una plataforma Flink y se requieren sus capacidades de procesamiento continuo. |
| Apache Spark | Cuando ya existe una plataforma Spark y predominan cargas por lotes. |

Esta guía usa Zeta en modo local. El parámetro `-m local` levanta el motor dentro del mismo proceso del trabajo, adecuado para aprendizaje y pruebas de concepto. El modo clúster separa la presentación del trabajo de su ejecución y requiere preparar nodos master y worker.

### 2.3 Flujo de ejecución

1. El usuario prepara el archivo de configuración.
2. SeaTunnel Core valida la estructura y busca los conectores requeridos.
3. Core construye el grafo de ejecución `Source -> Transform -> Sink`.
4. El motor asigna paralelismo y ejecuta las tareas.
5. Las fuentes producen registros, las transformaciones los modifican y los sinks confirman su escritura.
6. En un trabajo `BATCH`, el proceso termina al agotar la fuente. En `STREAMING`, permanece activo hasta ser cancelado o hasta que la fuente finalice según su comportamiento.

## 3. Casos de uso

- Sincronizar tablas entre bases de datos operacionales y un almacén analítico.
- Cargar archivos CSV o JSON hacia PostgreSQL, Elasticsearch, ClickHouse u otro destino.
- Extraer datos estructurados desde una API HTTP y almacenarlos para análisis.
- Replicar cambios de una base de datos mediante CDC cuando el conector lo soporte.
- Migrar múltiples tablas o bases de datos entre plataformas.
- Consolidar varias fuentes en un lago de datos u object storage.
- Ejecutar validaciones o transformaciones ligeras durante el movimiento.

SeaTunnel no reemplaza necesariamente a Airflow, Dagster o un planificador corporativo. Es común que uno de estos orquestadores programe y supervise trabajos de SeaTunnel.

## 4. Requisitos y conceptos previos

Para la instalación binaria de SeaTunnel 2.3.13 se necesita:

- Java 8 u 11. La documentación de 2.3.13 indica que versiones superiores a Java 8 pueden funcionar, pero para una PoC conviene usar Java 11 y establecer `JAVA_HOME`.
- Una terminal compatible con los scripts de shell. En Windows, WSL2 es la ruta más sencilla para la instalación binaria.
- Acceso a Maven Central durante la instalación de conectores.
- Docker y Docker Compose únicamente para los escenarios en contenedores.

Variables usadas en los ejemplos:

```bash
export SEATUNNEL_VERSION="2.3.13"
export SEATUNNEL_HOME="$PWD/apache-seatunnel-${SEATUNNEL_VERSION}"
```

En PowerShell, el equivalente es:

```powershell
$env:SEATUNNEL_VERSION = "2.3.13"
$env:SEATUNNEL_HOME = (Join-Path $PWD "apache-seatunnel-$env:SEATUNNEL_VERSION")
```

Los scripts `install-plugin.sh` y `seatunnel.sh` deben ejecutarse desde Linux, macOS, WSL2 o el interior del contenedor.

## 5. Instalación binaria

### 5.1 Descargar y extraer

En Linux, macOS o WSL2:

```bash
export SEATUNNEL_VERSION="2.3.13"

curl -LO "https://archive.apache.org/dist/seatunnel/${SEATUNNEL_VERSION}/apache-seatunnel-${SEATUNNEL_VERSION}-bin.tar.gz"
tar -xzf "apache-seatunnel-${SEATUNNEL_VERSION}-bin.tar.gz"
export SEATUNNEL_HOME="$PWD/apache-seatunnel-${SEATUNNEL_VERSION}"
```

Comprobar Java:

```bash
java -version
test -x "${SEATUNNEL_HOME}/bin/seatunnel.sh"
```

### 5.2 Instalar solo los conectores necesarios

Desde SeaTunnel 2.2.0-beta, el paquete binario no incluye todos los conectores. El archivo `${SEATUNNEL_HOME}/config/plugin_config` controla cuáles descarga `install-plugin.sh`.

Para ejecutar todas las PoC de esta guía, su contenido mínimo es:

```text
--seatunnel-connectors--
connector-fake
connector-console
connector-http-base
connector-jdbc
--end--
```

Después se instalan para la versión elegida:

```bash
cd "${SEATUNNEL_HOME}"
sh bin/install-plugin.sh "${SEATUNNEL_VERSION}"
ls connectors | grep -E 'connector-(fake|console|http-base|jdbc)'
```

El archivo `${SEATUNNEL_HOME}/connectors/plugins-mapping.properties` contiene los nombres admitidos por `plugin_config`.

### 5.3 Instalar el driver PostgreSQL

El conector JDBC y el driver de cada fabricante son dependencias distintas. Por motivos de licencia, el driver PostgreSQL debe proporcionarlo el usuario. Con Zeta debe quedar en `${SEATUNNEL_HOME}/lib`.

Ejemplo con la versión 42.7.7 del driver; puede reemplazarse por una versión compatible más reciente:

```bash
curl -L \
  -o "${SEATUNNEL_HOME}/lib/postgresql-42.7.7.jar" \
  "https://repo1.maven.org/maven2/org/postgresql/postgresql/42.7.7/postgresql-42.7.7.jar"

ls "${SEATUNNEL_HOME}/lib" | grep postgresql
```

Para Flink o Spark, consulte la documentación del conector: la ubicación del driver cambia respecto de Zeta.

## 6. Instalación y ejecución con Docker

### 6.1 Primera ejecución sin construir una imagen

La imagen oficial permite ejecutar inmediatamente el ejemplo `FakeSource -> Console`:

```bash
docker pull apache/seatunnel:2.3.13

docker run --rm -it apache/seatunnel:2.3.13 \
  ./bin/seatunnel.sh -m local -c config/v2.batch.config.template
```

El resultado correcto muestra registros escritos por `ConsoleSinkWriter` y el trabajo termina con estado exitoso.

Para ejecutar una configuración propia desde el directorio actual:

```bash
docker run --rm -it \
  -v "$PWD/jobs:/config" \
  apache/seatunnel:2.3.13 \
  ./bin/seatunnel.sh -m local -c /config/fake-to-console.conf
```

PowerShell:

```powershell
docker run --rm -it `
  -v "${PWD}/jobs:/config" `
  apache/seatunnel:2.3.13 `
  ./bin/seatunnel.sh -m local -c /config/fake-to-console.conf
```

### 6.2 Imagen personalizada con HTTP y PostgreSQL

La imagen base está pensada para el inicio rápido y no contiene todos los conectores ni drivers. Para una ejecución repetible, se recomienda construir una imagen inmutable.

Crear `docker/plugin_config`:

```text
--seatunnel-connectors--
connector-fake
connector-console
connector-http-base
connector-jdbc
--end--
```

Crear `docker/Dockerfile`:

```dockerfile
FROM alpine:3.21 AS dependencies

ARG POSTGRES_DRIVER_VERSION=42.7.7
RUN wget -O /postgresql.jar \
    "https://repo1.maven.org/maven2/org/postgresql/postgresql/${POSTGRES_DRIVER_VERSION}/postgresql-${POSTGRES_DRIVER_VERSION}.jar"

FROM apache/seatunnel:2.3.13

USER root
COPY plugin_config /opt/seatunnel/config/plugin_config
RUN sh /opt/seatunnel/bin/install-plugin.sh 2.3.13
COPY --from=dependencies /postgresql.jar /opt/seatunnel/lib/postgresql.jar
WORKDIR /opt/seatunnel
```

Construirla desde la raíz del laboratorio:

```bash
docker build -t seatunnel-poc:2.3.13 ./docker
docker image inspect seatunnel-poc:2.3.13 >/dev/null
```

La descarga de plugins ocurre durante el build. Si el proxy corporativo intercepta TLS, configure el proxy y certificados para Docker antes de construir la imagen.

### 6.3 Clúster con Docker Compose

SeaTunnel también permite un clúster Zeta con servicios master y worker en Docker Compose. Es útil para aproximarse a un entorno distribuido, pero añade complejidad que no aporta valor a las primeras PoC. La documentación oficial utiliza `ST_DOCKER_MEMBER_LIST` para el descubrimiento y ejecuta `seatunnel-cluster.sh -r master|worker` en cada servicio.

Antes de adoptar este modo deben definirse persistencia de checkpoints, recursos, logs, monitorización y procedimientos de recuperación. Para esta guía se mantiene `-m local`.

## 7. Estructura de una configuración

SeaTunnel usa HOCON. Un trabajo básico contiene:

```hocon
env {
  parallelism = 1
  job.mode = "BATCH"
}

source {
  # Uno o más conectores de entrada.
}

transform {
  # Sección opcional.
}

sink {
  # Uno o más conectores de salida.
}
```

Para pruebas iniciales, `parallelism = 1` hace que la salida y los errores sean fáciles de seguir. En producción, el paralelismo debe ajustarse a la fuente, al destino, al volumen y a los recursos disponibles.

## 8. Pruebas de concepto

### PoC 1: FakeSource, transformación y Console

**Objetivo:** validar el motor, el parser de configuración, los plugins mínimos y una transformación sin depender de servicios externos.

Guardar como `jobs/fake-to-console.conf`:

```hocon
env {
  parallelism = 1
  job.mode = "BATCH"
}

source {
  FakeSource {
    plugin_output = "personas"
    row.num = 5
    schema = {
      fields {
        nombre = "string"
        edad = "int"
      }
    }
  }
}

transform {
  FieldMapper {
    plugin_input = "personas"
    plugin_output = "personas_mapeadas"
    field_mapper = {
      nombre = nombre_completo
      edad = edad
    }
  }
}

sink {
  Console {
    plugin_input = "personas_mapeadas"
  }
}
```

Ejecución binaria:

```bash
"${SEATUNNEL_HOME}/bin/seatunnel.sh" \
  -m local \
  -c "$PWD/jobs/fake-to-console.conf"
```

Ejecución Docker:

```bash
docker run --rm -it \
  -v "$PWD/jobs:/config" \
  apache/seatunnel:2.3.13 \
  ./bin/seatunnel.sh -m local -c /config/fake-to-console.conf
```

**Validación:** deben aparecer cinco filas en el log de `ConsoleSinkWriter`, con los campos `nombre_completo` y `edad`, y el proceso debe finalizar.

### PoC 2: API pública HTTP hacia Console

**Objetivo:** comprobar una lectura HTTP real sin autenticación. Se usa el recurso `/posts/1` de [JSONPlaceholder](https://jsonplaceholder.typicode.com/), que devuelve un solo objeto JSON y evita introducir paginación.

Antes de ejecutar, comprobar que la API responde:

```bash
curl --fail --silent https://jsonplaceholder.typicode.com/posts/1
```

Guardar como `jobs/http-to-console.conf`:

```hocon
env {
  parallelism = 1
  job.mode = "BATCH"
}

source {
  Http {
    plugin_output = "post_api"
    url = "https://jsonplaceholder.typicode.com/posts/1"
    method = "GET"
    format = "json"
    connect_timeout_ms = 12000
    socket_timeout_ms = 60000
    retry = 2
    schema = {
      fields {
        userId = "int"
        id = "int"
        title = "string"
        body = "string"
      }
    }
  }
}

sink {
  Console {
    plugin_input = "post_api"
  }
}
```

Ejecución binaria:

```bash
"${SEATUNNEL_HOME}/bin/seatunnel.sh" \
  -m local \
  -c "$PWD/jobs/http-to-console.conf"
```

Ejecución con la imagen personalizada:

```bash
docker run --rm -it \
  -v "$PWD/jobs:/config" \
  seatunnel-poc:2.3.13 \
  ./bin/seatunnel.sh -m local -c /config/http-to-console.conf
```

**Validación:** la consola debe mostrar una fila con `userId=1`, `id=1`, título y cuerpo.

**Limitación intencional:** una API pública puede cambiar, limitar solicitudes o no estar disponible. Esta PoC no prueba autenticación, paginación ni acuerdos de nivel de servicio. Para respuestas con listas anidadas, el conector HTTP permite seleccionar registros mediante `content_field` o mapear rutas con `json_field`.

### PoC 3: FakeSource hacia PostgreSQL

**Objetivo:** validar el conector JDBC, el driver y la conectividad hacia una base de datos real, sin añadir complejidad a la fuente.

#### 8.3.1 Levantar PostgreSQL

Crear `docker-compose.yml`:

```yaml
services:
  postgres:
    image: postgres:16-alpine
    container_name: seatunnel-postgres
    environment:
      POSTGRES_DB: seatunnel_poc
      POSTGRES_USER: seatunnel
      POSTGRES_PASSWORD: seatunnel
    ports:
      - "5432:5432"
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U seatunnel -d seatunnel_poc"]
      interval: 5s
      timeout: 3s
      retries: 10
    volumes:
      - seatunnel_pgdata:/var/lib/postgresql/data
    networks:
      - seatunnel_poc

networks:
  seatunnel_poc:
    name: seatunnel-poc

volumes:
  seatunnel_pgdata:
```

Iniciar y esperar el estado `healthy`:

```bash
docker compose up -d postgres
docker compose ps
```

#### 8.3.2 Crear el esquema

```bash
docker exec -i seatunnel-postgres psql \
  -U seatunnel \
  -d seatunnel_poc \
  -c 'CREATE TABLE IF NOT EXISTS public.personas (nombre varchar(100) NOT NULL, edad integer NOT NULL);'
```

Esquema resultante:

```sql
CREATE TABLE public.personas (
    nombre varchar(100) NOT NULL,
    edad   integer      NOT NULL
);
```

#### 8.3.3 Configurar el trabajo

Guardar como `jobs/fake-to-postgres.conf`:

```hocon
env {
  parallelism = 1
  job.mode = "BATCH"
}

source {
  FakeSource {
    plugin_output = "personas_generadas"
    row.num = 10
    schema = {
      fields {
        nombre = "string"
        edad = "int"
      }
    }
  }
}

sink {
  Jdbc {
    plugin_input = "personas_generadas"
    url = "jdbc:postgresql://host.docker.internal:5432/seatunnel_poc"
    driver = "org.postgresql.Driver"
    user = "seatunnel"
    password = "seatunnel"
    query = "INSERT INTO public.personas(nombre, edad) VALUES (?, ?)"
    batch_size = 10
  }
}
```

`host.docker.internal` permite que SeaTunnel dentro de Docker acceda al puerto publicado por PostgreSQL en Docker Desktop. En Linux puede añadirse `--add-host=host.docker.internal:host-gateway`, o es preferible conectar ambos contenedores a la misma red y utilizar `postgres:5432`.

Ejecutar en Docker Desktop:

```bash
docker run --rm -it \
  -v "$PWD/jobs:/config" \
  seatunnel-poc:2.3.13 \
  ./bin/seatunnel.sh -m local -c /config/fake-to-postgres.conf
```

Ejecutar en Docker Engine sobre Linux:

```bash
docker run --rm -it \
  --add-host=host.docker.internal:host-gateway \
  -v "$PWD/jobs:/config" \
  seatunnel-poc:2.3.13 \
  ./bin/seatunnel.sh -m local -c /config/fake-to-postgres.conf
```

Alternativa para la instalación binaria: cambiar la URL JDBC a `jdbc:postgresql://localhost:5432/seatunnel_poc`.

#### 8.3.4 Verificar los datos

```bash
docker exec -it seatunnel-postgres psql \
  -U seatunnel \
  -d seatunnel_poc \
  -c 'SELECT nombre, edad FROM public.personas ORDER BY nombre;'
```

También puede comprobarse la cantidad:

```bash
docker exec -it seatunnel-postgres psql \
  -U seatunnel \
  -d seatunnel_poc \
  -c 'SELECT count(*) AS filas FROM public.personas;'
```

La primera ejecución debe insertar diez filas. Las ejecuciones posteriores agregan otras diez porque la consulta usa `INSERT` y no trunca la tabla.

### PoC 4 opcional: API HTTP hacia PostgreSQL

Una vez que las PoC 2 y 3 funcionan por separado, pueden unirse sin introducir nuevos componentes. Reutilice la sección `Http` de `http-to-console.conf` y el sink JDBC siguiente:

```hocon
sink {
  Jdbc {
    plugin_input = "post_api"
    url = "jdbc:postgresql://host.docker.internal:5432/seatunnel_poc"
    driver = "org.postgresql.Driver"
    user = "seatunnel"
    password = "seatunnel"
    query = "INSERT INTO public.posts(user_id, id, title, body) VALUES (?, ?, ?, ?)"
  }
}
```

Preparar antes la tabla respetando el orden de los campos del source:

```sql
CREATE TABLE public.posts (
    user_id integer NOT NULL,
    id      integer PRIMARY KEY,
    title   text NOT NULL,
    body    text NOT NULL
);
```

Como `id` es clave primaria, repetir la PoC produce una violación de unicidad. Esto es útil para evidenciar que la estrategia de escritura debe decidirse explícitamente. Para una carga idempotente se puede usar una consulta PostgreSQL con `ON CONFLICT`, por ejemplo:

```sql
INSERT INTO public.posts(user_id, id, title, body)
VALUES (?, ?, ?, ?)
ON CONFLICT (id) DO UPDATE
SET user_id = EXCLUDED.user_id,
    title = EXCLUDED.title,
    body = EXCLUDED.body;
```

## 9. Kubernetes, sin profundizar

SeaTunnel Zeta puede desplegarse en Kubernetes en modo local, clúster híbrido o clúster separado. Para producción, la documentación recomienda separar master y workers y administrarlos con StatefulSets; los servicios sin cabeza proporcionan identidades de red estables para el descubrimiento del clúster.

Antes de llevarlo a Kubernetes se recomienda:

- Construir una imagen propia con versiones fijas, conectores y drivers incluidos.
- Guardar configuración no sensible en ConfigMaps y secretos en Kubernetes Secrets o en un gestor externo.
- Usar almacenamiento compartido u object storage para checkpoints y estado recuperable.
- Definir requests, limits, slots y paralelismo de forma conjunta.
- Integrar logs y métricas con la plataforma de observabilidad.

Para las PoC iniciales, Docker o la instalación binaria reducen considerablemente el tiempo de diagnóstico.

## 10. Operación básica

### Lista de comprobación antes de ejecutar

1. El archivo HOCON es legible desde el proceso o contenedor.
2. Cada conector declarado aparece en `${SEATUNNEL_HOME}/connectors`.
3. El driver JDBC aparece en `${SEATUNNEL_HOME}/lib` al usar Zeta.
4. La URL que usa el proceso es alcanzable desde su propio contexto de red.
5. El esquema de SeaTunnel coincide en tipo y orden con la consulta JDBC parametrizada.
6. El usuario de base de datos tiene los permisos mínimos necesarios.
7. Las credenciales reales no están versionadas en el repositorio.

### Errores frecuentes

| Síntoma | Causa probable | Acción |
|---|---|---|
| No se encuentra el plugin | No está en `plugin_config` o no se ejecutó `install-plugin.sh`. | Instalar la versión exacta y revisar `connectors/`. |
| `ClassNotFoundException: org.postgresql.Driver` | Falta el JAR JDBC o está en una ruta incorrecta. | Copiarlo a `lib/` para Zeta y reconstruir/reiniciar. |
| `Connection refused` | Host, puerto, red Docker o servicio incorrecto. | Probar conectividad desde el mismo contenedor. |
| Error de campos JSON | El esquema no coincide con la respuesta de la API. | Inspeccionar la respuesta y ajustar tipos, `content_field` o `json_field`. |
| La tarea HTTP queda activa | Se configuró `STREAMING` o paginación sin condición de término. | Usar `BATCH` en la PoC y revisar `pageing`. |
| Violación de clave primaria | La carga se repitió con `INSERT` no idempotente. | Limpiar la tabla para la prueba o definir upsert. |
| No se ve el archivo montado | Ruta host relativa o sintaxis de volumen incorrecta. | Usar una ruta absoluta y verificar el montaje con `docker inspect`. |

### Seguridad mínima

- No escribir contraseñas reales en configuraciones versionadas.
- Inyectar secretos mediante variables, archivos montados con permisos restringidos o un gestor de secretos.
- Usar usuarios de base de datos específicos para SeaTunnel y privilegios mínimos.
- Validar certificados TLS; no deshabilitar su verificación para resolver problemas de conectividad.
- Fijar versiones de imágenes, plugins y drivers en entornos repetibles.

## 11. Criterios de evaluación después de las PoC

Una evaluación técnica posterior debería medir:

- Disponibilidad y madurez de los conectores requeridos.
- Semántica de entrega, reintentos e idempotencia de cada pipeline.
- Rendimiento con un volumen representativo, no con FakeSource únicamente.
- Manejo de cambios de esquema y tipos propios de cada sistema.
- Operación de checkpoints, recuperación y actualización de versiones.
- Observabilidad, alertas y procedimiento de soporte.
- Integración con el orquestador y la plataforma de secretos existentes.
- Coste operativo frente a otras herramientas de integración.

## 12. Limpieza del laboratorio

Detener PostgreSQL conservando el volumen:

```bash
docker compose down
```

Eliminar también los datos de la PoC:

```bash
docker compose down --volumes
```

El segundo comando borra el volumen de PostgreSQL y, por tanto, no es recuperable salvo que exista una copia de seguridad.

## 13. Referencias oficiales

- [Documentación de Apache SeaTunnel 2.3.13](https://seatunnel.apache.org/docs/2.3.13/)
- [Cómo funciona SeaTunnel](https://seatunnel.apache.org/docs/2.3.13/introduction/how-it-works/)
- [Instalación local y descarga de conectores](https://seatunnel.apache.org/docs/2.3.13/getting-started/locally/deployment/)
- [Inicio rápido con SeaTunnel Engine](https://seatunnel.apache.org/docs/2.3.13/getting-started/locally/quick-start-seatunnel-engine/)
- [Ejecución con Docker](https://seatunnel.apache.org/docs/2.3.13/getting-started/docker/)
- [Conector HTTP](https://seatunnel.apache.org/docs/2.3.13/connectors/source/Http/)
- [Source JDBC para PostgreSQL](https://seatunnel.apache.org/docs/2.3.13/connectors/source/PostgreSQL/)
- [Sink JDBC](https://seatunnel.apache.org/docs/2.3.13/connectors/sink/Jdbc/)
- [Despliegue en Kubernetes](https://seatunnel.apache.org/docs/2.3.13/getting-started/kubernetes/)
- [Licencia Apache 2.0 del proyecto](https://github.com/apache/seatunnel/blob/2.3.13/LICENSE)

