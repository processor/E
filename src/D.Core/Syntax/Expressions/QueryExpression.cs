namespace D.Syntax
{
    public class QueryExpression : ISyntax
    {
        public QueryExpression(
            ISyntax collection,
            ISyntax variable,
            ISyntax filter,
            ISyntax map,
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

        public ISyntax Collection { get; }       // from Y

        public ISyntax Variable { get; }         // from [x] in Y 

        public ISyntax Filter { get; }           // where a > 100

        public ISyntax Map { get; }              // select a || { a, b, c }  

        public OrderByStatement OrderBy { get; }     // orderby a desc

        public ISyntax Using { get; }            // using index_name

        public long Skip { get; }

        public long Take { get; }

        Kind IObject.Kind => Kind.QueryExpression;
    }

    public class OrderByStatement
    {
        public OrderByStatement(ISyntax member, bool descending)
        {
            Member = member;
            Descending = descending;
        }

        // orderby student.Last ascending, 
        // student.First ascending

        public ISyntax Member { get; }

        public bool Descending { get; }
    }

    // TODO: Support mutiple statements
}

// filter | where
// map    | select
// skip
// take
// orderby