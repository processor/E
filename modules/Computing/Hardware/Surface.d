Surface protocol {
  * touch ↺ : touching
  * stop  ∎ 
}

Touch protocol {
  * attach     : observing
  * | move     : moving
    | touch    : touching
    | pressure : pressuring    // pressure change
    ↺
  * release ∎

  move ([ Point ])

  press (point: Point, force: Force) -> Press
}

// pinch
// drag

Touching protocol {
  * ? pinch ↺ : pinching
  * ? drag  ↺ : dragging
  * release ∎ 
}

while touching, read touch into touches {
  pinching = (touches.count == 0 || pinching) && count touch.points == 2  

  if pinching && let pinch = Pinch(touch) {
    emit pinch
  }
}

  
Pinch protocol : Touch {
  let distance => √( ($0.x - $1.x) * ($0.y - $1.y])) + 
                  √( ($0.x - $1.x) * ($0.y - $1.y]))
}