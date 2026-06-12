# OpenFang

## Descripcion

**OpenFang** es un proyecto open source que se presenta como un **Agent Operating System** construido en Rust. Su enfoque no es ser solo un framework conversacional, sino una plataforma para ejecutar agentes autonomos que trabajan en segundo plano, con horarios, herramientas, memoria, canales de comunicacion, dashboard y controles de seguridad.

El repositorio oficial lo describe como un sistema compilado en un unico binario, con arquitectura modular en crates de Rust, API compatible con OpenAI, soporte para MCP/A2A, agentes preconstruidos y paquetes autonomos llamados **Hands**.

## Proposito

OpenFang busca resolver un problema comun en agentes de IA: pasar de un chatbot que responde cuando el usuario escribe, a agentes que pueden operar continuamente, ejecutar tareas programadas, mantener memoria, conectarse con canales externos y reportar resultados.

Es especialmente relevante para escenarios donde el agente debe:

- trabajar de forma autonoma;
- usar herramientas externas;
- integrarse con canales como Telegram, Slack, WhatsApp, Discord o email;
- mantener memoria persistente;
- ejecutar flujos repetibles;
- aplicar permisos, auditoria y limites de seguridad.

## Arquitectura

La arquitectura publicada del proyecto esta organizada como un workspace de Rust con varios crates especializados. Conceptualmente puede entenderse asi:

### 1. Kernel

`openfang-kernel` concentra la orquestacion del sistema: workflows, medicion de uso, RBAC, scheduler y seguimiento de presupuesto. Es la capa que coordina la ejecucion de agentes y tareas.

### 2. Runtime

`openfang-runtime` ejecuta el loop del agente, integra drivers de LLM, herramientas, sandbox WASM, MCP y A2A. Esta capa convierte decisiones del modelo en acciones controladas.

### 3. API y Dashboard

`openfang-api` expone endpoints REST, WebSocket y SSE. Tambien incluye una API compatible con OpenAI y sirve como punto de integracion para dashboard, agentes, memoria, workflows, canales, modelos y skills.

### 4. Canales

`openfang-channels` conecta agentes con plataformas de comunicacion. El proyecto declara adaptadores para canales como Telegram, Discord, Slack, WhatsApp, Signal, Matrix, email, Microsoft Teams, Google Chat, Reddit, LinkedIn, Twitch, IRC, Webhooks y otros.

### 5. Memoria

`openfang-memory` usa persistencia en SQLite, embeddings vectoriales, sesiones canonicas y compactacion. Su objetivo es permitir que los agentes recuerden contexto entre conversaciones, canales y ejecuciones.

### 6. Tipos y Seguridad

`openfang-types` define tipos base, seguimiento de taint, firma Ed25519 de manifiestos y catalogo de modelos. Estas piezas ayudan a controlar identidad, capacidades y flujo de informacion sensible.

### 7. Skills y Hands

`openfang-skills` gestiona skills, parsing de `SKILL.md` y marketplace. `openfang-hands` gestiona los **Hands**, que son paquetes autonomos con manifiesto `HAND.toml`, prompt operativo, skill de dominio, configuracion, metricas y guardrails.

### 8. Extensiones y Protocolos

`openfang-extensions` incluye plantillas MCP, vault de credenciales y OAuth2 PKCE. `openfang-wire` implementa el protocolo P2P OFP con autenticacion mutua HMAC-SHA256.

### 9. CLI y Desktop

`openfang-cli` permite iniciar daemon, administrar agentes, usar dashboard TUI y operar como servidor MCP. `openfang-desktop` usa Tauri 2.0 para una aplicacion nativa con bandeja del sistema, notificaciones y accesos globales.

## Caracteristicas Principales

- **Un binario**: instalacion y ejecucion simplificada mediante un binario unico.
- **Rust**: implementacion orientada a rendimiento, seguridad de memoria y baja huella operacional.
- **Hands autonomos**: paquetes preconstruidos que ejecutan tareas por horario o de forma continua.
- **Agentes preconstruidos**: agentes listos para tareas como investigacion, coding, soporte u orquestacion.
- **Herramientas integradas**: herramientas nativas, soporte MCP y automatizacion de navegador.
- **Memoria persistente**: SQLite, embeddings, sesiones y compactacion de contexto.
- **Canales multiples**: integracion con mensajeria, redes sociales, email y webhooks.
- **API compatible con OpenAI**: permite apuntar clientes existentes a OpenFang con endpoints tipo `/v1/chat/completions`.
- **Seguridad en capas**: sandbox WASM, auditoria con hash-chain, taint tracking, RBAC, scanner de prompt injection, proteccion SSRF, zeroization de secretos y rate limiting.
- **Desktop app**: interfaz nativa con Tauri para operar agentes desde una aplicacion local.
- **Migracion desde OpenClaw**: incluye comando de migracion para agentes, memoria, skills y configuracion.

## Hands Incluidos

OpenFang agrupa capacidades autonomas en paquetes llamados **Hands**. Segun el README oficial, los principales son:

