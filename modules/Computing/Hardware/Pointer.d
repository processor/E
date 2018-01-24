from Geometry import *

Pointer protocol { 
  * move ↺ : moving
  * stop   ∎ 
}

Mouse class : Pointer { }
Pen   class : Pointer { }
Touch class : Pointer { }

Pointer `Pressed class : Pointer `Event { 
  geometry : Geometry            // the geometry 
  position : Vector2            // the center of the contact
  pressure : physics:Pressure
  rotation : geometry:Angle
  tilt     : geometry:Ray
}

// Out
// Leave
// Enter
// Over
// Move
// Up
// Cancel


Pointer `Pressure event { 
  pressure : physics:Pressure
  position : Vector2
}

Pointer `Released event {

}

Pointer `Moved event {
  position : Vector2
}


// digitizer