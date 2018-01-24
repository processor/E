Claim<T> protocol {
  value: T
}

SSN`Claim           : Claim<SSN>          { } // claim to have an ssn
Email`Address`Claim : Claim<Email>        { } // claim to have an email
Phone`Number`Claim  : Claim<Phone`Number> { }