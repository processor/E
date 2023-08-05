// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;

namespace E.Inference;

public interface INode
{
    IType Infer(Environment env, ReadOnlySpan<IType> types);
}