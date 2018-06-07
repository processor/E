namespace D.Expressions
{
    // A protocol { }

    /*
    ∙ | open       `Account       
      | close      `Account     
      | settle     `Transaction
      | refuse     `Transaction 
      | underwrite `Loan        
      | process    `Transaction 
      ↺            : acting
    ∙ dissolve ∎   : dissolved
    */

    public interface IMessageDeclaration
    {
        bool Fallthrough { get; }
    }
}


/*
type Person = {
  name: String where length > 0
}
*/
