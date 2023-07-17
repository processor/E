using E.Symbols;

namespace E;

// x > 10
// x < 10

public sealed class Predicate(Operator op, Symbol left, IObject right) : IObject
{
    public Operator Operator { get; } = op;

    public Symbol Left { get; } = left;

    public IObject Right { get; } = right;

    ObjectType IObject.Kind => ObjectType.Predicate;

    public override string ToString()
    {
        return $"{Left} {Operator.Name} {Right}";
    }
}

//  {x | x is a positive integer less than 4} is the set {1,2,3}.

// x : Integer where value > 0 && value < 4,