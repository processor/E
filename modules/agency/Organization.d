Partnership type : Organization;
Corporation type : Organization;   // e.g. Microsoft, Inc
Institution type : Organization;
Partnership type : Organization;

Organization protocol {
  * incorporate : incorporated
  * action    ↺ : acting 
  * bankrupt    ↑ acting        // backto acting
  * merge     ∎ : merged
  * disolve   ∎ : dissolved
 
  incorporate   ()                     -> Incorporation   // established
  dissolve      ()                     -> Dissolution
  merge         (Organization: target) -> Corporate'Merger

  directors    -> [ ] Director      // including managing & officers
  jurisdiction -> Jurisdiction      // via Incorporation.jurisdiction

  has Bylaws
    of Rules, Regulations, and Terms through Corporate_Bylaws(entity);
    formatted into a Document;

  when dissolved {
     // logic that triggers when dissolved
  }
}