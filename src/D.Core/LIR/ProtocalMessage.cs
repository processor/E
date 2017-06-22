namespace D.Expressions
{
    // A protocol { }

    /*
    ∙ | open       `Account       
      | close      `Account     
      | settle     `Transaction
      | refuse     `Transaction 
      | underwrite `Loan        
      | process    `Transaction 
      ↺            : acting
    ∙ dissolve ∎   : dissolved
    */

    public interface IMessageDeclaration
    {
        bool Fallthrough { get; }
    }

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
