# Identity and Access Management (IAM)

## Concepto

**Identity and Access Management (IAM)** comprende políticas, procesos y sistemas para administrar identidades digitales y controlar su acceso a aplicaciones, datos, infraestructura y servicios. Su objetivo es que cada persona, aplicación o dispositivo tenga una identidad verificable y reciba únicamente los permisos necesarios durante el tiempo requerido.

IAM es una disciplina y una arquitectura, no un producto único. Puede incluir capacidades de directorio, autenticación, autorización, aprovisionamiento, gobierno, acceso privilegiado y auditoría. Las organizaciones pueden implementarlas con una o varias soluciones integradas.

## Componentes de IAM

### 1. Directorio y almacén de identidades

Mantiene identidades, atributos, grupos, relaciones organizativas y estado de las cuentas. Puede recibir datos de fuentes autoritativas —por ejemplo, recursos humanos o gestión de contratistas— y exponerlos a sistemas que necesitan consultar identidades.

**Características:** esquema de atributos; grupos y jerarquías; búsqueda y consulta; integración con directorios y fuentes de datos; replicación y disponibilidad; controles de privacidad y retención.

**Casos de uso:** centralizar cuentas laborales; buscar usuarios y grupos; mantener atributos para decisiones de acceso; sincronizar directorios entre dominios o sistemas.

### 2. Gobierno del ciclo de vida de identidades (IGA)

Define cómo se crean, modifican, revisan y eliminan identidades y sus cuentas desde el ingreso de una persona hasta su salida o cambio de función. Incluye gobierno, evidencia y revisión de los accesos.

**Características:** procesos Joiner-Mover-Leaver; aprovisionamiento y desaprovisionamiento; conciliación de cuentas; revisión periódica de accesos; certificaciones; segregación de funciones; flujos de aprobación; registro de decisiones.

**Casos de uso:** crear cuentas al ingresar una persona; actualizar accesos al cambiar de cargo; retirar permisos al terminar una relación laboral; ejecutar recertificaciones y preparar evidencia para auditorías.

### 3. Autenticación y proveedor de identidad (IdP)

Verifica una identidad durante el acceso. Un proveedor de identidad puede autenticar al usuario y emitir una afirmación o token para una aplicación integrada.

**Características:** inicio de sesión; autenticación multifactor; políticas adaptativas o basadas en contexto; gestión de sesiones; recuperación de cuenta; federación; inicio de sesión único (SSO); integración con estándares como OpenID Connect, OAuth 2.0 y SAML 2.0.

**Casos de uso:** unificar el inicio de sesión en varias aplicaciones; exigir MFA para accesos sensibles; permitir que usuarios de organizaciones asociadas ingresen a servicios autorizados; autenticar aplicaciones y servicios.

### 4. Autorización y control de acceso

Determina qué puede hacer una identidad sobre un recurso y bajo qué condiciones. La autenticación responde quién es la identidad; la autorización decide qué acciones se le permiten.

**Características:** permisos; roles; políticas; atributos y contexto; evaluación de acciones y recursos; delegación; separación de funciones; aplicación coherente de políticas.

**Modelos habituales:** RBAC (por roles), ABAC (por atributos), ACL (listas de control de acceso) y políticas basadas en relaciones. La elección depende de la granularidad, el contexto y la facilidad de administración.

**Casos de uso:** restringir datos por equipo o función; permitir una acción solo desde dispositivos o ubicaciones aprobadas; conceder acceso temporal; aplicar mínimo privilegio en aplicaciones e infraestructura.

### 5. Aprovisionamiento y conectores

Transfiere cambios de identidad, cuentas, grupos y derechos entre el sistema central y aplicaciones o plataformas conectadas.

**Características:** conectores por protocolo o API; creación y desactivación de cuentas; asignación de grupos y roles; sincronización de atributos; reconciliación de cambios; manejo de errores y reintentos; registro de operaciones.

**Casos de uso:** propagar altas y bajas hacia aplicaciones; sincronizar grupos; reducir tareas manuales de mesa de ayuda; identificar cuentas huérfanas o discrepancias entre sistemas.

### 6. Federación de identidades

Permite que una organización confíe en identidades autenticadas por otro dominio o proveedor, con acuerdos sobre confianza, atributos y protocolos.

**Características:** relaciones de confianza; intercambio de afirmaciones o tokens; mapeo de atributos; reglas de vinculación de cuentas; expiración y renovación de confianza; soporte para protocolos de federación.

**Casos de uso:** acceso de empleados a aplicaciones externas; colaboración B2B; portales para clientes o socios; consolidación de varias fuentes de autenticación.

### 7. Gestión de acceso privilegiado (PAM)

