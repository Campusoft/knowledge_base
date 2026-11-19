# Microsoft Agent 365

Investigación documental. Fecha de consulta: **18 de noviembre de 2026**.

## Qué es y para qué sirve

Microsoft Agent 365 proporciona una capa central de administración, observabilidad, gobierno y seguridad para los agentes de IA de una organización. Integra agentes propios, de Microsoft y de terceros con los servicios empresariales de Microsoft 365. La documentación actual organiza el producto en cuatro pilares: administración, gobierno, seguridad y valor de negocio.

Permite conocer qué agentes existen, quién responde por ellos, cómo se utilizan y qué señales de riesgo presentan. La visibilidad y las acciones disponibles dependen de la integración de cada agente. [Descripción general oficial](https://learn.microsoft.com/en-us/microsoft-agent-365/overview).

## Estado y disponibilidad

El producto alcanzó disponibilidad general para clientes comerciales el **1 de mayo de 2026**. Esto no convierte todas sus funciones e integraciones en funcionalidades de disponibilidad general: Microsoft mantiene capacidades adicionales en preview y en el programa Frontier. [Anuncio de disponibilidad general](https://www.microsoft.com/en-us/security/blog/2026/05/01/microsoft-agent-365-now-generally-available-expands-capabilities-and-integrations/).

Existe como suscripción independiente para suscripciones Microsoft 365 elegibles y está incluido en Microsoft 365 E7. La disponibilidad por nube y característica varía; la matriz oficial distingue, por ejemplo, funciones disponibles en GCC de otras que no lo están. [Descripción del servicio](https://learn.microsoft.com/en-us/office365/servicedescriptions/microsoft-agent-365/microsoft-agent-365).

## Características

| Área | Qué aporta | Dónde se administra |
|---|---|---|
| Inventario | Registro de agentes, metadatos y responsables. | Centro de administración de Microsoft 365. |
| Gobierno | Aprobación, publicación, asignación de acceso y bloqueo de agentes; plantillas para estandarizar controles. | Microsoft 365 y servicios asociados. |
| Identidad | Identidades de agentes y controles sobre su acceso a recursos. | Microsoft Entra. |
| Protección de información | Políticas para datos sensibles, auditoría y cumplimiento. | Microsoft Purview. |
| Detección y respuesta | Investigación de amenazas y protección de escenarios compatibles. | Microsoft Defender. |
| Dispositivos | Controles para agentes locales en escenarios admitidos. | Microsoft Intune. |

Las capacidades básicas de inventario y ciertas acciones administrativas ya están incluidas en planes Microsoft 365. Las funciones avanzadas, como observabilidad, plantillas y controles de herramientas, requieren Agent 365 o el plan correspondiente. Por ello, ver el registro de agentes en el portal no demuestra que se disponga de todas las funciones premium. [Matriz de capacidades y planes](https://learn.microsoft.com/en-us/office365/servicedescriptions/microsoft-agent-365/microsoft-agent-365).

## Arquitectura

La siguiente tabla separa los productos que suelen confundirse:

| Producto | Función en una solución |
|---|---|
| Microsoft Agent 365 | Administración y control empresarial de agentes. |
| Microsoft Agent 365 SDK | Añade identidad, herramientas gobernadas y notificaciones a un agente existente. |
| Microsoft 365 Agents SDK | Permite construir agentes conversacionales y gestionar canales y estado de conversación. |
| Framework o runtime del agente | Ejecuta la lógica, la planificación y las llamadas a herramientas. |
| Modelo y alojamiento | Proporcionan inferencia e infraestructura; los selecciona y opera el equipo del agente. |

Microsoft Agent 365 SDK no aloja ni ejecuta el agente. Puede acompañar a un runtime existente, incluso fuera de Azure. Microsoft 365 Agents SDK permite desarrollar en .NET, JavaScript y Python y publicar en canales como Teams, Microsoft 365 Copilot y web. [Comparación oficial de SDK](https://learn.microsoft.com/en-us/microsoft-agent-365/developer/agent-365-sdk), [repositorio Microsoft 365 Agents SDK](https://github.com/microsoft/Agents).

Interpretación arquitectónica: Agent 365 funciona como plano de control. El modelo, el runtime y los sistemas donde se ejecutan las acciones constituyen la parte operativa de la solución. Registrar el agente no traslada automáticamente su ejecución a Microsoft 365.

## Cómo integrar agentes

Hay tres caminos principales:

1. **Integración nativa:** usar la integración que ofrece una plataforma compatible.
2. **Plataformas conectadas:** importar agentes externos al inventario mediante sincronización del registro.
3. **Integración en el código:** añadir Agent 365 SDK cuando se requieren identidad, herramientas o notificaciones desde el propio agente.

La elección depende de quién construye y opera el agente y de las capacidades necesarias. Sincronizar inventario e instrumentar código resuelven necesidades diferentes. [Guía para elegir una integración](https://learn.microsoft.com/en-us/microsoft-agent-365/developer/choose-integration-option).

Para nuevas integraciones de telemetría, Microsoft indica utilizar **Microsoft OpenTelemetry Distro**. Los paquetes antiguos de observabilidad de Agent 365 están deprecados. Las notificaciones y las cuentas de usuario propias para agentes continúan sujetas a Frontier según la documentación consultada. [SDK y observabilidad](https://learn.microsoft.com/en-us/microsoft-agent-365/developer/agent-365-sdk).

### Plataformas externas y límites

La documentación enumera Amazon Bedrock, Google Vertex AI, Salesforce Agentforce, Databricks Genie, Anthropic Claude Managed Agents, Oracle Generative AI Agents y Snowflake Cortex. Oracle y Snowflake todavía figuran con observabilidad pendiente; otras funciones de observabilidad están sujetas a Frontier.

Limitaciones relevantes al corte de la investigación:

- La sincronización del inventario es manual, sin programación automática.
- Una baja en la plataforma original no garantiza la eliminación del registro sincronizado.
- Las conexiones usan credenciales basadas en claves; no admiten autenticación sin claves en este mecanismo.
- El diseño es de lectura; existe una excepción opcional para eliminación explícita por un administrador con permiso separado.
- Sincronizar el registro no añade capacidades al código ni permite ejecutar los agentes externos.

[Plataformas conectadas: compatibilidad, seguridad y limitaciones](https://learn.microsoft.com/en-us/microsoft-agent-365/admin/connected-platforms).

## Seguridad

### Identidad y políticas

Las plantillas agrupan controles de Entra, Purview, SharePoint y Defender. Algunas políticas necesitan configuración adicional en sus servicios respectivos. Una política de Entra puede estar asignada y aun así no aplicarse durante la ejecución si el agente no se autentica con su identidad de Entra.

Las actualizaciones de una plantilla afectan nuevas activaciones y no modifican automáticamente agentes ya aprobados. Este detalle exige revisar la cobertura de los agentes existentes. [Plantillas de políticas y limitaciones](https://learn.microsoft.com/en-us/microsoft-agent-365/admin/policy-template).

### Datos sensibles y cumplimiento

Purview aporta auditoría, clasificación, DLP, eDiscovery, retención y análisis de riesgos en interacciones compatibles. Varias funciones requieren incluir explícitamente la instancia del agente en las políticas.

Una limitación concreta: el contenido nuevo generado por Agent 365 no hereda automáticamente las etiquetas de sensibilidad de los documentos fuente. Los archivos cifrados también requieren derechos explícitos adecuados para que el agente acceda a ellos. La afirmación general de que se respetan etiquetas no debe interpretarse como etiquetado automático de toda salida. [Protección de datos con Purview](https://learn.microsoft.com/en-us/purview/ai-agent-365).

### Detección y respuesta

Defender integra descubrimiento, evaluación de postura e investigación. La incorporación de agentes locales es distinta de la de agentes cloud. Para investigar actividad se deben configurar las fuentes de datos; por ejemplo, la documentación exige el conector Microsoft 365 para que aparezcan determinadas alertas e incidentes de Copilot Studio. [Configuración de Defender para agentes](https://learn.microsoft.com/en-us/defender-xdr/security-for-ai/get-started-defender-security-for-ai).

## Precios y licenciamiento (*)

**(*) Fecha de consulta de precios: 18 de noviembre de 2026.** Moneda: USD. Precios de lista publicados por Microsoft:

| Modalidad | Precio publicado |
|---|---|
| Agent 365 independiente | USD 15 por usuario al mes. |
| Microsoft 365 E7, que incluye Agent 365 | USD 99 por usuario al mes por el paquete completo. |

La licencia se asigna a personas, no a cada agente. Las FAQ diferencian usuarios que delegan su acceso y responsables de agentes con acceso propio. El uso de capacidades premium determina la necesidad de licencia; habilitar el servicio con una licencia en el tenant no cubre automáticamente a toda la organización.

Microsoft indica que Agent 365 no tiene cargos propios por consumo en su modelo actual. Esto no acredita que sean gratuitos el alojamiento, los modelos o los servicios que utiliza el agente. Los precios locales, impuestos y condiciones contractuales requieren cotización. [FAQ oficial de licenciamiento](https://www.microsoft.com/licensing/faqs/122).

### Dependencias adicionales

La guía técnica especifica que Work IQ MCP está en preview y requiere Microsoft 365 Copilot; las notificaciones requieren una cuenta propia del agente y Frontier. La integración opcional de Purview DLP para IA tiene requisitos adicionales de configuración, licencias y facturación por uso.

Para observabilidad debe haber al menos un usuario con Agent 365 o E7 asignado. Microsoft advierte que, sin esa asignación, la telemetría puede descartarse aun cuando la solicitud devuelva HTTP 200. [Requisitos de incorporación](https://learn.microsoft.com/en-us/microsoft-agent-365/developer/get-started).

### Inconsistencia documental encontrada

La FAQ y la descripción del servicio no clasifican idénticamente todas las capacidades básicas y premium. Por ejemplo, difieren sobre sincronización del registro y automatización del ciclo de vida; la FAQ también menciona auditoría en ambos grupos. Este informe no resuelve esa discrepancia por inferencia: para compras se deben contrastar la matriz vigente, los Product Terms y la confirmación del proveedor. [FAQ](https://www.microsoft.com/licensing/faqs/122), [descripción del servicio](https://learn.microsoft.com/en-us/office365/servicedescriptions/microsoft-agent-365/microsoft-agent-365).

## Limitaciones

- La cobertura depende del tipo de agente, la plataforma y el nivel de integración.
- Registrar un agente no añade instrumentación ni control de ejecución automáticamente.
- Las plataformas conectadas requieren sincronización manual y no garantizan reconciliar las bajas del origen.
- Algunas capacidades continúan en preview o requieren Frontier y licencias adicionales.
- Las plantillas actualizadas no modifican automáticamente agentes ya aprobados.
- Se encontraron discrepancias entre la FAQ de licenciamiento y la matriz de características; deben confirmarse antes de contratar.

Estas condiciones se desarrollan y documentan con fuentes en los apartados de integración, seguridad y precios de este documento.

## Casos de uso

Los siguientes escenarios son propuestas de evaluación, no implementaciones verificadas en la empresa:

| Escenario | Qué evaluar |
|---|---|
| Agente de soporte interno | Propietario, acceso a procedimientos y trazabilidad de consultas. |
| Agente para gestión documental | Acceso mínimo a SharePoint, tratamiento de documentos sensibles y revisión de salidas. |
| Agentes de desarrollo | Cobertura real de agentes locales y señales disponibles en dispositivos administrados. |
| Agentes de varias plataformas | Calidad del inventario y mantenimiento de altas y bajas externas. |

Como criterio de adopción, resulta más relevante cuando existe una flota de agentes y se necesita responsabilidad, acceso controlado y evidencia de actividad. Para un experimento aislado, conviene comparar el costo y esfuerzo de integración con los controles que ya ofrece la plataforma utilizada.

## Recomendaciones

1. Seleccionar un agente y un proceso interno con datos de prueba.
2. Identificar propietario, usuarios, plataforma y tipo de autenticación.
3. Confirmar licencias y disponibilidad de cada función que se pretende medir.
4. Elegir integración nativa, sincronización o SDK.
5. Comprobar inventario y asignación del responsable.
6. Limitar permisos y probar una operación permitida y otra prohibida.
7. Validar telemetría, detección y acceso de los administradores a la evidencia.
8. Medir costos totales y esfuerzo operativo; decidir si se amplía el alcance.

Criterios de aceptación propuestos: agente identificado, responsable asignado, permisos comprobados, evidencia recuperable de una interacción y procedimiento probado para detener o revocar su acceso donde la integración lo permita.

## Alcance de la investigación

Investigación basada en fuentes públicas oficiales de Microsoft; no se realizó un despliegue ni una prueba en un tenant. Se consultó Context7 para contrastar Microsoft 365 Agents SDK. Al no devolver una colección específica de Microsoft Agent 365, se verificó este producto directamente en Microsoft Learn y en documentación comercial oficial.

Las características preview, los precios y las matrices de compatibilidad deben revisarse antes de una implementación. La disponibilidad general del producto no implica cobertura uniforme de todos los agentes.

## Documentación relacionada

- [Catálogo de agentes de IA de esta base](../../../agentes.ai.md).
- [Microsoft Graph](../../../../microsoft/graph/microsoft_graph.md).
- [Microsoft Teams](../../../../microsoft/teams/microsoft_teams.md).
- [SharePoint](../../../../microsoft/sharepoint/index.md).

- [Índice de Agent Control Planes](README.md).
