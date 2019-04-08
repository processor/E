// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

namespace D.Inference
{
    public interface IType
    {
        string Name { get; }

        IType Constructor { get; }

        IType[] Arguments { get; }

        IType Self { get; }

        IType Value { get; }
    }
}