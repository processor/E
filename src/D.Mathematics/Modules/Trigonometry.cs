using System;

namespace D.Mathematics
{
    using static Math;

    // The trigonometric functions (also called the circular functions) are functions of an angle. 

    public class TrigonometryModule : Module
    {
        public TrigonometryModule()
        {
            Add(Trigonometry.Sine);
            Add(Trigonometry.Cosine);
            Add(Trigonometry.Tangent);
            Add(Trigonometry.Cotangent);
            Add(Trigonometry.Secant);
            Add(Trigonometry.Cosecant);
            Add(Trigonometry.HyperbolicSine);
            Add(Trigonometry.HyperbolicCosine);
            Add(Trigonometry.HyperbolicTangent);
         }
    }

    public static class Trigonometry
    {
        // TODO: Implement as an expression tree in dscript

        public static readonly IFunction Sine               = new MathFunction("sin",   x => Sin(x));
        public static readonly IFunction Cosine             = new MathFunction("cos",   x => Cos(x));
        public static readonly IFunction Tangent            = new MathFunction("tan",   x => Tan(x));
        public static readonly IFunction Cotangent          = new MathFunction("cot",   x => 1d / Tan(x));
        public static readonly IFunction Secant             = new MathFunction("sec",   x => 1 / Cos(x));
        public static readonly IFunction Cosecant           = new MathFunction("cosec", x => 1 / Tan(x));
        
        // Analogs of oridinary circule functions
        public static readonly IFunction HyperbolicSine     = new MathFunction("sinh", x => (Exp(x) - Exp(-x)) / 2);
        public static readonly IFunction HyperbolicCosine   = new MathFunction("cosh", x => (Exp(x) + Exp(-x)) / 2);
        public static readonly IFunction HyperbolicTangent  = new MathFunction("tanh", x => (Exp(x) - Exp(-x)) / (Exp(x) + Exp(-x)));
        
        // public static readonly IFunction HyperbolicCoTangent; //

        public static readonly IFunction HyperbolicSecant   = new MathFunction("sech", x => 2 / (Exp(x) + Exp(-x)));
        public static readonly IFunction HyperbolicCosecant = new MathFunction("cosech", x => 2 / (Exp(x) - Exp(-x)));

        // arsin
        // arcos
        // artan
        // arcot
        // arcsec
    }
}