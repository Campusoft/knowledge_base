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

## Propiedades 

## State (Estado)

Difference of Props and State.

- Props are immutable i.e. once set the props cannot be changed, while State is an observable object that is to be used to hold data that may change over time and to control the behavior after each change.
- States can be used in Class Components, Functional components with the use of React \ (useState and other methods) while Props don’t have this limitation.
- While Props are set by the parent component, State is generally updated by event handlers. 



## Contexto

Now, with the help of the Context API, you can create a Context in your App. This Context will help you share your in-app data globally irregardless of the tree structure in your React app.



# Ciclo de Vida


How to use React Lifecycle Methods
- Indica cuales metodos estan deprecated 
- Indica algunos Use Case
https://www.andreasreiterer.at/reactjs-lifecycle-methods/

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

# Event Handler

Should you use arrow functions in React components?
https://sebhastian.com/react-arrow-function/

# Patrones

## Higher-order component

Un componente de orden superior (HOC por las siglas en inglés de higher-order component) 



# React applications

## Create React App

El paquete create-react-app, es un CLI oficial de React. Dicho paquete nos permite crear la estructura base de un proyecto en React con un simple comando.

- Internamente usa Babel y webpack, pero no necesitas saber nada de estas herramientas para usar Create React App.



Create React apps with no build configuration.

https://github.com/facebook/create-react-app


Para ello internamente usa Babel y webpack, pero no necesitas saber nada de estas herramientas para usar Create React App.



**Create new project react typescript based**
npx create-react-app project-name --template typescript
https://medium.com/codex/typescript-and-create-react-app-11bdebcbf763

Nota: El nombre no debe llevar puntos u otros caracteres especiales, en caso de requerir un nombre con esas condiciones renombrar después de crear



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

Adding Custom Environment Variables
https://create-react-app.dev/docs/adding-custom-environment-variables


How to add a custom ESLint configuration to a Create React App project
https://levelup.gitconnected.com/how-to-add-a-custom-eslint-configuration-to-a-create-react-app-project-aea3f7c1d7af

## Vite

Under the hood, CRA uses Webpack which is a popular asset bundler that helps us develop and build a web app. The Webpack was fine until 2–3 years ago but today, we have almighty Esbuild. 

Vite is another build tool like Webpack

JSX transpilation is also handled via esbuild.


You Should Choose Vite Over CRA for React Apps, Here’s Why
https://medium.com/codex/you-should-choose-vite-over-cra-for-react-apps-heres-why-47e2e7381d13


## bundling 

División de código
- import() dinámico
```
import("./math").then(math => {
  console.log(math.add(16, 26));
});
```
- La función React.lazy te deja renderizar un import dinámico como un componente regular.
- El componente lazy debería entonces ser renderizado adentro de un componente Suspense, lo que nos permite mostrar algún contenido predeterminado (como un indicador de carga) mientras estamos esperando a que el componente lazy cargue.
```
import React, { Suspense } from 'react';

const OtherComponent = React.lazy(() => import('./OtherComponent'));

function MyComponent() {
  return (
    <div>
      <Suspense fallback={<div>Loading...</div>}>
        <OtherComponent />
      </Suspense>
    </div>
  );
}
```
https://es.reactjs.org/docs/code-splitting.html


# Referencias


Pagina dedicada a patrones en react. 
https://reactpatterns.com/

(Git de reactpatterns.com)
https://github.com/chantastic/reactpatterns


10 React mini-patterns React
https://hackernoon.com/10-react-mini-patterns-c1da92f068c5


This course serves as an introduction to modern web application development with JavaScript. The main focus is on building single page applications with ReactJS that use REST APIs built with Node.js. The course also contains a section on GraphQL, a modern alternative to REST APIs.
https://fullstackopen.com/en/about

Learning React: The Main Concepts
- Posee los conceptos, con ilustraciones. 
https://owlypixel.com/learning-react-main-concepts/

React Function Components
- React Function Component Example
- React Function Component: props
- React Arrow Function Component
- React Stateless Function Component
- React Function Component: state
- React Function Component: Event Handler
- React Function Component: Callback Function
- React Function Component: Lifecycle
- Pure React Function Component
- React Function Component: Export and Import
- React Function Component: ref
- React Function Component: PropTypes
- React Function Component: TypeScript
- React Function Component vs Class Component
https://www.robinwieruch.de/react-function-component/

# Varios

Establecer el contenido html  de un div:

dangerouslySetInnerHTML is React’s replacement for using innerHTML in the browser DOM. 
https://reactjs.org/docs/dom-elements.html#dangerouslysetinnerhtml

# Best Practices

React Best Practices – Tips for Writing Better React Code in 2022
- Solid Understanding of React. Another root cause for problems React devs have is a poor basic understanding of how React works under the hood. 
https://www.freecodecamp.org/news/best-practices-for-react/



# Traning

[Traning](traning.md)


# Codigo Referencias


starter-kit

Cross-platform web development with Visual Studio Code, C#, F#, JavaScript, ASP.NET Core, EF Core, React (ReactJS), Redux, Babel. Single-page application boilerplate.
https://github.com/kriasoft/aspnet-starter-kit

"The mother of all demo apps" — Exemplary fullstack Medium.com clone powered by React, Angular, Node, Django, and many more medal_sports https://realworld.io/
https://github.com/gothinkster/realworld


React + Vite Example App
- react-query
https://github.com/romansndlr/react-vite-realworld-example-app



# Revisiones

