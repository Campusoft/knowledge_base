# n8n
 
n8n is an extendable workflow automation tool. With a fair-code distribution model, n8n will always have visible source code, be available to self-host, and allow you to add your own custom functions, logic and apps. n8n's node-based approach makes it highly versatile, enabling you to connect anything to everything.

 
Quick Start

Try n8n instantly with npx (requires Node.js):

```
npx n8n
```

Docker


https://docs.n8n.io/hosting/installation/docker/
 


Cuando ejecutas n8n start en Windows, el sistema utiliza por defecto una base de datos SQLite. El archivo y toda la configuración asociada se guardan en una carpeta oculta dentro de tu perfil de usuario.

Ubicación exacta
La ruta predeterminada es:
C:\Users\TU_USUARIO\.n8n\

Dentro de esa carpeta, encontrarás un archivo llamado:
database.sqlite


# Arquitectura  
 

Componente | Descripción
-- | --
Frontend (Editor UI) | Interfaz visual (Vue.js) para diseñar workflows arrastrando y soltando nodos.
Backend (Core Engine) | Servidor Node.js que ejecuta los flujos, maneja colas, webhooks y APIs.
Base de datos | Guarda workflows, ejecuciones, credenciales, usuarios. Soporta SQLite, PostgreSQL, y MySQL.
Motor de ejecución | Ejecuta los flujos nodo por nodo, gestionando dependencias, errores y datos intermedios.
 
 
Tecnologías base

- Node.js (motor principal)
- TypeScript
- Vue.js (frontend)
- SQLite / PostgreSQL / MySQL
- Express.js (API y webhooks)
- Redis (opcional) para colas de ejecución
 
 
# Integraciones y nodos

- Más de 350 nodos integrados: HTTP Request, Webhook, Email, Slack, MySQL, Postgres, Google Sheets, GitHub, etc.

- Nodos customizados: puedes crear los tuyos en TypeScript/JavaScript.

- Los nodos pueden ser activadores (triggers) o acciones (actions).
 
 
# Disparadores (Triggers)

- Webhook trigger: ejecuta un flujo cuando llega una llamada HTTP.
- Cron trigger: ejecuciones programadas (cada X minutos, diario, semanal, etc.).
- Interval trigger: repite acciones a intervalos configurables.
 
 
 
# wait

- n8n-nodes-base.wait

Use the Wait node pause your workflow's execution. When the workflow pauses it offloads the execution data to the database. When the resume condition is met, the workflow reloads the data and the execution continues.

 
 
¿Qué pasa internamente cuando se usa Wait?

- Se GUARDA en Base de Datos

Cuando un workflow llega a un nodo Wait, n8n:

Serializa todo el estado de ejecución incluyendo:
- Todos los datos de los nodos anteriores
- Variables y contexto
- El punto exacto donde se pausó
- Metadata de la ejecución


Guarda en la base de datos (PostgreSQL, MySQL, SQLite según tu configuración):

- Tabla: execution_entity
- Estado: waiting
- Incluye: data, workflowData, waitTill


Libera la memoria - El proceso NO permanece en memoria
Genera un token único para la URL de reanudación

Cuando se llama la URL de reinicio:

n8n busca la ejecución guardada por el token
Deserializa todo el estado
Continúa desde el nodo Wait con los datos guardados
Mantiene acceso a TODOS los nodos anteriores con $node.json 





https://docs.n8n.io/integrations/builtin/core-nodes/n8n-nodes-base.wait/


# Agentes IA

- Claude.ai (La interfaz de Chat)
  - API: Esta suscripción no te da acceso a la API.

- Console.anthropic.com (La consola de API)
  - Para qué sirve: Es exclusiva para desarrolladores y herramientas como n8n.

https://console.anthropic.com


## openai


Entra a la plataforma: Ve a platform.openai.com.

- Dashboard de API: Haz clic en la pestaña "API Keys" en la barra lateral izquierda.
- Crear Llave: Haz clic en "+ Create new secret key".
- Saldo (Credits): Al igual que con Claude, debes ir a "Settings" > "Billing" y cargar saldo (mínimo $5). OpenAI no funciona en n8n si no tienes créditos comprados, incluso si tienes la suscripción Plus activa.

# nodos


## Puppeteer/Playwright

n8n (Nodos de Puppeteer/Playwright): Permite crear flujos de trabajo donde un nodo recibe datos (ej. de un webhook o base de datos) y un script de Puppeteer ingresa esos datos en un sitio web de facturación.


# Trailer


# Recursos

280+ free n8n automation templates — ready-to-use workflows for Gmail, Telegram, Slack, Discord, WhatsApp, Google Drive, Notion, OpenAI, and more. AI agents, RAG chatbots, email automation, social media, DevOps, and document processing. The largest open-source n8n template collection.

https://github.com/enescingoz/awesome-n8n-templates

