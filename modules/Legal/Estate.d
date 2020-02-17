from Accounting import Account, Asset

Estate protocol { 
  assets        : [] Asset        // Held in one or more accounts 
  liability     : [] Liability    
  beneficiaries : [] Benefitary

  Benefitary struct {
    entity: Entity
  }
}

// All the money and property owned by a particular person, especially at death.


// An estate, in common law, is the net worth of a person at any point in time alive or dead. 

// It is the sum of a person's assets – legal rights, interests and entitlements to property of any kind – less all liabilities at that time. 