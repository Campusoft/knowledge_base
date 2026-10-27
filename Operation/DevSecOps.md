# DevSecOps

> Documento revisado: 2026-10-22
> Alcance: integración de la seguridad en todo el ciclo DevOps, desde el diseño hasta la operación.

## Descripción

**DevSecOps** es la práctica de integrar la seguridad en la cultura, los procesos y la automatización de DevOps. La seguridad deja de ser una revisión final y pasa a formar parte de cada etapa: planificación, código, build, test, release, despliegue, operación y monitorización.

La idea central es que **la seguridad sea responsabilidad compartida** entre desarrollo, seguridad y operaciones, y que se aplique mediante automatización, políticas y retroalimentación continua, no solo mediante auditorías manuales.

DevSecOps no es una herramienta ni un producto: es un modelo de trabajo apoyado en personas, procesos y plataforma.

## DevOps, DevSecOps, AppSec y SecOps

| Disciplina | Foco | Pregunta principal |
|------------|------|--------------------|
| **DevOps** | Entrega rápida y fiable | ¿Cómo entregamos software de forma continua? |
| **DevSecOps** | Seguridad integrada en DevOps | ¿Cómo entregamos rápido sin aumentar el riesgo? |
| **AppSec** | Seguridad de la aplicación | ¿El software es seguro por diseño y por código? |
| **SecOps** | Operación de seguridad | ¿Detectamos y respondemos a incidentes? |
| **Platform Engineering** | Plataforma interna de desarrollo | ¿Cómo damos guardrails y autoservicio? |

DevSecOps conecta las cuatro primeras: lleva prácticas de AppSec al pipeline, las hace repetibles y las conecta con la operación y la respuesta a incidentes.

## Principios

1. **Seguridad como código.** Políticas, controles y configuraciones versionadas y automatizadas.
2. **Shift left y shift right.** Prevenir en etapas tempranas y detectar también en producción.
3. **Automatización con criterio.** Los controles repetitivos se automatizan; las decisiones de riesgo siguen siendo humanas.
4. **Responsabilidad compartida.** Desarrollo, seguridad, operaciones y producto comparten el resultado.
5. **Mínimo privilegio.** Personas, pipelines, servicios y agentes con los permisos necesarios.
6. **Defensa en profundidad.** Varias capas de control, no una única barrera.
7. **Seguro por defecto.** La configuración segura es la opción fácil y predeterminada.
8. **Feedback rápido.** El desarrollador recibe hallazgos accionables en minutos, no en semanas.
9. **Observabilidad.** Logs, métricas, trazas y auditoría para detectar y explicar incidentes.
10. **Gestión de riesgo basada en prioridades.** No todo hallazgo es crítico; se prioriza por impacto y explotabilidad.
11. **Cumplimiento continuo.** La evidencia se genera automáticamente, no se reconstruye antes de una auditoría.
12. **Cultura sin culpa.** Se aprende del fallo y se corrige el sistema, no se busca un culpable.

## Ciclo de vida

```
Plan ──> Code ──> Build ──> Test ──> Release ──> Deploy ──> Operate ──> Monitor
  │        │        │         │          │           │            │
  │        │        │         │          │           │            └─ SIEM, IR, métricas
  │        │        │         │          │           └─ runtime, WAF, parches
  │        │        │         │          └─ firmas, SBOM, gates
  │        │        │         └─ DAST, fuzzing, pentest
  │        │        └─ SAST, SCA, IaC, contenedores
  │        └─ secret scanning, revisión, pre-commit
  └─ threat modeling, requisitos, privacidad
```

