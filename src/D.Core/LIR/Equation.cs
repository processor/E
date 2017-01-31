namespace D.Expressions
{
    // a + 5    =  a → a + 5
    // a + 5^2  =  a → a^2

    // A linear equation in one unknown x may always be rewritten
    // If a ≠ 0, there is a unique solution

    // Linear equation
    // y = 5 + x

    // differential equation
    // A differential equation is any equation which contains derivatives, either ordinary derivatives or partial derivatives.

    public class Equation : IExpression
    {
        public Equation(IObject left, IObject right, string[] symbols)
        {
            Left = left;
            Right = right;
            Symbols = symbols;
        }

        public IObject Left { get; }

        public IObject Right { get; }

        public string[] Symbols { get; }

        // TODO: Invariants
        // x > 10

        public Kind Kind => Kind.Equation;

        // Reduce / Simplify
    }
}
