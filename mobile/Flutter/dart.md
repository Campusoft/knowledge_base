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
