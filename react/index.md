# React

React components must represent the state of the view at any point in time and not only at initialization time.

Lo primero que se debe conocer de react:

Thinking in React
https://reactjs.org/docs/thinking-in-react.html

# Conceptos

- Cual es diferencia React y Angular ?
-- Bind. (Es una sola Via).

# JSX


##  Estados, Propiedades. Componete Padre / Hijo
![imagen](https://user-images.githubusercontent.com/222181/116284217-7363a880-a752-11eb-9549-ba257159ebf1.png)

## State (Estado)

## Propiedades 

## Contexto



# Components



# Patrones

## Higher-order component

Un componente de orden superior (HOC por las siglas en inglés de higher-order component) 


## Hooks

Hooks son funciones que te permiten “enganchar” el estado de React y el ciclo de vida desde componentes funcionales. Los hooks no funcionan dentro de las clases — te permiten usar React sin clases.

useEffect

Cuando llamas a useEffect, le estás diciendo a React que ejecute tu función de “efecto” después de vaciar los cambios en el DOM. Los efectos se declaran dentro del componente para que tengan acceso a sus props y estado. De forma predeterminada, React ejecuta los efectos después de cada renderizado — incluyendo el primer renderizado.

Esto es porque en muchas ocasiones queremos llevar a cabo el mismo efecto secundario sin importar si el componente acaba de montarse o si se ha actualizado
componentDidMount() {    document.title = `You clicked ${this.state.count} times`;  }  componentDidUpdate() {    document.title = `You clicked ${this.state.count} times`;  }

¿Qué hace useEffect? Al usar este Hook, le estamos indicando a React que el componente tiene que hacer algo después de renderizarse. React recordará la función que le hemos pasado (nos referiremos a ella como nuestro “efecto”), y la llamará más tarde después de actualizar el DOM. En este efecto, actualizamos el título del documento, pero también podríamos hacer peticiones de datos o invocar alguna API imperativa.

A veces, queremos reutilizar alguna lógica de estado entre componentes. Tradicionalmente, había dos soluciones populares para este problema: componente de orden superior y render props. Los Hooks personalizados te permiten hacer esto, pero sin agregar más componentes a tu árbol.
Reusing logic in React has been complex, and patterns like HOCs and Render Props tried to solve that problem. With the recent addition of Hooks, reusing logic becomes easier



## Referencias

Pagina dedicada a patrones en react. 
https://reactpatterns.com/

10 React mini-patterns React
https://hackernoon.com/10-react-mini-patterns-c1da92f068c5



# Varios


Establecer el contenido html  de un div:

dangerouslySetInnerHTML is React’s replacement for using innerHTML in the browser DOM. 
https://reactjs.org/docs/dom-elements.html#dangerouslysetinnerhtml




# Codigo Referencias


starter-kit

Cross-platform web development with Visual Studio Code, C#, F#, JavaScript, ASP.NET Core, EF Core, React (ReactJS), Redux, Babel. Single-page application boilerplate.
https://github.com/kriasoft/aspnet-starter-kit



