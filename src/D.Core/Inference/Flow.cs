namespace E.Inference;

public class Flow
{
    // TODO: Merge Node & Environment

    private readonly Environment env = new ();
    public static long kindId = 1_000_000;

    private readonly IType listType;
    private readonly IType itemType;
    private readonly IType any;


    private static readonly string[] s_binaryOperatorSymbols = [ "+", "-", "/", "**", "*", "%" ];
    private static readonly string[] s_comparisonSymbols     = [ ">", ">=", "==", "!=", "<", "<=" ];

    public Flow()
    {
        // Base Types
        GetType(ObjectType.Int32);
        GetType(ObjectType.Int64);
        GetType(ObjectType.String);
        GetType(ObjectType.Float32);
        GetType(ObjectType.Float64);
        GetType(ObjectType.String);
        GetType(ObjectType.Decimal);
        GetType(ObjectType.Number);

        var boolean = Add(Type.Get(ObjectType.Boolean));

        any = GetType(ObjectType.Object);

        itemType = TypeSystem.NewGeneric();
        listType = TypeSystem.NewType(KnownTypeNames.List, args: new[] { itemType });

        TypeSystem.Infer(env, new DefineNode(new VariableNode("contains"), Node.Abstract(new[] {
            new VariableNode("list", listType)
        }, new ConstantNode(boolean))));

        TypeSystem.Infer(env, new DefineNode(new VariableNode("head"), Node.Abstract(new[] {
            new VariableNode("list", listType)
        }, new ConstantNode(itemType), itemType)));

        // Binary Operators
        foreach (var op in s_binaryOperatorSymbols)
        {
            var g = TypeSystem.NewGeneric();

            TypeSystem.Infer(env, new DefineNode(new VariableNode(op), Node.Abstract(new[] {
                new VariableNode("lhs", g),
                new VariableNode("rhs", g)
            }, new ConstantNode(g))));
        }

        // Comparisons
        foreach (var op in s_comparisonSymbols)
        {
            var g = TypeSystem.NewGeneric();

            TypeSystem.Infer(env, new DefineNode(new VariableNode(op), Node.Abstract(new[] {
                new VariableNode("lhs", g),
                new VariableNode("rhs", g)
            }, new ConstantNode(boolean))));
        }

        var ifThenElse = TypeSystem.NewGeneric();

        TypeSystem.Infer(env, new DefineNode(new VariableNode("if"), Node.Abstract(new[] {
            new VariableNode("condition", boolean),
            new VariableNode("then", ifThenElse),
            new VariableNode("else", ifThenElse) },
            new VariableNode("then"), ifThenElse))
        );

        // ! {expression}
        TypeSystem.Infer(env, new DefineNode(new VariableNode("!"), Node.Abstract(new[] {
            new VariableNode("expression", boolean)
        }, new ConstantNode(boolean), boolean)));
    }

    public IType NewGeneric() => TypeSystem.NewGeneric();

    public IType GetListTypeOf(ObjectType elementKind)
    {
        var item = GetType(elementKind);

        return TypeSystem.NewType(listType, $"List<{elementKind}>", new[] { item });
    }

    public IType GetType(ObjectType kind) => GetType(new Type(kind));

    private IType GetType(Type kind)
    {
        if (!env.TryGetValue(kind.Name, out IType? type))
        {
            type = Add(kind);
        }

        return type;
    }

    public IType Add(Type kind)
    {
        IType type;

        if (kind.BaseType is not null)
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


        // Alias HACKs
        if (kind.Name is KnownTypeNames.Int32)
        {
            env["i32"] = type;
        }
        else if (kind.Name is KnownTypeNames.Int64)
        {
            env["i64"] = type;
        }
        else if (kind.Name is KnownTypeNames.Float64)
        {
            env["f64"] = type;
        }
        else if (kind.Name is KnownTypeNames.Float32)
        {
            env["f32"] = type;
        }

        return type;
    }

    public void AddFunction(string name, Parameter[] parameters, ObjectType returnKind)
    {
        var nodes = new VariableNode[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            Parameter parameter = parameters[i];

            nodes[i] = new VariableNode(parameter.Name ?? $"_{i}", GetType(parameter.Type));
        }

        var type = GetType(new Type(returnKind));

        TypeSystem.Infer(env, new DefineNode(new VariableNode(name), Node.Abstract(
            nodes,
            type: type,
            body: new ConstantNode(type))
       ));
    }

    public void AddFunction(string name, Parameter[] parameters, INode body)
    {
        var nodes = new VariableNode[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            Parameter parameter = parameters[i];

            nodes[i] = new VariableNode(parameter.Name ?? $"_{i}", GetType(parameter.Type));
        }

        AddFunction(name, nodes, body);
    }

    public void AddFunction(string name, VariableNode[] args, INode body)
    {
        TypeSystem.Infer(env, new DefineNode(new VariableNode(name), Node.Abstract(args, body)));
    }

    public VariableNode Define(string name, Type type)
    {
        var typeNode = GetType(type);

        // system.Infer(scope, Node.Var(name, type));

        var variable = new VariableNode(name);

        Infer(new DefineNode(variable, new ConstantNode(typeNode)));

        // nodes.Add(new Let(variable.Name, Node.Constant(variable.Type)));

        return variable;
    }


    public IType Assign(VariableNode variable, Type type)
    {
        return TypeSystem.Infer(env, new DefineNode(variable, new ConstantNode(GetType(type))));
    }

    public IType Evaluate(INode node)
    {
        return TypeSystem.Infer(env, node);
    }

    public IType? Infer(INode node) => TypeSystem.Infer(env, node);

    public IType Infer(string name) => TypeSystem.Infer(env, new VariableNode(name));
}
