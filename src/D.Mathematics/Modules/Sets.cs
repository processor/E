namespace D.Mathematics
{
    public class SetsModule : Module
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

        public static readonly Operator Intersection    = Operator.Infix(Kind.Intersection,   "∩",  precedence: 1);
        public static readonly Operator Union           = Operator.Infix(Kind.Union,          "∪", precedence: 1);
        public static readonly Operator Subset          = Operator.Infix(Kind.Subset,         "⊆", precedence: 1);
        public static readonly Operator ProperSubset    = Operator.Infix(Kind.ProperSubset,   "⊂", precedence: 1);
        public static readonly Operator NotSubset       = Operator.Infix(Kind.NotSubset,      "⊄",  precedence: 1);
        public static readonly Operator Superset        = Operator.Infix(Kind.Superset,       "⊇", precedence: 1);
        public static readonly Operator ProperSuperset  = Operator.Infix(Kind.ProperSuperset, "⊃", precedence: 1);
        public static readonly Operator NotSuperset     = Operator.Infix(Kind.NotSuperset,    "⊅",  precedence: 1);
    }
}