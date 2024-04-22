# Laravel


# Architecture


Laravel follows the Model-View-Controller (MVC) architectural pattern, which separates the application into three key components: Models, Views, and Controllers



## Request Lifecycle

The entry point for all requests to a Laravel application is the public/index.php file.



## Service Providers

One of the most important kernel bootstrapping actions is loading the service providers for your application. Service providers are responsible for bootstrapping all of the framework's various components, such as the database, queue, validation, and routing components.


# Routing


Routing plays a pivotal role in Laravel’s architecture. Routes define how incoming requests should be handled



## API Routes

If your application will also offer a stateless API, you may enable API routing using the install:api Artisan command:

´´´
php artisan install:api
´´´


# Eloquent ORM


Laravel includes Eloquent, an object-relational mapper (ORM) that makes it enjoyable to interact with your database. When using Eloquent, each database table has a corresponding "Model" that is used to interact with that table. In addition to retrieving records from the database table, Eloquent models allow you to insert, update, and delete records from the table as well.

# API REST



Laravel Sanctum

Laravel Sanctum provides a featherweight authentication system for SPAs (single page applications), mobile applications, and simple, token based APIs.



# Blade Templating Engine


# Asset Bundling

Asset Bundling (Vite)

When building applications with Laravel, you will typically use Vite to bundle your application's CSS and JavaScript files into production ready assets.


# Artisan 

Artisan is the command line interface included with Laravel. Artisan exists at the root of your application as the artisan script and provides a number of helpful commands that can assist you while you build your application.

´´´
php artisan help migrate
´´´

# deploy

Hacer deploy en Laravel para dummies 
- Git
- Cpanel
https://platzi.com/tutoriales/1842-intro-laravel-2020/6546-hacer-deploy-en-laravel-para-dummies/


# Starter Kits

## Breeze


Breeze provides a minimal and simple starting point for building a Laravel application with authentication. Styled with Tailwind, Breeze publishes authentication controllers and views to your application that can be easily customized based on your own application's needs.




# Version 

This command will display the Laravel version of your project.

´´´
php artisan --version
´´´

Laravel stores information about its version in a file Application.php within the framework’s core directory

´´´
vendor/laravel/framework/src/Illuminate/Foundation/Application.php
´´´





# Referencias



## Training


Laravel Bootcamp! In this guide we will walk through building a modern Laravel application from scratch. To explore the framework, we'll build a microblogging platform called Chirper.
https://bootcamp.laravel.com/