from Geometry import *

Pointer protocol { 
  * move ↺ : moving
  * stop   ∎ 


  position: Vector2

  // Events -

  Press event {
    geometry : Geometry            // the geometry 
    position : Vector2            // the center of the contact
    pressure : physics:Pressure
    rotation : geometry:Angle
    tilt     : geometry:Ray
  }

  Move event {
    position : Vector2
    target: Self
  }

  Pressure event {
    amount: Physics::Pressure
    target: Self
  }
  
  Released event { }
}

Mouse class : Pointer { }
Pen   class : Pointer { }
Touch class : Pointer { }

// Out
// Leave
// Enter
// Over
// Move
// Up
// Cancel



// digitizer