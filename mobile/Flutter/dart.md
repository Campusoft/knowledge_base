# Dart



# types

The Dart language has special support for the following:

- Numbers (int, double)
- Strings (String)
- Booleans (bool)
- Lists (List, also known as arrays)
- Sets (Set)
- Maps (Map)
- Runes (Runes; often replaced by the characters API)
- Symbols (Symbol)
- The value null (Null)


Dart supports generic types, like List<int> (a list of integers) or List<Object> (a list of objects of any type).

## late

In Dart, we use the late keyword to declare variables that will be initialized later. These are called non-nullable variables as they are initialized after the declaration. Hence, we use the late keyword.


## Maps

In general, a map is an object that associates keys and values. Both keys and values can be any type of object. 

```


var gifts = Map<String, String>();
gifts['first'] = 'partridge';
gifts['second'] = 'turtledoves';
gifts['fifth'] = 'golden rings';

var nobleGases = Map<int, String>();
nobleGases[2] = 'helium';
nobleGases[10] = 'neon';
nobleGases[18] = 'argon';

```

# operator

- equality 	==    !=   
- logical AND 	&&
- logical OR 	||

spread operator (...)
null-aware spread operator (...?)


# functions

# Class 

Dart is an object-oriented language with classes and mixin-based inheritance. Every object is an instance of a class, and all classes except Null descend from Object. Mixin-based inheritance means that although every class (except for the top class, Object?) has exactly one superclass, a class body can be reused in multiple class hierarchies. Extension methods are a way to add functionality to a class without changing the class or creating a subclass.

Using constructors

You can create an object using a constructor. Constructor names can be either ClassName or ClassName.identifier.

Initializing formal parameters

```
class Point {
  final double x;
  final double y;

  // Sets the x and y instance variables
  // before the constructor body runs.
  Point(this.x, this.y);
}
```
- Constructors aren’t inherited
- 

https://dart.dev/guides/language/language-tour#classes


# Generics

# Asynchronous 

## Future 

Future is like Promise in JS or Task in c#. They are the representation of an asynchronous request. Futures have one and only one response. A common usage of Future is to handle HTTP calls. What you can listen to on a Future is its state. Whether it's done, finished with success, or had an error. But that's it.

## Stream

Stream on the other hand is like async Iterator in JS. This can be assimilated to a value that can change over time. It usually is the representation of web-sockets or events (such as clicks). By listening to a Stream you'll get each new value and also if the Stream had an error or completed.
 
 
Stream provides the means to receive a sequence of events. Each event is either a data event, referred to as an element of the Stream, or an error event, a notification that something failed. When a Stream has emitted all its events, a single "done" event will notify the listener that they reached the end. 

async* means asynchronous generation function, it generates async data. In an async* function, the keyword yield pushes the data (integer) through the Stream river. Every time we yield something in an async* function, we send that data to the Stream.


dart:async provides an object called StreamController. StreamControllers are manager objects that instantiate both a stream and a sink. A sink is the opposite of a stream. You add data to the StreamController using Sink and listen with the Stream.


 