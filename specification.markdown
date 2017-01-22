## Primitives

Number
Integer
Float
String
Lists       [ ] String
Maps        Map<S, T>


## Protocals

```
Arithmetic<T> protocal { 
  * (this left: T, right:T) -> T;
  / (this left: T, right:T) -> T;
}
```

## Implementations

```
Vector2 impl for Arithmetic<Vector2> {
  * (right: Vector2) => Vector2(x * right.x, y * right.y)
  / (right: Vector2) => Vector2(x / right.x, y / right.y)
}
```

## Pattern Matching

a is (t: Type)

match x {
        1      => ...
        2      => ...
  (t: Type)    => ...
  (a: T, b, T) => ...
         _     => ...
}

## Pipes

pipes the result of the function into the next function as the first argument. 

```
a |> b |> c
```

## Ranges

Inclusive...

```
1..10 
```

## Enumerating

Single for statement

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

Override with 

await func().awaiter

...




