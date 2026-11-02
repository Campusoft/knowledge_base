# Gitleaks: detección de secretos en repositorios

> Gitleaks ayuda a detectar secretos expuestos; no sustituye la gestión segura de secretos ni la respuesta a incidentes.

## Propósito y arquitectura

Gitleaks es una herramienta de código abierto que busca secretos potenciales —como claves API, tokens, contraseñas y claves privadas— en archivos y repositorios Git. Puede revisar el contenido actual, los cambios preparados para un commit y el historial Git.

Se recomienda combinar tres capas:

1. **Desarrollo local:** hook `pre-commit` para dar feedback antes de crear el commit.
2. **Integración continua:** escaneo en GitHub Actions para validar pushes y pull requests.
3. **Respuesta:** revisar hallazgos y revocar o rotar credenciales reales. Si se expusieron en el historial, coordinar su limpieza después de invalidarlas.

El hook local mejora la prevención, pero puede omitirse o no estar instalado. El pipeline centralizado debe ser el control exigible para integrar cambios.

## Características

- Busca patrones conocidos de credenciales en archivos y repositorios Git.
- Escanea el historial y los cambios preparados para commit.
- Permite personalizar reglas mediante `.gitleaks.toml`.
- Genera reportes en formatos como JSON, CSV, JUnit o SARIF.
- Devuelve un código distinto de cero si detecta hallazgos, útil para bloquear hooks o jobs de CI.
- Ofrece opciones para redactar los valores detectados en la salida.

La detección es heurística: puede producir falsos positivos y no reconocer formatos que sus reglas no cubren.

## Escaneo local

