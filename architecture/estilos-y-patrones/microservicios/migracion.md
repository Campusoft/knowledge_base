# Migration

Strategy
- Strategy 2 – Split Frontend and Backend
- Strategy 3 – Extract Services
- Strategy 1 – Stop Digging



8 Steps for Migrating Existing Applications to Microservices 
https://insights.sei.cmu.edu/blog/8-steps-for-migrating-existing-applications-to-microservices/


Migrating Monoliths to Microservices with Decomposition and Incremental Changes
https://www.infoq.com/articles/migrating-monoliths-to-microservices-with-decomposition/ 

Refactoring a Monolith into Microservices
- Strategy 1 – Stop Digging
- Strategy 2 – Split Frontend and Backend
- Strategy 3 – Extract Services
https://www.nginx.com/blog/refactoring-a-monolith-into-microservices/ 


Implement new functionality as services

A good way to begin the migration to microservices is to implement significant new functionality as services. This is sometimes easier than breaking apart of the monolith. It also demonstrates to the business that using microservices significantly accelerates software delivery.

Extract services from the monolith

While implementing new functionality as services is extremely useful, the only way of eliminating the monolith is to incrementally extract modules out of the monolith and convert them into services.
https://microservices.io/refactoring/


Application modernization patterns with Apache Kafka, Debezium, and Kubernetes 
- Menciona porque es importante utilizar Sidecar pattern
https://developers.redhat.com/articles/2021/06/14/application-modernization-patterns-apache-kafka-debezium-and-kubernetes#after_the_migration__modernization_challenges


Step 3: Migrate the database
- Having a separate database requires data synchronization. Once again, we can choose from a few common technology approaches.
  - Triggers
  - Queries. The changes are typically detected with implementation strategies such as timestamps, version numbers, or status column changes in the source database
  - Log readers
  

# Referencias

Migración a microservicios: la transición del monolito a los microservicios

https://www.hiberus.com/crecemos-contigo/migracion-microservicios/