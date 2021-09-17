# Forms

Angular provides two different approaches to handling user input through forms: reactive and template-driven. Both capture user input events from the view, validate the user input, create a form model and data model to update, and provide a way to track changes.


https://angular.io/guide/forms-overview

# Template-driven

Template-driven forms rely on directives in the template to create and manipulate the underlying object model. They are useful for adding a simple form to an app, such as an email list signup form. They're easy to add to an app, but they don't scale as well as reactive forms. If you have very basic form requirements and logic that can be managed solely in the template, template-driven forms could be a good fit.


Indirect access to forms model in a template-driven form.
![imagen](https://user-images.githubusercontent.com/222181/132102271-f996cef2-e434-4a9c-923a-ff2cf40b60e4.png)


# Reactive

Reactive forms provide direct, explicit access to the underlying forms object model. Compared to template-driven forms, they are more robust: they're more scalable, reusable, and testable. If forms are a key part of your application, or you're already using reactive patterns for building your application, use reactive forms.

With reactive forms, you define the form model directly in the component class. The [formControl] directive links the explicitly created FormControl instance to a specific form element in the view, using an internal value accessor.

![imagen](https://user-images.githubusercontent.com/222181/132102216-4058d064-9530-41c4-9d23-f40ef2186f1c.png)


Here are the differences between Template-Driven and Reactive Forms:

- Template Driven Forms need the FormsModule, while Reactive forms need the ReactiveFormsModule
- Template Driven Forms are based only on template directives, while Reactive forms are defined programmatically at the level of the component class
- Reactive Forms are a better default choice for new applications, as they are more powerful and easier to use.
- The Reactive approach removes validation logic from the template, keeping the templates cleaner.



We can see that FormGroup provides two API methods for updating form values:

-    we have patchValue() which partially updates the form. This method does not need to receive values for all fields of the form, so we can use to update only a few fields at a time
-    there is also setValue(), to which we are passing all the values of the form. In the case of this method, values for all form fields will need to be provided, otherwise, we will get an error message saying that some fields are missing


```
fullUpdate() {
    this.form.setValue({firstName: 'Partial', password: 'monkey'});
}

partialUpdate() {
    this.form.patchValue({firstName: 'Partial'});
}
```

Ref: https://angular.io/guide/reactive-forms#updating-parts-of-the-data-model



**composing complex form**

Form groups can accept both individual form control instances and other form group instances as children. This makes composing complex form models easier to maintain and logically group together.

**FormBuilder**

## Creating dynamic forms

https://angular.io/guide/reactive-forms#creating-dynamic-forms

Building dynamic forms
https://angular.io/guide/dynamic-form


**Formly**

JSON powered / Dynamic forms for Angular. (ngx-formly)
Formularios en:
- Bootstrap 	
- Material2 
- Ionic 	
- PrimeNG 
- Kendo 	
Caracteristicas
- Ngx Formly is built on top of the Reactive forms in Angular.

Informacion
- Build Fast, JSON-Powered Forms on Angular With Ngx Formly
https://betterprogramming.pub/build-fast-json-powered-forms-on-angular-with-ngx-formly-b7a00733e66e

https://github.com/ngx-formly/ngx-formly

#  Validating form input

How to Validate Angular Reactive Forms
https://www.freecodecamp.org/news/how-to-validate-angular-reactive-forms/


Angular Custom Form Validators: Complete Guide
https://blog.angular-university.io/angular-custom-validators/

Angular Reactive Forms Validation
https://www.tektutorialshub.com/angular/angular-reactive-forms-validation/

# Generator

**Angular Form Generator**
 
Generates an Angular ReactiveForm from a Swagger or OpenAPI definition. 
https://github.com/verizonconnect/ngx-form-generator

Referencia codigo generado con jhipster para angular:
https://github.com/jhipster/jhipster-sample-app

