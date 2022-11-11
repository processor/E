namespace E.Mathematics;

using static Math;

// The trigonometric functions (also called the circular functions) are functions of an angle. 

public static class Trigonometry
{
    // TODO: Implement as an expression tree in dscript

    public static readonly MathFunction Sine               = new ("sin",   x => Sin(x));
    public static readonly MathFunction Cosine             = new ("cos",   x => Cos(x));
    public static readonly MathFunction Tangent            = new ("tan",   x => Tan(x));
    public static readonly MathFunction Cotangent          = new ("cot",   x => 1d / Tan(x));
    public static readonly MathFunction Secant             = new ("sec",   x => 1 / Cos(x));
    public static readonly MathFunction Cosecant           = new ("cosec", x => 1 / Tan(x));
    
    // Analogs of oridinary circule functions
    public static readonly MathFunction HyperbolicSine     = new ("sinh", x => (Exp(x) - Exp(-x)) / 2);
    public static readonly MathFunction HyperbolicCosine   = new ("cosh", x => (Exp(x) + Exp(-x)) / 2);
    public static readonly MathFunction HyperbolicTangent  = new ("tanh", x => (Exp(x) - Exp(-x)) / (Exp(x) + Exp(-x)));
    
    // public static readonly IFunction HyperbolicCoTangent; //

    public static readonly MathFunction HyperbolicSecant   = new ("sech", x => 2 / (Exp(x) + Exp(-x)));
    public static readonly MathFunction HyperbolicCosecant = new ("cosech", x => 2 / (Exp(x) - Exp(-x)));

    // arsin
    // arcos
    // artan
    // arcot
    // arcsec
}