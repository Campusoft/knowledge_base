# ANGULAR
 
Angular is an entire platform for building modern applications.

# Modules 
 
 
 Angular modules is a process or system to assemble multiple angular elements, like components, directives, pipes, service, etc. so that these Angular elements can be combined in such a way that all elements can be related with each other and ultimately create an application.

# Component

Pass data to a child component
The @Input() decorator indicates that the property value passes in from the component's parent

https://angular.io/start#pass-data-to-a-child-component

Pass data to a parent component
@Output(). Decorator that marks a class field as an output property and supplies configuration metadata

https://angular.io/start#pass-data-to-a-parent-component
 

Establecer class en el host element de un componente. Utilizar propiedad host del @Component
```
@Component({
   selector: 'my-component',
   template: 'app-element',
   host: {'class': '<NOMBRE-CLASE-HOST-ELEMENTO-COMPONENTE'}
})
export class App implements OnInit {
...
} 
```

## Referencias

Passing data from child to parent component in Angular 
- The @Output() decorator in a child component or directive lets data flow from the child to the parent.
- Bind Property in Parent Component template
- Use Property in Parent Component class 
https://dev.to/this-is-angular/passing-data-from-child-to-parent-component-in-angular-2f0m
 
# Routing
 
 
# Dependency injection in Angular

Injecting services

Injecting services results in making them visible to a component.
To inject a dependency in a component's constructor(), supply a constructor argument with the dependency type

```
constructor(fooService: FooService)
```

An object that implements one of the Provider interfaces. A provider object defines how to obtain an injectable dependency associated with a DI token. An injector uses the provider to create a new instance of a dependency for a class that requires it.


APP_INITIALIZER

The provided functions are injected at application startup and executed during app initialization. If any of these functions returns a Promise or an Observable, initialization does not complete until the Promise is resolved or the Observable is completed.

You can, for example, create a factory function that loads language data or an external configuration, and provide that function to the APP_INITIALIZER token. The function is executed during the application bootstrap process, and the needed data is available on startup.


# Consumir REST

Communicating with backend services using HTTP
https://angular.io/guide/http
 


# Angular para web y moviles
Apps That Work Natively on the Web and Mobile
https://blog.angular.io/apps-that-work-natively-on-the-web-and-mobile-9b26852495e7


Angular PWA


# Pipes

Use pipes to transform strings, currency amounts, dates, and other data for display. Pipes are simple functions to use in template expressions to accept an input value and return a transformed value

https://angular.io/guide/pipes

# Angular CLI


- ng generate component <NOMBRE-COMPONENTE>
- ng generate service <NOMBRE-SERVICIO>

Ejemplos para crear componentes:

Crear un componente en una libreria "@mre/administrative-unit", en una subcarpeta "components", con el nombre "unity-type"

```
ng generate component components/unity-type --project=@mre/administrative-unit
```

Crear un componente "new-component" en el modulo "Person", en el proyecto principal

```
ng g component Person/new-component
```

Crear un nuevo proyecto

```
ng new <Name-Project>
```

https://angular.io/cli

# Schematics 

 Creando nuestro primer Angular Schematics 
https://dev.to/fullstapps/creando-nuestro-primer-angular-schematics-4b2o


Generar codigo (CRUD). Con schematics
https://github.com/manfredsteyer/angular-crud
 




# Estilos

Angular applications are styled with standard CSS. That means you can apply everything you know about CSS stylesheets, selectors, rules, and media queries directly to Angular applications.

Additionally, Angular can bundle component styles with components, enabling a more modular design than regular stylesheets.

assets


# Varios

***Sharing data between child and parent directives and components***

A common pattern in Angular is sharing data between a parent component and one or more child components. Implement this pattern with the @Input() and @Output() decorators.

https://angular.io/guide/inputs-outputs


....
- Estructura 

# Versiones

**Angular 15**

- Standalone components are long-awaited features that enable developers to build Angular applications without using Modules.


# Referencias


Varios artículos para aprender angular 8 

-    What's New Angular 8
-    Angular 8: How to start (Day 1)
-    Angular 8: Components (Day 2)
-    Angular 8: Data Binding (Day 3)
-    Angular 8: Directives (Day 4)
-    Angular 8: Pipes (Day 5)
-    Angular 8: View Encapsulation (Day 6)
https://www.c-sharpcorner.com/article/learn-angular-8-step-by-step-in-10-days-angular-forms-day-7/


Manual, simple, para conectar con un API. 
https://www.freecodecamp.org/news/angular-8-tutorial-in-easy-steps/


**Referencias de Codigo**

Referencia complete de una aplicación grande 
https://github.com/gothinkster/realworld

Angular 2 services for consuming PrestaShop API
https://github.com/SKaDiZZ/angular2-presta

Solucion complete de .net core, con diferentes arquiecturas y tecnologias. 
Un client web, con angular. (*)
https://github.com/dotnet-architecture/eShopOnContainers

**Referencias de aplicaciones**

Add an AngularJS admin GUI to any RESTful API http://ng-admin-book.marmelab.com/


 Customizable admin dashboard template based on Angular 10+ 
https://github.com/akveo/ngx-admin





**Referencias de Libros**

Angular 2+ Notes for Professionals - Compiled from StackOverflow documentation
https://goalkicker.com/Angular2Book/


We developed this book to be used as course material for Rangle's Angular training, but many people have found it to be useful for learning Angular on their own. This book will cover the most important Angular topics, from getting started with the Angular toolchain to writing Angular applications in a scalable and maintainable manner. We hope you enjoy this book.
https://angular-training-guide.rangle.io/


 
