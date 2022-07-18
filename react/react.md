# React

React components must represent the state of the view at any point in time and not only at initialization time.

Lo primero que se debe conocer de react:

Thinking in React
https://reactjs.org/docs/thinking-in-react.html

# Conceptos

- Cual es diferencia React y Angular ?
-- Bind. (Es una sola Via).


##  Estados, Propiedades. Componete Padre / Hijo
![imagen](https://user-images.githubusercontent.com/222181/116284217-7363a880-a752-11eb-9549-ba257159ebf1.png)

## State (Estado)

## Propiedades 

## Contexto

# JSX

JSX es una extensión de sintaxis de JavaScript que nos permite mezclar JS y HTML (XML), de ahí su nombre JavaScript XML.

Characteristics of JSX:

- JSX is not mandatory to use there are other ways to achieve the same thing but using JSX makes it easier to develop react application.
- JSX allows writing expression in { }. The expression can be any JS expression or React variable.
- To insert a large block of HTML we have to write it in a parenthesis i.e, ().
- JSX produces react elements.
- JSX follows XML rule.
- After compilation, JSX expressions become regular JavaScript function calls.
- JSX uses camelcase notation for naming HTML attributes. For example, tabindex in HTML is used as tabIndex in JSX.
- ClassName and HTMLFor, not class and for in JSX	
- Write all HTML Attributes in camelCase in JSX. You need to write all HTML attributes and event references in camelCase while writing JSX. So, onclick becomes onClick, onmouseover becomes onMouseOver, and so on:
- Write Inline Styles as Objects in JSX. And lastly, to define inline styles for JSX, you write it as an object, with the properties in camelCase, the values in quotes, and then you pass it inline to the JSX.

```
  const inlineStyle = {
    color: "#2ecc71",
    fontSize: "26px",
  };
  return (
    <>
      <div className="container">
        <p style={inlineStyle}>Hi campers</p>         
      </div>
    </>
  );
```

# Elemento

Los elementos son livianos, sin estado y, por lo tanto, son más rápidos.

# Components


What is the difference between Element and Component ?
https://www.geeksforgeeks.org/what-is-the-difference-between-element-and-component/

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




# React applications

## Create React App

Create React apps with no build configuration.

https://github.com/facebook/create-react-app


Errores
------------------
cannot run yarn create if yarn is in a folder where the file path has a space #6851 
- Obtener el nombre carpeta con caracteres ~
```
dir /x in C:\Users
``` 
- Obtener carpeta cache
```
yarn cache dir
```
- Obtener carpeta bin global
```
 yarn global bin
```
- Establecer estas carpetas con nombres estilo ~. Ej. El nombre usuario "C:\Users\L E N O V O" al ejecutar dir /x in c:\users se obtiene C:\Users\LENOVO~1

```
yarn config set cache-folder "C:\Users\LENOVO~1\AppData\Local\Yarn\Cache\v6"
```

```
yarn config set prefix "C:\Users\LENOVO~1\AppData\Local\Yarn"
```
https://github.com/yarnpkg/yarn/issues/6851

-------------


## Vite

Under the hood, CRA uses Webpack which is a popular asset bundler that helps us develop and build a web app. The Webpack was fine until 2–3 years ago but today, we have almighty Esbuild. 

Vite is another build tool like Webpack

JSX transpilation is also handled via esbuild.


You Should Choose Vite Over CRA for React Apps, Here’s Why
https://medium.com/codex/you-should-choose-vite-over-cra-for-react-apps-heres-why-47e2e7381d13


# Referencias


Pagina dedicada a patrones en react. 
https://reactpatterns.com/

10 React mini-patterns React
https://hackernoon.com/10-react-mini-patterns-c1da92f068c5


This course serves as an introduction to modern web application development with JavaScript. The main focus is on building single page applications with ReactJS that use REST APIs built with Node.js. The course also contains a section on GraphQL, a modern alternative to REST APIs.
https://fullstackopen.com/en/about

Learning React: The Main Concepts
- Posee los conceptos, con ilustraciones. 
https://owlypixel.com/learning-react-main-concepts/


# Varios


Establecer el contenido html  de un div:

dangerouslySetInnerHTML is React’s replacement for using innerHTML in the browser DOM. 
https://reactjs.org/docs/dom-elements.html#dangerouslysetinnerhtml




# Codigo Referencias


starter-kit

Cross-platform web development with Visual Studio Code, C#, F#, JavaScript, ASP.NET Core, EF Core, React (ReactJS), Redux, Babel. Single-page application boilerplate.
https://github.com/kriasoft/aspnet-starter-kit


# Revisiones

