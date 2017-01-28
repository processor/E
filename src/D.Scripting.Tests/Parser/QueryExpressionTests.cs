using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class QueryExpressionTests : TestBase
    {
        [Fact]
        public void A()
        {
            var query = Parse<QueryExpression>(@"
from Accounts
where balance > 1000000
select { id, balance }
orderby id ascending
take 100
");

            Assert.Equal("Accounts", query.Collection.ToString());

            var map = (NewObjectExpressionSyntax)query.Map;

            Assert.Equal(2, map.Members.Length);

            Assert.Equal("id",      map.Members[0].Name);
            Assert.Equal("balance", map.Members[1].Name);

            Assert.Equal("id", query.OrderBy.Member.ToString());
            
            Assert.False(query.OrderBy.Descending);
            Assert.Equal(0, query.Skip);
            Assert.Equal(100, query.Take);
        }

        [Fact]
        public void WithIndex()
        {
            // FROM Accounts WITH (INDEX(AK_Contact_rowguid))

            var query = Parse<QueryExpression>(@"
from place in Places using idxplacekind
where place.population > 1000
  && place.kind == 3
orderby id descending
");

            Assert.True(query.OrderBy.Descending);
        }

        [Fact]
        public void B()
        {
            var query = Parse<QueryExpression>(@"
from place in Places
where population > 1000 && kind == 3
select { id, kind, population }
skip 25
take 50
");

            Assert.Equal(3, ((NewObjectExpressionSyntax)query.Map).Count);

            Assert.Equal(25, query.Skip);
            Assert.Equal(50, query.Take);
        }

        [Fact]
        public void C()
        {
            var query = Parse<QueryExpression>(@"
from city in Places
where city is City && city.population > 1000
select { id: city.id, population: city.population }
");


        }

        [Fact]
        public void D()
        {
            var query = Parse<QueryExpression>(@"
from x in 0...100
  where x > 1 && x != 3
select x
skip 3
");

            Assert.Equal("x", query.Map.ToString()); // variable
            Assert.Equal(3, query.Skip);
        }
    }
}