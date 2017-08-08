using System;

namespace D.Inference
{    
    public class Flow
    {
        private readonly ITypeSystem system = new TypeSystem();

        private readonly IEnvironment scope = new Environment();

        public static long kindId = 1000000;

        private readonly IType listType;
        private readonly IType itemType;
        private readonly IType any;

        public Flow()
        {
            var binary   = system.NewGeneric();
            var boolType = GetType(Kind.Boolean);

            any = system.NewType(id: "Object");
            
            itemType = system.NewGeneric();
            listType = system.NewType("List", args: new[] { itemType });
       
            system.Infer(scope, Node.Define(Node.Var("<"), Node.Abstract(new[] {
                Node.Var("lhs", binary),
                Node.Var("rhs", binary)
            }, boolType, Node.Const(boolType))));

            system.Infer(scope, Node.Define(Node.Var(">"), Node.Abstract(new[] {
                Node.Var("lhs", binary),
                Node.Var("rhs", binary)
            }, boolType, Node.Const(boolType))));

            system.Infer(scope, Node.Define(Node.Var("contains"), Node.Abstract(new[] {
                Node.Var("list", listType)
            }, Node.Const(boolType))));

            system.Infer(scope, Node.Define(Node.Var("head"), Node.Abstract(new[] {
                Node.Var("list", listType)
            }, itemType, Node.Const(itemType))));
            
            var ifThenElse = system.NewGeneric();

            system.Infer(scope, Node.Define(Node.Var("if"), Node.Abstract(new[] {
                Node.Var("condition", boolType),
                Node.Var("then", ifThenElse),
                Node.Var("else", ifThenElse) }, 
            ifThenElse, Node.Var("then"))));
        }

        public IType NewGeneric() => system.NewGeneric();

        public ITypeSystem System => system;

        public IType GetListTypeOf(Kind elementKind)
        {
            var item = GetType(elementKind);

            return system.NewType(listType, new[] { item });
        }

        public IType GetType(Kind kind)
        {
            return GetType(new Type(kind));
        }

        private IType GetType(Type kind)
        {
            #region Preconditions

            if (kind == null)
                throw new ArgumentNullException(nameof(kind));

            #endregion
            
            if (kind.Name == "Object")
            {
                return any;
            }

            if (!scope.TryGetValue(kind.Name, out IType type))
            {
                if (kind.BaseType != null)
                {
                    type = system.NewType(
                        constructor : GetType(kind.BaseType), 
                        id          : kind.Name,
                        args: null,
                        meta: null
                    );
                }
                else
                {
                    type = system.NewType(id: kind.Name);
                }

                scope[kind.Name] = type;
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

            system.Infer(scope, Node.Define(Node.Var(name), Node.Abstract(
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
            system.Infer(scope, Node.Define(Node.Var(name), Node.Abstract(args, body)));
        }

        public void AddVariable(string name, Kind kind)
        {
            var type = GetType(kind);

            // system.Infer(scope, Node.Var(name, type));

            system.Infer(scope, Node.Define(Node.Var(name), Node.Const(type)));

            // nodes.Add(new Let(variable.Name, Node.Constant(variable.Type)));
        }

        public IType Infer(Node node)
        {
            return system.Infer(scope, node);
        }
    }
}