- **Clip**: procesa videos, detecta momentos relevantes, genera shorts verticales, subtitulos y miniaturas.
- **Lead**: descubre prospectos, los enriquece con investigacion web, puntua oportunidades y entrega leads en CSV, JSON o Markdown.
- **Collector**: monitorea objetivos, detecta cambios, hace seguimiento de sentimiento y construye grafos de conocimiento.
- **Predictor**: recolecta senales, genera predicciones con intervalos de confianza y mide precision con Brier scores.
- **Researcher**: realiza investigacion profunda, cruza fuentes, evalua credibilidad y genera reportes citados.
- **Twitter**: gestiona contenido para X/Twitter con cola de aprobacion antes de publicar.
- **Browser**: automatiza navegacion web con Playwright, formularios y flujos multi-paso, con aprobacion obligatoria para compras.

## Casos de Uso

### Investigacion Autonoma

OpenFang puede ejecutar agentes de investigacion que recopilan fuentes, contrastan informacion, mantienen contexto y generan reportes. Es util para vigilancia tecnologica, analisis de mercado, research competitivo y seguimiento de temas.

### Generacion de Leads

El Hand de leads esta orientado a descubrir prospectos, enriquecer informacion, puntuar calidad y exportar resultados. Puede servir para equipos comerciales que necesitan pipelines automatizados con revision humana.

### Monitoreo y OSINT

Collector puede usarse para seguir empresas, personas, temas o eventos. La combinacion de monitoreo, alertas y grafos de conocimiento lo hace aplicable a inteligencia competitiva y vigilancia de cambios.

### Automatizacion Web

Browser permite automatizar sitios, formularios y tareas de navegador. Es util para flujos repetitivos donde una API no esta disponible, aunque requiere controles estrictos por riesgo operativo.

### Gestion Multicanal

Los adaptadores de canales permiten conectar agentes a mensajeria, comunidades y herramientas de trabajo. Esto permite exponer un mismo agente en Telegram, Slack, WhatsApp, Discord, email u otros canales.

### Asistentes Persistentes

La memoria persistente y las sesiones canonicas permiten construir asistentes que recuerdan conversaciones, preferencias y contexto entre canales.

### Plataforma Self-Hosted para Agentes

OpenFang puede ser evaluado como base self-hosted para organizaciones que quieren controlar infraestructura, modelos, credenciales, canales, herramientas y politicas.

## Limitaciones y Riesgos

- **Pre-1.0**: el README indica que el proyecto aun esta antes de v1.0 y puede introducir cambios incompatibles entre versiones menores.
- **Madurez desigual**: el propio proyecto indica que algunos Hands son mas maduros que otros; Browser y Researcher se describen como los mas probados.
- **Riesgo de autonomia**: agentes con acceso a navegador, archivos, APIs o canales pueden ejecutar acciones incorrectas si no hay aprobaciones y limites.
- **Dependencia de integraciones externas**: canales, APIs, proveedores LLM y servicios de terceros pueden cambiar, fallar o requerir credenciales.
- **Complejidad operacional**: aunque se distribuya como un binario, operar memoria, canales, credenciales, modelos y seguridad requiere disciplina de plataforma.
- **Datos sensibles**: memoria persistente, logs, auditoria y conversaciones pueden almacenar informacion privada si no se configuran politicas claras.
- **Validar claims tecnicos**: cifras como numero de pruebas, canales, herramientas o proveedores pueden cambiar entre releases; conviene verificar contra la version instalada.

## Recomendaciones

- Probar primero en entorno local o sandbox antes de conectarlo a canales reales.
- Fijar version o commit para pruebas serias y despliegues controlados.
- Revisar `openfang.toml.example` y configurar permisos minimos por agente.
- Usar proveedores LLM y credenciales con scopes limitados.
- Activar aprobacion humana para publicaciones, compras, acciones destructivas, mensajes externos y cambios costosos.
- Revisar logs, auditoria y memoria persistente periodicamente.
- Empezar con Hands maduros como Researcher o Browser antes de automatizar flujos criticos.
- Para produccion, separar entorno de pruebas, staging y produccion.
- Documentar cada agente: proposito, canales, herramientas permitidas, modelo usado, fuentes de datos y acciones que requieren aprobacion.
- Evaluar si OpenFang encaja mejor como plataforma de agentes completa o si un framework mas pequeno basta para el caso de uso.

## Relacion con Agent Harness

OpenFang puede verse como una implementacion amplia de un **agent harness**. No solo invoca un modelo: agrega runtime, herramientas, memoria, politicas, canales, auditoria, API, scheduler, seguridad y empaquetado de capacidades. Por eso encaja mas como plataforma operacional de agentes que como simple libreria de prompts.

## Resumen

OpenFang es una plataforma open source para construir y ejecutar agentes autonomos self-hosted. Su propuesta central es empaquetar en Rust un sistema completo con runtime, memoria, herramientas, canales, Hands, seguridad, API y dashboard. Es atractivo para investigacion, automatizacion, agentes multicanal y operaciones de agentes persistentes, pero al estar pre-1.0 requiere evaluacion cuidadosa, versionado fijo, permisos minimos y supervision humana en acciones sensibles.

## Fuentes Consultadas

- Repositorio oficial: https://github.com/RightNow-AI/openfang
- Sitio oficial: https://www.openfang.sh/
- Documentacion: https://openfang.sh

