﻿Place record {
  name   : String
  bounds : ?Polygon
}

  Universe
, Galaxy
, Planet,
, Continent : Place

// --- Countries --- 
  Country : Place

// --- First Order --- 
  Division
, Emirate                    
, Empire                     
, FederalDistrict            
, Kingdom                    
, Municipality               
, Prefecture                 
, Province                   
, Region                     
, Special`Administrative`Region
, State                      
, Subdistrict                
, Territory : Place

// ---  Second Order (Admistrive areas) --- 
  Borough     
, County      
, Department  
, District    
, Parish : Place

// ---  Localities --- 
  City
, Shire
, Town
, Township
, Village
, Tribe
, Voivodship : Place, Inhabitable 

// Neighborhood
Neighborhood : Place, Inhabitable 

Inhabitable protocol {
  inhabitants -> [ Inhabitant ]
}

Inhabitant : Entity {
 place   : Place
}

State record : Place {
  code: ISO:"3166-2"
}

Providence record : Place {
  code: string
}

Country record : Place {
  code: ISO:3166-2,
}

Park record : Place