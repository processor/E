Cuboid<T: Numeric & Blittable = Float64> struct { 
  polygons: [ Polygon ]

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

  from (center: Point, dimensions: Box) => Cuboid(
    from row in definition
    select Polygon(
      from i in row[0]
      select Vertex {
        position: Point(
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