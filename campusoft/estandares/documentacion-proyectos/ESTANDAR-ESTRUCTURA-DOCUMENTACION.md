# Estándar de estructura de documentación de proyectos

| Campo | Valor |
|---|---|
| Identificador | CAMP-DOC-STRUCT-001 |
| Versión | 1.0.0 |
| Fecha de versión | 2026-11-14 |
| Estado | Borrador para revisión y aprobación |
| Propietario | Por designar |
| Autoridad aprobadora | Por designar |
| Vigencia | Pendiente de aprobación |
| Alcance | Repositorios de proyectos de software Campusoft |

## 1. Propósito

Proporcionar una organización predecible para la documentación de proyectos, que facilite su creación, consulta, mantenimiento y reutilización. La versión inicial define nueve áreas: producto, requerimientos, arquitectura, desarrollo, plantillas, seguridad, operación, capacitación y guías.

## 2. Estructura de referencia

```text
doc/
├── README.md
├── producto/
├── requerimientos/
│   ├── README.md
│   ├── 00-propuestas/                 # Opcional
│   ├── 01-requisitos/
│   │   ├── producto/
│   │   ├── modulos/                   # Cuando aplique
│   │   └── plataforma/                # Cuando aplique
│   ├── 02-analisis/
│   │   ├── modulos/                   # Cuando aplique
│   │   └── plataforma/                # Cuando aplique
│   ├── 03-planes-implementacion/
│   │   ├── modulos/                   # Cuando aplique
│   │   └── plataforma/                # Cuando aplique
│   ├── 04-estado-sistema/
│   └── 90-insumos/
├── arquitectura/
│   ├── README.md
│   ├── vistas/
│   └── decisiones/
├── desarrollo/
│   ├── README.md
│   ├── build/
│   └── convenciones/
├── plantillas/
│   ├── README.md
│   ├── arquitectura/
│   └── requerimientos/
├── seguridad/
    ├── README.md
    ├── arquitectura/
    └── controles/
├── operacion/
│   ├── README.md
│   ├── infraestructura/
│   ├── instalacion/
│   │   └── guias/                      # Guías específicas por componente
│   ├── despliegue/
│   ├── soporte/
│   └── observabilidad/
├── capacitacion/
└── guias/
```

La plantilla de [v1.0.0](plantillas/v1.0.0/README.md) contiene índices que explican el propósito de cada área. Las áreas que no apliquen al proyecto pueden omitirse; no se crean carpetas vacías sin contenido que organizar.

## 3. Definiciones y contenido esperado

### `producto/`

Describe el producto desde la perspectiva del negocio y sus usuarios: propósito, objetivos, alcance, actores, procesos, restricciones y glosario. Los requisitos verificables se mantienen en `requerimientos/` y se enlazan desde aquí cuando corresponda.

### `requerimientos/`

Organiza necesidades, análisis y evolución por etapa documental. Su `README.md` explica las etapas, los estados y la trazabilidad.

- `00-propuestas/`: propuestas de alcance, estimaciones y alternativas todavía en discusión. Su presencia no significa aprobación.
- `01-requisitos/`: comportamiento esperado, restricciones y criterios de aceptación. Puede subdividirse en `producto/`, `modulos/` y `plataforma/`.
- `02-analisis/`: diagnóstico, reglas analizadas, alternativas y diseño propuesto. Debe enlazar los requisitos relacionados.
- `03-planes-implementacion/`: actividades y verificaciones para implementar cambios aprobados o autorizados. Debe enlazar requisitos y análisis relacionados.
- `04-estado-sistema/`: capacidades y comportamiento que existen en el sistema; debe distinguir lo implementado de lo planificado.
- `90-insumos/`: documentos recibidos o recolectados que aún no han sido validados como requisitos.
- Los documentos reemplazados se conservan en una subcarpeta `historicos/` creada dentro del área o módulo correspondiente, solo cuando sea necesario.

Identificadores recomendados: `REQ` para requisitos, `ANA` para análisis, `PLAN` para planes y `EST` para estado del sistema. Los documentos relacionados deben enlazarse para conservar la trazabilidad.

### `arquitectura/`

Describe la estructura técnica del sistema y las decisiones que la sustentan.

- `vistas/`: contexto, límites, componentes, flujos, despliegue y diagramas arquitectónicos.
- `decisiones/`: registros ADR con contexto, alternativas consideradas, decisión, consecuencias y estado.
Las vistas se organizan por backend, frontend u otro componente solo cuando esa división coincida con la arquitectura del proyecto. No se crean subdivisiones vacías de antemano.

