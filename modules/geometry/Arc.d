Arc<T: ℝ = f64> struct : Curve { 
  x            : T            // circle center x
  y            : T            // circle center y
  x `Radius    : T            // circle radius x
  y `Radius    : T            // circle radius y
  start `Angle : T of Degree
  end   `Angle : T of Degree
  clockwise    : Boolean

  getPoint(t: T) -> (x: T, y: T) {
    var deltaAngle = endAngle - startAngle

    let samePoints = abs(deltaAngle) < T.epsilon

    // ensures that deltaAngle is 0 .. 2 π
    while deltaAngle < 0     { deltaAngle += π * 2 }
    while deltaAngle > π * 2 { deltaAngle -= π * 2 }
    
    if deltaAngle < T.epsilon {
      deltaAngle = samePoints ? 0 : π * 2
    }

    if clockwise && !samePoints {
      deltaAngle = (deltaAngle == (π * 2)) ? - (π * 2) : deltaAngle - (π * 2)
    }

    let angle = startAngle + t * deltaAngle

    return (
      x: x + xRadius * cos(angle)
      y: y + yRadius * sin(angle)
    )
  }
}

// closed segment of a differentiable curve