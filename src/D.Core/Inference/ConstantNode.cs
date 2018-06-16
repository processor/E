// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System;
using System.Collections.Generic;

namespace D.Inference
{
    public sealed class ConstantNode : Node
    {
        public ConstantNode(IType spec)
        {
            Spec = spec ?? throw new ArgumentNullException(nameof(spec));
        }

        public override IType Infer(Environment env, IReadOnlyList<IType> types)
        {
            switch (Spec)
            {
                case IType type  : return type;
                case string name : return env[name]; 
            }

            throw new Exception("ConstantNode must be a type or name");
        }

        public override string ToString() => "{ " + Spec + " }";
    }
}