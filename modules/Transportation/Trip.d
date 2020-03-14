Trip protocol { 
  * depart : departed
  * arrive : arrived
  * cancel : canceled
}


Trip process {
  origin      : Origin
  destination : Place
  fare        : Fare
  route       : Route
  vehicle     : Vechile // Plane, Bus, Boat, ...

  
  Departure record { } 

  Arrival record { }
}