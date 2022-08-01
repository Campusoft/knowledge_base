# Hooks


Hooks son funciones que te permiten “enganchar” el estado de React y el ciclo de vida desde componentes funcionales. Los hooks no funcionan dentro de las clases — te permiten usar React sin clases.

useEffect

Cuando llamas a useEffect, le estás diciendo a React que ejecute tu función de “efecto” después de vaciar los cambios en el DOM. Los efectos se declaran dentro del componente para que tengan acceso a sus props y estado. De forma predeterminada, React ejecuta los efectos después de cada renderizado — incluyendo el primer renderizado.

Esto es porque en muchas ocasiones queremos llevar a cabo el mismo efecto secundario sin importar si el componente acaba de montarse o si se ha actualizado
componentDidMount() {    document.title = `You clicked ${this.state.count} times`;  }  componentDidUpdate() {    document.title = `You clicked ${this.state.count} times`;  }

¿Qué hace useEffect? Al usar este Hook, le estamos indicando a React que el componente tiene que hacer algo después de renderizarse. React recordará la función que le hemos pasado (nos referiremos a ella como nuestro “efecto”), y la llamará más tarde después de actualizar el DOM. En este efecto, actualizamos el título del documento, pero también podríamos hacer peticiones de datos o invocar alguna API imperativa.

A veces, queremos reutilizar alguna lógica de estado entre componentes. Tradicionalmente, había dos soluciones populares para este problema: componente de orden superior y render props. Los Hooks personalizados te permiten hacer esto, pero sin agregar más componentes a tu árbol.
Reusing logic in React has been complex, and patterns like HOCs and Render Props tried to solve that problem. With the recent addition of Hooks, reusing logic becomes easier

# useState

# useEffect

# useContext

# useReducer

# useCallback

The React useCallback Hook returns a memoized callback function.

# useMemo

The React useMemo Hook returns a memoized value.

Think of memoization as caching a value so that it does not need to be recalculated.
- Ejemplo de uso de useMemo, con una funcion para calcular un valor.
https://www.w3schools.com/react/react_usememo.asp


# Referencias

¿Por qué deberías usar React Query o SWR?
- Tener hooks que se consume en varios componentes, pero poseen mecanismo cache
- Utilizar Hooks, para tener una especie de servicio para consumir API REST.
- Reintentar Peticiones, Revalidación y Polling
https://www.thisdot.co/blog/por-que-deberias-usar-react-query-o-swr


