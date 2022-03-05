# typescript


# RxJS


Clear examples, explanations, and resources for RxJS.
https://www.learnrxjs.io/


***Concepts***

Observable

Subscription

Operators

Operators offer a way to manipulate values from a source, returning an observable of the transformed values.

Pipe
The pipe function is the assembly line from your observable data source through your operators. Just like raw material in a factory goes through a series of stops before it becomes a finished product, source data can pass through a pipe-line of operators where you can manipulate, filter, and transform the data to fit your use case


## Operators

***switchMap***

***tap***

The tap operator returns a new observable which is a mirror copy of the source observable. We use it mostly for debugging purposes ( for example for logging the values of observable as shown below).