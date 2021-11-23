# Router


Páginas y rutas Angular SPA
https://academia-binaria.com/paginas-y-rutas-angular-spa/


The appRoutes array of routes describes how to navigate. Pass it to the RouterModule.forRoot method in the module imports to configure the router.


Router outlet
The RouterOutlet is a directive from the router library that is used like a component. It acts as a placeholder that marks the spot in the template where the router should display the components for that outlet.


Routing & Navigation
https://angular.io/guide/router


# ActivatedRoute

There are two ways in which you can use the ActivatedRoute to get the parameter value from the ParamMap object.

- Using Snapshot
- Using observable


***Referencias***

Cómo usar los parámetros de consulta en Angular
- Presentamos queryParams y queryParamsHandling con Router.navigate y RouterLink. También presentamos queryParams y queryParamMap con ActivatedRoute.
https://www.digitalocean.com/community/tutorials/angular-query-parameters-es

Angular Route Parameters: Passing Parameters to Route. 
- Explica el uso de los metodos para obtener los parametros de una ruta
https://www.tektutorialshub.com/angular/angular-passing-parameters-to-route/

Routing en Angular: Guía completa: Parte 4
-  query params
https://www.acontracorrientech.com/routing-en-angular-guia-completa-parte-4/

# Util

Se puede activar tracing de route 
{ enableTracing: true } // <-- debugging purposes only

This should only be used for debugging purposes. You set the enableTracing: true option in the object passed as the second argument to the RouterModule.forRoot() method.

```
imports: [
    RouterModule.forRoot(
      routes,
      { enableTracing: true } // <-- debugging purposes only
    )
]
```


# Revision

- Rutas hijas
- Rutas en modulos
- Ruta hijas en modulos

- Lazy Loading
