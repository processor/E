namespace D.Mathematics
{

    using static ArithmethicFunction;

    public class ArithmeticModule : Module
    {
        public ArithmeticModule()
        {
            Add(ArithmethicFunction.Add);
            Add(Subtract);
            Add(Multiply);
            Add(Divide);
            Add(Modulus);
            Add(Power);

            // Generic
            Add(Floor);
            Add(SquareRoot);
            Add(Log);
            Add(Log10);
        }
    }
}