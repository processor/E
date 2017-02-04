# Types

### Declarations

```
Point type { 
   x, y, z: Number
}

```

### Implementations

A type may implement any number of protocals.

```

From<Vector3> impl for Point {
   from (vector: Vector3) => Point(vector.x, vector.y, vector.z)
}


Point.from(Vector3(1,2,3))

 
```


### Annotations

*@size*(Number) Defines the size of the object.

