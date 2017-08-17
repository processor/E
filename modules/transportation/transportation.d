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

Route protocol {
  * Departure
  * (Maneuver, Navigable) ↺ |
  * Arrival ∎ : arrived
}
