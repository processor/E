namespace D.Protocols
{
    public interface IProtocolMessage
    {
        bool Fallthrough { get; }
    }
}


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
