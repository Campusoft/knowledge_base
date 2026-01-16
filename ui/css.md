# CSS

# Selectors

Selectors are patterns used to select elements to style

- Id selector (#id): uses the id attribute defined for an element
Example:
"#firstname": this selects the element with id="firstname"

- Class selector (.class): Selects all elements with both name1 and name2 set within its class attribute

- Element selector: Select elements by tag
Example:
p: Selects all <p> elements

- Element by parent: Select all elements with a comun parent
Example:
div > p: Selects all <p> elements where the parent is a <div> element


# Override style
- Cascading order
- Inheritance
- Internal priorities
- !Important


Reference:
- https://www.w3docs.com/snippets/css/how-to-override-css-styles.html


# flex 

A Complete Guide to Flexbox 
- Properties for the Parent (flex container)
  - display
  - flex-direction
  - flex-wrap
- Properties for the Children (flex items)
  - order
  - flex-grow
https://css-tricks.com/snippets/css/a-guide-to-flexbox/#background

Conceptos Básicos de flexbox
https://developer.mozilla.org/es/docs/Web/CSS/CSS_Flexible_Box_Layout/Basic_Concepts_of_Flexbox


# Gradients 

## Radial  Gradients 

A Deep CSS Dive Into Radial And Conic Gradients
- How Does A Radial Gradient Work?. Existe ilustraciones para mejor entendimiento
- Conic Gradient
- Use Cases For Conic Gradients. Varios ejemplos
https://www.smashingmagazine.com/2022/01/css-radial-conic-gradient/


CSS Gradient is a happy little website and free tool that lets you create a gradient background for websites
https://cssgradient.io/

# CSS Preprocesor

## Sass

Sass is a CSS extension language, Sass allows to write code fro generate CSS.

- https://sass-lang.com/guide



# CSS Framework

## Bootstrap

## Tailwind CSS (tailwindcss)


The most popular component library component library for Tailwind CSS


¿Qué es Utility-first?
- Tradicionalmente, para dar estilo a un elemento, creamos una clase con un nombre (como .card-profile) y le asignamos muchas propiedades en un archivo CSS aparte.

En Tailwind, no inventas nombres de clases. En su lugar, utilizas pequeñas clases predefinidas directamente en tu HTML para construir el diseño.

Clases de Utilidad
- Una clase de utilidad es una clase CSS que hace una sola cosa. Es como tener piezas de LEGO: cada una tiene una forma y color, y tú las unes para crear algo complejo.

 
Ventaja | Descripción
-- | --
Adiós al "CSS Fatigue" | No pierdes tiempo pensando en nombres como .container-inner-wrapper-final.
Seguridad de cambio | Como las clases están en el HTML, si borras un elemento, los estilos se van con él. No dejas "código muerto" en archivos CSS.
Escalabilidad | Tu archivo CSS final es muy pequeño porque Tailwind solo exporta las clases que realmente usas.
Consistencia | Usas un sistema de diseño predefinido (colores, sombras y espacios exactos).

 


```
pnpm add tailwindcss @tailwindcss/vite
```
 

daisyUI adds component class names to Tailwind CSS so you can make beautiful websites faster than ever.
https://daisyui.com/


Tailwind CSS utiliza POST-CSS 


Headless UI

Completely unstyled, fully accessible UI components, designed to integrate beautifully with Tailwind CSS.
https://github.com/tailwindlabs/headlessui

## Bulma


# Estadisticas

State of CSS
The annual developer survey about the latest trends in CSS
https://stateofcss.com/en-US
