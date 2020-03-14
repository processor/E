Employment protocol {
  * commence : employed
  * | work   : working          
    | vacate : vacating
    | leave  : left       // sick leave
    ↺ : acting
  * | quit    ∎ : quit
    | dismiss ∎ : dimissed
    | retire  ∎ : retired
  
  level         -> Employment`Level
  position      -> Employment`Position // role?
  compensations -> [] Compensation
  terms         -> [] Legal::Term      // * Employment Terms */ CREATE TABLE k100452345 (m1 key long, m2 key long)

  // either party may terminate the employment under it's terms
  terminate() { }
}

Employment process {
  employer : Entity
  employee : Entity

  // events
  Terminated event {
    reason: Quit | Dismissed
  }
}

// notes: 
// An employment has state
// - work & compensation takes place through an employment

/* .e.g. -----------------------------------
Expected        `Work (40 hours,  Weekly)
Payment         `Term (5000 usd,  Monthly) 
Vacation`Leave  `Term (1  day,    Weekly)
Sick`Leave      `Term (1  day,    Weekly)
Maternity`Leave `Term (60 days,   Yearly)
------------------------------------------- */


// Employee of X
// Member of