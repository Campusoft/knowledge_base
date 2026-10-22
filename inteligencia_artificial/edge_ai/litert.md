# LiteRT

## Descripción

**LiteRT** es el runtime de Google para ejecutar modelos de aprendizaje automático e inteligencia artificial generativa directamente en dispositivos (*on-device*) y plataformas de borde (*edge*). Es la evolución de TensorFlow Lite y está orientado a llevar modelos entrenados a móviles, equipos de escritorio, web y sistemas embebidos compatibles.

Al ejecutar la inferencia localmente, una aplicación puede procesar datos sin enviarlos necesariamente a un servicio remoto. LiteRT se encarga de cargar el modelo, preparar las entradas y salidas y ejecutar la inferencia en el hardware disponible.

LiteRT es el runtime de inferencia. La preparación o conversión de un modelo depende del origen del modelo y de las herramientas compatibles con el flujo de trabajo elegido. Para flujos de modelos generativos también existe **LiteRT-LM**, un componente relacionado que proporciona capacidades específicas para ejecutar modelos de lenguaje.

## Características

- **Inferencia local:** permite ejecutar modelos cerca de donde se generan los datos, incluso en escenarios con conectividad limitada o intermitente.
- **Varias clases de modelos:** cubre cargas de trabajo de ML tradicional y, según el modelo y la plataforma, de IA generativa.
- **Aceleración de hardware:** puede utilizar CPU, GPU y, en dispositivos compatibles, NPU mediante las interfaces y aceleradores disponibles para cada plataforma.
- **Selección de acelerador y ejecución optimizada:** sus APIs modernas ofrecen opciones para configurar el acelerador, preparar buffers de entrada y salida y ejecutar la inferencia de forma eficiente.
- **Despliegue multiplataforma:** la documentación contempla flujos para Android, escritorio y web; el soporte concreto y las APIs varían según el destino.
- **Modelos compactos:** cuantización y otras técnicas de optimización pueden reducir el tamaño del modelo y el costo de inferencia, con posibles efectos sobre la precisión.
- **Privacidad y respuesta local:** los datos pueden permanecer en el dispositivo y la inferencia no depende de una ida y vuelta a la nube.

## Casos de uso

- **Visión por computadora móvil:** clasificación de imágenes, detección de objetos, lectura de texto y segmentación en cámaras o galerías.
- **Audio en el dispositivo:** reconocimiento de palabras clave, clasificación de sonidos y procesamiento de voz cuando existe un modelo compatible.
- **Experiencias con baja latencia:** filtros de cámara, asistencia contextual y otras funciones que deben responder sin esperar a un servidor.
- **Aplicaciones sin conexión constante:** análisis en campo, dispositivos industriales, quioscos o aplicaciones móviles que deben seguir funcionando con conectividad limitada.
- **Privacidad local:** inferencia sobre imágenes, audio u otros datos sensibles que conviene procesar en el propio dispositivo.
- **IA generativa en el borde:** ejecución local de modelos generativos compatibles, teniendo en cuenta los requisitos mayores de memoria y capacidad de cómputo.
- **Microcontroladores:** variantes específicas como LiteRT for Microcontrollers se dirigen a dispositivos con recursos muy limitados y tienen un conjunto de capacidades más reducido.

## Limitaciones y consideraciones

- **Compatibilidad del modelo:** no todos los modelos ni todas sus operaciones están disponibles en cada runtime o backend. Se debe comprobar que las operaciones del modelo sean compatibles con el destino y el acelerador.
- **Aceleradores dependientes del dispositivo:** la disponibilidad de GPU o NPU, sus controladores y las operaciones aceleradas varían entre fabricantes, modelos de dispositivo y sistemas operativos. Algunas operaciones pueden ejecutarse en CPU o requerir cambios en el modelo.
- **Recursos limitados:** memoria, almacenamiento, temperatura, consumo de batería y capacidad de cómputo del dispositivo restringen el tamaño del modelo y la velocidad de inferencia. Los modelos generativos suelen exigir más recursos.
- **Compromisos de optimización:** cuantizar o reducir un modelo puede disminuir su tamaño y latencia, pero también afectar su precisión o calidad. Hay que medir el resultado con datos representativos.
- **Trabajo adicional de integración:** la aplicación debe gestionar la preparación de datos, los formatos y dimensiones de tensores, los buffers, errores de carga, ciclo de vida del modelo y diferencias entre plataformas.
- **Variabilidad de rendimiento:** los resultados medidos en un dispositivo no garantizan el mismo rendimiento en otros. Conviene probar los dispositivos objetivo y comparar CPU, GPU y NPU cuando estén disponibles.
- **No sustituye todos los servicios de IA en la nube:** modelos muy grandes, actualizaciones centrales frecuentes, cómputo intensivo o procesamiento que requiera datos compartidos pueden ajustarse mejor a un servicio remoto o a una arquitectura híbrida.
- **Microcontroladores con capacidades reducidas:** LiteRT for Microcontrollers admite un conjunto limitado de operaciones y hardware, requiere gestión de memoria de bajo nivel y no proporciona entrenamiento del modelo en el dispositivo.

## Cuándo considerarlo

LiteRT resulta apropiado cuando la aplicación necesita inferencia local, baja latencia, funcionamiento sin conexión o procesamiento privado, y el modelo cabe en los recursos de los dispositivos objetivo. Si el modelo requiere más capacidad de la disponible o necesita actualizarse y ejecutarse centralmente, puede convenir una solución en la nube o híbrida.

Antes de elegirlo, valida la compatibilidad del modelo, el soporte de los aceleradores en los dispositivos objetivo, el consumo de memoria y batería, la latencia y la calidad del modelo optimizado.

## Referencias

- [Descripción general de LiteRT](https://developers.google.com/edge/litert/overview)
- [LiteRT para Android](https://developers.google.com/edge/litert/android)
- [Descripción general de LiteRT para IA generativa](https://developers.google.com/edge/litert/genai/overview)
- [Diseño y compatibilidad de modelos](https://developers.google.com/edge/litert/conversion/tensorflow/build/overview)
- [LiteRT for Microcontrollers](https://developers.google.com/edge/litert/microcontrollers/overview)
