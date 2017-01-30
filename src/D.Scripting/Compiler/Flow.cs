using System.Collections.Generic;

namespace D.Inference
{    
    public class Flow
    {
        private readonly ITypeSystem system = TypeSystem.Default;
       
        private readonly Dictionary<string, IType> env = new Dictionary<string, IType>();
        
        public static long kindId = 1000000;

        private IType any;
        private IType listType;
        private IType itemType;

        public Flow()
        {
            any = system.NewGeneric();
            var binary = system.NewGeneric();
            var b = GetType(Kind.Boolean);

            itemType = system.NewGeneric();
            listType = system.NewType("List", new[] { itemType });

            system.Infer(env, new Let("<", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, b, new Constant(b))));
            system.Infer(env, new Let(">", new FuncNode(new[] { new Variable("left", binary), new Variable("right", binary) }, b, new Constant(b))));

            system.Infer(env, new Let("contains", new FuncNode(new[] { new Variable("list", listType) }, new Constant(b))));

            system.Infer(env, new Let("head", new FuncNode(new[] { new Variable("list", listType) }, itemType, new Constant(itemType))));

            // A ternary if-then-else will come in handy too
            var ifThenElse = system.NewGeneric();

            system.Infer(env, new Let("if",
                new FuncNode(new[] {
                    new Variable("condition", b),
                    new Variable("then", ifThenElse),
                    new Variable("else", ifThenElse) }, 
                ifThenElse, new Variable("then"))));
        }

        public ITypeSystem System => system;

        public IType GetListTypeOf(Kind elementKind)
        {
            var item = GetType(elementKind);

            return system.NewType(listType, new[] { item });
        }

        private IType GetType(Kind kind)
        {
            return GetType(new Type(kind));
        }

        private IType GetType(IType kind)
        {
            if (kind.Name == "Any")
            {
                return any;
            }

            if (!env.TryGetValue(kind.Name, out IType type))
            {
                type = system.NewType(kind.Name);

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

                nodes[i] = parameter.Type.Name != "Any"
                    ? new Variable(parameter.Name, GetType(parameter.Type))
                    : new Variable(parameter.Name);
            }

            //  Variable("left", numberType),

            var func = new FuncNode(nodes, new Constant(GetType(new Type(returnKind))));

            system.Infer(env, new Let(name, func));
        }

        public void AddFunction(string name, Parameter[] parameters, Node returns)
        {
            var nodes = new Node[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                nodes[i] = parameter.Type.Name != "Any"
                    ? new Variable(parameter.Name, GetType(parameter.Type))
                    : new Variable(parameter.Name);
            }

            //  Variable("left", numberType),

            var func = new FuncNode(nodes, returns);

            system.Infer(env, new Let(name, func));
        }

        public void AddVariable(string name, Kind kind)
        {
            var type = GetType(new Type(kind));

            var node = new Let(name, new Constant(type));
            
            system.Infer(env, node);

            // nodes.Add(new Let(variable.Name, Node.Constant(variable.Type)));
        }

        public IType Infer(Node node)
            => system.Infer(env, node);
    }
}
