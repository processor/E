namespace E.Mathematics;

public sealed class SetsModule : Module
{
    public SetsModule()
    {
        AddExport(Intersection   ); // ∩
        AddExport(Union          ); // ∪
        AddExport(Subset         ); // ⊆
        AddExport(ProperSubset   ); // ⊂
        AddExport(NotSubset      ); // ⊄"
        AddExport(Superset       ); // ⊇
        AddExport(ProperSuperset ); // ⊃
        AddExport(NotSuperset);     // ⊅
    }

    public static readonly Operator Intersection    = Operator.Infix(ObjectType.Intersection,   "∩",  precedence: 1);
    public static readonly Operator Union           = Operator.Infix(ObjectType.Union,          "∪", precedence: 1);
    public static readonly Operator Subset          = Operator.Infix(ObjectType.Subset,         "⊆", precedence: 1);
    public static readonly Operator ProperSubset    = Operator.Infix(ObjectType.ProperSubset,   "⊂", precedence: 1);
    public static readonly Operator NotSubset       = Operator.Infix(ObjectType.NotSubset,      "⊄",  precedence: 1);
    public static readonly Operator Superset        = Operator.Infix(ObjectType.Superset,       "⊇", precedence: 1);
    public static readonly Operator ProperSuperset  = Operator.Infix(ObjectType.ProperSuperset, "⊃", precedence: 1);
    public static readonly Operator NotSuperset     = Operator.Infix(ObjectType.NotSuperset,    "⊅",  precedence: 1);
}