### `plantillas/`

Contiene formatos reutilizables dentro del proyecto, por ejemplo plantillas de ADR, requisitos, análisis o planes. Cada plantilla debe indicar para qué tipo de documento sirve y qué campos son obligatorios. Los formatos corporativos de esta versión se mantienen juntos en `plantillas/v1.0.0/documentos/`; un proyecto que los adopte los copia a `doc/plantillas/` y registra la versión utilizada.

### `seguridad/`

Documenta el diseño y los controles de seguridad específicos del sistema.

- `arquitectura/`: límites de confianza, flujos de datos, amenazas y decisiones de seguridad.
- `controles/`: controles aplicados, verificaciones, responsables y referencias a evidencias.
Los requisitos de seguridad se registran también en `requerimientos/01-requisitos/` y se enlazan aquí. Esta carpeta no debe contener secretos, credenciales ni datos sensibles innecesarios.

### `desarrollo/`

Reúne instrucciones y acuerdos para desarrollar y construir el sistema.

- `build/`: comandos, configuración y pasos para compilar, empaquetar o generar artefactos. No describe su publicación en ambientes.
- `convenciones/`: acuerdos de código, estructura del repositorio, ramas, commits y prácticas de trabajo del equipo.

### `operacion/`

Reúne documentación para preparar, ejecutar y mantener el sistema en sus ambientes.

- `infraestructura/`: recursos y servicios que alojan el sistema, topología, red, almacenamiento, dominios y dimensionamiento.
- `instalacion/`: prerrequisitos y procedimiento general para preparar un ambiente e instalar el sistema o sus componentes.
  - `guias/`: instrucciones específicas para instalar o configurar un componente concreto. Deben identificar el componente y enlazarse desde el índice de instalación.
- `despliegue/`: publicación de una versión o cambio en un ambiente preparado, con secuencia, autorizaciones, verificaciones y recuperación.
- `soporte/`: operación rutinaria, atención de incidentes, diagnóstico y escalamiento.
- `observabilidad/`: registros, métricas, alertas, tableros y diagnóstico operativo.

Infraestructura describe dónde funciona el sistema; instalación explica cómo dejarlo preparado inicialmente; despliegue explica cómo publicar cambios en ese sistema.

### `capacitacion/`

Contiene material de formación, ejercicios y recursos para capacitar a usuarios o equipos. Indicar público, objetivo y versión del sistema a la que aplica.

### `guias/`

Contiene guías prácticas del proyecto que no pertenecen a una categoría más específica. Cada guía debe indicar audiencia, objetivo y contexto. Las instrucciones para instalar componentes se ubican en `operacion/instalacion/guias/`. Esta versión no define subcarpetas `guias-usuario/` ni `guias-desarrollo/`.

## 4. Reglas de organización

1. Mantener un `README.md` en la raíz de `doc/` que sirva como índice y enlace los índices de las áreas utilizadas.
2. Organizar requerimientos por etapa primero y por módulo o plataforma después, cuando esa subdivisión aporte valor.
3. Separar necesidades, análisis, planes e información del estado implementado; la existencia de un documento no implica aprobación ni implementación.
4. Mantener una fuente vigente por tema y enlazarla desde otras áreas en lugar de duplicarla.
5. Usar nombres de carpetas consistentes, en minúsculas y con guiones. Mantener identificadores estables en los nombres de documentos cuando aplique.
6. No crear carpetas de históricos como parte fija de la plantilla. Cuando un área necesite conservar documentos reemplazados, puede crear dentro de ella una subcarpeta `historicos/`.
7. Crear otras subcarpetas solo cuando haya contenido que justifique la agrupación.
8. Registrar en el índice del proyecto las adaptaciones relevantes a esta estructura.

## 5. Uso y adaptación

En proyectos nuevos, copiar la estructura de la versión aprobada y completar sus índices. En proyectos existentes, evaluar cambios por su beneficio en búsqueda, trazabilidad y mantenimiento; no es necesario mover documentos únicamente para ajustarse a la plantilla. Al cambiar ubicaciones, deben actualizarse los enlaces afectados.

La versión de esta estructura identifica el paquete de carpetas e instrucciones utilizado. No reemplaza la versión, fecha, estado o aprobación propios de cada documento del proyecto.

## 6. Historial de cambios

| Versión | Fecha | Cambio |
|---|---|---|
| 1.0.0 | 2026-11-14 | Primera versión propuesta: áreas de producto, requerimientos, arquitectura, desarrollo, plantillas, seguridad, operación, capacitación y guías. |
