# Git y GitHub

Esta carpeta reúne apuntes para trabajar con Git, organizar ramas y etiquetas, usar GitHub Actions y proteger repositorios frente a secretos expuestos.

## Estructura

```text
git/
├── README.md (índice)
├── catalogo-git-y-github.md
├── git.md
├── branch_tag.md
├── guia-gestion-ramas.md
├── escenarios-gestion-ramas.md
├── github.md
├── tool.git.md
└── gitleaks.md
```

## Contenido

| Archivo | Contenido |
|---|---|
| [git.md](git.md) | Taller y conceptos básicos: control de versiones, comandos, estados, commits, remotos, historial, ramas y fusiones. |
| [branch_tag.md](branch_tag.md) | Ramas y etiquetas, con una introducción a Gitflow y referencias sobre flujos de trabajo. |
| [guia-gestion-ramas.md](guia-gestion-ramas.md) | Referencia adaptable paso a paso: tareas, sincronización, PR/MR, integración, publicación, hotfixes y limpieza. |
| [escenarios-gestion-ramas.md](escenarios-gestion-ramas.md) | Casos prácticos y criterios de decisión. Primer escenario: publicar cambios pequeños sin bloquearse por funcionalidades grandes. |
| [github.md](github.md) | Conceptos de GitHub Actions: workflows, eventos, jobs, steps, acciones y runners. Incluye un enlace a la guía de Gitleaks para escaneo en `push` y `pull_request`. |
| [tool.git.md](tool.git.md) | Herramientas y recursos para generar archivos `.gitignore`. |
| [gitleaks.md](gitleaks.md) | Guía de detección de secretos: escaneo local, hook `pre-commit`, GitHub Actions, uso conceptual de Docker Compose, configuración y respuesta ante hallazgos. |

## Recorridos sugeridos

- **Empezar con Git:** [git.md](git.md) → [branch_tag.md](branch_tag.md).
- **Trabajar con ramas en equipo:** [guía práctica](guia-gestion-ramas.md) → [escenarios y alternativas](escenarios-gestion-ramas.md).
- **Automatizar en GitHub:** [github.md](github.md).
- **Prevenir secretos en commits y pushes:** [gitleaks.md](gitleaks.md), que incluye ejemplos para adaptar a cada proyecto.

## Documentación relacionada

- [DevSecOps](../Operation/DevSecOps.md): prácticas de seguridad en el ciclo de desarrollo y secret scanning.
- [Gestión de secretos](../architecture/seguridad/seguridad-aplicativa/gestor.secretos.md): almacenamiento, acceso y protección de credenciales.
