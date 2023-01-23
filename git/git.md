# Taller Git Essentials
###  ¿Qué es un sistema de Control de Versiones?
  * [Wikipedia - Definición de software de Control de versiones](https://en.wikipedia.org/wiki/Version_control)
  * [Wikipedia - Listado de software de Control de versiones](https://en.wikipedia.org/wiki/List_of_version-control_software)

# ¿Qué es Git?
  * [Wikipedia - Git](https://es.wikipedia.org/wiki/Git)
  * [Descargar Git](https://git-scm.com/)
  * [Instrucciones de instalación](https://git-scm.com/book/es/v2/Inicio---Sobre-el-Control-de-Versiones-Instalaci%C3%B3n-de-Git)

# Conociendo la Ayuda de Git
  * [Ayuda en línea ](https://git-scm.com/docs) ó `git help`
  * Guías: `git help -g` 
  * Comandos principales: `git help -a` 

# Flujo de guardado de cambios en el repositorio
  * [Guardando cambios](https://git-scm.com/book/es/v2/Fundamentos-de-Git-Guardando-cambios-en-el-Repositorio)
  * [Estados básicos](https://git-scm.com/book/en/v2/images/lifecycle.png)
  * `git status`
  * `git add <file>` ó `git add .`
  * `git commit -m "mensaje"`
  * `git log --all --graph --decorate --oneline` para revisar el historial
  * `git reset --soft HEAD~1` para deshacer el commit "infierno"
  * `git restore --staged <file|files> para deshacer del "limbo"

# Entendiendo el Modelo de Objetos de Git
  * commit -> puntero a un tree
  * tree   -> un directorio
  * Blob   -> un archivo
  * Tag    -> una etiqueta o marca en un commit
  * [Detalle del modelo de objetos de Git](http://shafiul.github.io/gitbook/1_the_git_object_model.html)
  * `git cat-file -p <HashCommit>` para ir revisando los detalles de los objetos

# Ramificaciones y Fusiones (Branching y Merging)
  * [Ramificar y fusionar](https://git-scm.com/book/es/v2/Ramificaciones-en-Git-Procedimientos-B%C3%A1sicos-para-Ramificar-y-Fusionar)
  * [Fusionamiento avanzado](https://git-scm.com/book/en/v2/Git-Tools-Advanced-Merging)

# Trabajando con remotos
  * [Repositorios remotos](https://git-scm.com/book/es/v2/Fundamentos-de-Git-Trabajar-con-Remotos)

# Video sugerido de Git
  * [Teoría y  Práctica a partir del minuto 26:28](https://www.youtube.com/watch?v=2sjqTHE0zok)

# Laboratorio en grupo
  * Crear cuenta de github
  * Crear repositorio remoto y configurar accesos
  * Varios ejercicios en grupo

# Crear ssh key

1. Abrir git bash
2. ssh-keygen -t rsa -b 4096 -C "[git email account]"
3. Nombre del archivo
4. Frase para encriptar: [cualquier texto]
5. eval $(ssh-agent -s)
6. ssh-add ~/.ssh/[nombre del archivo]
7. Ingresar la frase usada anteriormente
8. Entrar a github.com con las credenciales respectiva e ir a settings/SSH and GPG keys; New SSH key
9. Copiar la key al portapapeles para cargarla a git con: clip < ~/.ssh/[nombre del archivo] o abrir el archivo .pub creado anteriormente
10. Ingresar un nombre que represente esa key y pegar la key copiada anteriormente  

# Comandos básicos
  
  - Clonar un repositorio
	   git clone path_repositorio
	   
	 - Obtener los ultimos cambios de un repositorio
	   git pull
	   
	 - Ver todas las ramas de un repositorio
	   git branch -a
	   
	 - Cambiar a una rama
	   git checkout nombre_rama
	   
	 - Realizar un commit
	   git commit -m "Comentario"
	   
	 - Enviar los cambios al repositorio remoto
	   git push origin nombre_rama_remota

- Shows URLs of remote repositories when listing your current remote connections. By default, listing remote repositories only shows you their shortnames (e.g. "origin"). Using the "-v" option, you will also see the remote's URLs in listings.
 git remote -v

# Varios


Escenario
- Tener un disco externo, en el cual existe algunos repositorios. Utilizar el disco externo en un computador y otro.


```
fatal: detected dubious ownership in repository at '<Folder-Repositorys>'
'<Folder-Repositorys>'' is owned by:
        'S-1-5-21-1866082202-1529751841-862191509-1001'
but the current user is:
        'S-1-5-21-1448444211-3940322899-2338718349-1001'
To add an exception for this directory, call:

        git config --global --add safe.directory <Folder-Repositorys>'

```

# Refencias

Monorepo vs Multi-Repo: Ventajas y Desventajas de las Estrategias de Repositorio de Código 
https://kinsta.com/es/blog/monorepo-vs-multi-repo/
  