| Etapa | Objetivo | Prácticas | Ejemplos de herramientas |
|-------|----------|-----------|--------------------------|
| **Plan** | Entender el riesgo antes de escribir código | Threat modeling, requisitos de seguridad, clasificación de datos, privacidad | OWASP Threat Dragon, IriusRisk, STRIDE |
| **Code** | Evitar introducir vulnerabilidades y secretos | Secure coding, revisión entre pares, hooks pre-commit, secret scanning, plugins de IDE | Gitleaks, TruffleHog, Semgrep, linters |
| **Build** | Detectar problemas en código y dependencias | SAST, SCA, escaneo de IaC y contenedores, SBOM | CodeQL, SonarQube, Semgrep, Dependabot, Renovate, Trivy, Checkov |
| **Test** | Validar el comportamiento seguro en ejecución | DAST, IAST, fuzzing, tests de seguridad, pentest | OWASP ZAP, Burp Suite, AFL++, OSS-Fuzz |
| **Release** | Asegurar integridad y trazabilidad | Firmas, procedencia, SBOM, gates de política, aprobaciones | Sigstore/cosign, SLSA, in-toto, OPA, Kyverno |
| **Deploy** | Desplegar con configuración segura | Secretos externos, mínimo privilegio, admission control, hardening | Vault, Sealed Secrets, OPA Gatekeeper, Kyverno, CIS Benchmarks |
| **Operate** | Mantener la seguridad en ejecución | Runtime security, WAF, gestión de vulnerabilidades, parches | Falco, Tetragon, Wazuh, Trivy, DefectDojo |
| **Monitor** | Detectar, responder y aprender | SIEM, alertas, incident response, métricas, postmortems | Elastic, Splunk, Microsoft Sentinel, Wazuh |

## Pipeline de referencia

```
┌─────────────┐   ┌─────────────┐   ┌─────────────┐   ┌─────────────┐
│ Pre-commit  │   │ Pull request│   │ Build       │   │ Release     │
│             │   │             │   │             │   │             │
│ secret scan │   │ SAST        │   │ SCA         │   │ SBOM        │
│ lint        │   │ revisión    │   │ IaC scan    │   │ firma       │
│ tests       │   │ tests       │   │ image scan  │   │ policy gate │
└──────┬──────┘   └──────┬──────┘   └──────┬──────┘   └──────┬──────┘
       │                 │                 │                 │
       └─────────────────┴─────────────────┴─────────────────┘
                                   │
                                   ▼
                         ┌──────────────────┐
                         │ Deploy + runtime │
                         │ admission control│
                         │ observabilidad   │
                         └──────────────────┘
```

### Gates recomendados

Un gate debe ser **claro, rápido y accionable**:

- **Bloquear** secretos detectados, artefactos sin firmar, vulnerabilidades críticas explotables y configuraciones IaC críticas.
- **Advertir** vulnerabilidades altas sin explotación conocida y hallazgos de menor impacto.
- **Permitir excepciones** documentadas, con propietario y fecha de expiración.
- **No bloquear** por ruido o falsos positivos sin una vía de resolución.

El objetivo no es tener cero hallazgos, sino **evitar que el riesgo inaceptable llegue a producción**.

## Prácticas clave

### Threat modeling

Identificar activos, actores, superficies de ataque y controles antes de implementar. Debe actualizarse cuando cambia la arquitectura.

### Secure coding

- Validación de entradas y codificación de salidas.
- Parametrización de consultas.
- Gestión segura de sesiones y autenticación.
- Manejo de errores sin filtrar información.
- Cifrado y gestión de claves.
- Dependencias actualizadas.

### Secret scanning

Detectar credenciales en el código, el historial de Git y los artefactos. Un secreto detectado debe **rotarse**, no solo borrarse del archivo. La limpieza del historial con `git filter-repo` o BFG es el último paso, no el primero.

### SAST

Análisis estático del código fuente. Es rápido y se integra en el IDE y el pipeline, pero genera falsos positivos. Conviene empezar por código nuevo y por reglas de alto valor.

### SCA y dependencias

Inventario de dependencias, detección de vulnerabilidades conocidas, licencias y paquetes abandonados. Requiere lockfiles, actualizaciones automatizadas y política de versiones.

### IaC scanning

Revisar Terraform, Kubernetes, Helm, CloudFormation y Docker antes de desplegar: permisos excesivos, cifrado deshabilitado, puertos abiertos, imágenes sin pin.

### Escaneo de contenedores

