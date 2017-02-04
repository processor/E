# Types

By default, all types are immutable structured -- passed by reference.

TODO: Transfer ownership vs borrow. Define who's responsible for disposal.

### Declarations

```
Point type @size(24) { 
   x, y, z: Number
   
   from (vector: Vector3) => Point { x: vector.x, y: vector.y, z: vector.z }   
}

Bank type {
  id   : Integer,
  name : String
}

```

### Implementations

A type may implement any number of protocals.

```

Geometry impl for Point {
   center => Point(vector.x, vector.y, vector.z)
}



 
```


### Annotations

*@size*(Number) Defines the size of the object.

