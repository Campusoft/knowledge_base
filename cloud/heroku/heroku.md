# heroku 


Officially supported languages

-	Node.js
-	Ruby
-	Java
-	PHP
-	Python
-	Go
-	Scala
-	Clojure


Dependency mechanisms vary across languages: in Ruby you use a Gemfile, in Python a requirements.txt, in Node.js a package.json, in Java a pom.xml and so on.

# Knowing what to execute

Knowing what to execute

Heroku can figure it out. For example, in Ruby on Rails, it’s typically rails server, in Django it’s python <app>/manage.py runserver and in Node.js it’s the main field in package.json

For other applications, you may need to explicitly declare what can be executed. You do this in a text file that accompanies your source code - a Procfile.

https://devcenter.heroku.com/articles/how-heroku-works#knowing-what-to-execute


# Databases & Data Management

Heroku provides three managed data services to all customers:

- Heroku Postgres
  - Row Limit 10K
  - Connections 20
- Heroku Redis
  - 25MB Libre
- Apache Kafka on Heroku



# Precios

Tiene capas libres para postgres, Redis

- RAM 512MB (libre)
- Custom domains (SI)
- Free SSL on custom domains (NO)

https://www.heroku.com/pricing