Escanear la imagen y sus capas, no solo el Dockerfile. Usar imágenes base mínimas, usuario no root, filesystem de solo lectura y registro privado.

### DAST, IAST y fuzzing

Probar la aplicación en ejecución. DAST desde fuera, IAST desde dentro y fuzzing para entradas y parsers. Se complementan con pruebas de seguridad y pentest periódicos.

### SBOM y cadena de suministro

Generar una **Software Bill of Materials** en formato SPDX o CycloneDX. Permite saber qué componentes contiene un artefacto y responder rápido ante una vulnerabilidad.

### Firmas y procedencia

Firmar artefactos e imágenes y verificar su procedencia. SLSA e in-toto aportan niveles y metadatos de la cadena de build.

### Política como código

Expresar reglas como código y evaluarlas automáticamente:

- OPA/Gatekeeper o Kyverno para Kubernetes.
- Conftest para configuraciones.
- Reglas de admisión en el pipeline.
- Excepciones versionadas y con caducidad.

### Gestión de secretos

- Secretos en un gestor dedicado, no en el repositorio.
- Inyección en tiempo de ejecución.
- Rotación automática cuando sea posible.
- Auditoría de acceso.
- Cifrado en tránsito y en reposo.

### Seguridad en runtime

- Detección de comportamiento anómalo en contenedores y hosts.
- WAF y protección contra abuso.
- Límites de recursos y aislamiento.
- Parcheo y gestión de vulnerabilidades continuas.

### Respuesta a incidentes

Runbooks, roles, canales de comunicación, evidencia forense y postmortem sin culpa. La seguridad no termina en el despliegue.

### Cumplimiento continuo

Generar evidencia automática de controles: escaneos, aprobaciones, firmas, inventario, accesos y cambios. Facilita auditorías ISO 27001, SOC 2, PCI DSS o GDPR.

## Herramientas por categoría

| Categoría | Ejemplos representativos |
|-----------|--------------------------|
| **Secret scanning** | Gitleaks, TruffleHog, GitHub Secret Scanning |
| **SAST** | Semgrep, CodeQL, SonarQube, Snyk Code |
| **SCA** | Dependabot, Renovate, Snyk Open Source, OWASP Dependency-Check |
| **DAST** | OWASP ZAP, Burp Suite, Nikto |
| **IAST / RASP** | Contrast, Sqreen (histórico), soluciones comerciales |
| **IaC** | Checkov, Trivy, tfsec, KICS, Terrascan |
| **Contenedores** | Trivy, Grype, Clair, Snyk Container |
| **SBOM** | Syft, CycloneDX, SPDX, Dependency-Track |
| **Firmas y procedencia** | Sigstore/cosign, SLSA, in-toto |
| **Política como código** | OPA/Gatekeeper, Kyverno, Conftest |
| **Secretos** | HashiCorp Vault, AWS Secrets Manager, Azure Key Vault, SOPS, Sealed Secrets |
| **Runtime** | Falco, Tetragon, Sysdig |
| **SIEM / detección** | Wazuh, Elastic, Splunk, Microsoft Sentinel |
| **Vulnerabilidades** | DefectDojo, Dependency-Track, Jira |
| **Threat modeling** | OWASP Threat Dragon, IriusRisk |
| **CI/CD** | GitHub Actions, GitLab CI/CD, Jenkins, Argo CD |

La elección depende del stack, el presupuesto y el equipo. Es mejor **pocas herramientas bien integradas** que muchas desconectadas.

## Integración en CI/CD

Un pipeline típico:

```yaml
stages:
  - pre-commit
  - build
  - test
  - security
  - release
  - deploy

secret-scan:
  stage: pre-commit
  script:
    - gitleaks detect --source . --verbose

sast:
  stage: security
  script:
    - semgrep ci

dependencies:
  stage: security
  script:
    - trivy fs --scanners vuln,secret,misconfig .

iac:
  stage: security
  script:
    - checkov -d infra/

container:
  stage: security
  script:
    - trivy image "$IMAGE"

sbom:
  stage: release
  script:
    - syft "$IMAGE" -o cyclonedx-json > sbom.json

sign:
  stage: release
  script:
    - cosign sign --yes "$IMAGE"
```

