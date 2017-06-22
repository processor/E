Building : Place { 
  lot		      	: Land'Lot,
  jurisdiction	: Entity
}

Building protocol { 
  geometry -> [ ] Geometry
  units    -> [ ] Unit
}

  Apartment
, Basement,
, Floor,
, Suite
, Room,
, Lobby	
: Building'Unit record {
  building : Building 
  name     : String
}

Land `Lot protocol { 
  zone      -> [ ] Building'Zone
  geometry  -> [ ] Geometry
  buildings -> [ ] Building
}

Building `Permit record { 
  code   : String
  bounds : Polygon
}

Building `Zone record {
  code   : String
  bounds : Polygon
}