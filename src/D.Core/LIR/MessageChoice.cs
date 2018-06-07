namespace D.Expressions
{
    public class MessageChoice : IMessageDeclaration
    {
        public MessageChoice(ProtocolMessage[] options, MessageFlags flags)
        {
            Options = options;
            Flags   = flags;
        }

        public ProtocolMessage[] Options { get; }

        public ProtocolMessage this[int index] => Options[index];

        public int Count => Options.Length;

        public MessageFlags Flags { get; }

        public bool Fallthrough 
            => Flags.HasFlag(MessageFlags.Fallthrough);

        public bool Repeats
            => Flags.HasFlag(MessageFlags.Repeats);

        public bool IsEnd 
            => Flags.HasFlag(MessageFlags.End);

    }
}


/*
type Person = {
  name: String where length > 0
}
*/
