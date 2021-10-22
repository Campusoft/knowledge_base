# ANGULAR
 
Angular is an entire platform for building modern applications.

# Modules 
 
 
 Angular modules is a process or system to assemble multiple angular elements, like components, directives, pipes, service, etc. so that these Angular elements can be combined in such a way that all elements can be related with each other and ultimately create an application.

# Component

Pass data to a child component
The @Input() decorator indicates that the property value passes in from the component's parent

- https://angular.io/start#pass-data-to-a-child-component

Pass data to a parent component
@Output(). Decorator that marks a class field as an output property and supplies configuration metadata

- https://angular.io/start#pass-data-to-a-parent-component
 

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
 
# Routing
 
 
# Dependency injection in Angular

Injecting services

Injecting services results in making them visible to a component.
To inject a dependency in a component's constructor(), supply a constructor argument with the dependency type

```
constructor(fooService: FooService)
```

# Consumir REST

Communicating with backend services using HTTP
https://angular.io/guide/http
 


# Angular para web y moviles
Apps That Work Natively on the Web and Mobile
https://blog.angular.io/apps-that-work-natively-on-the-web-and-mobile-9b26852495e7


Angular PWA

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

https://angular.io/cli

# Schematics 

 Creando nuestro primer Angular Schematics 
https://dev.to/fullstapps/creando-nuestro-primer-angular-schematics-4b2o


Generar codigo (CRUD). Con schematics
https://github.com/manfredsteyer/angular-crud
 
# Library

In Angular, a project that provides functionality that can be included in other Angular applications. A library isn't a complete Angular application and can't run independently.

https://angular.io/guide/libraries


# best practice standards

Angular coding style guide
https://angular.io/guide/styleguide


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

# Varios

- Estructura 
 
