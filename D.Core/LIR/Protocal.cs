namespace D
{
    public class Protocal : IObject
    {  
        public Protocal(Symbol name, Function[] members)
        {
            Domain = name.Domain;
            Name = name.Name;
            Members = members;
        }

        public string Domain { get; set; }

        public string Name { get; set; }

        public Function[] Members { get; }

        // StateMachine

        Kind IObject.Kind => Kind.Protocal;
    }
}