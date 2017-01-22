, Natural_Science   // the study of natural phenomena (including fundamental forces and biological life)
, Formal _Science   // the study of mathematics and logic, which use an a priori, as opposed to factual, methodology)
, Social _Science   // the study of human behavior and societies[1]
, Applied_Science   // apply existing scientific knowledge to develop more practical applications (like healthcare, technology or inventions).
, Informal_Science  // ?
, Humanity           // things produced for human experience
: Domain type { 

}


Pysical_Science : Natural_Science;

, // Base Libraries
, Base                   // Types, Records, Tuples, Arrays, Implementations
, Collections            
, Graphics               
, Imaging                
, Multimedia             // Mime, ...
, Text                   // Typeface, ...
, IO                     
: Library

, // Formal, Physical, & Life 
, Chemistry   : Pysical_Science 
, Biology     : Natural_Science   
, Mathematics : Formal _Science
, Physics     : Natural_Science

, // Social (Business, being, law, history, etc)
, Antropology           
, Archaeology           
, Business              
, Communication         
, Criminology           
, Economics             
, Education             
, Geography             
, Government            
, History               
, Philosophy            
, Politics               // Political science
, Linguistics           
, Sociology             
, Law                   
: Social_Science; 

, // Applied
, Agriculture             // Crops & livestock, includes Agrochemistry
, Architechure          
, Astronautics‎         
, Bioengineering        
, Computing               // Computer science
, Cryptography            // RSA, ..
, Engineering           
, Information             // Information sciences
, Mechanics             
, Medicine              
, Metrology‎            
, Psychology            
, Robotics              
, Transportation      
: Applied_Science  




// Disipline          // e.g. neurology
// Interdiscipline    // e.g. biophysics, quantum chemistry 

Disipline record {
  name   : String
  entity : Entity
}