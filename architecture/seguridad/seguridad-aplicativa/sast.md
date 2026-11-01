# SAST: análisis estático de seguridad de aplicaciones

## Concepto

**SAST** (*Static Application Security Testing*) es el análisis del código fuente, bytecode o artefactos de una aplicación para identificar patrones que podrían producir vulnerabilidades, sin ejecutar la aplicación. Se le suele llamar análisis de caja blanca porque puede examinar la implementación interna.

El análisis puede buscar patrones directos —por ejemplo, una consulta SQL construida concatenando entradas— y flujos de datos desde un origen no confiable (*source*) hasta una operación peligrosa (*sink*), considerando si hubo validación o saneamiento intermedio. La profundidad del análisis depende del motor, el lenguaje, el framework y las reglas habilitadas.

## Qué puede detectar

Según las reglas disponibles, un SAST puede señalar:

- Inyección SQL, de comandos o de plantillas.
- Cross-site scripting (XSS) y codificación insegura de salidas.
- Traversal de rutas y acceso inseguro a archivos.
- Deserialización insegura y uso riesgoso de criptografía.
- Validaciones, controles de acceso o manejo de errores implementados de forma insegura.
- Uso de APIs peligrosas o configuraciones inseguras en código.

La detección de secretos, dependencias vulnerables, infraestructura como código y comportamiento en ejecución son áreas relacionadas, pero normalmente requieren controles específicos como secret scanning, SCA, análisis de IaC y DAST.

## Características

- **Analiza sin ejecutar:** puede ejecutarse durante edición, revisión de cambios o CI.
- **Reglas y modelos:** combina reglas de patrones, análisis semántico y, en herramientas compatibles, análisis de flujo de datos (*taint analysis*).
- **Hallazgos localizables:** normalmente reporta archivo, línea, regla, severidad y una explicación; algunos motores muestran el flujo de datos que conduce al riesgo.
- **Cobertura dependiente del contexto:** lenguajes, frameworks, reglas y profundidad varían entre herramientas y ediciones.
- **Resultados probabilísticos en la práctica:** produce falsos positivos y puede omitir vulnerabilidades. Un resultado limpio no demuestra por sí solo que el sistema sea seguro.
- **Integrable al ciclo de desarrollo:** puede informar en el IDE, pre-commit, pull request, pipeline y panel de seguridad.

## Flujo recomendado

```text
Desarrollador / IDE
        ↓ feedback temprano
Pull request / CI ──→ SAST sobre cambios y política de hallazgos
        ↓ integración aprobada
Rama principal ─────→ análisis completo periódico y seguimiento
```

La revisión en pull request acorta el tiempo de corrección. Un análisis completo de la rama principal ayuda a encontrar problemas preexistentes, revisar cobertura y detectar cambios en reglas o dependencias del analizador.

## Herramientas

| Herramienta | Enfoque | Consideraciones |
|---|---|---|
| [Semgrep](https://semgrep.dev/docs/semgrep-code/overview) | Análisis guiado por reglas y patrones, con capacidades semánticas y de flujo de datos según motor y plan. Permite usar reglas existentes y crear reglas propias. | Revisar qué capacidades corresponden a la edición y al lenguaje. Las reglas transparentes facilitan explicar y ajustar hallazgos. |
| [GitHub CodeQL](https://codeql.github.com/docs/codeql-overview/about-codeql/) | Convierte el código en una base de datos consultable y ejecuta consultas para localizar vulnerabilidades y variantes de errores. Se integra con code scanning de GitHub y también ofrece CLI para CI propio. | Verificar lenguajes, configuración y consultas disponibles para el repositorio; los análisis pueden requerir configurar compilación y entorno. |
| [SonarQube](https://docs.sonarsource.com/sonarqube-server/quality-standards-administration/managing-rules/security-related-rules) | Análisis estático con reglas de seguridad; ciertas reglas identifican inyecciones siguiendo datos desde entradas hasta operaciones sensibles. | Cobertura y capacidades de seguridad dependen del lenguaje y del producto o edición. Distinguir vulnerabilidades de *Security Hotspots* que requieren revisión humana. |

La selección no debe basarse solo en la cantidad de reglas. Compara cobertura de los lenguajes y frameworks reales, calidad del análisis de flujo, integración con el flujo de pull requests, formato de reportes, operación alojada o autogestionada, privacidad del código, coste y capacidad del equipo para atender resultados.

## Buenas prácticas

1. **Empieza con una línea base.** Ejecuta un análisis completo, clasifica los hallazgos existentes y asigna responsables; evita introducir de golpe una política que bloquee todo el trabajo por deuda heredada.
2. **Prioriza el código nuevo.** Bloquea o exige revisión para vulnerabilidades nuevas de severidad alta, y crea un plan gradual para los hallazgos antiguos.
3. **Aplica el análisis temprano y en CI.** Usa feedback local cuando sea práctico y una comprobación centralizada en pull requests; no dependas únicamente de herramientas instaladas en cada equipo.
4. **Valida los hallazgos.** Confirma si la ruta es alcanzable, qué datos controla un atacante y si existen validaciones efectivas antes de marcar un resultado como falso positivo.
5. **Documenta las excepciones.** Registra razón, responsable, alcance y fecha de revisión. Evita desactivar reglas globalmente para silenciar pocos casos.
6. **Protege la salida.** Los reportes pueden incluir código, rutas internas y fragmentos sensibles; limita acceso y retención, especialmente en servicios externos.
7. **Mantén reglas y analizadores bajo control de cambios.** Actualiza versiones de forma periódica, revisa cambios de reglas y observa variaciones importantes en el número de hallazgos.
8. **Haz accionables los resultados.** Incluye contexto y orientación de corrección, capacita al equipo y mide el tiempo de remediación, no solo el número de escaneos.
9. **Combínalo con otros controles.** SAST no reemplaza revisión de diseño, pruebas dinámicas, análisis de dependencias, escaneo de secretos ni pruebas manuales para riesgos de negocio.

## Métricas útiles

- Repositorios y lenguajes cubiertos por análisis exitosos.
- Hallazgos nuevos por severidad y antigüedad.
- Tiempo medio de triage y remediación.
- Porcentaje de falsos positivos y excepciones pendientes de revisión.
- Incidentes o defectos de seguridad detectados después de liberar.

## Documentación relacionada

- [Seguridad de aplicaciones](security.md): contexto de pruebas estáticas y dinámicas.
- [DevSecOps](../../../Operation/DevSecOps.md): ubicación de SAST dentro del ciclo y pipeline.
- [SCA y dependencias](../../../Operation/DevSecOps.md#sca-y-dependencias): análisis de componentes de terceros.

## Referencias

- [Semgrep Code: overview and rules](https://semgrep.dev/docs/semgrep-code/overview)
- [GitHub CodeQL: about CodeQL](https://codeql.github.com/docs/codeql-overview/about-codeql/)
- [SonarQube: security-related rules](https://docs.sonarsource.com/sonarqube-server/quality-standards-administration/managing-rules/security-related-rules)
