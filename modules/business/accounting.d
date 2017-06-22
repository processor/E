use commerce::Account 
use finance::Asset

Estate protocol { 
  accounts      -> [ ] Account      // Estate'Accounts -- CREATE TABLE k100452345 (m1 key long, m2 key long)
  assets        -> [ ] Asset
  beneficiaries -> [ ] Entity
}
