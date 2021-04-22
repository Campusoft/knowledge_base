# Event-Driven
 
Los eventos son sucesos o cambios considerables en el estado del hardware o el software de un sistema. Un evento y su notificación no son lo mismo: la segunda es un mensaje que el sistema envía para comunicar a otra parte del sistema que se produjo cierto evento.

Esta arquitectura está compuesta por consumidores y productores de eventos. El productor detecta los eventos y los representa como mensajes. No conoce al consumidor del evento ni el resultado que generará este último. 

Una vez que se detecta un evento, este se transmite del productor a los consumidores a través de canales de eventos, donde se procesa de manera asíncrona con una plataforma para este fin. Ni bien se produce un evento, se debe informar a los consumidores, quienes podrían procesarlo o simplemente verse afectados por él. 