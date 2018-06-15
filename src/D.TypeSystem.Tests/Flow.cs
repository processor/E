using System;

namespace D.Inference
{
    public class Flow
    {
        private readonly Environment env = new Environment();

        public static long kindId = 1000000;

        private readonly IType listType;
        private readonly IType itemType;
        private readonly IType any;

        public Flow()
        {
            var binary   = TypeSystem.NewGeneric();
            var boolean  = GetType(Kind.Boolean);
            var i32      = GetType(Kind.Int32);
            var @string  = GetType(Kind.String);

            any = GetType(Kind.Object);

            itemType = TypeSystem.NewGeneric();
            listType = TypeSystem.NewType("List", args: new[] { itemType });

            TypeSystem.Infer(env, Node.Define(Node.Var("<"), Node.Abstract(new[] {
                Node.Var("lhs", binary),
                Node.Var("rhs", binary)
            }, boolean, Node.Const(boolean))));

          
            TypeSystem.Infer(env, Node.Define(Node.Var("contains"), Node.Abstract(new[] {
                Node.Var("list", listType)
            }, Node.Const(boolean))));

            TypeSystem.Infer(env, Node.Define(Node.Var("head"), Node.Abstract(new[] {
                Node.Var("list", listType)
            }, itemType, Node.Const(itemType))));


            // Binary Operators
            foreach (var op in new[] { "+", "-", "/", "**", "*", "%" })
            {
                var g = TypeSystem.NewGeneric();

                TypeSystem.Infer(env, Node.Define(Node.Var(op), Node.Abstract(new[] {
                    Node.Var("lhs", g),
                    Node.Var("rhs", g)
                }, Node.Const(g))));
            }

            // Comparisions
            foreach (var op in new[] { ">", ">=", "==", "!=", "<=" })
            {
                var g = TypeSystem.NewGeneric();

                TypeSystem.Infer(env, Node.Define(Node.Var(op), Node.Abstract(new[] {
                    Node.Var("lhs", g),
                    Node.Var("rhs", g)
                }, Node.Const(boolean))));
            }




            var ifThenElse = TypeSystem.NewGeneric();

            TypeSystem.Infer(env, Node.Define(Node.Var("if"), Node.Abstract(new[] {
                Node.Var("condition", boolean),
                Node.Var("then", ifThenElse),
                Node.Var("else", ifThenElse) }, 
            ifThenElse, Node.Var("then"))));

            // ! {expression}
            TypeSystem.Infer(env, Node.Define(Node.Var("!"), Node.Abstract(new[] {
                Node.Var("expression", boolean)
            }, boolean, Node.Const(boolean))));
        }

        public IType NewGeneric() => TypeSystem.NewGeneric();

        public IType GetListTypeOf(Kind elementKind)
        {
            var item = GetType(elementKind);

            return TypeSystem.NewType(listType, "List<" + elementKind.ToString() + ">", new[] { item });
        }

        public IType GetType(Kind kind)
        {
            return GetType(new Type(kind));
        }

        private IType GetType(Type kind)
        {
            if (kind == null) throw new ArgumentNullException(nameof(kind));
           
            if (!env.TryGetValue(kind.Name, out IType type))
            {
                if (kind.BaseType != null)
                {
                    type = TypeSystem.NewType(
                        constructor : GetType(kind.BaseType), 
                        id          : kind.Name,
                        args        : null
                    );
                }
                else
                {
                    type = TypeSystem.NewType(id: kind.Name, args: null);
                }

                env[kind.Name] = type;
            }

            return type;
        }

        public void AddFunction(string name, Parameter[] parameters, Kind returnKind)
        {
            var nodes = new Node[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                nodes[i] = Node.Var(parameter.Name, GetType(parameter.Type));
            }

            var type = GetType(new Type(returnKind));

            TypeSystem.Infer(env, Node.Define(Node.Var(name), Node.Abstract(
                nodes, 
                type: type, 
                body: Node.Const(type))
           ));
        }

        public void AddFunction(string name, Parameter[] parameters, Node body)
        {
            var nodes = new Node[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                nodes[i] = Node.Var(parameter.Name, GetType(parameter.Type));
            }

            AddFunction(name, nodes, body);
        }

        public void AddFunction(string name, Node[] args, Node body)
        {
            TypeSystem.Infer(env, Node.Define(Node.Var(name), Node.Abstract(args, body)));
        }

        
        public VarNode AddVariable(string name, Kind kind)
        {
            var type = GetType(kind);

            // system.Infer(scope, Node.Var(name, type));

            var variable = Node.Var(name);

            var a = Node.Define(variable, Node.Const(type));

            TypeSystem.Infer(env, a);

            // nodes.Add(new Let(variable.Name, Node.Constant(variable.Type)));

            return variable;
        }

        public IType Infer(Node node)
        {
            return TypeSystem.Infer(env, node);
        }
    }
}
