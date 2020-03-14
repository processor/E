Cuboid<T: ℝ = f64> struct { 
  polygons: [] Polygon

  let definition = [
      // faces       normals (aka direction)
      [ [ 0, 4, 6, 2 ], [ -1,  0,  0 ] ],       
      [ [ 1, 3, 7, 5 ], [ +1,  0,  0 ] ],
      [ [ 0, 1, 5, 4 ], [  0, -1,  0 ] ],
      [ [ 2, 6, 7, 3 ], [  0, +1,  0 ] ],
      [ [ 0, 2, 3, 1 ], [  0,  0, -1 ] ],
      [ [ 4, 5, 7, 6 ], [  0,  0, +1 ] ]
  ]
      
  // from * Polygon => Cuboid { faces: $0 to Array }

  from (center: Vector3<T>, dimensions: Vector3<T>) => Cuboid(
    from row in definition
    select Polygon(
      from i in row[0]
      select Vertex {
        position: Vector3(
          x: center.x + dimensions.width  * (2 * !!(i & 1) - 1)
          y: center.y + dimensions.height * (2 * !!(i & 2) - 1)
          z: center.z + dimensions.depth  * (2 * !!(i & 4) - 1)
        )
        normal: row[1]
      }
    )
  )
}

// 6 rectangle faces
// 12 edges
// 8 verticies

Cuboid impl for Geometry {
  vertices -> null
  faces    -> null
}

// [0] https://en.wikipedia.org/wiki/Cuboid
// A cuboid is a convex polyhedron bounded by six quadrilateral faces, whose polyhedral graph is the same as that of a cube