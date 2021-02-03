namespace E.Protocols
{
    public sealed class ProtocolMessage : IProtocolMessage
    {
        // name
        // label
        // Optional
        // Fallsthrough?
        // Repeats

        public ProtocolMessage(string name, string? label, ProtocolMessageFlags flags)
        {
            Name  = name;
            Label = label;
            Flags = flags;
        }

        public string Name { get; }

        public string? Label { get; }

        public ProtocolMessageFlags Flags { get; }

        public bool IsOptional => Flags.HasFlag(ProtocolMessageFlags.Optional);

        public bool Repeats => Flags.HasFlag(ProtocolMessageFlags.Repeats);

        public bool Fallthrough => Flags.HasFlag(ProtocolMessageFlags.Fallthrough);

        public bool IsEnd => Flags.HasFlag(ProtocolMessageFlags.End);
    }
}