Protege cuentas con permisos elevados y sesiones administrativas. PAM puede complementar IAM general con controles específicos para credenciales y actividades privilegiadas.

**Características:** bóveda y rotación de credenciales; elevación temporal; aprobación just-in-time; grabación o supervisión de sesiones; control de comandos; separación entre cuentas ordinarias y administrativas.

**Casos de uso:** administrar servidores y bases de datos; otorgar privilegios por tiempo limitado; proteger cuentas de emergencia; investigar actividades administrativas.

### 8. Identidades no humanas

Gestiona identidades de aplicaciones, servicios, cargas de trabajo, dispositivos y automatizaciones, que necesitan autenticarse y acceder a recursos sin una persona presente.

**Características:** credenciales de corta duración; certificados o identidades de carga de trabajo; rotación; permisos mínimos; asociación de identidad con servicio; inventario y monitoreo del uso.

**Casos de uso:** permitir que un servicio lea un almacenamiento; autenticar procesos de CI/CD; autorizar comunicación entre servicios; evitar claves estáticas compartidas.

### 9. Auditoría, monitoreo y analítica

Registra y analiza eventos de identidad, autenticación, cambios de permisos y decisiones de acceso.

**Características:** logs con actor, acción, recurso y resultado; correlación temporal; alertas; reportes; retención; exportación a SIEM; detección de anomalías y soporte para investigación.

**Casos de uso:** investigar accesos sospechosos; demostrar quién aprobó o cambió un permiso; detectar fallos de inicio de sesión; sustentar revisiones de cumplimiento.

## Características transversales

- **Mínimo privilegio:** entregar solo los permisos necesarios para cada función.
- **Separación de funciones:** evitar que una sola identidad controle actividades incompatibles o sensibles sin revisión.
- **Ciclo de vida automatizado:** vincular los cambios de acceso a eventos organizativos y técnicos.
- **Interoperabilidad:** usar protocolos, APIs y formatos documentados cuando sea posible.
- **Resiliencia:** planificar disponibilidad del inicio de sesión, respaldos, recuperación y acceso de emergencia.
- **Privacidad y cumplimiento:** limitar atributos recopilados, proteger información personal y aplicar retención adecuada.
- **Trazabilidad:** registrar decisiones y cambios con suficiente contexto para auditoría.

## Recomendaciones de implementación

1. Inventariar identidades humanas y no humanas, aplicaciones, recursos y fuentes de datos.
2. Definir fuentes autoritativas y responsables del dato de identidad.
3. Priorizar MFA, SSO y permisos mínimos en los sistemas críticos.
4. Diseñar el ciclo Joiner-Mover-Leaver y automatizar primero las altas, cambios y bajas de mayor impacto.
5. Establecer roles comprensibles y revisar su acumulación; recurrir a políticas contextuales cuando los roles no sean suficientes.
6. Asignar propietarios a aplicaciones y permisos, con revisiones periódicas basadas en riesgo.
7. Separar y proteger cuentas privilegiadas y credenciales de servicios.
8. Probar recuperación, fallos del proveedor de identidad y procedimientos de acceso de emergencia.
9. Centralizar registros y medir cobertura de MFA, cuentas huérfanas, permisos privilegiados y bajas pendientes.
10. Adoptar estándares y documentar integraciones, dependencias y límites de confianza.

## Productos IAM on-premise

Las siguientes fichas describen opciones autogestionadas o desplegables en infraestructura propia. Sus alcances difieren: algunas se centran en SSO e identidad de aplicaciones; otras en gobierno del ciclo de vida o identidad de sistemas Linux.

- [Microsoft Active Directory Domain Services (AD DS)](Microsoft-Active-Directory.md): directorio, autenticación de dominio y administración de recursos Windows on-premise.
- [Keycloak](Keycloak.md): SSO, federación y autenticación para aplicaciones.
- [WSO2 Identity Server](WSO2-Identity-Server.md): IAM extensible para autenticación, federación y CIAM.
- [authentik](authentik.md): proveedor de identidad autohospedado para aplicaciones y acceso integrado.
- [midPoint](midPoint.md): gobierno y administración de identidades, aprovisionamiento y cumplimiento.
- [FreeIPA](FreeIPA.md): identidad, autenticación y políticas centralizadas para entornos Linux/Unix.

## Fuentes de referencia

- [NIST Digital Identity Guidelines](https://pages.nist.gov/800-63-4/)
- [OpenID Connect](https://openid.net/developers/how-connect-works/)
- [OAuth 2.0 Security Best Current Practice](https://www.rfc-editor.org/rfc/rfc9700.html)
- [SAML Technical Overview](https://www.oasis-open.org/standard/saml/)
