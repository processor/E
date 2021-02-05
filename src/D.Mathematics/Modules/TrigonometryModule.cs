namespace E.Mathematics
{
    public sealed class TrigonometryModule : Module
    {
        public TrigonometryModule()
        {
            AddExport(Trigonometry.Sine);
            AddExport(Trigonometry.Cosine);
            AddExport(Trigonometry.Tangent);
            AddExport(Trigonometry.Cotangent);
            AddExport(Trigonometry.Secant);
            AddExport(Trigonometry.Cosecant);
            AddExport(Trigonometry.HyperbolicSine);
            AddExport(Trigonometry.HyperbolicCosine);
            AddExport(Trigonometry.HyperbolicTangent);
         }
    }
}