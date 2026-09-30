# LoRA

LoRA (*Low-Rank Adaptation*, adaptación de bajo rango) es una técnica de ajuste fino eficiente en parámetros para adaptar modelos preentrenados a una tarea o dominio. En lugar de actualizar todos los pesos del modelo, LoRA los mantiene congelados y agrega matrices pequeñas entrenables en capas seleccionadas. Al terminar, se guarda el adaptador con los cambios aprendidos y se combina con el modelo base durante la inferencia.

## Conceptos

En una capa lineal con pesos `W`, el ajuste completo aprende una matriz de cambios `ΔW`. LoRA aproxima ese cambio mediante el producto de dos matrices de menor rango: `ΔW = B × A`. El rango `r` controla el tamaño y la capacidad de esa actualización. La salida de la capa se calcula conceptualmente como `W x + (α/r) B A x`, donde `α` es un factor de escala en la formulación original.

El modelo base permanece congelado durante el entrenamiento. Solo se optimizan las matrices del adaptador (y, si se configura, otros módulos seleccionados). Esto reduce los parámetros entrenables, el estado del optimizador y el tamaño de los artefactos guardados. La memoria total y el tiempo de entrenamiento no desaparecen: también dependen del tamaño del modelo, longitud de secuencia, lote, precisión, módulos elegidos y motor de entrenamiento.

### LoRA y QLoRA

LoRA entrena adaptadores sobre el modelo base, que puede estar en precisión completa o reducida. QLoRA combina LoRA con un modelo base cuantizado, normalmente a 4 bits, y propaga los gradientes hacia los adaptadores mientras mantiene congelados los pesos cuantizados. El trabajo original de QLoRA describe NF4, cuantización doble y optimizadores paginados como técnicas para reducir el uso de memoria. QLoRA puede hacer viable el ajuste en hardware más limitado, aunque la cuantización y la configuración del entrenamiento pueden afectar rendimiento y calidad.

## Características

- **Eficiencia en parámetros:** se entrena una fracción pequeña de los parámetros del modelo.
- **Adaptadores pequeños:** se puede guardar y distribuir el adaptador sin duplicar los pesos completos del modelo.
- **Modelo base reutilizable:** varios adaptadores pueden corresponder a distintas tareas, sujetos a la compatibilidad con el modelo base y el formato.
- **Despliegue flexible:** el adaptador puede cargarse junto al modelo o fusionarse con sus pesos cuando la arquitectura y las herramientas lo permiten.
- **Menor consumo que el ajuste completo:** suele reducir memoria de entrenamiento y almacenamiento, pero no elimina el consumo del modelo base ni las necesidades de activaciones y contexto.
- **Capacidad configurable:** el rango, las capas objetivo y otros parámetros permiten equilibrar tamaño, recursos y calidad.
- **No añade conocimiento de forma garantizada:** el ajuste puede enseñar patrones y comportamiento a partir de ejemplos, pero no garantiza hechos correctos ni reemplaza una fuente de conocimiento actualizable.

## Casos de uso

- Adaptar un modelo a instrucciones, tono, formato o vocabulario de una organización.
- Clasificar, extraer o transformar información de acuerdo con ejemplos representativos.
- Ajustar la salida para formatos estructurados, como respuestas con campos definidos, si el modelo puede aprenderlos de los datos.
- Especializar un modelo en un dominio con terminología o patrones recurrentes.
- Probar varias especializaciones de un mismo modelo base con adaptadores independientes.
- Ajustar modelos de lenguaje en equipos con memoria de GPU limitada, usando QLoRA cuando sea compatible.

LoRA no suele ser la primera opción para incorporar información que cambia con frecuencia o que debe citarse con precisión. Para esos casos, conviene evaluar recuperación aumentada por generación (RAG), bases de datos o herramientas junto con el modelo. Tampoco garantiza mejoras en razonamiento complejo solo por aumentar los ejemplos de entrenamiento.

## Recomendaciones para realizar un ajuste fino

### 1. Definir el objetivo y establecer una línea base

- Describe la tarea de forma verificable: entrada esperada, respuesta deseada, idioma, formato y criterios de aceptación.
- Prueba primero el modelo base con instrucciones claras y, cuando aplique, con RAG o ejemplos en el prompt. Ajusta solo si existe una brecha persistente que los datos de entrenamiento puedan corregir.
- Conserva un conjunto de evaluación representativo y separado antes de iniciar el entrenamiento. Incluye casos típicos, límites y ejemplos donde el modelo debe abstenerse o reconocer incertidumbre.

### 2. Elegir el modelo base

- Selecciona un modelo compatible con el idioma, licencia, arquitectura, contexto y entorno de despliegue previstos.
- Comprueba los requisitos de uso y redistribución de los pesos y del conjunto de datos.
- Empieza con un modelo instruct si la tarea consiste en seguir instrucciones. Considera un modelo base si necesitas controlar de forma particular el proceso de entrenamiento.
- Confirma que la herramienta de ajuste reconoce la arquitectura y los nombres de sus módulos antes de preparar un experimento grande.

### 3. Preparar los datos

