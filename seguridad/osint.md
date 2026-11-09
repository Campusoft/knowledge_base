# OSINT (inteligencia de fuentes abiertas)

> OSINT: *Open Source Intelligence*, traducido como inteligencia de fuentes abiertas.

## Concepto

OSINT es el proceso de responder una pregunta concreta mediante la búsqueda, recopilación, evaluación y análisis de información obtenida de fuentes abiertas o de acceso público. El resultado útil no es una colección de enlaces, sino información analizada, contextualizada y comunicada para responder a un requerimiento. Esta idea coincide con la definición empleada por organismos de inteligencia y en la doctrina de la OTAN ([DIA](https://www.dia.mil/About/Open-Source-Intelligence/), [NATO](https://coi.nato.int/EWCOI/EW%20COI%20Shared%20Documents/EMO%20DOCTRINE%20PREP/AJP%20HARMONIZATION/AJP-2%20EDB%20V1%20E.pdf)).

“Fuentes abiertas” no significa únicamente páginas web ni garantiza que todo contenido sea exacto. Las fuentes pueden incluir publicaciones, registros públicos, mapas, imágenes, informes, repositorios y redes sociales. La información puede estar desactualizada, incompleta, manipulada o fuera de contexto.

## Para qué se utiliza

- **Ciberseguridad defensiva:** conocer la exposición pública de una organización, sus dominios y servicios publicados, y detectar divulgaciones accidentales.
- **Investigación y verificación:** contrastar afirmaciones, fechas, lugares y material publicado, manteniendo trazabilidad de las fuentes.
- **Análisis de contexto:** entender actores, eventos, tendencias o riesgos a partir de datos públicos.
- **Gestión de riesgos:** apoyar evaluaciones de proveedores, incidentes y amenazas con información verificable.

El alcance debe estar autorizado y definido de antemano, especialmente cuando el análisis involucra personas, organizaciones o infraestructura tecnológica.

## Ciclo de trabajo

1. **Definir el requerimiento.** Formular qué se necesita saber, para quién y con qué propósito. Evitar búsquedas amplias sin una pregunta clara.
2. **Planificar.** Determinar alcance, periodo, fuentes apropiadas, restricciones y criterios de éxito.
3. **Recopilar.** Consultar fuentes públicas pertinentes y registrar consultas, fechas y enlaces.
4. **Evaluar y corroborar.** Revisar quién publicó la información, cuándo, con qué evidencia y si fuentes independientes la confirman.
5. **Analizar.** Relacionar hallazgos, distinguir hechos de hipótesis y considerar explicaciones alternativas.
6. **Comunicar.** Presentar conclusiones con evidencia, nivel de confianza, limitaciones y referencias que permitan reproducir la revisión.
7. **Revisar y conservar de forma responsable.** Guardar solo lo necesario, limitar el acceso y aplicar las reglas de retención correspondientes.

## Familias de fuentes

| Familia | Ejemplos | Consideraciones |
|---|---|---|
| Fuentes oficiales y documentales | Sitios gubernamentales, legislación, comunicados, informes y registros accesibles | Confirmar jurisdicción, fecha y versión del documento. |
| Medios y publicaciones | Prensa, revistas, blogs y comunicados corporativos | Distinguir reportes originales de republicaciones y opiniones. |
| Datos técnicos de Internet | DNS, certificados, registros de dominios y documentación técnica pública | Mantener la actividad dentro del alcance autorizado; los datos pueden estar incompletos o desactualizados. |
| Mapas e imágenes | Cartografía, imágenes satelitales y fotografías disponibles públicamente | Revisar fecha, resolución, procedencia y posibles errores de geolocalización. |
| Plataformas sociales y comunidades | Publicaciones, foros, canales y repositorios públicos | El contenido puede ser falso, editado, automatizado o publicado sin consentimiento de las personas mencionadas. |
| Datos abiertos | Portales de datos, estadísticas y conjuntos publicados por organizaciones | Consultar metodología, licencia, cobertura y fecha de actualización. |

## Evaluación de hallazgos

Para cada afirmación relevante, registra:

- **Fuente original:** URL, título, autor u organización y fecha de publicación.
- **Fecha de consulta:** cuándo se observó el contenido, porque puede cambiar o desaparecer.
- **Evidencia:** fragmento, documento o captura estrictamente necesaria, conservando contexto y procedencia.
- **Corroboración:** fuentes independientes que apoyan o contradicen la afirmación.
- **Confianza y límites:** qué se sabe, qué se infiere y qué queda sin confirmar.

La popularidad de una fuente no es prueba de veracidad. Dos páginas que repiten un mismo comunicado no necesariamente son dos confirmaciones independientes. Separa claramente observaciones verificables, inferencias y preguntas pendientes.

## Herramientas

Las herramientas pueden ayudar a buscar, organizar, visualizar o verificar datos, pero no reemplazan el criterio analítico. Elige herramientas según el requerimiento, la procedencia de sus datos, sus condiciones de uso y el impacto en la privacidad. Para empezar, suelen bastar buscadores, sitios oficiales, portales de datos abiertos, mapas, hojas de cálculo y herramientas de archivo o toma de notas.

Antes de automatizar una recopilación, revisa las condiciones del servicio, límites técnicos y políticas aplicables. No intentes eludir controles de acceso ni acceder a cuentas, sistemas o información privada sin autorización.

## Ética, privacidad y límites

- Utiliza información de acceso legítimo y respeta las leyes, políticas internas, condiciones de uso y derechos de las personas.
- No conviertas la investigación en acoso, vigilancia invasiva, doxxing, suplantación o contacto engañoso.
- Evita recopilar o divulgar datos personales que no sean necesarios para el objetivo autorizado.
- No asumas que un dato visible públicamente puede reutilizarse sin límites; evalúa contexto, propósito, sensibilidad y posibles daños.
- Protege notas, capturas y resultados: pueden contener datos personales o información operativa sensible.
- En una investigación de seguridad, limita las pruebas técnicas a activos expresamente autorizados y al alcance acordado.
- Si un hallazgo implica riesgo inmediato o datos sensibles, sigue el proceso de reporte y escalamiento de la organización.

Este documento ofrece orientación general y no sustituye asesoría legal ni la autorización formal necesaria para una investigación concreta.

## Plantilla breve de informe

```text
Título:
Fecha de elaboración:
Requerimiento y propósito:
Alcance y límites:
Resumen de hallazgos:
Hallazgos (evidencia, fuente, fecha y nivel de confianza):
Contradicciones y explicaciones alternativas:
Limitaciones y datos no confirmados:
Conclusión y recomendaciones:
Referencias:
```

## Referencias

- [Defense Intelligence Agency: Open Source Intelligence](https://www.dia.mil/About/Open-Source-Intelligence/)
- [NATO: Allied Joint Publication AJP-2](https://coi.nato.int/EWCOI/EW%20COI%20Shared%20Documents/EMO%20DOCTRINE%20PREP/AJP%20HARMONIZATION/AJP-2%20EDB%20V1%20E.pdf)
- [Portal de datos abiertos de la Unión Europea: What is OSINT?](https://data.europa.eu/mt/node/10386)
