# Sequences

A an immutable sequence of expressions.

## Literals

```
0..5..10  start, step, end  # { 0, 5, 10 }

{ 1, 2, 3 }                 # Array<Number> { 1, 2, 3 }

```

## Dimensions

```
[ [ 1, 2, 3 ],
  [ 3, 4, 6 ],
  [ 7, 8, 9 ]
]
```

## Functions

```

.head
.tail

{ 1, 2, 3 } + { 1 }          -> { 1, 2, 3, 4 }
{ 0 } + { 1, 2, 3 }          -> { 0, 1, 2, 3 }
{ 1, 2, 3 }.excluding({ 3 }) -> { 1, 2 }

{ 1, 2 } ∪ { 1, 3} -> { 1 } 
```

## Conversions
