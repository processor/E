using System;
using System.Numerics;
using System.Reflection.Emit;

namespace E.Units;

internal static class ConverterFactorCompiler
{
    public static Func<T, T> Compile<T>(T conversionFactor)
        where T: INumberBase<T>
    {
        if (conversionFactor == T.One)
        {
            return static (T source) => source;
        }
        if (typeof(T) == typeof(double))
        {
            return (Func<T, T>)(object)ConverterFactorCompiler.Compile((double)(object)conversionFactor);
        }      
        else
        {
            return (T source) => source * conversionFactor;
        }
    }

    public static Func<double, double> Compile(double conversionFactor)
    {
        // Define a dynamic method
        var method = new DynamicMethod("MultiplyByConst", typeof(double), [typeof(double)]);

        ILGenerator il = method.GetILGenerator();

        // Emit IL instructions:
        il.Emit(OpCodes.Ldarg_0);                  // Load the function argument (source)
        il.Emit(OpCodes.Ldc_R8, conversionFactor); // Load the constant
        il.Emit(OpCodes.Mul);                      // Multiply
        il.Emit(OpCodes.Ret);                      // Return

        // Create the delegate
        var func = (Func<double, double>)method.CreateDelegate(typeof(Func<double, double>));

        return func;
    }
}
