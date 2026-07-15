# Agente Inteligente


Características principales de un Agente Inteligente:

- Autonomía: Opera independientemente sin intervención humana constante
- Percepción del entorno: Recibe y procesa información de su ambiente
- Capacidad de acción: Puede ejecutar tareas y modificar su entorno
- Orientación a objetivos: Trabaja hacia metas específicas
- Aprendizaje: Mejora su desempeño con la experiencia
- Razonamiento: Procesa información para tomar decisiones complejas


#  Memoria


Tipos de memoria

Memoria de corto plazo (Short-Term / Working Memory)

Similar a lo que tienes en la mente durante una conversación

Características:

- Contexto reciente (últimos mensajes)
- Se envía al LLM en cada prompt
- Limitada por tokens

Tecnologías:

- Buffer de conversación
- Redis (opcional)
- Context window del modelo


Memoria de largo plazo (Long-Term Memory)

Similar a lo que recuerdas de una persona con el tiempo

Características:

- Persistente
- Independiente de la conversación actual
- Recuperada bajo demanda

Tecnologías:

- Bases de datos (Postgres, Mongo)
- Vector DB (Pinecone, Chroma, Weaviate)

Memoria semántica

Conocimiento estructurado

Incluye:

- Información del negocio
- Catálogo de productos
- Políticas


Esto normalmente se implementa con:
- RAG (Retrieval Augmented Generation)

# Enrutamiento (Routing)

El enrutamiento introduce lógica condicional en el flujo de trabajo de un agente Permite que el sistema analice una entrada (como una consulta de un usuario) y decida, basándose en criterios específicos, cuál es la función, herramienta o subproceso más adecuado para continuar Es lo que permite pasar de un sistema rígido a uno adaptable y consciente del contexto

Métodos de Implementación

- Enrutamiento basado en LLM: El propio modelo de lenguaje analiza la consulta y devuelve un identificador (como una categoría) que el sistema usa para dirigir el flujo
- Enrutamiento basado en Embeddings: Se convierte la consulta en un vector y se compara con otros vectores que representan diferentes rutas, eligiendo la más similar semánticamente
- Enrutamiento basado en Reglas: Utiliza lógica tradicional (si-entonces) basada en palabras clave o patrones definidos
- Enrutamiento basado en Modelos de ML: Emplea un modelo clasificador especializado y entrenado específicamente para esta tarea de clasificación



# Skills

Skills (habilidades) son capacidades modulares y especializadas que se le otorgan a un agente de IA para que pueda realizar tareas concretas más allá de solo generar texto.

Conceptualmente, un skill es una unidad de funcionalidad que el agente puede invocar cuando detecta que una tarea lo requiere. En lugar de que el LLM "sepa todo de memoria", los skills le permiten actuar en el mundo real: buscar en internet, ejecutar código, leer archivos, llamar APIs, etc.


Un Skill como archivo es una forma de codificar el conocimiento de tu equipo — tus convenciones, patrones, estándares — para que el agente IA los respete automáticamente, sin que tengas que explicárselos cada vez.


El repositorio anthropics/skills es un proyecto oficial de Anthropic diseñado para albergar, demostrar y estandarizar el uso de las "Skills" (Habilidades) de Claude.
https://github.com/anthropics/skills


Codex NO usa "Skills" — usa AGENTS.md


# sim

 
Open-source platform to build and deploy AI agent workflows.

Sim is a visual workflow builder for AI applications that lets you build AI agent workflows visually. Create powerful AI agents, automation workflows, and data processing pipelines by connecting blocks on a canvas—no coding required.


# OpenAI AgentKit 



# Microsoft 365 Agents SDK  
 
With the Microsoft 365 Agents SDK, you can create agents deployable to channels of your choice, such as Microsoft 365 Copilot, Microsoft Teams, Web & Custom Apps and more, with scaffolding to handle the required communication. Developers can use the AI Services of their choice, and make the agents they build available using the channel management capabilities of the SDK.

# archivos README de instrucciones


