namespace D.Expressions
{
    public class ProtocolMessage : IMessageDeclaration
    {
        // name
        // label
        // Optional
        // Fallsthrough?
        // Repeats

        public ProtocolMessage(string name, string label, MessageFlags flags)
        {
            Name = name;
            Label = label;
            Flags = flags;
        }

        public string Name { get; }

        public string Label { get; }

        public MessageFlags Flags { get; }

        public bool Optional
            => Flags.HasFlag(MessageFlags.Optional);

        public bool Repeats 
            => Flags.HasFlag(MessageFlags.Repeats);

        public bool Fallthrough 
            => Flags.HasFlag(MessageFlags.Fallthrough);

        public bool IsEnd
            => Flags.HasFlag(MessageFlags.End);
    }


    public enum MessageFlags
    {
        None        = 0,
        Repeats     = 1 << 1,
        Fallthrough = 1 << 2,
        Optional    = 1 << 3,
        End         = 1 << 4

    }
}


/*
type Person = {
  name: String where length > 0
}
*/