- Usa ejemplos correctos, consistentes y cercanos a las entradas que recibirá el sistema en producción.
- Representa el formato conversacional esperado por el modelo, incluidos los roles y el marcador de fin de turno que indique su plantilla. No mezcles formatos sin una razón y una validación.
- Elimina duplicados, errores, datos irrelevantes y contenido sensible que no deba procesarse. Confirma permisos y procedencia de los datos.
- Evita la fuga entre entrenamiento y evaluación: ejemplos casi idénticos en ambos conjuntos pueden dar una impresión engañosa de calidad.
- Prefiere cobertura y calidad a volumen sin control. Revisa manualmente una muestra y mide si las clases, idiomas y casos límite están representados.
- En tareas conversacionales, incluye respuestas deseables y, cuando sea importante, ejemplos de rechazo, aclaración o abstención apropiados.

### 4. Configurar LoRA

- **Módulos objetivo (`target_modules`):** selecciona módulos compatibles con la arquitectura. Las proyecciones de atención son un punto de partida común; algunas tareas se benefician de incluir más proyecciones o capas MLP. Verifica los nombres reales del modelo y compara configuraciones de forma controlada.
- **Rango (`r`):** empieza con un rango modesto y aumenta solo si la evaluación indica que el adaptador no tiene capacidad suficiente. Un rango mayor aumenta parámetros entrenables y tamaño del adaptador, y no garantiza mejor calidad.
- **Escala (`lora_alpha`):** configúrala junto al rango; la escala efectiva depende de la implementación. Algunas implementaciones ofrecen variantes como RS-LoRA, cuya regla de escala cambia. No traslades valores entre métodos sin revisar la documentación correspondiente.
- **Dropout (`lora_dropout`):** puede ayudar a regularizar cuando el conjunto de datos es pequeño o existe riesgo de sobreajuste. Evalúa su efecto en vez de asumir que un valor concreto siempre es mejor.
- **Sesgo y módulos adicionales:** entrena sesgos o guarda módulos completos solo cuando la tarea y arquitectura lo requieran; esto aumenta el estado que se entrena o se distribuye.
- **QLoRA:** si la memoria es el límite, evalúa cuantización de 4 bits compatible con la arquitectura, NF4 y doble cuantización cuando estén disponibles. Mantén los adaptadores en una precisión adecuada para el entrenamiento. Valida estabilidad, consumo real y calidad en tu hardware.

### 5. Entrenar con control

- Haz primero una prueba pequeña que confirme que los ejemplos se tokenizan, el modelo recibe etiquetas correctas y los parámetros previstos son los únicos entrenables.
- Ajusta longitud máxima, lote y acumulación de gradientes según la memoria disponible. El truncamiento debe preservar las partes importantes de entrada y respuesta.
- Registra configuración, versión del modelo, revisión de pesos, datos, semilla, métricas y cambios realizados para poder reproducir y comparar experimentos.
- Guarda checkpoints y evalúa durante el entrenamiento. Detén el proceso si la pérdida de entrenamiento baja mientras la calidad de validación se estanca o empeora.
- No elijas hiperparámetros solo por una cifra estándar: compara pocas configuraciones cambiando una variable a la vez y con el mismo conjunto de evaluación.

### 6. Evaluar y desplegar

- Compara el adaptador con el modelo base usando exactamente los mismos casos y criterios. Mide calidad de tarea, formato, latencia, consumo de memoria y tasa de errores.
- Revisa ejemplos manualmente, incluidos los casos difíciles, respuestas inventadas, filtración de información y comportamiento fuera del dominio.
- Prueba el adaptador con la misma plantilla de conversación, tokenizador, versión del modelo y parámetros de inferencia previstos para producción.
- Versiona el adaptador junto con la referencia inequívoca al modelo base, configuración y procedencia de datos. Un adaptador no suele ser intercambiable entre modelos o revisiones distintas.
- Decide si mantener el adaptador separado o fusionarlo en el modelo. La fusión simplifica ciertos despliegues, pero reduce la flexibilidad para alternar adaptadores y requiere validar el formato final.
- Supervisa el comportamiento en producción y conserva una forma de volver al modelo base o a una versión anterior del adaptador.

## Señales para revisar el experimento

| Señal | Posible causa | Acción sugerida |
| --- | --- | --- |
| Buen resultado en entrenamiento y malo en validación | Sobreajuste, datos duplicados o evaluación distinta del uso real | Revisar particiones, calidad y diversidad; reducir entrenamiento o regularizar. |
| Respuestas válidas pero formato inconsistente | Plantilla o etiquetas inconsistentes | Unificar ejemplos y marcadores; añadir casos representativos del formato esperado. |
| No mejora frente al modelo base | Objetivo poco definido, datos insuficientes o técnica inadecuada | Revisar la línea base y probar instrucciones, RAG u otra estrategia antes de escalar. |
| Memoria insuficiente | Secuencias largas, lote grande, pesos/activaciones o cuantización no efectiva | Medir el uso real; reducir longitud o lote, acumular gradientes y evaluar QLoRA compatible. |
| Empeora tareas generales | Especialización excesiva o datos estrechos | Ampliar cobertura, reducir pasos y comprobar si se necesita un adaptador por tarea. |

## Referencias

- Hu et al., [LoRA: Low-Rank Adaptation of Large Language Models](https://arxiv.org/abs/2106.09685).
- Dettmers et al., [QLoRA: Efficient Finetuning of Quantized LLMs](https://arxiv.org/abs/2305.14314).
- Hugging Face PEFT, [guía conceptual de LoRA](https://huggingface.co/docs/peft/main/en/conceptual_guides/lora) y [referencia de configuración](https://huggingface.co/docs/peft/main/package_reference/lora).
