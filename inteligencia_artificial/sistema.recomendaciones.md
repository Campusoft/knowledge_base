# Reglas heurísticas


Las reglas heurísticas son métodos o principios basados en la experiencia y el sentido común que se utilizan para resolver problemas, tomar decisiones o realizar cálculos de manera rápida y práctica. Estas reglas no garantizan una solución óptima o perfecta, pero suelen ser lo suficientemente buenas para situaciones donde una solución exacta no es necesaria o sería demasiado costosa en tiempo o recursos.


Ejemplo de Reglas Heurísticas:
Imagina que estás implementando un sistema de recomendaciones para un sitio de comercio electrónico. Usar reglas heurísticas significa definir estrategias simples basadas en patrones comunes, como:

- Productos Más Populares:

Regla: Mostrar los productos más comprados o vistos por todos los usuarios.
Ejemplo: "Recomendamos los artículos más vendidos esta semana."

-  Productos Recientemente Vistos:

Regla: Mostrar al usuario productos que ha visto recientemente.
Ejemplo: "Basado en su historial, estos productos podrían interesarle."

-  Categorías Relacionadas:

Regla: Si el usuario está explorando una categoría, sugerir los productos más populares dentro de esa categoría.
Ejemplo: "Está viendo laptops; recomendamos estas laptops más populares."

- Productos Similares:

Regla: Si un usuario mira un producto, recomendar otros productos en la misma categoría o con características similares.
Ejemplo: "Otros clientes también compraron..."


# Filtrado Basado en Contenido


Utiliza características de los productos (como categoría, marca, descripciones, etc.) para recomendar productos similares a los que el usuario ha visto.



Métodos:
- Representar productos mediante vectores de características.
- Utilizar similitud de coseno, distancia Euclidiana, o algoritmos más avanzados como TF-IDF si trabajas con texto.


# Enfoque Basado en Filtrado Colaborativo


Utiliza datos históricos de interacciones de los usuarios con los productos para recomendar productos que usuarios similares han visto o comprado.



- SVD (Singular Value Decomposition) 
- Factorization Machines.
 