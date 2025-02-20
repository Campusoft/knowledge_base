# Python


# Python virtual environment / Entornos Virtuales



Un entorno virtual es un entorno Python en el que el intérprete Python, las bibliotecas y los scripts instalados en él están aislados de los instalados en otros entornos virtuales, y (por defecto) cualquier biblioteca instalada en un «sistema» Python, es decir, uno que esté instalado como parte de tu sistema operativo.
Un entorno virtual es un árbol de directorios que contiene archivos ejecutables de Python y otros archivos que indican que es un entorno virtual.


 Virtual environment, a self-contained directory tree that contains a Python installation for a particular version of Python, plus a number of additional packages.
 
 Virtual environments are a common and effective technique used in Python development. 
 
 Virtual environments provide a simple solution to a host of potential problems. In particular, they help you to:

- Resolve dependency issues by allowing you to use different versions of a package for different projects. For example, you could use Package A v2.7 for Project X and Package A v1.3 for Project Y.
- Make your project self-contained and reproducible by capturing all package dependencies in a requirements file.
- Install packages on a host on which you do not have admin privileges.
- Keep your global site-packages/ directory tidy by removing the need to install packages system-wide which you might only need for one project.
	

A virtual environment is a Python tool for dependency management and project isolation. They allow Python site packages (third party libraries) to be installed locally in an isolated directory for a particular project, as opposed to being installed globally (i.e. as part of a system-wide Python).

To create a virtual environment, decide upon a directory where you want to place it, and run the venv module as a script with the directory path:

```
python3 -m venv tutorial-env
```


Once you’ve created a virtual environment, you may activate it.

On Windows, run:

tutorial-env\Scripts\activate.bat

On Unix or MacOS, run:

source tutorial-env/bin/activate

Activating the virtual environment will change your shell’s prompt to show what virtual environment you’re using, and modify the environment so that running python will get you that particular version and installation of Python. 

```
(tutorial-env) $ python
```

# Frameworks

## Flask 



# WSGI

WSGI son las siglas de Web Server Gateway Interface. Es una especificación que describe cómo se comunica un servidor web con una aplicación web, y cómo se pueden llegar a encadenar diferentes aplicaciones web para procesar una solicitud/petición (o request).

WSGI es un estándar Python que está descrito en detalle en la especificación PEP 3333.

Actualmente Gunicorn es el servidor HTTP WSGI más usado por los Pythonistas. El nombre viene de la abreviatura Green Unicorn. Está escrito en Python y su uso es extremadamente simple.


Usar Nginx como proxy inverso y Gunicorn como servidor WSGI
![image](https://github.com/proyecto-facturacion/erp/assets/222181/24e72ece-f627-4100-a650-1d9e7d634cd3)


# ASGI

ASGI (Asynchronous Server Gateway Interface) is a spiritual successor to WSGI, intended to provide a standard interface between async-capable Python web servers, frameworks, and applications.

Where WSGI provided a standard for synchronous Python apps, ASGI provides one for both asynchronous and synchronous apps, with a WSGI backwards-compatibility implementation and multiple servers and application frameworks.



# Revisiones

Celery es un gestor de tareas distribuido y asíncrono desarrollado en Python. Es una herramienta magnífica para aplicaciones de alta disponibilidad y con alta carga, y también recomendable cuando consideramos que la carga va a ir aumentando progresivamente y vamos a ir incorporando nuevas máquinas poco a poco a nuestro cluster inicial.

Los nodos o workers

Los nodos o workers son programas Python que se pueden ejecutar en modo demonio en un servidor. Se pueden ejecutar uno o varios nodos por servidor. Cada nodo se puede configurar para que coja tareas de un determinado tipo y según una configuración particular.

Si tienes tareas especializadas que quieres ejecutar en un servidor concreto, entonces solo lanzarás nodos de un determinado tipo en dicho servidor.

