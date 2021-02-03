// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

namespace E.Inference
{
    public interface IType
    {
        string Name { get; }

        IType BaseType { get; }

        IType[] ArgumentTypes { get; }

        IType? Self { get; }

        IType Value { get; }
    }
}