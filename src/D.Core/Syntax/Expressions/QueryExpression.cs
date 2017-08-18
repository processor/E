namespace D.Syntax
{
    public class QueryExpression : SyntaxNode
    {
        public QueryExpression(
            SyntaxNode collection,
            SyntaxNode variable,
            SyntaxNode filter,
            SyntaxNode map,
            OrderByStatement orderBy,
            long skip = 0,
            long take = 0)
        {
            Collection = collection;
            Variable = variable;
            Filter = filter;
            Map = map;
            OrderBy = orderBy;
            Skip = skip;
            Take = take;
        }

        public SyntaxNode Collection { get; }       // from Y

        public SyntaxNode Variable { get; }         // from [x] in Y 

        public SyntaxNode Filter { get; }           // where a > 100

        public SyntaxNode Map { get; }              // select a || { a, b, c }  

        public OrderByStatement OrderBy { get; }     // orderby a desc

        public SyntaxNode Using { get; }            // using index_name

        public long Skip { get; }

        public long Take { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.QueryExpression;
    }

    public class OrderByStatement
    {
        public OrderByStatement(SyntaxNode member, bool descending)
        {
            Member = member;
            Descending = descending;
        }

        // orderby student.Last ascending, 
        // student.First ascending

        public SyntaxNode Member { get; }

        public bool Descending { get; }
    }

    // TODO: Support mutiple statements
}

// filter | where
// map    | select
// skip
// take
// orderby