# Ilustraciones

## Propósito

Esta carpeta reúne documentos cortos de apoyo: resúmenes, comparaciones y material visual sobre conceptos de arquitectura y sistemas. Cada documento se complementa con videos, gráficas, animaciones u otros recursos multimedia guardados en local.

A diferencia de las [guías conceptuales](../README.md), que explican paso a paso cómo implementar una solución, estos documentos ofrecen una vista rápida para entender, comparar y recordar un tema.

## Contenido esperado

- Resumen breve del tema, con los puntos esenciales.
- Tabla comparativa o criterios para elegir entre alternativas.
- Enlace al recurso multimedia (video, imagen o animación) almacenado en la carpeta del tema.
- Enlaces internos a documentos del repositorio que desarrollen el tema.
- Referencias y fuente del material, indicando qué parte es contenido original y qué parte es aporte propio.

## Organización

Cada tema se compone de:

- Un documento `tema.md` en esta carpeta.
- Una subcarpeta con el mismo nombre, `tema/`, que contiene los recursos multimedia del documento.

Reglas:

- Nombrar archivos y carpetas en minúsculas, con guiones, de forma que reflejen el tema.
- Guardar los recursos en local; no depender de enlaces externos para el material principal.
- Registrar en Referencias el origen del recurso y, si se conoce, su autor.
- Agregar cada documento nuevo a la tabla de este índice.

## Documentos

| Documento | Descripción | Recursos |
|---|---|---|
| [Algoritmos de balanceo de carga](algoritmos-balanceo-de-carga.md) | Ocho algoritmos para repartir solicitudes entre servidores, con comparación y guía de elección. | [Video](algoritmos-balanceo-de-carga/algoritmos-balanceo-de-carga.mp4) |
| [Escalamiento de bases de datos](escalamiento-de-bases-de-datos.md) | Diez técnicas para escalar una base de datos, con comparación y orden sugerido de aplicación. | [Video](escalamiento-de-bases-de-datos/escalamiento-de-bases-de-datos.mp4) |

## Fuentes de información

Fuentes externas útiles para este tipo de contenido. Evaluar cada una con criterio y contrastar los datos relevantes con la documentación oficial antes de usarlos como referencia técnica.

### AlgoMaster.io

- **Sitio:** <https://algomaster.io/>
- **Qué es:** plataforma de aprendizaje para preparar entrevistas técnicas de ingeniería de software. Ofrece aprendizaje basado en patrones, con visualizaciones y animaciones.
- **Contenido según el sitio:**
  - Patrones y problemas de estructuras de datos y algoritmos, como Two Pointers, Sliding Window, Binary Search, BFS, DFS y programación dinámica.
  - Material de diseño de sistemas, con temas como caching, sharding, replicación, elección de líder, tráfico alto de lectura y escritura, y manejo de fallos.
  - Capítulos con lectura y audio, notas, resaltado de ideas, seguimiento de progreso y un tutor de IA.
  - Práctica de código contra casos de prueba y entrevistas simuladas con un entrevistador de IA.
  - Newsletter, blog y canal de YouTube asociados.
- **Autor:** el sitio identifica como creador a Ashish, autor de AlgoMaster.
- **Modelo de acceso:** el sitio ofrece contenido gratuito y un plan de pago (Premium).
- **Uso en este repositorio:** origen de la lista de conceptos que inspira [Escalamiento de bases de datos](escalamiento-de-bases-de-datos.md). Las lecciones enlazadas en la publicación original no se reproducen aquí.
- **Contacto del sitio:** contact@algomaster.io y un canal de Discord.

Notas de uso:

- El enfoque del sitio es la preparación de entrevistas. Sirve como material introductorio y de repaso; para decisiones de diseño o producción, consultar documentación oficial y documentos del repositorio.
- Parte del contenido puede requerir suscripción de pago.
- Las cifras y comentarios de usuarios que publica el sitio son afirmaciones de la propia plataforma y no se verificaron.
