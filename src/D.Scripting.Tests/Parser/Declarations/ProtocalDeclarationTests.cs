using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class ProtocalDeclarationTests : TestBase
    {
        [Fact]
        public void EmptyBody()
        {
            var protocal = Parse<ProtocalDeclarationSyntax>("SVG protocal { }");

            Assert.Equal("SVG", protocal.Name.ToString());
        }

        [Fact]
        public void PropertyTests2()
        {
            var protocal = Parse<ProtocalDeclarationSyntax>(@"
Node protocal { 
  kind     -> Kind
  children -> [ ] Node
}");

            var a = protocal.Members[0];

            Assert.Equal("kind", a.Name);
            Assert.Equal("Kind", a.ReturnType.Name);

            var b = protocal.Members[1];

            Assert.Equal("children", b.Name);
            Assert.Equal("List",     b.ReturnType.Name);
            Assert.Equal("Node",     b.ReturnType.Arguments[0].Name);
        }


        [Fact]
        public void VoidTests()
        {

            var protocal = Parse<ProtocalDeclarationSyntax>(@"
Memory protocal { 
  free()
}");

            Assert.Equal("free", protocal.Members[0].Name);
            Assert.Equal("Void", protocal.Members[0].ReturnType);
        }

        [Fact]
        public void PropertyTests()
        {

            var protocal = Parse<ProtocalDeclarationSyntax>(@"
Point protocal { 
  length -> Number
}");

            var length = protocal.Members[0];

            Assert.Equal("length", length.Name);
            Assert.Equal("Number", length.ReturnType);
        }

        [Fact]
        public void ChannelAndActions()
        {
            var protocal = Parse<ProtocalDeclarationSyntax>(@"
Bank protocal { 
  * | open       `Account       
    | close      `Account     
    | settle     `Transaction
    | refuse     `Transaction 
    | underwrite `Loan        
    | process    `Transaction 
    ↺            : acting
  * dissolve ∎   : dissolved
 
  open    `Account     (Account)     -> Account
  close   `Account     (Account)     -> Account`Closure
  settle  `Transaction (Transaction) -> Transaction`Settlement
  refuse  `Transaction (Transaction) -> Transaction`Refusal
  reverse `Transaction (Transaction) -> Transaction`Reversed
}");
            Assert.Equal(2, protocal.Messages.Length);
            
            Assert.Equal(6, ((MessageChoice)protocal.Messages[0]).Count);

            var member = protocal.Members[0];

            Assert.Equal("openAccount",     member.Name);
            Assert.Equal(1,                 member.Parameters.Length);
            Assert.Equal("Account",         member.Parameters[0].Type);
            Assert.Equal("Account",         member.ReturnType);

            member = protocal.Members[1];

            Assert.Equal("closeAccount",   member.Name);
            Assert.Equal("AccountClosure", member.ReturnType);

            Assert.Equal(5, protocal.Members.Length);
        }

        [Fact]
        public void B()
        {

            var protocal = Parse<ProtocalDeclarationSyntax>(@"
Bank protocal { 
  * | open       `Account
    | close      `Account 
    | settle     `Transaction
    | refuse     `Transaction
    | underwrite `Loan        
    | process    `Transaction 
    ↺            : acting
  * dissolve ∎   : dissolved
}");

            Assert.Equal("Bank", protocal.Name.ToString());

            Assert.Equal(2, protocal.Messages.Length);

            var a = (MessageChoice)protocal.Messages[0];
            var b = (ProtocalMessage)protocal.Messages[1];

            Assert.Equal(6, a.Count);
            Assert.True(a.Repeats);
            Assert.False(a.IsEnd);

            Assert.Equal("openAccount",        a[0].Name);
            Assert.Equal("closeAccount",       a[1].Name);
            Assert.Equal("settleTransaction",  a[2].Name);
            Assert.Equal("refuseTransaction",  a[3].Name);
            Assert.Equal("underwriteLoan",     a[4].Name);
            Assert.Equal("processTransaction", a[5].Name);

            Assert.Equal("dissolve", b.Name);
            Assert.Equal("dissolved", b.Label);
            Assert.True(b.IsEnd);
        }
    }
}

