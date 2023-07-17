using System;
using Carbon.Color;

namespace E.Imaging;

public class ColorFunctions
{
    // Hue
    // Saturation
    // Lightness
    // Whiteness
    // Blackness
    // Tint
    // Shade
    // Blend
    // Blenda
    // Contrast

    public static ColorFunction AdjustAlpha(IColor source, double amount)      => new("alpha");
    public static ColorFunction AdjustHue(IColor source, double amount)        => new("hue");
    public static ColorFunction AdjustSaturation(IColor source, double amount) => new("saturation");
    public static ColorFunction AdjustLightness(IColor source, double amount)  => new("lightness");
    public static ColorFunction AdjustWhiteness(IColor source, double amount)  => new("whiteness");
    public static ColorFunction AdjustBlackness(IColor source, double amount)  => new("blackness");
    public static ColorFunction AdjustTint(IColor source, double amount)       => new("tint");
    public static ColorFunction AdjustShade(IColor source, double amount)      => new("shade");
    public static ColorFunction Blend(IColor lhs, IColor rhs, double amount)   => new("blend");
    public static ColorFunction Blenda(IColor source, double amount)           => new("blenda");
    public static ColorFunction AdjustContrast(IColor source, double amount)   => new("contrast");
}

public sealed class ColorFunction : IFunction
{
    private readonly Func<IColor, double, IColor>? _func;

    public ColorFunction(string name, Func<IColor, double, IColor>? func = null)
    {
        Name = name;
        _func = func;
    }

    public Parameter[] Parameters => throw new NotImplementedException();

    public string Name { get; }

    public ObjectType Kind => throw new NotImplementedException();

    public object Invoke(IArguments args)
    {
        var color = args[0];
        
        return color;
    }
}


// color-mod(#937b19 contrast(25%)
// color-mod(var(--base-color) tint(59%))
