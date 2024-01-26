using System;

using Carbon.Color;

namespace E.Imaging;

public sealed class RgbConstructor : IFunction
{
    public Parameter[] Parameters => throw new NotImplementedException();

    public string Name => "rgb";

    public ObjectType Kind => throw new NotImplementedException();

    public object Invoke(IArguments args)
    {
        return new SRgb(
           r: (float)((INumber)(args[0])).Real,
           g: (float)((INumber)(args[1])).Real,
           b: (float)((INumber)(args[2])).Real
       );
    }
}

/*
rgb() = rgb( <rgb-component>#{3} )
rgba() = rgba( <rgb-component>#{3} , <alpha-value> )
<rgb-component> = <number> | <percentage>
<alpha-value> = <number> | <percentage>
*/


/*
rgb() = rgb( <rgb-component>#{3} )
rgba() = rgba( <rgb-component>#{3} , <alpha-value> )
<rgb-component> = <number> | <percentage>
<alpha-value> = <number> | <percentage>
*/

