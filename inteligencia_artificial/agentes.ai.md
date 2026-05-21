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

# Skills

Skills (habilidades) son capacidades modulares y especializadas que se le otorgan a un agente de IA para que pueda realizar tareas concretas más allá de solo generar texto.

Conceptualmente, un skill es una unidad de funcionalidad que el agente puede invocar cuando detecta que una tarea lo requiere. En lugar de que el LLM "sepa todo de memoria", los skills le permiten actuar en el mundo real: buscar en internet, ejecutar código, leer archivos, llamar APIs, etc.


Un Skill como archivo es una forma de codificar el conocimiento de tu equipo — tus convenciones, patrones, estándares — para que el agente IA los respete automáticamente, sin que tengas que explicárselos cada vez.


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

## CrewAI 

Framework for orchestrating role-playing autonomous AI agents in collaborative teams. GitHub stars



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