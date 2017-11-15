namespace D.Syntax
{
    public class QueryExpression : ISyntaxNode
    {
        public QueryExpression(
            ISyntaxNode collection,
            ISyntaxNode variable,
            ISyntaxNode filter,
            ISyntaxNode map,
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

        public ISyntaxNode Collection { get; }       // from Y

        public ISyntaxNode Variable { get; }         // from [x] in Y 

        public ISyntaxNode Filter { get; }           // where a > 100

        public ISyntaxNode Map { get; }              // select a || { a, b, c }  

        public OrderByStatement OrderBy { get; }     // orderby a desc

        public ISyntaxNode Using { get; }            // using index_name

        public long Skip { get; }

        public long Take { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.QueryExpression;
    }

    public class OrderByStatement
    {
        public OrderByStatement(ISyntaxNode member, bool descending)
        {
            Member = member;
            Descending = descending;
        }

        // orderby student.Last ascending, 
        // student.First ascending

        public ISyntaxNode Member { get; }

        public bool Descending { get; }
    }

    // TODO: Support mutiple statements
}

// filter | where
// map    | select
// skip
// take
// orderby