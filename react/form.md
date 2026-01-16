# Form



React Uncontrolled Elements With “useRef” Hooks
- 
https://medium.com/technofunnel/react-uncontrolled-elements-with-useref-hooks-9c5873476c6f

You May Not Need Controlled Form Components
- You can access HTML name attributes in event handlers. event.currentTarget.nameField.value
https://www.swyx.io/no-controlled-forms

# React Hook Form

React Hook Form is a performant and easy-to-use library that takes advantage of React Hooks to build forms. It works by registering components to a React hook using a provided register method, and provides a handleSubmit method that validates all form data before calling the onSubmit callback that you provide.

Performant, flexible and extensible forms with easy-to-use validation.

react-hook-form (RHF) es una librería para manejo de formularios en React basada en:

- Inputs no controlados (uncontrolled components)
- Uso intensivo de refs
- Minimizar re-renders
- API simple y extensible

Su foco principal es performance + DX (developer experience).


Donde RHF brilla

- Formularios grandes o complejos
- +20 campos
- Validaciones cruzadas
- Secciones dinámicas

Sistemas enterprise

- Backoffice
- Finanzas
- Educación / elecciones
- Flujos multi-step

Integración con UI libraries

- MUI
- Ant Design
- shadcn/ui
- Syncfusion (como en tus proyectos)

Formularios dinámicos

- useFieldArray
- Campos que aparecen/desaparecen



https://react-hook-form.com/



Schema Validation

We also support schema-based form validation with Yup, Zod , Superstruct & Joi, where you can pass your schema to useForm as an optional config. It will validate your input data against the schema and return with either errors or a valid result.


| Aspecto              | React-Hook-Form                                   | TanStack Form                                                                  |
| -------------------- | ------------------------------------------------- | ------------------------------------------------------------------------------ |
| Filosofía            | Hooks + uncontrolled inputs                       | Headless, reactivo, framework-agnóstico                                        |
| UI                   | Tú decides, integra con `register` o `Controller` | Headless, tú conectas UI a la lógica                                           |
| TypeScript           | Excelente inferencia                              | **Inferencia aún más profunda** y completa en campos complejos ([TanStack][1]) |
| Frameworks           | **Solo React**                                    | **Multiples (React, Vue, Angular, etc.)** ([TanStack][2])                      |
| Validación           | Inline o vía schema (Zod, Yup, etc)               | Schema + validación síncrona/asíncrona integrada ([TanStack][2])               |
| Performance          | Muy alta                                          | Muy alta con enfoque reactivo local ([LogRocket Blog][3])                      |
| Soporte de comunidad | **Amplio y maduro**                               | **Más nuevo pero creciendo** ([LogRocket Blog][3])                             |

[1]: https://tanstack.com/form/latest/docs/comparison?utm_source=chatgpt.com "Comparison | TanStack Form | TanStack Form Docs"
[2]: https://tanstack.com/form?utm_source=chatgpt.com "TanStack Form"
[3]: https://blog.logrocket.com/tanstack-form-vs-react-hook-form/?utm_source=chatgpt.com "TanStack Form vs. React Hook Form - LogRocket Blog"

# TanStack Form       


# Formik


Formik is a popular form building solution because it provides you a reusable form component where you can simply use its API for handling the three most annoying part in form building:

 -   Getting values in and out of form state
 -   Validation and error messages
 -   Handling form submission
	
https://formik.org/ 


# Redux Form

The best way to manage your form state in Redux.
https://redux-form.com/


#  formsy-react

A form input builder and validator for React.

https://github.com/formsy/formsy-react/

https://github.com/christianalfoni/formsy-react