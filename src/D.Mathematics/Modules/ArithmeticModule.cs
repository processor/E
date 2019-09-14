namespace D.Mathematics
{
    using static ArithmethicFunction;

    public class ArithmeticModule : Module
    {
        public ArithmeticModule()
        {
            AddExport(ArithmethicFunction.Add);
            AddExport(Subtract);
            AddExport(Multiply);
            AddExport(Divide);
            AddExport(Modulus);
            AddExport(Power);

            // Generic
            AddExport(Floor);
            AddExport(SquareRoot);
            AddExport(Log);
            AddExport(Log10);
        }
    }
}