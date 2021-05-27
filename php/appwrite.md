# Appwrite


Secure Open-Source Backend Server for Web, Mobile & Flutter Developers

Appwrite is a self-hosted solution that provides developers with a set of easy-to-use and integrate REST APIs to manage their core backend needs.

SDKs

Below is a list of currently supported platforms and languages. If you wish to help us add support to your platform of choice, you can go over to our SDK Generator project and view our contribution guide.
Client

- Web (Maintained by the Appwrite Team)
- Flutter (Maintained by the Appwrite Team)


Secure Open-Source Backend Server for Web, Mobile & Flutter Developers
Appwrite is a self-hosted solution that provides developers with a set of easy-to-use and integrate REST APIs to manage their core backend needs.

https://appwrite.io/

Features
- Database
- Storage
- Users
- GEO & Localization
- Functions
- Security

Tecnologia
- php
- mysql


Other Technologies

- Redis - for managing cache and in-memory data (currently, we do not use Redis for persistent data)
- MariaDB - for database storage and queries
- InfluxDB - for managing stats and time-series based data
- Statsd - for sending data over UDP protocol (using Telegraf)
- ClamAV - for validating and scanning storage files
- Imagemagick - for manipulating and managing image media files.
- Webp - for better compression of images on supporting clients
- SMTP - for sending email messages and alerts
- Resque - for managing data queues and scheduled tasks over a Redis server


https://appwrite.io/


# Architecture

Appwrite's current structure is a combination of both Monolithic and Microservice architectures, but our final goal, as we grow, is to be using only microservices.


Appwrite backend API is written primarily with PHP version 7 and above on top of the Utopia PHP framework. The Appwrite frontend is built with tools like gulp, less, and litespeed.js. We use Docker as the container technology to package the Appwrite server for easy integration on-cloud, on-premise, or on-localhosts.



# User Interface

Appwrite uses an internal micro-framework called Litespeed.js to build simple UI components in vanilla JS and less for compiling CSS code. To apply any of your changes to the UI, use the gulp build or gulp less commands, and restart the Appwrite main container to load the new static files to memory using docker-compose restart appwrite.
