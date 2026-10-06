# Entornos virtuales de Python

Un entorno virtual es una instalación aislada de Python para un proyecto. Permite instalar y mantener dependencias del proyecto separadas de los paquetes globales y de los que usan otros proyectos.

## Objetivo

El objetivo principal es controlar qué paquetes y versiones necesita cada proyecto. Así, por ejemplo, una aplicación que requiere una versión de `requests` puede convivir con otra que necesita una versión distinta, sin que sus instalaciones interfieran.

Los entornos virtuales también hacen más predecible la configuración de desarrollo, las pruebas y el despliegue. El entorno suele poder reconstruirse a partir de una lista de dependencias, en vez de compartirse como una carpeta con todos los paquetes instalados.

## Características

- **Aislamiento de paquetes:** al activar el entorno, `python` y `pip` apuntan al entorno y sus paquetes.
- **Independencia por proyecto:** cada proyecto puede definir dependencias y versiones diferentes.
- **Fácil de recrear:** normalmente se crea un entorno nuevo y se reinstalan sus dependencias declaradas.
- **No incluye el intérprete en el repositorio:** el directorio del entorno suele ser local y se excluye del control de versiones.
- **No es una máquina virtual ni un contenedor:** comparte el sistema operativo y el intérprete base; aísla principalmente los paquetes de Python.

## Crear y usar un entorno con `venv`

`venv` forma parte de la biblioteca estándar de Python. Desde la carpeta del proyecto, se crea el entorno así:

```bash
python -m venv .venv
```

`python -m venv` ejecuta el módulo estándar `venv`; `.venv` es la ruta del directorio donde se guardará el entorno. El punto inicial lo hace visible como directorio oculto en algunos sistemas.

### Activarlo

```powershell
# Windows PowerShell
.venv\Scripts\Activate.ps1
```

```bat
:: Windows cmd.exe
.venv\Scripts\activate.bat
```

```bash
# Linux o macOS (bash/zsh)
source .venv/bin/activate
```

Tras la activación, la terminal suele mostrar `(.venv)` y los comandos `python` y `pip` usan el entorno. Para comprobarlo:

```bash
python --version
python -m pip --version
```

Usar `python -m pip` vincula `pip` al intérprete `python` que se invocó y ayuda a evitar instalar paquetes en otro intérprete por accidente.

### Instalar paquetes y salir

```bash
python -m pip install requests
python -m pip list
deactivate
```

`deactivate` devuelve la terminal al intérprete y al entorno previos. No elimina el entorno.

## Casos de uso

### Aplicación o servicio

Crear un entorno por repositorio permite instalar bibliotecas de ejecución sin alterar otros proyectos:

```bash
python -m venv .venv
# Activar el entorno según el sistema operativo
python -m pip install flask
```

### Pruebas de compatibilidad

Se pueden crear entornos separados para comprobar el proyecto con distintas versiones de Python o de sus dependencias. Cada entorno se crea con el intérprete deseado, por ejemplo `py -3.12 -m venv .venv312` en Windows si está instalado el lanzador `py` y esa versión.

### Análisis de datos y notebooks

Un entorno por proyecto ayuda a mantener juntas las versiones de herramientas como `pandas`, `jupyter` o `scikit-learn`, y evita que una actualización para un análisis afecte a otro.

### Automatización y CI

Un flujo de integración continua puede crear un entorno limpio, instalar las dependencias declaradas y ejecutar las comprobaciones. Esto permite detectar dependencias ausentes o instalaciones que solo funcionan en una máquina concreta.

## Registrar dependencias

Para un proyecto pequeño, `pip` puede guardar las versiones instaladas en un archivo:

```bash
python -m pip freeze > requirements.txt
```

En otra máquina o en un entorno recién creado:

```bash
python -m venv .venv
# Activar el entorno
python -m pip install -r requirements.txt
```

`pip freeze` captura lo instalado en ese entorno, incluidas dependencias indirectas. En proyectos más grandes se puede preferir un archivo de dependencias mantenido deliberadamente o herramientas de gestión de proyectos que declaren dependencias directas y un archivo de bloqueo. Conviene documentar cuál es el método de instalación oficial del proyecto.

## Buenas prácticas

1. **Usar un entorno por proyecto** y activarlo antes de instalar paquetes o ejecutar comandos.
2. **Elegir un nombre reconocible**, normalmente `.venv`, para que las herramientas de desarrollo lo detecten.
3. **Excluirlo del control de versiones.** Añadir `.venv/` a `.gitignore`; no guardar el entorno en Git.
4. **Declarar las dependencias** en `requirements.txt` o en los archivos que utilice el gestor del proyecto. No depender de paquetes instalados globalmente.
5. **Instalar con el intérprete correcto:** `python -m pip install ...` reduce confusiones entre varias instalaciones de Python.
6. **Recrear en vez de copiar el entorno** entre equipos o sistemas operativos. Las rutas y algunos componentes pueden depender de la máquina y de la versión de Python.
7. **Mantener separados los paquetes de desarrollo** (por ejemplo, formateadores y herramientas de pruebas) de las dependencias de ejecución cuando el proyecto lo requiera.
8. **No activar el entorno no es requisito para usarlo:** se puede invocar directamente `.venv/bin/python` en Linux/macOS o `.venv\Scripts\python.exe` en Windows, útil en scripts y CI.

## Problemas frecuentes

- **`python` no se reconoce:** verificar que Python esté instalado y disponible en `PATH`; en Windows también puede utilizarse `py`.
- **PowerShell bloquea `Activate.ps1`:** la política de ejecución puede impedir ejecutar scripts. Se puede usar `cmd.exe` o invocar directamente el ejecutable del entorno; cualquier cambio de política debe seguir las reglas del equipo.
- **Los paquetes parecen no estar instalados:** comprobar que el entorno esté activo y revisar `python -m pip --version` para confirmar qué intérprete está usando `pip`.
- **Se movió o copió `.venv`:** crear de nuevo el entorno en su ubicación y reinstalar las dependencias declaradas.
- **El módulo `venv` o `ensurepip` no está disponible:** algunas distribuciones de Linux empaquetan los componentes de creación de entornos por separado; consultar la documentación de la distribución e instalar el paquete correspondiente.

## Referencias

- [Documentación oficial de `venv`](https://docs.python.org/3/library/venv.html)
- [Guía oficial de empaquetado: instalar paquetes](https://packaging.python.org/en/latest/tutorials/installing-packages/)
- [Documentación de `pip install`](https://pip.pypa.io/en/stable/cli/pip_install/)
- [Documentación de `pip freeze`](https://pip.pypa.io/en/stable/cli/pip_freeze/)
