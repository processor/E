Arc<T: Numeric & Blittable = Float64> struct { 
  x            : T            // circle center x
  y            : T            // circle center y
  x `Radius    : T            // circle radius x
  y `Radius    : T            // circle radius y
  start `Angle : T<Angle>
  end   `Angle : T<Angle>
  clockwise    : Boolean
}

Curve impl for Arc {
  getPoint(t: Number) {
    var deltaAngle = endAngle - startAngle

    let samePoints = abs(deltaAngle) < T.epsilon

    // ensures that deltaAngle is 0 .. 2 π
    while deltaAngle < 0     { deltaAngle += π * 2 }
    while deltaAngle > π * 2 { deltaAngle -= π * 2 }
    
    if deltaAngle < Number.elipson {
      deltaAngle = samePoints ? 0 : π * 2
    }

    if clockwise && !samePoints {
      deltaAngle = (deltaAngle == (π * 2)) ? - (π * 2) : deltaAngle - (π * 2)
    }

    let angle = startAngle + t * deltaAngle

    return Vector3(
      x: x + xRadius * cos(angle)
      y: y + yRadius * sin(angle),
      z: 0
    )
  }
}

// closed segment of a differentiable curve

// three.js
