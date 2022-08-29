# Python



To install Python using the Microsoft Store:
https://docs.microsoft.com/en-us/windows/python/beginners


How to Install Python on Windows 10
https://www.digitalocean.com/community/tutorials/install-python-windows-10


CMD opens Windows Store when I type 'python'
Use the Windows search bar to find "Manage app execution aliases". There should be two aliases for Python. Unselect them, and this will allow the usual Python aliases "python" and "python3". See the image below.
https://stackoverflow.com/questions/58754860/cmd-opens-windows-store-when-i-type-python

# Varios

Requirements Files

“Requirements files” are files containing a list of items to be installed using pip install like so:

python -m pip install -r requirements.txt

https://pip.pypa.io/en/stable/user_guide/

Crear el archivo requirements.txt utilizando pip en la línea de comandos (ejecuta los siguiente en la raíz del repositorio):

```
pip3 freeze > requirements.txt
```
