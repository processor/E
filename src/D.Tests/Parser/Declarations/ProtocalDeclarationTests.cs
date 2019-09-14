using Xunit;

using D.Protocols;
using D.Syntax;

namespace D.Parsing.Tests
{
    public class ProtocolDeclarationTests : TestBase
    {
        [Fact]
        public void EmptyBody()
        {
            var protocol = Parse<ProtocolDeclarationSyntax>("SVG protocol { }");

            Assert.Equal("SVG", protocol.Name.ToString());
        }

        [Fact]
        public void PropertyTests2()
        {
            var protocol = Parse<ProtocolDeclarationSyntax>(@"
Node protocol { 
  kind     -> Kind
  children -> [ Node ]
}");

            var a = protocol.Members[0];

            Assert.Equal("kind", a.Name);
            Assert.Equal("Kind", a.ReturnType.Name);

            var b = protocol.Members[1];

            Assert.Equal("children", b.Name);
            Assert.Equal("Array",    b.ReturnType.Name);
            Assert.Equal("Node",     b.ReturnType.Arguments[0].Name);
        }


        [Fact]
        public void VoidTests()
        {

            var protocol = Parse<ProtocolDeclarationSyntax>(@"
Memory protocol { 
  free()
}");

            Assert.Equal("free", protocol.Members[0].Name);
            Assert.Equal("Void", protocol.Members[0].ReturnType);
        }

        [Fact]
        public void PropertyTests()
        {

            var protocol = Parse<ProtocolDeclarationSyntax>(@"
Point protocol { 
  length -> Number
}");

            var length = protocol.Members[0];

            Assert.Equal("length", length.Name);
            Assert.Equal("Number", length.ReturnType);
        }

        [Fact]
        public void ChannelAndActions()
        {
            var protocol = Parse<ProtocolDeclarationSyntax>(@"
Bank protocol { 
  * | open       `Account       
    | close      `Account     
    | settle     `Transaction
    | refuse     `Transaction 
    | underwrite `Loan        
    | process    `Transaction 
    ↺            : acting
  * dissolve ∎   : dissolved
 
  open    `Account     (account: Account)         -> Account
  close   `Account     (account: Account)         -> Account`Closure
  settle  `Transaction (transaction: Transaction) -> Transaction`Settlement
  refuse  `Transaction (transaction: Transaction) -> Transaction`Refusal
  reverse `Transaction (transaction: Transaction) -> Transaction`Reversed
}");
            Assert.Equal(2, protocol.Messages.Length);
            
            Assert.Equal(6, ((ProtocolMessageChoice)protocol.Messages[0]).Count);

            var member = protocol.Members[0];

            Assert.Equal("openAccount",     member.Name);
            Assert.Single(                  member.Parameters);
            Assert.Equal("account",         member.Parameters[0].Name);
            Assert.Equal("Account",         member.Parameters[0].Type);
            Assert.Equal("Account",         member.ReturnType);

            member = protocol.Members[1];

            Assert.Equal("closeAccount",   member.Name);
            Assert.Equal("AccountClosure", member.ReturnType);

            Assert.Equal(5, protocol.Members.Length);
        }

        [Fact]
        public void B()
        {
            var protocol = Parse<ProtocolDeclarationSyntax>(@"
Bank protocol { 
  * | open       `Account
    | close      `Account 
    | settle     `Transaction
    | refuse     `Transaction
    | underwrite `Loan        
    | process    `Transaction 
    ↺            : acting
  * dissolve ∎   : dissolved
}");

            Assert.Equal("Bank", protocol.Name.ToString());

            Assert.Equal(2, protocol.Messages.Length);

            var a = (ProtocolMessageChoice)protocol.Messages[0];
            var b = (ProtocolMessage)protocol.Messages[1];

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

