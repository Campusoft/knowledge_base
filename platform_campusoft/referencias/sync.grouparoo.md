# grouparoo
 
Grouparoo is an open source framework that helps you move data between your data warehouse and all of your cloud-based tools.

Reverse ETL, on the other hand, is the process that takes data from a data warehouse and sends that data to any number of different destinations or tools, finally letting you use your data warehouse for more than just analysis.

Reverse ETL allows you to sync records and groups across your tools

You already have the data you need in your warehouse, Reverse ETLs allow you to sync that tailored data to tools such as Salesforce, Zendesk, or Marketo where you can put it to use 


UI Editions

The Grouparoo User Interface (UI) is offered in two separate editions: @grouparoo/ui-community and @grouparoo/ui-enterprise.

Airbyte acquires Grouparoo to accelerate Data Movement
The consolidation of these two communities around one single project will allow Airbyte to deliver the high quality, reliable integrations, and be the only solution to cover the long-tail of connectors across ELT and reverse-ETL.

# architecture

Grouparoo is written in Typescript


Dependencies
- Actionhero
- Sequelize
- resque [node-resque] (/knowledge_base/node/varios.md)
Web
- Next.js
- React
- React-Bootstrap
- fontawesome
	
https://www.grouparoo.com/docs/development#dependencies


Grouparoo stores all of your data in a database. You can use a Postgres or SQLITE database.

The Grouparoo Community Web UI. This package is the open-source Grouparoo Enterprise website UI. It is a Next.JS + React Project. It is meant to be run alongside @grouparoo/core inside a Grouparoo client installation.

# API

Grouparoo's REST API contains endpoints for over 25 topics -- everything from installing a Grouparoo Plugin to getting a list of all your Schedules. To see a full list of API endpoints, navigate to /swagger in the UI of your Grouparoo instance.
# Plugins

# install

Grouparoo is not supported on Windows
https://www.grouparoo.com/docs/installation/node


https://github.com/grouparoo/app-example-docker

# revisiones

npm install @grouparoo/awesome-plugin
npm install @grouparoo/demo

Grouparoo Environment Variables
https://github.com/grouparoo/grouparoo/blob/main/documents/environment-variables.md

No se logra iniciar el programa localmente.
- Inconvenientes con inicio sesion
