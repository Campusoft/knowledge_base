# OpenSpec

OpenSpec es un marco de desarrollo guiado por especificaciones que ayuda a equipos y asistentes de programación con IA a acordar el alcance y el comportamiento de un cambio antes de implementarlo. También proporciona una CLI y flujos de trabajo que pueden integrarse con distintos asistentes; no es un modelo de IA ni reemplaza el criterio del equipo.

Su idea central es mantener separadas las especificaciones que describen el comportamiento vigente del sistema y las propuestas de cambio que todavía están en revisión o implementación. Así se puede discutir, revisar e implementar cada cambio como una unidad rastreable.

## ¿Para qué sirve?

- Aclarar qué problema resuelve un cambio y qué queda fuera de su alcance.
- Expresar el comportamiento esperado de forma revisable antes de programar.
- Dar a desarrolladores y asistentes de IA una referencia compartida.
- Mantener un registro del diseño, las tareas y los requisitos modificados.
- Reducir interpretaciones distintas entre la solicitud, la implementación y la revisión.

OpenSpec resulta especialmente útil en nuevas funcionalidades, cambios de comportamiento, integraciones y tareas donde participan asistentes de IA. Para correcciones triviales o cambios puramente mecánicos puede ser suficiente un proceso más ligero.

## Características

- **Spec-driven:** el trabajo parte de requisitos y escenarios observables.
- **Orientado a cambios:** cada iniciativa se documenta en su propia carpeta hasta que se completa.
- **Revisable:** la propuesta y los artefactos pueden discutirse antes de modificar código.
- **Compatible con asistentes:** los artefactos ofrecen contexto persistente a personas y agentes de programación.
- **Evolutivo:** al completar un cambio, sus requisitos se integran a las especificaciones vigentes.
- **Adaptable:** el flujo y los esquemas pueden ajustarse al tamaño y las prácticas del proyecto.

## Organización básica

La estructura habitual tiene dos zonas principales:

```text
openspec/
├── specs/                    # Comportamiento vigente del sistema
│   └── autenticacion/
│       └── spec.md
└── changes/                  # Cambios en propuesta o ejecución
    └── restablecer-contrasena/
        ├── proposal.md
        ├── design.md
        ├── tasks.md
        └── specs/
            └── autenticacion/
                └── spec.md   # Requisitos delta de este cambio
```

`specs/` es la referencia del comportamiento acordado actualmente. `changes/` contiene propuestas temporales; al archivarlas, los requisitos delta se incorporan a `specs/` y se conserva la carpeta del cambio como historial.

## Metodología

El flujo spec-driven predeterminado consta de estas etapas:

1. **Explorar el problema (opcional):** entender usuarios, contexto, restricciones y posibles soluciones. Esta etapa sirve para pensar y no requiere escribir código.
2. **Preparar la propuesta (`proposal.md`):** resumir por qué se necesita el cambio, qué se propone, sus capacidades principales y qué queda explícitamente fuera del alcance.
3. **Definir las especificaciones delta (`specs/.../spec.md`):** describir qué comportamiento se agrega, modifica, elimina o renombra. Incluir escenarios verificables.
4. **Diseñar la solución (`design.md`):** explicar cómo se implementará, las decisiones técnicas relevantes y los riesgos o alternativas que afecten el trabajo.
5. **Desglosar tareas (`tasks.md`):** convertir los requisitos y el diseño en una lista concreta y comprobable de trabajo.
6. **Implementar y comprobar:** ejecutar las tareas, contrastar el comportamiento con los escenarios y actualizar el estado de las tareas.
7. **Archivar el cambio:** cuando esté terminado, integrar sus requisitos delta en las especificaciones vigentes y conservar los artefactos como historial.

La propuesta precede a las especificaciones y al diseño; las tareas se preparan con ambos como base. En la práctica, algunas actividades de planificación pueden desarrollarse en paralelo si el esquema configurado lo permite, pero deben quedar alineadas antes de implementar.

## Artefactos principales

| Artefacto | Ubicación habitual | Propósito |
|---|---|---|
| **Propuesta** | `proposal.md` | Explica la necesidad, el objetivo y el alcance del cambio. |
| **Especificación delta** | `specs/<capacidad>/spec.md` dentro del cambio | Registra el comportamiento que se añade, modifica, elimina o renombra. |
| **Diseño** | `design.md` | Describe el enfoque técnico y las decisiones de implementación. |
| **Tareas** | `tasks.md` | Lista el trabajo ejecutable y permite seguir su avance. |
| **Especificación vigente** | `openspec/specs/<capacidad>/spec.md` | Describe el comportamiento acordado actual de una capacidad del sistema. |

