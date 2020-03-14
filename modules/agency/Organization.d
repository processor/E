Organization protocol {
  * act       ↺ : acting 
  * bankrupt    ↑ acting        // backto acting
  * merge     ∎ : merged
  * dissolve  ∎ : dissolved
 
  // Actions
  incorporate   ()                     -> Incorporation   // established
  dissolve      ()                     -> Dissolution
  merge         (target: Organization) -> Merger

  incorporation :    Incorporation    
  directors     : [] Director      // including managing & officers
  jurisdiction  :    Jurisdiction  // via Incorporation.jurisdiction
  bylaws        : [] Bylaw        // rules of governance
  parent        : Organization?
  children      : [] Organization

  // Specific messages
  receive (invoice: Invoice)

  when dissolved {
     // logic that triggers when dissolved
  }
}

Partnership class : Organization   
Corporation class : Organization     // e.g. Microsoft, Inc
Institution class : Organization


// Registration Date

// US-DE