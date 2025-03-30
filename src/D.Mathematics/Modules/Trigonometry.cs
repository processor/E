namespace E.Mathematics;

using static Math;

// The trigonometric functions (also called the circular functions) are functions of an angle. 

public static class Trigonometry
{
    // TODO: Implement as an expression tree

    public static readonly MathFunction<double> Sine               = new("sin",   Sin);
    public static readonly MathFunction<double> Cosine             = new("cos",   Cos);
    public static readonly MathFunction<double> Tangent            = new("tan",   Tan);
    public static readonly MathFunction<double> Cotangent          = new("cot",   static x => 1d / Tan(x));
    public static readonly MathFunction<double> Secant             = new("sec",   static x => 1 / Cos(x));
    public static readonly MathFunction<double> Cosecant           = new("cosec", static x => 1 / Tan(x));
    
    // Analogs of ordinary circule functions
    public static readonly MathFunction<double> HyperbolicSine     = new("sinh", x => (Exp(x) - Exp(-x)) / 2);
    public static readonly MathFunction<double> HyperbolicCosine   = new("cosh", x => (Exp(x) + Exp(-x)) / 2);
    public static readonly MathFunction<double> HyperbolicTangent  = new("tanh", x => (Exp(x) - Exp(-x)) / (Exp(x) + Exp(-x)));
    
    // public static readonly IFunction HyperbolicCoTangent; //

    public static readonly MathFunction<double> HyperbolicSecant   = new("sech", x => 2 / (Exp(x) + Exp(-x)));
    public static readonly MathFunction<double> HyperbolicCosecant = new("cosech", x => 2 / (Exp(x) - Exp(-x)));

    // arsin
    // arcos
    // artan
    // arcot
    // arcsec
}