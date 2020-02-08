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
            var binary = TypeSystem.NewGeneric();
            var boolean = Add(Type.Get(ObjectType.Boolean));
            var i32 = Add(Type.Get(ObjectType.Int32));
            var @string = Add(Type.Get(ObjectType.String));

            Add(Type.Get(ObjectType.Int64));
            Add(Type.Get(ObjectType.Float32));
            Add(Type.Get(ObjectType.Float64));
            Add(Type.Get(ObjectType.Decimal));
            Add(Type.Get(ObjectType.Number));

            any = GetType(ObjectType.Object);

            itemType = TypeSystem.NewGeneric();
            listType = TypeSystem.NewType("List", args: new[] { itemType });

            env.Infer(Node.Define(Node.Variable("contains"), Node.Abstract(new[] {
                Node.Variable("list", listType)
            }, Node.Constant(boolean))));

            env.Infer(Node.Define(Node.Variable("head"), Node.Abstract(new[] {
                Node.Variable("list", listType)
            }, itemType, Node.Constant(itemType))));

            // Binary Operators
            foreach (var op in new[] { "+", "-", "/", "**", "*", "%" })
            {
                var g = TypeSystem.NewGeneric();

                TypeSystem.Infer(env, Node.Define(Node.Variable(op), Node.Abstract(new[] {
                    Node.Variable("lhs", g),
                    Node.Variable("rhs", g)
                }, Node.Constant(g))));
            }

            // Comparisions
            foreach (var op in new[] { ">", ">=", "==", "!=", "<", "<=" })
            {
                var g = TypeSystem.NewGeneric();

                env.Infer(Node.Define(Node.Variable(op), Node.Abstract(new[] {
                    Node.Variable("lhs", g),
                    Node.Variable("rhs", g)
                }, Node.Constant(boolean))));
            }

            var ifThenElse = TypeSystem.NewGeneric();

            env.Infer(Node.Define(Node.Variable("if"), Node.Abstract(new[] {
                Node.Variable("condition", boolean),
                Node.Variable("then", ifThenElse),
                Node.Variable("else", ifThenElse) },
            ifThenElse, Node.Variable("then"))));

            // ! {expression}
            env.Infer(Node.Define(Node.Variable("!"), Node.Abstract(new[] {
                Node.Variable("expression", boolean)
            }, boolean, Node.Constant(boolean))));
        }

        public IType NewGeneric() => TypeSystem.NewGeneric();

        public IType GetListTypeOf(ObjectType elementKind)
        {
            var item = GetType(elementKind);

            return TypeSystem.NewType(listType, "List<" + elementKind.ToString() + ">", new[] { item });
        }

        public IType GetType(ObjectType kind) => GetType(new Type(kind));

        private IType GetType(Type kind)
        {
            if (!env.TryGetValue(kind.Name, out IType type))
            {
                type = Add(kind);
            }

            return type;
        }

        public IType Add(Type kind)
        {
            IType type;

            if (kind.BaseType != null)
            {
                type = TypeSystem.NewType(
                    constructor: GetType(kind.BaseType),
                    id: kind.Name,
                    args: null
                );
            }
            else
            {
                type = TypeSystem.NewType(id: kind.Name, args: null);
            }

            env[kind.Name] = type;

            return type;
        }

        public void AddFunction(string name, Parameter[] parameters, ObjectType returnKind)
        {
            var nodes = new Node[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Parameter parameter = parameters[i];

                nodes[i] = Node.Variable(parameter.Name ?? "_" + i, GetType(parameter.Type));
            }

            var type = GetType(new Type(returnKind));

            TypeSystem.Infer(env, Node.Define(Node.Variable(name), Node.Abstract(
                nodes,
                type: type,
                body: Node.Constant(type))
           ));
        }

        public void AddFunction(string name, Parameter[] parameters, Node body)
        {
            var nodes = new Node[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Parameter parameter = parameters[i];

                nodes[i] = Node.Variable(parameter.Name ?? "_" + i, GetType(parameter.Type));
            }

            AddFunction(name, nodes, body);
        }

        public void AddFunction(string name, Node[] args, Node body)
        {
            env.Infer(Node.Define(Node.Variable(name), Node.Abstract(args, body)));
        }

        public VariableNode Define(string name, Type type)
        {
            var typeNode = GetType(type);

            // system.Infer(scope, Node.Var(name, type));

            var variable = Node.Variable(name);

            Infer(Node.Define(variable, Node.Constant(typeNode)));

            // nodes.Add(new Let(variable.Name, Node.Constant(variable.Type)));

            return variable;
        }


        public IType Assign(VariableNode variable, Type type)
        {
            return TypeSystem.Infer(env, Node.Define(variable, Node.Constant(GetType(type))));
        }

        public IType Evaluate(Node node)
        {
            return TypeSystem.Infer(env, node);
        }

        public IType Infer(Node node) => TypeSystem.Infer(env, node);

        public IType Infer(string name) => TypeSystem.Infer(env, Node.Variable(name));
    }
}
