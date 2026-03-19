# Hangfire


# Quartz.NET


Quartz.NET opera bajo un modelo de tres pilares fundamentales que separan la lógica de qué se hace de cuándo se hace:

- Job (El "Qué"): Es una clase que implementa IJob. Contiene la lógica de negocio que quieres ejecutar. Es totalmente desacoplada del tiempo.
- Trigger (El "Cuándo"): Define el horario. Puede ser un simple intervalo (cada 5 min) o una expresión Cron compleja (el segundo lunes de cada mes a las 10 AM).
- Scheduler (El Orquestador): Es el motor que asocia Jobs con Triggers. Se encarga de disparar el Job correcto cuando el Trigger lo indica.


# Cron Expressions

Una expresión Cron es una cadena de texto que representa un horario. En el ecosistema .NET (especialmente con Quartz o Hangfire), se suele usar el formato de 5 o 6 campos.

Anatomía de una Cron Expression:

* * * * * * (segundos) (minutos) (horas) (día del mes) (mes) (día de la semana)


 
Expresión | Significado
-- | --
0 0 * * * | Todos los días a medianoche.
0 15 10 * * ? | Todos los días a las 10:15 AM.
0 0/5 14 * * ? | Cada 5 minutos, entre las 2:00 PM y las 2:55 PM.
0 0 12 ? * MON-FRI | Todos los días laborables a mediodía.

 
# PeriodicTimer

- Introducido en .NET 6, es la respuesta moderna y eficiente a los problemas de los antiguos timers (System.Timers.Timer o System.Threading.Timer).

 Ventajas 
- Evita la "Reentrada" (Overlapping): En los timers viejos, si tu tarea tardaba 40 segundos pero el timer era de 30, se disparaba una segunda ejecución mientras la primera seguía corriendo. Con PeriodicTimer, el siguiente intervalo no empieza a contarse hasta que tú vuelves a llamar a WaitForNextTickAsync. La ejecución es secuencial por diseño. 

- Basado en async/await: Está diseñado desde cero para ser asíncrono. No hay necesidad de lidiar con delegados o eventos complejos.

- Memoria eficiente: Es extremadamente ligero y no genera "garbage collection" excesivo.

- Limpieza fácil: Implementa IDisposable, lo que facilita la limpieza de recursos.