Los comandos son ilustrativos; adapta versiones, credenciales y políticas a tu entorno.

## Métricas

| Métrica | Qué mide |
|---------|----------|
| **Cobertura de escaneo** | % de repos, imágenes y despliegues analizados |
| **MTTR de vulnerabilidades** | Tiempo medio hasta remediar por severidad |
| **Vulnerabilidades críticas abiertas** | Riesgo acumulado fuera de SLA |
| **Secretos detectados** | Fugas encontradas y rotadas |
| **Tasa de falsos positivos** | Ruido que genera el sistema |
| **Artefactos firmados** | % de releases con firma y procedencia |
| **Cobertura SBOM** | % de artefactos con inventario |
| **Excepciones activas** | Riesgos aceptados y su caducidad |
| **Tiempo de pipeline** | Coste de los controles en la entrega |
| **Incidentes de seguridad** | Detección, respuesta y aprendizaje |
| **Change failure rate** | Calidad del cambio en producción |
| **Lead time** | Velocidad de entrega |

Las métricas deben servir para mejorar, no para castigar equipos.

## Modelo de madurez

| Nivel | Estado | Características |
|-------|--------|-----------------|
| **0. Inicial** | Manual o inexistente | Revisiones puntuales, secretos en repositorios, sin inventario |
| **1. Básico** | Escaneos aislados | SAST/SCA en algunos repos, sin gates ni propietarios |
| **2. Gestionado** | Integrado en CI | Escaneos automáticos, triage, políticas básicas y responsables |
| **3. Medido** | Gates y métricas | Umbrales, SBOM, firmas, métricas de remediación y excepciones |
| **4. Optimizado** | Continuo y basado en riesgo | Runtime, cadena de suministro, cumplimiento continuo y mejora sistemática |

## Roles y responsabilidades

| Rol | Responsabilidad |
|-----|-----------------|
| **Desarrollo** | Código seguro, corregir hallazgos, tests, revisión entre pares |
| **Seguridad (AppSec)** | Estándares, threat modeling, reglas, triage y formación |
| **Plataforma / DevOps** | Pipeline, automatización, guardrails y herramientas |
| **SRE / Operaciones** | Runtime, parches, disponibilidad y respuesta |
| **Producto** | Priorizar riesgo y aceptar excepciones con criterio |
| **Cumplimiento** | Evidencia, auditoría y requisitos regulatorios |
| **Dirección** | Presupuesto, cultura y tolerancia al riesgo |

## Roadmap de adopción

### Fase 0: inventario

- Repositorios, servicios, imágenes, dependencias y despliegues.
- Datos sensibles y flujos.
- Responsables de cada sistema.

### Fase 1: quick wins

- Secret scanning en pre-commit y CI.
- Actualización automatizada de dependencias.
- SAST en código nuevo.
- Formación básica.

### Fase 2: cobertura

- SAST/SCA en todos los repos.
- Escaneo de IaC y contenedores.
- Triage y propietarios.
- Políticas de severidad.

### Fase 3: gates y trazabilidad

- Gates en el pipeline.
- SBOM y firmas.
- Procedencia y verificación.
- Excepciones con caducidad.

### Fase 4: runtime y cumplimiento

- Seguridad en ejecución.
- SIEM y respuesta a incidentes.
- Evidencia automática.
- Métricas y SLA.

### Fase 5: optimización

- Riesgo basado en explotabilidad.
- Reducción de falsos positivos.
- Autoservicio con guardrails.
- Mejora continua.

## Retos habituales

- **Falsos positivos y fatiga de alertas.** Sin triage, el equipo ignora el sistema.
- **Fricción con el desarrollador.** Controles lentos o poco claros se saltan.
- **Proliferación de herramientas.** Demasiados scanners sin integración.
- **Código legacy.** No se puede arreglar todo a la vez; hay que priorizar.
- **Secretos en el historial.** Borrar el archivo no elimina la fuga.
- **Cadena de suministro.** Dependencias, imágenes y artefactos de terceros.
- **Falta de propietarios.** Un hallazgo sin dueño no se corrige.
- **Cultura.** La seguridad percibida como bloqueo, no como habilitador.
- **Cumplimiento como checkbox.** Evidencia sin control real.
- **Permisos excesivos en CI.** Un pipeline comprometido puede afectar a todo.

