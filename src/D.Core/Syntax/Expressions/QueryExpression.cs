namespace E.Syntax;

public sealed class QueryExpression(
    ISyntaxNode collection,
    ISyntaxNode? variable,
    ISyntaxNode? filter,
    ISyntaxNode? map,
    OrderByStatement? orderBy,
    long skip = 0,
    long take = 0) : ISyntaxNode
{

    // | from Y
    public ISyntaxNode Collection { get; } = collection;

    // | from [x] in Y 
    public ISyntaxNode? Variable { get; } = variable;

    // | where a > 100
    public ISyntaxNode? Filter { get; } = filter;

    // | select a || { a, b, c }
    public ISyntaxNode? Map { get; } = map;

    // | orderby a desc
    public OrderByStatement? OrderBy { get; } = orderBy;

    // | using index_name
    public ISyntaxNode? Using { get; }

    public long Skip { get; } = skip;

    public long Take { get; } = take;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.QueryExpression;
}

public sealed class OrderByStatement(ISyntaxNode member, bool descending)
{

    // orderby student.Last ascending, 
    // student.First ascending

    public ISyntaxNode Member { get; } = member;

    public bool Descending { get; } = descending;
}

// TODO: Support multiple statements

// filter | where
// map    | select
// skip
// take
// orderby