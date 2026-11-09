# Guía inicial CI/CD para .NET con GitHub Actions

| Campo | Valor |
|---|---|
| ID | CAMP-GUIA-CICD-DOTNET-GHA-001 |
| Versión | 0.2.0 |
| Fecha de versión | 2026-11-08 |
| Estado | Guía inicial para adaptar y validar en cada repositorio |
| Propietario | Por designar |
| Revisión prevista | Al actualizar el estándar, SDK o herramientas del pipeline |
| Alcance | Repositorios Campusoft de .NET/C# que usan GitHub Actions |

## 1. Propósito

Proporcionar un flujo inicial para proyectos .NET que compile el código, ejecute pruebas, aplique controles de seguridad con Semgrep (SAST), NuGet Audit/`dotnet package list --vulnerable` y OWASP Dependency-Check (SCA), y Gitleaks para detectar secretos. Si el proyecto incluye un `Dockerfile`, la guía también muestra cómo validar la imagen en pull requests y publicarla en GitHub Container Registry (GHCR) desde ramas/tags autorizados.

La guía implementa requisitos de la [Política CI/CD](../../politicas/POLITICA-CI-CD.md) y del [Estándar CI/CD](../../estandares/ci-cd/ESTANDARES-CI-CD.md). Es una receta tecnológica de partida; cada equipo debe completar rutas, versiones, umbrales, ramas protegidas, tiempos de atención y estrategia de despliegue de su producto.

## 2. Flujo incluido

```text
Pull request / push a main / ejecución manual / agenda
                         │
         ┌───────────────┼────────────────┐
         ▼               ▼                ▼
       Gitleaks       Semgrep         Build y pruebas
       secretos        SAST           NuGet Audit
                                           │
                                           ▼
                                OWASP Dependency-Check
                                           │
                                           ▼
                    Docker build ── PR: validar sin publicar
                         │           main/release tag: GHCR
                         ▼
              imagen versionada + digest para despliegue
```

Los jobs de análisis pueden ejecutarse en paralelo. La protección de ramas debe exigir como checks requeridos los jobs que la política del proyecto define como bloqueantes. El job de imagen es opcional: inclúyelo solo si el repositorio tiene `Dockerfile`. Valida la construcción en PR sin publicar; publica a GHCR solo en `main` y tags de release `v*`. Este ejemplo no despliega a ambientes. El despliegue debe añadirse en un job separado, protegido con environments, aprobaciones, permisos y secretos de solo despliegue.

## 3. Valores que deben adaptarse

Antes de copiar el workflow, define:

| Valor | Ejemplo del workflow | Ajuste requerido |
|---|---|---|
| Solución | `src/Product.sln` | Ruta real a `.sln`, `.slnx` o proyecto. |
| SDK | `10.0.x` | SDK compatible con los proyectos y con `global.json`. Fija la versión en el repositorio y usa la misma en CI. |
| Rama principal | `main` | Rama protegida real. Añade reglas para release si aplica. |
| Configuración | `Release` | Configuración de build acordada. |
| Umbral de vulnerabilidad | `7` CVSS en Dependency-Check | Riesgo aceptado y regla de bloqueo; no uses CVSS como único criterio. |
| Umbrales NuGet | NU1903/NU1904 | Ajustar severidad bloqueante y estrategia para deuda existente. |
| Dockerfile y contexto | `src/Api/Dockerfile`, `.` | Ajustar a la aplicación que se publica y al contexto mínimo que necesita el build. |
| Nombre del paquete GHCR | `ghcr.io/${{ github.repository }}` | Confirmar la organización, nombre del repositorio y visibilidad deseada del paquete. |
| Tags de imagen | branch, versión semántica, SHA | Definir formato, política de inmutabilidad, tags de release y si se permitirá `latest`. |
| Versiones de herramientas/actions | fijadas en YAML | Actualizar de forma controlada; preferir SHA completo para actions y versión/tag o digest inmutable para imágenes. |

Si la solución depende de paquetes privados, configura credenciales de restore con permisos de lectura mínimos y evita exponerlas a workflows originados en forks.

