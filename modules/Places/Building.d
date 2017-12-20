Building protocol {
  geometry : [ Geometry ]
  units    : [ Unit ]
}

Building class : Place {
  lot		      	: Land `Lot
  jurisdiction	: Entity
}

Building `Unit record {
  building : Building 
  name     : String
}

Apartment : Building `Unit { }
Basement  : Building `Unit { }
Floor     : Building `Unit { }
Suite     : Building `Unit { }
Room      : Building `Unit { }
Lobby	    : Building `Unit { }

Land `Lot protocol {
  zone      : [ Building` Zone ]
  geometry  : [ Geometry ]
  buildings : [ Building ]
}

Building `Permit record {
  code   : String
  lot    : Lot
}

Building `Zone record {
  code   : String
  bounds : Polygon
}