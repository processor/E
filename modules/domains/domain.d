, Natural  `Science // the study of natural phenomena (including fundamental forces and biological life)
, Formal   `Science // the study of mathematics and logic, which use an a priori, as opposed to factual, methodology)
, Social   `Science // the study of human behavior and societies[1]
, Applied  `Science // apply existing scientific knowledge to develop more practical applications (like healthcare, technology or inventions).
, Informal `Science // ?
, Humanity          // things produced for human experience
: Domain type { 

}

Pysical `Science : Natural `Science;

// Base Libraries
  Base                   // Types, Records, Tuples, Arrays, Implementations       
, Graphics                                        
: Library

 // Formal, Physical, & Life 
, Chemistry   : Pysical `Science 
, Biology     : Natural `Science   
, Mathematics : Formal  `Science
, Physics     : Natural `Science

// Social Sciences (Business, being, law, history, etc)
  Antropology           
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
: Social `Science; 

// Applied Sciences
  Agriculture             // Crops & livestock, includes Agrochemistry
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
: Applied `Science  

// Disipline          // e.g. neurology
// Interdiscipline    // e.g. biophysics, quantum chemistry 

Disipline record {
  name   : String
  entity : Entity
}