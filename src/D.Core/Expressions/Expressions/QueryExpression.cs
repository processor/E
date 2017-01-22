namespace D.Expressions
{
    public class QueryExpression : IExpression
    {
        public QueryExpression(
            IExpression collection,
            IExpression variable,
            IExpression filter,
            IExpression map,
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

        public IExpression Collection { get; }       // from Y

        public IExpression Variable { get; }         // from [x] in Y 

        public IExpression Filter { get; }           // where a > 100

        public IExpression Map { get; }              // select a || { a, b, c }  

        public OrderByStatement OrderBy { get; }     // orderby a desc

        public IExpression Using { get; }            // using index_name

        public long Skip { get; }

        public long Take { get; }

        Kind IObject.Kind => Kind.QueryExpression;
    }

    public class OrderByStatement
    {
        public OrderByStatement(IExpression member, bool descending)
        {
            Member = member;
            Descending = descending;
        }

        // orderby student.Last ascending, 
        // student.First ascending

        public IExpression Member { get; }

        public bool Descending { get; }
    }

    // TODO: Support mutiple statements
}

// filter | where
// map    | select
// skip
// take
// orderby