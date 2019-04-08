Employment protocol {
  * commence : employed
  * | work   : working          
    | vacate : vacating
    | sick   : sick
    ↺
  * | quit    ∎ : quit
    | dismiss ∎ : dimissed
    | retire  ∎ : retired
  
  level         -> Employment`Level
  position      -> Employment`Position
  compensations -> [ Compensation ] 
  terms         -> [ Legal::Term ] // * Employment Terms */ CREATE TABLE k100452345 (m1 key long, m2 key long)

  retire    () -> Retirement
  leave     () -> Employment`Termination
  dismiss   () -> Employment`Termination
}


Employment actor {
  employer : Entity
  employee : Entity
}

// notes: 
// An employment has state
// - work & compensation takes place through an employment

Employment`Termination event { 
  reason: Quit | Dismissed
}

/* .e.g. -----------------------------------
Expected        `Work (40 hours,  Weekly)
Payment         `Term (5000 usd,  Monthly) 
Vacation`Leave  `Term (1  day,    Weekly)
Sick`Leave      `Term (1  day,    Weekly)
Maternity`Leave `Term (60 days,   Yearly)
------------------------------------------- */


// Employee of X
// Member of