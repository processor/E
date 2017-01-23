## Constants

- Defined with the let operator
- Immutable

```
let a = 1  // immutable
var b = 2  // mutable
```

## Variables

- Defined with the var operator
- Mutiple variables may be defined at once using a comma.
```
var a = 1
var x = 1, y = 2, z = 3


a = a + 1
```

## Type Annotations

By default, types are infered by the value defined on the right hand site of a constant or variable declaration.

They may be explictly specified using a type annotation.

 ```
 
 var a: Integer = 1
 
 ```

## Literals

* Numbers   1, 1.1         DEC64 Encoded
* Integer   1i             Integer
* Float     1f             Float64
* String    "hello"        UTF8 Encoded
* Lists     [ 1, 2, 3 ]    List<Number>
* Maps      { a: 1, b: 2 } Map<string, Number>
* Tuples    (1, 2, 3)      Tuple<Number, Number, Number>
 
## Protocals

A protocal defines a set of operations that may be implemented by a Type.

```
Arithmetic<T> protocal { 
  * (this left: T, right:T) -> T;
  / (this left: T, right:T) -> T;
}
```

## Implementations

Any type may implement one or more protocals.

Unlike a class, implementations are defined in isolation.

```
Vector2 impl for Arithmetic<Vector2> {
  * (right: Vector2) => Vector2(x * right.x, y * right.y)
  / (right: Vector2) => Vector2(x / right.x, y / right.y)
}
```

## Pattern Matching

- Constants
- Shapes

a is (t: Type)

match x {
  1            => ...
  2            => ...
  (t: Type)    => ...
  (a: T, b, T) => ...
  _            => ...
}

## Pipes

pipes the result of the function into the next function as the first argument. 

```
a |> b("extra") |> c
```

## Ranges

Ranges are inclusive.

```
1..10 
```

## Loops

All enumerable objects may be looped through using a for operator.

```
for x in list {
  log(x)
}

- automatically bound to $0 if an argument is not specified

for list { 
  log($0)
}

```

## Deconstruction

```
let (a, b) = func()
```

## Multithreading
```

Async by default.

Automatically yields thread when calling an async function and registers a continuation.

todo: overriding...

...




