# GitHub Actions 

GitHub Actions is a continuous integration and continuous delivery (CI/CD) platform that allows you to automate your build, test, and deployment pipeline. You can create workflows that build and test every pull request to your repository, or deploy merged pull requests to production.

GitHub Actions goes beyond just DevOps and lets you run workflows when other events happen in your repository.  


GitHub Actions es una herramienta de integración continua (CI) y entrega continua (CD) que te permite automatizar, personalizar y ejecutar flujos de trabajo de desarrollo de software directamente dentro de tu repositorio de GitHub.

En esencia, te permite definir una serie de pasos que se ejecutan automáticamente cuando ocurre un evento específico en tu repositorio, como hacer un push de código, crear una pull request, o incluso en un horario programado


# Componentes Clave

Para entender GitHub Actions, es útil conocer sus componentes principales:

- Flujo de trabajo (Workflow): Es un proceso automatizado que tú defines. Se configura mediante un archivo YAML en el directorio .github/workflows/ de tu repositorio.

- Evento (Event): Es la actividad que dispara el flujo de trabajo. Ejemplos comunes son push, pull_request, issue_comment, o schedule.

- Trabajo (Job): Es un conjunto de pasos que se ejecutan en el mismo runner. Un flujo de trabajo puede tener múltiples jobs que se ejecutan en paralelo o secuencialmente.

- Paso (Step): Es una tarea individual dentro de un job. Puede ser un script que ejecutas (como $ npm install) o una Acción que ejecutas.

- Acción (Action): Son comandos preconstruidos que puedes combinar para formar un step. GitHub y la comunidad han creado miles de Actions disponibles en el GitHub Marketplace para tareas comunes (configurar un entorno, autenticarse en servicios en la nube, publicar paquetes, etc.).

- Runner: Es el servidor donde se ejecutan los jobs. GitHub proporciona runners alojados (máquinas virtuales con diferentes sistemas operativos) o puedes usar runners autoalojados (self-hosted).



# Workflows

A workflow is a configurable automated process that will run one or more jobs. Workflows are defined by a YAML file checked in to your repository and will run when triggered by an event in your repository, or they can be triggered manually, or at a defined schedule.


# Integración Continua (CI)

- Compilar el código.
- Ejecutar pruebas unitarias y de integración.
- Analizar la calidad del código.

# Entrega Continua (CD)

- Empaquetar la aplicación (crear imágenes Docker).
- Desplegar el código en entornos de desarrollo, staging o producción (AWS, Azure, GCP, Vercel, etc.).


