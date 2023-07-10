
# Bransh / Tags

Ramas, basadas en GitFlow. El GitFlow Workflow es una metodología de trabajo basada en el división de las distintas etapas de producción de software en distintas ramas del repositorio:

- main/master: En la rama máster se encuentran las releases estables de nuestro software. Esta es la rama que un usuario típico se descargará para usar nuestro software, por lo que todo lo que hay en esta rama debería ser funcional. Sin embargo, puede que las últimas mejoras introducidas en el software no estén disponibles todavía en esta rama.
- develop: En esta rama surge de la última release de master. En ella se van integrando todas las nuevas características hasta la siguiente release.
- feature-X: Cada nueva mejora o característica que vayamos a introducir en nuestro software tendrá una rama que contendrá su desarrollo. Las ramas de feature salen de la rama develop y una vez completado el desarrollo de la mejora, se vuelven a integrar en develop.
- release-X: Las ramas de release se crean cuando se va a publicar la siguiente versión del software y surgen de la rama develop . En estas ramas, el desarrollo de nuevas características se congela, y se trabaja en arreglar bugs y generar documentación. Una vez listo para la publicación, se integra en master y se etiqueta con el número de versión correspondiente. Se integran también con develop, ya que su contenido ha podido cambiar debido a nuevas mejoras.
- hotfix-X: Si nuestro código contiene bugs críticos que es necesario parchear de manera inmediata, es posible crear una rama hotfix a partir de la publicación correspondiente en la rama master. Esta rama contendrá únicamente los cambios que haya que realizar para parchear el bug. Una vez arreglado, se integrará en master, con su etiqueta de versión correspondiente y en develop.


# GitFlow Workflow



# Referencias

Buenas prácticas al trabajar con Git - Sigue el GitFlow Workflow
https://david-estevez.gitbooks.io/the-git-the-bad-and-the-ugly/content/es/buenas-practicas-al-trabajar-con-git.html



Flujo de trabajo de Gitflow
https://www.atlassian.com/es/git/tutorials/comparing-workflows/gitflow-workflow


Ilustracion squash vs merge vs rebase
https://twitter.com/kamranahmedse/status/1512392092537999368