## 4. Workflow inicial

Guarda el archivo como `.github/workflows/dotnet-ci.yml`. Es un ejemplo integrado; requiere adaptar `SOLUTION`, `DOTNET_VERSION`, la rama y los criterios de bloqueo. Las referencias de actions usan tags legibles como punto de partida: para uso organizacional, revisa sus fuentes y fíjalas a SHA completos aprobados.

```yaml
name: .NET CI

on:
  pull_request:
    branches: [main]
  push:
    branches: [main]
    tags: ["v*"]
  workflow_dispatch:
  schedule:
    - cron: "23 6 * * 1"

permissions:
  contents: read

concurrency:
  group: dotnet-ci-${{ github.workflow }}-${{ github.ref }}
  cancel-in-progress: true

env:
  SOLUTION: src/Product.sln
  DOTNET_VERSION: "10.0.x"

jobs:
  build-test-nuget-audit:
    name: Build, tests y auditoría NuGet
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v6

      - name: Configurar .NET SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}

      - name: Crear carpeta de reportes
        run: mkdir -p artifacts/security

      - name: Restaurar y auditar dependencias
        run: dotnet restore "${SOLUTION}" -p:NuGetAudit=true -p:NuGetAuditMode=all

      - name: Listar dependencias NuGet vulnerables
        run: dotnet package list --project "${SOLUTION}" --vulnerable --include-transitive --format json --output-version 1 --no-restore > artifacts/security/nuget-vulnerabilities.json

      - name: Verificar formato y reglas .NET
        run: dotnet format "${SOLUTION}" --verify-no-changes --no-restore

      - name: Compilar
        run: dotnet build "${SOLUTION}" --configuration Release --no-restore

      - name: Ejecutar pruebas
        run: dotnet test "${SOLUTION}" --configuration Release --no-build --no-restore

      - name: Publicar reporte NuGet
        if: always()
        uses: actions/upload-artifact@v7.0.1
        with:
          name: nuget-vulnerability-report
          path: artifacts/security/nuget-vulnerabilities.json
          if-no-files-found: ignore
          retention-days: 14

  semgrep:
    name: Semgrep SAST
    runs-on: ubuntu-latest
    container:
      image: semgrep/semgrep:1.182.0
    steps:
      - name: Checkout
        uses: actions/checkout@v6

      - name: Crear carpeta de reportes
        run: mkdir -p artifacts/security

      - name: Analizar código C#
        run: semgrep scan --config p/csharp --error --sarif --output artifacts/security/semgrep.sarif .

      - name: Publicar reporte Semgrep
        if: always()
        uses: actions/upload-artifact@v7.0.1
        with:
          name: semgrep-sast-report
          path: artifacts/security/semgrep.sarif
          if-no-files-found: ignore
          retention-days: 14

  gitleaks:
    name: Gitleaks secret scan
    runs-on: ubuntu-latest
    steps:
      - name: Checkout del historial completo
        uses: actions/checkout@v6
        with:
          fetch-depth: 0

      - name: Crear carpeta de reportes
        run: mkdir -p artifacts/security

      - name: Escanear historial y contenido Git
        run: >-
          docker run --rm
          -v "${GITHUB_WORKSPACE}:/repo"
          -w /repo
          ghcr.io/gitleaks/gitleaks:v8.30.1
          git --redact --report-format sarif
          --report-path artifacts/security/gitleaks.sarif .

      - name: Publicar reporte Gitleaks
        if: always()
        uses: actions/upload-artifact@v7.0.1
        with:
          name: gitleaks-report
          path: artifacts/security/gitleaks.sarif
          if-no-files-found: ignore
          retention-days: 14

  dependency-check:
    name: OWASP Dependency-Check SCA
    runs-on: ubuntu-latest
    # Los secretos no se entregan a pull requests de forks.
    if: github.event_name != 'pull_request' || github.event.pull_request.head.repo.fork == false
    env:
      NVD_API_KEY: ${{ secrets.NVD_API_KEY }}
    steps:
      - name: Checkout
        uses: actions/checkout@v6

      - name: Crear carpetas de reporte y datos
        run: mkdir -p artifacts/security .dependency-check-data

      - name: Escanear componentes y artefactos
        run: >-
          docker run --rm
          -v "${GITHUB_WORKSPACE}:/src"
          -v "${GITHUB_WORKSPACE}/artifacts/security:/report"
          -v "${GITHUB_WORKSPACE}/.dependency-check-data:/usr/share/dependency-check/data"
          -w /src
          owasp/dependency-check:13.0.0
          --project "Campusoft .NET"
          --scan /src
          --out /report
          --format SARIF
          --failOnCVSS 7
          --nvdApiKey "${NVD_API_KEY}"

      - name: Publicar reporte Dependency-Check
        if: always()
        uses: actions/upload-artifact@v7.0.1
        with:
          name: dependency-check-report
          path: artifacts/security/dependency-check-report.sarif
          if-no-files-found: ignore
          retention-days: 14

  # Incluir estos jobs solo si el repositorio construye una imagen Docker.
  container-image-check:
    name: Validar Dockerfile en pull request
    needs:
      - build-test-nuget-audit
      - semgrep
      - gitleaks
      - dependency-check
    if: >-
      always() &&
      github.event_name == 'pull_request' &&
      needs.build-test-nuget-audit.result == 'success' &&
      needs.semgrep.result == 'success' &&
      needs.gitleaks.result == 'success' &&
      (needs.dependency-check.result == 'success' ||
       needs.dependency-check.result == 'skipped')
    runs-on: ubuntu-latest
    permissions:
      contents: read
    env:
      REGISTRY: ghcr.io
      IMAGE_NAME: ${{ github.repository }}
      DOCKERFILE: ./src/Api/Dockerfile
      DOCKER_CONTEXT: .
    steps:
      - name: Checkout
        uses: actions/checkout@v6

      - name: Configurar Docker Buildx
        uses: docker/setup-buildx-action@v4

      - name: Crear metadata y tags
        id: meta
        uses: docker/metadata-action@v6
        with:
          images: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}
          tags: |
            type=ref,event=branch
            type=ref,event=pr
            type=semver,pattern={{version}}
            type=semver,pattern={{major}}.{{minor}}
            type=sha

      - name: Construir imagen sin publicarla
        uses: docker/build-push-action@v7
        with:
          context: ${{ env.DOCKER_CONTEXT }}
          file: ${{ env.DOCKERFILE }}
          push: false
          tags: ${{ steps.meta.outputs.tags }}
          labels: ${{ steps.meta.outputs.labels }}

  container-image-publish:
    name: Publicar imagen versionada en GHCR
    needs:
      - build-test-nuget-audit
      - semgrep
      - gitleaks
      - dependency-check
    if: >-
      always() &&
      github.event_name == 'push' &&
      (github.ref == 'refs/heads/main' || startsWith(github.ref, 'refs/tags/v')) &&
      needs.build-test-nuget-audit.result == 'success' &&
      needs.semgrep.result == 'success' &&
      needs.gitleaks.result == 'success' &&
      needs.dependency-check.result == 'success'
    runs-on: ubuntu-latest
    permissions:
      contents: read
      packages: write
    env:
      REGISTRY: ghcr.io
      IMAGE_NAME: ${{ github.repository }}
      DOCKERFILE: ./src/Api/Dockerfile
      DOCKER_CONTEXT: .
    steps:
      - name: Checkout
        uses: actions/checkout@v6

      - name: Configurar Docker Buildx
        uses: docker/setup-buildx-action@v4

      - name: Crear metadata y tags
        id: meta
        uses: docker/metadata-action@v6
        with:
          images: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}
          tags: |
            type=ref,event=branch
            type=semver,pattern={{version}}
            type=semver,pattern={{major}}.{{minor}}
            type=sha

      - name: Login a GHCR
        uses: docker/login-action@v4
        with:
          registry: ${{ env.REGISTRY }}
          username: ${{ github.actor }}
          password: ${{ secrets.GITHUB_TOKEN }}

      - name: Construir y publicar imagen
        id: build
        uses: docker/build-push-action@v7
        with:
          context: ${{ env.DOCKER_CONTEXT }}
          file: ${{ env.DOCKERFILE }}
          push: true
          tags: ${{ steps.meta.outputs.tags }}
          labels: ${{ steps.meta.outputs.labels }}
          cache-from: type=gha
          cache-to: type=gha,mode=max

      - name: Registrar digest de la imagen
        run: echo "Docker image digest: ${{ steps.build.outputs.digest }}" >> "$GITHUB_STEP_SUMMARY"
```