Las especificaciones delta usan secciones como `ADDED`, `MODIFIED`, `REMOVED` y `RENAMED`. Los requisitos suelen redactarse como comportamientos obligatorios y acompañarse de escenarios con condiciones y resultados esperados (por ejemplo, `WHEN` / `THEN`). Los nombres exactos de carpetas y artefactos dependen del esquema y la versión instalados.

## Recomendaciones

- Escribir requisitos desde el comportamiento visible para el usuario o para otro sistema, no desde detalles de código innecesarios.
- Mantener cada cambio enfocado; separar iniciativas independientes en propuestas distintas.
- Hacer que cada escenario tenga una condición y un resultado que se puedan comprobar.
- Aclarar límites, permisos, errores y casos alternativos importantes antes de implementar.
- Registrar decisiones técnicas relevantes en el diseño, sin convertirlo en una copia de la propuesta.
- Desglosar tareas pequeñas con un resultado verificable y vincularlas a requisitos cuando aporte claridad.
- Pedir a la IA que señale ambigüedades y supuestos; revisar sus documentos y código como equipo responsable.
- Mantener las especificaciones vigentes actualizadas después de completar el cambio.
- Evitar duplicar documentación de arquitectura que ya tiene una fuente oficial en el proyecto; enlazarla cuando corresponda.
- Aplicar el flujo de forma proporcional: usar los artefactos que aporten valor y evitar burocracia para cambios mínimos.

## Ejemplo: restablecimiento de contraseña

Supongamos que una aplicación permite a sus usuarios recuperar el acceso mediante un enlace enviado por correo electrónico.

### 1. Propuesta (`proposal.md`)

```markdown
# Restablecimiento de contraseña

## Por qué
Los usuarios que olvidan su contraseña no pueden recuperar el acceso sin pedir ayuda al equipo de soporte.

## Qué cambia
- Permitir solicitar un enlace de restablecimiento desde el inicio de sesión.
- Enviar un enlace de un solo uso al correo asociado a la cuenta.
- Permitir establecer una contraseña nueva desde un enlace válido.

## Fuera de alcance
- Cambiar el correo electrónico de la cuenta.
- Recuperar cuentas cuyo correo ya no está disponible.
```

### 2. Especificación delta

Archivo propuesto: `changes/restablecer-contrasena/specs/autenticacion/spec.md`.

```markdown
## ADDED Requirements

### Requirement: Solicitar restablecimiento de contraseña
El sistema SHALL permitir solicitar un enlace de restablecimiento para una cuenta registrada.

#### Scenario: Solicitud para una cuenta registrada
- **WHEN** una persona envía el formulario con el correo de una cuenta registrada
- **THEN** el sistema envía un enlace de restablecimiento a ese correo

### Requirement: Usar enlace de restablecimiento
El sistema SHALL permitir cambiar la contraseña con un enlace válido y no utilizado.

#### Scenario: Enlace válido
- **WHEN** la persona envía una contraseña nueva mediante un enlace vigente
- **THEN** el sistema actualiza la contraseña e invalida el enlace
```

En un proyecto real también se definirían escenarios para enlaces vencidos o ya utilizados, datos inválidos y respuestas que eviten revelar si un correo está registrado.

### 3. Diseño (`design.md`)

El diseño podría decidir que el enlace incluya un token aleatorio de un solo uso, que el servidor almacene solo su representación protegida, que tenga vencimiento y que la respuesta de solicitud sea genérica. También documentaría cómo se valida el token, cómo se invalida después del uso y qué componentes existentes se reutilizan. Estas son decisiones de ejemplo: deben confirmarse según la arquitectura y los controles de seguridad del proyecto.

### 4. Tareas (`tasks.md`)

```markdown
- [ ] Añadir la operación para solicitar el restablecimiento.
- [ ] Generar y almacenar un token protegido con fecha de expiración.
- [ ] Enviar el enlace al correo asociado a la cuenta.
- [ ] Añadir la pantalla y operación para establecer una contraseña nueva.
- [ ] Invalidar el token después del uso y rechazar tokens vencidos.
- [ ] Comprobar los escenarios de la especificación y los casos de error.
```

### 5. Implementación y archivo

Se implementan y revisan las tareas comparando el resultado con los requisitos y escenarios. Al concluir, se archiva la propuesta y se integra el requisito de restablecimiento a la especificación vigente de autenticación. Así, la especificación principal describe el comportamiento actual y el cambio queda preservado como historial.

## Referencias

- [OpenSpec: conceptos y organización](https://github.com/Fission-AI/OpenSpec/blob/main/docs/concepts.md)
- [OpenSpec: esquema spec-driven y artefactos](https://github.com/Fission-AI/OpenSpec/blob/main/docs/schemas/spec-driven/index.md)
- [OpenSpec: guía rápida](https://github.com/Fission-AI/OpenSpec/blob/main/docs/start/quickstart.md)