Los archivos README o de instrucciones para agentes son documentos diseñados para guiar el comportamiento, capacidades, límites y uso adecuado de un agente de software (por ejemplo, un agente de IA, microservicio autónomo, bot, worker, etc.). Funcionan como el “manual operativo” que un agente sigue para ejecutar su función dentro de un sistema.


- CLAUDE.md
- CODEX.md
- AGENTS.md



# Agent Frameworks


## Microsoft Agent Framework 

Unified framework merging AutoGen + Semantic Kernel. Multi-agent conversations with enterprise features. GA Q1 2026.

## Microsoft Semantic Kernel

Microsoft Semantic Kernel es un SDK open-source para construir aplicaciones y agentes de IA integrando modelos de lenguaje con código tradicional. Funciona como una capa de orquestación entre el LLM, servicios externos, plugins, memoria, prompts y lógica de negocio existente.

Está orientado principalmente a desarrolladores que ya trabajan con ecosistemas empresariales, especialmente .NET y Azure, aunque también tiene soporte para Python y Java. Su enfoque no es reemplazar una aplicación existente, sino permitir que esa aplicación use capacidades de IA de forma controlada.

Características principales:

- Kernel central: Contenedor que agrupa servicios de IA, plugins, prompts, configuración y dependencias necesarias para ejecutar una tarea.
- Plugins: Funciones reutilizables que exponen capacidades al modelo, como consultar una base de datos, llamar una API, enviar correos, buscar información o ejecutar reglas de negocio.
- Function calling: Permite que el modelo seleccione e invoque funciones disponibles, reduciendo la necesidad de planificadores manuales.
- Conectores de modelos: Soporta servicios como Azure OpenAI, OpenAI y otros proveedores compatibles, según el lenguaje y versión usada.
- Prompts y plantillas: Permite definir prompts parametrizables y combinarlos con funciones nativas.
- Memoria y RAG: Puede integrarse con almacenes vectoriales y conectores de memoria para recuperar información relevante.
- Agentes: Incluye capacidades para construir agentes conversacionales, agentes con herramientas y flujos multiagente.
- Integración empresarial: Encaja bien con aplicaciones .NET, Azure, Microsoft 365, APIs internas y arquitecturas donde se necesita trazabilidad y control.
- OpenAPI y MCP: Puede exponer o consumir herramientas mediante especificaciones OpenAPI y Model Context Protocol, facilitando interoperabilidad con otros sistemas.

Limitaciones:

- Curva de aprendizaje: Requiere entender bien prompts, plugins, servicios de IA, inyección de dependencias y el ciclo de ejecución del kernel.
- APIs en evolución: Algunas capacidades cambian con rapidez, especialmente agentes, memoria, conectores y planificadores.
- Planificadores clásicos: Para nuevos agentes se recomienda usar function calling; los planificadores tradicionales han perdido prioridad o pueden estar deprecados.
- No elimina la necesidad de arquitectura: El framework ayuda a orquestar, pero no resuelve por sí solo seguridad, permisos, observabilidad, costos, evaluación o manejo de errores.
- Dependencia del modelo: La calidad de las decisiones depende del LLM usado, su soporte de function calling, su ventana de contexto y su capacidad de seguir instrucciones.
- Riesgo en herramientas: Dar acceso a funciones reales puede producir acciones incorrectas si no se validan entradas, permisos y confirmaciones humanas.
- Menos natural para prototipos simples: Para un chatbot pequeño o una llamada puntual a un modelo puede ser más complejo de lo necesario.

Recomendaciones:

