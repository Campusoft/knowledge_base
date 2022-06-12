# Monitoring

# Sentry

Sentry is cross-platform application monitoring, with a focus on error reporting.

Sentry se ha convertido en la elección predeterminada para muchos de nuestros equipos a la hora de reportar errores de frontend. La utilidad de características como la agrupación de errores o poder definir patrones para descartar errores con ciertos parámetros ayuda a gestionar la avalancha de errores que llegan de muchos dispositivos de usuarios finales.

La integración de Sentry en el pipeline de despliegue continuo permite cargar mapas de orígenes para una depuración de errores más eficiente. Aunque Sentry se ofrece principalmente como un SaaS, también valoramos que su código fuente está disponible públicamente y se puede usar gratis en casos de uso más pequeños y en instalaciones auto-hospedadas

https://github.com/getsentry/sentry


##  Sentry groups

issues - An issue is a grouping of similar error events. Every error event has a set of characteristics called its fingerprint, which is what Sentry uses to group them. For example, Sentry groups events together when they are triggered by the same part of your code. This grouping of events into issues allows you to see how frequently a problem is happening and how many users it's affecting.