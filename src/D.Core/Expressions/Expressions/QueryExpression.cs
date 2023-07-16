namespace E.Expressions;

public sealed class QueryExpression(
    IExpression collection,
    IExpression variable,
    IExpression filter,
    IExpression map,
    OrderByStatement orderBy,
    long skip = 0,
    long take = 0) : IExpression
{

    // | from Y
    public IExpression Collection { get; } = collection;

    // | from [x] in Y 
    public IExpression Variable { get; } = variable;

    // | where a > 100
    public IExpression Filter { get; } = filter;

    // | select a || { a, b, c }  
    public IExpression Map { get; } = map;

    // | orderby a desc
    public OrderByStatement OrderBy { get; } = orderBy;

    // | using index_name
    public IExpression? Using { get; }

    public long Skip { get; } = skip;

    public long Take { get; } = take;

    ObjectType IObject.Kind => ObjectType.QueryExpression;
}

public sealed class OrderByStatement(IExpression member, bool isDescending)
{

    // orderby student.Last ascending, 
    // student.First ascending

    public IExpression Member { get; } = member;

    public bool IsDescending { get; } = isDescending;
}

// TODO: Support multiple statements

// filter | where
// map    | select
// skip
// take
// orderby