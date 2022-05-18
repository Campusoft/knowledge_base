# Python


# Python virtual environment

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


