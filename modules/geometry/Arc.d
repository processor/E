Arc type { 
  x                : Float            // circle center x
  y                : Float            // circle center y
  x`Radius         : Float            // circle radius x
  y`Radius         : Float            // circle radius y
  start`Angle      : Float<Angle>
  end`Angle        : Float<Angle>
  clockwise        : Boolean
}

Curve impl for Arc {
  getPoint(t: Float) {
    var deltaAngle = endAngle - startAngle

    let samePoints = abs(deltaAngle) < T.epsilon

    // ensures that deltaAngle is 0 .. 2 π
    while deltaAngle < 0     { deltaAngle += π * 2 }
    while deltaAngle > π * 2 { deltaAngle -= π * 2 }
    
    if deltaAngle < Float.elipson {
      deltaAngle = samePoints ? 0 : π * 2
    }

    if clockwise && !samePoints {
      deltaAngle = (deltaAngle == (π * 2)) ? - (π * 2) : deltaAngle - (π * 2)
    }

    let angle = startAngle + t * deltaAngle

    return Vector3 {
      x: x + xRadius * cos(angle)
      y: y + yRadius * sin(angle),
      z: 0
    }
  }
}

// closed segment of a differentiable curve

// three.js
