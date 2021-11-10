# Router


Páginas y rutas Angular SPA
https://academia-binaria.com/paginas-y-rutas-angular-spa/


The appRoutes array of routes describes how to navigate. Pass it to the RouterModule.forRoot method in the module imports to configure the router.


Router outlet
The RouterOutlet is a directive from the router library that is used like a component. It acts as a placeholder that marks the spot in the template where the router should display the components for that outlet.


Routing & Navigation
https://angular.io/guide/router



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