Instala Gitleaks según las instrucciones del [repositorio oficial](https://github.com/gitleaks/gitleaks#installing). Desde la raíz del repositorio:

```bash
# Escanear historial Git
gitleaks git --redact .

# Escanear archivos actuales sin recorrer el historial
gitleaks dir --redact .

# Escanear los cambios preparados para commit
gitleaks git --staged --redact .
```

El subcomando `dir` revisa los archivos actuales de la carpeta indicada; no recorre el historial Git. Para ver detalles de cada hallazgo, añade `--verbose`:

```bash
gitleaks dir --verbose --redact .
```

## Uso mediante Docker Compose

Docker Compose puede ejecutar Gitleaks como un proceso puntual dentro de un contenedor. El archivo Compose debe vivir en el proyecto que vaya a escanear o recibir explícitamente la ruta de ese proyecto. El volumen conecta una carpeta del equipo anfitrión con una ruta dentro del contenedor; `read_only: true` permite leerla sin modificarla.

El siguiente fragmento es **ilustrativo**. No es una configuración ejecutable de esta base de conocimiento; adáptalo al Compose del proyecto que utilizará Gitleaks:

```yaml
services:
  gitleaks:
    image: ghcr.io/gitleaks/gitleaks:<version>
    entrypoint: ["gitleaks"]
    command: ["git", "--verbose", "--redact", "/repo"]
    volumes:
      - type: bind
        source: <ruta-del-proyecto-a-escanear>
        target: /repo
        read_only: true
```

Por ejemplo, si el proyecto Git que se desea revisar está en `D:/proyectos/tienda-api`, esa carpeta completa es la ruta de origen. Dentro del contenedor se verá como `/repo`:

```yaml
volumes:
  - type: bind
    source: D:/proyectos/tienda-api
    target: /repo
    read_only: true
```

Gitleaks leerá desde `/repo`, mientras Docker compartirá la carpeta `D:/proyectos/tienda-api` del equipo anfitrión. Para otro proyecto se sustituye `source` por la ruta de su carpeta raíz de Git. Usa una versión concreta de la imagen y asegúrate de que el contenedor pueda leer `.git` si se analizarán el historial o los cambios preparados. Para escanear archivos actuales sin historial, cambia el comando a `dir --redact /repo`. Consulta la [documentación de Docker Compose](https://docs.docker.com/compose/) para adaptar rutas y sintaxis a cada entorno.

### Compose dentro del mismo repositorio que se escanea

Si el archivo Compose está en la raíz del proyecto que se va a revisar, se puede montar esa raíz con `.`:

```text
tienda-api/
├── .git/
├── docker-compose.gitleaks.yml
└── src/
```

En `docker-compose.gitleaks.yml`:

```yaml
volumes:
  - type: bind
    source: .
    target: /repo
    read_only: true
```

La ruta `.` representa la carpeta del proyecto junto al archivo Compose; dentro del contenedor se accede a ella como `/repo`. Se puede iniciar el escaneo desde esa raíz con:

```bash
docker compose -f docker-compose.gitleaks.yml run --rm gitleaks
```

Si el Compose se guarda en una subcarpeta, ajusta `source` para volver a la raíz del repositorio. Por ejemplo, desde `tools/gitleaks/compose.yaml`, usa `../../`:

```yaml
volumes:
  - type: bind
    source: ../..
    target: /repo
    read_only: true
```

Las rutas relativas del Compose se resuelven tomando como base el archivo de configuración. Por eso, `.` y `../..` apuntan a ubicaciones distintas según dónde se guarde el archivo.

### Mostrar detalles y escanear solo archivos actuales

Para revisar solo los archivos actuales dentro de `/repo`, ejecuta el subcomando `dir`. Al usar Compose, se puede reemplazar el comando configurado en el servicio así:

```bash
docker compose -f docker-compose.gitleaks.yml run --rm gitleaks dir --verbose --redact /repo
```

`--verbose` muestra los datos necesarios para localizar cada hallazgo. Con `--redact`, el valor detectado se oculta. La salida puede tener este aspecto:

```text
Finding:     "<fragmento de línea con el valor oculto>"
Secret:      REDACTED
RuleID:      <regla que detectó el patrón>
File:        src/config/settings.env
Line:        18
Fingerprint: <identificador del hallazgo>
```

Revisa `File` y `Line` para ubicar el código, y `RuleID` para entender qué patrón se activó. El escaneo `dir` no incluye metadatos de commits porque no inspecciona el historial. El ejemplo es ilustrativo: los campos exactos dependen del tipo de hallazgo y la versión de Gitleaks.

## Hook local con pre-commit

El framework [pre-commit](https://pre-commit.com/) instala y ejecuta el hook antes de crear cada commit. En la raíz, crea `.pre-commit-config.yaml`:

```yaml
repos:
  - repo: https://github.com/gitleaks/gitleaks
    rev: v8.30.1
    hooks:
      - id: gitleaks
```

Instala `pre-commit` siguiendo su guía oficial y registra el hook:

```bash
pre-commit install
```

Para ejecutarlo manualmente:

```bash
pre-commit run --all-files
```

El hook revisa los cambios del commit. Para auditar también el historial, ejecuta el comando `gitleaks git --redact .` o el escaneo de CI.

## GitHub Actions

Una opción es usar la acción oficial de Gitleaks. Crea `.github/workflows/gitleaks.yml`:

```yaml
name: Gitleaks

on:
  push:
  pull_request:
  workflow_dispatch:

permissions:
  contents: read

jobs:
  scan:
    name: Scan for secrets
    runs-on: ubuntu-latest
    steps:
      - name: Checkout repository
        uses: actions/checkout@3d3c42e5aac5ba805825da76410c181273ba90b1 # v7.0.1
        with:
          fetch-depth: 0

      - name: Run Gitleaks
        uses: gitleaks/gitleaks-action@e0c47f4f8be36e29cdc102c57e68cb5cbf0e8d1e # v3
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
          # Requerido para repositorios de una organización; no se necesita
          # para repositorios de cuentas personales:
          # GITLEAKS_LICENSE: ${{ secrets.GITLEAKS_LICENSE }}
```

`GITLEAKS_LICENSE` es la licencia de **Gitleaks Action**, no un token del proyecto escaneado ni una licencia necesaria para ejecutar la CLI o la imagen Docker de Gitleaks. La acción la requiere cuando analiza repositorios pertenecientes a una organización de GitHub; no se requiere para cuentas personales. Se obtiene en [gitleaks.io](https://gitleaks.io) y se guarda en GitHub como secreto cifrado del repositorio o de la organización, con el nombre exacto `GITLEAKS_LICENSE`.

La acción debe fijarse a una versión o SHA revisado y actualizarse de forma controlada. Configura la protección de rama para exigir que el job pase antes de integrar el pull request. El checkout completo (`fetch-depth: 0`) permite analizar el historial disponible.

## Configuración y falsos positivos

Gitleaks admite reglas personalizadas y allowlists en `.gitleaks.toml`. Antes de excluir un hallazgo:

1. Confirma que sea un ejemplo, dato sintético o falso positivo.
2. Limita la excepción a la regla y ruta necesarias.
3. Documenta la razón, el responsable y una fecha de revisión.
4. Nunca agregues un secreto real a una allowlist para hacer pasar el pipeline.

### Ejemplo mínimo de `.gitleaks.toml`

Este archivo conserva las reglas predeterminadas de Gitleaks. Guárdalo con el nombre `.gitleaks.toml` en la raíz del repositorio que se escaneará:

```toml
title = "Configuración de Gitleaks"

[extend]
useDefault = true
```

La configuración completa puede incluir reglas propias (`[[rules]]`) y allowlists globales (`[[allowlists]]`) o específicas de una regla (`[[rules.allowlists]]`). Usa allowlists solo para falsos positivos confirmados y limítalas a la ruta, regla o condición necesaria. La sintaxis y un ejemplo completo están en la [referencia oficial de configuración de Gitleaks](https://github.com/gitleaks/gitleaks#configuration).

### Escenarios de configuración

Los siguientes fragmentos son ejemplos independientes para agregar al `.gitleaks.toml` mínimo anterior. Sustituye los nombres, prefijos y patrones por los de tu proyecto. Las expresiones regulares de Gitleaks usan la sintaxis de Go RE2.

#### 1. Detectar un token interno con prefijo reconocible

Útil cuando el sistema emite tokens como `acme_live_` seguido de 32 caracteres alfanuméricos:

```toml
[[rules]]
id = "acme-service-token"
description = "Token del servicio Acme"
regex = '''acme_live_[A-Za-z0-9]{32}'''
keywords = ["acme_live_"]
tags = ["acme", "token"]
```

Gitleaks reportará las coincidencias con ese formato. No pongas tokens reales en la configuración ni en ejemplos del repositorio.

#### 2. Excluir un falso positivo conocido solo en un archivo de pruebas

Si una regla propia detecta una cadena de ejemplo conocida en `tests/fixtures/config.example`, se puede limitar la excepción a esa ruta y a esa cadena. Dentro de la regla correspondiente, agrega:

```toml
    [[rules.allowlists]]
    description = "Valor ficticio documentado en fixture de pruebas"
    condition = "AND"
    paths = ['''^tests/fixtures/config\\.example$''']
    regexTarget = "match"
    regexes = ['''acme_live_00000000000000000000000000000000''']
```

Al usar `AND`, deben coincidir tanto la ruta como la expresión. Ajusta el patrón al valor ficticio exacto y confirma que nunca sea una credencial activa. La ruta se expresa con `/`, incluso al escanear desde Windows.

#### 3. Desactivar una regla predeterminada que no aplica al proyecto

Si tras revisar los hallazgos confirmas que la regla genérica `generic-api-key` produce ruido que no puedes resolver con una allowlist acotada, puedes desactivarla al extender la configuración predeterminada:

```toml
[extend]
useDefault = true
disabledRules = ["generic-api-key"]
```

Esto deja de detectar todos los casos cubiertos por esa regla; úsalo solo si aceptas esa pérdida de cobertura. Prefiere una allowlist específica cuando el falso positivo corresponda a una ruta o valor concreto.

Para hallazgos históricos conocidos, un baseline puede ayudar a adoptar el control sin ocultar nuevas filtraciones. Debe tener propietario y plan para reducir los hallazgos. Los secretos activos requieren respuesta inmediata.

## Buenas prácticas

- Combina el hook local con CI; no dependas de la configuración de cada equipo.
- Exige que el escaneo pase en pull requests y protege la rama principal.
- Mantén Gitleaks y las reglas actualizadas; revisa los cambios al actualizar versiones.
- Redacta secretos en la salida y restringe el acceso a reportes y logs del pipeline.
- Usa un gestor de secretos o variables de entorno, nunca credenciales incrustadas en el código.
- Trata cada hallazgo como potencialmente real hasta verificarlo sin divulgar su valor.
- Si una credencial real se expuso, revócala o rótala de inmediato. Borrar el archivo o reescribir el historial no invalida la credencial.
- Coordina cualquier reescritura del historial con los colaboradores y servicios que consumen el repositorio.
- Asigna responsables y mide cobertura, falsos positivos y tiempo de remediación.

## Referencias

- [Repositorio y README de Gitleaks](https://github.com/gitleaks/gitleaks)
- [Escaneo con Gitleaks](https://github.com/gitleaks/gitleaks/wiki/Scanning)
- [Acción oficial de Gitleaks](https://github.com/gitleaks/gitleaks-action)
- [pre-commit](https://pre-commit.com/)
- [DevSecOps](../Operation/DevSecOps.md)
- [GitHub Actions](github.md)
- [Gestión de secretos](../architecture/seguridad/seguridad-aplicativa/gestor.secretos.md)
