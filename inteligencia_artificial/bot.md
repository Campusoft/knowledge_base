

# Dialogflow


Plataforma de NLU/NLP de Google para crear chatbots con comprensión del lenguaje natural avanzada.

 Ventajas:

- Excelente NLU (basado en modelos de IA de Google).
- Integración nativa con Google Assistant, Google Cloud, Contact Center AI.
- Interfaz visual de diseño de intents y entidades.
- Soporte multilenguaje robusto.
- Integración fácil con apps web, apps móviles, y servicios de Google.
- Versiones Standard y CX (para flujos complejos).

Desventajas:
- Limitado en personalización de lógica fuera de Google.
- Costos pueden dispararse con alto volumen.
- Menos control si se quiere autohospedar (no es open-source).
- Menos adecuado para integraciones no-Google.

Precios:
- Standard Edition (gratuita): hasta 180 requests/min y 100K intents/mes.
- CX Edition: desde $0.002 por request (entrada/salida).
- Costos por detectIntent, streaming, etc.
- Puede volverse costoso con tráfico alto.

Arquitectura:
- SaaS completamente gestionado por Google Cloud.
- No autohospedable.
- API basada en REST/gRPC.
- Arquitectura basada en intents, entities, contexts y fulfillment (webhooks).
- CX permite flujos de diálogo más complejos (state machines).

Casos de uso:
- Asistentes virtuales con comprensión de lenguaje natural.
- Integración con Google Assistant (smart speakers, apps).
- Centros de contacto automatizados (con CCAI).
- Bots con alta necesidad de entendimiento semántico.

# Botpress

- Ventajas: Interface visual + código, open source, flujos conversacionales claros
- Casos de uso: Empresas que necesitan personalización pero quieren rapidez de desarrollo
- Ideal para: Trámites con múltiples pasos, escalación a humanos


Botpress — The open-source Virtual Assistant platform


Plataforma de código abierto (open-source) para crear chatbots con una interfaz visual intuitiva. Se enfoca en la facilidad de uso y la personalización.

Ventajas:
- Open-source (versión Community gratuita).
- Arquitectura modular y extensible (módulos, integraciones, plugins).
- Interfaz visual de diseño de flujos (drag & drop).
- Autohospedaje (on-premise) posible.
- Soporte para NLU integrado (basado en Transformers).
- Integración con múltiples canales: WhatsApp, Telegram, Webchat, etc.
- Ideal para equipos técnicos que quieren control total.

Desventajas:
- La versión gratuita tiene limitaciones en soporte y funcionalidades avanzadas.
- Curva de aprendizaje moderada para personalización avanzada.
- Rendimiento puede decaer con bots muy complejos sin optimización.

Precios:
- Community (gratuita): Autohospedada, código abierto.
- Cloud (SaaS): Desde $49/mes (hasta 100K mensajes/mes).
- Enterprise: Precio personalizado (on-premise + soporte avanzado).

Arquitectura:
- Basada en Node.js.
- Frontend en React.
- Base de datos: PostgreSQL o SQLite.
- Autohospedable o en la nube.
- Arquitectura modular: se pueden añadir módulos personalizados.

Casos de uso:
- Chatbots internos para empresas.
- Servicio al cliente automatizado (SAC).
- Automatización de procesos internos.
- Proyectos con necesidad de privacidad de datos (por autohospedaje).



https://github.com/botpress/botpress




# Microsoft Bot Framework 


Plataforma completa de Microsoft para crear, desplegar y gestionar bots. Parte del ecosistema Azure.