Para tags de release semánticos, crea tags Git con prefijo `v` (por ejemplo `v1.4.2`). El job de PR construye sin acceso de escritura a paquetes y sin publicar. El job de publicación solo corre en push a `main` o a un tag `v*`, después de que los controles requeridos terminen correctamente. Los demás eventos (agenda o ejecución manual) no publican imágenes.

### 4.1 Preparar el repositorio y el paquete GHCR

Antes de activar los jobs:

1. Añade y revisa un `Dockerfile` multi-stage para la aplicación desplegable. Alinea la versión de las imágenes base SDK/runtime con el target framework del proyecto.
2. Añade `.dockerignore` para excluir `.git`, `bin`, `obj`, artefactos locales, archivos de configuración privados y secretos. Mantén el contexto de build tan pequeño como sea posible.
3. Define `DOCKERFILE` y `DOCKER_CONTEXT` en ambos jobs. Ajusta también nombres de proyectos, dependencias entre proyectos y puertos usados por la aplicación.
4. Habilita GitHub Actions y concede `packages: write` únicamente al job de publicación. `GITHUB_TOKEN` autentica el push; no se requiere un PAT de usuario para publicar el paquete asociado a este repositorio.
5. Decide la visibilidad del paquete (privado o público), sus permisos de lectura y su vínculo al repositorio en las opciones del paquete GitHub.
6. Configura protección de ramas y de tags para que solo releases autorizados puedan publicar versiones.

