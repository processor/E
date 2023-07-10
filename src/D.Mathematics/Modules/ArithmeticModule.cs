namespace E.Mathematics;

public sealed class ArithmeticModule : Module
{
    public ArithmeticModule()
    {
        AddExport(ArithmeticFunction.Add);
        AddExport(ArithmeticFunction.Subtract);
        AddExport(ArithmeticFunction.Multiply);
        AddExport(ArithmeticFunction.Divide);
        AddExport(ArithmeticFunction.Modulus);
        AddExport(ArithmeticFunction.Power);

        // Generic
        AddExport(ArithmeticFunction.Floor);
        AddExport(ArithmeticFunction.SquareRoot);
        AddExport(ArithmeticFunction.Log);
        AddExport(ArithmeticFunction.Log10);
    }
}