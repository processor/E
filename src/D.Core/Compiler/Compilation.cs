using System.Collections.Generic;
using D.Expressions;

namespace D
{
    public class Compilation
    {
        public List<IExpression> Expressions { get; } = new List<IExpression>();
    }
}