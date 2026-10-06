# Desarrollo web con Python

Esta guía reúne conceptos comunes del desarrollo web en Python. Su tema principal es WSGI, la interfaz estándar que conecta aplicaciones Python síncronas con servidores web. También incluye servidores, middleware, despliegue y su relación con ASGI.

## WSGI

**WSGI** significa **Web Server Gateway Interface**. Es una especificación de interfaz entre un servidor web y una aplicación web Python. Está definida en [PEP 3333](https://peps.python.org/pep-3333/), la versión para Python 3 de la especificación WSGI.

WSGI establece un contrato: el servidor recibe una solicitud HTTP y llama a la aplicación Python con los datos de la solicitud; la aplicación devuelve el estado, las cabeceras y el contenido de la respuesta. WSGI no es un framework ni un servidor web: es el protocolo de aplicación que permite que servidores y frameworks compatibles trabajen juntos.

### Clasificación y componentes

WSGI se clasifica como una **interfaz estándar de servidor a aplicación** (gateway interface) del ecosistema Python. Sus partes habituales son:

- **Servidor HTTP/WSGI:** acepta conexiones HTTP, administra procesos o hilos y llama a la aplicación WSGI. Ejemplos: Gunicorn, Waitress, uWSGI y mod_wsgi para Apache.
- **Aplicación WSGI:** objeto invocable de Python que procesa solicitudes. Puede ser una aplicación escrita directamente con WSGI o el objeto que expone un framework como Django o Flask.
- **Middleware:** componente WSGI que se coloca entre servidor y aplicación para adaptar o envolver llamadas; puede añadir funciones como registro, autenticación o enrutamiento.
- **Proxy inverso (opcional):** Nginx o Apache puede recibir el tráfico público, terminar TLS, servir archivos estáticos y reenviar las solicitudes al servidor WSGI.

Un servidor de desarrollo integrado en un framework sirve para desarrollar localmente. En producción suele utilizarse un servidor WSGI dedicado, con la configuración de procesos y proxy apropiada para la aplicación.

### Ciclo de una solicitud

1. El cliente envía una solicitud HTTP.
2. El servidor WSGI analiza la solicitud y construye el diccionario `environ` con información del entorno y de la solicitud.
3. El servidor llama al objeto de aplicación con `environ` y la función `start_response`.
4. La aplicación llama a `start_response` con el estado y las cabeceras, y devuelve un iterable de bytes.
5. El servidor envía la respuesta al cliente y cierra el iterable cuando termina.

WSGI es una interfaz síncrona: el servidor invoca la aplicación y espera su resultado conforme al contrato WSGI. La concurrencia y el número de procesos o hilos los gestiona el servidor y su configuración.

### Contrato de una aplicación WSGI

Una aplicación WSGI básica es un objeto invocable con esta forma:

```python
def application(environ, start_response):
    body = b"Hola desde WSGI\n"
    status = "200 OK"
    headers = [
        ("Content-Type", "text/plain; charset=utf-8"),
        ("Content-Length", str(len(body))),
    ]
    start_response(status, headers)
    return [body]
```

- `environ` es un diccionario con metadatos de la solicitud y del entorno WSGI. Entre sus claves estándar figuran `REQUEST_METHOD`, `PATH_INFO`, `QUERY_STRING`, `SERVER_NAME`, `SERVER_PORT`, `wsgi.version`, `wsgi.input` y `wsgi.url_scheme`.
- `start_response(status, response_headers, exc_info=None)` inicia la respuesta con un estado como `200 OK` y una lista de pares `(nombre, valor)` para las cabeceras. El argumento opcional `exc_info` permite comunicar errores durante el procesamiento según la especificación.
- El valor devuelto es un iterable cuyos elementos son bytes (`bytes`). El servidor consume esos bloques para transmitir el cuerpo de la respuesta.

En una aplicación real se deben respetar los detalles de PEP 3333 sobre lectura del cuerpo de la solicitud, respuestas de error, cierre del iterable y cabeceras. No se deben devolver cadenas `str` como cuerpo: se devuelven bytes.

### Aplicación mínima ejecutable

Guarda la función anterior en `hello.py`, con el nombre `application`. Una forma simple de ejecutarla localmente usando la biblioteca estándar es:

```python
# run_local.py
from wsgiref.simple_server import make_server
from hello import application

with make_server("127.0.0.1", 8000, application) as server:
    print("Servidor local en http://127.0.0.1:8000")
    server.serve_forever()
```

`wsgiref` es una implementación de referencia útil para aprendizaje y pruebas básicas; no se recomienda como servidor de producción. Para producción se suele elegir un servidor dedicado y configurarlo de acuerdo con el framework y el entorno.

### Frameworks y objeto de aplicación

Los frameworks suelen implementar internamente la interfaz WSGI y exponer un objeto que el servidor puede cargar. En Flask, por ejemplo, una instancia de `Flask` se puede usar como aplicación WSGI:

```python
# myapp.py
from flask import Flask

app = Flask(__name__)

@app.get("/")
def home():
    return "Hola"
```

Gunicorn puede cargar el objeto `app` del módulo `myapp` con `myapp:app`. El nombre antes de `:` es el módulo importable y el de después es el objeto de aplicación.

```bash
python -m pip install gunicorn
gunicorn --workers 2 myapp:app
```

Este comando es un ejemplo de ejecución; los requisitos y opciones dependen del sistema y del despliegue. Gunicorn está orientado a sistemas Unix. Para Windows se puede considerar Waitress u otra opción compatible con la plataforma.

En Django, el proyecto normalmente expone `application` en `mi_proyecto/wsgi.py`. Un servidor puede apuntar a ella con una referencia equivalente a `mi_proyecto.wsgi:application`.

### Servidores WSGI

| Servidor | Descripción general | Uso habitual |
| --- | --- | --- |
| Gunicorn | Servidor Python con modelo de workers; carga aplicaciones mediante una ruta como `modulo:objeto`. | Despliegues Unix con aplicaciones WSGI y algunos modos ASGI. |
| Waitress | Servidor WSGI escrito en Python, multiplataforma, incluido Windows. | Aplicaciones WSGI en Windows y otros sistemas. |
| uWSGI | Servidor y conjunto de herramientas con varias opciones de despliegue e integración. | Despliegues que ya usan su protocolo y ecosistema. |
| mod_wsgi | Módulo para alojar aplicaciones WSGI dentro de Apache HTTP Server. | Entornos basados en Apache. |
| `wsgiref` | Implementación de referencia incluida en la biblioteca estándar. | Aprendizaje, depuración y pruebas locales sencillas. |

La elección depende de la plataforma, el framework, el patrón de concurrencia, la infraestructura existente y las necesidades operativas. Revisar la documentación del servidor elegido antes de desplegar.

### Middleware WSGI

Un middleware es, a su vez, una aplicación WSGI: recibe `environ` y `start_response`, y llama a otra aplicación WSGI. Por eso puede envolver la aplicación sin que el servidor tenga que conocer su implementación.

```python
class AddHeader:
    def __init__(self, app):
        self.app = app

    def __call__(self, environ, start_response):
        def start_response_with_header(status, headers, exc_info=None):
            headers = list(headers)
            headers.append(("X-Example", "enabled"))
            return start_response(status, headers, exc_info)

        return self.app(environ, start_response_with_header)
```

Se puede envolver una aplicación así: `application = AddHeader(application)`. En producción es preferible utilizar middleware mantenido por el framework o una biblioteca reconocida cuando exista una opción apropiada.

### WSGI detrás de un proxy inverso

Una topología frecuente es:

```text
Navegador --HTTPS--> Nginx/Apache --HTTP local o socket--> servidor WSGI --> aplicación
```

El proxy puede gestionar certificados TLS, compresión, límites de petición y archivos estáticos; el servidor WSGI ejecuta la aplicación Python. Para desplegar correctamente:

- limitar el acceso directo al servidor de aplicación si solo debe recibir tráfico del proxy;
- configurar explícitamente los encabezados de proxy que el servidor y framework necesitan;
- confiar en encabezados reenviados como `X-Forwarded-For` o `X-Forwarded-Proto` solo cuando los establece un proxy de confianza;
- definir workers, timeouts, registros y reinicio del servicio según la carga y el entorno;
- no exponer el servidor de desarrollo del framework como servidor público de producción.

### Ventajas y límites

**Ventajas**

- Define una frontera común entre frameworks y servidores compatibles.
- Permite intercambiar el servidor de aplicación sin reescribir la lógica web.
- Facilita el uso de middleware compuesto con el mismo contrato.
- Cuenta con un ecosistema maduro de servidores y frameworks.

**Límites**

- El contrato WSGI es síncrono y no define directamente WebSockets ni el manejo nativo de conexiones asíncronas de larga duración.
- El modelo de concurrencia efectivo depende del servidor, los workers, los hilos y las características de la aplicación.
- WSGI no especifica por sí solo cómo desplegar, balancear carga, terminar TLS o administrar secretos.

## WSGI y ASGI

**ASGI** (Asynchronous Server Gateway Interface) es una interfaz más reciente que admite aplicaciones asíncronas y patrones de conexión de larga duración, incluidos WebSockets. WSGI sigue siendo adecuado para muchas aplicaciones HTTP síncronas.

| Aspecto | WSGI | ASGI |
| --- | --- | --- |
| Interfaz | Llamada síncrona con `environ` y `start_response` | Aplicación asíncrona que recibe `scope`, `receive` y `send` |
| Uso típico | Solicitudes HTTP de tipo petición/respuesta | HTTP asíncrono, WebSockets y conexiones de larga duración |
| Ecosistema | Muy extendido en frameworks y servidores Python tradicionales | Usado por frameworks y servidores que admiten programación asíncrona |
| Adaptación | Algunas aplicaciones ASGI pueden exponer compatibilidad WSGI mediante adaptadores, con límites | Puede alojar código síncrono mediante mecanismos de adaptación, con costes y restricciones |

Elegir ASGI cuando la aplicación requiere WebSockets o concurrencia asíncrona de extremo a extremo; elegir WSGI cuando el framework y el patrón de la aplicación son síncronos y cubren las necesidades. La decisión debe considerar las capacidades reales de cada componente.

## Buenas prácticas

1. Mantener separadas la lógica de la aplicación, el servidor WSGI y el proxy inverso; son componentes con responsabilidades distintas.
2. Usar un servidor de producción compatible con el framework y el sistema operativo, en lugar del servidor de desarrollo.
3. Declarar las dependencias y fijar versiones según la política del proyecto; recrear el entorno desde sus archivos de dependencias.
4. Probar la aplicación con el mismo tipo de servidor y configuración que se utilizarán en el despliegue.
5. Evitar trabajo lento o bloqueante dentro de una solicitud sin configurar timeouts y capacidad adecuados; trasladar trabajos de larga duración a una cola cuando corresponda.
6. No asumir que WSGI proporciona aislamiento, TLS, autenticación o protección contra tráfico malicioso: esas funciones requieren controles en la aplicación y la infraestructura.
7. Configurar con cuidado los proxies confiables y los encabezados reenviados para evitar confiar en valores que el cliente pueda falsificar.
8. Revisar documentación y recomendaciones de seguridad vigentes del servidor y del framework usados.

## Referencias

- [PEP 3333: Python Web Server Gateway Interface v1.0.1](https://peps.python.org/pep-3333/) — especificación WSGI.
- [Documentación de Python: `wsgiref`](https://docs.python.org/3/library/wsgiref.html) — implementación de referencia.
- [Documentación de Gunicorn: ejecutar una aplicación](https://docs.gunicorn.org/en/stable/run.html) — carga de aplicaciones y configuración de workers.
- [Documentación de Waitress](https://docs.pylonsproject.org/projects/waitress/en/stable/) — servidor WSGI multiplataforma.
- [Documentación de mod_wsgi](https://modwsgi.readthedocs.io/en/develop/) — alojar aplicaciones WSGI con Apache.
- [Especificación ASGI](https://asgi.readthedocs.io/en/latest/specs/main.html) — interfaz asíncrona y conceptos relacionados.
- [Guía de Django: despliegue WSGI](https://docs.djangoproject.com/en/stable/howto/deployment/wsgi/) — objeto `application` de Django.
