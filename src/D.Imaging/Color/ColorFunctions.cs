using System;
using Carbon.Color;

namespace E.Imaging
{
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

        ColorFunction AdjustAlpha(IColor source, double amount)      => new ColorFunction("alpha");
        ColorFunction AdjustHue(IColor source, double amount)        => new ColorFunction("hue");
        ColorFunction AdjustSaturation(IColor source, double amount) => new ColorFunction("saturation");
        ColorFunction AdjustLightness(IColor source, double amount)  => new ColorFunction("lightness");
        ColorFunction AdjustWhiteness(IColor source, double amount)  => new ColorFunction("whiteness");
        ColorFunction AdjustBlackness(IColor source, double amount)  => new ColorFunction("blackness");
        ColorFunction AdjustTint(IColor source, double amount)       => new ColorFunction("tint");
        ColorFunction AdjustShade(IColor source, double amount)      => new ColorFunction("shade");
        ColorFunction Blend(IColor lhs, IColor rhs, double amount)   => new ColorFunction("blend");
        ColorFunction Blenda(IColor source, double amount)           => new ColorFunction("blenda");
        ColorFunction AdjustContrast(IColor source, double amount)   => new ColorFunction("contrast");
    }

    public sealed class ColorFunction : IFunction
    {
        private readonly Func<IColor, double, IColor>? func;

        public ColorFunction(string name, Func<IColor, double, IColor>? func = null)
        {
            Name = name;

            this.func = func;
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
}