El nombre base `ghcr.io/${{ github.repository }}` se normaliza a minúsculas mediante Docker Metadata Action. Los tags de branch y SHA sirven para trazabilidad; los tags semver se producen desde tags Git `vX.Y.Z`. Evita desplegar con `latest`: despliega por tag inmutable o, preferiblemente, por digest `sha256` registrado por el job.

Ejemplo de `.dockerignore` que debe adaptarse a la estructura del proyecto:

```dockerignore
.git/
**/bin/
**/obj/
**/.vs/
artifacts/
.env
.env.*
secrets/
```

### 4.2 Dockerfile .NET multi-stage de referencia

Este esqueleto reduce la imagen final al runtime y a los archivos publicados. Ajusta las rutas, referencias de proyecto, versión SDK/runtime, puertos y usuario al servicio real.

```dockerfile
ARG DOTNET_VERSION=10.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copiar primero los archivos de proyecto permite reutilizar la capa de restore.
COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
RUN dotnet restore "src/Api/Api.csproj"

COPY . .
WORKDIR /src/src/Api
RUN dotnet publish "Api.csproj" --configuration Release --no-restore \
    --output /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app
COPY --from=build /app/publish .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "Api.dll"]
```

La imagen base y el `Dockerfile` son parte de la cadena de suministro: mantén tags de base controlados, considera fijar digest para requisitos estrictos de reproducibilidad, revisa actualizaciones de parches y escanea la imagen resultante. No copies archivos `.env`, credenciales, certificados privados ni configuración sensible a la imagen. Para secretos necesarios solo durante el build, utiliza BuildKit secret mounts; no los pases como `ARG` o `ENV` persistentes.

### 4.3 Consumo y promoción de la imagen

Después de publicar, registra el nombre del paquete, el tag y el digest inmutable. Los ambientes posteriores deben descargar y promover esa misma imagen ya validada, no reconstruirla:

