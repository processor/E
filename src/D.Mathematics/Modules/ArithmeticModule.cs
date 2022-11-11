namespace E.Mathematics;

public sealed class ArithmeticModule : Module
{
    public ArithmeticModule()
    {
        AddExport(ArithmethicFunction.Add);
        AddExport(ArithmethicFunction.Subtract);
        AddExport(ArithmethicFunction.Multiply);
        AddExport(ArithmethicFunction.Divide);
        AddExport(ArithmethicFunction.Modulus);
        AddExport(ArithmethicFunction.Power);

        // Generic
        AddExport(ArithmethicFunction.Floor);
        AddExport(ArithmethicFunction.SquareRoot);
        AddExport(ArithmethicFunction.Log);
        AddExport(ArithmethicFunction.Log10);
    }
}