- Usarlo cuando ya existe una aplicación empresarial y se necesita integrar IA con código, APIs y procesos reales.
- Definir plugins pequeños, con nombres claros, parámetros explícitos y descripciones pensadas para que el LLM los entienda.
- Mantener las funciones sensibles detrás de validaciones, autorización y confirmaciones humanas cuando puedan modificar datos o ejecutar acciones irreversibles.
- Preferir function calling para flujos nuevos en lugar de planificadores clásicos.
- Crear kernels con ciclo de vida controlado; en aplicaciones .NET suele ser recomendable tratarlos como servicios transitorios porque la colección de plugins es mutable.
- Registrar trazas, prompts, llamadas a herramientas, resultados y costos para poder depurar comportamiento.
- Separar conocimiento, acciones y razonamiento: usar RAG para información, plugins para acciones y prompts para instrucciones.
- Evaluar respuestas y ejecuciones con pruebas automatizadas, especialmente cuando el agente decide que herramienta usar.
- No exponer directamente APIs internas completas; crear una capa de funciones seguras y específicas para el agente.

Casos de uso:

- Asistentes empresariales conectados a datos internos, documentos, CRM, ERP o sistemas de tickets.
- Automatización de procesos donde el agente interpreta una solicitud y ejecuta pasos mediante APIs controladas.
- Copilotos para aplicaciones .NET o Azure que ayudan al usuario dentro del flujo de trabajo existente.
- Agentes de soporte que combinan RAG, búsqueda en base de conocimiento y creación de tickets.
- Orquestación de herramientas internas, por ejemplo consultar inventario, validar políticas, generar reportes o resumir expedientes.
- Aplicaciones multiagente donde distintos agentes tienen roles especializados y comparten herramientas.
- Integración de IA generativa en Microsoft 365, Teams o aplicaciones corporativas usando conectores y plugins.

Referencias:

- https://learn.microsoft.com/en-us/semantic-kernel/overview/
- https://learn.microsoft.com/en-us/semantic-kernel/agents/kernel/
- https://learn.microsoft.com/en-us/semantic-kernel/agents/plugins/
- https://learn.microsoft.com/en-us/semantic-kernel/concepts/planning

## CrewAI 

Framework for orchestrating role-playing autonomous AI agents in collaborative teams. GitHub stars

agentes autónomos de IA



## LangChain.js

LangChain.js es la versión para JavaScript y TypeScript de LangChain, uno de los frameworks más populares para construir aplicaciones basadas en Inteligencia Artificial y Modelos de Lenguaje Grande (LLMs), como OpenAI (GPT), Anthropic (Claude), Llama, entre otros.


¿Por qué usar la versión de JavaScript/TypeScript?

Aunque LangChain nació originalmente en Python (el lenguaje rey de la IA), la versión de JS/TS ha ganado un terreno masivo por varias razones:

- Fullstack real: Puedes construir toda la lógica de IA directamente en tu servidor Node.js, Next.js, Deno o Bun, compartiendo tipos de TypeScript con tu frontend.
- Edge Computing: LangChain.js está optimizado para correr en entornos modernos como Cloudflare Workers o Vercel Edge Functions, lo que significa respuestas increíblemente rápidas y menor latencia.
- Ecosistema Web: Ideal para desarrolladores web que no quieren o no necesitan saltar a Python para integrar IA en sus aplicaciones actuales.


#  Agentes de IA autónomos y auto-alojados (self-hosted)


- Hermes Agent
- OpenClaw
- [OpenFang](openfang.md)




# Herramientas / Servicios / Repositorios

## Design research for humans and AI

Refero es una plataforma de referencia y biblioteca visual enfocada en el diseño de interfaces de usuario (UI) y experiencia de usuario (UX). Su función principal es recopilar capturas de pantalla de productos digitales reales (como Stripe, Calendly o Slack) para que diseñadores y desarrolladores puedan estudiar patrones de diseño aplicados en el mercado.


> 2.000+ DESIGN.md de los mejores productos del mercado
> Todo está ahí: colores, tipografía, espaciado, componentes
> La IA lo lee antes de generar, por fin tiene un marco de referencia real
> Gratis, compatible con Claude Code, Cursor, Lovable, Bolt
> Tú eliges un estilo, lo añades a tu repositorio


https://refero.design/


# Referencias


A curated list of AI Agent frameworks, tools, platforms, and resources for 2026 — the year agents went mainstream
https://github.com/Zijian-Ni/awesome-ai-agents-2026