```bash
docker pull ghcr.io/ORG/REPOSITORY:sha-COMMIT
docker inspect ghcr.io/ORG/REPOSITORY:sha-COMMIT
```

NuGet Audit y Dependency-Check en este workflow revisan paquetes/componentes del repositorio; no equivalen a un escaneo completo de capas y paquetes del sistema operativo dentro de la imagen. Para despliegues de mayor criticidad, agrega un escáner de imágenes (por ejemplo, Trivy), firma/atestación de procedencia y una política que verifique el digest antes de desplegar. Consulta [escaneo de contenedores en DevSecOps](../../../Operation/DevSecOps.md#escaneo-de-contenedores). Estos controles pueden añadirse como jobs posteriores y deben compartir la referencia exacta del digest.

### 4.4 Acciones Docker utilizadas

El ejemplo usa `docker/setup-buildx-action`, `docker/login-action`, `docker/metadata-action` y `docker/build-push-action`. Revisa repositorio, mantenedores y versión; fija cada action a SHA completo aprobado. `GITHUB_TOKEN` requiere `packages: write` para publicar; el job de validación de PR solo tiene permisos de lectura y no inicia sesión en GHCR.

## 5. Qué valida cada job

### Build, pruebas y NuGet Audit

- **Restore:** resuelve paquetes y ejecuta NuGet Audit. El ejemplo solicita auditoría de dependencias directas y transitivas.
- **Listado JSON:** entrega un informe explícito para revisión y conservación. Se ejecuta después de restore y con `--no-restore` para evitar otra restauración.
- **Formato/análisis:** `dotnet format --verify-no-changes` falla si hay cambios de formato o correcciones aplicables según configuración.
- **Build:** compila toda la solución en `Release` usando las dependencias restauradas.
- **Tests:** ejecuta los proyectos de prueba incluidos en la solución.

`dotnet package list --vulnerable` genera información; no debe suponerse que su código de salida bloquea todas las vulnerabilidades en todas las versiones del SDK. Para bloquear según severidad, establece `NuGetAudit`/`NuGetAuditMode` en configuración común del repositorio (por ejemplo `Directory.Build.props`) y define advertencias como errores con revisión de la deuda existente. Ejemplo orientativo para bloquear altas y críticas:

```xml
<Project>
  <PropertyGroup>
    <NuGetAudit>true</NuGetAudit>
    <NuGetAuditMode>all</NuGetAuditMode>
    <WarningsAsErrors>$(WarningsAsErrors);NU1903;NU1904</WarningsAsErrors>
  </PropertyGroup>
</Project>
```

La disponibilidad del nombre `dotnet package list` depende del SDK: se introdujo con .NET 10; para SDK 9 o anteriores usa `dotnet list "${SOLUTION}" package --vulnerable --include-transitive --format json --no-restore`. Confirma opciones en `dotnet list package --help` o `dotnet package list --help` con el SDK fijado.

### Semgrep

El ejemplo ejecuta el conjunto C# del Registry y falla ante hallazgos configurados como bloqueantes con `--error`. Revisa versión, reglas y formato de salida antes de promoverlo a check requerido. La cobertura de C# y el análisis interarchivo dependen de la edición y del motor: Semgrep CE tiene límites de sintaxis/profundidad diferentes de Semgrep Code. Valida la versión de lenguaje C# que usa el repositorio; si es necesario, configura el motor/plan compatible y protege su token como secreto.

### Gitleaks

Se escanea el historial Git completo (`fetch-depth: 0`) además del contenido que conoce el subcomando. En un repositorio con deuda histórica, revisa los hallazgos y establece una línea base controlada antes de convertir el primer escaneo completo en bloqueo. No silencies findings sin verificar; una credencial expuesta requiere revocación o rotación.

El ejemplo usa el contenedor de Gitleaks CLI. La acción GitHub de Gitleaks tiene versiones y términos/licencias propios; si se decide usarla, revisa su estado y condiciones antes de adoptarla. Fija imagen y acción a versiones o digest/SHA aprobados.

### OWASP Dependency-Check

El escaneo analiza el árbol de trabajo montado en `/src`, crea reportes SARIF y hace fallar el job a partir del umbral CVSS indicado. El repositorio debe configurar el secreto `NVD_API_KEY` en GitHub Actions. La base de datos de vulnerabilidades es persistida en el directorio `.dependency-check-data` durante el job; en runners efímeros esta caché desaparece al terminar. Evalúa una caché protegida o un servicio compartido para evitar descargas completas en cada ejecución, controlando su antigüedad y concurrencia.

Dependency-Check es complemento de NuGet Audit: en .NET, revisa qué archivos/artefactos detecta y valida la identificación del paquete. Sus coincidencias CPE pueden requerir triage para distinguir falsos positivos. Escanea archivos del repositorio, así que define exclusiones para código generado, binarios voluminosos o directorios irrelevantes sin excluir manifiestos de dependencias.

## 6. Seguridad y gobierno del workflow

- Configura permisos de rama para que solo cambios revisados modifiquen `.github/workflows/**` y la configuración de escáneres.
- Exige los checks acordados en protección de ramas; incluir un job en YAML por sí solo no lo vuelve un gate obligatorio.
- Usa `permissions` mínimas. El workflow base solo requiere lectura del repositorio; añadir permisos de escritura solo para funciones concretas, por ejemplo subir SARIF al panel de seguridad.
- Fija actions externas a SHA completo, revisa repositorio, mantenedores, permisos y cambios antes de actualizar la referencia. Fija contenedores a tags exactos y, para mayor inmutabilidad, a digest aprobado.
- No uses `pull_request_target` para ejecutar código no confiable del PR con secretos o permisos privilegiados.
- No expongas tokens de Semgrep, NVD, NuGet privado ni credenciales de despliegue a workflows de forks. El job de Dependency-Check se omite para forks por depender de `NVD_API_KEY`; las otras comprobaciones no necesitan secretos.
- No compartas agentes autohospedados con trabajos no confiables y ambientes de despliegue sensibles.
- Limita retención y acceso a reportes. Los resultados pueden contener nombres de paquetes, rutas y fragmentos de código.
- El workflow usa artefactos por job de CI. Si se necesitan reportes en el mismo artifact bundle, descargarlos/combinar desde jobs dependientes o usar un job de agregación.

## 7. PR, ramas protegidas y despliegue

1. Habilita branch protection para `main`: pull request requerido, revisores designados, resolución de conversaciones y checks requeridos.
2. Los jobs de build/test, Semgrep y Gitleaks deben ser obligatorios conforme a la política de Campusoft. Decide si Dependency-Check también bloquea PR o corre como análisis informativo/periódico por su duración y necesidad de datos NVD.
3. Ejecuta un escaneo completo al menos en la rama principal y de forma periódica para encontrar avisos publicados después de integrar una dependencia.
4. Para desplegar, añade job(s) separados que descarguen el artefacto validado; no vuelvas a compilar para producción.
5. Protege los GitHub Environments de staging/production con aprobadores, restricciones de rama, secretos por ambiente y permisos mínimos.
6. Ejecuta smoke tests post-deploy y documenta rollback/recuperación según el proyecto.

## 8. Adaptación por tipo de proyecto

### Solución multiproyecto

Usa la solución raíz para restore/build/test si todos los proyectos deben verificarse juntos. Si hay módulos independientes, divide jobs o matrices y mapea claramente qué controles cubren cada proyecto.

### Monorepo

Usa filtros de rutas para ahorrar ejecuciones solo si los cambios compartidos y los archivos del workflow disparan igualmente las validaciones correctas. Ejecuta también escaneos completos programados para detectar dependencias nuevas, advisories y cobertura omitida.

### .NET Framework legado

No asumas que `ubuntu-latest` compila todos los proyectos .NET Framework. Puede requerirse `windows-latest`, MSBuild/Visual Studio Build Tools y ajustes de restore/test. Mantén gates de seguridad separados de las particularidades de compilación y documenta runtimes y herramientas.

### Proyecto sin tests automatizados

No elimines el paso sin dejar constancia; crea al menos una suite inicial apropiada, define una excepción temporal con responsable y fecha objetivo, y evita afirmar que el pipeline valida comportamiento no cubierto.

## 9. Lista de puesta en marcha

- [ ] Confirmar que el estándar y la política aplicables estén aprobados o que esta ejecución se trate como piloto.
- [ ] Definir SDK, `global.json`, solución, proyectos de test y plataformas destino.
- [ ] Incorporar configuración de NuGet Audit y decidir cómo tratar warnings NU190x.
- [ ] Revisar cobertura y licencia/reglas de Semgrep para la versión C# del repositorio.
- [ ] Revisar baseline histórico de Gitleaks y asignar responsables para hallazgos existentes.
- [ ] Crear `NVD_API_KEY` como secreto de Actions y valorar caché persistente para Dependency-Check.
- [ ] Revisar contenedores/actions y fijarlos a SHA/digest aprobados.
- [ ] Probar casos controlados que hagan fallar cada gate y verificar la publicación de reportes.
- [ ] Proteger la rama principal y requerir los checks acordados.
- [ ] Documentar nombres de artefactos, retención, proceso de triage y contactos responsables.
- [ ] Añadir deployment solo después de definir ambientes, autorizaciones, secretos, health checks y rollback.

## 10. Documentación relacionada

- [Política CI/CD Campusoft](../../politicas/POLITICA-CI-CD.md)
- [Estándares CI/CD](../../estandares/ci-cd/ESTANDARES-CI-CD.md)
- [Índice de guías CI/CD Campusoft](README.md)
- [Azure Pipelines](../../../microsoft/azure-devops/azure-pipelines.md): alternativa/plataforma CI/CD documentada en la base de conocimiento.
- [Calidad y pruebas .NET](../../../dotnet/net/calidad-y-pruebas/README.md)
- [SCA de dependencias .NET](../../../dotnet/net/calidad-y-pruebas/sca-dependencias.md)
- [Semgrep para .NET y C#](../../../dotnet/net/calidad-y-pruebas/semgrep.md)
- [Gitleaks](../../../git/gitleaks.md)
- [DevSecOps](../../../Operation/DevSecOps.md)

## 11. Referencias oficiales

- [GitHub Actions: compilar y probar .NET](https://docs.github.com/en/actions/tutorials/build-and-test-code/net)
- [GitHub Actions: seguridad y uso seguro](https://docs.github.com/en/actions/reference/security/secure-use)
- [GitHub Actions: sintaxis de workflows](https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax)
- [Semgrep: soporte C#](https://semgrep.dev/docs/languages/csharp)
- [Semgrep: integración en CI](https://semgrep.dev/docs/semgrep-ci/sample-ci-configs)
- [Microsoft: auditoría NuGet](https://learn.microsoft.com/nuget/concepts/auditing-packages)
- [OWASP Dependency-Check: argumentos CLI](https://dependency-check.github.io/DependencyCheck/dependency-check-cli/arguments.html)
- [OWASP Dependency-Check: analizadores de archivos](https://dependency-check.github.io/DependencyCheck/analyzers/index.html)
- [Gitleaks: CLI y configuración](https://github.com/gitleaks/gitleaks)
- [GitHub: publicar imágenes Docker en GitHub Packages](https://docs.github.com/en/actions/tutorials/publish-packages/publish-docker-images)
- [Docker: Build and push action](https://github.com/docker/build-push-action)
- [Docker: Metadata action](https://github.com/docker/metadata-action)
- [Docker: Login action](https://github.com/docker/login-action)
- [Microsoft: crear aplicaciones .NET en contenedores](https://learn.microsoft.com/dotnet/core/docker/build-container)

## 12. Historial de cambios

| Versión | Fecha | Cambio |
|---|---|---|
| 0.2.0 | 2026-11-08 | Añade validación de Dockerfile en PR y publicación controlada de imágenes .NET en GHCR. |
| 0.1.0 | 2026-11-08 | Guía inicial de CI con build, pruebas, SAST, SCA y escaneo de secretos. |
