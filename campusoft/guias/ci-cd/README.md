# Guías para aplicar CI/CD en Campusoft

Estas guías explican cómo llevar los requisitos del [estándar CI/CD](../../estandares/ci-cd/ESTANDARES-CI-CD.md) a proyectos y plataformas concretas. No sustituyen ni rebajan el estándar. Los documentos técnicos de detalle se mantienen en su área temática de la base de conocimiento y se enlazan aquí para evitar copias divergentes.

## Mapa de aplicación

| Área | Guías de detalle en la base de conocimiento | Cobertura actual |
|---|---|---|
| Diseño del proceso DevOps | [DevOps](../../../Operation/DevOps.md) | Ciclo de trabajo, capacidades, entrega y operación, sin prescribir plataforma. |
| Seguridad del pipeline | [DevSecOps](../../../Operation/DevSecOps.md) | Ubicación de controles de seguridad, SAST, SCA, secretos, IaC y artefactos. |
| Azure Pipelines | [Azure Pipelines](../../../microsoft/azure-devops/azure-pipelines.md) | YAML, etapas, agentes, artefactos, entornos, aprobaciones y seguridad. |
| Azure DevOps | [Azure DevOps](../../../microsoft/azure-devops/azure-devops.md) y [Azure DevOps Server](../../../microsoft/azure-devops/azure-devops-server.md) | Servicios de plataforma y opción autogestionada. |
| .NET y C# | [Calidad y pruebas .NET](../../../dotnet/net/calidad-y-pruebas/README.md), [EditorConfig](../../../dotnet/net/calidad-y-pruebas/editorconfig.md), [`dotnet format`](../../../dotnet/net/calidad-y-pruebas/dotnet-format.md), [SCA de dependencias .NET](../../../dotnet/net/calidad-y-pruebas/sca-dependencias.md), [Semgrep para C#](../../../dotnet/net/calidad-y-pruebas/semgrep.md), [Gitleaks](../../../git/gitleaks.md), [guía .NET con GitHub Actions](dotnet-github-actions.md) | Formato, pruebas, SAST, SCA, secretos y publicación opcional a GHCR; requiere adaptación por repositorio y criterios de despliegue. |
| JavaScript y TypeScript | [JavaScript y TypeScript](../../../javascript/README.md), [TypeScript](../../../javascript/typescript.md), [DevSecOps](../../../Operation/DevSecOps.md) | Conceptos del lenguaje y controles generales; falta una guía específica de calidad, pruebas, SCA y pipeline para este stack. |

## Guías de implementación

- [CI/CD .NET con GitHub Actions](dotnet-github-actions.md): build, pruebas, Semgrep, NuGet Audit, OWASP Dependency-Check, Gitleaks y publicación opcional de imágenes Docker en GHCR.

## Cómo aplicar el estándar

Para cada repositorio, crea una ficha o documento de implementación del pipeline que registre:

1. tecnologías, proyectos y artefactos que se construyen;
2. eventos que disparan CI/CD y políticas de ramas/revisión;
3. controles ejecutados, alcance, orden y cuáles son bloqueantes;
4. estrategia de pruebas y gestión de dependencias;
5. permisos, secretos, agentes y conexiones utilizados;
6. artefactos producidos, versión, trazabilidad y promoción entre entornos;
7. aprobaciones, controles de despliegue, verificaciones posteriores y recuperación;
8. excepciones aprobadas, dueño, motivo y fecha de revisión;
9. enlaces a YAML, plantillas y configuración que implementan los controles.

Usa las referencias de la tabla para la sintaxis y capacidades de herramientas. Registra en el documento de implementación las decisiones específicas del proyecto; no copies páginas enteras de la base de conocimiento.

## Guías tecnológicas por desarrollar

- **Pipeline .NET:** la [guía inicial con GitHub Actions](dotnet-github-actions.md) cubre validación y análisis; cada proyecto aún debe añadir su publicación de artefactos y despliegue por ambientes.
- **Pipeline JavaScript/TypeScript:** instalación reproducible desde lockfile, lint, chequeo de tipos, pruebas, build, auditoría de dependencias y publicación.
- **Plantillas Azure Pipelines:** cuando haya pipelines compartidos, definir plantillas versionadas para construir y desplegar sin ocultar controles de seguridad.

Estas guías deben incluir ejemplos que puedan adaptarse a los repositorios reales y separar los requisitos comunes de las elecciones propias de Azure Pipelines u otra plataforma.
