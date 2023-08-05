using E.Parsing;
using E.Symbols;

namespace E.Expressions;

public static class Expression
{
    public static CallExpression Call(Symbol name, IArguments arguments) => new(null, name, arguments, false);

    public static Parameter Parameter(string name) => new(name);

    #region Logic

    public static BinaryExpression And(IExpression x, IExpression y) => new(Operator.LogicalAnd, x, y);

    public static BinaryExpression Or(IExpression x, IExpression y) => new(Operator.LogicalOr, x, y);

    public static UnaryExpression Not(IExpression x) => new(Operator.Not, x);

    #endregion

    #region Arthimetic

    public static BinaryExpression Multiply(IObject lhs, IObject rhs)
    {
        return new BinaryExpression(Operator.Multiply, lhs, rhs);
    }

    public static BinaryExpression Multiply(IObject lhs, double rhs)
    {
        return new BinaryExpression(Operator.Multiply, lhs, new Number(rhs));
    }

    public static BinaryExpression Divide(IObject lhs, IObject rhs)
    {
        return new BinaryExpression(Operator.Divide, lhs, rhs);
    }

    public static BinaryExpression Add(IObject lhs, IObject rhs)
    {
        return new BinaryExpression(Operator.Add, lhs, rhs);
    }

    #endregion

    #region Comparisions

    public static BinaryExpression GreaterThan(IExpression x, IExpression y)
        => new(Operator.GreaterThan, x, y);

    public static BinaryExpression GreaterThanOrEqual(IExpression x, IExpression y)
        => new(Operator.GreaterOrEqual, x, y);

    public static BinaryExpression LessThanOrEqual(IExpression x, IExpression y)
        => new(Operator.LessThan, x, y);

    public static BinaryExpression LessThan(IExpression x, IExpression y)
        => new(Operator.LessOrEqual, x, y);

    #endregion

    public static IExpression Parse(string text)
    {
        var compiler = new Compiler();

        var syntax = Parser.Parse(text);

        return compiler.Visit(syntax);
    }
}