## Anti-patrones

- Revisar seguridad solo antes de producción.
- Bloquear releases sin un camino claro de remediación.
- Escanear sin triage, priorización ni responsables.
- Ocultar hallazgos para que el pipeline pase.
- Guardar secretos en variables de repositorio o en el código.
- No rotar un secreto después de detectarlo.
- No generar SBOM ni firmar artefactos.
- Ejecutar contenedores como root o con privilegios.
- Ignorar la seguridad en runtime.
- Aceptar excepciones sin fecha de caducidad.
- Confundir cumplimiento documental con seguridad real.

## Cadena de suministro

La seguridad de la cadena de suministro merece un apartado propio:

- Fijar versiones y usar lockfiles.
- Revisar dependencias directas y transitivas.
- Generar SBOM por release.
- Firmar artefactos e imágenes.
- Verificar firmas antes de desplegar.
- Usar procedencia SLSA/in-toto.
- Limitar permisos del CI y de los tokens.
- Usar registros privados y proxies de dependencias.
- Proteger contra dependency confusion y typosquatting.
- Evaluar la salud del proyecto con OpenSSF Scorecard.

## Cumplimiento

DevSecOps facilita la evidencia para marcos como:

- **NIST SSDF (SP 800-218):** prácticas de desarrollo seguro.
- **OWASP ASVS:** requisitos de verificación de seguridad de aplicaciones.
- **OWASP SAMM:** modelo de madurez de seguridad de software.
- **ISO/IEC 27001:** sistema de gestión de seguridad de la información.
- **SOC 2:** controles de seguridad, disponibilidad y confidencialidad.
- **PCI DSS:** protección de datos de tarjetas.
- **GDPR:** privacidad y protección de datos personales.
- **CIS Benchmarks:** configuración segura de sistemas y nubes.

La interpretación legal y regulatoria corresponde a cada organización; este documento es una guía técnica.

## Casos de uso

- **Startup** que necesita controles básicos sin frenar la entrega.
- **Empresa regulada** que requiere evidencia y trazabilidad.
- **Migración a cloud** con IaC y políticas de seguridad.
- **Kubernetes** con admission control, runtime y supply chain.
- **Open source** con revisiones, dependencias y firma de releases.
- **Legacy** con un plan gradual de cobertura y remediación.
- **Equipos de plataforma** que ofrecen seguridad como autoservicio.

## Ver también

- [DevOps](DevOps.md): cultura, CI/CD y fundamentos.
- [Herramientas](Tools.md): portal y herramientas de plataforma.
- [Observabilidad](Observabilidad.md): métricas, logs y trazas.
- [Training DevOps](Training_DevOps.md): recursos de formación.
- [Gestión de claves criptográficas](../seguridad/gestion.claves.criptograficas.md): secretos y claves.
- [Calidad de código](../architecture/fundamentos/code-quality.md): análisis y estándares.
- [Git](../git/git.md): control de versiones e historial.

## Referencias

- OWASP DevSecOps Guideline: <https://owasp.org/www-project-devsecops-guideline/>
- OWASP SAMM: <https://owasp.org/www-project-samm/>
- OWASP ASVS: <https://owasp.org/www-project-application-security-verification-standard/>
- NIST SSDF (SP 800-218): <https://csrc.nist.gov/publications/detail/sp/800-218/final>
- SLSA: <https://slsa.dev/>
- Sigstore: <https://www.sigstore.dev/>
- OpenSSF Scorecard: <https://github.com/ossf/scorecard>
- CIS Benchmarks: <https://www.cisecurity.org/cis-benchmarks>
- DevSecOps awesome list: <https://github.com/sottlmarek/DevSecOps>
