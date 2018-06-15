// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;
using System.Linq;

namespace D.Inference
{
    public sealed class LetNode : Node
    {
        public override string ToString()
        {
            var args = string.Join<Node>("; ", Arguments);

            return $"({args}) {Body}";
        }

        public override IType Infer(Environment env, IReadOnlyList<IType> types)
        {
            env = env.Nested();

            return Arguments.Select(define => TypeSystem.Infer(env, define, types)).Concat(new[] {
                TypeSystem.Infer(env, Body, types)
            }).Last();
        }
    }

    // let a = TYPE
}