  Road
, Waterway,
, Airspace record : Navigable

  Street
, Avenue
, Boulvard : Road

  Departure
, Arrival event {
  place: Place
}

Route protocal {
  * Departure
  * (Maneuver, Navigable) ↺ |
  * Arrival ∎
}
