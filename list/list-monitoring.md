# Monitoring

# Sentry

Sentry is cross-platform application monitoring, with a focus on error reporting.

Sentry se ha convertido en la elecci�n predeterminada para muchos de nuestros equipos a la hora de reportar errores de frontend. La utilidad de caracter�sticas como la agrupaci�n de errores o poder definir patrones para descartar errores con ciertos par�metros ayuda a gestionar la avalancha de errores que llegan de muchos dispositivos de usuarios finales.

La integraci�n de Sentry en el pipeline de despliegue continuo permite cargar mapas de or�genes para una depuraci�n de errores m�s eficiente. Aunque Sentry se ofrece principalmente como un SaaS, tambi�n valoramos que su c�digo fuente est� disponible p�blicamente y se puede usar gratis en casos de uso m�s peque�os y en instalaciones auto-hospedadas

https://github.com/getsentry/sentry


##  Sentry groups

issues - An issue is a grouping of similar error events. Every error event has a set of characteristics called its fingerprint, which is what Sentry uses to group them. For example, Sentry groups events together when they are triggered by the same part of your code. This grouping of events into issues allows you to see how frequently a problem is happening and how many users it's affecting.