using Geometry

Pointer type { 

}

Mouse type : Pointer { }
Pen   type : Pointer { }
Touch type : Pointer { }

Pointer protocol {
  * move ↺ : moving
  * stop   ∎ 
}

Pointer `Pressed : Pointer `Event { 
  geometry: Geometry            // the geometry 
  position : Vector2            // the center of the contact
  pressure : physics::Pressure
  rotation : geometry::Angle
  tilt     : geometry::Ray
}

// Out
// Leave
// Enter
// Over
// Move
// Up
// Cancel


Pointer `Pressure event { 
  pressure : physics::Pressure
  position : Vector2
}

Pointer `Released event {

}


Pointer `Moved event {
  position : Vector2
}


// digitizer