using System.Collections.Generic;
using E.Expressions;

namespace E;

public class Compilation
{
    public List<IExpression> Expressions { get; } = new List<IExpression>();
}