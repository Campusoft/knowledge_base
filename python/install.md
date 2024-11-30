# Python



To install Python using the Microsoft Store:
https://docs.microsoft.com/en-us/windows/python/beginners


How to Install Python on Windows 10
https://www.digitalocean.com/community/tutorials/install-python-windows-10


CMD opens Windows Store when I type 'python'
Use the Windows search bar to find "Manage app execution aliases". There should be two aliases for Python. Unselect them, and this will allow the usual Python aliases "python" and "python3". See the image below.
https://stackoverflow.com/questions/58754860/cmd-opens-windows-store-when-i-type-python

# Anaconda

Anaconda es una distribución de código abierto de los lenguajes de programación Python y R que se utiliza para aplicaciones de ciencia de datos, aprendizaje automático e inteligencia artificial.

Incluye más de 250 paquetes populares de ciencia de datos y herramientas de gestión para simplificar la instalación y despliegue de paquetes.

Anaconda incluye pip como parte de su instalación. Aunque Anaconda tiene su propio administrador de paquetes llamado conda

# pip

pip es el administrador de paquetes oficial de Python. Su función principal es facilitar la instalación, actualización y gestión de bibliotecas y paquetes de Python desde el índice oficial de paquetes, conocido como PyPI (Python Package Index). Es una herramienta esencial para cualquier desarrollador que trabaje con Python.


# Varios

Requirements Files

“Requirements files” are files containing a list of items to be installed using pip install like so:

python -m pip install -r requirements.txt

https://pip.pypa.io/en/stable/user_guide/

Crear el archivo requirements.txt utilizando pip en la línea de comandos (ejecuta los siguiente en la raíz del repositorio):

```
pip3 freeze > requirements.txt
```




**Herramientas para entornos virtuales**

1. virtualenv (Clásico y muy usado):
Una de las herramientas más populares.
Permite crear entornos virtuales de forma sencilla.

2. venv (Incluido en Python 3.3 y versiones posteriores):
Es una herramienta estándar de Python.
Similar a virtualenv pero no necesita instalación adicional.

3. conda (Usado principalmente con Anaconda/Miniconda):
Más avanzado, permite gestionar Python y paquetes de datos como NumPy y Pandas.