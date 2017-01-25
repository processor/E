namespace D
{
    public class Protocal : IObject
    {  
        public Protocal(Symbol name, Function[] members)
        {
            Name = name;
            Members = members;
        }

        public Symbol Name { get; set; }

        public Function[] Members { get; }

        // public IMessageDeclaration[] Channel { get; set; }

        Kind IObject.Kind => Kind.Protocal;
    }
}