Ventajas:
- Integración profunda con Azure y servicios de Microsoft (Teams, Power Platform).
- SDKs en múltiples lenguajes (C#, JavaScript, Python).
- Bot Framework Composer: herramienta visual para diseñar conversaciones.
- Escalabilidad en la nube con Azure.
- Excelente soporte para bots en Microsoft Teams.
- Conector universal para múltiples canales (Slack, Facebook, Telegram, etc.).


Desventajas:
- Complejidad técnica mayor (requiere conocimientos de desarrollo).
- Costos pueden escalar rápido en Azure (especialmente si se usan servicios adicionales como LUIS, QnA Maker, etc.).
- Menos intuitivo para usuarios no técnicos.
- Dependencia del ecosistema Microsoft.

Precios:
- Framework en sí es gratuito (SDKs, Composer).
- Costos asociados a Azure:
- Bot Service: gratuito hasta cierto uso.
- LUIS (NLU): desde $1 por 1K transacciones.
- App Service, Cognitive Services, etc.: costos variables.
- Total puede variar desde $0 (pequeños proyectos) hasta miles.

Arquitectura:
- Basado en servicios en la nube de Azure.
- Arquitectura basada en microservicios.
- Puede desplegarse en contenedores (Docker) o Azure App Service.
- Comunicación mediante el Bot Connector.
- Uso de LUIS para NLU o Azure Cognitive Services.

Casos de uso:
- Integración con Microsoft Teams (bots internos, productividad).
- Empresas que ya usan Azure.
- Soluciones empresariales complejas.
- Automatización con Power Automate.



- Azure Bot Service Pricing:

Free (10,000 messages/month)
S1 $0,50 per 1,000 messages (on Premium Channels)

## Bot Framework Emulator

Bot Framework Emulator being retired in favor of Agents Playground

We are in the process of archiving the Bot Framework Emulator repository on GitHub. This means that this project will no longer be updated or maintained. Customers using this tool will not be disrupted. However, the tool will no longer be supported through service tickets in the Azure portal and will not receive product updates.

We plan to archive this project no later than end of December of 2025.


## Bot Framework Composer

Bot Framework Composer is an open-source, visual authoring canvas for developers and multi-disciplinary teams to design and build conversational experiences with Language Understanding and QnA Maker, and a sophisticated composition of bot replies (Language Generation). Within this tool, you'll have everything you need to build a sophisticated conversational experience.



# Chatwoot

Plataforma open-source de atención al cliente con chatbot incluido. Más orientado a mensajería conversacional unificada que a IA pura.

Ventajas:
- Open-source y autohospedable.
- Enfocado en soporte al cliente (no solo chatbots).
- Interfaz tipo "inbox" (como Intercom o Zendesk).
- Integración con WhatsApp, Facebook, Instagram, email, etc.
- Chatbot básico con reglas (no NLU avanzado por defecto, pero puede integrarse).
- Ideal para equipos de soporte.

Desventajas:
- Capacidad de NLU limitada (requiere integrar con otras herramientas como Dialogflow o Rasa).
- Menos potente para bots con lógica conversacional compleja.
- La automatización es más básica (por reglas).
- Menos madurez en flujos de diálogo avanzados.


Precios:
- Self-hosted (gratuito): Código abierto, puedes instalarlo gratis.
- Cloud (SaaS): Desde $29/mes (hasta 10 agentes, 10K conversaciones/mes).
- Escalabilidad con planes empresariales.

Arquitectura:
- Backend en Ruby on Rails.
- Frontend en Vue.js.
- Base de datos: PostgreSQL.
- Autohospedable o en la nube.
- API RESTful y webhooks para integraciones.

Casos de uso:
- Centros de atención al cliente.
- Empresas que quieren unificar canales de comunicación.
- Chatbots simples (FAQ, rutas de conversación fijas).
- Startups que buscan una solución todo-en-uno de soporte.


# Revisiones

https://wit.ai/


Kore.ai


Chatbots 
https://wotnot.io/

# Referencias

Chatbots: The Definitive Guide (2021)
https://www.artificial-solutions.com/chatbots

Chatbot Software — Ultimate Guide 2019 (& The Best Software!)
https://medium.com/@taylorbologni/chatbot-software-ultimate-guide-2019-the-best-software-921c664978a6