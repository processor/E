import [ Bylaw ] from Legal

Organization protocol {
  * incorporate : incorporated
  * action    ↺ : acting 
  * bankrupt    ↑ acting        // backto acting
  * merge     ∎ : merged
  * disolve   ∎ : dissolved
 
  incorporate   ()                     -> Incorporation   // established
  dissolve      ()                     -> Dissolution
  merge         (target: Organization) -> Merger

  directors    -> [ Director ]   // including managing & officers
  jurisdiction ->   Jurisdiction // via Incorporation.jurisdiction

  bylaws       -> [ Bylaw ]

  when dissolved {
     // logic that triggers when dissolved
  }
}

  Partnership type,   
  Corporation type,     // e.g. Microsoft, Inc
  Institution type,   
: Organization;